import { CommonModule } from '@angular/common';
import { Component, Input, ViewEncapsulation } from '@angular/core';
import { VIconButtonComponent } from '../icon-button/app.icon-button';

@Component({
  selector: 'app-modal',
  imports: [CommonModule, VIconButtonComponent],
  templateUrl: './app.modal.html',
  styleUrls: ['./app.modal.scss']
})
export class VModalComponent {
  @Input() titulo: string = '';
  @Input() style: string = '';
  @Input() extraClass: string = '';
  show = false;
  @Input() showClose: boolean = true;
  cerrar() {
    this.show = false;
  }
}
