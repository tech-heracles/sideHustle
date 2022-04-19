using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAdmin
{
    public class clsWebhooks : IDataBase
    {
        #region Atributet
        private int idWebhook;
        private string kodiWebhook;
        private string urlpritese;
        private int idNdermarrje;
        private int idPerdoruesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        bool aktiv;
        private int idStatusDok;
        private int kategoria; 
        private IDataRecord record;
        private int eventi;
        #endregion
        #region Properties
        public int IdWebhook
        {
            get
            {
                return idWebhook;
            }
            set
            {
                idWebhook = value;
            }

        }
        public string KodiWebhook
        {
            get
            {
                return kodiWebhook;
            }
            set
            {
                kodiWebhook = value;
            }
        }
        public string Urlpritese
        {
            get
            {
                return urlpritese;
            }
            set
            {
                urlpritese = value;
            }
        }
        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos id e ndermarrjes
        /// </summary>
        public int IdNdermarje
        {
            get
            {
                return idNdermarrje;
            }
            set
            {
                idNdermarrje = value;
            }
        }
        /// <summary>
        /// Kthen daten e krijimit
        /// </summary>
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }

        }
        /// <summary>
        /// Kthen daten e modifikimit te fundit
        /// </summary>
        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }
        }
        public bool Aktive
        {
            get
            {
                return aktiv;
            }
            set
            {
                aktiv = value;
            }
        }
        public int IdStatusDok
        {
            get
            {
                return idStatusDok;
            }
            set
            {
                idStatusDok = value;
            }
        }
        public int Kategoria
        {
            get
            {
                return kategoria;
            }
            set
            {
                kategoria = value;
            }
        }
        public int Eventi
        {
            get
            {
                return eventi;
            }
            set
            {
                eventi = value;
            }
        }

        #endregion
        #region Konstruktoret
        public clsWebhooks()
        {
        }
        public clsWebhooks(IDataRecord record)
        {
            Mbush(record);
        }
        public clsWebhooks(int idNdermarrje)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
                dbAdm.mbushWebhooksIdNdermarrje(idNdermarrje, this);
        }
        public clsWebhooks(int idWebhooks, int idNdermarrje)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
            {
                dbAdm.merrWebhookSipasId(idWebhooks, idNdermarrje, this);
            }
        }
        public clsWebhooks(int idWebhooks, string kodi, string url, int idNdermarrje, int idPerdoruesi, bool aktive, int idStatusDok, int kategoria, int eventi)
        {
            this.idWebhook = idWebhooks;
            this.kodiWebhook = kodi;
            this.urlpritese = url;
            this.idNdermarrje = idNdermarrje;
            this.idPerdoruesi = idPerdoruesi;
            this.aktiv = aktive;
            this.idStatusDok = idStatusDok;
            this.kategoria = kategoria;
            this.eventi = eventi;
        }
        #endregion
        #region Metoda Publike
        public clsMesazh Ruaj()
        {
            clsMesazh mesazh = new clsMesazh(false);
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
               mesazh = ruajWebhookTeRi(dbAdmin);
            }
            return mesazh;
        }
        public clsMesazh Modifiko()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
                return modifiko(dbAdmin);

        }
        public clsMesazh ruajWebhookTeRi(clsDatabaseAdmin dbAdmin)
        {
            int id;
            clsMesazh u_ruajt = dbAdmin.ruajWebhook(out id, kodiWebhook, urlpritese,  idNdermarrje, idPerdoruesi, aktiv, kategoria, eventi);
            this.idWebhook = id;
            return u_ruajt;
        }
        public clsMesazh modifiko(clsDatabaseAdmin dbAdmin)
        {
            return dbAdmin.modifikowebhook(idWebhook, kodiWebhook, urlpritese,  idNdermarrje, idPerdoruesi, aktiv, kategoria, eventi);
        }
        public void mbushwEBHOOKSipasIdNdermarrje(int idNdermarrje)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
                dbAdm.mbushWebhooksIdNdermarrje(idNdermarrje, this);
        }
        public clsMesazh Fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiWebhook(this.idWebhook);
            data.Dispose();
            return u_fshi;
        }

        public static DataTable merrWebhookSipasNderm(int idNdermarrje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataTable tabela = data.ktheGjitheWebhookSipasNdermarrje(idNdermarrje);
            data.Dispose();
            return tabela;
        }
        public static DataRow merrWebhookSipasIdDR(int idWebhook)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataRow dr = data.ktheWebhookSipasIdDR(idWebhook);
            data.Dispose();
            return dr;
        }
        public static bool ekzistonWebhookFtp(string kodiwebhook, int idNdermarrje)
        {
            using (var data = new clsDatabaseAdmin())
                return data.ekzistonWebhookMeKeteKodPerKeteNdermarje(kodiwebhook, idNdermarrje);
        }
        public static int ktheIDWebhook(string kod, int idNdermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.merrIDWebhook(kod, idNdermarrje);
        }
        public static bool eshteEebhookAktiv(string kodiWebhook,int idndermarrje)
        {
            using (DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim())
                return dbRegjistrim.eshteAktiveWebhookSipasKodit(kodiWebhook,idndermarrje);
        }
        public void Mbush(IDataRecord record)
        {
            try
            {
                int.TryParse(record["IDWEBHOOKS"].ToString(), out idWebhook);
                kodiWebhook = record["KODIWEBHOOK"].ToString();
                bool.TryParse(record["AKTIVE"].ToString(), out aktiv);
                int.TryParse(record["IDNDERMARRJE"].ToString(), out idNdermarrje);
                int.TryParse(record["IDPERDORUESI"].ToString(), out idPerdoruesi);
                DateTime.TryParse(record["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(record["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
                int.TryParse(record["KATEGORIA"].ToString(), out kategoria);
                int.TryParse(record["EVENTI"].ToString(), out eventi);
                
            }
            catch (InvalidCastException ex)
            {
                ImbLogger.Error(ex);
                throw new MyException("ERROR: Gabim casti gjate marrjes se konfigurimit nga databaza!");
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                throw new MyException("ERROR: Gabim gjate marrjes se konfigurimit nga databaza!");
            }
        }

        #endregion
    }
}
