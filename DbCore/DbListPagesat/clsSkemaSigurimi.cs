using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje skeme sigurimi punonjesi
    ///  (Te dhenat  merren nga tabela : T_SKEMASIGURIMI)
    /// </summary>
    public class clsSkemaSigurimi
    {
        private const string gabimNeTeDhena = "ERROR: Gabim konvtertimi gjate marrjes se skemes se sigurimit nga db-ja";
        #region Atribute

        private int idSkemaSig;
        private int idPunonjes;
        private int idSigurimi;
        private DateTime dtAktivizimi;
        private int idPerdoruesi;
        private DataRow rreshti;
        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idskemasig">Id e skemes se sigurimit</param>
        /// <param name="idpunonjes">Id e punonjesit</param>
        /// <param name="idsigurimi">Id e sigurimit</param>
        /// <param name="dtaktivizimi">data e aktivizimit</param>
        public clsSkemaSigurimi(int idskemasig, int idpunonjes, int idsigurimi, DateTime dtaktivizimi, int idperdoruesi)
        {
            idSkemaSig = idskemasig;
            idPunonjes = idpunonjes;
            idSigurimi = idsigurimi;
            dtAktivizimi = dtaktivizimi;
            idPerdoruesi = idperdoruesi;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaSigurimi()
        {
        }

        /// <summary>
        /// konstruktori me dy parametra
        /// </summary>
        /// <param name="idpunonjes">id punonjes</param>
        /// <param name="data">data</param>
        public clsSkemaSigurimi(int idpunonjes, DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushSkemaSigurimi(db.ktheSkemeSigurimiSipasPunonjesitDheDates(idpunonjes, data));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// </summary>
        /// <param name="idskemasig">id skemasig</param>
        /// <param name="data">data</param>
        public clsSkemaSigurimi(int idskemasig)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushSkemaSigurimi(db.ktheSkeme(idskemasig));
            db.Dispose();
        }
        public clsSkemaSigurimi(int idskemasig, clsDatabazeListPagesa db)
        {
            mbushSkemaSigurimi(db.ktheSkeme(idskemasig));

        }

        public clsSkemaSigurimi(DataRow rreshti)
        {
            
            mbushSkemaSigurimi(rreshti);
        }
        #endregion

        #region Properties

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
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdSkemaSig
        {
            get { return idSkemaSig; }
            set { idSkemaSig = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e punonjesit
        /// </summary>
        public int IdPunonjes
        {
            get { return idPunonjes; }
            set { idPunonjes = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e sigurimit
        /// </summary>
        public int IdSigurimi
        {
            get
            {
                return idSigurimi;
            }
            set
            {
                idSigurimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos daten e aktivizimit.
        /// </summary>
        public DateTime DtAktivizimi
        {
            get { return dtAktivizimi; }
            set { dtAktivizimi = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e adreses ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.ruajSkeme"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj(clsDatabazeListPagesa data)
        {
            int id;
            clsMesazh u_ruajt = data.ruajSkeme(out id, idPunonjes, idSigurimi, dtAktivizimi, idPerdoruesi);
            this.idSkemaSig = id;
            return u_ruajt;
        }
        public clsMesazh ruaj()
        {
            using (clsDatabazeListPagesa data = new DbListPagesat.clsDatabazeListPagesa())
                return ruaj(data);
        }

        /// <summary>
        /// Modifikon objektin e adreses ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.modifikoSkeme"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko(clsDatabazeListPagesa data)
        {
            clsMesazh u_modifikua = data.modifikoSkeme(idSkemaSig, idPunonjes, idSigurimi, dtAktivizimi, idPerdoruesi);
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e adreses ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.fshiSkeme"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public bool fshi()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_fshi = data.fshiSkeme(idSkemaSig);
            data.Dispose();
            return u_fshi.Status;
        }
        internal static string kontrolloskema(clsSkemaSigurimi skema, clsSkemaSigurimi skemaVjeter, out string fushatmod, clsDatabazeListPagesa db)
        {
            string mesazh = "U modifikuan fushat per skemat:";
            fushatmod = "U modifikuan fushat per skemat:";
            if (skema.IdSigurimi != skemaVjeter.IdSigurimi)
            {
                mesazh += " Skema e sigurimit,";
                clsSigurimet skvj = new clsSigurimet(skemaVjeter.IdSigurimi, db);
                clsSigurimet skri = new clsSigurimet(skema.IdSigurimi, db);
                fushatmod += String.Format(" Skema nga {0} ne {1},", skvj.Kodi, skri.Kodi);
            }

            if (skema.DtAktivizimi != skemaVjeter.DtAktivizimi)
            {
                mesazh += " Data e aktivizimit,";
                fushatmod += String.Format(" Data e aktivizimit nga {0} ne {1},", skemaVjeter.DtAktivizimi.ToShortDateString(), skema.DtAktivizimi.ToShortDateString());
            }
            if (mesazh.Substring(mesazh.Length - 1, 1) == ",")
            {
                mesazh = mesazh.Substring(0, mesazh.Length - 1) + ".";
                fushatmod = fushatmod.Substring(0, fushatmod.Length - 1) + ".";
            }
            return mesazh;
        }
        /// <summary>
        /// merr skemen sipas id
        /// </summary>
        public void merr()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            mbushSkemaSigurimi(data.ktheSkeme(idSkemaSig));
            data.Dispose();
        }

        /// <summary>
        /// merr skemen sipas idpunonjes dhe data deri
        /// </summary>
        public void merrSipasIdPunonjesDheDataDeri(int idpunonjes, DateTime date)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            mbushSkemaSigurimi(data.ktheSkemeSigurimiSipasPunonjesitDheDatesDeri(idpunonjes, date));
            data.Dispose();
        }
        #endregion

        #region Metoda Internal

        [Obsolete("perdor ate me idatarecord")]
        /// <summary>
        /// mbush skema sigurimi nga databaza
        /// </summary>
        /// <param name="dbDataRowSkemaSigurimi">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>

        internal bool mbushSkemaSigurimi(DataRow dbDataRowSkemaSigurimi)
        {
            if (dbDataRowSkemaSigurimi != null)
            {
                try
                {
                    int.TryParse(dbDataRowSkemaSigurimi["IDSKEMASIG"].ToString(), out idSkemaSig);
                    int.TryParse(dbDataRowSkemaSigurimi["IDPUNONJES"].ToString(), out idPunonjes);
                    int.TryParse(dbDataRowSkemaSigurimi["IDSIGURIMI"].ToString(), out idSigurimi);
                    DateTime.TryParse(dbDataRowSkemaSigurimi["DTAKTIVIZIMI"].ToString(), out dtAktivizimi);
                    int.TryParse(dbDataRowSkemaSigurimi["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(gabimNeTeDhena);
                }
            }
            else
                return false;
        }

        public static clsSkemaSigurimi Krijo(IDataRecord dbDataRowSkemaSigurimi)
        {
            var skema = new clsSkemaSigurimi();
            skema.MbushSkemaSigurimi(dbDataRowSkemaSigurimi);
            return skema;
        }

        public void MbushSkemaSigurimi(IDataRecord dbDataRowSkemaSigurimi)
        {
            int.TryParse(dbDataRowSkemaSigurimi["IDSKEMASIG"].ToString(), out idSkemaSig);
            int.TryParse(dbDataRowSkemaSigurimi["IDPUNONJES"].ToString(), out idPunonjes);
            int.TryParse(dbDataRowSkemaSigurimi["IDSIGURIMI"].ToString(), out idSigurimi);
            DateTime.TryParse(dbDataRowSkemaSigurimi["DTAKTIVIZIMI"].ToString(), out dtAktivizimi);
            int.TryParse(dbDataRowSkemaSigurimi["IDPERDORUESI"].ToString(), out idPerdoruesi);
        }


        #endregion
    }
}
