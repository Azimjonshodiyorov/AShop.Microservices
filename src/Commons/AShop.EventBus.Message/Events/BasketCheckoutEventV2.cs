namespace AShop.EventBus.Message.Events;

public class BasketCheckoutEventV2 : BaseIntegrationEvent
{
    public string UserName { get; set; }
    public string TotalPrice { get; set; }
}