import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { SharedService } from '../../core/services/shared/shared.service';
import { VModalComponent } from '../angular/modal/app.modal';
import { VButtonComponent } from '../angular/button/app.button';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { VInputTextComponent } from '../angular/input-text/app.input-text';
import { VFormularyComponent } from '../angular/formulary/app.formulary';
import { ApiService } from '../../core/services/api.service';
import { IRoom } from '../../core/domain/room';
import { VInputNumberComponent } from '../angular/input-number/app.input-number';
import { ToolService } from '../../core/services/shared/tool.service';

@Component({
  selector: 'app-rooms',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, VModalComponent,VFormularyComponent, VButtonComponent, VInputTextComponent, VInputNumberComponent],
  templateUrl: './app.rooms.html',
  styleUrls: ['./app.rooms.scss']
})
export class RoomsComponent implements OnInit{
  @ViewChild('modalAdd') modalAdd!: VModalComponent;
  @ViewChild('modalEdit') modalEdit!: VModalComponent;
  addForm!: FormGroup;
  editForm!: FormGroup;
  rooms: IRoom[] = [];
  selectedId: string;
  constructor(public _shs: SharedService, private fb: FormBuilder, private _api: ApiService, private _tool: ToolService) {
    this.addForm = this.fb.group(
      {
        name: ['', Validators.required],
        rows: ['', Validators.required],
        columns: ['', Validators.required]
      });
    this.editForm = this.fb.group(
      {
        name: ['', Validators.required]
      });
  }
  async ngOnInit() {
    await this._shs.tryCatchLoading(async () => {
      this.rooms = (await this._api.getRooms().toAsync()).data;
    });
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
      const response = await this._api.createRoom(this.addForm.value).toAsync();
      this.rooms.push(response.data);

      this.modalAdd.show = false;
    });
  }
  async edit() {
    await this._shs.tryCatchLoading(async () => {
      const response = await this._api.updateRoom(this.selectedId, this.editForm.value).toAsync();
      this._tool.replaceOnArray(this.rooms, this.selectedId, response.data);      
      this.modalEdit.show = false;
    });
  }
  async delete(item: any) {
    await this._shs.tryCatchLoading(async () => {
      await this._api.deleteRoom(item.id).toAsync();
      this._tool.removeOnArray(this.rooms, item.id);      
    });
  }
}

