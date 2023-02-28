import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Products } from 'src/app/models/products';

@Injectable({
  providedIn: 'root'
})
export class ProductListService {

  private url = 'https://localhost:7277/api/Seller';

  constructor(private http: HttpClient) { }

  addProduct(product: Products): Observable<Products> {
    return this.http.post<Products>(`${this.url}/AddProduct`, product);
  }

  getAllProducts(): Observable<Products[]> {
    return this.http.get<Products[]>(`${this.url}/DisplayAllProducts`);
  }

  getProductById(id: number): Observable<Products> {
    return this.http.get<Products>(`${this.url}/GetById?productId=${id}`);
  }

  updateProduct(id: number, product: Products): Observable<Products> {
    const url = `${this.url}/UpdateProduct/?id=${product.productId}`;
    return this.http.put<Products>(url, product);
  }

  deleteProduct(id : number): Observable<any>{
    const url = `${this.url}/DeleteProduct/?id=${id}`;
    return this.http.delete<any>(url);
  }
}
