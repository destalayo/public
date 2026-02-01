import { firstValueFrom, Observable } from 'rxjs';

declare module 'rxjs' {
  interface Observable<T> {
    toAsync(): Promise<T>;
  }
}

Observable.prototype.toAsync = function <T>(): Promise<T> {
  return firstValueFrom(this as Observable<T>);
};
