namespace DbCore.WebAutoFear.Klasat
{
  public class ItemBase
  {  
    public string WholesalerArticleNumber { get; set; }
    public string SupplierName { get; set; }
    public int SupplierId { get; set; }    
    public string SupplierArticleNumber { get; set; }
    public Quantity RequestedQuantity { get; set; }
    public string[] Memo { get; set; }
    public TecDocType[] TecDocTypes { get; set; }
    public GenericArticle[] GenericArticles { get; set; }
    public string[] EAN { get; set; }
    public string Description { get; set; }
    public byte FlagPackedForSelfService { get; set; }
    public byte FlagMaterialLabelingObligation { get; set; }
    public byte FlagExchangePart { get; set; }
    public byte FlagAccessory { get; set; }
    public int ArticleStatus { get; set; }
    public string ExtendedDescription { get; set; }
    public string[] ReferenceNumbers { get; set; }
    public string[] UtilityNumbers{ get; set; }
    public Criterion[] Criteria { get; set; }
  } 
} 
