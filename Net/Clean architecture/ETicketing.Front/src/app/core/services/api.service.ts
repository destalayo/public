import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IReservation } from '../domain/reservation';
import { IRoom } from '../domain/room';
import { ISeason } from '../domain/season';
import { ISeat } from '../domain/seat';
import { ISession } from '../domain/session';
import { IUser } from '../domain/user';
import { IHttpResponse } from '../models/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  public readonly baseUrl = 'http://localhost:7115/v1';

  constructor(private http: HttpClient) { }

  // -------------------------
  // Sessions (login / refresh)
  // -------------------------

  createSession(data: { email: string, password:string }): Observable<IHttpResponse<ISession>> {
    return this.http.post<IHttpResponse<ISession>>(
      `${this.baseUrl}/sessions`,
      data
    );
  }

  updateSession(data: { refreshToken: string }): Observable<IHttpResponse<ISession>> {
    return this.http.put<IHttpResponse<ISession>>(
      `${this.baseUrl}/sessions`,
      data
    );
  }

  // -------------------------
  // Rooms
  // -------------------------

  getRooms(): Observable<IHttpResponse<IRoom[]>> {
    return this.http.get<IHttpResponse<IRoom[]>>(
      `${this.baseUrl}/rooms`
    );
  }

  createRoom(data: { id: string, name: string, rows: number, columns: number }): Observable<IHttpResponse<IRoom>> {
    return this.http.post<IHttpResponse<IRoom>>(
      `${this.baseUrl}/rooms`,
      data
    );
  }

  updateRoom(roomId: string, data: { name: string }): Observable<IHttpResponse<IRoom>> {
    return this.http.put<IHttpResponse<IRoom>>(
      `${this.baseUrl}/rooms/${roomId}`,
      data
    );
  }

  deleteRoom(id: string): Observable<void> {
    return this.http.delete<void>(
      `${this.baseUrl}/rooms/${id}`
    );
  }

  // -------------------------
  // Users
  // -------------------------

  getUsers(): Observable<IHttpResponse<IUser[]>> {
    return this.http.get<IHttpResponse<IUser[]>>(
      `${this.baseUrl}/users`
    );
  }

  // -------------------------
  // Seats
  // -------------------------

  getSeats(): Observable<IHttpResponse<ISeat[]>> {
    return this.http.get<IHttpResponse<ISeat[]>>(
      `${this.baseUrl}/seats`
    );
  }

  getSeatsBySeason(seasonId: string): Observable<IHttpResponse<string[]>> {
    return this.http.get<IHttpResponse<string[]>>(
      `${this.baseUrl}/seats/seasons/${seasonId}/reserved`
    );
  }

  // -------------------------
  // Reservations
  // -------------------------

  getReservations(): Observable<IHttpResponse<IReservation[]>> {
    return this.http.get<IHttpResponse<IReservation[]>>(
      `${this.baseUrl}/reservations`
    );
  }

  createReservation(data: { seasonId: string, seatIds: string[] }): Observable<IHttpResponse<IReservation[]>> {
    return this.http.post<IHttpResponse<IReservation[]>>(
      `${this.baseUrl}/reservations`,
      data
    );
  }

  deleteReservation(id: string): Observable<void> {
    return this.http.delete<void>(
      `${this.baseUrl}/reservations/${id}`
    );
  }

  // -------------------------
  // Seasons
  // -------------------------

  getSeasons(): Observable<IHttpResponse<ISeason[]>> {
    return this.http.get<IHttpResponse<ISeason[]>>(
      `${this.baseUrl}/seasons`
    );
  }

  createSeason(data: ISeason): Observable<IHttpResponse<ISeason>> {
    return this.http.post<IHttpResponse<ISeason>>(
      `${this.baseUrl}/seasons`,
      data
    );
  }

  updateSeason(seasonId: string, data: { name: string }): Observable<IHttpResponse<ISeason>> {
    return this.http.put<IHttpResponse<ISeason>>(
      `${this.baseUrl}/seasons/${seasonId}`,
      data
    );
  }

  deleteSeason(id: string): Observable<void> {
    return this.http.delete<void>(
      `${this.baseUrl}/seasons/${id}`
    );
  }
}

