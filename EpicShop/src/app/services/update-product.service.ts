import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Product } from '../models/product';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class updateProductService {

  private apiUrl = 'https://localhost:7277/api/Seller/';

  constructor(private http: HttpClient) { }

  updateProduct(id: number, product: Product): Observable<Product> {
    const url = `${this.apiUrl}/UpdateProduct/${id}`;
    return this.http.put<Product>(url, product, httpOptions).pipe(
      tap(_ => console.log(`updated product with id=${id}`)),
      catchError(this.handleError<Product>('updateProduct'))
    );
  }
  

}
