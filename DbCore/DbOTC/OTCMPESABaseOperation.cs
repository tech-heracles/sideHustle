using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.Integrime.OTC;

namespace DbCore.DbOTC
{
    public abstract class OTCMPESAOperation : OTCLogData, IDataBaseReader
    {
        protected int _id;
        protected string _nrLlogarie;
        protected string _conversationId;
        #region atributet
        public string ConversationId
        {
            get { return _conversationId; }
            set { _conversationId = value; }
        }
        public string NrLlogarie
        {
            get { return _nrLlogarie; }
            set { _nrLlogarie = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion
        public virtual void Mbush(IDataRecord record)
        {
            int.TryParse(record["Id"].ToString(), out _id);
            ConversationId = record["ConversationID"]?.ToString();
            NrLlogarie = record["NrLlogarie"]?.ToString();
            Enum.TryParse(record["STATUSTRANSAKSIONI"].ToString(), out _statusTransaksioni);
            mesazhTransaksioni = record["MESAZHTRANSAKSIONI"]?.ToString();
            int.TryParse(record["IdPerdoruesiAlphaWeb"].ToString(), out idPerdoruesiAlphaWeb);
            int.TryParse(record["IdPerdoruesiMpesa"].ToString(), out idPerdoruesiMPESA);
            int.TryParse(record["IdNdermarrje"].ToString(), out idNdermarrje);
        }
        public abstract clsMesazh Modifiko();
        protected static clsMesazh ModifikoStatus(string conversationID, StatusOTC statusi, string mesazhi)
        {
            using (var db = new clsDatabaseOTC())
                return db.ModifikoStatusMPESAOperation(conversationID, statusi, mesazhi);
        }

        public static clsMesazh VendosPergjigjeOperacioni(MPESAResult rezultati, ResultParameters resultParam)
        {
            if (rezultati.ResultCode == ((int)MPESAStatusCode.Sukses).ToString())
            {
                var accountBalance = resultParam.ResultParameter.FirstOrDefault(x => x.Key == "AccountBalance");

                //rezultat suksesi!
                if (accountBalance != null)
                {
                    //pergjigje balances!
                    var balanca = decimal.Parse(accountBalance.Value.Split('|')[2]);
                    var checkBalance = new OTCMPESACheckBalance(rezultati.OriginatorConversationID);

                    if (checkBalance.StatusTransaksioni == StatusOTC.Completed)
                        throw new MyException($"Per balancen me ID {checkBalance.ConversationId} eshte vendosur njehere rezultati!");

                    checkBalance.MesazhTransaksioni = rezultati.ResultDesc;
                    checkBalance.StatusTransaksioni = StatusOTC.Completed;
                    checkBalance.Balanca = balanca;

                    return checkBalance.Modifiko();
                }
                var transferMoney = new OTCMPESATransferMoney(rezultati.OriginatorConversationID);

                if (transferMoney.StatusTransaksioni == StatusOTC.Completed)
                    throw new MyException($"Per transferten me ID {transferMoney.ConversationId} eshte vendosur njehere rezultati!");

                transferMoney.MesazhTransaksioni = rezultati.ResultDesc;
                transferMoney.StatusTransaksioni = StatusOTC.Completed;

                return transferMoney.Modifiko();
            }
            return ModifikoStatus(rezultati.OriginatorConversationID, StatusOTC.Fail, $"pergjigja: {rezultati.ToString()} ");
        }

    }
}
