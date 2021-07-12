import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';

import { NgbDateAdapter, NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { SpottersListComponent } from './pages/spotters-list/spotters-list.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { SpottersManageComponent } from './pages/spotters-manage/spotters-manage.component';
import { NgbStringAdapter } from './pages/adapter/ngb-string-adapter';


@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    NgbModule,
    RouterModule,
    AppRoutingModule,
    Ng2SmartTableModule
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    AuthLayoutComponent
  ],
  providers: [
    {provide: NgbDateAdapter, useClass: NgbStringAdapter}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
