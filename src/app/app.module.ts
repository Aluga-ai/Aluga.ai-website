import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { HomeComponent } from './home/home.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {MatIconModule} from '@angular/material/icon';
import { FeedCardComponent } from './feed-card/feed-card.component';
import {MatChipsModule} from '@angular/material/chips';
import { MainRegisterComponent } from './register/main-register/main-register.component';
import { AnuncianteRegisterComponent } from './register/anunciante-register/anunciante-register.component';
import { UniversitarioRegisterComponent } from './register/universitario-register/universitario-register.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    FeedCardComponent,
    MainRegisterComponent,
    AnuncianteRegisterComponent,
    UniversitarioRegisterComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatIconModule,
    MatChipsModule,
   
  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync('noop')
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
