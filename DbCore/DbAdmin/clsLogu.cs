using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per te ruajtur veprimet e perdoruesve(klikimet ne ambjentet e ndryshme te alpha web) ne nje tabele ne db
    ///  ruhen klikimet ne ambjente, id e dokumentave te regjistrimet si dhe dhe raportet dhe faturat, kjo nqs kushti i logut te ndermarrja eshte true
    /// </summary> 
    public class clsLogu
    {
        #region Atributet

        private int idLogu;
        private int idKompon;
        private int idNdermarje;
        private int idPerdorues;
        private DateTime dtVeprimi;
        private string idDokRegjistrimi;
        private int idFature;
        #endregion

       #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        //konstruktoret
        public clsLogu(int idLogu, int idKompon, int idNdermarje, int idPerdorues, DateTime dtVeprimi, string idDokRegjistrimi, int idFature)
        {
            this.idLogu = idLogu;
            this.idKompon = idKompon;
            this.idNdermarje = idNdermarje;
            this.idPerdorues=idPerdorues;
            this.dtVeprimi = dtVeprimi;
            this.idDokRegjistrimi = idDokRegjistrimi;
            this.idFature = idFature;
     

        }

        public clsLogu(int idLogu, int idKompon, int idNdermarje, int idPerdorues, DateTime dtVeprimi, string idDokRegjistrimi, int idFature, bool ruajlog)
        {
            this.idLogu = idLogu;
            this.idKompon = idKompon;
            this.idNdermarje = idNdermarje;
            this.idPerdorues = idPerdorues;
            this.dtVeprimi = dtVeprimi;
            this.idDokRegjistrimi = idDokRegjistrimi;
            this.idFature = idFature;
              if (ruajlog)
            ruajLogun();

        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        //konstruktoret
        public clsLogu()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLogu
        {
            get
            {
                return idLogu;
            }
            set
            {
                idLogu = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e komponentes ne te cilen po klikon perdoruesi.
        /// </summary>
        public int IdKompon
        {
            get
            {
                return idKompon;
            }
            set
            {
                idKompon = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e ndermarrjes ne te cilen po punon perdoruesi
        /// </summary>
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e perdoruesit 
        /// </summary>
        public int IdPerdorues
        {
            get
            {
                return idPerdorues;
            }
            set
            {
                idPerdorues = value;
            }
        }

        /// <summary>
        /// Kthen daten dhe oren ne te cilen perdoruesi po klikon ne nje ambjent ose dokument te caktuar
        /// </summary>
        public DateTime DtVeprimi
        {
            get
            {
                return dtVeprimi;
            }
            set
            {
                dtVeprimi = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e dokumentit qe po hap perdoruesi, 0 nqs seshte dok. regjistrimi ose raport 
        /// </summary>
        public string IdDokRegjistrimi
        {
            get
            {
                return idDokRegjistrimi;
            }
            set
            {
                idDokRegjistrimi = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e fatures qe po hap klienti, null nqs seshte duke hapur fature
        /// </summary>
        public int IdFature
        {
            get
            {
                return idFature;
            }
            set
            {
                idFature = value;
            }
        }

        

        #endregion      

        #region Metoda Publike

        /// <summary>
        /// Ruan logun e veprimeve te klienteve
        /// </summary>
        /// <returns>Mesazhin qe jep logu ne lidhje me ruajtjen e sukseshme, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public clsMesazh ruajLogun()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh;

            dbAdmin.beginTransaksion();
            try
            {
              
                int id;
                //ruajLogun(out int idlogu, int idkomponente, int idndermarje, int idperdorues, DateTime dtveprimi, int iddokregj, int idfature)
                mesazh = dbAdmin.ruajLogun(out id, this.idKompon, this.idNdermarje, this.idPerdorues, this.dtVeprimi, this.idDokRegjistrimi, this.idFature);
                //Nqs ruhet me sukses koka e infos, vazhdojme me ruajtjen e trupit.
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
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
        #endregion
        #region Metoda Internal

        internal bool mbushLogun(DataRow dbDataRowLogu)
        {
            if (dbDataRowLogu != null)
            {
                try
                {
                    int.TryParse(dbDataRowLogu["IDLOGU"].ToString(), out idLogu);
                    int.TryParse(dbDataRowLogu["IDKOMPONENTE"].ToString(), out idKompon);
                    int.TryParse(dbDataRowLogu["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowLogu["IDPERDORUESI"].ToString(), out idPerdorues);
                    DateTime.TryParse(dbDataRowLogu["DTVEPRIMI"].ToString(), out dtVeprimi);
                    idDokRegjistrimi = dbDataRowLogu["IDDOKREGJ"].ToString().ToString();
                    int.TryParse(dbDataRowLogu["IDFATURE"].ToString(), out idFature);
                   
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se logut se t nga db-ja");
                }
            }
            else
                return false;
        }
        #endregion
    }
}
