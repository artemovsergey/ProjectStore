import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth-service';

import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./components/header/header.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})


export class AppComponent {
  title = 'ProjectStore';

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.init();
  }

}
