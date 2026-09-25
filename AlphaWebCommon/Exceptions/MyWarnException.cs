using System;
using System.Runtime.Serialization;

namespace DbCore
{

    [Serializable]
    public class MyWarnException : Exception
    {
        public MyWarnException()
        {

        }
        public MyWarnException(string message)
            : base(message)
        {

        }
        public MyWarnException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
  //: base(string.Format(message, args))
        public MyWarnException(string message, Exception innerException, params object[] args)
         : base(message, innerException)
        {

        }
        protected MyWarnException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
