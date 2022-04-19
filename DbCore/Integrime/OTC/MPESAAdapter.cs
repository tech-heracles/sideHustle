using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbOTC;
using DbCore.MPESARequestProxy;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using DbCore.DbAdmin;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Net;
using DbCore.IMBUtils.Logging;

namespace DbCore.Integrime.OTC
{
    public class MPESAAdapter : IDisposable
    {
        MpesaServiceClient client;
        const string requestMessageHeader = "<![CDATA[<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        const string requestMessageFooter = "]]>";
        const string balanceQueryCommand = "InitTrans_BalanceQuery";
        const string transfertAmountCommand = "InitTrans_BusinessDeposit";
        MPESARequest _kerkesaDefault;
        MPESARequest KerkesaDefault
        {
            get
            {
                if (_kerkesaDefault == null)
                {
                    
                    var konfigDefault = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.MPESA_DEFAULT_REQUEST);
                    try
                    {
                        _kerkesaDefault = JsonConvert.DeserializeObject<MPESARequest>(konfigDefault.ToString());
                    }
                    catch (Exception ex)
                    {
                        ImbLogger.LogOTC("Deshtoi leximi i konfigurimit default per MPESA REQUEST ", konfigDefault, ex);
                        throw new Exception("Deshtoi leximi i konfigurimit default per MPESA REQUEST!Shikoni loget per me teper detaje!");
                    }

                }
                return _kerkesaDefault;
            }
        }
 
        public void Dispose()
        {
            client.Close();
        }
    }
}
