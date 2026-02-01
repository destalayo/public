export interface ISession {
  accessToken: string;
  refreshToken: string;
  accessTokenExpires: string;
}

export class Session implements ISession {
  constructor(public accessToken: string, public refreshToken: string, public accessTokenExpires: string) { }


}
