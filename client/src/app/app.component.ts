import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Pagination } from './models/pagination';
import { Product } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Shop404';
  products: Product[]=[];

  constructor(private http: HttpClient)
  {
  }
  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>('https://localhost:5001/api/products?pageSize=50').subscribe({
      next: response=>this.products=response.data, // What to do Next
      error: error=>console.log(error), // what to do if there is an error
      complete: ()=>{
        console.log("Request Completed");
        console.log("Extra Statement");
      }
    }
    )
  }
}
