namespace AShop.Order.Application.Excaptions;

public class OrderNotFoundExcaption : ApplicationException
{
    public OrderNotFoundExcaption(string name, object key) 
        : base($"Entity {name} - {key} is not found.")
    {
        
    }
}