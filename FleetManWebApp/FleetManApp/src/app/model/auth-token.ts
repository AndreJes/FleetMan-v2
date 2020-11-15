export class AuthToken {
  public token: string;
  private expires_in: number;
  public created_on: Date;

  constructor(token: string, expires_in: number) {
    this.token = token;
    this.expires_in = expires_in;
    this.created_on = new Date();
  }

  public TokenExpired(): boolean {
    try {

      let current_time = new Date();
      let limit = this.created_on.getTime() + this.expires_in;

      if (current_time.getTime() > limit) {
        return true;
      } else {
        return false;
      }

    }
    catch (e) {

      console.error(`[tokenExpired] Erro ao obter informação de token expirado:`);
      console.error(`${e}`)
      throw e;

    }
  }
}
