import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl, AsyncValidatorFn} from '@angular/forms';
import { BaseFormComponent } from '../../components/base/base-form-component';
import { AuthService } from '../../services/auth-service';
import { LoginRequest } from '../../auth/login-request';
import { LoginResult } from '../../auth/login-result';
import { ReactiveFormsModule } from '@angular/forms'; // Импорт ReactiveFormsModul
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterModule }  from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule,ReactiveFormsModule,CommonModule, MatFormFieldModule, MatTableModule, MatButtonModule, MatIconModule, MatSortModule, MatPaginatorModule,MatInputModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})

export class LoginComponent implements OnInit {

  title?: string;
  loginResult?: LoginResult;
  form!: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService) {

  }

  ngOnInit() {
    this.form = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }

  onSubmit() {
    var loginRequest = <LoginRequest>{};
    loginRequest.email = this.form.controls['email'].value;
    loginRequest.password = this.form.controls['password'].value;
    this.authService
      .login(loginRequest)
      .subscribe(result => {
        console.log(result);
        this.loginResult = result;
        if (result.success && result.token) {
          localStorage.setItem(this.authService.tokenKey, result.token);
          this.router.navigate([""]);
        }
      }, error => {
        console.log(error);
        if (error.status == 401) {
          this.loginResult = error.error;
        }
      });
  }


  getErrors(
    control: AbstractControl,
    displayName: string,
  ): string[] {
    var errors: string[] = [];
    Object.keys(control.errors || {}).forEach((key) => {
      switch (key) {
        case 'required':
          errors.push('${displayName} is required.');
          break;
        case 'pattern':
          errors.push('${displayName} contains invalid characters.');
          break;
        case 'isDupeField':
          errors.push('${displayName} already exists: please choose another.');
          break;
        default:
          errors.push('${displayName} is invalid.');
          break;
      }
    });
    return errors;
  }

  getErrorControl(controlName: string): AbstractControl | null {
    return this.form.get(controlName);
  }


}
