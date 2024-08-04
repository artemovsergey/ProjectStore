import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';

import { AuthService } from '../../services/auth-service';

import { CommonModule } from '@angular/common';


import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule,RouterModule,MatButtonModule,MatIconModule,MatToolbarModule ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit, OnDestroy {

  private destroySubject = new Subject();
  isLoggedIn: boolean = false;
  constructor(private authService: AuthService,
              private router: Router) {
    this.authService.authStatus
      .pipe(takeUntil(this.destroySubject))
      .subscribe(result => {
        this.isLoggedIn = result;
      })
   }

    onLogout(): void {
      this.authService.logout();
      this.router.navigate(["login"]);
    }
    ngOnInit(): void {
      this.isLoggedIn = this.authService.isAuthenticated();
    }
    ngOnDestroy() {
      this.destroySubject.next(true);
      this.destroySubject.complete();
    }

}
