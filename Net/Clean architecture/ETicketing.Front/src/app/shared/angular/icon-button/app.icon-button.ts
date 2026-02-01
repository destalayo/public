import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, forwardRef } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-icon-button',
  imports: [CommonModule, FormsModule],
  templateUrl: './app.icon-button.html',
  styleUrls: ['./app.icon-button.scss']  
})
export class VIconButtonComponent {
  @Input() disabled = false;
  @Input() icon: string | { [className: string]: boolean };
  @Input() show = true;
  @Input() class = "";
  @Input() size = 40;
  @Input() color = "red";
  @Output() clicked = new EventEmitter<void>();

  onClick() {
    if (!this.disabled) {
      this.clicked.emit();
    }
  }
  getSize() {
    return this.size.toString();
  }
  getClass() {
    if (!this.disabled) {
      return "clickable fa-solid";
    }
    return "";
  }
}
