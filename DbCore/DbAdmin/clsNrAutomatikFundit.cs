using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using AlphaWeb.Core.SharedKernel;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqsojne lidhjen e nje ambjenti (Clejeje apo Resgjistrimi) me
    /// nje konfigurim te caktuar te nje numri automatik. (Te dhenat merren nga tabela: T_ACRNUMRAAUTOMATIKE)
    /// </summary>
    public class clsNrAutomatikFundit
    {
        #region Atribute

        private int idNrFunditAutomatik;
        private int idNrAutom;
        private DateTime data;
        private string vlera;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsNrAutomatikFundit(int idNrFunditAutomatik, int idNrAutom, DateTime data,  string vlera, int idperdoruesi, int idndermarje, int idstatusdok)
        {
            this.idNrFunditAutomatik = idNrFunditAutomatik;
            this.idNrAutom = idNrAutom;
            this.data = data;
            this.vlera = vlera;
            this.idPerdoruesi = idperdoruesi;
            this.idNdermarje = idndermarje;
            this.idStatusDok = idstatusdok;

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e cr</param>
        public clsNrAutomatikFundit(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushNumraAutomatikeFundit(data.merrNumraAutomatikeFunditSipasID(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsNrAutomatikFundit()
        {
        }

        public clsNrAutomatikFundit(DataRow rreshti)
        {
            
            mbushNumraAutomatikeFundit(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdNrFunditAutomatik
        {
            get
            {
                return idNrFunditAutomatik;
            }
            set
            {
                idNrFunditAutomatik = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e numrit automatik se cilit i perket ky objekt
        /// </summary>
        public int IdNrAutom
        {
            get
            {
                return idNrAutom;
            }
            set
            {
                idNrAutom = value;
            }
        }

        /// <summary>
        /// Kthen daten e objektit
        /// </summary>
        public DateTime Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }


        /// <summary>
        /// Kthen vleren e fundit qe eshte gjeneruar per kete numer automatik
        /// </summary>
        public string Vlera
        {
            get
            {
                return vlera;
            }
            set
            {
                vlera = value;
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
        /// Funksioni qe kthen numrin e numrave automatik me kodin e kaluar si parameter duke thirrur funksionin si me poshte
        /// ne klasen clsDatabaseAdmin
        /// </summary>
        /// <param name="kodiNrAutomatik"></param>
        /// <returns></returns>
        public int kaNumraAutoTeFundit(int id)
        {
            clsDatabaseAdmin cls = new clsDatabaseAdmin();
            return cls.kaNumraAutoTeFundit(id);
        }


        /// <summary>
        /// Ruan objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {//metoda qe therret klasen clsDatabaseAdmin per ruajtjen e nje ambjenti celje regjistrim per numrat automatike
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = ruaj(data);
            data.Dispose();
            return u_ruajt;
        }
        /// <summary>
        /// Ruan objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit ne databaze.
        /// </summary>
        /// <param name="data"></param>
        public clsMesazh ruaj(clsDatabaseAdmin data)
        {//metoda qe therret klasen clsDatabaseAdmin per ruajtjen e nje ambjenti celje regjistrim per numrat automatike
            int id = 0;
            clsMesazh u_ruajt = data.ruajNumraAutomatikeFundit(out id, this.IdNrAutom, this.Data, this.Vlera, this.IdPerdoruesi, this.IdNdermarje, this.IdStatusDok);
            this.IdNrFunditAutomatik = id;
            return u_ruajt;
        }
        /// <summary>
        /// Modifikon objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit ne databaze.
        /// </summary>
        public clsMesazh modifiko(clsDatabaseAdmin data)
        {//metoda qe therret klasen clsDatabaseAdmin per modifikimin e nje ambjenti celje regjistrim per numrat automatike
            clsMesazh u_modifikua = data.modifikoNumraAutomatikeFundit(this.IdNrFunditAutomatik, this.IdNrAutom, this.Data, this.Vlera, this.IdPerdoruesi, this.IdNdermarje, this.IdStatusDok);
            return u_modifikua;
        }
        /// <summary>
        /// Modifikon objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit ne databaze.
        /// </summary>
        public clsMesazh modifiko()
        {//metoda qe therret klasen clsDatabaseAdmin per modifikimin e nje ambjenti celje regjistrim per numrat automatike            
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = modifiko(data);
            data.Dispose();
            return u_modifikua;
        }
        ///// <summary>
        ///// Fshin objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit ne databaze.
        ///// </summary>
        //public clsMesazh fshi()
        //{//metoda qe therret klasen clsDatabaseAdmin per fshirjen e nje  ambjenti celje regjistrim per numrat automatike
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    clsMesazh u_fshi = data.fshiNumraAutomatikeFunditSipasID(this.IdNrFunditAutomatik);
        //    data.Dispose();
        //    return u_fshi;
        //}
        /// <summary>
        /// Fshin objektin e lidhjes se Numrit Automatike me Ambjentet e Cleje/Regjistrimit ne databaze.
        /// </summary>
        
        
        public clsMesazh fshistatus()
        {//metoda qe therret klasen clsDatabaseAdmin per fshirjen e nje  ambjenti celje regjistrim per numrat automatike
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiNumraAutomatikeFunditStatus(this.IdNrFunditAutomatik, this.IdPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        public static DataRow merrNrAutoFunditSipasID(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataRow nrAuto = data.merrNumraAutomatikeFunditSipasID(id);
            data.Dispose();
            return nrAuto;
        }

        /// <summary>
        /// Kontrollon nese nje vlere e dhene eshte vlere paraardhese e numrit automatik qe i kalohet si parameter
        /// </summary>
        /// <param name="vlera"></param>
        /// <param name="nrAutom"></param>
        /// <returns></returns>
        public bool eshteNrParaardhes(string vlera, clsNrAutom nrAutom)
        {
            //njehesohet vlera numerike e vleres qe i kalohet si parameter 
            Int64 vleraNr1 = ktheVlerenNumerikeFundit(vlera, nrAutom);

            //njehesohet vlera numerike e numrit automatik te fundit
            Int64 vleraNr2 = ktheVlerenNumerikeFundit(this.Vlera, nrAutom);

            //nese numri automatik eshte rrites dhe vlera eshte me e vogel apo eshte zbrites dhe vlera me e vogel , athere vlera do jete paraardhese dhe do kthehet true
            int k = (int)DrejtimiNumraveAutomatike.Rrites;
            if ((nrAutom.DrejtimiNrAutom == k && vleraNr1 < vleraNr2) || (nrAutom.DrejtimiNrAutom != k && vleraNr1 > vleraNr2))
                return true;
            else
                return false;
                
        }

        public Int64 ktheVlerenNumerikeFundit(string vlera, clsNrAutom nrAutom)
        {
            int karakteremajtas = nrAutom.MajtasNrAutom.Length;
            int karakteredjathtas = vlera.Length - nrAutom.DjathtasNrAutom.Length - karakteremajtas;
            string strVleraNr = vlera.Substring(karakteremajtas, karakteredjathtas);
            Int64 vleraNr = Convert.ToInt64(strVleraNr);
            return vleraNr;
        }

        public static clsNrAutomatikFundit merrNrAutomatikFunditIdDataPerdoruesiNdermarrja(int nrAutom, DateTime data, int idNdermarrje)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsNrAutomatikFundit nrFundit = new clsNrAutomatikFundit();
            if (nrFundit.mbushNumraAutomatikeFundit(dbAdmin.merrNrAutomatikFunditIdDataPerdoruesiNdermarrja(nrAutom, data, idNdermarrje)))
            {
                dbAdmin.Dispose();
                return nrFundit;
            }
            dbAdmin.Dispose();
            return null;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushNumraAutomatikeFundit(DataRow dbDataRowACRNumraAutomatike)
        {
            if (dbDataRowACRNumraAutomatike != null)
            {
                try
                {
                    int.TryParse(dbDataRowACRNumraAutomatike["IDNRAUTOFUNDIT"].ToString(), out idNrFunditAutomatik);
                    int.TryParse(dbDataRowACRNumraAutomatike["IDNRAUTOM"].ToString(), out idNrAutom);
                    DateTime.TryParse(dbDataRowACRNumraAutomatike["DATA"].ToString(), out data);
                    vlera = dbDataRowACRNumraAutomatike["VLERA"].ToString();
                    int.TryParse(dbDataRowACRNumraAutomatike["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowACRNumraAutomatike["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowACRNumraAutomatike["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowACRNumraAutomatike["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowACRNumraAutomatike["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se numrave automatike te fundit nga db-ja");
                }
            }
            else
                return false;
        }
      

        #endregion
    }
}