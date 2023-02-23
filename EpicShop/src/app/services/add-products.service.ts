import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class AddProductsService {

  private apiUrl = 'https://localhost:7277/api/Seller/AddProduct';

  constructor(private http: HttpClient) {}

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl , product);
  }
}
