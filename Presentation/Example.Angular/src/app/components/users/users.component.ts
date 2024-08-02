import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.prod';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})



export class UsersComponent implements OnInit {

  public users!: User[];

  constructor(private http: HttpClient) { }
  
  ngOnInit() {
    this.http.get<User[]>(environment.baseUrl + 'api/users')
    .subscribe(result => {
    this.users = result;
    }, error => console.error(error));
  }
 }
