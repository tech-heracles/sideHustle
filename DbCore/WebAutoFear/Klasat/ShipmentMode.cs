namespace DbCore.WebAutoFear.Klasat
{
  public class ShipmentMode
  { 
    public string Id { get; set; }
    public string Description { get; set; }
    public string[] Memo { get; set; }    
    public decimal Cost { get; set; }
    public string CurrencyCode { get; set; }
    public bool Default { get; set; }
    public int SortId { get; set; }
    public decimal? DefaultDeliveryCosts { get; set; } 
    public decimal? CarriageFree { get; set; }
    public decimal? ExpressCosts { get; set; }  
    public bool IsExpressDelivery { get; set; } 
    public string DeliveryConditions { get; set; }
  } 
} 
