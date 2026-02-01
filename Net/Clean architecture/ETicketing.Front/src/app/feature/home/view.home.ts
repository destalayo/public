import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { HomeComponent } from '../../shared/home/app.home';

@Component({
  selector: 'view-home',
  imports: [CommonModule, HomeComponent],
  templateUrl: './view.home.html',
  styleUrl: './view.home.scss',
})
export class ViewHomeComponent {
 
}
