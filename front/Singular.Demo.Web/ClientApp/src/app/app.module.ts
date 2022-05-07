import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PhonesModule } from './phones/phones.module';
import { PhonesListComponent } from './phones/phones-list/phones-list.component';
import { AuthGuard, AuthHttpInterceptor, AuthModule } from '@auth0/auth0-angular';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', canActivate: [AuthGuard], component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', canActivate: [AuthGuard], component: CounterComponent },
      { path: 'fetch-data', canActivate: [AuthGuard], component: FetchDataComponent },
      { path: 'phones', canActivate: [AuthGuard], loadChildren: () => import('./phones/phones.module').then(m => m.PhonesModule) },
    ]),
    PhonesModule,
    // Import the module into the application, with configuration
    AuthModule.forRoot({
      domain: 'dev-x3vx68sh.auth0.com',
      clientId: 'XQQL7autCgguEsxkwSCu6fyGItjvgFcb',
      audience: 'https://localhost:7163',
      scope: 'create:phones update:phones delete:phones get:phones list:phones',
      httpInterceptor: {
        allowedList: [
          {
            uri: 'https://localhost:7163/api/*',
            tokenOptions: {
              audience: 'https://localhost:7163',
              scope: 'create:phones update:phones delete:phones get:phones list:phones'
            }
          }
        ]
      }
    }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
