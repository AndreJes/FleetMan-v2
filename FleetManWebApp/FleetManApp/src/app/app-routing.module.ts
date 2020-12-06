import { NgModule } from '@angular/core';
import { RouterModule , Routes } from '@angular/router';

import { LoginPageComponent } from './login-page/login-page.component';
import { ClientPageComponent } from './client-page/client-page.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: "full" },
  { path: 'login', component: LoginPageComponent },
  { path: 'client', component: ClientPageComponent}
]

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})

export class AppRoutingModule { }
