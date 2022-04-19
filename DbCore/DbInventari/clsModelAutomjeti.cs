using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne modelet e automjeteve
    ///  (Te dhenat  merren nga tabela : T_MODELAUTOMJETI)
    /// </summary>
    public class clsModelAutomjeti
    {
        #region Atribute

        private int idModelAutomjeti;
        private string kodModelAutomjeti;
        private string pershkrimModelAutomjeti;
        private int idStatusDok;
        private int idNdermarrje;
        private int idKrijuesi;
        private int idPerdorues;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e modelit te automjetit.
        /// </summary>
        public int IdModelAutomjeti
        {
            get { return idModelAutomjeti; }
            set { idModelAutomjeti = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e modelit.
        /// </summary>
        public string KodModelAutomjeti
        {
            get { return kodModelAutomjeti; }
            set { kodModelAutomjeti = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e modelit.
        /// </summary>
        public string PershkrimModelAutomjeti
        {
            get { return pershkrimModelAutomjeti; }
            set { pershkrimModelAutomjeti = value; }
        }

        /// <summary>
        /// Kthen/Vendos idStatusDok
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e krijuesit.
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdorues
        {
            get { return idPerdorues; }
            set { idPerdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e krijimit.
        /// </summary>
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
            set
            {
                dtKrijimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit.
        /// </summary>
        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }
            set
            {
                dtModifikimi = value;
            }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsModelAutomjeti()
        {
        }

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        public clsModelAutomjeti(int idModeli, string kodModeli, string pershkrimModeli, int idStatusDok, int idNdermarrje, int idKrijuesi, int idPerdorues)
        {
            this.idModelAutomjeti = idModeli;
            this.kodModelAutomjeti = kodModeli;
            this.pershkrimModelAutomjeti = pershkrimModeli;
            this.idStatusDok = idStatusDok;
            this.idNdermarrje = idNdermarrje;
            this.idKrijuesi = idKrijuesi;
            this.idPerdorues = idPerdorues;
        }

        /// <summary>
        /// Konstruktori i klases me id
        /// </summary>
        public clsModelAutomjeti(int idModeli)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            if (!mbushModelAutomjeti(dbInventari.merrModelAutomjetiSipasId(idModeli)))
                idModelAutomjeti = -1;
            dbInventari.Dispose();
        }

        public clsModelAutomjeti(DataRow rreshti)
        {
            
            mbushModelAutomjeti(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// mbush automjetin sipas id-se 
        /// </summary>
        /// <param name="idAutomjet">id e automjetit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushModelAutomjeti(int idModeli)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushModelAutomjeti(dbInventari.merrModelAutomjetiSipasId(idModeli));
            dbInventari.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush automjetin sipas id-se se ndermarrjes dhe nr te shasise
        /// </summary>
        /// <param name="idNdermarrje">id e automjetit</param>
        ///<param name="nrShasie">nr i shasise</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushModelAutomjeti(int idNdermarrje, string kodi)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushModelAutomjeti(dbInventari.merrModelAutomjetiSipasNdermarrjeDheKodit(idNdermarrje, kodi));
            dbInventari.Dispose();
            return sukses;
        }

        /// <summary>
        /// kthe nje datarow me automjetin e marre nga db sipas id.
        /// </summary>
        /// <param name="idAutomjet"></param>
        /// <returns>datarow</returns>
        public static DataRow ktheModelAutomjetiSipasId(int idModelAutomjet)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataRow dr = dbInventari.merrModelAutomjetiSipasId(idModelAutomjet);
            return dr;
        }

        /// <summary>
        /// kthe nje datarow me automjetin e marre nga db sipas id.
        /// </summary>
        /// <param name="idAutomjet"></param>
        /// <returns>datarow</returns>
        public static DataTable ktheModelAutomjetiSipasIdDt(int idModelAutomjet)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable dt = dbInventari.merrModelAutomjetiSipasIdDt(idModelAutomjet);
            return dt;
        }

        public static bool ekzistonModelAutomjeti(int idNdermarrje, string kodi)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            bool ekziston = dbInventar.ekzistonModelAutomjeti(kodi, idNdermarrje);
            return ekziston;
        }

        public static bool eshteLidhurModelAutomjeti(int idModeli)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            bool ekziston = dbInventar.eshteLidhurModelAutomjeti(idModeli);
            return ekziston;
        }

        public clsMesazh ruaj()
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            clsMesazh mesazh = new clsMesazh();
            try
            {
                dbInventar.beginTransaksion();
                int idModeli = 0;
                mesazh = dbInventar.ruajModelAutomjeti(out idModeli, this.kodModelAutomjeti, this.pershkrimModelAutomjeti, this.idStatusDok, this.idNdermarrje, this.idKrijuesi, this.idPerdorues);
                this.idModelAutomjeti = idModeli;
                if (!mesazh.Status)
                {
                    dbInventar.rollbackTransaksion();
                    return mesazh;
                }
                dbInventar.commitTransaksion();
                return mesazh;
            }
            catch
            {
                dbInventar.rollbackTransaksion();
                return mesazh;
            }
        }

        public clsMesazh modifiko()
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            clsMesazh mesazh = new clsMesazh();
            try
            {
                dbInventar.beginTransaksion();
                mesazh = dbInventar.modifikoModelAutomjet(this.idModelAutomjeti, this.kodModelAutomjeti, this.pershkrimModelAutomjeti, this.idStatusDok, this.idNdermarrje, this.idPerdorues);
                if (!mesazh.Status)
                {
                    dbInventar.rollbackTransaksion();
                    return mesazh;
                }
                dbInventar.commitTransaksion();
                return mesazh;
            }
            catch
            {
                dbInventar.rollbackTransaksion();
                return mesazh;
            }
        }

        public clsMesazh fshi(int idPerdoruesi)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            clsMesazh mesazh = new clsMesazh();
            try
            {
                dbInventar.beginTransaksion();
                mesazh = dbInventar.fshiModelAutomjetiMeStatusDok(this.idModelAutomjeti, idPerdoruesi);
                if (!mesazh.Status)
                {
                    dbInventar.rollbackTransaksion();
                    return mesazh;
                }
                dbInventar.commitTransaksion();
                return mesazh;
            }
            catch
            {
                dbInventar.rollbackTransaksion();
                return mesazh;
            }
        }


        #endregion

        #region Metoda Internal

        /// <summary>
        /// Metode per mbushjen e modelit te automjetit nga databaza. Thirret nga metoda mbushModeleAutomjetesh e colModeleAutomjetesh.cs
        /// </summary>
        /// <param name="dbDataRowModelAutomjeti">Si parameter merr nje DataRow.</param>
        /// <returns>Kthen true nese mbushja ndodh me sukses. Ne te kundert false.</returns>
        /// <param name="dbinventari"></param>
        internal bool mbushModelAutomjeti(DataRow dbDataRowModelAutomjeti)
        {
            if (dbDataRowModelAutomjeti == null)
                return false;
            try
            {
                int.TryParse(dbDataRowModelAutomjeti["IDMODELI"].ToString(), out idModelAutomjeti);
                kodModelAutomjeti = dbDataRowModelAutomjeti["KODMODELI"].ToString();
                pershkrimModelAutomjeti = dbDataRowModelAutomjeti["PERSHKRIMMODELI"].ToString();
                int.TryParse(dbDataRowModelAutomjeti["IDSTATUSDOK"].ToString(), out idStatusDok);
                int.TryParse(dbDataRowModelAutomjeti["IDNDERMARRJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowModelAutomjeti["IDKRIJUESI"].ToString(), out idKrijuesi);
                int.TryParse(dbDataRowModelAutomjeti["IDPERDORUESI"].ToString(), out idPerdorues);
                DateTime.TryParse(dbDataRowModelAutomjeti["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowModelAutomjeti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate cast-it!");
            }
            catch (Exception)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se modelit te automjetit nga db-ja!");
            }
        }

        #endregion
    }
}