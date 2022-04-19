using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using DbCore.IMBUtils.Extensions;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKompListPagese
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    ///  komponentet e list pageses ne dokumentin e list pageses
    /// </summary>
    /// <seealso cref="clsKompListPagese"/>
    public class colKompListPagese : List<clsKompListPagese>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parameter
        /// </summary>
        public colKompListPagese()
        {
        }


        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKompListPagese</param>
        public colKompListPagese(IEnumerable<clsKompListPagese> collection)
            : base(collection)
        {

        }

        /// <summary>
        /// merr si parameter nje list me id nga trupi i listpagese dhe mbush collectionin 
        /// </summary>
        /// <param name="idTrupashLp"></param>
        public colKompListPagese(List<int> idTrupashLp) : base(clsKompListPagese.MerrKompListPagese(idTrupashLp))
        {

        }
        public static Dictionary<int, colKompListPagese> MerrKompListPageseDic(List<int> idTrupashLp)
        {
            var result = new clsDatabazeListPagesa().MerrKomponenteListPageseSipasTrupave(idTrupashLp);
            return result.GroupBy(x => x.IdTrupi).ToDictionary(x => x.Key, x => new colKompListPagese(x));
        }
        public void LidhTrupatMeKomponenteMuaji(int idTrupi, ref int colKompCounter, ref int colKompMuajiCounter, Dictionary<string, colKomponenteMuaji> vleratEMujave, int idpunonjesi, int idGjuha, DateTime data, string nrpersonal)
        {
            List<string> kodeTeDetyrueshme = colKomponenteListPagesePunonjesi.ktheKomponenteListPagesePunonjesiTeDetyrueshme(data, idpunonjesi);
            foreach (var fat in this)
            {
                fat.IdTrupi = idTrupi;
                fat.IdKompListPagese = (++colKompCounter);
                if (fat.Vlera == 0 && kodeTeDetyrueshme.Contains(fat.KodKomponente))
                    if (idGjuha == 0)
                        throw new Exception("Kujdes, plotesoni vleren e komponentit te detyrueshem " + fat.KodKomponente + " per punonjesin " + nrpersonal);
                    else throw new Exception("Caution! Fill in the value of the mandatory component " + fat.KodKomponente + " for employee " + nrpersonal);
                if (vleratEMujave != null && vleratEMujave.ContainsKey(fat.KodKomponente))
                {
                    fat.OcolKomponenteMuaji = vleratEMujave[fat.KodKomponente];
                    fat.OcolKomponenteMuaji.VendosIdKomponenteLP(fat.IdKompListPagese, ref colKompMuajiCounter);
                }
                else
                    fat.OcolKomponenteMuaji = new colKomponenteMuaji();
            }
        }
        public void MbushKomponenteMuajiNgaSessioni(int idTrupi, ref int colKompCounter, ref int colKompMuajiCounter, colKompListPagese rreshtiTrupit, HttpSessionState session, int idpunonjesi)
        {
            var vlerat = Utils.MerrVleratMujoreTeNjePunonjesi(session, idpunonjesi);
            foreach (var fat in rreshtiTrupit)
            {
                fat.IdTrupi = idTrupi;
                fat.IdKompListPagese = (++colKompCounter);
                if (fat.Vlera == 0 && fat.EDetyrueshme)
                    throw new Exception("Kujdes, plotesoni vleren e komponentit te detyrueshem " + fat.KodKomponente);
                if (vlerat != null && vlerat.ContainsKey(fat.KodKomponente))
                {
                    fat.OcolKomponenteMuaji = vlerat[fat.KodKomponente];
                    fat.OcolKomponenteMuaji.VendosIdKomponenteLP(fat.IdKompListPagese, ref colKompMuajiCounter);
                }
                else
                    fat.OcolKomponenteMuaji = new colKomponenteMuaji();
            }
        }
        internal void VendosPaguarDheCosto(clsTrupiListPagese trupi)
        {
            trupi.Cost = 0;
            trupi.Paguar = 0;
            // "SICKLEAVES" || kompPage.Kodi == "UNPAIDLEAVES"
            ForEach(komp =>
            {
                switch (komp.Tipi)
                {
                    case (int)TipPagese.Pagese:
                        trupi.Paguar += komp.Vlera;
                        trupi.Cost += komp.Vlera;
                        break;
                    case (int)TipPagese.Ndalese:
                        trupi.Paguar -= komp.Vlera;
                        break;
                    default:
                        break;
                }
                if (komp.KodKomponente.EqualsAnyIgnoreCase("SN", "COMPPENS"))
                    trupi.Cost += komp.Vlera;
                if (komp.KodKomponente.EqualsAnyIgnoreCase("SICKLEAVES", "UNPAIDLEAVES"))
                    trupi.Cost -= komp.Vlera;
            });
        }
        internal bool KontrolloTotalin(decimal paguar)
        {

            decimal paguarERillogaritur = 0;
            ForEach(komp =>
            {
                switch (komp.Tipi)
                {
                    case (int)TipPagese.Pagese:
                        paguarERillogaritur += komp.Vlera;
                        break;
                    case (int)TipPagese.Ndalese:
                        paguarERillogaritur -= komp.Vlera;
                        break;
                    default:
                        break;
                }
            });
            return paguarERillogaritur == paguar;
        }

        /// <summary>
        /// kontrukutori me 3 parametra
        /// merr komponente list pagese sipas ndermarjes llojit te komponentes dhe dates ne gjendje fillestare pra vlerat jane zero
        /// </summary>
        /// <param name="idndermarje"> idndermarje</param>
        /// <param name="lloji">lloji i komponentes</param>
        /// <param name="data">data</param>
        /// <see cref="LlojKomponentePage"/>
        public colKompListPagese(bool lloji, DateTime data, int idndermarje, int idpunonjes) : base(new clsDatabazeListPagesa().ktheKompListPageseFillestare(lloji, data, idndermarje, idpunonjes))
        {
            //var db = new clsDatabazeListPagesa();
            //mbushKompListPagese(db.ktheKompListPageseFillestare(lloji, data, idndermarje, idpunonjes));
            //db.Dispose();
        }
        public colKompListPagese(bool lloji, DateTime data, int idNdermarrje, int idPunonjes, bool paMuajt)
            : base(new clsDatabazeListPagesa().ktheKompListPageseFillestarePaKomponenteMuaji(lloji, data, idNdermarrje, idPunonjes))
        {

        }

        /// <summary>
        /// kontrukutori me 3 parametra
        /// merr te dhenat sipas punonjesit dhe dates
        /// </summary>
        /// <param name="idpunonjes"> idpunonjes</param>
        /// <param name="data">data</param>
        /// <param name="idNdermarrje">id e ndermarrjes</param>
        public colKompListPagese(int idpunonjes, DateTime data, int idNdermarrje)
            : base(new clsDatabazeListPagesa().ktheKompListPagesePunonjesDheDate(idpunonjes, data, idNdermarrje))
        {
        }
        #endregion

        /// <summary>
        /// grupon komponentet sipas punonjesve
        /// </summary>
        /// <param name="punonjesIDs"></param>
        /// <param name="colKomponente"></param>
        /// <returns></returns>
        private static Dictionary<int, colKompListPagese> krijoDictionaryPunonjesMeColKomponente(IReadOnlyCollection<int> punonjesIDs, IEnumerable<clsKompListPagese> colKomponente)
        {
            var lista = colKomponente.GroupBy(x => x.IdTrupi).Select(x => new
            {
                IdPunonjes = x.Key,
                ColKomponente = new colKompListPagese(x)
            });

            var col = new Dictionary<int, colKompListPagese>(punonjesIDs.Count);
            foreach (var item in lista)
                col.Add(item.IdPunonjes, item.ColKomponente);
            return col;
        }
        /// <summary>
        /// kthen nje dictionary i cili permban si celes idPunonjesi dhe si vlere collectionin me komponentet e punonjesit
        /// </summary>
        /// <param name="lloji">komponente page apo listpagese false=>page true LP</param>
        /// <param name="data"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="punonjesIDs"></param>
        /// <param name="paMuajt"></param>
        /// <returns></returns>
        public static Dictionary<int, colKompListPagese> MerrListKomponenteshPerPunonjesit(bool lloji, DateTime data, int idNdermarrje, List<int> punonjesIDs, bool paMuajt)
        {
            List<clsKompListPagese> colKomponente;
            using (var db = new clsDatabazeListPagesa())
                //te gjitha komponentet per gjithe punonjesit
                colKomponente = new List<clsKompListPagese>(db.ktheKompListPageseFillestarePerPunonjesitPaKomponenteMuaji(lloji, data, idNdermarrje, punonjesIDs));
            return krijoDictionaryPunonjesMeColKomponente(punonjesIDs, colKomponente);
        }
        public static Dictionary<int, colKompListPagese> MerrListKomponenteshPerPunonjesitBasic(bool lloji, DateTime data, int idNdermarrje, List<int> punonjesIDs, bool paMuajt)
        {
            if (punonjesIDs.Count == 0) return new Dictionary<int, colKompListPagese>();
            List<clsKompListPagese> colKomponente;
            using (var db = new clsDatabazeListPagesa())
                //te gjitha komponentet per gjithe punonjesit
                colKomponente = new List<clsKompListPagese>(db.ktheKompListPageseFillestarePerPunonjesitPaKomponenteMuajiBasic(lloji, data, idNdermarrje, punonjesIDs));
            return krijoDictionaryPunonjesMeColKomponente(punonjesIDs, colKomponente);
        }

        public static Dictionary<int, Dictionary<DateTime, colKompListPagese>> MerrListKomponenteshPerPunonjesitBasic(bool lloji, DateTime data, int idNdermarrje, List<PunonjesMeData> punonjesMeData, bool paMuajt)
        {
            if (punonjesMeData.Count == 0) return new Dictionary<int, Dictionary<DateTime, colKompListPagese>>();
            using (var db = new clsDatabazeListPagesa())
            //te gjitha komponentet per gjithe punonjesit
            {
                var result = (db.KtheKompListPageseFillestarePerPunonjesitSipasDatavePaKomponenteMuajiBasic(lloji, data, idNdermarrje, punonjesMeData));

                return result.GroupBy(x => x.IdTrupi)
                       .ToDictionary(x => x.Key, x => x.GroupBy(d => d.Data.Date)
                       .ToDictionary(d => d.Key, d => new colKompListPagese(d)));
            }

        }
        public static Dictionary<int, Dictionary<DateTime, colKompListPagese>> MerrDicKomponenteshPerPunonjesitSipasDataveBasicAsParallel(bool lloji, DateTime data, int idNdermarrje, List<PunonjesMeData> punonjesMeData, bool paMuajt, int nrTaskesh)
        {
            var currentContext = HttpContext.Current;
            var teGrupuara = punonjesMeData.GroupBy(x => x.IdPunonjesi).ToDictionary(x => x.Key, x => x.ToList<PunonjesMeData>());
            var myPunonjesCount = teGrupuara.Count;
            var myParaleleCount = nrTaskesh;
            var thela = myPunonjesCount / myParaleleCount;
            if (thela == 0)
            {
                thela = myPunonjesCount;
                myParaleleCount = 1;
            }
            var listOfListPunonjesish = new List<PunonjesMeData>[myParaleleCount];
            var tasks = new Task<Dictionary<int, Dictionary<DateTime, colKompListPagese>>>[myParaleleCount];
            for (var i = 0; i < myParaleleCount; i++)
            {
                var baseValue = i;
                listOfListPunonjesish[i] = (i == myParaleleCount - 1) ? teGrupuara.Skip(thela * i).SelectMany(x => x.Value).ToList() : teGrupuara.Skip(thela * i).Take(thela).SelectMany(x => x.Value).ToList();
                tasks[i] = Task.Factory.StartNew(b =>
                {
                    HttpContext.Current = currentContext;
                    return MerrListKomponenteshPerPunonjesitBasic(true, data, idNdermarrje, listOfListPunonjesish[(int)b], true);
                }, baseValue);

            }
            return Task.WhenAll(tasks).Result.SelectMany(x => x).ToDictionary(k => k.Key, v => v.Value);
        }
        public colKompListPagese Clone()
        {

            var col = new colKompListPagese { Capacity = this.Count };
            foreach (var cls in this)
            {
                col.Add(cls.Clone());
            }

            return col;
        }
        public static Dictionary<int, colKompListPagese> MerrListKomponenteshPerPunonjesitBasic(List<int> punonjesitIDs, DateTime data)
        {
            List<clsKompListPagese> colKomponente;
            using (var db = new clsDatabazeListPagesa())
                //te gjitha komponentet per gjithe punonjesit
                colKomponente = new List<clsKompListPagese>(db.MerrKomponenteLispagesePerPunonjesitSipasDatesBasic(punonjesitIDs, data));
            return krijoDictionaryPunonjesMeColKomponente(punonjesitIDs, colKomponente);
        }
        public static Dictionary<int, colKompListPagese> MerrListKomponenteshPerPunonjesitBasicAsParallel(List<int> punonjesIDs, DateTime data, int nrTaskesh)
        {
            var currentContext = HttpContext.Current;
            var myPunonjesCount = punonjesIDs.Count;
            var myParaleleCount = nrTaskesh;
            var thela = myPunonjesCount / myParaleleCount;
            if (thela == 0)
            {
                thela = myPunonjesCount;
                myParaleleCount = 1;
            }
            var listOfListPunonjesish = new List<int>[myParaleleCount];
            var tasks = new Task<Dictionary<int, colKompListPagese>>[myParaleleCount];
            for (var i = 0; i < myParaleleCount; i++)
            {
                var baseValue = i;
                if (i == myParaleleCount - 1)
                    listOfListPunonjesish[i] = punonjesIDs.Skip(thela * i).ToList();
                else
                    listOfListPunonjesish[i] = punonjesIDs.Skip(thela * i).Take(thela).ToList();
                tasks[i] = Task.Factory.StartNew(b =>
                {
                    HttpContext.Current = currentContext;
                    return MerrListKomponenteshPerPunonjesitBasic((listOfListPunonjesish[(int)b]), data);
                }, baseValue);

            }
            return Task.WhenAll(tasks).Result.SelectMany(x => x).ToDictionary(k => k.Key, v => v.Value);
        }
        public colKomponenteMuaji MerrListenEPloteKomponenteMuaji()
        {
            var colKomp = this.Select(x => x.OcolKomponenteMuaji);
            //qendrojme ne inumeracion per te mos zene memorie dyfish
            var count = this.Sum(x => x.OcolKomponenteMuaji.Count); //merr madhesine e listes per te bere allokimin
            var colKompIbashkuar = new colKomponenteMuaji { Capacity = count };
            foreach (var item in colKomp)
                colKompIbashkuar.AddRange(item);
            return colKompIbashkuar;
        }
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKompListPagese"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKompListPagese this[int index]
        {
            get { return base[index]; }
        }

        /// <summary>
        /// merr komponente sipas idtrupit
        /// </summary>
        /// <param name="idtrupi"> id e trupit te dokumentit te list pageses</param>
        public void merrKomponenteSipasIdTrupit(int idtrupi)
        {
            var db = new clsDatabazeListPagesa();
            // mbushKompListPagese(db.ktheKompListPageseSipasIdTrupi(idtrupi));
            AddRange(db.ktheKompListPageseSipasIdTrupi(idtrupi));
            db.Dispose();
        }
        #endregion


    }
}



