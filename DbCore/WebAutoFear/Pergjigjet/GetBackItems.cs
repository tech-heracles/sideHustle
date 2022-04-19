using DbCore.WebAutoFear.Klasat;
namespace DbCore.WebAutoFear.Pergjigjet
{
    public class GetBackItems : BaseGetBack
    {
        public Item[] Items { get; set; }
        public ResponseLimit ResponseLimit { get; set; }

        public GetBackItems()
        {

        }

        public GetBackItems(int ErrorCode, string ErrorMessage, Item[] Items, ResponseLimit ResponseLimit) : base(ErrorCode, ErrorMessage)
        {
            this.Items = Items;
            this.ResponseLimit = ResponseLimit;
        }
    }
}