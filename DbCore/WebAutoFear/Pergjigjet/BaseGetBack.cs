namespace DbCore.WebAutoFear.Pergjigjet
{
    public class BaseGetBack
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public BaseGetBack()
        {

        }

        public BaseGetBack(int ErrorCode, string ErrorMessage)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMessage = ErrorMessage;
        }
    }
}