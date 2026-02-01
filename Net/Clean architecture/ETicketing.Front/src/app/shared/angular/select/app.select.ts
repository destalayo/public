import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  Output,
  forwardRef
} from '@angular/core';
import {
  ControlValueAccessor,
  FormsModule,
  NG_VALUE_ACCESSOR
} from '@angular/forms';

@Component({
  selector: 'app-select',
  imports: [CommonModule, FormsModule],
  templateUrl: './app.select.html',
  styleUrls: ['./app.select.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => VSelectComponent),
      multi: true
    }
  ]
})
export class VSelectComponent implements ControlValueAccessor {
  @Input() options: any[] = [];
  @Input() valueField: string = 'id';
  @Input() labelField: string = 'name';
  @Input() selectedValue: any;

  @Output() selectedValueChange = new EventEmitter<any>();
  @Output() valueChanged = new EventEmitter<any>();

  // Métodos de ControlValueAccessor
  private onTouched = () => { };
  private onChangeFn = (_: any) => { };

  writeValue(value: any): void {
    this.selectedValue = value;
  }

  registerOnChange(fn: any): void {
    this.onChangeFn = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    // Si necesitas manejar el estado disabled
  }

  onChangeModel(value: any): void {
    this.selectedValue = value;

    // Emitimos los eventos personalizados
    this.selectedValueChange.emit(value);
    this.valueChanged.emit(value);

    // Notificamos al sistema de formularios de Angular
    this.onChangeFn(value);
    this.onTouched();
  }
}
