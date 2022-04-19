namespace DbCore.WebAutoFear.Klasat
{
  public class BillingMode
  {   
    public string Id { get; set; }  
    public string Description { get; set; }
    public string[] Memo { get; set; } 
    public bool Default { get; set; }   
    public int SortId { get; set; }
  } 
}