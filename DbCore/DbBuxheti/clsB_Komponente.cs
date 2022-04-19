using DbCore.IMBUtils.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Core.Interfaces.Localization;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Logging;
using DbCore.DbAdmin;

namespace DbCore.DbBuxheti
{
    public class ClsBKomponente : IDataBase
    {
        #region Properties
        public int Id { get; set; }
        public int IdNdermarrje { get; set; }
        public int IdKrijuesi { get; set; }
        public int IdModifikuesi { get; set; }
        public DateTime? DtKrijimi { get; set; }
        public DateTime? DtModifikimi { get; set; }
        public string Kodi { get; set; }
        public string Pershkrimi { get; set; }
        public int Tipi { get; set; }
        public int Njesia { get; set; }
        public int IdBuxheti { get; set; }
        public string Formula { get; set; }
        public decimal? VleraMin { get; set; }
        public decimal? VleraMax { get; set; }
        public int? LlojKufizimi { get; set; }
        public bool Aktive { get; set; }
        public ColBKomponenteLidhje LidhjeKomponente { get; set; }

        #endregion

        #region Konstruktore
        public ClsBKomponente() { }

        public ClsBKomponente(int id, int idNdermarrje, int idKrijuesi, int idModifikuesi, DateTime? dtKrijimi, DateTime? dtModifikimi, string kodi, string pershkrimi, int tipi, int njesia, int idBuxheti, string formula, decimal? vleraMin, decimal? vleraMax, int? llojKufizimi, bool aktive)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, id, idNdermarrje, idKrijuesi, idModifikuesi, dtKrijimi, dtModifikimi, kodi, pershkrimi, tipi, njesia, idBuxheti, formula, vleraMin, vleraMax, llojKufizimi, aktive);
            Id = id;
            IdNdermarrje = idNdermarrje;
            IdKrijuesi = idKrijuesi;
            IdModifikuesi = idModifikuesi;
            DtKrijimi = dtKrijimi;
            DtModifikimi = dtModifikimi;
            Kodi = kodi;
            Pershkrimi = pershkrimi;
            Tipi = tipi;
            Njesia = njesia;
            IdBuxheti = idBuxheti;
            Formula = formula;
            VleraMin = vleraMin;
            VleraMax = vleraMax;
            LlojKufizimi = llojKufizimi;
            Aktive = aktive;
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, id, idNdermarrje, idKrijuesi, idModifikuesi, dtKrijimi, dtModifikimi, kodi, pershkrimi, tipi, njesia, idBuxheti, formula, vleraMin, vleraMax, llojKufizimi, aktive);
        }

        public ClsBKomponente(IDataRecord record)
        {
            Mbush(record);
        }
        public ClsBKomponente(int id)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.ktheKomponenteSipasId(id, this);

            LidhjeKomponente = new ColBKomponenteLidhje(id);
        }
        #endregion

        #region Public
        public clsMesazh Ruaj()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
                using (var scope = new MyTransactionScope())
                {
                    var mesazh = Valido();
                    if (!mesazh)
                        return mesazh;

                    mesazh = RuajKomponente();
                    if (!mesazh)
                        return mesazh;

                    mesazh = LidhjeKomponente.Ruaj(Id);
                    if (!mesazh)
                        return mesazh;

                    mesazh = RuajKomponenteNeHistorik();
                    if (!mesazh)
                        return mesazh;

                    scope.Complete();
                }
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return new MesazhSuksesi(MessagesResource.Messages["msgRuajtjeMeSukses"]);
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Modifiko()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
                using (var scope = new MyTransactionScope())
                {
                    var mesazh = Valido();
                    if (!mesazh)
                        return mesazh;

                    mesazh = ModifikoKomponente();
                    if (!mesazh)
                        return mesazh;

                    mesazh = LidhjeKomponente.Modifiko(Id);
                    if (!mesazh)
                        return mesazh;

                    mesazh = RuajKomponenteNeHistorik();
                    if (!mesazh)
                        return mesazh;

                    scope.Complete();
                }
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return new MesazhSuksesi(MessagesResource.Messages["msgModifikimiMeSukses"]);
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Fshi()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
                using (var scope = new MyTransactionScope())
                {
                    var mesazh = ValidoFshirje();
                    if (!mesazh)
                        return mesazh;

                    mesazh = RuajKomponenteNeHistorik();
                    if (!mesazh)
                        return mesazh;

                    mesazh = LidhjeKomponente.Fshi(Id);
                    if (!mesazh)
                        return mesazh;

                    mesazh = FshiKomponente();
                    if (!mesazh)
                        return mesazh;

                    scope.Complete();
                }
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return new MesazhSuksesi(MessagesResource.Messages["msgFshirjeMeSukses"]);
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh ValidoFshirje()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            bool eshtePerdorurKomponenteNeFormule = EshtePerdorurKomponenteNeFormule(false).Status;
            bool eshtePerdorurKomponenteNeVeprime = EshtePerdorurKomponenteNeVeprime().Status;
            bool eshteLidhurMeNdermarrje = EshteLidhurMeNdermarrje().Status;

            clsMesazh mesazh = ValidoFshirje(MessagesResource.Messages, eshtePerdorurKomponenteNeFormule, eshtePerdorurKomponenteNeVeprime, eshteLidhurMeNdermarrje);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return mesazh;
        }

        public static clsMesazh ValidoFshirje(IMessagesResource messages ,bool eshtePerdorurKomponenteNeFormule, bool eshtePerdorurKomponenteNeVeprime, bool eshteLidhurMeNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, eshtePerdorurKomponenteNeFormule, eshtePerdorurKomponenteNeVeprime, eshteLidhurMeNdermarrje);

            clsMesazh mesazh = new clsMesazh();

            if (eshtePerdorurKomponenteNeFormule)
                mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, messages["msgKomponentePerdorurNeFormule"] + messages["msgKomponenteNukMundTeFshihet"]);

            else if (eshtePerdorurKomponenteNeVeprime)
                mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, messages["msgKomponentePerdorurNeVeprime"] + messages["msgKomponenteNukMundTeFshihet"]);

            else if (eshteLidhurMeNdermarrje)
                mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, messages["msgKomponenteLidhurMeNdermarrje"] + messages["msgKomponenteNukMundTeFshihet"]);

            else mesazh = new MesazhSuksesi();

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, eshtePerdorurKomponenteNeFormule, eshtePerdorurKomponenteNeVeprime, eshteLidhurMeNdermarrje);
            return mesazh;
        }

        public clsMesazh EshteSakteFormula()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            if (String.IsNullOrEmpty(Formula))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgKomponenteFormulaBosh"]);
            
            string formula = Formula;
            int klapafillimi = formula.Count(f => f == '(');
            int klapambarimi = formula.Count(f => f == ')');
            if (klapafillimi != klapambarimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgKomponenteFormulaKeq"]); 

            string[] parametra = parametraFormule(formula);
            var colKomponetetAll = new ColBKomponente(IdNdermarrje);
            for (int i = 0; i < parametra.Length; i++)
            {
                if (parametra[i] == "")
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgKomponenteFormulaKeq"]);

                double nr = 0;
                if (double.TryParse(parametra[i], out nr)) // a eshte nr
                    continue;

                var komponenteEkzistuese = colKomponetetAll.Find(x => x.Kodi == parametra[i]);
                if (komponenteEkzistuese == null) /// a ekziston ne db
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgKomponenteMeKod"] + parametra[i] + MessagesResource.Messages["msgKomponenteQePerdoretNeFormule"]);
                if (!komponenteEkzistuese.Aktive) /// a eshte aktive
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgKomponenteMeKod"] + parametra[i] + MessagesResource.Messages["msgKomponenteQePerdoretNeFormuleJoAktive"]);
            }
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new MesazhSuksesi(MessagesResource.Messages["msgKomponenteFormulaSakte"]);
        }
        
        public static decimal llogaritFormule(string formula, int idQendraShendetesore, ClsBKomponente komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, formula, idQendraShendetesore, komponente);
            decimal vlera;
            using (var dt = new DataTable())
            {
                try
                {
                    vlera = Convert.ToDecimal(dt.Compute(formula, ""));
                }
                catch (Exception ex)
                {
                    ImbLogger.LogErrorBuxhetimi(ex);
                    clsNdermarrje qendraSh = new clsNdermarrje(idQendraShendetesore);
                    throw new MyException($"Nuk mund te vlersohet formula {komponente.Formula} per komponenten {komponente.Kodi} dhe qendren shendetesore {qendraSh.NdermarrjeKodi}", false);
                }
            }
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, formula, idQendraShendetesore, komponente);
            return vlera;
        }

        public static string gjeneroKomponenteNgaFormula(ClsBKomponente komponente, string formula, int idQendraShendetesore, ColBKomponente komponentetAll, ColBKomponenteVlere komponenteVleraAll, ref ColBKomponenteVlere komponenteNgaGjenerimi, int idPerdoruesi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            var komponenteLlogaritur = komponenteNgaGjenerimi.Find(x => x.IdKomponente == komponente.Id && x.IdQendraShendetesore == idQendraShendetesore);
            if (komponenteLlogaritur != null)
                return komponenteLlogaritur.Vlera.ToString();

            string vlera;
            if (String.IsNullOrEmpty(formula))
            {   //Komponente qe e kane formulen bosh (zakonisht tabelare)
                var komponenteVlere = komponenteVleraAll.FirstOrDefault<ClsBKomponenteVlere>(x => x.IdKomponente == komponente.Id && x.IdQendraShendetesore == idQendraShendetesore);
                if (komponenteVlere != null)
                {
                    vlera = kontrolloDheKtheVlereKomponente(komponente, komponenteVlere, 0);
                    komponenteNgaGjenerimi.Add(new ClsBKomponenteVlere(0, 0, komponente.IdNdermarrje, idPerdoruesi, 0, null, null, komponente.Id, idQendraShendetesore, Convert.ToDecimal(vlera), komponenteVlere.VleraMin, komponenteVlere.VleraMax, komponenteVlere.Tipi, true, "", ""));
                    return vlera;
                }
                komponenteNgaGjenerimi.Add(new ClsBKomponenteVlere(0, 0, komponente.IdNdermarrje, idPerdoruesi, 0, null, null, komponente.Id, idQendraShendetesore, 0, komponente.VleraMin, komponente.VleraMax, komponente.LlojKufizimi, true, "", ""));
                return "0";
            }

            var parametra = parametraFormule(formula);
            for (int i = 0; i < parametra.Length; i++)
            {
                var komponentePerberese = komponentetAll.FirstOrDefault(x => x.Kodi == parametra[i]);
                if (komponentePerberese == null)
                    continue;

                string formulePerberese = gjeneroKomponenteNgaFormula(komponentePerberese, komponentePerberese.Formula, idQendraShendetesore, komponentetAll, komponenteVleraAll, ref komponenteNgaGjenerimi, idPerdoruesi);
                formula = ZevendesoParameter(formula, parametra[i], formulePerberese );
            }

            var vleraComputed = llogaritFormule(formula, idQendraShendetesore, komponente);
            vlera = kontrolloDheKtheVlereKomponente(komponente, null, vleraComputed);
            komponenteNgaGjenerimi.Add(new ClsBKomponenteVlere(0, 0, komponente.IdNdermarrje, idPerdoruesi, 0, null, null, komponente.Id, idQendraShendetesore, Convert.ToDecimal(vlera), komponente.VleraMin, komponente.VleraMax, komponente.LlojKufizimi, true, "", ""));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return vlera;
        }

        public static decimal? KtheSipasVlereMinMax(decimal? vlera, decimal? vleraMin, decimal? vleraMax, int? llojKufizimi)
        {
            if (vleraMin == null && vleraMax == null)
                return vlera;

            switch (llojKufizimi)
            {
                case (int)EnumBLlojKufizimi.Perfshires:
                    if (vleraMax == null && vlera < vleraMin)
                        return (decimal)vleraMin;
                    if (vleraMax == null && vlera >= vleraMin)
                        return vlera;
                    if (vleraMin == null && vlera < vleraMax)
                        return (decimal)vleraMax;
                    if (vleraMin == null && vlera >= vleraMax)
                        return vlera;
                    if (vlera > vleraMin && vlera < vleraMax)
                        return (decimal)vleraMax;
                    if (vlera >= vleraMax)
                        return vlera;
                    if (vlera < vleraMin)
                        return (decimal)vleraMin;
                    return vlera;
                case (int)EnumBLlojKufizimi.Perjashtues:
                    if (vleraMax == null && vlera < vleraMin)
                        return 0;
                    if (vleraMax == null && vlera >= vleraMin)
                        return vlera;
                    if (vleraMin == null && vlera < vleraMax)
                        return 0;
                    if (vleraMin == null && vlera >= vleraMax)
                        return vlera;
                    if (vlera > vleraMin && vlera < vleraMax)
                        return vlera;
                    if (vlera >= vleraMax)
                        return vlera;
                    if (vlera < vleraMin)
                        return 0;
                    return vlera;
                default:
                    return vlera;
            }
        }

        #endregion

        #region Private
        private clsMesazh Valido()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            bool ekzistonMeTeNjejtinKod = EkzistonKomponenteMeKeteKod(Kodi).Status;
            bool perdorurNeFormule = EshtePerdorurKomponenteNeFormule(true).Status;
            clsMesazh eshteSakteFormula = EshteSakteFormula();
            ClsBKomponente komponenteEkzistuese = new ClsBKomponente(Id);
            bool perdorurNeVeprimeEksistuesja = komponenteEkzistuese.EshtePerdorurKomponenteNeVeprime().Status;

            clsMesazh mesazh = ValidoKomponente(MessagesResource.Messages, this.Id, this.Kodi, this.Pershkrimi, this.Njesia, this.Tipi, this.Aktive, this.VleraMin, this.VleraMax, this.LlojKufizimi, ekzistonMeTeNjejtinKod, perdorurNeFormule, komponenteEkzistuese.Tipi, perdorurNeVeprimeEksistuesja, eshteSakteFormula);            
           
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return mesazh;
        }

        public static clsMesazh ValidoKomponente(IMessagesResource messages, int id, string kodi, string pershkrimi, int njesia, int tipi, bool aktive, decimal? vleraMin, decimal? vleraMax, int? llojKufizimi, bool ekzistonMeTeNjejtinKod, bool eshtePerdorurNeFormule, int tipiEkzistuesja, bool eshtePerdorurNeVeprimeEksistuesja, clsMesazh eshteSakteFormula)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, messages, id, kodi, njesia, tipi, aktive, vleraMin, vleraMax, llojKufizimi, ekzistonMeTeNjejtinKod, eshtePerdorurNeFormule, tipiEkzistuesja, eshtePerdorurNeVeprimeEksistuesja, eshteSakteFormula);
            clsMesazh mesazh = new clsMesazh(true);

            mesazh = clsFunksione.kontrolloKaraktereMeMesazh(kodi, FusheKontrolli.Kodi, messages, false);

            if (mesazh.Status)
                mesazh = clsFunksione.kontrolloKaraktereMeMesazh(pershkrimi, FusheKontrolli.Pershkrimi, messages, true);

            if (mesazh.Status && (vleraMin != null || vleraMax != null) && llojKufizimi == null)
                mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, messages["msgPlotesoLlojKufizimi"]);

            else if (mesazh.Status && vleraMin != null && vleraMax != null && vleraMin > vleraMax)
                mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, messages["msgVleraMinMaxKeq"]);

            else if (mesazh.Status && id <= 0 && ekzistonMeTeNjejtinKod)
                mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, messages["msgKompEkziston"]);

            else if (mesazh.Status && njesia == (int)EnumBNjesiKomponente.Formule)
                mesazh = eshteSakteFormula;

            if (mesazh.Status && id > 0 && !aktive && eshtePerdorurNeFormule)
                mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, messages["msgKomponentePerdorurNeFormule"] + messages["msgKomponenteNukMundTeBehetInaktive"]);

            else if (mesazh.Status && id > 0 && tipi != tipiEkzistuesja && (eshtePerdorurNeFormule || eshtePerdorurNeVeprimeEksistuesja))
                mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, (eshtePerdorurNeFormule ? messages["msgKomponentePerdorurNeFormule"] : messages["msgKomponentePerdorurNeVeprime"]) + messages["msgKomponenteNukNdryshohetTipi"]);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, messages, id, kodi, njesia, tipi, aktive, vleraMin, vleraMax, llojKufizimi, ekzistonMeTeNjejtinKod, eshtePerdorurNeFormule, tipiEkzistuesja, eshtePerdorurNeVeprimeEksistuesja, eshteSakteFormula);
            if (!mesazh.Status)
                return mesazh;
            return new MesazhSuksesi();
        }

        private clsMesazh RuajKomponente()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.RuajKomponente(this);
        }

        private clsMesazh ModifikoKomponente()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.ModifikoKomponente(this);
        }

        private clsMesazh RuajKomponenteNeHistorik()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.RuajKomponenteNeHistorik(this);
        }

        private clsMesazh FshiKomponente()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.FshiKomponente(this);
        }

        private clsMesazh EkzistonKomponenteMeKeteKod(string kodi)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EkzistonKomponenteMeKeteKod(kodi, IdNdermarrje);
        }

        private clsMesazh EshtePerdorurKomponenteNeFormule(bool vtmAktive)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EshtePerdorurKomponenteNeFormule(Kodi, IdNdermarrje, vtmAktive);
        }

        private clsMesazh EshtePerdorurKomponenteNeVeprime()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EshtePerdorurKomponenteNeVeprime(this.Id);
        }

        private clsMesazh EshteLidhurMeNdermarrje()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EshteLidhurMeNdermarrje(this.Id);
        }

        private static string kontrolloDheKtheVlereKomponente(ClsBKomponente komponente, ClsBKomponenteVlere komponenteVlere, decimal vleraFormules)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponente, komponenteVlere, vleraFormules);
            string vlera;
            switch (komponente.Njesia)
            {
                case (int)EnumBNjesiKomponente.Numer:
                case (int)EnumBNjesiKomponente.Formule:
                    vlera = KtheSipasVlereMinMax(vleraFormules, komponente.VleraMin, komponente.VleraMax, komponente.LlojKufizimi).ToString();
                    break;
                case (int)EnumBNjesiKomponente.Tabelare:
                    vlera = KtheSipasVlereMinMax(komponenteVlere.Vlera, komponenteVlere.VleraMin, komponenteVlere.VleraMax, komponenteVlere.Tipi).ToString();
                    break;
                default:
                    vlera = String.Empty;
                    break;
            }
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponente, komponenteVlere, vleraFormules);
            return vlera;
        }

        private static string[] parametraFormule(string formula)
        {
            formula = formula.Replace("(", "")
                    .Replace(")", "")
                    .Replace("+", "&")
                    .Replace("-", "&")
                    .Replace("*", "&")
                    .Replace("/", "&");
            return formula.Split('&');
        }

        private static string ZevendesoParameter(string formula, string parameter, string vlera)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, formula, parameter, vlera);

            var pattern = $@"\b{parameter}\b";
            var rgx = new Regex(pattern);
            formula = rgx.Replace(formula, vlera);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, formula, parameter, vlera);
            return formula;
        }

        #endregion
             
        #region Mbushja
        public void Mbush(IDataRecord record)
        {
            Id = !Convert.IsDBNull(record["ID"]) ? Convert.ToInt32(record["ID"]) : 0;
            IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0;
            IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0;
            IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0;
            DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null;
            DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null;
            Kodi = !Convert.IsDBNull(record["KODI"]) ? Convert.ToString(record["KODI"]) : String.Empty;
            Pershkrimi = !Convert.IsDBNull(record["PERSHKRIMI"]) ? Convert.ToString(record["PERSHKRIMI"]) : String.Empty;
            Tipi = !Convert.IsDBNull(record["TIPI"]) ? Convert.ToInt32(record["TIPI"]) : 0;
            Njesia = !Convert.IsDBNull(record["NJESIA"]) ? Convert.ToInt32(record["NJESIA"]) : 0;
            IdBuxheti = !Convert.IsDBNull(record["IDBUXHETI"]) ? Convert.ToInt32(record["IDBUXHETI"]) : 0;
            Formula = !Convert.IsDBNull(record["FORMULA"]) ? Convert.ToString(record["FORMULA"]) : String.Empty;
            VleraMin = !Convert.IsDBNull(record["VLERAMIN"]) ? Convert.ToInt32(record["VLERAMIN"]) : (decimal?)null;
            VleraMax = !Convert.IsDBNull(record["VLERAMAX"]) ? Convert.ToInt32(record["VLERAMAX"]) : (decimal?)null;
            LlojKufizimi = !Convert.IsDBNull(record["LLOJKUFIZIMI"]) ? Convert.ToInt32(record["LLOJKUFIZIMI"]) : (int?)null;
            Aktive = !Convert.IsDBNull(record["AKTIVE"]) ? Convert.ToBoolean(record["AKTIVE"]) : false;
        }
        #endregion
    }
}
