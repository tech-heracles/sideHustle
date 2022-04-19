using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbKontabiliteti;
using System.Data;
using DbCore.DbAdmin;
using DbCore.DbRegjistrim;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne cmimet e artikujve
    ///  (Te dhenat  merren nga tabela : T_CMIMARTIKULLI)
    /// </summary>
    public class clsCmimArtikulli
    {
        public static int IdAutomatike { get; internal set; }
        #region Atribute

        private int idCmimArtikulli;
        private int idArtikulli;
        private int idNivelCmimi;
        private int idNjesia;
        private int idMonedha;
        private DateTime dateFillimit;
        private DateTime dateMbarimit;
        private decimal sasiMin;
        private decimal sasiMax;
        private decimal cmimi;

        private int idPerdoruesi;
        private int idNdermarje;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idNjesia2;
        private decimal cmimi2;
        private decimal koeficent;
        private double kosto;
        private decimal gjendje;
        private string kodNiveli;
        private string kodMonedha;
        //private string formula;
        private double kursi;
        private DateTime koheFillimi;
        private DateTime koheMbarimi;
        private int idTvsh;
        private decimal cmimiTvsh;
        private decimal cmimi2Tvsh;
        private decimal norme;
        private int idDetajim;
        private int idCmimRetail;
        #endregion

        #region Auto Properties
        public string EmerArtikulli1 { get; set; }
        /// <summary>
        /// Kthen/Vendos Pershkrimin e njesise se pare
        /// </summary>
        public string PershkrimNjesia1 { get; set; }
        /// <summary>
        /// Kthen/Vendos Pershkrimin e njesise se dyte
        /// </summary>
        public string PershkrimNjesia2 { get; set; }
        /// <summary>
        /// Kthen/Vendos emrin e artikullit ne gjuhe te huaj.
        /// </summary>
        public string EmerArtikulli2 { get; set; }
        /// <summary>
        /// Kthen/Vendos kodbarin e artikullit.
        /// </summary>
        public string KodbarArtikulli { get; set; }
        /// <summary>
        /// Kthen/Vendos kodifikimin e pare te artikullit.
        /// </summary>
        public string KodifikimArtikulli1 { get; set; }
        /// <summary>
        /// Kthen/Vendos kodifikimin e dyte te artikullit.
        /// </summary>
        public string KodifikimArtikulli2 { get; set; }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se furnitorit.
        /// </summary>
        public string NrLlogariFurnitori { get; set; }

        public Boolean Update { get; set; }
        /// <summary>
        /// Kthen/Vendos nese njesite jane te varura nga njera tjetra apo jo
        /// </summary>
        public Boolean NjesiTeVarura { get; private set; }
        /// <summary>
        /// Kthen/Vendos kodin e artikullit.
        /// </summary>
        public string KodArtikulli { get; set; }
        /// <summary>
        /// Kthen/Vendos Emertimin e Klient/Furnitorit
        /// </summary>
        public string EmertimiKF { get; set; }
        /// <summary>
        /// Kthen/Vendos Pershkrimin e grupimit te pare te artikullit
        /// </summary>
        public string PershkrimGrup1 { get; set; }
        /// <summary>
        /// Kthen/Vendos Pershkrimin e grupimit te dyte te artikullit
        /// </summary>
        public string PershkrimGrup2 { get; set; }
        /// <summary>
        /// Kthen/Vendos Pershkrimin e grupimit te trete te artikullit
        /// </summary>
        public string PershkrimGrup3 { get; set; }
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdCmimArtikulli
        {
            get { return idCmimArtikulli; }
            set { idCmimArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikulli; }
            set { idArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te cmimit.
        /// </summary>
        public int IdNivelCmimi
        {
            get { return idNivelCmimi; }
            set { idNivelCmimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e njesise se artikullit.
        /// </summary>
        public int IdNjesia
        {
            get { return idNjesia; }
            set { idNjesia = value; }
        }
        public int IdNjesia2
        {
            get { return idNjesia2; }
            set { idNjesia2 = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e monedhes.
        /// </summary>
        public int IdMonedha
        {
            get { return idMonedha; }
            set { idMonedha = value; }
        }
        /// <summary>
        /// Kthen/Vendos daten e fillimit.
        /// </summary>
        public DateTime DateFillimi
        {
            get { return dateFillimit; }
            set { dateFillimit = value; }
        }
        /// <summary>
        /// Kthen/Vendos daten e mbarimit.
        /// </summary>
        public DateTime DateMbarimi
        {
            get { return dateMbarimit; }
            set { dateMbarimit = value; }
        }
        public DateTime KoheFillimi
        {
            get { return koheFillimi; }
            set { koheFillimi =value; }
        }
        public DateTime KoheMbarimi
        {
            get { return koheMbarimi; }
            set { koheMbarimi =value; }
        }
        /// <summary>
        /// Kthen/Vendos sasine minimale.
        /// </summary>
        public decimal SasiMin
        {
            get { return sasiMin; }
            set { sasiMin = value; }
        }
        /// <summary>
        /// Kthen/Vendos sasine maksimale.
        /// </summary>
        public decimal SasiMax
        {
            get { return sasiMax; }
            set { sasiMax = value; }
        }
        /// <summary>
        /// Kthen/Vendos Cmimin
        /// </summary>
        public decimal Cmimi
        {
            get { return cmimi; }
            set { cmimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos Cmimin
        /// </summary>
        public decimal Cmimi2
        {
            get { return cmimi2; }
            set { cmimi2 = value; }
        }

        /// <summary>
        /// Kthen/Vendos Koston e artikullit
        /// </summary>
        public double Kosto
        {
            get { return kosto; }
            set { kosto = value; }
        }

        /// <summary>
        /// Kthen/Vendos Gjendjen e artikullit
        /// </summary>
        public decimal Gjendje
        {
            get { return gjendje; }
        }

        /// <summary>
        /// Kthen/Vendos Cmimin
        /// </summary>
        public decimal Koeficent
        {
            get { return koeficent; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe ka kryer veprimin.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit.
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
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
        public Double Kursi
        {
            get { return kursi; }
            set { kursi = value; }
        }
        public int IdTvsh
        {
            get { return idTvsh; }
            set { idTvsh = value; }
        }
        public decimal CmimiTvsh
        {
            get { return cmimiTvsh; }
            set { cmimiTvsh = value; }
        }
        public decimal Cmimi2Tvsh
        {
            get { return cmimi2Tvsh; }
            set { cmimi2Tvsh = value; }
        }
        public decimal Norme
        {
            get { return norme; }           
        }
        public decimal Formula
        {
            get { return 1; }
        }
        public int IdDetajim
        {
            get { return idDetajim; }
            set { idDetajim = value; }
        }
        public int IdCmimRetail
        {
            get { return idCmimRetail; }
            set { idCmimRetail = value; }
        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCmimArtikulli"> id ritese e cmimit te artikullit</param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="idNivelCmimi"> id e nivelit te cmimit</param>
        /// <param name="idNjesia">id njesia e artikullit</param>
        /// <param name="idMonedha"> id monedha</param>
        /// <param name="dateFillimit"> data e fillimit te periudhes per kete cmim</param>
        /// <param name="dateMbarimit"> data e mbarimit te periudhes per kete cmim</param>
        /// <param name="sasiMin"> sasia minimale</param>
        /// <param name="sasiMax"> sasia maksimale</param>
        /// <param name="cmimi">cmimi</param>
        /// <param name="idPerdoruesi"> id  e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        public clsCmimArtikulli(int idCmimArtikulli, int idArtikulli, int idNivelCmimi, int idNjesia, int idMonedha,
        DateTime dateFillimit, DateTime dateMbarimit, decimal sasiMin, decimal sasiMax, decimal cmimi, int idPerdoruesi, int idNdermarje, int idkonfig, int idstatusdok, int idnjesia2, decimal cmimi2, bool njesitevarura, double kursi, DateTime kohefillimi, DateTime kohembarimi, int idtvsh, decimal cmimitvsh, decimal cmimitvsh2, decimal norma, int detajim , int idCmimRetail )//, string formula  
        {
            this.idCmimArtikulli = idCmimArtikulli;
            this.idArtikulli = idArtikulli;
            this.idNivelCmimi = idNivelCmimi;
            this.idNjesia = idNjesia;
            this.idMonedha = idMonedha;
            this.dateFillimit = dateFillimit;
            this.dateMbarimit = dateMbarimit;
            this.sasiMin = sasiMin;
            this.sasiMax = sasiMax;
            this.cmimi = cmimi;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.idNdermarje = idNdermarje;
            this.idKonfig = idkonfig;
            this.idStatusDok = idstatusdok;
            this.idNjesia2 = idnjesia2;
            this.cmimi2 = cmimi2;
            this.NjesiTeVarura = njesitevarura;
            this.koheFillimi = kohefillimi;
            this.koheMbarimi = kohembarimi;
            //this.formula = formula;
            this.kursi = kursi;
            this.idTvsh = idtvsh;
            this.cmimiTvsh = cmimitvsh;
            this.cmimi2Tvsh = cmimitvsh2;
            this.norme = norma;
            this.idDetajim = detajim;
            this.idCmimRetail = idCmimRetail;
        }

        public clsCmimArtikulli(int idCmimArtikulli, int idArtikulli, string kodartikulli, int idNivelCmimi, string kodniveli, int idNjesia, string kodnjesia, int idMonedha, string kodmonedha,
        DateTime dateFillimit, DateTime dateMbarimit, decimal sasiMin, decimal sasiMax, decimal cmimi, int idPerdoruesi, int idNdermarje, int idkonfig, int idnjesia2, string kodnjesia2, decimal cmimi2, bool njesitevarura, DateTime kohefillimi, DateTime kohembarimi, int idtvsh, decimal cmimitvsh, decimal cmimitvsh2,int detajim, int idCmimRetail , ResourceManager rm, CultureInfo ci)//, string formula
        {
            try
            {
                this.idCmimArtikulli = idCmimArtikulli;
                this.idArtikulli = idArtikulli;
                this.KodArtikulli = kodartikulli;
                this.idNivelCmimi = idNivelCmimi;
                this.kodNiveli = kodniveli;
                this.idNjesia = idNjesia;
                this.PershkrimNjesia1 = kodnjesia; ;
                this.PershkrimNjesia2 = kodnjesia2;
                this.idMonedha = idMonedha;
                this.kodMonedha = kodmonedha;
                this.dateFillimit = dateFillimit;
                this.dateMbarimit = dateMbarimit;
                this.sasiMin = sasiMin;
                this.sasiMax = sasiMax;
                this.cmimi = cmimi;
                this.idPerdoruesi = idPerdoruesi;
                this.idNdermarje = idNdermarje;
                this.idKonfig = idkonfig;
                this.idStatusDok = 1;
                this.idNjesia2 = idnjesia2;
                this.cmimi2 = cmimi2;
                this.NjesiTeVarura = njesitevarura;
                this.koheFillimi = kohefillimi;
                this.koheMbarimi = kohembarimi;
                this.idTvsh = idtvsh;
                this.cmimiTvsh = cmimitvsh;
                this.cmimi2Tvsh = cmimitvsh2;
                this.idDetajim = detajim;
                this.idCmimRetail = idCmimRetail;

                //this.formula = formula;
                clsMesazh mesazh = this.kontrolloCmim(rm, ci);

                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// kontruktori pa parametra
        /// </summary>
        public clsCmimArtikulli()
        {
        }

        public clsCmimArtikulli(DataRow rreshti)
        {
            
            mbushCmimArtikulli(rreshti);
        }

        #endregion

        #region Metoda Publike
        public clsCmimArtikulli krijoCmimPerImport(string pershkrimniveli, string kodartikulli, string kodmonedha, string kodnjesi1, string kodnjesi2, decimal cmim1, decimal cmim2, DateTime dtfillimi, DateTime dtmbarimi, decimal sasimin, decimal sasimax, int idPerdoruesi, int idNdermarje, int idKonfigCSH, int idKonfigCB, DateTime kohefillimi, DateTime kohembarimi, string tvsh, decimal cmimitvsh, decimal cmimitvsh2, ResourceManager rm, CultureInfo ci, colNiveleCmimesh colNiveleCmimi)//, string formula
        {
            try
            {

                int idKonfgAmbienti = 0;

                clsNivelCmimi nc = new clsNivelCmimi();
                nc = colNiveleCmimi.FirstOrDefault(x => x.PershkrimNivelCmimi == pershkrimniveli);
                if (nc == null)
                    throw new Exception("Niveli i cmimit nuk ekziston!");
                if (nc.LlojiNivelCmimi == 1)
                    idKonfgAmbienti = idKonfigCB;
                else
                    idKonfgAmbienti = idKonfigCSH;

                DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
                art.mbushArtikull(kodartikulli, idNdermarje);
                idArtikulli = art.IdArtikulli;
                if (art.IdArtikulli < 1)
                 throw new MyException(string.Format("Artikulli me kod : '{0}' nuk ekziston!", kodartikulli));
                if (!art.Aktiv)
                  throw new MyException(string.Format("Artikulli me kod : '{0}' nuk eshte aktiv!", kodartikulli));

                clsTaksa taks = new clsTaksa();

                if (tvsh == "" && (cmimitvsh != 0 || cmimitvsh2 != 0))
                    throw new Exception("Ju lutem shenoni nivelin e tvsh te cmimit");

                taks = new clsTaksa(tvsh, idNdermarje);

                idNivelCmimi = nc.IdNivelCmimi;
                NjesiTeVarura = nc.NjesiTeVarura;
                


                if (!NjesiTeVarura)
                {
                    if (cmim1 == 0 && cmimitvsh == 0 && (cmim2 != 0 || cmimitvsh2 != 0))
                        throw new Exception("Ju lutem plotesoni vleren e cmimit");
                    if (cmim1 != 0 && cmimitvsh != 0 && cmim1 != cmimitvsh / (1 + taks.NormaPerqindje / 100))
                        throw new Exception("Cmimi me tvsh duhet te jete i barabarte me cmimin * (1+tvsh)!");
                    if (cmim2 != 0 && cmimitvsh2 != 0 && cmim2 != cmimitvsh2 / (1 + taks.NormaPerqindje / 100))
                        throw new Exception("Cmimi i dyte me tvsh duhet te jete i barabarte me cmimin e dyte * (1+tvsh)!");

                    if (cmim1 != 0 && cmimitvsh == 0)
                        cmimitvsh = cmim1 * (1 + taks.NormaPerqindje / 100);
                    else if (cmim1 == 0 && cmimitvsh != 0)
                        cmim1 = cmimitvsh / (1 + taks.NormaPerqindje / 100);

                    if (cmim2 != 0 && cmimitvsh2 == 0)
                        cmimitvsh2 = cmim2 * (1 + taks.NormaPerqindje / 100);
                    else if (cmim2 == 0 && cmimitvsh2 != 0)
                        cmim2 = cmimitvsh2 / (1 + taks.NormaPerqindje / 100);

                    if (cmimi2 == 0 && cmim1 != 0)
                        cmimi2 = cmim1 * art.KoeficientArtikulli;

                    if (cmimitvsh2 == 0 && cmimitvsh != 0)
                        cmimitvsh2 = cmimitvsh * art.KoeficientArtikulli;
                }
                else
                {
                    if (cmim1 != 0 && cmim2 != 0 && cmim2 != cmim1 * art.KoeficientArtikulli)
                        throw new Exception("Cmimi i dyte duhet te jete sa cmimi i pare shumezim koeficentin e artikullit!");

                    if (cmimitvsh != 0 && cmimitvsh2 != 0 && cmimitvsh2 != cmimitvsh * art.KoeficientArtikulli)
                        throw new Exception("Cmimi i dyte me tvsh duhet te jete sa cmimi i pare me tvsh shumezim koeficentin e artikullit!");

                    if (cmim1 != 0 && cmimitvsh != 0 && cmim1 != cmimitvsh / (1 + taks.NormaPerqindje / 100))
                        throw new Exception("Cmimi me tvsh duhet te jete i barabarte me cmimin * (1+tvsh)!");

                    if (cmim2 != 0 && cmimitvsh2 != 0 && cmim2 != cmimitvsh2 / (1 + taks.NormaPerqindje / 100))
                        throw new Exception("Cmimi i dyte me tvsh duhet te jete i barabarte me cmimin e dyte * (1+tvsh)!");


                    if (cmim2 != 0 && cmimitvsh != 0 && cmimitvsh!= (cmim2 /art.KoeficientArtikulli) * (1 + taks.NormaPerqindje / 100))
                        throw new Exception("Cmimi me tvsh duhet te jete i barabarte me cmimin * (1+tvsh)!");

                    if (cmim1 != 0 && cmimitvsh2 != 0 && cmimitvsh2 != cmim1 * art.KoeficientArtikulli * (1 + taks.NormaPerqindje / 100))
                        throw new Exception("Cmimi me tvsh duhet te jete i barabarte me cmimin * (1+tvsh)!");
                    
                    if (cmim1 != 0 && cmim2 == 0)
                    {
                        cmim2 = cmim1 * art.KoeficientArtikulli;
                        cmimitvsh = cmim1 * (1 + taks.NormaPerqindje / 100);
                        cmimitvsh2 = cmim2 * (1 + taks.NormaPerqindje / 100);
                    }
                    else if (cmim2 != 0 && cmim1 == 0)
                    {
                        cmim1 = cmim2 / art.KoeficientArtikulli;
                        cmimitvsh = cmim1 * (1 + taks.NormaPerqindje / 100);
                        cmimitvsh2 = cmim2 * (1 + taks.NormaPerqindje / 100);
                    }
                    else if (cmim1 != 0 && cmim2 != 0)
                    {
                        cmimitvsh = cmim1 * (1 + taks.NormaPerqindje / 100);
                        cmimitvsh2 = cmim2 * (1 + taks.NormaPerqindje / 100);
                    }
                    if (cmimitvsh != 0 && cmim1 == 0 && cmim2 == 0)
                    {
                        cmim1 = cmimitvsh / (1 + taks.NormaPerqindje / 100);
                        cmim2 = cmim1 * art.KoeficientArtikulli;
                        cmimitvsh2 = cmim2 * (1 + taks.NormaPerqindje / 100);
                    }
                    else if (cmimitvsh2 != 0 && cmim1 == 0 && cmim2 == 0)
                    {
                        cmim2 = cmimitvsh2 / (1 + taks.NormaPerqindje / 100);
                        cmim1 = cmim2 / art.KoeficientArtikulli;
                        cmimitvsh = cmim1 * (1 + taks.NormaPerqindje / 100);
                    }
                }
                
                DbAdmin.clsMonedha mon = new DbAdmin.clsMonedha();
                mon.mbushMonedhen(kodmonedha, idNdermarje);
                idMonedha = mon.IdMonedha;
                if (kodnjesi1 == "")
                    throw new Exception("Plotesoni njesine 1!");
                if (kodnjesi2 == "")
                    throw new Exception("Plotesoni njesine 2!");
                idNjesia = clsNjesiArtikulli.ktheIdNjesiArtikulli(kodnjesi1, idNdermarje);
                if (idNjesia == 0)
                    throw new Exception($"Njesia {kodnjesi1} nuk ekziston!");

                if (taks.IdTaksa < 1 && !String.IsNullOrEmpty(tvsh))
                    throw new Exception("Niveli i tvsh nuk ekziston!");
                decimal koeficenti = 1;
                if (kodnjesi2 != kodnjesi1)
                {
                    idNjesia2 = clsNjesiArtikulli.ktheIdNjesiArtikulli(kodnjesi2, idNdermarje);
                    koeficenti = clsArtikulli.ktheKoeficent(idArtikulli);
                }
                else
                    idNjesia2 = idNjesia;
                if (idNjesia2 == 0)
                    throw new Exception($"Njesia {kodnjesi2} nuk ekziston!");
                if (idNjesia == idNjesia2 && cmim1 != cmim2)
                    throw new Exception("Cmimi 1 dhe cmimi 2 duhet te jene te barabarte!");
                else if (nc.NjesiTeVarura && idNjesia != idNjesia2 && cmim2 != cmim1 * koeficenti)
                    throw new Exception("Cmimi 2 duhet te jete sa cmimi 1 * koeficenti!");
                if (sasimin > sasimax)
                    throw new Exception("Sasia maksimale nuk duhet te jete me e vogel se sasia minimale!");
                return new clsCmimArtikulli(0, idArtikulli, kodartikulli, idNivelCmimi, pershkrimniveli, idNjesia, kodnjesi1, idMonedha, kodmonedha, dtfillimi, dtmbarimi, sasimin, sasimax, cmim1, idPerdoruesi, idNdermarje, idKonfgAmbienti, idNjesia2, kodnjesi2, cmim2, NjesiTeVarura, kohefillimi, kohembarimi, taks.IdTaksa, cmimitvsh, cmimitvsh2, 0, 0, rm, ci);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private clsMesazh kontrolloCmim(ResourceManager rm, CultureInfo ci)
        {
           
            if (KodArtikulli == "")
                return new clsMesazh(false, "Plotesoni kodin e artikullit!");
            clsMesazh kontrollkodArtikulli = clsFunksione.kontrolloKaraktereMeMesazh(KodArtikulli, FusheKontrolli.Kodi, false);
            if (!kontrollkodArtikulli.Status)
                return kontrollkodArtikulli;
            if (kodNiveli == "")
                return new clsMesazh(false, "Plotesoni kodin e nivelit!");
            if (kodMonedha == "")
                return new clsMesazh(false, "Plotesoni kodin e monedhes!");
            if (PershkrimNjesia1 == "")
                return new clsMesazh(false, "Plotesoni njesine e pare te artikullit!");
            if (PershkrimNjesia2 == "")
                return new clsMesazh(false, "Plotesoni njesine e dyte te artikullit!");
            clsArtikulli art = new clsArtikulli(KodArtikulli, idNdermarje);    
            if (kodNiveli != "" && idNivelCmimi == 0)
                return new clsMesazh(false, "Niveli i cmimit nuk ekziston!");
            if (!DbAdmin.clsMonedha.ekziston(kodMonedha, idNdermarje))
                return new clsMesazh(false, "Monedha nuk ekziston!");
            DbAdmin.clsMonedha mon = new DbAdmin.clsMonedha();
            mon.mbushMonedhen(kodMonedha, idNdermarje);
            if (!mon.AktivMonedha)
                return new clsMesazh(false, "Monedha nuk eshte aktive!");
            if (!DbCore.DbInventari.clsNjesiArtikulli.ekzistonNjesiArtikulliMeKod(PershkrimNjesia1, idNdermarje))
            {
                return new clsMesazh(false, "Njesia e pare  nuk ekziston!");
            }
            if (!DbCore.DbInventari.clsNjesiArtikulli.ekzistonNjesiArtikulliMeKod(PershkrimNjesia2, idNdermarje))
            {
                return new clsMesazh(false, "Njesia e dyte  nuk ekziston!");
            }
            if (sasiMax < 0)
            {
                return new clsMesazh(false, "Sasia Max duhet te jete numer pozitiv!");
            }
            if (sasiMin < 0)
            {
                return new clsMesazh(false, "Sasia Min duhet te jete numer pozitiv!");
            }
            if (cmimi < 0)
            {
                return new clsMesazh(false, "Cmimi duhet te jete numer pozitiv!");
            }
            if (cmimi2 < 0)
            {
                return new clsMesazh(false, "Cmimi i dyte duhet te jete numer pozitiv!");
            }
            if (sasiMin > sasiMax)
                return new clsMesazh(false, "Sasia Min nuk mund te jete me e madhe sesa sasia Max!");
            
            if (dateFillimit > dateMbarimit && dateMbarimit != DateTime.MinValue)
                return new clsMesazh(false, "Date mbarimi duhet te jete me e madhe ose e barabarte me date fillimi!");
            else if (dateMbarimit == DateTime.MinValue)
                dateMbarimit = new DateTime(9999, 12, 31).Add(Convert.ToDateTime("0001-01-01T12:30:00").TimeOfDay);


            if (koheFillimi > KoheMbarimi)
                return new clsMesazh(false, "Koha e fillimit nuk mund te jete me e madhe se koha e mbarimit!");
            
            int idMon  = DbCore.DbInventari.clsNivelCmimi.merrIdMonedhaSipasNivelCmimi(IdNivelCmimi, idNdermarje);

            if (idMon != idMonedha)
                return new clsMesazh(false, "Monedha nuk i perket nivelit te cmimit te zgjedhur!");
            if (art.Njesi1Artikulli != idNjesia)
                return new clsMesazh(false, "Njesia e pare nuk i perket ketij artikulli!");
            if (art.Njesi2Artikulli != idNjesia2)
                return new clsMesazh(false, "Njesia e dyte nuk i perket ketij artikulli!");
            return new clsMesazh(true, "Kontrollet e cmimeve u kaluan me sukses");
        }


        public clsMesazh transfero(colCmimeArtikujsh cmimet, List<object> idndermarje, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh();
            bool sukses = true;
            for (int i = 0; i < cmimet.Count; i++)
            {

                foreach (object id in idndermarje)
                {
                    clsDatabaseInventari db = new clsDatabaseInventari();
                    db.beginTransaksion();
                    mesazh = kontrollotransferim(cmimet[i], int.Parse(id.ToString()), db, idperdoruesi);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        sukses = false;
                    }
                    db.commitTransaksion();
                }
            }
            if (sukses) { mesazh.PershkrimMesazhi = "Transferimi mbaroi me sukses!"; mesazh.Status = true; }
            else { mesazh.Status = false; }
            return mesazh;
        }
        public static clsMesazh pergatiCmim(clsCmimArtikulli cmim, int idndermarje, clsDatabaseInventari db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            clsNjesiArtikulli njesi1 = new clsNjesiArtikulli(cmim.idNjesia, db);
            mesazh = njesi1.kontrollotransferim(njesi1, idndermarje, db, idperdoruesi);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            if (cmim.idNjesia == cmim.idNjesia2)
            {
                cmim.idNjesia = njesi1.IdNjesia;
                cmim.idNjesia2 = njesi1.IdNjesia;

            }
            else
            {
                cmim.idNjesia = njesi1.IdNjesia;
                clsNjesiArtikulli njesi2 = new clsNjesiArtikulli(cmim.idNjesia2, db);
                mesazh = njesi2.kontrollotransferim(njesi2, idndermarje, db, idperdoruesi);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                cmim.idNjesia2 = njesi2.IdNjesia;
            }
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db);
            clsMonedha mon = new clsMonedha(cmim.idMonedha, dbadm);
            mesazh = mon.kontrollotransferim(mon, idndermarje, dbadm, idperdoruesi);
            if (!mesazh.Status)
            {
                return mesazh;
            }

            cmim.idMonedha = mon.IdMonedha;

            clsNivelCmimi nivel = new clsNivelCmimi(cmim.idNivelCmimi, db);
            mesazh = nivel.kontrollotransferim(nivel, idndermarje, db, idperdoruesi);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            cmim.idNivelCmimi = nivel.IdNivelCmimi;

            cmim.idNdermarje = idndermarje;
            cmim.idPerdoruesi = idperdoruesi;
            DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
            DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(db );

            konf.mbushKonfigAmbjSipasKod("CSH", idndermarje, dbshare);

            cmim.idKonfig = konf.IdKonfigAmbjente;

            return mesazh;
        }
        public clsMesazh kontrollotransferim(clsCmimArtikulli cmim, int idndermarje, clsDatabaseInventari db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");

            mesazh = pergatiCmim(cmim, idndermarje, db, idperdoruesi);

            clsArtikulli art = new clsArtikulli(cmim.idArtikulli, db);
            if (!db.ekzistonArtikull(art.KodArtikulli, idndermarje))
            {
                return new clsMesazh(false, "Transferoni artikujt perpara cmimeve!");
            }
            art.merrSipasKodArtikullit(art.KodArtikulli, idndermarje, db);
            cmim.idArtikulli = art.IdArtikulli;

            if (!mesazh.Status)
            {
                return mesazh;
            }
            if (!db.ekzistonCmimArtikulli(cmim.IdArtikulli, cmim.idNivelCmimi, out cmim.idCmimArtikulli))
            {
                mesazh = cmim.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;
            }
            else
            {

                mesazh = cmim.modifiko(db);
                if (!mesazh.Status)
                    return mesazh;
            }
            return mesazh;
        }

        /// <summary>
        /// Ruan objektin cmimin e artikullit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajCmimArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>    
        public clsMesazh ruaj()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            int id;
            clsMesazh u_ruajt = data.ruajCmimArtikulli(out id, this.IdArtikulli, this.IdNivelCmimi, this.IdNjesia, this.IdMonedha, this.DateFillimi,
                this.DateMbarimi, this.SasiMin, this.SasiMax, this.Cmimi, this.IdPerdoruesi, this.IdNdermarje, this.IdKonfig, this.idStatusDok, this.idNjesia2, this.cmimi2, this.koheFillimi,this.KoheMbarimi,this.IdDetajim);//, this.formula
            data.Dispose();
            //clsMesazh u_ruajt = data.ruajCmimArtikulli(this);
            return u_ruajt;
        }

        public clsMesazh ruaj(clsDatabaseInventari data)
        {

            int id;
            clsMesazh u_ruajt = data.ruajCmimArtikulli(out id, this.IdArtikulli, this.IdNivelCmimi, this.IdNjesia, this.IdMonedha, this.DateFillimi,
                this.DateMbarimi, this.SasiMin, this.SasiMax, this.Cmimi, this.IdPerdoruesi, this.IdNdermarje, this.IdKonfig, this.idStatusDok, this.idNjesia2, this.cmimi2, this.koheFillimi,this.koheMbarimi, this.idDetajim);//, this.formula

            //clsMesazh u_ruajt = data.ruajCmimArtikulli(this);
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin cmimin e artikullit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoCmimArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>    
        public clsMesazh modifiko()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_modifikua = data.modifikoCmimArtikulli(this.IdCmimArtikulli, this.IdArtikulli, this.IdNivelCmimi, this.IdNjesia, this.IdMonedha, this.DateFillimi,
                this.DateMbarimi, this.SasiMin, this.SasiMax, this.Cmimi, this.IdPerdoruesi, this.IdNdermarje, this.IdKonfig, this.idStatusDok, this.idNjesia2, this.cmimi2, this.koheFillimi,this.koheMbarimi, this.idDetajim);//, this.Formula
            data.Dispose();
            //clsMesazh u_modifikua = data.modifikoCmimArtikulli(this);
            return u_modifikua;
        }

        public clsMesazh modifiko(clsDatabaseInventari data)
        {
            data.hidhCmimArtikulliNeHistorik(IdCmimArtikulli);


            clsMesazh u_modifikua = data.modifikoCmimArtikulli(this.IdCmimArtikulli, this.IdArtikulli, this.IdNivelCmimi, this.IdNjesia, this.IdMonedha, this.DateFillimi,
                this.DateMbarimi, this.SasiMin, this.SasiMax, this.Cmimi, this.IdPerdoruesi, this.IdNdermarje, this.IdKonfig, this.idStatusDok, this.idNjesia2, this.cmimi2, this.koheFillimi,this.koheMbarimi, this.idDetajim);//, this.Formula

            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin cmimin e artikullit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiCmimArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiCmimArtikulliStatus(this.IdCmimArtikulli, this.idPerdoruesi);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiCmimArtikulli(this);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin cmimin e artikullit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ktheCmimArtikulli"/> 
        /// </summary>
        public void merr()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.ktheCmimArtikulli(this.idCmimArtikulli);
            data.Dispose();
            //data.merrCmimArtikulli(this);
        }

        /// <summary>
        /// perdoret per te ruajtur cmimet e artikujve. Perdoret nje transaksion ne menyre qe te ruhen te gjitha cmimet.
        /// Nqs ekziston nje cmim per kete artikull atehere modifikohet cmimi i tij perndryshe ruhet artikulli me cmimin e ri
        /// <param name="dt"> koleksioni me cmimet e artikujve qe do te ruhen</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        public static clsMesazh ruajCmim(colCmimeArtikujsh cmimeArtikujsh)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            clsMesazh mesazh;
            try
            {
                dbInventar.beginTransaksion();
                foreach (clsCmimArtikulli c in cmimeArtikujsh)
                {

                    if (!dbInventar.ekzistonCmimArtikulli(c.IdArtikulli, c.idNivelCmimi, out c.idCmimArtikulli))
                    {
                        int idC;
                        mesazh = dbInventar.ruajCmimArtikulli(out idC, c.IdArtikulli, c.IdNivelCmimi, c.IdNjesia, c.IdMonedha, c.DateFillimi, c.DateMbarimi, c.SasiMin,
                            c.SasiMax, c.Cmimi, c.IdPerdoruesi, c.IdNdermarje, c.IdKonfig, c.idStatusDok, c.idNjesia2, c.cmimi2,c.koheFillimi,c.koheMbarimi,c.idDetajim );//, c.formula
                        if (!mesazh.Status)
                        {
                            dbInventar.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                    else
                    {
                        mesazh = dbInventar.modifikoCmimArtikulli(c.IdCmimArtikulli, c.IdArtikulli, c.IdNivelCmimi, c.IdNjesia, c.IdMonedha, c.DateFillimi, c.DateMbarimi, c.SasiMin,
                            c.SasiMax, c.Cmimi, c.IdPerdoruesi, c.IdNdermarje, c.IdKonfig, c.idStatusDok, c.idNjesia2, c.cmimi2, c.koheFillimi,c.koheMbarimi, c.idDetajim);//, c.formula
                        if (!mesazh.Status)
                        {
                            dbInventar.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }
                dbInventar.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbInventar.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsCmimArtikulli krijoCmim(Dictionary<string, object> rresht)
        {
            IdCmimArtikulli = int.Parse(rresht["IdCmimArtikulli"].ToString());
            this.Cmimi = decimal.Parse(rresht["Cmimi"].ToString());
            this.Cmimi2 = decimal.Parse(rresht["Cmimi2"].ToString());
            this.DateFillimi = DateTime.Parse(rresht["DateFillimi"].ToString());
            this.DateMbarimi = DateTime.Parse(rresht["DateMbarimi"].ToString());
            this.IdArtikulli = int.Parse(rresht["IdArtikulli"].ToString());
            this.IdKonfig = int.Parse(rresht["IdKonfig"].ToString());
            this.IdMonedha = int.Parse(rresht["IdMonedha"].ToString());
            this.IdNdermarje = int.Parse(rresht["IdNdermarje"].ToString());
            //this.IdNderViti = int.Parse(rresht["IdNderViti"].ToString());
            this.IdNivelCmimi = int.Parse(rresht["IdNivelCmimi"].ToString());
            this.IdNjesia = int.Parse(rresht["IdNjesia"].ToString());
            this.IdNjesia2 = int.Parse(rresht["IdNjesia2"].ToString());
            this.IdPerdoruesi = int.Parse(rresht["IdPerdoruesi"].ToString());
            this.IdStatusDok = int.Parse(rresht["IdStatusDok"].ToString());
            this.NjesiTeVarura = bool.Parse(rresht["NjesiTeVarura"].ToString());
            this.SasiMax = decimal.Parse(rresht["SasiMax"].ToString());
            this.SasiMin = decimal.Parse(rresht["SasiMin"].ToString());
            this.koheFillimi = DateTime.Parse(rresht["KoheFillimi"].ToString());
            this.koheMbarimi = DateTime.Parse(rresht["KoheMbarimi"].ToString());
            this.idTvsh = int.Parse(rresht["IdTvsh"].ToString());
            this.CmimiTvsh = decimal.Parse(rresht["CmimiTvsh"].ToString());
            this.Cmimi2Tvsh = decimal.Parse(rresht["Cmimi2Tvsh"].ToString());
            //this.Formula = rresht["Formula"].ToString();
            return this;
        }

        #endregion

        #region Metoda Internal

        public void mbushCmimArtikulli(IDataRecord dbDataRowCmimArtikulli)
        {


            try
            {
                int.TryParse(dbDataRowCmimArtikulli["IDCMIMARTIKULLI"].ToString(), out idCmimArtikulli);
                if (idCmimArtikulli == 0)
                    idCmimArtikulli = IdAutomatike--;
                int.TryParse(dbDataRowCmimArtikulli["IDARTIKULLI"].ToString(), out idArtikulli);
                int.TryParse(dbDataRowCmimArtikulli["IDNIVELCMIMI"].ToString(), out idNivelCmimi);
                int.TryParse(dbDataRowCmimArtikulli["IDNJESIA"].ToString(), out idNjesia);
                int.TryParse(dbDataRowCmimArtikulli["IDNJESIA2"].ToString(), out idNjesia2);
                int.TryParse(dbDataRowCmimArtikulli["IDMONEDHA"].ToString(), out idMonedha);
                int.TryParse(dbDataRowCmimArtikulli["Idkonfig"].ToString(), out idKonfig);
                int.TryParse(dbDataRowCmimArtikulli["Idstatusdok"].ToString(), out idStatusDok);
                DateTime.TryParse(dbDataRowCmimArtikulli["DateFillimi"].ToString(), out dateFillimit);
                DateTime.TryParse(dbDataRowCmimArtikulli["DateMbarimi"].ToString(), out dateMbarimit);
                decimal.TryParse(dbDataRowCmimArtikulli["SASIMIN"].ToString(), out sasiMin);
                decimal.TryParse(dbDataRowCmimArtikulli["SASIMAX"].ToString(), out sasiMax);
                decimal.TryParse(dbDataRowCmimArtikulli["CMIMI"].ToString(), out cmimi);
                decimal.TryParse(dbDataRowCmimArtikulli["CMIMI2"].ToString(), out cmimi2);
                DateTime.TryParse(dbDataRowCmimArtikulli["Kohefillimi"].ToString(), out koheFillimi);
                DateTime.TryParse(dbDataRowCmimArtikulli["Kohembarimi"].ToString(), out koheMbarimi);
                int.TryParse(dbDataRowCmimArtikulli["IDPERDORUESI"].ToString(), out idPerdoruesi);
                //int.TryParse(dbDataRowCmimArtikulli["IDNDERVITI"].ToString(), out idNderViti);
                int.TryParse(dbDataRowCmimArtikulli["IDNDERMARJE"].ToString(), out idNdermarje);
                int.TryParse(dbDataRowCmimArtikulli["IDKONFIG"].ToString(), out idKonfig);
                int.TryParse(dbDataRowCmimArtikulli["IDSTATUSDOK"].ToString(), out idStatusDok);
                DateTime.TryParse(dbDataRowCmimArtikulli["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowCmimArtikulli["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                KodbarArtikulli = dbDataRowCmimArtikulli["KodbarArtikulli"].ToString();
                EmerArtikulli1 = dbDataRowCmimArtikulli["EmerArtikulli1"].ToString();
                EmerArtikulli2 = dbDataRowCmimArtikulli["EmerArtikulli2"].ToString();
                KodifikimArtikulli1 = dbDataRowCmimArtikulli["KodifikimArtikulli1"].ToString();
                KodifikimArtikulli2 = dbDataRowCmimArtikulli["KodifikimArtikulli2"].ToString();
                NrLlogariFurnitori = dbDataRowCmimArtikulli["NrLlogariFurnitori"].ToString();
                KodArtikulli = dbDataRowCmimArtikulli["KodArtikulli"].ToString();
                double.TryParse(dbDataRowCmimArtikulli["Kosto"].ToString(), out kosto);
                decimal.TryParse(dbDataRowCmimArtikulli["Gjendje"].ToString(), out gjendje);
                decimal.TryParse(dbDataRowCmimArtikulli["Koeficent"].ToString(), out koeficent);
                //   double.TryParse(dbDataRowCmimArtikulli["Kursi"].ToString(), out kursi); 
                decimal.TryParse(dbDataRowCmimArtikulli["norme"].ToString(), out norme);
                NjesiTeVarura = bool.Parse(dbDataRowCmimArtikulli["NjesiTeVarura"].ToString());
                PershkrimNjesia1 = dbDataRowCmimArtikulli["PershkrimNjesia1"].ToString();
                PershkrimNjesia2 = dbDataRowCmimArtikulli["PershkrimNjesia2"].ToString();
                int.TryParse(dbDataRowCmimArtikulli["idtvsh"].ToString(), out idTvsh);
                decimal.TryParse(dbDataRowCmimArtikulli["cmimitvsh"].ToString(), out cmimiTvsh);
                decimal.TryParse(dbDataRowCmimArtikulli["cmimi2tvsh"].ToString(), out cmimi2Tvsh);
                EmertimiKF = dbDataRowCmimArtikulli["EmertimiKF"].ToString();
                PershkrimGrup1 = dbDataRowCmimArtikulli["PershkrimGrup1"].ToString();
                PershkrimGrup2 = dbDataRowCmimArtikulli["PershkrimGrup2"].ToString();
                PershkrimGrup3 = dbDataRowCmimArtikulli["PershkrimGrup3"].ToString();
                int.TryParse(dbDataRowCmimArtikulli["IdDetajim"].ToString(), out idDetajim);
                int.TryParse(dbDataRowCmimArtikulli["IdCmimRetail"].ToString(), out idCmimRetail);
            }

            catch (Exception ex)
            {
                throw new MyException($"gabim ne mbushjen e cmimit per artikullin me idcmimartikull {idCmimArtikulli} per artikullin {IdArtikulli},errori : {ex.Message}");
            }

        }

        public static clsCmimArtikulli Krijo(IDataRecord record)
        {
            clsCmimArtikulli cmimArtikulli = new clsCmimArtikulli();
            cmimArtikulli.mbushCmimArtikulli(record);
            return cmimArtikulli;
        }

        internal bool mbushCmimArtikulli(DataRow dbDataRowCmimArtikulli)
        {
            if (dbDataRowCmimArtikulli != null)
            {

                try
                {
                    int.TryParse(dbDataRowCmimArtikulli["IDCMIMARTIKULLI"].ToString(), out idCmimArtikulli);
                    int.TryParse(dbDataRowCmimArtikulli["IDARTIKULLI"].ToString(), out idArtikulli);
                    int.TryParse(dbDataRowCmimArtikulli["IDNIVELCMIMI"].ToString(), out idNivelCmimi);
                    int.TryParse(dbDataRowCmimArtikulli["IDNJESIA"].ToString(), out idNjesia);
                    int.TryParse(dbDataRowCmimArtikulli["IDNJESIA2"].ToString(), out idNjesia2);
                    int.TryParse(dbDataRowCmimArtikulli["IDMONEDHA"].ToString(), out idMonedha);

                    DateTime.TryParse(dbDataRowCmimArtikulli["DTFILLIMIT"].ToString(), out dateFillimit);
                    DateTime.TryParse(dbDataRowCmimArtikulli["DTMBARIMIT"].ToString(), out dateMbarimit);
                    decimal.TryParse(dbDataRowCmimArtikulli["SASIMIN"].ToString(), out sasiMin);
                    decimal.TryParse(dbDataRowCmimArtikulli["SASIMAX"].ToString(), out sasiMax);
                    decimal.TryParse(dbDataRowCmimArtikulli["CMIMI"].ToString(), out cmimi);
                    decimal.TryParse(dbDataRowCmimArtikulli["CMIMI2"].ToString(), out cmimi2);
                    DateTime.TryParse(dbDataRowCmimArtikulli["Kohefillimi"].ToString(), out koheFillimi);
                    DateTime.TryParse(dbDataRowCmimArtikulli["Kohembarimi"].ToString(), out koheMbarimi);
                    int.TryParse(dbDataRowCmimArtikulli["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    //int.TryParse(dbDataRowCmimArtikulli["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowCmimArtikulli["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowCmimArtikulli["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowCmimArtikulli["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowCmimArtikulli["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowCmimArtikulli["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    KodbarArtikulli = dbDataRowCmimArtikulli["kodbari"].ToString();
                    EmerArtikulli1 = dbDataRowCmimArtikulli["PERSHKRIMARTIKULLI"].ToString();
                    EmerArtikulli2 = dbDataRowCmimArtikulli["PERSHKRIMIANGARTIKULLI"].ToString();
                    KodifikimArtikulli1 = dbDataRowCmimArtikulli["KODIFIKIMI1ARTIKULLI"].ToString();
                    KodifikimArtikulli2 = dbDataRowCmimArtikulli["KODIFIKIMI2ARTIKULLI"].ToString();
                    NrLlogariFurnitori = dbDataRowCmimArtikulli["furnitori"].ToString();
                    KodArtikulli = dbDataRowCmimArtikulli["KODARTIKULLI"].ToString();
                    NjesiTeVarura = bool.Parse(dbDataRowCmimArtikulli["NjesiTeVarura"].ToString());
                    int.TryParse(dbDataRowCmimArtikulli["idtvsh"].ToString(), out idTvsh);
                    decimal.TryParse(dbDataRowCmimArtikulli["cmimitvsh"].ToString(), out cmimiTvsh);
                    decimal.TryParse(dbDataRowCmimArtikulli["cmimi2tvsh"].ToString(), out cmimi2Tvsh);
                    decimal.TryParse(dbDataRowCmimArtikulli["norme"].ToString(), out norme);
                    int.TryParse(dbDataRowCmimArtikulli["IdDetajim"].ToString(), out idDetajim);
                    int.TryParse(dbDataRowCmimArtikulli["IdCmimRetail"].ToString(), out idCmimRetail);
                    //formula = dbDataRowCmimArtikulli["Formula"].ToString();                    
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new MyException("ERROR: Gabim gjate cast-it!");
                }
                catch (Exception)
                {
                    throw new MyException("ERROR: Gabim gjate marrjes se cmimit te artikullit nga db-ja");
                }
            }
            return false;

        }

        internal bool mbushCmimArtikulliMeKosto(DataRow dbDataRowCmimArtikulli)
        {
            if (dbDataRowCmimArtikulli != null)
            {
                try
                {
                    int.TryParse(dbDataRowCmimArtikulli["IDCMIMARTIKULLI"].ToString(), out idCmimArtikulli);
                    int.TryParse(dbDataRowCmimArtikulli["IDARTIKULLI"].ToString(), out idArtikulli);
                    int.TryParse(dbDataRowCmimArtikulli["IDNIVELCMIMI"].ToString(), out idNivelCmimi);
                    int.TryParse(dbDataRowCmimArtikulli["IDNJESIA"].ToString(), out idNjesia);
                    int.TryParse(dbDataRowCmimArtikulli["IDNJESIA2"].ToString(), out idNjesia2);
                    int.TryParse(dbDataRowCmimArtikulli["IDMONEDHA"].ToString(), out idMonedha);
                    DateTime.TryParse(dbDataRowCmimArtikulli["DTFILLIMIT"].ToString(), out dateFillimit);
                    DateTime.TryParse(dbDataRowCmimArtikulli["DTMBARIMIT"].ToString(), out dateMbarimit);
                    dateFillimit = dateFillimit.Date;//te ike pjesa e ores sepse nuk duhet
                    dateMbarimit = dateMbarimit.Date;
                    DateTime.TryParse(dbDataRowCmimArtikulli["Kohefillimi"].ToString(), out koheFillimi);
                    DateTime.TryParse(dbDataRowCmimArtikulli["Kohembarimi"].ToString(), out koheMbarimi);
                    decimal.TryParse(dbDataRowCmimArtikulli["SASIMIN"].ToString(), out sasiMin);
                    decimal.TryParse(dbDataRowCmimArtikulli["SASIMAX"].ToString(), out sasiMax);
                    decimal.TryParse(dbDataRowCmimArtikulli["CMIMI"].ToString(), out cmimi);
                    decimal.TryParse(dbDataRowCmimArtikulli["CMIMI2"].ToString(), out cmimi2);
                    int.TryParse(dbDataRowCmimArtikulli["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowCmimArtikulli["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowCmimArtikulli["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowCmimArtikulli["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowCmimArtikulli["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowCmimArtikulli["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    KodbarArtikulli = dbDataRowCmimArtikulli["kodbari"].ToString();
                    EmerArtikulli1 = dbDataRowCmimArtikulli["PERSHKRIMARTIKULLI"].ToString();
                    EmerArtikulli2 = dbDataRowCmimArtikulli["PERSHKRIMIANGARTIKULLI"].ToString();
                    KodifikimArtikulli1 = dbDataRowCmimArtikulli["KODIFIKIMI1ARTIKULLI"].ToString();
                    KodifikimArtikulli2 = dbDataRowCmimArtikulli["KODIFIKIMI2ARTIKULLI"].ToString();
                    NrLlogariFurnitori = dbDataRowCmimArtikulli["furnitori"].ToString();
                    KodArtikulli = dbDataRowCmimArtikulli["KODARTIKULLI"].ToString();
                    NjesiTeVarura = bool.Parse(dbDataRowCmimArtikulli["NjesiTeVarura"].ToString());
                    double.TryParse(dbDataRowCmimArtikulli["Kosto"].ToString(), out kosto);
                    double.TryParse(dbDataRowCmimArtikulli["Kursi"].ToString(), out kursi);
                    int.TryParse(dbDataRowCmimArtikulli["idtvsh"].ToString(), out idTvsh);
                    decimal.TryParse(dbDataRowCmimArtikulli["cmimitvsh"].ToString(), out cmimiTvsh);
                    decimal.TryParse(dbDataRowCmimArtikulli["cmimi2tvsh"].ToString(), out cmimi2Tvsh);
                    decimal.TryParse(dbDataRowCmimArtikulli["norme"].ToString(), out norme);
                    int.TryParse(dbDataRowCmimArtikulli["IdDetajim"].ToString(), out idDetajim);
                    int.TryParse(dbDataRowCmimArtikulli["IdCmimRetail"].ToString(), out idCmimRetail);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new MyException( $"Gabim ne mbushje te nivelit te cmimit m id {IdCmimArtikulli} dhe idArtikulli {idArtikulli} Ex :{ex.Message}");
                }
            }
            return false;
        }

        #endregion

   
    }
}