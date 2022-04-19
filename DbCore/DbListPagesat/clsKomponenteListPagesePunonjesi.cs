using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;

namespace DbCore.DbListPagesat
{
    /// <summary>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje komponente list pagese punonjesi
    ///  (Te dhenat  merren nga tabela : T_KOMPONENTELISTPAGESEPUNONJES)
    /// </summary>
    public class clsKomponenteListPagesePunonjesi : IDataBaseReader
    {
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se komponente listpagese punonjesi nga db-ja";
        public const string FormatiDates = "dd/MM/yyyy";
        public const string RrumbullakimiDecimal = "n4";
        #region Atribute

        private int idKomponenteListPagesePunonjes;
        private int idPunonjes;
        private int idKomponente;
        private DateTime dtAktivizimi;
        private bool edukshme;
        private string komponente;
        private int njesia;
        private string formula;
        private string kodKomponente;
        private bool edetyrueshme;
        private int idPerdoruesi;
        private decimal vleraDefault;
        private DataRow rreshti;
        private string grup;
        private IDataRecord record;
        private string nrPersonalPunonjesi;
        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idkomponentelistpagesepunonjes">Id e komponente listpagese punonjes</param>
        /// <param name="idpunonjes">Id e punonjesit</param>
        /// <param name="idkomponente">Id e komponente</param>
        /// <param name="dtaktivizimi">data e aktivizimit</param>
        /// <param name="edukshme">e dukshme</param>
        public clsKomponenteListPagesePunonjesi(int idkomponentelistpagesepunonjes, int idpunonjes, int idkomponente, DateTime dtaktivizimi, bool edukshme, bool edetyrueshme, int idperdoruesi, decimal vlera)
        {
            idKomponenteListPagesePunonjes = idkomponentelistpagesepunonjes;
            idPunonjes = idpunonjes;
            idKomponente = idkomponente;
            dtAktivizimi = dtaktivizimi;
            this.edukshme = edukshme;
            this.edetyrueshme = edetyrueshme;
            this.idPerdoruesi = idperdoruesi;
            vleraDefault = vlera;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsKomponenteListPagesePunonjesi()
        {
        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// </summary>
        /// <param name="idkomponentelistpagesepunonjes">id idkomponentelistpagesepunonjes</param>
        public clsKomponenteListPagesePunonjesi(int idkomponentelistpagesepunonjes)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheKomponenteListPagesePunonjes(idkomponentelistpagesepunonjes, this);
            }
        }

        public clsKomponenteListPagesePunonjesi(DataRow rreshti)
        {

            // mbushKomponenteListPagesePunonjesi(rreshti);
        }

        public clsKomponenteListPagesePunonjesi(IDataRecord record)
        {
            Mbush(record);
        }
        #endregion

        #region Properties

        /// <summary>
        /// tregon nqs komponentja do jete e detyrueshme per tu plotesuar gjate regjistrimit te list pagesave
        /// </summary>
        public bool Edetyrueshme
        {
            get
            {
                return edetyrueshme;
            }
            set
            {
                edetyrueshme = value;
            }
        }
        /// <summary>
        /// grupi i komponentes
        /// </summary>

