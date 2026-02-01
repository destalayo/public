import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject } from "rxjs";

@Injectable({ providedIn: 'root' })
export class SharedService {


  private _data = new SharedData();
  private stateChanged = new BehaviorSubject<SharedData>(this._data);

  stateChanged$ = this.stateChanged.asObservable();
  constructor(private router: Router) { }


  get data() {
    return { ...this._data };
  }
  set data(sharedData) {
    const newData = { ...sharedData };
    this._data = newData;
    queueMicrotask(() => { this.stateChanged.next(newData); });
  }

  setLoading(loading:boolean) {
    let sharedData = this.data;
    sharedData.isLoading = loading;
    this.data = sharedData;
  }

  setLoggued(logued: boolean) {
    let sharedData = this.data;
    sharedData.isLogged = logued;
    this.data = sharedData;
  }

  navigate(ruta: string): void {
    let sharedData = this.data;
    sharedData.isNavigation = true;
    this.data = sharedData;
    setTimeout(() => {
      let sharedData = this.data;
      sharedData.isNavigation = false;
      this.data = sharedData;
      this.router.navigate([ruta]);
    }, 650);
  }
  async tryCatchLoading<T>(p: () => T | Promise<T>): Promise<T | undefined> {
    try
    {
      this.setLoading(true);
      return await p();
    }
    catch (err: unknown)
    {
      return undefined;
    }
    finally {
      this.setLoading(false);
    }
  }
}
export class SharedData {
  public isNavigation: boolean;
  public isLoading: boolean;
  public isLogged: boolean;
  public navOptions: NavOptionModel[] = [
    {
      icon: 'fa-solid fa-calendar-check', title: 'Sesiones', path:'seasons'
    },
    {
      icon: 'fa-solid fa-tent', title: 'Salas', path: 'rooms'
    },
    {
      icon: 'fa-solid fa-users', title: 'Usuarios', path: 'users'
    },
    {
      icon: 'fa-solid fa-file-signature', title: 'Reservas', path: 'reservations'
    }
  ];

}
export class NavOptionModel {
  public icon?: string | null = null;
  public title?: string | null = null;
  public path?: string | null = null;
  public selected?: boolean = false;
}
