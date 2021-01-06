import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  logInForm: FormGroup;
  destroy$: Subject<boolean> = new Subject<boolean>();
  
  constructor(private fb: FormBuilder, private router: Router, private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    this.logInForm = this.fb.group({
      email: ["mdu@yahoo.com", [Validators.required, Validators.pattern("[a-z0-9.@]*")]],
      password: ["", [Validators.required]]
    });
  }

  onSubmit(form: FormGroup) {
    if (form.valid) {
      let user = { Email: form.value.email, Password: form.value.password };
      
      this.authenticationService
          .logIn(user)
          .pipe(takeUntil(this.destroy$))
          .subscribe(user => {
            console.log(user);
            if(user) {
              this.router.navigate(["weather"]);
            }
          })
    }
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }
}
