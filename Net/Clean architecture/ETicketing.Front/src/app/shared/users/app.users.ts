import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IUser } from '../../core/domain/user';
import { ApiService } from '../../core/services/api.service';
import { SharedService } from '../../core/services/shared/shared.service';

@Component({
  selector: 'app-users',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './app.users.html',
  styleUrls: ['./app.users.scss']
})
export class UsersComponent implements OnInit{
  
  users: IUser[] = [];
  constructor(public _shs: SharedService, private _api: ApiService) {
  }
  async ngOnInit() {
    await this._shs.tryCatchLoading(async () => {
      this.users = (await this._api.getUsers().toAsync()).data;
    });
  }
  
}

