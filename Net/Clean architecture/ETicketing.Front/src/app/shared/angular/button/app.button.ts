import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, forwardRef } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-button',
  imports: [CommonModule, FormsModule],
  templateUrl: './app.button.html',
  styleUrls: ['./app.button.scss']  
})
export class VButtonComponent {
  @Input() disabled = false;
  @Input() label = 'Botón';
  @Input() show = true;
  @Output() clicked = new EventEmitter<void>();

  onClick() {
    if (!this.disabled) {
      this.clicked.emit();
    }
  }
}
