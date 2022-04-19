using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    public class clsInfoKoka
    {
        /// <summary>
        /// Kjo klase permban metodat e nevojshme per te perdorur te dhenat e tabeles T_INFOKOKA
        /// Keto te dhena percaktojne konfigurimin e infos se t per faturat e shitjes dhe blerjes
        /// </summary>
       
        #region Atribute

        private int idInfoKoka;
        private string kodi;
        private string pershkrimi;
        private int idPeriudha;
        private int idNdermarje;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimit;
        private DateTime dtModifikimit;
        private colInfoTrupi infoTrupi;
        private int lloji;
        private string konfiguro;
        private int idFormatNumri;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori me parametra
        /// </summary>
        /// <param name="idInfoKoka">id infos t</param>
        /// <param name="kodiInfo">kodi</param>
        /// <param name="pershkrimiInfo">pershkrimi</param>
        /// <param name="idPeriudhe">periudha</param>
        /// <param name="idNder">id ndermarrjes</param>
        /// <param name="idPerd">id perdoruesit</param>
        /// <param name="idStatus">id statusit dok</param>
        /// <param name="lloji"> lloji 1-artikull 2-klient furnitor</param>
        public clsInfoKoka(int idInfoKoka, string kodiInfo, string pershkrimiInfo, int idPeriudhe, int idNder,
                                    int idPerd, int idStatus, int lloji, int idFormatNumri)
        {
            this.idInfoKoka = idInfoKoka;
            this.kodi = kodiInfo;
            this.pershkrimi = pershkrimiInfo;
            this.idPeriudha = idPeriudhe;
            this.idNdermarje = idNder;
            this.idPerdoruesi = idPerd;
            this.idStatusDok = idStatus;
            this.lloji = lloji;
            this.idFormatNumri = idFormatNumri;
            infoTrupi = new colInfoTrupi();
        }
        /// <summary>
        /// Konstruktori qe kthen info  koka sipas id qe i kalohet si parameter
        /// </summary>
        /// <param name="idInfoKoka">id infos se t</param>
        public clsInfoKoka(int idInfoKoka)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushInfoKoka(data.merrInfoKokaSipasID(idInfoKoka));
            data.Dispose();
        }

        public clsInfoKoka(int idInfoKoka, clsDatabaseAdmin data)
        {
            mbushInfoKoka(data.merrInfoKokaSipasID(idInfoKoka));
        }


        /// <summary>
        /// Konstruktori qe kthen info  koka sipas kodit dhe ndermarrjes qe i kalohet si parameter
        /// </summary>
        /// <param name="kodiInfoKoka">kodi infos se t</param>
        /// <param name="idNdermarje">id ndermarrjes se infos se t</param>
        public clsInfoKoka(string kodiInfoKoka, int idNdermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushInfoKoka(data.merrInfoKokaSipasKodit(kodiInfoKoka, idNdermarje));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default
        /// </summary>
        public clsInfoKoka()
        {

        }

        public clsInfoKoka(DataRow rreshti)
        {
            
            mbushInfoKoka(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdInfoKoka
        {
            get { return idInfoKoka; }
            set { idInfoKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e modelit te infos se t
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }


        /// <summary>
        /// Kthen/Vendos pershkrimin e modelit te infos se t
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }


        /// <summary>
        /// Kthen/Vendos vleren numerike te periudhes(0, 1) se modelit te infos se t
        /// </summary>
        public int IdPeriudha
        {
            get { return idPeriudha; }
            set { idPeriudha = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e ndermarrjes se modelit te infos se t
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos id se perdoruesit te modelit te infos se t
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }


        /// <summary>
        /// Kthen/Vendos id e statusit te modelit te infos se t
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e krijimit te modelit te infos se t
        /// </summary>
        public DateTime DtKrijimit
        {
            get { return dtKrijimit; }
            set { dtKrijimit = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit te modelit te infos se t
        /// </summary>
        public DateTime DtModifikimit
        {
            get { return dtModifikimit; }
            set { dtModifikimit = value; }
        }

        /// <summary>
        /// Kthen/Vendos collection-in me trupat e infos se t
        /// </summary>
        public colInfoTrupi InfoTrupi
        {
            get { return infoTrupi; }
            set { infoTrupi = value; }
        }

        public int Lloji
        {
            get
            {
                return lloji;
            }
            set
            {
                lloji = value;
            }
        }
        public string Konfiguro
        {
            get
            {
                return konfiguro;
            }
            set
            {
                konfiguro = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos id e formatit te numrit, pra me sa shifra pas presjes do jene vlerat
        /// </summary>
        public int IdFormatNumri
        {
            get { return idFormatNumri; }
            set { idFormatNumri = value; }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan modelin e infos se t
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me ruajtjen e sukseshme, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public clsMesazh ruajInfo()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh;
            
            dbAdmin.beginTransaksion();
            try
            {
                //Kontrollome nese ekziston ne DB model info  me kete kod, brenda ndermarrjes
                if (dbAdmin.ekzistonInfoKokaSipasKodNdermarje(this.kodi, this.idNdermarje))
                {
                    return new clsMesazh(false, "Ekziston nje model info  me kete kod!");
                }
                int id;
                mesazh = dbAdmin.ruajInfoKoka(out id, this.Kodi, this.Pershkrimi, this.IdPeriudha, this.IdNdermarje, this.IdPerdoruesi, this.IdStatusDok, this.lloji, this.IdFormatNumri);
                //Nqs ruhet me sukses koka e infos, vazhdojme me ruajtjen e trupit.
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                this.idInfoKoka = id;
                foreach (clsInfoTrupi tr in this.infoTrupi)
                {
                    mesazh = dbAdmin.ruajInfoTrupi(id, tr.EmerKolone, tr.PershkrimKolone, tr.Visibility, tr.Rendi);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return new clsMesazh(false, mesazh.PershkrimMesazhi);
                    }
                }
                //Nqs ruhet me sukses edhe trupi kthejme mesazhin e suksesit
                dbAdmin.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                //Ne te gjitha rastet e tjera kthejme mesazhin me pershkrimin e gabimit qe ka ndodhur                                             
            }
            catch (Exception e)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, e.Message);
            }
        }

        /// <summary>
        /// Modifikon koken dhe trupin e modelit te infos se t
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me modifikimin e sukseshem, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public clsMesazh modifikoInfo()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh;
            
            dbAdmin.beginTransaksion();
            try
            {
                mesazh = dbAdmin.modifikoInfoKoka(this.idInfoKoka, this.Kodi, this.Pershkrimi, this.IdPeriudha, this.IdNdermarje, this.IdPerdoruesi, this.IdStatusDok, this.lloji, this.IdFormatNumri);
                //Nqs ruhet me sukses koka e infos, vazhdojme me ruajtjen e trupit.
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                mesazh = dbAdmin.fshiInfoTrupiSipasIdKoka(this.idInfoKoka);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                foreach (clsInfoTrupi tr in this.infoTrupi)
                {
                    mesazh = dbAdmin.ruajInfoTrupi(this.idInfoKoka, tr.EmerKolone, tr.PershkrimKolone, tr.Visibility, tr.Rendi);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return new clsMesazh(false, mesazh.PershkrimMesazhi);
                    }
                }
                //Nqs ruhet me sukses edhe trupi kthejme mesazhin e suksesit                    
                dbAdmin.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            }
            catch (Exception e)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, e.Message);
            }
        }

        /// <summary>
        /// Fshin infon e t
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me fshirjen e sukseshme, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public clsMesazh fshiInfo()
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            clsMesazh mesazh;
            
            dbAdmin.beginTransaksion();
            try
            {
                //Ndryshon statusin e infos se t(nuk e fshin ate plotesisht nga DB)
                mesazh = dbAdmin.fshiInfoStatus(this.idInfoKoka, this.idPerdoruesi);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                //Ne rastin kur fshirja behet ne menyre te sukseshme bejme commit transkasionin dhe kthejme mesazhin e suksesit
                dbAdmin.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            }
            catch (Exception e)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, e.Message);
            }
        }

        /// <summary>
        /// Kthen nje datatable me te gjitha modelet e infos se t per ndermarrjen me id qe i kalohet si parameter
        /// </summary>
        /// <param name="idNdermarje"></param>
        /// <returns></returns>
        public static DataTable ktheInfoPerGride(int idNdermarje)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DataTable result = dbAdmin.merrInfoKokaNdermarrjesPerGride(idNdermarje);
            dbAdmin.Dispose();
            return result;
        }

         /// <summary>
        /// Kthen nje datatable me te gjitha modelet e infos se t per ndermarrjen me id qe i kalohet si parameter
        /// </summary>
        /// <param name="idNdermarje"></param>
        /// <returns></returns>
        public static DataTable ktheInfoPerGrideDheLlojit(int idNdermarje, int lloji)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DataTable result = dbAdmin.merrInfoKokaNdermarrjesPerGrideSipasLloji(idNdermarje, lloji);
            dbAdmin.Dispose();
            return result;
        }
        /// <summary>
        /// Kthen nje datarow me modelin e infos se t me id qe i kalohet si parameter
        /// </summary>
        /// <param name="idNdermarje"></param>
        /// <returns></returns>
        public static DataRow ktheInfoSipasID(int idInfo)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DataRow result = dbAdmin.merrInfoKokaSipasID(idInfo);
            dbAdmin.Dispose();
            return result;
        }
        public static bool kaVeprime(int idinfo)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool sukses = db.kaVeprimeInfo(idinfo);
            db.Dispose();
            return sukses;
        }
        #endregion

        #region Metoda Internal

        internal bool mbushInfoKoka(DataRow dbDataRowInfoKoka)
        {
            if (dbDataRowInfoKoka != null)
            {
                try
                {
                    int.TryParse(dbDataRowInfoKoka["IDINFOKOKA"].ToString(), out idInfoKoka);
                    kodi = dbDataRowInfoKoka["KODI"].ToString();
                    pershkrimi = dbDataRowInfoKoka["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowInfoKoka["PERIUDHA"].ToString(), out idPeriudha);
                    int.TryParse(dbDataRowInfoKoka["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowInfoKoka["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowInfoKoka["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowInfoKoka["LLOJI"].ToString(), out lloji);
                    DateTime.TryParse(dbDataRowInfoKoka["DTKRIJIMI"].ToString(), out dtKrijimit);
                    DateTime.TryParse(dbDataRowInfoKoka["DTMODIFIKIMI"].ToString(), out dtModifikimit);
                    if (dbDataRowInfoKoka["IDFORMATNUMRI"] != DBNull.Value)
                        idFormatNumri = Convert.ToInt32(dbDataRowInfoKoka["IDFORMATNUMRI"]);
                    infoTrupi = DbCore.DbAdmin.colInfoTrupi.merrInfoTrupiSipasIdKoka(idInfoKoka);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se infos se t nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
