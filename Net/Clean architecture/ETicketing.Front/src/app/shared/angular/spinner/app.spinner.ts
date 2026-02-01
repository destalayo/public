import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-spinner',
  imports: [CommonModule],
  templateUrl: './app.spinner.html',
  styleUrls: ['./app.spinner.scss']
})
export class VSpinnerComponent {
  @Input() isLoading: boolean = false;
}
