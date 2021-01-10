import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { LoginService } from '../login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})

export class LoginPageComponent implements OnInit {

  loginForm;

  constructor(
    private formBuilder: FormBuilder,
    private loginService: LoginService,
    private router: Router
  ) {
    this.loginForm = this.formBuilder.group({
      'email': '',
      'password': ''
    });
  }

  ngOnInit(): void {
    if (this.loginService.Status().code === 1) {
      console.info("[login-page] Acesso anterior continua válido")
      this.navigateToInitPage()
    }
  }

  async logIn(loginData) {

    console.log("[login-page] Tentando realizar login...");

    await this.loginService.login(loginData.email, loginData.password)
      .then(loggedIn => {
        console.info("[login-page] LoggedIn: " + loggedIn);
      });

    let status = this.loginService.Status();

    console.info("[login-page] Login status code: " + status.code)
    console.info("[login-page] Login status message: " + status.message)

    if (status.code == 1) {
      //To do: Adicionar redirecionamento para pagina inicial.
      this.navigateToInitPage()
    }
  }

  private navigateToInitPage() {
    console.info("[login-page] Navegando para página inicial")
    this.router.navigate(['/client'])
  }

}
