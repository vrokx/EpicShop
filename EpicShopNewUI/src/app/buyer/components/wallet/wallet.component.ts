import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { WalletModel } from 'src/app/models/wallet-model'; 
import { BuyerProductListService } from '../../services/buyer-product-list.service';

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
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    const userId = 4; // replace with actual user ID
    this.walletService.createOrRetrieveWallet(userId).subscribe(wallet => {
      this.wallet = wallet;
    });

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
    const userId = 1; // replace with actual user ID

    this.walletService.addBalance(userId, amount).subscribe(wallet => {
      this.wallet = wallet;
    });
  }

}
