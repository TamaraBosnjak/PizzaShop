﻿namespace PizzaShop.Models
{
    public interface IShoppingCart
    {
        void AddToCart(Pizza pizza, int amount);
        int RemoveFromCart(Pizza pizza);
        List<ShoppingCartItem> GetShoppingCartItems();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
        void ClearCart();
        decimal GetShoppingCartTotalQuantity();
    }
}
