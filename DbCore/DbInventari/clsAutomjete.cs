using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbKontabiliteti;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne automjetet
    ///  (Te dhenat  merren nga tabela : T_AUTOMJETE)
    /// </summary>
    public class clsAutomjete
    {
        #region Atribute

        private int idAutomjeti;
        private string nrShasie;
        private string targa;
        private int idModelAutomjeti;
        private string pershkrimModeli;
        private int vitiProdhimit;
        private double kilometra;
        private string kodMotorri;
        private int idStatusDok;
        private int idKlienti;
        private string klienti;
        private int idKrijuesi;
        private int idNdermarrje;
        private int idPerdorues;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string marka;
        private DataRow rreshti;
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdAutomjeti
        {
            get { return idAutomjeti; }
            set { idAutomjeti = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e shasise.
        /// </summary>
        public string NrShasie
        {
            get { return nrShasie; }
            set { nrShasie = value; }
        }

        /// <summary>
        /// Kthen/Vendos targen.
        /// </summary>
        public string Targa
        {
            get { return targa; }
            set { targa = value; }
        }

        /// <summary>
        /// Kthen/Vendos Pershkrimin e Modelit te automjetit.
        /// </summary>
        public string PershkrimModeli
        {
            get { return pershkrimModeli; }
            set { pershkrimModeli = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e modelit te automjetit.
        /// </summary>
        public int IdModelAutomjeti
        {
            get { return idModelAutomjeti; }
            set { idModelAutomjeti = value; }
        }

        /// <summary>
        /// Kthen/Vendos vitin e prodhimit te automjetit.
        /// </summary>
        public int VitiProdhimit
        {
            get { return vitiProdhimit; }
            set { vitiProdhimit = value; }
        }

        /// <summary>
        /// Kthen/Vendos kilometrat
        /// </summary>
        public Double Kilometra
        {
            get { return kilometra; }
            set { kilometra = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e motorrit.
        /// </summary>
        public string KodMotorri
        {
            get { return kodMotorri; }
            set { kodMotorri = value; }
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
        /// Kthen/Vendos ID-ne e klientit.
        /// </summary>
        public int IdKlienti
        {
            get { return idKlienti; }
            set { idKlienti = value; }
        }

        /// <summary>
        /// Kthen/Vendos emertimin e klientit.
        /// </summary>
        public string Klienti
        {
            get { return klienti; }
            set { klienti = value; }
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
        /// Kthen/Vendos marken.
        /// </summary>
        public string Marka
        {
            get { return marka; }
            set { marka = value; }
        }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsAutomjete()
        {
            ImbLogger.LogTraceShitje("U krijua nje klas e tipit clsAutomjete!");
        }

        public clsAutomjete(int idAutomjeti, string nrShasie, string targa, int idModeli, int vitiProdhimit, double kilometra, string kodMotorri, int idStatusDok, int idKlienti, int idKrijues, int idNdermarrje, int idPerdorues, DateTime dtKrijimi, DateTime dtModifikimi, string klienti, string pershkrimModeli, bool shtim, string marka, ResourceManager rm, CultureInfo ci)
        {
            try
            {
                this.idAutomjeti = idAutomjeti;
                this.nrShasie = nrShasie;
                this.targa = targa;
                this.idModelAutomjeti = idModeli;
                this.vitiProdhimit = vitiProdhimit;
                this.kilometra = kilometra;
                this.kodMotorri = kodMotorri;
                this.idStatusDok = idStatusDok;
                this.idKlienti = idKlienti;
                this.idKrijuesi = idKrijues;
                this.idNdermarrje = idNdermarrje;
                this.idPerdorues = idPerdorues;
                this.dtKrijimi = dtKrijimi;
                this.dtModifikimi = dtModifikimi;
                this.klienti = klienti;
                this.pershkrimModeli = pershkrimModeli;
                this.marka = marka;

                clsMesazh mesazh = this.kontrolloAutomjete(shtim, rm, ci);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public clsAutomjete(DataRow rreshti)
        {
            
            mbushAutomjet(rreshti);
        }
        public clsAutomjete(string nrShasie, int idNdermarrje, clsDatabaseInventari dbI)
        {
            mbushAutomjet(dbI.TransCache.getAutomjet(nrShasie, idNdermarrje, dbI));
        }
        public clsAutomjete(int idNdermarrje, string targa, clsDatabaseInventari dbI)
        {
            mbushAutomjet(dbI.TransCache.getAutomjet(idNdermarrje, targa, dbI));
        }

        #endregion

        #region Metoda Publike

        private clsMesazh kontrolloAutomjete(bool shtim, ResourceManager rm, CultureInfo ci)
        {
            if (nrShasie == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoNrShasie"]);
            clsMesazh kontrollnrShasie = clsFunksione.kontrolloKaraktereMeMesazh(nrShasie, FusheKontrolli.Kodi, false);
            if (!kontrollnrShasie.Status)
                return kontrollnrShasie;

            clsDatabaseInventari db = new clsDatabaseInventari();
            if (shtim)
            {
                if (db.ekzistonAutomjet(nrShasie, idNdermarrje))
                {
                    db.Dispose();
                    return new clsMesazh(false, MessagesResource.Messages["msgEkzistonAutoNrShasie"]);
                }
            }
            else if (db.ekzistonAutomjetTjeter(nrShasie, idAutomjeti, idNdermarrje))
            {
                db.Dispose();
                return new clsMesazh(false, MessagesResource.Messages["msgEkzistonAutoNrShasieTjeter"]);
            }
            if (targa == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoTargen"]);
            if (db.ekzistonAutomjetSipasTarges(targa, idNdermarrje))
            {
                db.Dispose();
                if (shtim)
                    return new clsMesazh(false, MessagesResource.Messages["msgEkzistonAutoTarge"]);
                else
                {
                    clsAutomjete automjet = new clsAutomjete();
                    int idAutoTarge = merrIdAutomjetiSipasTarges(targa, idNdermarrje);
                    if (idAutoTarge != this.idAutomjeti)
                        return new clsMesazh(false, MessagesResource.Messages["msgEkzistonAutoTarge"]);
                }
            }
            //if (pershkrimModeli == "")
            //    return new clsMesazh(false, "Plotesoni modelin e automjetit!");
            if (pershkrimModeli != "" && (idModelAutomjeti == 0 || idModelAutomjeti == -1))
                return new clsMesazh(false, "Modeli nuk ekziston!");

            //if (klienti == "")
            //    return new clsMesazh(false, "Plotesoni klientin!");
            if (klienti != "" && (idKlienti == 0 || idKlienti == -1))
                return new clsMesazh(false, "Klienti nuk ekziston!");
            return new clsMesazh(true, "Kontrollet e artikullit u kaluan me sukses");
        }

        private bool mbushAutomjetSipasTarges(string targa, int idNdermarrje)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushAutomjet(dbInventari.merrAutomjetSipasTarges(targa, idNdermarrje));
            dbInventari.Dispose();
            return sukses;
        }

        protected int merrIdAutomjetiSipasTarges(string targa, int idNdermarrje)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            int idAutoTarge = dbInventari.merrIdAutomjetiSipasTarges(targa, idNdermarrje);
            dbInventari.Dispose();
            return idAutoTarge;
        }

        public static int ktheidAutomjetSipasShasise(string shasia, int idNdermarrje)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            int idAuto = dbInventari.merrIdAutomjetiSipasShasise(shasia, idNdermarrje);
            dbInventari.Dispose();
            return idAuto;
        }

        /// <summary>
        /// mbush automjetin sipas id-se 
        /// </summary>
        /// <param name="idAutomjet">id e automjetit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushAutomjet(int idAutomjet)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushAutomjet(dbInventari.merrAutomjetSipasId(idAutomjet));
            dbInventari.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush automjetin sipas id-se se ndermarrjes dhe nr te shasise
        /// </summary>
        /// <param name="idNdermarrje">id e automjetit</param>
        ///<param name="nrShasie">nr i shasise</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushAutomjet(int idNdermarrje, string nrShasie)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushAutomjet(dbInventari.merrAutomjetSipasNdermarrjeDheNrShasie(idNdermarrje, nrShasie));
            dbInventari.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush automjetin sipas id-se se ndermarrjes dhe nr te shasise
        /// </summary>
        /// <param name="idNdermarrje">id e automjetit</param>
        ///<param name="nrShasie">nr i shasise</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushAutomjetSipasTarges(int idNdermarrje, string targa)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushAutomjet(dbInventari.merrAutomjetSipasTarges(targa, idNdermarrje));
            dbInventari.Dispose();
            return sukses;
        }

        /// <summary>
        /// kthe nje datarow me automjetin e marre nga db sipas id.
        /// </summary>
        /// <param name="idAutomjet"></param>
        /// <returns>datarow</returns>
        /// 
        public static DataRow ktheAutomjetSipasId(int idAutomjet)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataRow dr = dbInventari.merrAutomjetSipasId(idAutomjet);
            return dr;
        }

        /// <summary>
        /// kthen targen e automjetin te marre nga db sipas id.
        /// </summary>
        /// <param name="idAutomjet"></param>
        /// <returns>datarow</returns>
        public static string ktheTargeAutomjetSipasId(int idAutomjet)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            string targa = dbInventari.merrTargeAutomjetSipasId(idAutomjet);
            return targa;
        }

        /// <summary>
        /// kthen targen e automjetin te marre nga db sipas id.
        /// </summary>
        /// <param name="idAutomjet"></param>
        /// <returns>datarow</returns>
        public static string ktheTargeAutomjetSipasIdAuto(int idAutomjet)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            string targa = dbInventari.merrTargeAutomjetSipasId(idAutomjet);
            return targa;
        }

        /// <summary>
        /// kthe nje datatable me automjetin e marre nga db sipas id.
        /// </summary>
        /// <param name="idAutomjet"></param>
        /// <returns>datatable</returns>
        public static DataTable ktheAutomjetSipasIdDt(int idAutomjet)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable dt = dbInventari.merrAutomjetSipasIdDt(idAutomjet);
            dbInventari.Dispose();
            return dt;
        }

        public static bool ekzistonAutomjet(int idNdermarrje, string nrShasie)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            bool ekziston = dbInventar.ekzistonAutomjet(nrShasie, idNdermarrje);
            dbInventar.Dispose();
            return ekziston;
        }

        public static bool ekzistonAutomjetSipasTarges(int idNdermarrje, string targa, clsDatabaseInventari dbInventar)
        {
            bool ekziston = dbInventar.ekzistonAutomjetSipasTarges(targa, idNdermarrje);
            return ekziston;
        }

        public static bool ekzistonAutomjetSipasTarges(int idNdermarrje, string targa)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            bool ekziston = dbInventar.ekzistonAutomjetSipasTarges(targa, idNdermarrje);
            dbInventar.Dispose();
            return ekziston;
        }

        public clsMesazh ruaj(IDictionary<string, object> hfNrAuto)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            clsMesazh mesazh = new clsMesazh();
            bool kaNdryshimNumri;
            try
            {
                dbInventar.beginTransaksion();
                mesazh = kontrolloAutomjet(out kaNdryshimNumri, dbInventar, hfNrAuto, false);
                if (!mesazh.Status)
                {
                    dbInventar.rollbackTransaksion();
                    return mesazh;
                }
                int idAuto = 0;
                mesazh = dbInventar.ruajAutomjet(out idAuto, this.nrShasie, this.targa, this.idModelAutomjeti, this.vitiProdhimit, this.kilometra, this.kodMotorri, this.idStatusDok, this.idKlienti, this.idNdermarrje, this.idKrijuesi, this.idPerdorues, this.marka);
                this.idAutomjeti = idAuto;
                this.idAutomjeti = idAuto;
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

        private clsMesazh kontrolloAutomjet(out bool kaNdryshimNrAuto, clsDatabaseInventari db, IDictionary<string, object> hfNrAutoKF, bool modifikim)
        {
            kaNdryshimNrAuto = false;
            if (!modifikim)
            {
                clsMesazh mes = new clsMesazh();
                if (hfNrAutoKF != null)
                {
                    mes = kontrolloNrAutoAutomjet(out kaNdryshimNrAuto, db, hfNrAutoKF);
                    if (!mes.Status)
                        return mes;
                }
                return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }

        private clsMesazh kontrolloNrAutoAutomjet(out bool kaNdryshimNumri, clsDatabaseInventari db, IDictionary<string, object> hfNrAutoKf)
        {
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db);
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoKf, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "NrShasie") != "")
                this.NrShasie = NrAuto.ktheVlerenEre(list, "NrShasie");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, this.idPerdorues, this.idNdermarrje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        public clsMesazh modifiko()
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            clsMesazh mesazh = new clsMesazh();
            try
            {
                dbInventar.beginTransaksion();
                mesazh = dbInventar.modifikoAutomjet(this.idAutomjeti, this.nrShasie, this.targa, this.idModelAutomjeti, this.vitiProdhimit, this.kilometra, this.kodMotorri, this.idStatusDok, this.idKlienti, this.idNdermarrje, this.idPerdorues, this.marka);
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
                mesazh = dbInventar.fshiAutomjetMeStatusDok(this.idAutomjeti, idPerdorues);
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
        internal bool mbushAutomjet(DataRow dbDataRowAutomjet)
        {
            if (dbDataRowAutomjet == null)
                return false;
            try
            {
                int.TryParse(dbDataRowAutomjet["IDAUTOMJETI"].ToString(), out idAutomjeti);
                nrShasie = dbDataRowAutomjet["NRSHASIE"].ToString();
                targa = dbDataRowAutomjet["TARGA"].ToString();
                int.TryParse(dbDataRowAutomjet["MODELAUTOMJETI"].ToString(), out idModelAutomjeti);
                pershkrimModeli = dbDataRowAutomjet["PershkrimModeli"].ToString();
                int.TryParse(dbDataRowAutomjet["VITPRODHIMI"].ToString(), out vitiProdhimit);
                double.TryParse(dbDataRowAutomjet["KILOMETRA"].ToString(), out kilometra);
                kodMotorri = dbDataRowAutomjet["KODMOTORRI"].ToString();
                int.TryParse(dbDataRowAutomjet["IDSTATUSDOK"].ToString(), out idStatusDok);
                int.TryParse(dbDataRowAutomjet["IDKLIENTI"].ToString(), out idKlienti);
                klienti = dbDataRowAutomjet["Klienti"].ToString();
                int.TryParse(dbDataRowAutomjet["IDNDERMARRJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowAutomjet["IDKRIJUES"].ToString(), out idKrijuesi);
                int.TryParse(dbDataRowAutomjet["IDPERDORUES"].ToString(), out idPerdorues);
                DateTime.TryParse(dbDataRowAutomjet["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowAutomjet["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                marka = dbDataRowAutomjet["MARKA"].ToString();
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se automjetit nga db-ja");
            }
        }

        public clsMesazh mbushAutomjet(clsAutomjete automjeti)
        {
            ImbLogger.LogTraceShitje("Filloi mbushja e cls automjeti");
            IdAutomjeti = automjeti.IdAutomjeti;
            NrShasie = automjeti.NrShasie;
            Targa = automjeti.Targa;
            IdModelAutomjeti = automjeti.IdModelAutomjeti;
            PershkrimModeli = automjeti.PershkrimModeli;
            VitiProdhimit = automjeti.VitiProdhimit;
            Kilometra = automjeti.Kilometra;
            KodMotorri = automjeti.KodMotorri;
            IdStatusDok = automjeti.IdStatusDok;
            IdKlienti = automjeti.IdKlienti;
            Klienti = automjeti.Klienti;
            IdNdermarrje = automjeti.IdNdermarrje;
            IdKrijuesi = automjeti.IdKrijuesi;
            IdPerdorues = automjeti.IdPerdorues;
            DtKrijimi = automjeti.DtKrijimi;
            DtModifikimi = automjeti.DtModifikimi;
            Marka = automjeti.Marka;
            ImbLogger.LogTraceShitje($"Mbushja e cls automjetit me Nr.Shasie {automjeti.NrShasie} u krye me sukses!");
            return new clsMesazh(true, $"Mbushja e automjetit me Nr.Shasie {automjeti.NrShasie} u krye me sukses!");

        }

        #endregion
    }
}
