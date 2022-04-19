using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.Integrime.OTC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace DbCore.DbOTC
{
    public class OTCMPESATransferMoney : OTCMPESAOperation
    {
        private string _llogariaTransferimit;
        private decimal _shumaPerTuTrasferuar;
        private string nrFature;

        public string LlogariaTransferimit
        {
            get
            {
                return _llogariaTransferimit;
            }

            set
            {
                _llogariaTransferimit = value;
            }
        }

        public decimal ShumaPerTuTrasferuar
        {
            get
            {
                return _shumaPerTuTrasferuar;
            }

            set
            {
                _shumaPerTuTrasferuar = value;
            }
        }

        public string NrFature
        {
            get
            {
                return nrFature;
            }

            set
            {
                nrFature = value;
            }
        }

        public OTCMPESATransferMoney(string conversationID)
        {
            Id = int.Parse(conversationID.Split('_')[1]);
            LexoTransferte();
        }


        public void LexoTransferte()
        {
            using (var db = new clsDatabaseOTC())
                LexoTransferte(db);

        }
        internal void LexoTransferte(clsDatabaseOTC db)
        {
            db.MerrTransferteSipasID(Id, this);
        }
        public OTCMPESATransferMoney() { }
        /// <summary>
        /// todo sipas dokut
        /// </summary>

        public OTCMPESATransferMoney(string conversID, StatusOTC statusi, string mesazhi)
        {
            _conversationId = conversID;
            _id = int.Parse(conversID.Split('_')[1]);
            _statusTransaksioni = statusi;
            mesazhTransaksioni = mesazhi;
        }
        public override clsMesazh Ruaj()
        {
            using (var db = new clsDatabaseOTC())
            {
                Id = db.RuajTransferte(ConversationId, NrLlogarie, LlogariaTransferimit, ShumaPerTuTrasferuar, StatusTransaksioni, MesazhTransaksioni, IdPerdoruesiAlphaWeb, IdPerdoruesiMpesa, IdNdermarrje);
                ConversationId = $"OTC_{Id}";
            }
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        public override void Mbush(IDataRecord record)
        {
            base.Mbush(record);
            LlogariaTransferimit = record["LlogariaTransferimit"]?.ToString();
            decimal.TryParse(record["VLERATRANSFERUAR"]?.ToString(), out _shumaPerTuTrasferuar);
        }

        public override clsMesazh Modifiko()
        {
            using (var db = new clsDatabaseOTC())
            {
                db.ModifikoTransferte(Id, ConversationId, NrLlogarie, LlogariaTransferimit, ShumaPerTuTrasferuar, StatusTransaksioni, MesazhTransaksioni, IdPerdoruesiAlphaWeb, IdPerdoruesiMpesa, IdNdermarrje);
            }
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }


        /// <summary>
        /// kryen transferten ne MPESA
        /// </summary>
        /// <param name="pagesa"></param>
        /// <param name="user"></param>
        /// <returns></returns>
     
        private static clsMesazh MundTeTransferohetPagesa(OTCPagesa pagesa)
        {
            switch (pagesa.Transferta.StatusTransaksioni)
            {
                case StatusOTC.Pending:
                    return new clsMesazh(false, "Transferta nuk mund te kryhet sepse nje transferte eshte duke u ekzekutuar!");
                case StatusOTC.Completed:
                    return new clsMesazh(false, "Transferta nuk mund te kryhet sepse eshte transferuar njehere!!");
            }
            return new clsMesazh(true, "Pagesa eshte gati per transferim!");
        }
    }
}
