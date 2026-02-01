import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class StorageService {

  saveDataString(key: string, value: string): void {
    localStorage.setItem(key, value);
  }
  readDataString(key:string): string {
    return localStorage.getItem(key);
  }
  saveDataObject<T>(key: string, value: T): void {
    this.saveDataString(key, JSON.stringify(value));
  }
  readDataObject<T>(key: string): T {
    return JSON.parse(localStorage.getItem(key));
  }
  readKeys(): string[] {
    let result: string[] = [];
    for (let i = 0; i < localStorage.length; i++) {
      result.push(localStorage.key(i));
    }
    return result;
  }
  deleteData(key: string): void {
    localStorage.removeItem(key);
  }
}
