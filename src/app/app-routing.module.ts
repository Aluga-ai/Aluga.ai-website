import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FeedCardComponent } from './feed-card/feed-card.component';
import { MainRegisterComponent } from './register/main-register/main-register.component';
import { UniversitarioRegisterComponent } from './register/universitario-register/universitario-register.component';
import { AnuncianteRegisterComponent } from './register/anunciante-register/anunciante-register.component';


const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,

  },
  {
    path: '',
    component: FeedCardComponent,

  },
  {
    path: 'faculdades',
    component: HomeComponent,

  },
  {
    path: 'universitarios',
    component: HomeComponent,

  },
  {
    path: 'destaque-se',
    component: HomeComponent,

  },
  {
    path: 'anuncie',
    component: HomeComponent,

  },
  {
    path: 'entrar',
    component: HomeComponent,

  },
  {
    path: 'registrar',
    component: MainRegisterComponent,
  },
  {
    path: 'registrar/universitario',
    component: UniversitarioRegisterComponent,

  },
  {
    path: 'registrar/anunciante',
    component: AnuncianteRegisterComponent,

  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
