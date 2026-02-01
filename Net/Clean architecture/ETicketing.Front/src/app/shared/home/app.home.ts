import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NavOptionModel, SharedService } from '../../core/services/shared/shared.service';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './app.home.html',
  styleUrls: ['./app.home.scss']
})
export class HomeComponent {

  constructor(public _shs: SharedService) {
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
