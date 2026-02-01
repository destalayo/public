import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { SharedService } from '../../core/services/shared/shared.service';
import { VModalComponent } from '../angular/modal/app.modal';
import { VButtonComponent } from '../angular/button/app.button';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { VInputTextComponent } from '../angular/input-text/app.input-text';
import { VFormularyComponent } from '../angular/formulary/app.formulary';
import { ApiService } from '../../core/services/api.service';
import { VSelectComponent } from '../angular/select/app.select';
import { IRoom } from '../../core/domain/room';
import { ISeason } from '../../core/domain/season';
import { ToolService } from '../../core/services/shared/tool.service';

@Component({
  selector: 'app-seasons',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, VModalComponent,VFormularyComponent, VButtonComponent, VInputTextComponent, VSelectComponent],
  templateUrl: './app.seasons.html',
  styleUrls: ['./app.seasons.scss']
})
export class SeasonsComponent implements OnInit{
  @ViewChild('modalAdd') modalAdd!: VModalComponent;
  @ViewChild('modalEdit') modalEdit!: VModalComponent;
  addForm!: FormGroup;
  editForm!: FormGroup;
  seasons: ISeason[] = [];
  rooms: IRoom[] = [];
  selectedId: string;
  constructor(public _shs: SharedService, private fb: FormBuilder, private _api: ApiService, private _tool: ToolService) {
    this.addForm = this.fb.group(
      {
        name: ['', Validators.required],
        roomId: ['', Validators.required]
      });
    this.editForm = this.fb.group(
      {
        name: ['', Validators.required]
      });
  }
  async ngOnInit() {  

    await this._shs.tryCatchLoading(async () => {
      const [rooms, seasons] = await Promise.all([
        this._api.getRooms().toAsync(),
        this._api.getSeasons().toAsync()
      ]);

      this.seasons = seasons.data;
      this.rooms = rooms.data;
    });
  }
  getRoomName(roomId:string) {
    return this.rooms.find(x=>x.id==roomId).name;
  }
  addModal() {
    this.addForm.reset();
    this.modalAdd.show = true;
  }
  editModal(item: any) {
    this.selectedId = item.id;
    this.editForm.reset();
    this.editForm.patchValue(item);
    this.modalEdit.show = true;
  }

  async add() {
    await this._shs.tryCatchLoading(async () => {
      const response = await this._api.createSeason(this.addForm.value).toAsync();
      this.seasons.push(response.data);

      this.modalAdd.show = false;
    });
  }
  async edit() {
    await this._shs.tryCatchLoading(async () => {
      const response = await this._api.updateSeason(this.selectedId, this.editForm.value).toAsync();
      this._tool.replaceOnArray(this.seasons, this.selectedId, response.data); 

      this.modalEdit.show = false;
    });
  }
  async delete(item: any) {
    await this._shs.tryCatchLoading(async () => {
      await this._api.deleteSeason(item.id).toAsync();
      this._tool.removeOnArray(this.seasons, item.id); 
    });
  }
}

