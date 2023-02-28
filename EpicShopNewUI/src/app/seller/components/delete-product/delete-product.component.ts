import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductListService } from '../../services/product-list.service';
import { Products } from 'src/app/models/products';

@Component({
  selector: 'app-delete-product',
  templateUrl: './delete-product.component.html',
  styleUrls: ['./delete-product.component.css']
})
export class DeleteProductComponent implements OnInit {

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

  deleteProduct(): void {
    this.productService.deleteProduct(this.product.productId).subscribe(() => {
      this.router.navigate(['']); // navigate back to product list after deleting
    });
  }

  onCancel(): void {
    this.router.navigate(['/']); // navigate back to product list without deleting
  }
}
