import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  public lgform: FormGroup;
  public showError: boolean = false;
  public isClicked = false;
  
  constructor(private router: Router, private fb: FormBuilder, private service: AuthService) {
    this.lgform = this.fb.group({
      'username': ['', Validators.required],
      'password': ['', Validators.required]
    });
    this.service.signout();
  }

  public onSubmit(values): void {
    this.showError = false;
    this.isClicked = true;
    console.log(values);
    if (this.lgform.valid) {
      var obj = { username: values.username, password: values.password }
      this.service.sigin(obj).subscribe(
        (data: any) => {
          localStorage.setItem('cr_de', JSON.stringify(data));
          var momt = moment(new Date()).add(230, 'm').toDate();
          localStorage.setItem('ex_kr', momt.getTime().toString());
          this.router.navigate(['spotter']);
        },
        (error: any) => {
          this.showError = true;
          this.isClicked = false;
        }
      );

    }
  }

  ngOnInit() {
  }
  ngOnDestroy() {
  }

}
