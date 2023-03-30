import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { WalletModel } from 'src/app/models/wallet-model'; 
import { BuyerProductListService } from '../../services/buyer-product-list.service';
import { Router } from '@angular/router';
import { startWith, switchMap } from 'rxjs';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.css']
})
export class WalletComponent implements OnInit {

  wallet!: WalletModel;
  addBalanceForm!: FormGroup;

  constructor(
    private walletService: BuyerProductListService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    const userId = 2; // replace with actual user ID
    this.walletService.createOrRetrieveWallet(userId)
      .pipe(
        switchMap(wallet => {
          this.wallet = wallet;
          return this.addBalanceForm.valueChanges.pipe(startWith(this.addBalanceForm.value));
        })
      )
      .subscribe(() => {});
    
    this.addBalanceForm = this.formBuilder.group({
      amount: ['', [Validators.required, Validators.min(1)]]
    });
  }
  

  get form() { return this.addBalanceForm.controls; }

  addBalance(): void {
    if (this.addBalanceForm.invalid) {
      return;
    }

    const amount = this.addBalanceForm.value.amount;
    const userId = 2; // replace with actual user ID

    this.walletService.addBalance(userId, amount).subscribe(wallet => {
      this.wallet = wallet;
    });
  }
  buyNow(): void {
    this.router.navigate(['paymentMode']);
  }

}
