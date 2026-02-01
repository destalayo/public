import { CommonModule } from '@angular/common';
import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-input-datetime',
  imports: [CommonModule, FormsModule],
  templateUrl: './app.input-datetime.html',
  styleUrls: ['./app.input-datetime.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => VDateTimePickerComponent),
      multi: true
    }
  ]
})
export class VDateTimePickerComponent implements ControlValueAccessor {
  @Input() disabled: boolean = false;

  // Valor interno en formato string para el input
  value: string = '';

  // Funciones que Angular usará para notificar cambios
  private onChange: (_: Date) => void = () => { };
  private onTouched: () => void = () => { };

  // ✅ Recibe Date o string desde el padre
  writeValue(value: Date | string | null): void {
    if (value instanceof Date) {
      this.value = this.formatDate(value);
    } else if (typeof value === 'string') {
      this.value = value;
    } else {
      this.value = '';
    }
  }

  registerOnChange(fn: (_: Date) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  // ✅ Convierte string del input a Date y lo envía al padre
  onInputChange(event: Event): void {
    const val = (event.target as HTMLInputElement).value;
    this.value = val;
    const date = val ? new Date(val) : null;
    if (date) this.onChange(date);
    this.onTouched();
  }

  // 🔧 Convierte Date a string compatible con datetime-local
  private formatDate(date: Date): string {
    const pad = (n: number) => n.toString().padStart(2, '0');
    const yyyy = date.getFullYear();
    const mm = pad(date.getMonth() + 1);
    const dd = pad(date.getDate());
    const hh = pad(date.getHours());
    const min = pad(date.getMinutes());
    return `${yyyy}-${mm}-${dd}T${hh}:${min}`;
  }
}
