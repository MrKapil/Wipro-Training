export interface OrderItem {
  orderItemId: number;
  productId: number;
  productName: string;
  quantity: number;
  unitPrice: number;
  lineTotal: number;
}

export interface Order {
  orderId: number;
  userId: number;
  totalAmount: number;
  discountAmount: number;
  finalAmount: number;
  status: string;
  createdAt: string;
  items: OrderItem[];
}

export interface CheckoutRequest {
  shippingAddress?: string;
  couponCode?: string;
}
