import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../product';

@Injectable({
  providedIn: 'root'
})
export class AllProductsComponent implements OnInit{
  Products: Product[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<Product[]>('Seller/DisplayAllProducts').subscribe(products => {
      this.Products = products;
    });
}}
