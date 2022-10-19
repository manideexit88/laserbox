import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BoxComponent } from './box/box.component';
import { BoxService } from './services/box.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    BoxComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
        { path: '', component: BoxComponent, pathMatch: 'full' },
        { path: 'box', component: BoxComponent },
    ])
    ],
    providers: [BoxService],
  bootstrap: [AppComponent]
})
export class AppModule { }
