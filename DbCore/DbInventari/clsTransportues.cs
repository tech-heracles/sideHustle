using System;
using System.Data;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne transportuesit
    ///  (Te dhenat  merren nga tabela : T_TRANSPORTUES)
    /// </summary>
    public class clsTransportues
    {
        #region Atribute

        private int idTransportues;
        private string emertimi;
        private string adresa;
        private string nipt;
        private string tel;
        private int idStatusDok;
        private int idKrijuesi;
        private int idNdermarrje;
        private int idPerdorues;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string targa;
        private DataRow rreshti;
        private bool aktiv;
        private string tipiId;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTransportues
        {
            get { return idTransportues; }
            set { idTransportues = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e shasise.
        /// </summary>
        public string Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos targen.
        /// </summary>
        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }

        /// <summary>
        /// Kthen/Vendos Pershkrimin e Modelit te automjetit.
        /// </summary>
        public string NIPT
        {
            get { return nipt; }
            set { nipt = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e modelit te automjetit.
        /// </summary>
        public string TEL
        {
            get { return tel; }
            set { tel = value; }
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
        /// <summary>
        /// Kthen/Vendos targen.
        /// </summary>
        public string Targa
        {
            get { return targa; }
            set { targa = value; }
        }
        public string TipiId
        {
            get { return tipiId; }
            set { tipiId = value; }
        }

        public bool Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTransportues()
        {
        }

        /// <summary>
        /// konstrukotr me 1 parameter
        /// </summary>
        /// <param name="id">id e transportuesit</param>
        public clsTransportues(int idTransportues)
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            mbushTransportues(data.merrTransportuesSipasId(idTransportues));
            data.Dispose();
        }

        /// <summary>
        /// konstrukotr me 2 parameter
        /// </summary>
        /// <param name="id">id e transportuesit</param>
        public clsTransportues(string emertimi, int idNdermarje)
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            mbushTransportues(data.merrTransportuesSipasEmertimit(emertimi, idNdermarje));
            data.Dispose();
        }

        public clsTransportues(int idTransportues, string emertimi, string nipt, string adresa, string tel, int idStatusDok, int idKrijues, int idNdermarrje, int idPerdorues, DateTime dtKrijimi, DateTime dtModifikimi, bool shtim, string targa, bool aktiv, string tipiId)
        {
            try
            {
                this.idTransportues = idTransportues;
                this.emertimi = emertimi;
                this.adresa = adresa;
                this.nipt = nipt;
                this.tel = tel;
                this.idStatusDok = idStatusDok;
                this.idKrijuesi = idKrijues;
                this.idNdermarrje = idNdermarrje;
                this.idPerdorues = idPerdorues;
                this.dtKrijimi = dtKrijimi;
                this.dtModifikimi = dtModifikimi;
                this.targa = targa;
                this.aktiv = aktiv;
                this.tipiId = tipiId;
                clsMesazh mesazh = this.kontrolloTransportues(shtim);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public clsTransportues(DataRow rreshti)
        {

            mbushTransportues(rreshti);
        }

        #endregion

        #region Metoda Publike

        private clsMesazh kontrolloTransportues(bool shtim)
        {
            if (emertimi == "")
                return new clsMesazh(false, "Plotesoni emertimin e transportuesit!");
            clsDatabaseInventari db = new clsDatabaseInventari();
            if (shtim && db.ekzistonTrasportues(emertimi, idNdermarrje))
            {
                db.Dispose();
                return new clsMesazh(false, "Ekziston nje transportues me kete emertim!");
            }

            return new clsMesazh(true, "Kontrollet e artikullit u kaluan me sukses");
        }

        private bool mbushTransportuesSipasEmertimit(string emertimi, int idNdermarrje)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushTransportues(dbInventari.merrTransportuesSipasEmertimit(emertimi, idNdermarrje));
            dbInventari.Dispose();
            return sukses;
        }

        public static int merrIdTransportuesSipasEmertimit(string emertimi, int idNdermarrje)
        {
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
            {
                return dbInventari.merrIdTransportuesSipasEmertimit(emertimi, idNdermarrje);
            }
        }

        public static string merrEmertimTransportuesSipasId(int idTransportues)
        {
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
            {
                return dbInventari.merrEmertimTransportuesSipasId(idTransportues);
            }
        }

        /// <summary>
        /// mbush automjetin sipas id-se 
        /// </summary>
        /// <param name="idTransportues">id e transportuesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushTransportues(int idTransportues)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushTransportues(dbInventari.merrTransportuesSipasId(idTransportues));
            dbInventari.Dispose();
            return sukses;
        }




        /// <summary>
        /// kthe nje datarow me transportuesin e marre nga db sipas id.
        /// </summary>
        /// <param name="idAutomjet"></param>
        /// <returns>datarow</returns>
        public static DataRow ktheTransportuesSipasId(int idTransportues)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataRow dr = dbInventari.merrTransportuesSipasId(idTransportues);
            return dr;
        }

        /// <summary>
        /// kthe nje datatable me transportuesin e marre nga db sipas id.
        /// </summary>
        /// <param name="idTransportues"></param>
        /// <returns>datatable</returns>
        public static DataTable ktheTransportuesSipasIdDt(int idTransportues)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable dt = dbInventari.merrTransportuesSipasIdDt(idTransportues);
            return dt;
        }
        public static string ktheTargeTransportuesSipasId(int idtransportues)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            string targa = dbInventari.merrTargeTransportuesiSipasId(idtransportues);
            return targa;
        }
        public static bool ekzistonTransportues(string emertimi, int idNdermarrje)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            bool ekziston = ekzistonTransportues(dbInventar, emertimi, idNdermarrje);
            dbInventar.Dispose();
            return ekziston;
        }

        public static bool ekzistonTransportues(clsDatabaseInventari dbInventar, string emertimi, int idNdermarrje)
        {
            bool ekziston = dbInventar.ekzistonTrasportues(emertimi, idNdermarrje);
            return ekziston;
        }

        public clsMesazh ruaj()
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            clsMesazh mesazh = new clsMesazh();
            try
            {
                dbInventar.beginTransaksion();
                int idTransportues = 0;
                mesazh = dbInventar.ruajTransportues(out idTransportues, this.emertimi, this.nipt, this.adresa, this.tel, this.idStatusDok, this.idNdermarrje, this.idKrijuesi, this.idPerdorues, this.targa, this.aktiv, this.tipiId, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
                this.idTransportues = idTransportues;
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
                mesazh = dbInventar.modifikoTransportues(this.idTransportues, this.emertimi, this.nipt, this.adresa, this.tel, this.idStatusDok, this.idNdermarrje, this.idPerdorues, this.targa, this.aktiv, this.tipiId, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
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

        public clsMesazh fshi(int idPerdorues)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            clsMesazh mesazh = new clsMesazh();
            try
            {
                dbInventar.beginTransaksion();
                mesazh = dbInventar.fshiTransportuesMeStatusDok(this.idTransportues, idPerdorues);
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
        /// Metode per mbushjen e automjetit nga databaza. Thirret nga metoda mbushAutomjetet e colAutomjete.cs
        /// </summary>
        /// <param name="dbDataRowAutomjet">Si parameter merr nje DataRow.</param>
        /// <returns>Kthen true nese mbushja ndodh me sukses. Ne te kundert false.</returns>
        /// <param name="dbinventari"></param>
        internal bool mbushTransportues(DataRow dbDataRowTransportues)
        {
            if (dbDataRowTransportues == null)
                return false;
            try
            {
                int.TryParse(dbDataRowTransportues["IDTRANSPORTUES"].ToString(), out idTransportues);
                emertimi = dbDataRowTransportues["EMERTIMI"].ToString();
                nipt = dbDataRowTransportues["NIPT"].ToString();
                adresa = dbDataRowTransportues["ADRESA"].ToString();
                tel = dbDataRowTransportues["TEL"].ToString();
                int.TryParse(dbDataRowTransportues["IDSTATUSDOK"].ToString(), out idStatusDok);
                int.TryParse(dbDataRowTransportues["IDNDERMARRJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowTransportues["IDKRIJUES"].ToString(), out idKrijuesi);
                int.TryParse(dbDataRowTransportues["IDPERDORUES"].ToString(), out idPerdorues);
                DateTime.TryParse(dbDataRowTransportues["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowTransportues["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                targa = Convert.ToString(dbDataRowTransportues["TARGA"]);
                if (dbDataRowTransportues["AKTIV"] != DBNull.Value)
                    aktiv = Convert.ToBoolean(dbDataRowTransportues["AKTIV"]);
                if (dbDataRowTransportues.Table.Columns.Contains("TIPIID"))
                    tipiId = Convert.ToString(dbDataRowTransportues["TIPIID"]);

                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se automjetit nga db-ja");
            }
        }

        #endregion
    }
}
