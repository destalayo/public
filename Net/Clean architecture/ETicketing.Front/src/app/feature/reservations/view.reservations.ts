import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReservationsComponent } from '../../shared/reservations/app.reservations';

@Component({
  selector: 'view-reservations',
  imports: [CommonModule, ReservationsComponent],
  templateUrl: './view.reservations.html',
  styleUrl: './view.reservations.scss',
})
export class ViewReservationsComponent {
 
}
