namespace DbCore.WebAutoFear.Klasat
{
  public class Order
  {
    public string OrderId { get; set; }
    public string OwnOrderId { get; set; }
    public string[] Memo { get; set; }
    public Address DeliveryAddress { get; set; }
    public string ShipmentMode { get; set; }
    public string PaymentMode { get; set; }
    public string BillingMode { get; set; }
    public string Warehouse { get; set; }
    public string TourId { get; set; }    
    public string ExpectedDelivery { get; set; }
    public string WantedDelivery { get; set; }
  }
} 
