import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { VIconButtonComponent } from '../icon-button/app.icon-button';

@Component({
  selector: 'app-accordion',
  imports: [CommonModule, VIconButtonComponent, FormsModule],
  templateUrl: './app.accordion.html',
  styleUrls: ['./app.accordion.scss']
})
export class VAccordionComponent {
  @Input() title: string = '';
  show = true;
  toogle() {
    this.show = !this.show;
  }
}
