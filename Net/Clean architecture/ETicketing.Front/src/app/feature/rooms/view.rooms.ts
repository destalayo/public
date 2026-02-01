import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RoomsComponent } from '../../shared/rooms/app.rooms';

@Component({
  selector: 'view-rooms',
  imports: [CommonModule, RoomsComponent],
  templateUrl: './view.rooms.html',
  styleUrl: './view.rooms.scss',
})
export class ViewRoomsComponent {
 
}
