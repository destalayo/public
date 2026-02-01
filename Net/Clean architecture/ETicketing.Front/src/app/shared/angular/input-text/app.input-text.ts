import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, forwardRef } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-input-text',
  imports: [CommonModule, FormsModule],
  templateUrl: './app.input-text.html',
  styleUrls: ['./app.input-text.scss']
  ,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => VInputTextComponent),
      multi: true
    }
  ]
})
export class VInputTextComponent implements ControlValueAccessor {
  @Input() show: boolean = true;
  @Output() onValueChange = new EventEmitter<string>();
  value: string = '';
  onChange = (_: any) => { };
  onTouched = () => { };

  writeValue(value: string): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.value = input.value;
    this.onChange(this.value);
    this.onValueChange.emit(this.value);
  }
}
