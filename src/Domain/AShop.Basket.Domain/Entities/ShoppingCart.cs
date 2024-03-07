namespace AShop.Basket.Domain.Entities;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    public ShoppingCart()
    {
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

}