import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IUser } from '../../core/domain/user';
import { ApiService } from '../../core/services/api.service';
import { SharedService } from '../../core/services/shared/shared.service';
import { AuthService } from '../../core/services/auth/auth.service';
import { VFormularyComponent } from '../angular/formulary/app.formulary';
import { VButtonComponent } from '../angular/button/app.button';
import { VInputTextComponent } from '../angular/input-text/app.input-text';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, ReactiveFormsModule,VFormularyComponent, VInputTextComponent, VButtonComponent],
  templateUrl: './app.login.html',
  styleUrls: ['./app.login.scss']
})
export class LoginComponent{
  form!: FormGroup;
  constructor(public _shs: SharedService, private _api: ApiService, private fb: FormBuilder, private _auth:AuthService) {
    this.form = this.fb.group(
      {
        email: ['', Validators.required],
        password: ['', Validators.required]
      });
  }
  async login() {
    await this._shs.tryCatchLoading(async () => {
      await this._auth.login(this.form.value.email, this.form.value.password);
      this._shs.navigate("/home");
    });
  }
  
}

