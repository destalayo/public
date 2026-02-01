import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SharedService } from "../shared/shared.service";
import { StorageService } from "../shared/storage.service";
import { ApiService } from "../api.service";

@Injectable({ providedIn: 'root' })
export class AuthService {

  private accessTokenKey = 'access_token';
  private refreshTokenKey = 'refresh_token';
  private accessTokenExpiresKey = 'accessTokenExpires';

  constructor(
    private http: HttpClient,
    private shared: SharedService,
    private storage: StorageService,
    private api:ApiService
  ) {
    this.shared.setLoggued(this.hasValidToken());
  }

  async login(username: string, password: string) {
    const response = await this.api.createSession({ email: username, password: password }).toAsync();
    this.saveTokens(response.data.accessToken, response.data.refreshToken, response.data.accessTokenExpires);
  }

  async logout() {
    this.storage.deleteData(this.accessTokenKey);
    this.storage.deleteData(this.refreshTokenKey);
    this.storage.deleteData(this.accessTokenExpiresKey);
    this.shared.setLoggued(false);
    this.shared.navigate('/login');
  }

  saveTokens(access: string, refresh: string, expire: string) {
    this.storage.saveDataString(this.accessTokenKey, access);
    this.storage.saveDataString(this.refreshTokenKey, refresh);
    this.storage.saveDataString(this.accessTokenExpiresKey, expire);
    this.shared.data.isLogged = true;
  }

  getAccessToken() {
    return this.storage.readDataString(this.accessTokenKey);
  }

  getRefreshToken() {
    return this.storage.readDataString(this.refreshTokenKey);
  }

  refreshToken() {
    return this.api.updateSession({ refreshToken: this.getRefreshToken() });
  }

  hasValidToken(): boolean {
    const token = this.storage.readDataString(this.accessTokenKey);
    const expires = this.storage.readDataString(this.accessTokenExpiresKey);

    if (!token || !expires) return false;

    const expiresAt = new Date(expires).getTime();
    const now = Date.now();

    return now < expiresAt;
  }
  decodeJwt(): any {
    try {
      const payload = this.getAccessToken().split('.')[1];
      const decoded = atob(payload);
      return JSON.parse(decoded);
    } catch {
      return null;
    }
  }

}
