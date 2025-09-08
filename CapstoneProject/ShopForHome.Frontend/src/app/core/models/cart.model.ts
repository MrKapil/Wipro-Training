export interface CartItem {
  cartItemId: number;
  productId: number;
  productName: string;
  quantity: number;
  unitPrice: number;
  imageFileName?: string;
}

export interface Cart {
  cartId: number;
  userId: number;
  items: CartItem[];
  totalAmount: number;
}
