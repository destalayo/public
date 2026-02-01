import { Routes } from '@angular/router';
import { ViewHomeComponent } from './feature/home/view.home';
import { ViewportComponent } from './shared/viewport/app.viewport';
import { ViewErrorComponent } from './feature/error/view.error';
import { ViewSeasonsComponent } from './feature/seasons/view.seasons';
import { ViewUsersComponent } from './feature/users/view.users';
import { ViewReservationsComponent } from './feature/reservations/view.reservations';
import { ViewRoomsComponent } from './feature/rooms/view.rooms';
import { AuthGuard, ReverseAuthGuard } from './core/services/auth/guard.service';
import { ViewLoginComponent } from './feature/login/view.login';


export const routes: Routes = [
  {
    path: '', component: ViewportComponent, children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: ViewHomeComponent, canActivate: [AuthGuard] },
      { path: 'seasons', component: ViewSeasonsComponent, canActivate: [AuthGuard] },
      { path: 'rooms', component: ViewRoomsComponent, canActivate: [AuthGuard] },
      { path: 'reservations', component: ViewReservationsComponent, canActivate: [AuthGuard] },
      { path: 'users', component: ViewUsersComponent, canActivate: [AuthGuard] }
    ]
  },
  { path: 'login', component: ViewLoginComponent, canActivate: [ReverseAuthGuard] },
  { path: 'error', component: ViewErrorComponent },
  { path: '**', redirectTo: '/error', pathMatch: 'full' }
];
