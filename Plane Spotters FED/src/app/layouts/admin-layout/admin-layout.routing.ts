import { Routes } from '@angular/router';


import { SpottersListComponent } from 'src/app/pages/spotters-list/spotters-list.component';
import { SpottersManageComponent } from 'src/app/pages/spotters-manage/spotters-manage.component';
import { AuthGuard } from 'src/app/auth/_guards/AuthGuard';

export const AdminLayoutRoutes: Routes = [    
    {
        path: 'spotter',
        children: [
            { path: '', component: SpottersListComponent, canActivate: [AuthGuard]},
            { path: 'list', component: SpottersListComponent, canActivate: [AuthGuard]},
            { path: 'new', component: SpottersManageComponent, canActivate: [AuthGuard]},
            { path: ':id', component: SpottersManageComponent, canActivate: [AuthGuard]}
        ]
    },
];
