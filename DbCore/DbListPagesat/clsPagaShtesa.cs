using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;

namespace DbCore.DbListPagesat
{
    /// <summary>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje paga dhe shtesa punonjesi
    ///  (Te dhenat  merren nga tabela : T_PAGASHTESA)
    /// </summary>
    public class clsPagaShtesa
    {
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se paga dhe shtesa nga db-ja";
        public const string FormatiDates = "dd/MM/yyyy";
        public const string RrumbullakimiDecimal = "n4";
        #region Atribute

        private int idPagaShtesa;
        private int idPunonjes;
        private int idKomponente;
        private DateTime dtAktivizimi;
        private decimal vleraParam;
        private decimal vlera;
        private string komponente;
        private int njesia;
        private string emerparam;
        private int idPerdoruesi;
        private clsKomponentePage komponentePage;
        private DataRow rreshti;
        private string nrPersonalPunonjesi;
        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idpagashtesa">Id e PagaShtesas se sigurimit</param>
        /// <param name="idpunonjes">Id e punonjesit</param>
        /// <param name="idkomponente">Id e sigurimit</param>
        /// <param name="dtaktivizimi">data e aktivizimit</param>
        /// <param name="vleraparam">vlera e parametrit</param>
        /// <param name="vlera"> vlera</param>
        public clsPagaShtesa(int idpagashtesa, int idpunonjes, int idkomponente, DateTime dtaktivizimi, decimal vleraparam, decimal vlera, int idperdoruesi)
        {
            idPagaShtesa = idpagashtesa;
            idPunonjes = idpunonjes;
            idKomponente = idkomponente;
            dtAktivizimi = dtaktivizimi;
            vleraParam = vleraparam;
            this.vlera = vlera;
            idPerdoruesi = idperdoruesi;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsPagaShtesa()
        {
        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// </summary>
        /// <param name="idPagaShtesa">id pagashtesa</param>
        public clsPagaShtesa(int idPagaShtesa)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            db.kthePagaShtesa(idPagaShtesa, this);
            db.Dispose();
        }

        //public clsPagaShtesa(DataRow rreshti)
        //{
        //    
        //    mbushPagaShtesa(rreshti);
        //}
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdPagaShtesa
        {
            get { return idPagaShtesa; }
            set { idPagaShtesa = value; }
        }
        /// <summary>
        /// perdoruesi qe ka kryer veprimin
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
        /// Kthen/Vendos ID-ne e punonjesit
        /// </summary>
        public int IdPunonjes
        {
            get { return idPunonjes; }
            set { idPunonjes = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e komponentes se pages
        /// </summary>
        public int IdKomponentePage
        {
            get
            {
                return idKomponente;
            }
            set
            {
                idKomponente = value;
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

        /// <summary>
        /// kthen/vendos vleren e parametrit
        /// </summary>
        public decimal VleraParam
        {
            get
            {
                return vleraParam;
            }
            set
            {
                vleraParam = value;
            }
        }

        /// <summary>
        /// kthen/vendos vleren
        /// </summary>
        public decimal Vlera
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
        /// emri i komponentes
        /// </summary>
        public string Komponente
        {
            get
            {
                return komponente;
            }
        }
        /// <summary>
        /// njesia
        /// </summary>
        public int Njesia
        {
            get
            {
                return njesia;
            }
        }
        /// <summary>
        /// emer parametri
        /// </summary>
        public string Emerparam
        {
            get
            {
                return emerparam;
            }
        }

        public clsKomponentePage KomponentePage
        {
            get { return komponentePage; }
            set { komponentePage = value; }
        }

        public string NrPersonalPunonjesi => nrPersonalPunonjesi;


        #endregion

        #region Metoda Publike
        private static string zevendesoParameter(string formula, string parameter, string vlera)
        {
            string pattern = String.Format(@"\b{0}\b", parameter);
            Regex rgx = new Regex(pattern);
            formula = rgx.Replace(formula, vlera);
            return formula;
        }
        private static void formula(colKomponentePage col, string param, string vlera)
        {
            for (int i = 0; i < col.Count; i++)
            {
                if (col[i].Njesi != 0)
                {
                    col[i].Formula = zevendesoParameter(col[i].Formula, param, vlera);
                }
            }

        }
        public clsMesazh ruaj()
        {
            int id;
            using (var data = new clsDatabazeListPagesa())
            {
                clsMesazh u_ruajt = data.ruajPagaShtesa(out id, idPunonjes, idKomponente, dtAktivizimi, vleraParam, vlera, idPerdoruesi);
                this.IdPagaShtesa = id;

                return u_ruajt;
            }
        }
        /// <summary>
        /// Ruan objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.ruajPagaShtesa"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj(int idndermarje)
        {
            clsKomponentePage kompon = new clsKomponentePage(idKomponente);
            clsPunonjes pun = new clsPunonjes(idPunonjes);
            bool ekziston = false;
            colPagaShtesa colpaga = new colPagaShtesa(idPunonjes, dtAktivizimi, idKomponente);
            colBankaPunonjes bankavjeter = new colBankaPunonjes(idPunonjes);
            if (colpaga.Count > 0)///ekziston komponentja per kete date per kete punonjes
            {
                ekziston = true;
                colpaga = new colPagaShtesa(idPunonjes, dtAktivizimi);
            }
            else
            {
                colpaga = colPagaShtesa.merrPagaShtesaSipasPunonjesiDheDatesMeTeFundit(dtAktivizimi, idPunonjes);
                if (colpaga.Count == 0)
                    colpaga = new colPagaShtesa(false, dtAktivizimi, idndermarje);
            }
            colpaga.Find(x => x.komponente == kompon.Pershkrimi).Vlera = vlera;
            colKomponentePage colkom = new colKomponentePage();
            for (int i = 0; i < colpaga.Count; i++)
            {
                clsKomponentePage komp = new clsKomponentePage(colpaga[i].IdKomponentePage);
                if (komp.Njesi == 2 && komp.ParamKodi != "")
                {
                    komp.Formula = zevendesoParameter(komp.Formula, komp.ParamKodi, colpaga[i].VleraParam.ToString());
                }
                colkom.Add(komp);
            }
            for (int i = 0; i < colkom.Count; i++)
            {
                if (colkom[i].Njesi != 2)
                    formula(colkom, colkom[i].Kodi, colpaga[i].Vlera.ToString());
                else formula(colkom, colkom[i].Kodi, String.Format("({0})", colkom[i].Formula));
            }
            if (pun.LlojPagese == 1)
                formula(colkom, "LP", "1");
            else if (pun.LlojPagese == 2)
            {
                clsKomponentePage komp = new clsKomponentePage("DM", idndermarje, dtAktivizimi);
                formula(colkom, "LP", komp.Formula);
            }
            else
            {
                clsKomponentePage komp = new clsKomponentePage("OD", idndermarje, dtAktivizimi);
                clsKomponentePage kompdita = new clsKomponentePage("DM", idndermarje, dtAktivizimi);
                formula(colkom, "LP", String.Format("{0}*{1}", komp.Formula, kompdita.Formula));
            }
            using (var myScope = new MyTransactionScope())
            {
                var data = new clsDatabazeListPagesa();
                clsMesazh mes = new clsMesazh();
                for (int i = 0; i < colpaga.Count; i++)
                {
                    var kompVjeter = colpaga[i];
                    if (colkom[i].Formula != "")
                    {
                        DataTable dt = new DataTable();
                        decimal v = decimal.Parse(dt.Compute(colkom[i].Formula, "").ToString());
                        kompVjeter.vlera = v;
                    }
                    if (ekziston)
                    {
                        mes = data.modifikoPagaShtesa(kompVjeter.idPagaShtesa, kompVjeter.idPunonjes, kompVjeter.idKomponente, kompVjeter.dtAktivizimi, kompVjeter.vleraParam, kompVjeter.vlera, kompVjeter.idPerdoruesi);
                        if (!mes) return mes;
                        int idLog;

                        mes = data.ruajLogPunonjes(out idLog, IdPunonjes, IdPerdoruesi, 1, $"modifikim komponente page nga importi per punonjesin me numer personal {NrPersonalPunonjesi} dhe dtAktivizimi : {this.DtAktivizimi} ", $"Komponente :{kompVjeter.Komponente}, Vlera ishte :{kompVjeter.Vlera}, u be :{this.Vlera}, VleraParam ishte {kompVjeter.VleraParam}, u be :{this.VleraParam}");
                        if (!mes)  return mes;
                        
                    }
                    else
                    {
                        int id;
                        mes = data.ruajPagaShtesa(out id, idPunonjes, kompVjeter.idKomponente, dtAktivizimi, kompVjeter.vleraParam, kompVjeter.vlera, idPerdoruesi);
                        if (!mes) return mes;
                        
                        
                    }
                }
                int idLoga;
                mes = data.ruajLogPunonjes(out idLoga, IdPunonjes, IdPerdoruesi, 1, $"shtim komponente page nga importi per punonjesin me numer personal {NrPersonalPunonjesi} dhe dtAktivizimi : {this.DtAktivizimi} ", $"Komponente :{komponente}, Vlera :{this.Vlera}, VleraParam :{this.VleraParam}");
                if (!mes) return mes;
                if (!ekziston)
                {
                    int id;
                    if (bankavjeter.Count > 0)
                    {
                        mes = data.ruajBankaPunonjes(out id, idPunonjes, bankavjeter[0].LlogBankare, bankavjeter[0].IdBanka, bankavjeter[0].LimitTel, bankavjeter[0].LimitInternet, dtAktivizimi, idPerdoruesi);
                        if (!mes) return mes;

                    }
                    else
                    {
                        mes = data.ruajBankaPunonjes(out id, idPunonjes, "", 0, 0, 0, dtAktivizimi, idPerdoruesi);
                        if (!mes) return mes;
                    }
                }
                myScope.Complete();

                return mes;
            }
        }

        public clsPagaShtesa Clone()
        {
            return (clsPagaShtesa)MemberwiseClone();
        }

        public clsPagaShtesa krijoPerImport(string kodi, string emer, string mbiemer, DateTime data, decimal totali, int idperdoruesi, int idndermarje, string komponente, int vitinderm)
        {
            try
            {
                clsPunonjes pun = new clsPunonjes(kodi, idndermarje);
                idPunonjes = pun.IdPunonjes;
                if (pun.IdPunonjes <= 0)
                    throw new Exception("Punonjesi nuk ekziston!");
                if (emer != "" && pun.Emer != emer)
                    throw new Exception("Emri i punonjesit nuk eshte i sakte!");
                if (mbiemer != "" && pun.Mbiemer != mbiemer)
                    throw new Exception("Mbiemri i punonjesit nuk eshte i sakte!");

                //if (data.Year != vitinderm)
                //    throw new Exception("Viti i pages dhe shtesa duhet ti perkase vitit ushtrimor!");
                clsKomponentePage komp = new clsKomponentePage(komponente, idndermarje, data);
                if (komp.IdKomponentePage <= 0)
                    throw new Exception("Komponentja nuk ekziston!");


                return new clsPagaShtesa(0, idPunonjes, komp.IdKomponentePage, data, 0, totali, idperdoruesi)
                {
                    komponente = komp.Kodi,
                    nrPersonalPunonjesi = kodi
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Modifikon objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.modifikoPagaShtesa"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko(clsDatabazeListPagesa data)
        {
            clsMesazh u_modifikua = data.modifikoPagaShtesa(idPagaShtesa, idPunonjes, idKomponente, dtAktivizimi, vleraParam, vlera, idPerdoruesi);
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.fshiPagaShtesa"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshi(clsDatabazeListPagesa data)
        {
            clsMesazh u_fshi = data.fshiPagaShtesa(idPagaShtesa);
            return u_fshi;
        }

        internal static string kontrolloPaga(colPagaShtesa oColPagaShtesa, colPagaShtesa pagavjeter, out string fushatmod, out bool modifikuar, clsDatabazeListPagesa db)
        {
            string mesazh = "U modifikuan fushat per pagat dhe shtesat:";
            fushatmod = "U modifikuan fushat per pagat dhe shtesat:";
            modifikuar = false;
            if (oColPagaShtesa[0].DtAktivizimi.ToString(FormatiDates) != pagavjeter[0].DtAktivizimi.ToString(FormatiDates))
            {
                mesazh += " Data e aktivizimit,";
                fushatmod += String.Format(" Data e aktivizimit nga {0} ne {1},", pagavjeter[0].DtAktivizimi.ToString(FormatiDates), oColPagaShtesa[0].DtAktivizimi.ToString(FormatiDates));
            }
            for (int i = 0; i < oColPagaShtesa.Count; i++)
            {
                clsPagaShtesa pagvj = pagavjeter.Find(x => x.IdKomponentePage == oColPagaShtesa[i].IdKomponentePage);
                if (pagvj != null)
                {
                    if (oColPagaShtesa[i].Vlera.ToString(RrumbullakimiDecimal) != pagvj.Vlera.ToString(RrumbullakimiDecimal))
                    {
                        mesazh += " Vlera e komponentes " + pagvj.Komponente + ",";
                        fushatmod += String.Format(" Vlera e komponentes " + pagvj.Komponente + " nga {0} ne {1},", pagvj.Vlera, oColPagaShtesa[i].Vlera);
                        modifikuar = true;
                    }
                    if (oColPagaShtesa[i].VleraParam.ToString(RrumbullakimiDecimal) != pagvj.VleraParam.ToString(RrumbullakimiDecimal))
                    {
                        mesazh += " Vlera e parametrit te komponentes " + pagvj.Komponente + ",";
                        fushatmod += String.Format(" Vlera e parametrit te komponentes " + pagvj.Komponente + " nga {0} ne {1},", pagvj.VleraParam, oColPagaShtesa[i].VleraParam);
                        modifikuar = true;
                    }
                }
            }
            if (mesazh.Substring(mesazh.Length - 1, 1) == ",")
            {
                mesazh = mesazh.Substring(0, mesazh.Length - 1) + ".";
                fushatmod = fushatmod.Substring(0, fushatmod.Length - 1) + ".";
            }
            return mesazh;
        }

        #endregion

        #region Metoda Internal



        internal void mbushPagaShtesa(IDataRecord dbDataRowPagaShtesa)
        {

            try
            {
                Converter.ParseExact(dbDataRowPagaShtesa["IDPAGASHTESA"].ToString(), out idPagaShtesa, "idPagaShtesa");
                Converter.ParseExact(dbDataRowPagaShtesa["IDPUNONJES"].ToString(), out idPunonjes, "idPunonjes");
                Converter.ParseExact(dbDataRowPagaShtesa["IdKomponentePage"].ToString(), out idKomponente, "idKomponente");
                Converter.Parse(dbDataRowPagaShtesa["DTAKTIVIZIMI"].ToString(), out dtAktivizimi, "dtAktivizimi");
                Converter.Parse(dbDataRowPagaShtesa["VLERAPARAM"].ToString(), out vleraParam, "vleraParam");
                Converter.Parse(dbDataRowPagaShtesa["VLERA"].ToString(), out vlera, "vlera");
                komponente = dbDataRowPagaShtesa["Komponente"].ToString();
                Converter.Parse(dbDataRowPagaShtesa["Njesia"].ToString(), out njesia, "njesia");
                emerparam = dbDataRowPagaShtesa["EmerParam"].ToString();
                Converter.Parse(dbDataRowPagaShtesa["IDPERDORUESI"].ToString(), out idPerdoruesi, "idPerdoruesi");

            }
            catch (MyWarnException warn)
            {
                ImbLogger.Warn(warn);
            }
            catch (MyException e)
            {
                throw new MyException("gabim ne mbushje te pagaShtesa per punonjesit me id  {0} {1}" + IdPunonjes, e.Message);
            }

        }

        public static clsPagaShtesa Krijo(IDataRecord dbDataRowPagaShtesa)
        {
            var pagaShtesa = new clsPagaShtesa();
            pagaShtesa.mbushPagaShtesa(dbDataRowPagaShtesa);
            return pagaShtesa;
        }

        #endregion
    }
}
