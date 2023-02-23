import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../product';
import { DisplayAllProductsService } from 'src/app/display-all-products.service'
import { subscribeOn } from 'rxjs';

@Component({
  selector: 'app-all-products',
  templateUrl: './all-products.component.html',
  styleUrls: ['./all-products.component.scss']
})
export class AllProductsComponent implements OnInit{
  Products: Product[] = [];

  constructor(private displayAllProductsService : DisplayAllProductsService ) { }

  ngOnInit() : void {
    this.displayAllProductsService.getProducts()
    .subscribe({
      next:(products) => {
        console.log(products);
      },
      error:(response) => {
        console.log(response);
      }
    });
  }
}