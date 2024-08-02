import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { UsersComponent } from './components/users/users.component';

// определение маршрутов
export const routes: Routes =[
    { path: 'home', component: UsersComponent},
    { path: 'login', component: LoginComponent},
    { path: 'contact', redirectTo: 'login', pathMatch:'full'}, // переадресация c полным соответствием
    { path: '**', component: LoginComponent } // если не подходит все маршруты
  ];