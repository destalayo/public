import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SharedService } from '../../core/services/shared/shared.service';
import { NavComponent } from '../../shared/nav/app.nav';
import { VSpinnerComponent } from '../angular/spinner/app.spinner';

@Component({
  selector: 'app-viewport',
  imports: [RouterOutlet, NavComponent, CommonModule, VSpinnerComponent],
  templateUrl: './app.viewport.html',
  styleUrl: './app.viewport.scss',
})
export class ViewportComponent {
  constructor(public _shs: SharedService) {

  }
}
