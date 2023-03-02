import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductListService } from '../../services/product-list.service';
import { Products } from 'src/app/models/products';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.css']
})
export class UpdateProductComponent implements OnInit {

  product: Products = { productId: 0, productName: '', price: 0, image: '' };

  constructor(private productService: ProductListService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.onLoad();
  }

  onLoad(): void {
    const productId = parseInt(this.route.snapshot.paramMap.get('id') as string);
    this.productService.getProductById(productId).subscribe(product => {
      this.product = product;
    });
  }

  updateProduct(): void {
    let updatedProduct: Products = { ...this.product }; // create a copy of the product
    this.productService.updateProduct(updatedProduct.productId, updatedProduct).subscribe(() => {
      this.router.navigate(['/seller/product-list']); // navigate back to product list after updating
    });
  }

  onCancel(): void {
    this.router.navigate(['/seller/product-list']);
  }

  onSubmit(): void {
    const updatedProduct: Products = { ...this.product };
    this.productService.updateProduct(this.product.productId, updatedProduct).subscribe(() => {
      this.router.navigate(['/seller/product-list']);
    });
  }
}