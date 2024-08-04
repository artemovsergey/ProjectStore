import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatTableModule, MatButtonModule, MatIconModule, MatSortModule, MatPaginatorModule,MatInputModule ],
  templateUrl: './repository.component.html',
  styleUrl: './repository.component.scss'
})

export class RepositoryComponent implements OnInit {

  public displayedColumns: string[] = ['id', 'name', 'owner','stargazers','url','watchers'];
  public repositories!: MatTableDataSource<Repository>;

  defaultPageIndex: number = 0;
  defaultPageSize: number = 10;
  public defaultSortColumn: string = "name";
  public defaultSortOrder: "asc" | "desc" = "asc";

  defaultFilterColumn: string = "name";
  filterQuery?:string;


  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private http: HttpClient)
  {
  }

  ngOnInit() {
    this.loadData();
  }

  loadData(query?: string) {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    this.filterQuery = query;
    this.getData(pageEvent);
  }

  getData(event: PageEvent) {

    var url = environment.baseUrl + 'api/Repository';
    console.log("url = " + url);
    var params = new HttpParams()
      .set("pageIndex", event.pageIndex.toString())
      .set("pageSize", event.pageSize.toString())
      .set("sortColumn", (this.sort)
        ? this.sort.active
        : this.defaultSortColumn)
      .set("sortOrder", (this.sort)
        ? this.sort.direction
        : this.defaultSortOrder);

    if (this.filterQuery) {
      params = params
        .set("filterColumn", this.defaultFilterColumn)
        .set("filterQuery", this.filterQuery);
    }

    //const headers = { 'Authorization': 'Bearer ' + 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlciIsImV4cCI6MTc1NDE3MjUzOSwiaXNzIjoiWW91cklzc3VlciIsImF1ZCI6IllvdXJBdWRpZW5jZSJ9.oO-h4Wj3KeAWAlaCgIuwlKyeaTRuek6OghELywJWeO0' }; // Получаем JWT из localStorage

    this.http.get<any>(url,  { params })
      .subscribe(result => {
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.pageIndex;
        this.paginator.pageSize = result.pageSize;
        this.repositories = new MatTableDataSource<Repository>(result.data);
      }, error => console.error(error));
  }
 }

export interface Repository {
  id: number;
  name: string;
  owner: string;
  stargazers: number;
  url: string | null;
  watchers: number | null;
}
