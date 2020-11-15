import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { AuthToken } from './model/auth-token';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class LoginService {

  public auth_token: AuthToken;

  private auth_url = '/data/token';

  constructor(
    private http: HttpClient
  ) { }

  public async login(user: string, password: string): Promise<boolean>{
    try {

      let grant_type = "password"

      let body = `grant_type=${grant_type}&username=${user}&password=${password}`;

      return this.http.post(this.auth_url, body, { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
        .toPromise()
        .then(
                response => {
                    console.info(response);
                    
                    return true;
                },
                error => {
                    console.error("Erro request login: ");
                    console.error(error);
                    return false;
                }
            );

    }
    catch (e) {

      console.error(`[tokenExpired] Erro ao realizar login:`);
      console.error(e);
      return false;

    }
  }
}
