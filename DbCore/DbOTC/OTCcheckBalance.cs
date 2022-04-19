using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using DbCore.Integrime;
using DbCore.Integrime.OTC;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbOTC
{
    public class OTCMPESACheckBalance : OTCMPESAOperation
    {
        private decimal? _balanca;
        public int MyTimeout => 300000;
        public int FrekuencaKontrollit => 100;

        public decimal? Balanca
        {
            get { return _balanca; }
            set { _balanca = value; }
        }
        public OTCMPESACheckBalance() { }
        public OTCMPESACheckBalance(string conversationID)
        {
            Id = int.Parse(conversationID.Split('_')[1]);
            LexoBalance();

        }

        public void LexoBalance()
        {
            using (var db = new clsDatabaseOTC())
            {
                db.MerrCheckBalanceSipasID(Id, this);
            }
        }

        public override clsMesazh Ruaj()
        {
            try
            {
                using (var db = new clsDatabaseOTC())
                {
                    Id = db.RuajBalancen(ConversationId, NrLlogarie, Balanca, StatusTransaksioni, MesazhTransaksioni, IdPerdoruesiAlphaWeb, IdPerdoruesiMpesa, IdNdermarrje);
                    ConversationId = $"OTC_{Id}";
                }
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorOTC(ex);
                return new clsMesazh(false, $"Nuk mund te ruhet Balana ne db {ex.Message}");
            }
        }
        public override clsMesazh Modifiko()
        {
            try
            {
                using (var db = new clsDatabaseOTC())
                {
                    db.ModifikoBalancen(Id, ConversationId, NrLlogarie, Balanca, StatusTransaksioni, MesazhTransaksioni, IdPerdoruesiAlphaWeb, IdPerdoruesiMpesa, IdNdermarrje);
                }
                return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorOTC(ex);
                return new clsMesazh(false, $"Nuk mund te modifikohet balanca ne db {ex.Message}");
            }
        }

        public override void Mbush(IDataRecord record)
        {
            base.Mbush(record);
            decimal balanca;
            if (decimal.TryParse(record["Balanca"]?.ToString(), out balanca))
                _balanca = balanca;
        }
    }
}
