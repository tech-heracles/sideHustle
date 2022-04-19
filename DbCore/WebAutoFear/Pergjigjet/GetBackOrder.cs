using DbCore.WebAutoFear.Klasat;
namespace DbCore.WebAutoFear.Pergjigjet
{
  public class GetBackOrder : BaseGetBack
  {
     public Order Item { get; set; }
     public Item[] OrderedItems { get; set; } 

        public GetBackOrder()
        {

        }

        public GetBackOrder(int ErrorCode, string ErrorMessage, Order Item, Item[] OrderedItems) : base(ErrorCode, ErrorMessage)
        {
            this.OrderedItems = OrderedItems;
            this.Item = Item;
        }
    }
}