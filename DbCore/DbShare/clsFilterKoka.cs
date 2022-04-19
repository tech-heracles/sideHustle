using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbAdmin;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbShare
{
    public class clsFilterKoka
    {
        #region Attribute
        private int idKokaFilter;
        private String kokaKodi;
        private String kokaPershkrimi;
        private int idPerdoruesi;
        private int idNdermarrje;
        private int idRaport;
        private bool localRaprot;
        private bool localPerdorues;
        private bool localNdermarrje;
        //private colFilterTrupi oColFilterTrupi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;
        #endregion

        #region Properties

        public int IdKokaFilter
        {
            get { return idKokaFilter; }
            set { idKokaFilter = value; }
        }

        public String KokaFilterKodi
        {
            get { return kokaKodi; }
            set { kokaKodi = value; }
        }

        public String KokaFilterPershkrimi
        {
            get { return kokaPershkrimi; }
            set { kokaPershkrimi = value; }
        }

        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }
        public int IdStatusDok
            {
            get { return idStatusDok; }
            set { idStatusDok = value; }
            }
        public DateTime DtKrijimi
            {
            get { return dtKrijimi; }

            }
        public DateTime DtModifikimi
            {
            get { return dtModifikimi; }

            }
        //public colFilterTrupi OColFilterTrupi
        //{
        //    get { return oColFilterTrupi; }
        //    set { oColFilterTrupi = value; }
        //}
        /// <summary>
        /// kam pershtypjen qe set-i nuk duhet lene
        /// </summary>
        public int IdRaport
        {
            get
            {
                return idRaport;
            }           
            set
            {
                if (idRaport == value)
                    return;
                idRaport = value;
            }
        }
        #endregion

        #region Kontruktoret
        /// <summary>
        /// Konstruktor qe perdoret per te krijuar nje filterkoka te ri ne db idFilterKoka nuk jepet si parameter sepse eshte autoincrement
        /// </summary>
        /// <param name="kodi">kodi i filtrit</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idperdoruesi">perdoruesi qe po e krijon</param>
        /// <param name="idndermarrje">ndermarrja ku ndodhet</param>
        /// <param name="idRaport">raporti ku po krijohet</param>
        /// <param name="localRaprot">eshte local per raportin apo jo</param>
        /// <param name="localPerdorues">eshte personal per perdoruesin apo jo</param>
        /// <param name="localNdermarrje">eshte local per ndermarrjen apo jo</param>
        public clsFilterKoka(String kodi, String pershkrimi, int idperdoruesi, int idndermarrje, 
            int idRaport, bool localRaprot, bool localPerdorues, bool localNdermarrje, int idstatusdok)
        {            
            kokaKodi = kodi;
            kokaPershkrimi = pershkrimi;
            //filterDefault = def;
            idPerdoruesi = idperdoruesi;
            idNdermarrje = idndermarrje;
            this.idRaport = idRaport;
            this.localRaprot = localRaprot;
            this.localPerdorues = localPerdorues;
            this.localNdermarrje = localNdermarrje;
            this.idStatusDok = idstatusdok;
            //oColFilterTrupi = new colFilterTrupi();            
        }

        public clsFilterKoka(String kodi)
        {
            kokaKodi = kodi;

        }
        public clsFilterKoka(int idkoka)
        {
            idKokaFilter = idkoka;
        }
        public clsFilterKoka()
        {

        }

        public clsFilterKoka(DataRow rreshti)
        {
            
            mbushFilterKoka(rreshti);
        }
        #endregion

        #region Metoda Publike

        public clsMesazh ruajFiltrin(int idKokaFilter, String kodi, String pershkrimi, int idperdoruesi, int idndermarrje, int idRaport, colFilterTrupi filterTrupi, int idstatusdok)
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            clsMesazh mesazh;
            DbCore.DbShare.clsDatabaseShare dbshare= new clsDatabaseShare();
            //dbshare.krijoManager();
            dbshare.beginTransaksion();
          
            try
            {
                int idK;
                mesazh = dbshare.ruajFilterKoka(out idK, kodi, pershkrimi, idperdoruesi, idndermarrje, idstatusdok);
                if (mesazh.Status)
                {
                    idKokaFilter = idK;
                    foreach (clsFilterTrupi o in filterTrupi)
                    {
                        if (mesazh.Status)
                        {
                            o.IdKokaFilter = idKokaFilter;
                            mesazh = o.ruaj();
                        }
                        else
                        {
                            dbshare.rollbackTransaksion();
                             
                            return mesazh;
                        }
                    }
                    if (mesazh.Status)
                    {
                        mesazh = dbshare.RuajLidhjenRaportFilter(idRaport, idKokaFilter);
                        if (mesazh.Status)
                        {
                            dbshare.commitTransaksion();
                            
                            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                            return mesazh;
                        }

                        else
                        {
                            dbshare.rollbackTransaksion();
                            
                            return new clsMesazh(false, mesazh.PershkrimMesazhi);
                        }
                    }
                    else
                    {
                        dbshare.rollbackTransaksion();
                        
                        return new clsMesazh(false, mesazh.PershkrimMesazhi);
                    }
                }
                else
                {
                    dbshare.rollbackTransaksion();
                    
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }

            }

            catch (Exception ce)
            {
                dbshare.rollbackTransaksion();
                
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh krijoFilter(colFilterTrupi filterTrupi)
        {
            clsDatabaseShare data = new clsDatabaseShare();            
            clsMesazh u_ruajt = null;
            try
            {
                data.beginTransaksion();
                u_ruajt = data.krijoFilterKoka(out idKokaFilter, kokaKodi, kokaPershkrimi, idPerdoruesi,
                    idNdermarrje, idRaport, localRaprot, localPerdorues, localNdermarrje,idStatusDok);
                if (u_ruajt.Status)
                    foreach (clsFilterTrupi trup in filterTrupi)
                        trup.IdKokaFilter = idKokaFilter;
                if (u_ruajt.Status)
                    u_ruajt = data.krijoFilterTrupi(filterTrupi.mbushColFilterTrupi());
                if (u_ruajt.Status)
                    data.commitTransaksion();
                else
                    data.rollbackTransaksion();
                return u_ruajt;
            }
            catch (Exception)
            {
                data.rollbackTransaksion();
                if (u_ruajt == null)
                    return new clsMesazh(false, "Gabim i pa njohur gjate krijimit te filtrit");
                return u_ruajt;
            }
        }

        public clsMesazh ruaj()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            int id;
            clsMesazh u_ruajt = data.ruajFilterKoka(out id, this.KokaFilterKodi, this.KokaFilterPershkrimi, this.IdPerdoruesi, this.IdNdermarrje, this.idStatusDok);
            data.Dispose();
            return u_ruajt;
        }

        public clsMesazh modifiko()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_modifikua = data.modifikoFilterKoka(this.IdKokaFilter, this.KokaFilterKodi, this.KokaFilterPershkrimi, this.IdPerdoruesi, this.IdNdermarrje, this.idStatusDok);
            data.Dispose();
            return u_modifikua;
        }
        public clsMesazh fshistatus()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh ufshi = data.fshiFilterKokaStatus(this.IdKokaFilter, this.IdPerdoruesi);
            data.Dispose();
            return ufshi;
        }
        public clsMesazh fshi()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            //data.krijoManager();
            data.beginTransaksion();
            colFilterTrupi filtratrupi = new colFilterTrupi(idKokaFilter);
            bool uFshiTrupi = filtratrupi.fshi(data);
            clsMesazh u_fshi = data.fshiFilterKoka(this.IdKokaFilter);
            u_fshi.Status = u_fshi.Status && uFshiTrupi;
            if (u_fshi.Status)
                data.commitTransaksion();
            else
                data.rollbackTransaksion();
            
            return u_fshi ;
        }

        //duhet pare metoda merrTrupinFiltri
        //public colFilterTrupi merrFilterTrupin()
        //{
        //    clsDatabaseShare data = new clsDatabaseShare();
        //    return mbushFilterKoka(data.merrTrupinFiltri(idKokaFilter));
        //}
        //[Obsolete("Nuk perdoret:",true)]
        //public void merr(int idRaporti)
        //{
        //    clsDatabaseShare data = new clsDatabaseShare();
        //    data.merrFilterKokaDefaultRap(idRaporti);
        //}

        public colFilterKoka merriTeGjitheNgaModuli(int idModuli)
        {
            colFilterKoka data = new colFilterKoka();
            data.mbushGjitheFilterKokaModuli(idModuli);
            return data;

        }
        #endregion

        #region Metoda Internal

        internal bool mbushFilterKoka(DataRow rreshti)
        {   
            int.TryParse(rreshti["IDKOKAFILTER"].ToString(),out idKokaFilter);
            kokaKodi = rreshti["KOKAFILTERKODI"].ToString();
            kokaPershkrimi = rreshti["KOKAFILTERPERSHKRIMI"].ToString();
            int.TryParse(rreshti["IDPERDORUESI"].ToString(),out idPerdoruesi);           
            int.TryParse(rreshti["IDNDERMARRJE"].ToString(),out idNdermarrje);        
            int.TryParse(rreshti["IDRAP"].ToString(),out idRaport); 
            bool.TryParse(rreshti["LOCALRAPORT"].ToString(),out localRaprot); 
            bool.TryParse(rreshti["LOCALPERDORUES"].ToString(),out localPerdorues); 
            bool.TryParse(rreshti["LOCALNDERMARRJE"].ToString(), out localNdermarrje);
            int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
            DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
            DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
            // oColFilterTrupi = oTrupi.merrFilterTrupin(koka);
            return true;
        }

        #endregion
    }
}
