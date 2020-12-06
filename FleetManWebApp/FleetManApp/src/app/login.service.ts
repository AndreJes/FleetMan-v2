import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { AuthToken } from './model/auth-token';
import { Observable } from 'rxjs';
import { CustomStatus } from './model/custom-status';
import { Cliente } from './model/cliente';

@Injectable({
  providedIn: 'root'
})

export class LoginService {

  private auth_token: AuthToken;

  private auth_url = '/data/token';

  private token_key = "sessionToken";

  constructor(
    private http: HttpClient
  ) { }

  public async login(user: string, password: string): Promise<boolean> {
    try {

      let session_token = this.getSessionToken();

      console.log(session_token);

      if (session_token) {
        this.auth_token = session_token;

        //returns true if current access token still valid
        if (!this.auth_token.Expired()) {
          return true
        }
      }

      let grant_type = "password"

      let body = `grant_type=${grant_type}&username=${user}&password=${password}`;

      return this.http.post(this.auth_url, body, { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
        .toPromise()
        .then(
          response => {
            console.info("Retornou novo Token de acesso...");
            let res = this.setSessionToken(new AuthToken(response["access_token"], response["expires_in"]));
            return res;
          },
          error => {
            console.error("Erro request login: ");
            console.error(error);
            return false;
          }
        );
    }
    catch (e) {

      console.error(`Erro ao realizar login:`);
      console.error(e);
      return false;

    }
  }

  public Status(): CustomStatus {
    if (!this.auth_token) {
      this.auth_token = this.getSessionToken();

      if (!this.auth_token) {
        return new CustomStatus(-1, "Invalid: No access token");
      }
    }

    if (this.auth_token.Expired()) {
      return new CustomStatus(-2, "Invalid: Token Expired");
    }

    return new CustomStatus(1, "Valid access token");
  }

  private setSessionToken(token: AuthToken): boolean {
    try {
      console.info("Gravando token na sessão..");
      sessionStorage.removeItem(this.token_key);
      sessionStorage.setItem(this.token_key, JSON.stringify(token));
      this.auth_token = token;
      return true;
    }
    catch (e) {
      console.error(e);
      return false;
    }
  }

  private getSessionToken(): AuthToken {
    try {
      let stored_token = JSON.parse(sessionStorage.getItem(this.token_key));

      if (stored_token) {
        return AuthToken.fromSession(stored_token);
      }
    }
    catch (e) {
      console.error(e);
      return null;
    }
  }

  public async logout(): Promise<boolean>{
    try {
      this.auth_token = null;
      sessionStorage.removeItem(this.token_key);
      return true;
    }
    catch (e) {
      console.error(e);
      return false;
    }
  }
}
