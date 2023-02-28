import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AllProductsService } from 'src/app/services/all-products.service';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.css']
})
export class UpdateProductComponent {
  @Input() productId: number = 0;

  productForm: FormGroup = this.formBuilder.group({
    productName: '',
    image: '',
    price: '',
  });

  constructor(
    private formBuilder: FormBuilder,
    private productListService: AllProductsService
  ) {}

  onSubmit(): void {
    const updatedProduct = {
      productName: this.productForm.value.productName,
      image: this.productForm.value.image,
      price: this.productForm.value.price,
    };
    this.productListService.updateProduct(this.productId, updatedProduct).subscribe();
  }
}
