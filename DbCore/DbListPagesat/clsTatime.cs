using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje tatim
    ///  (Te dhenat  merren nga tabela : T_TATIME)
    /// </summary>
    public class clsTatime
    {
        private const string vlerareshtitjeter = "Vlera maksimale nuk mund te jete me e madhe se vlera maksimale e rreshtit tjeter!";
        public static string mbushjeSukses = "Tatimi u mbush me sukses";
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se tatimit nga db-ja";
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";

        #region Atribute

        private int idTatime;
        private decimal min;
        private decimal max;
        private string norma;
        private int menyra;
        private int model;
        private DateTime data;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsTatime()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idTatime">id e tatimeve</param>
        /// <param name="min"> vlera minimale</param>
        /// <param name="max"> vlera maksimale</param>
        /// <param name="norma">norma e perqindjes</param>
        /// <param name="menyra">menyra e llogaritjes 0-progresive 1-totale</param>
        /// <param name="model"> modeli default, te ndermarjes qe nuk fshihen, te krijuara nga perdoruesi</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        public clsTatime(int idTatime, DateTime data, decimal min, decimal max, string norma, int menyra, int model, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok)
        {
            this.idTatime = idTatime;
            this.min = min;
            this.max = max;
            this.norma = norma;
            this.menyra = menyra;
            this.model = model;
            this.data = data;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idKonfig = idKonfig;
            this.idStatusDok = idStatusDok;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idtatime">id e tatimit</param>
        public clsTatime(int idtatime)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushTatime(db.ktheTatim(idtatime));
            db.Dispose();
        }

        public clsTatime(DataRow rreshti)
        {
            
            mbushTatime(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// id i tatimit 
        /// </summary>
        public int IdTatime
        {
            get
            {
                return idTatime;
            }
            set
            {
                idTatime = value;
            }
        }
        /// <summary>
        /// vlera minimale
        /// </summary>
        public decimal Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
            }
        }
        /// <summary>
        /// vlera maksimale
        /// </summary> 
        public decimal Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
            }
        }
        /// <summary>
        /// menyra e llogaritjes
        /// <example> 0 -progresive 1-totale</example>
        /// </summary>
        public int Menyra
        {
            get
            {
                return menyra;
            }
            set
            {
                menyra = value;
            }
        }
        /// <summary>
        /// norma e perqindjes
        /// </summary>
        public string Norma
        {
            get
            {
                return norma;
            }
            set
            {
                norma = value;
            }
        }
        /// <summary>
        /// modeli
        /// <example> 0- modelet e sistemit qe perdoren per ndermarjet e reja 1- modelet e ndermarjes qe nuk fshihen 2- modelet e krijuar nga perdoruesi</example>
        /// </summary>
        public int Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }
        /// <summary>
        /// data e aktivizimit
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
        /// id e perdoruesit qe ka kryer veprimin
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
        /// id e ndermarjes 
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
        /// id e konfigurimit te dokumentit
        /// </summary>
        public int IdKonfig
        {
            get
            {
                return idKonfig;
            }
            set
            {
                idKonfig = value;
            }
        }
        /// <summary>
        /// id e status te dok
        /// <example> 0 draft, 1-ruajtur,2 -fshire</example>
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
        /// data e krijimit te kompoentes
        /// </summary>
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
        }
        /// <summary>
        /// data e modifikimi te fundit te komponentes
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
        /// ruan tatime
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo tatimi</returns>
        public clsMesazh ruaj()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh mesazh = new clsMesazh();
            int idtatim = 0;
            mesazh = db.ruajTatim(out idtatim, data, min, max, norma, menyra, model, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
            idTatime = idtatim;
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// ruan tatimet default 
        /// </summary>
        /// <param name="idndermarje"> ndermarja</param>
        /// <param name="idperdoruesi">perdoruesi</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo tatimet</returns>
        public static clsMesazh ruajDefault(int idndermarje, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mesazh = db.ruajDefaultTatime(idperdoruesi, idndermarje);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// modifikon tatimin
        /// modifikon vleren minimale te tatimit pasardhes dhe tatimin aktual
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo tatimi</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh(true);
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            //db.krijoManager();
            db.beginTransaksion();
            colTatimet col = new colTatimet(idNdermarje, data);
            IEnumerable<clsTatime> tatimtjeter = (from c in col
                                                  where c.Min > min
                                                  orderby c.Min
                                                  select c).Take(1);
            foreach (clsTatime t in tatimtjeter)
                if (t.Max <= Max && t.IdTatime != idTatime)
                {
                    db.rollbackTransaksion();
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = vlerareshtitjeter;
                    return mesazh;
                }
                else
                    mesazh = db.modifikoTatim(t.idTatime, t.data, max + 1, t.max, t.norma, t.menyra, t.model, t.idKonfig, idPerdoruesi, t.idNdermarje, t.idStatusDok);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            mesazh = db.modifikoTatim(idTatime, data, min, max, norma, menyra, model, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
            if (mesazh.Status)
                db.commitTransaksion();
            else db.rollbackTransaksion();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin tatim ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh u_fshi = db.fshiTatimStatus(idTatime, idPerdoruesi);
            db.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin tatimin nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        public void merr()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            db.ktheTatim(idTatime);
            db.Dispose();
        }

        /// <summary>
        /// Merr datatable tatime  te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje datatable me te gjithe tatimet te kesaj ndermarje</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.ktheGjitheTatimeSipasNdermarjes(IdNdermarje);
            db.Dispose();
            return dt;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush tatime me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushTatime(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDTATIME"].ToString(), out idTatime);
                    decimal.TryParse(dbDataRow["MIN"].ToString(), out min);
                    DateTime.TryParse(dbDataRow["DATE"].ToString(), out data);
                    decimal.TryParse(dbDataRow["MAX"].ToString(), out max);
                    norma = dbDataRow["NORMA"].ToString();
                    int.TryParse(dbDataRow["MENYRA"].ToString(), out menyra);
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["MODEL"].ToString(), out model);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsTatime.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsTatime.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsTatime.drbosh);
        }

        /// <summary>
        /// mbush tatimet kur te dhenat vijne nga dt e grides
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns>kthen nese mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushTatimeNgaDT(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IdTatime"].ToString(), out idTatime);
                    decimal.TryParse(dbDataRow["Min"].ToString(), out min);
                    DateTime.TryParse(dbDataRow["Data"].ToString(), out data);
                    decimal.TryParse(dbDataRow["Max"].ToString(), out max);
                    norma = dbDataRow["Norma"].ToString();
                    int.TryParse(dbDataRow["Menyra"].ToString(), out menyra);
                    int.TryParse(dbDataRow["IdPerdoruesi"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["Model"].ToString(), out model);
                    int.TryParse(dbDataRow["IdNdermarje"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IdKonfig"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["IdStatusDok"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DtKrijimi"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DtModifikimi"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsTatime.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsTatime.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsTatime.drbosh);
        }

        #endregion
    }
}
