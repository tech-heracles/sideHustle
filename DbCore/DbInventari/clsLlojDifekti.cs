using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne llojet e difektit
    ///  (Te dhenat  merren nga tabela : T_LLOJDIFEKTI)
    /// </summary>
   public class clsLlojDifekti
    {
        #region Atribute

        private int id;
        private string kodi;
        private string  pershkrimi;
        private int idPerdoruesi;
        private int idKrijuesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;
        #endregion 

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kod"></param>
        /// <param name="pershkrim"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idkrijuesi"></param>
        /// <param name="idnderm"></param>
        /// <param name="idstatusdok"></param>
        public clsLlojDifekti(int id, string kod, String pershkrim, int idPerdoruesi,int idkrijuesi, int idnderm, int idstatusdok)
        {
            this.id = id;
            this.kodi = kod;
            this.pershkrimi = pershkrim;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idkrijuesi;
            this.idNdermarje = idnderm;
            this.idStatusDok = idstatusdok;
        }
       /// <summary>
       /// konstruktori pa id
       /// </summary>
       /// <param name="kod"></param>
       /// <param name="pershkrim"></param>
       /// <param name="idPerdoruesi"></param>
       /// <param name="idkrijuesi"></param>
       /// <param name="idnderm"></param>
       /// <param name="idstatusdok"></param>
        public clsLlojDifekti( string kod, String pershkrim, int idPerdoruesi,int idkrijuesi, int idnderm, int idstatusdok)
        {
            this.kodi = kod;
            this.pershkrimi = pershkrim;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idkrijuesi;
            this.idNdermarje = idnderm;
            this.idStatusDok = idstatusdok;
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id </param>
        public clsLlojDifekti(int id)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            mbushLlojDifekti(db.merrLlojDifekti(id));
            db.Dispose();
        }  
        public clsLlojDifekti(int id,clsDatabaseInventari db )
        {

            mbushLlojDifekti(db.merrLlojDifekti(id));
           
        }        
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsLlojDifekti()
        { 
        }

        public clsLlojDifekti(DataRow rreshti)
        {
            
            mbushLlojDifekti(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id  ; }
            set { id  = value; }
        }
        /// <summary>
        /// Kthen/Vendos kod .
        /// </summary>
        public String Kodi
        {
            get { return kodi ; }
            set { kodi  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi ; }
            set { idPerdoruesi  = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin .
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi ; }
            set { pershkrimi  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e krijuesit.
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
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
   
        #endregion

        #region Metoda Publike
        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="pershkrimi">pershkrim </param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public bool mbushLlojDifektiMePershk(string pershkrimi, int idnderm)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = mbushLlojDifekti(db.merrLlojDifektiMePershk(pershkrimi, idnderm));
            db.Dispose();
            return sukses;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kod"></param>
        /// <param name="idnderm"></param>
        /// <returns></returns>
        public bool mbushLlojDifektiMeKod(string kod, int idnderm)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = mbushLlojDifekti(db.merrLlojDifektiMeKod(kod, idnderm));
            db.Dispose();
            return sukses;
        }
        public bool mbushLlojDifektiMeKod(string kod, int idnderm, clsDatabaseInventari db)
        {

            bool sukses = mbushLlojDifekti(db.merrLlojDifektiMeKod(kod, idnderm));
          
            return sukses;
        }
        public static bool ekzistonLlojDifekti(string kod, int idnderm) 
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = db.ekzistonLlojDifekti(kod, idnderm);
            db.Dispose();
            return sukses;
        } 
       public static bool ekzistonLlojDifekti(string kod, int idnderm,clsDatabaseInventari db ) 
        {
           
            bool sukses = db.ekzistonLlojDifekti(kod, idnderm);
      
            return sukses;
        }


        public clsMesazh kontrollotransferim(clsLlojDifekti kod, int idndermarje, clsDatabaseInventari db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonLlojDifekti(kod.Kodi, idndermarje))
            {
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;
            }
            else
            {
                clsLlojDifekti kodnderm = new clsLlojDifekti();
                kodnderm.mbushLlojDifektiMeKod(kod.kodi, idndermarje, db);
                if (kodnderm.dtModifikimi < kod.dtModifikimi)
                {
                    kod.idPerdoruesi = idperdoruesi;
                    kod.idNdermarje = idndermarje;
                    kod.id = kodnderm.id;
                    mesazh = kod.modifiko(db);
                    if (!mesazh.Status)
                        return mesazh;
                }

            }
            return mesazh;
        }
        public clsMesazh transfero(object[] idkodifikimi, List<object> idndermarje, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh();
            bool sukses = true;
            for (int i = 0; i < idkodifikimi.Length; i++)
            {
                clsLlojDifekti kod = new clsLlojDifekti(int.Parse(idkodifikimi[i].ToString()));
                foreach (object id in idndermarje)
                {
                    clsDatabaseInventari db = new clsDatabaseInventari();
                    db.beginTransaksion();
                    mesazh = kontrollotransferim(kod, int.Parse(id.ToString()), db, idperdoruesi);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        sukses = false;
                    }
                    db.commitTransaksion();
                }
            }
            if (sukses) { mesazh.PershkrimMesazhi = "Transferimi mbaroi me sukses!"; mesazh.Status = true; }
            else { mesazh.PershkrimMesazhi = "Disa nga llojet e difekteve nuk u transferuan!"; mesazh.Status = false; }
            return mesazh;
        }

   
       /// <summary>
       /// ruan llojin e difektit
       /// </summary>
       /// <returns></returns>
        public clsMesazh ruaj()
        {
            using (clsDatabaseInventari data = new clsDatabaseInventari())
            {
                return ruaj(data);
            }
        } 
        public clsMesazh ruaj( clsDatabaseInventari data )
        {
          
            int id;
            clsMesazh u_ruajt = data.ruajLlojDifekti(out id, this.Kodi, this.Pershkrimi, this.IdPerdoruesi,this.IdKrijuesi, this.IdNdermarje, this.idStatusDok);
            this.id = id;
            return u_ruajt;
        }
       /// <summary>
       /// modifikon lloj difekti
       /// </summary>
       /// <returns></returns>
       public clsMesazh modifiko()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_modifikua = modifiko(data); 
            data.Dispose();
           return u_modifikua;
        }
        public clsMesazh modifiko(clsDatabaseInventari data)
        {
            clsMesazh u_modifikua = data.modifikoLlojDifekti(this.Id, this.Kodi, this.Pershkrimi, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok);
           
            return u_modifikua;
        }
       /// <summary>
       /// fshin lloj difekti duke i ndryshuar statusin
       /// </summary>
       /// <returns></returns>
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiLlojDifektiStatus(this.Id, this.idPerdoruesi);
            data.Dispose();
           
            return u_fshi;
        }

        public static bool eshteTransferuarTekBij(string kodi, int idndermarje)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool ekziston = db.eshteTransferuarTekBijLlojDifekti(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }
        #endregion

        #region Metoda Internal

       /// <summary>
       /// mbush llojin e difektit nga databaza
       /// </summary>
       /// <param name="db"></param>
       /// <returns></returns>
        internal bool mbushLlojDifekti(DataRow db)
        {

            if (db != null)
            {

                try
                {

                    int.TryParse(db["ID"].ToString(), out id);
                    kodi = db["KODI"].ToString();
                    pershkrimi = db["PERSHKRIMI"].ToString();
                    int.TryParse(db["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(db["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(db["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(db["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(db["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(db["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                  
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se lloj difekti nga db-ja");
                }
            }
            else
                return false;

        }

        #endregion
    }
}
