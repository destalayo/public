import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { SeasonsComponent } from '../../shared/seasons/app.seasons';

@Component({
  selector: 'view-seasons',
  imports: [CommonModule, SeasonsComponent],
  templateUrl: './view.seasons.html',
  styleUrl: './view.seasons.scss',
})
export class ViewSeasonsComponent {
 
}
