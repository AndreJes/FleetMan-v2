import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { LoginService } from '../login.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})

export class LoginPageComponent implements OnInit {

  loginForm;

  constructor(
    private formBuilder: FormBuilder,
    private loginService: LoginService
  ) {
    this.loginForm = this.formBuilder.group({
      'email': '',
      'password': ''
    });
  }

  ngOnInit(): void {
  }

  async logIn(loginData) {

    console.log("Tentando realizar login...");

    await this.loginService.login(loginData.email, loginData.password)
      .then(loggedIn => {
        console.info("LoggedIn: " + loggedIn);
      });

  }

}
