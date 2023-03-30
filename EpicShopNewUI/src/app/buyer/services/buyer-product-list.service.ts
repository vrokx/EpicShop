import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CartModel } from 'src/app/models/cart-model';
import { Order } from 'src/app/models/orders';
import { Products } from 'src/app/models/products';
import { WalletModel } from 'src/app/models/wallet-model';

@Injectable({
  providedIn: 'root'
})
export class BuyerProductListService {
  public grandTotal: number = 0;

  private url = 'https://localhost:7277/api/Buyer';

  constructor(private http: HttpClient) { }

  getAllProducts(): Observable<Products[]> {
    return this.http.get<Products[]>(`${this.url}/BuyerDisplayAllProduct`);
  }

  getProductById(id: number): Observable<Products> {
    return this.http.get<Products>(`${this.url}/GetById?productId=${id}`);
  }

  addToCart(productId: number, qty: number, cartItem: CartModel): Observable<CartModel> {
    const url = `${this.url}/AddToCart?productId=${productId}&qty=${qty}`;
    return this.http.post<CartModel>(url, cartItem);
  }
  getCartItems(): Observable<any> {
    return this.http.get(`${this.url}/ViewCart`);
  }

  removeCartItem(cartId: number): Observable<any> {
    return this.http.delete(`${this.url}/RemoveCart?cartId=${cartId}`);
  }

  createOrRetrieveWallet(userId: number): Observable<WalletModel> {
    return this.http.post<WalletModel>(`${this.url}/wallet?userId=${userId}`, {});
  }

  addBalance(userId: number, amount: number): Observable<WalletModel> {
    const addBalanceDto = { userId, amount };
    return this.http.post<WalletModel>(`${this.url}/wallet/addbalance`, addBalanceDto);
  }
  placeOrder(mode: string, grandTotal: number, order: Order): Observable<Order> {
    const url = `${this.url}/PaymentMode?mode=${mode}&grandTotal=${grandTotal}`;
    return this.http.post<Order>(url, order);
  }
  showOrder(): Observable<Order>{
    const url = `${this.url}/OrderDetails`;
    return this.http.get<Order>(url);
  }

  sendEmail(userId: number): Observable<any> {
    return this.http.post<any>(`${this.url}/sendEmail?userId=${userId}`, {});
  }
  
}
