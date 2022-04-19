
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace DbCore.Integrime.OTC
{
    [XmlRoot(ElementName = "Caller", IsNullable = true)]
    public class Caller
    {
        [XmlElement(ElementName = "CallerType", IsNullable = true)]
        public string CallerType { get; set; }
        [XmlElement(ElementName = "ThirdPartyID", IsNullable = true)]
        public string ThirdPartyID { get; set; }
        [XmlElement(ElementName = "Password", IsNullable = true)]
        public string Password { get; set; }
        [XmlElement(ElementName = "ResultURL", IsNullable = true)]
        public string ResultURL { get; set; }
    }

    [XmlRoot(ElementName = "Initiator", IsNullable = true)]
    public class Initiator
    {
        [XmlElement(ElementName = "IdentifierType", IsNullable = true)]
        public string IdentifierType { get; set; }
        [XmlElement(ElementName = "Identifier", IsNullable = true)]
        public string Identifier { get; set; }
        [XmlElement(ElementName = "SecurityCredential", IsNullable = true)]
        public string SecurityCredential { get; set; }
        [XmlElement(ElementName = "ShortCode", IsNullable = true)]
        public string ShortCode { get; set; }
    }

    [XmlRoot(ElementName = "ReceiverParty", IsNullable = true)]
    public class ReceiverParty
    {
        [XmlElement(ElementName = "IdentifierType", IsNullable = true)]
        public string IdentifierType { get; set; }
        [XmlElement(ElementName = "Identifier", IsNullable = true)]
        public string Identifier { get; set; }
        [XmlElement(ElementName = "SecurityCredential", IsNullable = true)]
        public string SecurityCredential { get; set; }
    }

    [XmlRoot(ElementName = "Identity", IsNullable = true)]
    public class Identity
    {
        [XmlElement(ElementName = "Caller", IsNullable = true)]
        public Caller Caller { get; set; }
        [XmlElement(ElementName = "Initiator", IsNullable = true)]
        public Initiator Initiator { get; set; }
        [XmlElement(ElementName = "ReceiverParty", IsNullable = true)]
        public ReceiverParty ReceiverParty { get; set; }
    }

    [XmlRoot(ElementName = "Parameter", IsNullable = true)]
    public class Parameter
    {
        [XmlElement(ElementName = "Key", IsNullable = true)]
        public string Key { get; set; }
        [XmlElement(ElementName = "Value", IsNullable = true)]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "Parameters", IsNullable = true)]
    public class Parameters
    {
        [XmlElement(ElementName = "Parameter", IsNullable = true)]
        public List<Parameter> Parameter { get; set; }
    }

    [XmlRoot(ElementName = "ReferenceItem", IsNullable = true)]
    public class ReferenceItem
    {
        [XmlElement(ElementName = "Key", IsNullable = true)]
        public string Key { get; set; }
        [XmlElement(ElementName = "Value", IsNullable = true)]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "ReferenceData", IsNullable = true)]
    public class ReferenceData
    {
        [XmlElement(ElementName = "ReferenceItem", IsNullable = true)]
        public ReferenceItem ReferenceItem { get; set; }
    }

    [XmlRoot(ElementName = "Transaction", IsNullable = true)]
    public class Transaction
    {
        [XmlElement(ElementName = "CommandID", IsNullable = true)]
        public string CommandID { get; set; }
        [XmlElement(ElementName = "LanguageCode", IsNullable = true)]
        public string LanguageCode { get; set; }
        [XmlElement(ElementName = "ConversationID", IsNullable = true)]
        public string ConversationID { get; set; }

        [XmlElement(ElementName = "Remark", IsNullable = true)]
        public string Remark { get; set; }
        [XmlElement(ElementName = "OriginatorConversationID", IsNullable = true)]
        public string OriginatorConversationID { get; set; }
        [XmlElement(ElementName = "Parameters", IsNullable = true)]
        public Parameters Parameters { get; set; }
        [XmlElement(ElementName = "ReferenceData", IsNullable = true)]
        public ReferenceData ReferenceData { get; set; }
        [XmlElement(ElementName = "Timestamp", IsNullable = true)]
        public string Timestamp { get; set; }
    }

    [XmlRoot(ElementName = "Request", IsNullable = true)]
    public class MPESARequest
    {
        [XmlElement(ElementName = "Identity", IsNullable = true)]
        public Identity Identity { get; set; }
        [XmlElement(ElementName = "Transaction", IsNullable = true)]
        public Transaction Transaction { get; set; }
        [XmlElement(ElementName = "KeyOwner", IsNullable = true)]
        public string KeyOwner { get; set; }
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
    [XmlRoot(ElementName = "Response", IsNullable = true)]
    public class MPESAResponse
    {
        [XmlElement(ElementName = "ResponseCode", IsNullable = true)]
        public string ResponseCode { get; set; }
        [XmlElement(ElementName = "ConversationID", IsNullable = true)]
        public string ConversationID { get; set; }
        [XmlElement(ElementName = "ResponseDesc", IsNullable = true)]
        public string ResponseDesc { get; set; }
        [XmlElement(ElementName = "OriginatorConversationID", IsNullable = true)]
        public string OriginatorConversationID { get; set; }
        [XmlElement(ElementName = "ServiceStatus", IsNullable = true)]
        public string ServiceStatus { get; set; }
    }
    [XmlRoot(ElementName = "ResultParameter")]
    public class ResultParameter
    {
        [XmlElement(ElementName = "Key")]
        public string Key { get; set; }
        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "ResultParameters")]
    public class ResultParameters
    {
        [XmlElement(ElementName = "ResultParameter")]
        public List<ResultParameter> ResultParameter { get; set; }
    }

    [XmlRoot(ElementName = "Result")]
    public class MPESAResult
    {
        [XmlElement(ElementName = "ResultParameters")]
        public ResultParameters ResultParameters { get; set; }
        [XmlElement(ElementName = "ResultType")]
        public string ResultType { get; set; }
        [XmlElement(ElementName = "ResultCode")]
        public string ResultCode { get; set; }
        [XmlElement(ElementName = "ResultDesc")]
        public string ResultDesc { get; set; }
        [XmlElement(ElementName = "OriginatorConversationID")]
        public string OriginatorConversationID { get; set; }
        [XmlElement(ElementName = "ConversationID")]
        public string ConversationID { get; set; }
        [XmlElement(ElementName = "TransactionID")]
        public string TransactionID { get; set; }
        [XmlElement(ElementName = "ReferenceData")]
        public ReferenceData ReferenceData { get; set; }
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }


}

