import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, forwardRef } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-input-number',
  imports: [CommonModule, FormsModule],
  templateUrl: './app.input-number.html',
  styleUrls: ['./app.input-number.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => VInputNumberComponent),
      multi: true
    }
  ]
})
export class VInputNumberComponent implements ControlValueAccessor {
  @Input() show: boolean = true;
  @Output() onValueChange = new EventEmitter<number>();
  value: number | null = null;

  onChange = (_: any) => { };
  onTouched = () => { };

  writeValue(value: number | null): void {
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
    let rawValue = input.value;

    // Eliminar cualquier carácter que no sea dígito, punto o signo negativo
    rawValue = rawValue.replace(/[^0-9\.\-]/g, '');

    // Actualizar el input visualmente
    input.value = rawValue;

    // Convertir a número
    const parsed = rawValue === '' ? null : Number(rawValue);

    // Validar que sea un número válido
    if (!isNaN(parsed)) {
      this.value = parsed;
      this.onChange(this.value);
      this.onValueChange.emit(this.value);
    } else {
      this.value = null;
      this.onChange(null);
      this.onValueChange.emit(null);
    }
  }
}
