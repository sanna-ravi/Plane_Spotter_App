import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ClipboardModule } from 'ngx-clipboard';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { NgbDateAdapter, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { SpottersListComponent } from 'src/app/pages/spotters-list/spotters-list.component';
import { SpotterService } from 'src/app/services/spotter.service';
import { SpottersManageComponent } from 'src/app/pages/spotters-manage/spotters-manage.component';
import { NgbStringAdapter } from 'src/app/pages/adapter/ngb-string-adapter';
import { AuthGuard } from 'src/app/auth/_guards/AuthGuard';
import { ErrorInterceptor, JwtInterceptor } from 'src/app/auth/interceptor';
import { AuthService } from 'src/app/auth/services/auth.service';
// import { ToastrModule } from 'ngx-toastr';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule, ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule,
    Ng2SmartTableModule
  ],
  declarations: [
    SpottersListComponent,
    SpottersManageComponent  
  ],
  providers:[
    SpotterService,
    AuthGuard,
    AuthService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    {provide: NgbDateAdapter, useClass: NgbStringAdapter}
  ]
})

export class AdminLayoutModule {}
