import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth/auth.service';
import { NavOptionModel, SharedService } from '../../core/services/shared/shared.service';

@Component({
  selector: 'app-nav',
  imports: [CommonModule],
  templateUrl: './app.nav.html',
  styleUrls: ['./app.nav.scss'],
})
export class NavComponent implements OnInit {
  constructor(public _shs: SharedService, private auth:AuthService)  {}  

  userName: string;
  ngOnInit() {
    this.userName=this.auth.decodeJwt().sub;
  }
  logout() {
    this.auth.logout();
  }

  goto(item: NavOptionModel) {
    let options = this._shs.data.navOptions;

    options.forEach(x => {

      if (x == item) {
        x.selected = true;
      }
      else {
        x.selected = false;
      }
    });
    this._shs.data.navOptions = options;
    this._shs.navigate(item.path);
  }



}
