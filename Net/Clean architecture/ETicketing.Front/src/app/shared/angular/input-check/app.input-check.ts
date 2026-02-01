import { CommonModule } from '@angular/common';
import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
@Component({
  selector: 'app-input-check',
  imports: [CommonModule, FormsModule],
  templateUrl: './app.input-check.html',
  styleUrls: ['./app.input-check.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => VInputCheckComponent),
    multi: true
  }] 
})
export class VInputCheckComponent implements ControlValueAccessor {
  @Input() size = 40;
  @Input() disabled = false;
  value: boolean | null = null;
  icon: string;
  private onChange = (_: any) => { };
  private onTouched = () => { };
  toggle() {
    if (this.disabled) return;
    if (this.value === null)
    { this.value = true; }
    else if (this.value === true)
    { this.value = false; }
    else { this.value = null; }
    this.updateIcon();
    this.onChange(this.value);
    this.onTouched();
  }
  updateIcon() {
    switch (this.value) {
      case true:
        this.icon = 'fa-circle-check green';
        break;
      case false:
        this.icon = 'fa-square-xmark red';
        break;
      default:
        this.icon = 'fa-square white';
        break;
    }
  }
  writeValue(value: boolean | null): void {
    this.value = value;
    this.updateIcon();
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
  getSize() {
    return this.size.toString();
  }
}
