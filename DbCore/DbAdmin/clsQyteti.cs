using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje qytet.
    ///  (Te dhenat  merren nga tabela : T_QYTETI)
    /// </summary>
    public class clsQyteti
    {
        #region Atributet

        private int idQyteti;
        private String kodiQyteti;
        private String emriQyteti;

        private int idNdermarja;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsQyteti(int idqyteti, String kodiqyteti, String emriqyteti,
                             int idndermarja, int idperdoruesi, int idstatusdok)
        {
            idQyteti = idqyteti;
            kodiQyteti = kodiqyteti;
            emriQyteti = emriqyteti;
            idNdermarja = idndermarja;
            idPerdoruesi = idperdoruesi;
            idStatusDok = idstatusdok;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsQyteti(String kodiqyteti, String emriqyteti,
                             int idndermarja, int idperdoruesi, int idstatusdok, bool shtim)
        {
            kodiQyteti = kodiqyteti;
            emriQyteti = emriqyteti;
            idNdermarja = idndermarja;
            idPerdoruesi = idperdoruesi;
            idStatusDok = idstatusdok;
            clsMesazh mesazhi = kontrolloQytet(shtim);
            if (!mesazhi.Status)
                throw new DbCore.MyException(mesazhi.PershkrimMesazhi);
        }

        protected clsMesazh kontrolloQytet(bool shtim)
        {
            if (String.IsNullOrEmpty(this.kodiQyteti))
                return new clsMesazh(false, "Plotesoni kodin e qytetit!");
            if (String.IsNullOrEmpty(this.emriQyteti))
                return new clsMesazh(false, "Plotesoni emrin e qytetit!");
            if (ekzistonQytetiSipasEmerDheStatusShtimi(this.emriQyteti, this.idNdermarja, shtim, this.idQyteti))
                return new clsMesazh(false, String.Format("Ekziston nje qytet me emrin {0}!", this.emriQyteti));
            if (ekzistonQytetiSipasKoditDheStatusShtimi(this.kodiQyteti, this.idNdermarja, shtim, this.idQyteti))
                return new clsMesazh(false, String.Format("Ekziston nje qytet me kodin {0}!", this.kodiQyteti));
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        /// <summary>
        /// konstuktor me 1 parameter
        /// </summary>
        /// <param name="id">id e qytetit</param>
        public clsQyteti(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();            
            mbushQyteti(data.merrQytet(id));
            data.Dispose();
        }

        public clsQyteti(int id,clsDatabaseAdmin data)
        {                       
            mbushQyteti(data.merrQytet(id));
        }

        /// <summary>
        /// konstuktor me 2 parameter
        /// </summary>
        ///<param name="idndermarje"> idndermarje</param>
        ///<param name="pershkrimi"> pershkrimi</param>
        public clsQyteti(string pershkrimi, int idndermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushQyteti(data.merrQytetSipasPershkrimit(pershkrimi,idndermarje));
            data.Dispose();
        }
        /// <summary>
        /// Konstruktori default i klases.
        /// </summary>
        public clsQyteti()
        {
        }

       public clsQyteti(DataRow rreshti)
        {
            
            mbushQyteti(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdQyteti
        {
            get { return idQyteti; }
            set { idQyteti = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodin e qytetit.
        /// </summary>
        public String KodiQyteti
        {
            get { return kodiQyteti; }
            set { kodiQyteti = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e qytetit.
        /// </summary>
        public String EmriQyteti
        {
            get { return emriQyteti; }
            set { emriQyteti = value; }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes se ciles i perket ky qytet. 
        /// </summary>
        public int IdNdermarja
        {
            get { return idNdermarja; }
            set { idNdermarja = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe e krijoi kete qyetet.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
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

        //public override string ToString()
        //{
        //    return "Qyteti: " + this.emriQyteti + " ka ID: " + this.idQyteti; ;
        //}

        /// <summary>
        /// Ruan objektin e qytetit ne tabelen perkatese ne databaze.Theret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ruajQytet"/> 
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            colQytetet qytetdefault = new colQytetet();
            qytetdefault.mbushGjitheQytetetPozitive(-1);
            //colQytetet qytetdefault = data.merrGjitheQytetetPozitive(-1);
            int idQ;
            clsMesazh u_ruajt = data.ruajQytet(out idQ, this.KodiQyteti, this.EmriQyteti, this.IdNdermarja, this.IdPerdoruesi, this.idStatusDok);
            if (!u_ruajt.Status)
                return u_ruajt;
            //bool ekziston = false;
            //foreach (clsQyteti q in qytetdefault)
            //{
            //    if (q.KodiQyteti == this.KodiQyteti)
            //        ekziston = true;
            //}
            //if (!ekziston)
            //{
            //    this.IdNdermarja = -1;
            //    u_ruajt = data.ruajQytet(out idQ, this.KodiQyteti, this.EmriQyteti, this.IdNdermarja, this.IdPerdoruesi, this.idStatusDok);
            //}
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e qytetit ne tabelen perkatese ne databaze.Theret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.modifikoQytet"/> 
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoQytet(this.IdQyteti, this.KodiQyteti, this.EmriQyteti, this.IdNdermarja, this.IdPerdoruesi, this.idStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin nje qytet nga databaza duke u bazuar tek Id-ja e qytetit. Theret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.fshiQytet"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiQytetStatus(this.IdQyteti, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Nuk perdoret.
        /// </summary>
        public void merr()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            data.merrQytetPakthim(this.IdQyteti);
            data.Dispose();
        }

        /// <summary>
        /// Kthen nje collection me nje objekte te tipit <see cref="clsQyteti"/>. Ky objekt eshte
        /// ose objekti i gjetur duke perdorur ID-ne e qytetit , ose nje objekt bosh nese nuk eshte
        /// gjetur asnje rresht ne databaze qe i korespondon kesaj ID-je.Theret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.merrQytet"/> 
        /// </summary>
        public clsQyteti kthe()
        {
            clsQyteti data = new clsQyteti(IdQyteti);
            return data;
        }

        //public colQytetet merriTeGjithe()
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    return data.merrGjitheQytetet();

        //}

        /// <summary>
        /// Kjo metode kthen true nqs qyteti eshte i lidhur, pra ka nje ndermarrje apo klient/furnitor me kete qytet
        /// dhe false ne rast te kundert.
        /// </summary>
        /// <returns></returns>
        public bool eshteQytetILidhur()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool eshteLidhur = data.eshteQytetILidhur(this.idQyteti);
            data.Dispose();
            return eshteLidhur;
        }

        public static bool ekzistonQyteti(int idQyteti)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool ekziston = data.ekzistonQyteti(idQyteti);
            data.Dispose();
            return ekziston;
        }

        public static bool ekzistonQytetiSipasEmer(string qyteti, int idndermarje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.ekzistonQytetMeKeteEmer(qyteti, idndermarje);
        }

        public static int ktheIdQytetiSipasEmritDheNdermarrjes(string emerQyteti, int idNdermarrje)
        {
            using (clsDatabaseAdmin sharedb = new clsDatabaseAdmin())
                return sharedb.ktheIdQytetiSipasEmritDheNdermarrjes(emerQyteti, idNdermarrje);
        }
        public static bool ekzistonQytetiSipasEmerDheStatusShtimi(string emriQyteti, int idndermarje, bool shtim, int idQyteti)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.EkzistonQytetMeKetePershkrim(emriQyteti, idndermarje, shtim, idQyteti);
        }


        public static bool ekzistonQytetiSipasKoditDheStatusShtimi(string kodiQyteti, int idndermarje, bool shtim, int idQyteti)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.EkzistonQytetMeKeteKod(kodiQyteti, idndermarje, shtim, idQyteti);
        }
        

        public static int ktheIdQytetiSipasEmerPerNdermMeme(string qyteti)
        {
            int idNderm = clsNdermarrje.ktheIdNdermarrjeMeme();
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.ktheIdQytetMeKeteEmer(qyteti, idNderm);
        }
        #endregion

        #region Metoda Internal

        internal bool mbushQyteti(DataRow dbDataRowQyteti)
        {
            if (dbDataRowQyteti != null)
            {
                try
                {
                    int.TryParse(dbDataRowQyteti["IDQYTETI"].ToString(), out idQyteti);
                    kodiQyteti = dbDataRowQyteti["QYTETIKODI"].ToString();
                    emriQyteti = dbDataRowQyteti["QYTETIEMRI"].ToString();
                    int.TryParse(dbDataRowQyteti["IDNDERMARJE"].ToString(), out idNdermarja);
                    int.TryParse(dbDataRowQyteti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowQyteti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowQyteti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowQyteti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se qytetit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
