using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqsojne lidhjen e nje ambjenti (Clejeje apo Resgjistrimi) me
    /// nje konfigurim te caktuar te nje numri automatik. (Te dhenat merren nga tabela: T_ACRNUMRAAUTOMATIKE)
    /// </summary>
    public class clsACRNumraAutomatike
    {
        #region Atribute

        private int idLidhjeNrAuto;
        private int idLidhjeCR;
        private int idLlojiLidhje;
        private int idNumraAutoLidhje;
        private string vleraFunditLidhje;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsACRNumraAutomatike(int idLidhjeNrAuto, int idLidhjeCR, int idLlojiLidhje, int idNumraAutoLidhje, string vleraFunditLidhje, int idperdoruesi, int idndermarje, int idstatusdok)
        {
            this.idLidhjeNrAuto = idLidhjeNrAuto;
            this.idLidhjeCR = idLidhjeCR;
            this.idLlojiLidhje = idLlojiLidhje;
            this.idNumraAutoLidhje = idNumraAutoLidhje;
            this.vleraFunditLidhje = vleraFunditLidhje;
            this.idPerdoruesi = idperdoruesi;
            this.idNdermarje = idndermarje;
            this.idStatusDok = idstatusdok;
           
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idcr">id cr</param>
        /// <param name="idlloj">id e llojit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public clsACRNumraAutomatike(int idcr, int idlloj, int idndermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushACRNumraAutomatike(data.ktheNrAutomatikPerKodin(idcr, idlloj, idndermarje));
            data.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e cr</param>
        public clsACRNumraAutomatike(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushACRNumraAutomatike(data.merrACRNumraAutomatike(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsACRNumraAutomatike()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLidhjeNrAuto
        {
            get
            {
                return idLidhjeNrAuto;
            }
            set
            {
                idLidhjeNrAuto = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e ambjentit te Celje/Regjistrim-it.
        /// </summary>
        public int IdLidhjeCR
        {
            get
            {
                return idLidhjeCR;
            }
            set
            {
                idLidhjeCR = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne qe perfaqson llojin e lidhjes.(psh: Kod,NrDokumenti etj..)
        /// </summary>
        public int IdLlojiLidhje
        {
            get
            {
                return idLlojiLidhje;
            }
            set
            {
                idLlojiLidhje = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e numrit automatik.
        /// </summary>
        public int IdNumraAutoLidhje
        {
            get
            {
                return idNumraAutoLidhje;
            }
            set
            {
                idNumraAutoLidhje = value;
            }
        }

        /// <summary>
        /// Kthen vleren e fundit qe eshte gjeneruar nga kjo lidhje. Perdoret per te gjeneruar
        /// numrin pasardhes.
        /// </summary>
        public string VleraFunditLidhje
        {
            get
            {
                return vleraFunditLidhje;
            }
            set
            {
                vleraFunditLidhje = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e e perdoruesit
        /// </summary>
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
        /// Kthen ID-ne e ndermarrjes.
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
        /// Kthen ID-ne e statusit te dokumentit 0-draft, 1-ruajtur, 2-fshire,3- stornim.
        /// </summary>
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
 
        /// <summary>
        /// Kthen Dt e krijimit e cila ruhet vete ne db sa here shtohet nje rresht.
        /// </summary>
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
           
        }   
        /// <summary>
        /// Kthen dt e modifikimit e cila ruhet ne sp e modifikimit
        /// </summary>
        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }
           
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {//metoda qe therret klasen clsDatabaseAdmin per ruajtjen e nje ambjenti celje regjistrim per numrat automatike
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajACRNumraAutomatike(this.IdLidhjeNrAuto, this.IdLidhjeCR, this.IdLlojiLidhje, this.IdNumraAutoLidhje, this.VleraFunditLidhje, this.IdPerdoruesi, this.IdNdermarje, this.IdStatusDok);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit ne databaze.
        /// </summary>
        public clsMesazh modifiko()
        {//metoda qe therret klasen clsDatabaseAdmin per modifikimin e nje ambjenti celje regjistrim per numrat automatike
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoACRNumraAutomatike(this.IdLidhjeNrAuto, this.IdLidhjeCR, this.IdLlojiLidhje, this.IdNumraAutoLidhje, this.VleraFunditLidhje, this.IdPerdoruesi, this.IdNdermarje, this.IdStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit ne databaze.
        /// </summary>
        public clsMesazh fshi()
        {//metoda qe therret klasen clsDatabaseAdmin per fshirjen e nje  ambjenti celje regjistrim per numrat automatike
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiACRNumraAutomatike(this.IdLidhjeNrAuto);
            data.Dispose();
            return u_fshi;
        }
         /// <summary>
        /// Fshin objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit ne databaze.
        /// </summary>
        public clsMesazh fshistatus()
        {//metoda qe therret klasen clsDatabaseAdmin per fshirjen e nje  ambjenti celje regjistrim per numrat automatike
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiACRNumraAutomatikeStatus(this.IdLidhjeNrAuto, this.IdPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        ///// <summary>
        ///// Kthen  objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit 
        ///// nga  databaza sipas ID-se.
        ///// </summary>       
        //public void merr()
        //{//metoda qe therret klasen clsDatabaseAdmin per marrjen e nje  ambjenti celje regjistrim per numrat automatike
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    data.merrACRNumraAutomatikeVoid(this.IdLidhjeNrAuto);
        //    data.Dispose();
        //}

        /// <summary>
        /// Kthen gjithe objektet e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit 
        /// nga  databaza.
        /// </summary> 
        public colACRNumratAutomatike merrGjitheACRNumraAutomatike()
        {//metoda qe therret klasen clsDatabaseAdmin per marrjen e te gjithave  ambjenteve celje regjistrim per numrat automatike
            colACRNumratAutomatike data = new colACRNumratAutomatike(this.IdNdermarje);
            return data;

        }

        #endregion

        #region Metoda Internal

        internal bool mbushACRNumraAutomatike(DataRow dbDataRowACRNumraAutomatike)
        {
            if (dbDataRowACRNumraAutomatike != null)
            {
                try
                {
                    int.TryParse(dbDataRowACRNumraAutomatike["IDLIDHJENRAUTO"].ToString(), out idLidhjeNrAuto);
                    int.TryParse(dbDataRowACRNumraAutomatike["IDLIDHJECR"].ToString(), out idLidhjeCR);
                    int.TryParse(dbDataRowACRNumraAutomatike["IDLLOJILIDHJE"].ToString(), out idLlojiLidhje);
                    int.TryParse(dbDataRowACRNumraAutomatike["IDNUMRAAUTOLIDHJE"].ToString(), out idNumraAutoLidhje);
                    vleraFunditLidhje = dbDataRowACRNumraAutomatike["VLERAFUNDITLIDHJE"].ToString();
                    int.TryParse(dbDataRowACRNumraAutomatike["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowACRNumraAutomatike["IDNDERMARJE"].ToString(), out idNdermarje);    
                    int.TryParse(dbDataRowACRNumraAutomatike["IDSTATUSDOK"].ToString(), out idStatusDok);  
                    DateTime.TryParse(dbDataRowACRNumraAutomatike["DTKRIJIMI"].ToString(), out dtKrijimi);      
                    DateTime.TryParse(dbDataRowACRNumraAutomatike["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se numrave ACR automatike nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
