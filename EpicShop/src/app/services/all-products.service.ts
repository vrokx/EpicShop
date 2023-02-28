import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class AllProductsService {

  private baseUrl = 'https://localhost:7277/api/Seller';

  constructor(private http: HttpClient) { }

  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/DisplayAllProducts`);
  }

  getProduct(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/GetById/${id}`);
  }

  updateProduct(id: number, product: Product): Observable<Product> {
    const url = `${this.baseUrl}/UpdateProduct/${product.productId}`;
    return this.http.put<Product>(url, product);
  }
}
