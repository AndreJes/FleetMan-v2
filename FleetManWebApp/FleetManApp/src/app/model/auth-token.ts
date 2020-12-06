export class AuthToken {
  public token: string;
  private expires_in: number;
  public created_on: Date;

  constructor(token: string, expires_in: number, create_on?: Date) {
    this.token = token;
    //converts given seconds to miliseconds
    this.expires_in = expires_in * 1000;
    this.created_on = create_on ? create_on : new Date();
  }

  public Expired(): boolean {
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

      console.error(`Erro ao obter informação de token expirado:`);
      console.error(`${e}`)
      throw e;

    }
  }

  public static fromSession(session_token) {
    return new this(session_token.token, session_token.expires_in / 1000, new Date(session_token.created_on));
  }
}
