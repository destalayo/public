
import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-formulary',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './app.formulary.html',
  styleUrls: ['./app.formulary.scss']

})
export class VFormularyComponent   {
  @Input() form: FormGroup | undefined;
}