        public string Grup
        {
            get
            {
                return grup;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKomponenteListPagesePunonjes
        {
            get { return idKomponenteListPagesePunonjes; }
            set { idKomponenteListPagesePunonjes = value; }
        }

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
        /// kthen/vendos e dukshme
        /// </summary>
        public bool Edukshme
        {
            get
            {
                return edukshme;
            }
            set
            {
                edukshme = value;
            }
        }
        /// <summary>
        /// kthen kodin e komponentes
        /// </summary>
        public string KodKomponente
        {
            get
            {
                return kodKomponente;
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
        public string Formula
        {
            get
            {
                return formula;
            }
        }
        public decimal VleraDefault
        {
            get
            {
                return vleraDefault;
            }
            set
            {
                vleraDefault = value;
            }
        }

        public string NrPersonalPunonjesi => nrPersonalPunonjesi;
        #endregion

        #region Metoda Publike
        public clsKomponenteListPagesePunonjesi krijoPerImport(string kodi, string emer, string mbiemer, DateTime data, decimal totali, int idperdoruesi, int idndermarje, string komponente, int vitinderm)
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

                //if (ekzistonListOrari(data, idPunonjesi, dblist))
                //    throw new Exception("Ekziston nje list orari per punonjesin " + pun.NrPersonal + " per daten " + data.ToShortDateString());
                return new clsKomponenteListPagesePunonjesi(0, idPunonjes, komp.IdKomponentePage, data, komp.ShfaqDefault, komp.ShfaqDefault, idperdoruesi, totali)
                {
                    kodKomponente = komp.Kodi,
                    nrPersonalPunonjesi = kodi
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public clsMesazh ruaj()
        {
            int id;
            using (var data = new clsDatabazeListPagesa())
            {
                clsMesazh u_ruajt = data.ruajKomponenteListPagesePunonjes(out id, idPunonjes, idKomponente, dtAktivizimi, edukshme, edetyrueshme, idPerdoruesi, vleraDefault);
                this.IdKomponenteListPagesePunonjes = id;
                return u_ruajt;
            }
        }
        /// <summary>
        /// Ruan objektin e KomponenteListPagesePunonjes ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.ruajKomponenteListPagesePunonjes"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj(int idndermarje)
        {
            using (var myScope = new MyTransactionScope())
            {
                var komp = new clsKomponentePage(idKomponente);
                clsMesazh mes;
                //merr komp per punonjesin sipas dates se percaktuar ne import
                var colpaga = new colKomponenteListPagesePunonjesi(idPunonjes, dtAktivizimi, komp.Kodi);
                if (colpaga.Count > 0)
                {//nese ekziston modifkoje
                    mes = modifiko(colpaga);
                    if (mes) myScope.Complete();
                    return mes;
                }
                //else krijo nje skeme te re,me daten e vendosur ne import
                colpaga.ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesMeTeFundit(dtAktivizimi, idPunonjes);
                if (colpaga.Count == 0)
                    colpaga = new colKomponenteListPagesePunonjesi(true, dtAktivizimi, idndermarje);
                colpaga.Find(x => x.KodKomponente == komp.Kodi).vleraDefault = vleraDefault;
                mes = Ruaj(colpaga);
                if (!mes) return mes;

                var sig = new clsSigurimet();
                sig.ktheSigurimeSipasDatesMeTeAfert(idndermarje, dtAktivizimi);
                if (sig.IdSigurime > 0)
                {
                    sig.IdPerdoruesi = IdPerdoruesi;
                    sig.RuajSipasDates(IdPunonjes, dtAktivizimi);
                }
                myScope.Complete();
                return mes;
            }
        }

        private clsMesazh modifiko(colKomponenteListPagesePunonjesi colpaga)
        {
            using (var data = new clsDatabazeListPagesa())
            {
                var kompVjeter = colpaga[0];
                var mes = data.modifikoKomponenteListPagesePunonjes(kompVjeter.idKomponenteListPagesePunonjes, idPunonjes, idKomponente, dtAktivizimi, kompVjeter.edukshme, kompVjeter.edetyrueshme, idPerdoruesi, vleraDefault);
                if (!mes) return mes;
                mes = data.ruajLogPunonjes(out idKomponenteListPagesePunonjes, IdPunonjes, IdPerdoruesi, 1, $"modifikim komponente listpagese nga importi per punonjesin me numer personal {NrPersonalPunonjesi} dhe dtAktivizimi : {this.DtAktivizimi} ", $"Komponente :{KodKomponente}, VleraDefault ishte :{kompVjeter.VleraDefault}, u be :{this.VleraDefault}, EDukshme ishte {kompVjeter.Edukshme}, u be :{this.Edukshme} ,EDetyrueshme ishte :{kompVjeter.Edetyrueshme} , u be :{this.Edetyrueshme}");
                return mes;
            }
        }

        private clsMesazh Ruaj(colKomponenteListPagesePunonjesi colpaga)
        {

            var data = new clsDatabazeListPagesa();

            foreach (var komp in colpaga)
            {
                int id;

                var mes = data.ruajKomponenteListPagesePunonjes(out id, idPunonjes, komp.idKomponente, dtAktivizimi, komp.edukshme, komp.edetyrueshme, idPerdoruesi, komp.vleraDefault);
                komp.IdKomponenteListPagesePunonjes = id;
                if (!mes) return mes;
                int idLog = 0;
                mes = data.ruajLogPunonjes(out idLog, IdPunonjes, IdPerdoruesi, 1, $"shtim komponente listpagese nga importi per punonjesin me numer personal {NrPersonalPunonjesi} dhe dtAktivizimi : {this.DtAktivizimi} ", $"Komponente :{KodKomponente}, VleraDefault :{this.VleraDefault}, EDukshme :{this.Edukshme} ,EDetyrueshme :{this.Edetyrueshme}");
                if (!mes) return mes;
            }
            return new MesazhSuksesi("Ruajtja perfundoi me sukses");
        }

        /// <summary>
        /// Modifikon objektin e KomponenteListPagesePunonjesne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.modifikoKomponenteListPagesePunonjes"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko(clsDatabazeListPagesa data)
        {

            clsMesazh u_modifikua = data.modifikoKomponenteListPagesePunonjes(idKomponenteListPagesePunonjes, idPunonjes, idKomponente, dtAktivizimi, edukshme, edetyrueshme, idPerdoruesi, vleraDefault);
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e KomponenteListPagesePunonjesne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.fshiKomponenteListPagesePunonjes"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshi(clsDatabazeListPagesa data, int idPerdoruesi)
        {

            clsMesazh u_fshi = data.fshiKomponenteListPagesePunonjes(idKomponenteListPagesePunonjes, idPerdoruesi);
            return u_fshi;
        }
        /// <summary>
        /// merr KomponenteListPagesePunonjes sipas id
        /// </summary>
        public void merr()
        {
            using (clsDatabazeListPagesa data = new clsDatabazeListPagesa())
            {
                data.ktheKomponenteListPagesePunonjes(idKomponenteListPagesePunonjes, this);
            }
        }

        public static decimal MerrVlereDefaultPerKomponente(string kodi, int idPunonjes, DateTime data)
        {

            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                return db.MerrVlereDefaultPerKomponente(kodi, idPunonjes, data);

            }
        }

        public static bool  MerrEDukshmePerKomponente(string kodi, int idPunonjes, DateTime data)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                return db.MerrEDukshmePerKomponente(kodi, idPunonjes, data);

            }
        }
        #endregion

        #region Metoda Internal

        internal static string kontrolloKomp(colKomponenteListPagesePunonjesi oColKomponente, colKomponenteListPagesePunonjesi kompvjeter, out string fushatmod, out bool modifikuar, clsDatabazeListPagesa db)
        {
            string mesazh = "U modifikuan fushat per komponente listpagese:";
            fushatmod = "U modifikuan fushat per komponente listpagese:";
            modifikuar = false;
            if (oColKomponente[0].DtAktivizimi.ToString(FormatiDates) != kompvjeter[0].DtAktivizimi.ToString(FormatiDates))
            {
                mesazh += " Data e aktivizimit,";
                fushatmod += String.Format(" Data e aktivizimit nga {0} ne {1},", kompvjeter[0].DtAktivizimi.ToString(FormatiDates), oColKomponente[0].DtAktivizimi.ToString(FormatiDates));
            }
            for (int i = 0; i < oColKomponente.Count; i++)
            {
                clsKomponenteListPagesePunonjesi pagvj = kompvjeter.Find(x => x.IdKomponentePage == oColKomponente[i].IdKomponentePage);
                if (pagvj != null)
                {
                    if (oColKomponente[i].Edetyrueshme != pagvj.Edetyrueshme)
                    {
                        mesazh += " E detyrueshme e komponentes " + pagvj.Komponente + ",";
                        fushatmod += String.Format(" E detyrueshme e komponentes " + pagvj.Komponente + " nga {0} ne {1},", pagvj.Edetyrueshme, oColKomponente[i].Edetyrueshme);
                        modifikuar = true;
                    }
                    if (oColKomponente[i].Edukshme != pagvj.Edukshme)
                    {
                        mesazh += " E dukshme e parametrit te komponentes " + pagvj.Komponente + ",";
                        fushatmod += String.Format(" E dukshme e parametrit e komponentes " + pagvj.Komponente + " nga {0} ne {1},", pagvj.Edukshme, oColKomponente[i].Edukshme);
                    }
                    if (oColKomponente[i].VleraDefault.ToString(RrumbullakimiDecimal) != pagvj.VleraDefault.ToString(RrumbullakimiDecimal))
                    {
                        mesazh += " Vlera e parametrit te komponentes " + pagvj.Komponente + ",";
                        fushatmod += String.Format(" Vlera e parametrit e komponentes " + pagvj.Komponente + " nga {0} ne {1},", pagvj.VleraDefault, oColKomponente[i].VleraDefault);
                        modifikuar = true;
                    }
                }
            }
            if (mesazh.Substring(mesazh.Length - 1, 1) == ",")
                mesazh = mesazh.Substring(0, mesazh.Length - 1) + ".";
            return mesazh;
        }

        public clsKomponenteListPagesePunonjesi Clone()
        {
            return (clsKomponenteListPagesePunonjesi)MemberwiseClone();
        }

        public void Mbush(IDataRecord dbData)
        {

            try
            {
                Converter.ParseExact(dbData["IDKOMPLISTPAGESE"].ToString(), out idKomponenteListPagesePunonjes, "idKomponenteListPagesePunonjes");
                Converter.ParseExact(dbData["IDPUNONJES"].ToString(), out idPunonjes, "idPunonjes");
                Converter.ParseExact(dbData["IDKOMPONENTE"].ToString(), out idKomponente, "idKomponente");
                Converter.ParseExact(dbData["DTAKTIVIZIMI"].ToString(), out dtAktivizimi, "dtAktivizimi");
                Converter.Parse(dbData["EDUKSHME"].ToString(), out edukshme, "edukshme");
                komponente = dbData["Komponente"].ToString();
                kodKomponente = dbData["KodKomponente"].ToString();
                Converter.Parse(dbData["Njesia"].ToString(), out njesia, "njesia");
                formula = dbData["Formula"].ToString();
                Converter.Parse(dbData["EDETYRUESHME"].ToString(), out edetyrueshme, "edetyrueshme");
                Converter.Parse(dbData["IDPERDORUESI"].ToString(), out idPerdoruesi, "idPerdoruesi");
                grup = dbData["GRUP"].ToString();
                Converter.Parse(dbData["VLERADEFAULT"].ToString(), out vleraDefault, "vleraDefault");

            }
            catch (MyWarnException warn)
            {
                ImbLogger.Warn($"gabim ne mbushjen e komponenteve te listpageses per punonjesin  {idPunonjes},{ warn.Message}");
            }
            catch (MyException myex)
            {
                ImbLogger.Error($"gabim ne mbushjen e komponenteve te listpageses per punonjesin :{idPunonjes} {myex.Message}");
            }
        }

        #endregion
    }
}
