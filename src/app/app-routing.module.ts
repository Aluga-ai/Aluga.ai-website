import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    
  },
  {
    path: '',
    component: HomeComponent,
    
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
    path: 'registrar-se',
    component: HomeComponent,
    
  },
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
