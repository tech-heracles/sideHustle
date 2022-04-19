using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;
using DbCore.DbKontabiliteti;
using DbCore.DbAdmin;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti qendra kosto
    ///  (Te dhenat  merren nga tabela : T_TRUPIQENDRAKOSTO)
    /// </summary>
    public class clsTrupiQendraKosto
    {

        #region Attributet
        /// <summary>
        /// id e trupit 
        /// </summary>
        private int idTrupi;
        /// <summary>
        /// id e kokes 
        /// </summary>
        private int idKoka;
        /// <summary>
        /// id e qendres se kostos
        /// </summary>
        private int idQK;
        /// <summary>
        /// kodi i qendres
        /// </summary>
        private string qendra;
        /// <summary>
        /// pershkrimi i qendres
        /// </summary>
        private string pershkrimiQendra;     
        /// <summary>
        /// vlefta ne monedhe llog
        /// </summary>
        private double vleftaLlog;
        /// <summary>
        /// id e objektives se kostos
        /// </summary>
        private int idOk;
        /// <summary>
        /// vlefta ne monedhen e qendres se kostos
        /// </summary>
        private double vleftaQK;
        /// <summary>
        /// vlefta ne monedhen baze 
        /// </summary>
        private double vleftaMonBaze;
        /// <summary>
        /// id e llogarise
        /// </summary>
        private int idLlog;
        /// <summary>
        /// kodi i objektives
        /// </summary>
        private string objektiva;
        /// <summary>
        /// pershkrimi i objektives
        /// </summary>
        private string pershkrimiObj;
        /// <summary>
        /// nr i llogarise
        /// </summary>
        private string llogaria;
        /// <summary>
        /// pershkrimi i llogarise
        /// </summary>
        private string pershkrimiLlog;
        /// <summary>
        /// monedha e llogarise
        /// </summary>
        private string monedhaLlog;
        /// <summary>
        /// debi/kredi
        /// </summary>
        private int debiKredi;
        /// <summary>
        /// kursi i llogarise
        /// </summary>
        private double kursiLlog;
        private DataRow rreshti;
        /// <summary>
        /// pershkrimi ne nivel rreshti
        /// </summary>
        private string pershkrimi;
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi { get { return idTrupi; } set { idTrupi = value; } }
        
        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te qendra kosto.
        /// </summary>
        public int IdKoka { get { return idKoka; } set { idKoka = value; } }

        /// <summary>
        /// Kthen/Vendos ID-ne e qendres se kostos .
        /// </summary>
        public int IdQK { get { return idQK; } set { idQK = value; } }

        /// <summary>
        /// Kthen/Vendos Kodi i qendres ose skemes se kostos.
        /// </summary>
        public String Qendra { get { return qendra; } set { qendra = value; } }

        /// <summary>
        /// Kthen/Vendos pershkrimi i qendres ose skemes.
        /// </summary>
        public String PershkrimiQendra { get { return pershkrimiQendra; } set { pershkrimiQendra = value; } }       

        /// <summary>
        /// Kthen/Vendos vleften ne monedhe llogarie.
        /// </summary>
        public double VleftaLlog { get { return vleftaLlog; } set { vleftaLlog = value; } }

        /// <summary>
        /// Kthen/Vendos ID-ne e objektivtes se kostos.
        /// </summary>
        public int IdOk { get { return idOk; } set { idOk = value; } }

        /// <summary>
        /// vlefta ne monedhe qender kosto
        /// </summary>
        public double VleftaQK { get { return vleftaQK; } set { vleftaQK = value; } }

        /// <summary>
        /// vlefta ne monedhen baze 
        /// </summary>
        public double VleftaMonBaze { get { return vleftaMonBaze; } set { vleftaMonBaze = value; } }

        /// <summary>
        /// id e llogarise
        /// </summary>
        public int IdLlog { get { return idLlog; } set { idLlog = value; } }

        /// <summary>
        /// kodi i objektives
        /// </summary>
        public string Objektiva { get { return objektiva; } }

        /// <summary>
        /// pershkrimi i objektives
        /// </summary>
        public string PershkrimiObj { get { return pershkrimiObj; } }

        /// <summary>
        /// nr i llogarise
        /// </summary>
        public string Llogaria { get { return llogaria; } }

        /// <summary>
        /// pershkrimi i llogarise
        /// </summary>
        public string PershkrimiLlog { get { return pershkrimiLlog; } }

        /// <summary>
        /// monedha e llogarise
        /// </summary>
        public string MonedhaLlog { get { return monedhaLlog; } }
       
        /// <summary>
        /// debi/kredi
        /// </summary>
        public int DebiKredi { get { return debiKredi; } set { debiKredi = value; } }
        /// <summary>
        /// kursi i llogarise
        /// </summary>
        public double KursiLlog { get { return kursiLlog; } set { kursiLlog = value; } }
        /// <summary>
        /// pershkrimi ne nivel rreshti
        /// </summary>
        public string Pershkrimi { get { return pershkrimi; } set { pershkrimi = value; } }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idkoka">id e kokes se dokumentit te planifikimit</param>
        /// <param name="idtrupi">id ritese e trupit te dokumentit te planifikimit</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="KodArt">kodi i artikullit</param>
        /// <param name="pershkArt">pershkrimi i artikullit</param>
        /// <param name="sas"> sasia e artikullit</param>
        public clsTrupiQendraKosto(int idtrupi, int idkoka, int idqk, string qendra, string pershkrimqendra, int idok, string obj, string pershkrimobj, int idllog, string llogari, string pershkrimllog, int debikr, double vleftallog, double vleftaqk, double vleftamonbaze, string monedha, double kursiLlog, string pershkrimi)
        {
            this.idTrupi = idtrupi;
            this.idKoka = idkoka;
            this.idQK = idqk;
            this.qendra = qendra;
            this.pershkrimiQendra = pershkrimqendra;
            this.objektiva = obj;
            this.pershkrimiObj = pershkrimobj;
            this.llogaria = llogari;
            this.pershkrimiLlog = pershkrimllog;
            this.vleftaLlog = vleftallog;
            this.idOk = idok;
            this.idLlog = idllog;            
            this.debiKredi = debikr;
            this.vleftaQK = vleftaqk;
            this.vleftaMonBaze = vleftamonbaze;
            this.monedhaLlog = monedha;
            this.kursiLlog = kursiLlog;
            this.pershkrimi = pershkrimi;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupiPlanifikim">id e trupit</param>
        /// <param name="db">clsDatabaseprodhimi per raste transaksioni</param>
        public clsTrupiQendraKosto(int idTrupi)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushTrupQendraKosto(db.ktheTrupiQendraKostoSipasID(idTrupi));
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiQendraKosto()
        {
        }

        public clsTrupiQendraKosto(int idndermarje, Dictionary<string, object> rresht, DateTime dtdok, colTrupatFletetKontabel trupiFk)
        {
            this.vleftaLlog = double.Parse(rresht["VleftaLlog"].ToString());

            if (rresht["DebiKredi"] != null)
                DebiKredi = int.Parse(rresht["DebiKredi"].ToString());

            DbCore.DbKontabiliteti.clsLlogari llog = new DbKontabiliteti.clsLlogari(rresht["Llogaria"].ToString(), idndermarje);

            this.idLlog = (llog.IdLlogari != 0 && llog.IdLlogari != -1 && llog.Aktiv != false)? llog.IdLlogari : 0;
            this.llogaria = (llog.IdLlogari != 0 && llog.IdLlogari != -1 && llog.Aktiv != false) ? llog.NrLlogari : "";
            this.pershkrimiLlog = (llog.IdLlogari != 0 && llog.IdLlogari != -1 && llog.Aktiv != false) ? llog.EmerLlogari1 : "";
            this.monedhaLlog = (llog.IdLlogari != 0 && llog.IdLlogari != -1 && llog.Aktiv != false) ? llog.KodiMonedha : "";
            double vleraKursit = 1;
            int idMonedheNderm = clsNdermarrje.ktheIdMonedheNdermSipasID(idndermarje);
            if (idMonedheNderm == llog.IdMonedha)
                this.vleftaMonBaze = vleftaLlog;
            else
            {
                clsTrupiFleteKontabel trupi = trupiFk != null ? trupiFk.Find(x => x.IdLlogari == this.idLlog) : null;
                if (trupi != null)
                    vleraKursit = trupi.Kursi;
                else
                {
                    clsKurset kursi = new clsKurset(llog.IdMonedha, dtdok);
                    vleraKursit = kursi.VleraKursi == 0 ? 1 : kursi.VleraKursi;
                }
                this.vleftaMonBaze = vleftaLlog * vleraKursit;
            }
            this.kursiLlog = vleraKursit;
            var kodQendra = rresht["Qendra"].ToString();
            clsQendraKosto qendra = new clsQendraKosto(kodQendra, idndermarje);
            if (!String.IsNullOrEmpty(kodQendra) && qendra.Id <= 0)
                throw new DbCore.MyException(MessagesResource.Messages["msgQendraKostosMeKodNukEkziston"].Replace("#kodi", kodQendra));

            if (qendra.Id != 0 && qendra.Id != -1)
            {
                this.idQK = qendra.Id;
                this.qendra = qendra.Kodi;
                this.pershkrimiQendra = qendra.Pershkrimi;
                if (qendra.IdMonedha == llog.IdMonedha)
                    this.vleftaQK = vleftaLlog;
                else if (qendra.IdMonedha == idMonedheNderm)
                    this.vleftaQK = vleftaMonBaze;
                else
                {
                    clsKurset kursi = new clsKurset(qendra.IdMonedha, dtdok);
                    if (kursi.VleraKursi == 0)
                        kursi.VleraKursi = 1;
                    this.vleftaQK = vleftaMonBaze / kursi.VleraKursi;
                }
            }
            else
            {
                this.idQK = 0;
                this.qendra = "";
                this.pershkrimiQendra = "";
                this.vleftaQK = 0;
                this.vleftaMonBaze = 0;
            }

            clsObjektivaKosto obj = new clsObjektivaKosto(rresht["Objektiva"].ToString(), idndermarje);
            this.idOk = (obj.Id != 0 && obj.Id != -1) ? obj.Id : 0;
            this.objektiva = (obj.Id != 0 && obj.Id != -1) ? obj.Kodi : "";
            this.pershkrimiObj = (obj.Id != 0 && obj.Id != -1) ? obj.Pershkrimi : "";
        }

        public clsTrupiQendraKosto(int idndermarje, clsTrupiQendraKosto tr, DateTime dtdok, bool eshteDokGjeneruar, colTrupatFletetKontabel trupiFk, string pershkrimiKoka)
        {
            this.vleftaLlog = tr.VleftaLlog;
            this.DebiKredi = tr.DebiKredi;
            this.Pershkrimi = String.IsNullOrWhiteSpace(tr.Pershkrimi) ? pershkrimiKoka : tr.Pershkrimi;
            clsLlogari llog = new clsLlogari(tr.IdLlog);
            int idMonedheNderm = clsNdermarrje.ktheIdMonedheNdermSipasID(idndermarje);

            this.idLlog = (llog.IdLlogari != 0 && llog.IdLlogari != -1 && llog.Aktiv != false) ? llog.IdLlogari : 0;
            this.llogaria = (llog.IdLlogari != 0 && llog.IdLlogari != -1 && llog.Aktiv != false) ? llog.NrLlogari : "";
            this.pershkrimiLlog = (llog.IdLlogari != 0 && llog.IdLlogari != -1 && llog.Aktiv != false) ? llog.EmerLlogari1 : "";
            this.monedhaLlog = (llog.IdLlogari != 0 && llog.IdLlogari != -1 && llog.Aktiv != false) ? llog.KodiMonedha : "";

            double vleraKursit = 1;
            this.vleftaMonBaze = vleftaLlog;

            if (eshteDokGjeneruar && idMonedheNderm != llog.IdMonedha)
            {
                clsTrupiFleteKontabel trupi = trupiFk != null ? trupiFk.Find(x => x.IdLlogari == this.idLlog) : null;
                if (trupi != null)
                    vleraKursit = trupi.Kursi;
                else
                {
                    clsKurset kursi = new clsKurset(llog.IdMonedha, dtdok);
                    vleraKursit = kursi.VleraKursi == 0 ? 1 : kursi.VleraKursi;
                }
                this.vleftaMonBaze = vleftaLlog * vleraKursit;
            }
            this.kursiLlog = vleraKursit;
            var kodQendra = tr.Qendra;
            clsQendraKosto qendra = new clsQendraKosto(kodQendra, idndermarje);
            if (!String.IsNullOrEmpty(kodQendra) && qendra.Id <= 0)
                throw new DbCore.MyException(MessagesResource.Messages["msgQendraKostosMeKodNukEkziston"].Replace("#kodi", kodQendra));

            if (qendra.Id != 0 && qendra.Id != -1)
            {
                this.idQK = qendra.Id;
                this.qendra = qendra.Kodi;
                this.pershkrimiQendra = qendra.Pershkrimi;
                if (qendra.IdMonedha == llog.IdMonedha)
                    this.vleftaQK = vleftaLlog;
                else if (qendra.IdMonedha == idMonedheNderm)
                    this.vleftaQK = vleftaMonBaze;
                else
                {
                    clsKurset kursi = new clsKurset(qendra.IdMonedha, dtdok);
                    if (kursi.VleraKursi == 0)
                        kursi.VleraKursi = 1;
                    this.vleftaQK = vleftaMonBaze / kursi.VleraKursi;
                }
            }
            else
            {
                this.idQK = 0;
                this.qendra = "";
                this.pershkrimiQendra = "";
                this.vleftaQK = 0;
                this.vleftaMonBaze = 0;
            }

            clsObjektivaKosto obj = new clsObjektivaKosto(tr.IdOk);
            this.idOk = (obj.Id != 0 && obj.Id != -1) ? obj.Id : 0;
            this.objektiva = (obj.Id != 0 && obj.Id != -1) ? obj.Kodi : "";
            this.pershkrimiObj = (obj.Id != 0 && obj.Id != -1) ? obj.Pershkrimi : "";
        }
        public clsTrupiQendraKosto(DataRow rreshti)
        {            
            mbushTrupQendraKosto(rreshti);
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e qendra kosto nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupQendraKosto(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDTRUPI"].ToString(), out this.idTrupi);
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out this.idKoka);
                    int.TryParse(dbDataRow["IDQK"].ToString(), out this.idQK);
                    double.TryParse(dbDataRow["VLEFTALLOG"].ToString(), out this.vleftaLlog);
                    double.TryParse(dbDataRow["VLEFTAMONBAZE"].ToString(), out this.vleftaMonBaze);
                    int.TryParse(dbDataRow["IDLLOG"].ToString(), out this.idLlog);
                    int.TryParse(dbDataRow["IDOK"].ToString(), out this.idOk);
                    int.TryParse(dbDataRow["DEBIKREDI"].ToString(), out this.debiKredi);
                    this.qendra = dbDataRow["Qendra"].ToString();
                    this.pershkrimiQendra = dbDataRow["PershkrimiQendra"].ToString();
                    double.TryParse(dbDataRow["VLEFTAQK"].ToString(), out this.vleftaQK);
                    this.objektiva = dbDataRow["Objektiva"].ToString();
                    this.pershkrimiObj = dbDataRow["PershkrimiObj"].ToString();
                    this.llogaria = dbDataRow["Llogaria"].ToString();
                    this.pershkrimiLlog = dbDataRow["PershkrimiLlog"].ToString();
                    this.monedhaLlog = dbDataRow["Monedha"].ToString();
                    double.TryParse(dbDataRow["KURSI"].ToString(), out this.kursiLlog);
                    this.pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te qendra kosto nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
