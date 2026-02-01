import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { UsersComponent } from '../../shared/users/app.users';

@Component({
  selector: 'view-users',
  imports: [CommonModule, UsersComponent],
  templateUrl: './view.users.html',
  styleUrl: './view.users.scss',
})
export class ViewUsersComponent {
 
}
