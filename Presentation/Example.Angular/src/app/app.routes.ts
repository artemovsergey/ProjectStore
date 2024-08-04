import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RepositoryComponent } from './components/repository/repository.component';
import { AuthGuard } from './guards/auth-guard';


// определение маршрутов
export const routes: Routes =[
    { path: '', component: RepositoryComponent},
    { path: 'login', component: LoginComponent},
    { path: 'home', component: HomeComponent,  canActivate: [AuthGuard]},
    { path: '**', component: LoginComponent }
  ];
