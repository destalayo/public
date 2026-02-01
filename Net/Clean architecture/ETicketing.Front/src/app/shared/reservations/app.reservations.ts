import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IReservation } from '../../core/domain/reservation';
import { ISeason } from '../../core/domain/season';
import { ISeat } from '../../core/domain/seat';
import { IUser } from '../../core/domain/user';
import { ApiService } from '../../core/services/api.service';
import { SharedService } from '../../core/services/shared/shared.service';
import { ToolService } from '../../core/services/shared/tool.service';
import { VButtonComponent } from '../angular/button/app.button';
import { VFormularyComponent } from '../angular/formulary/app.formulary';
import { VInputCheckComponent } from '../angular/input-check/app.input-check';
import { VModalComponent } from '../angular/modal/app.modal';
import { VSelectComponent } from '../angular/select/app.select';

@Component({
  selector: 'app-reservations',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, VModalComponent, VFormularyComponent, VButtonComponent, VInputCheckComponent, VSelectComponent],
  templateUrl: './app.reservations.html',
  styleUrls: ['./app.reservations.scss']
})
export class ReservationsComponent implements OnInit{
  @ViewChild('modalAdd') modalAdd!: VModalComponent;
  addForm!: FormGroup;
  editForm!: FormGroup;
  reservations: IReservation[] = [];
  seasons: ISeason[] = [];
  users: IUser[] = [];
  seats: ISeat[] = [];
  seatsByRoom: { seat: ISeat, reserved:boolean, selected: boolean }[][] = [];
  constructor(public _shs: SharedService, private fb: FormBuilder, private _api: ApiService, private _tool: ToolService) {
    this.addForm = this.fb.group(
      {
        seasonId: ['', Validators.required]
      });
  }
  async ngOnInit() {
    await this._shs.tryCatchLoading(async () => {
      const [reservations, seasons, users, seats] = await Promise.all([
        this._api.getReservations().toAsync(),
        this._api.getSeasons().toAsync(),
        this._api.getUsers().toAsync(),
        this._api.getSeats().toAsync()
      ]);
      this.users = users.data;
      this.seasons = seasons.data;
      this.reservations = reservations.data;
      this.seats = seats.data;
    });
  }
  getSeasonName(id: string) {
    return this.seasons.find(x => x.id == id).name
  }
  getUserName(id: string) {
    return this.users.find(x => x.id == id).id
  }
  getSeatName(id: string) {
    const seat = this.seats.find(x => x.id == id);
    return `${seat.row}:${seat.column}`
  }
  async onSeasonChange(seasonId: string) {
    this.seatsByRoom = [];
    const season: ISeason = this.seasons.find(x => x.id == seasonId);

    await this._shs.tryCatchLoading(async () => {

      const reservedSeatsByRoom = (await this._api.getSeatsBySeason(season.id).toAsync()).data;

      const seats: ISeat[] = this.seats.filter(x => x.roomId == season.roomId);
      const maxRow = Math.max(...seats.map(x => x.row));
      const maxColumn = Math.max(...seats.map(x => x.column));
      for (let row = 1; row <= maxRow; row++) {
        let rows = [];       
          for (let column = 1; column <= maxColumn; column++) {
          const seat = this.seats.find(x => x.column == column && x.row == row);
          if (seat != null) {
            const reserved = reservedSeatsByRoom.some(x=>x==seat.id);
            rows.push({ reserved: reserved, selected: reserved, seat :seat});
          }
        }
        if (rows.length>0) {
          this.seatsByRoom.push(rows);
        }
      }
    }); 
  }
  addModal() {
    this.seatsByRoom = [];
    this.addForm.reset();
    this.modalAdd.show = true;
  }

  async add() {
    await this._shs.tryCatchLoading(async () => {

      const selected: string[] = [];

      this.seatsByRoom.forEach(x => {
        x.forEach(y => {
          if (!y.reserved && y.selected) {
            selected.push(y.seat.id);
          }
        });
      });

      const response = await this._api.createReservation({ ...this.addForm.value, seatIds: selected }).toAsync();

      this.seatsByRoom.forEach(x => {
        x.forEach(y => {
          if (response.data.some(z => z.seatId == y.seat.id)) {
            y.reserved = true;
            y.selected = true;
          }
        });
      });

      response.data.forEach(x => {
        this.reservations.push(x);
      });

      this.modalAdd.show = false;
    });
  }
  async delete(item: any) {
    await this._shs.tryCatchLoading(async () => {
      await this._api.deleteReservation(item.id).toAsync();
      this._tool.removeOnArray(this.reservations, item.id);
    });
  }
}
