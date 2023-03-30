export interface Order {
    orderId: number;
    orderDate: Date;
    modeOfPayment: string;
    orderStatus: string;
    amountPaid: number;
  }