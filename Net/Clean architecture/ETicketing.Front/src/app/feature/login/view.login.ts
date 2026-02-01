import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { LoginComponent } from '../../shared/login/app.login';

@Component({
  selector: 'view-login',
  imports: [CommonModule, LoginComponent],
  templateUrl: './view.login.html',
  styleUrl: './view.login.scss',
})
export class ViewLoginComponent {
 
}
