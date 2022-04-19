
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.SessionState;
using DbCore.IMBUtils;
using System.Web;
using DbCore.IMBUtils.MultiTasking;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiListPagese
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsTrupiListPagese"/>
    public class colTrupiListPagese : List<clsTrupiListPagese>
    {
        private List<int> punonjesitIds;
        private List<int> trupatIds;
        private Dictionary<string, string>[] dokumenti;
        private List<colKompListPagese> komponente;
        private int idNdermarrje;

        /// <summary>
        /// kthen objektin <see cref="clsTrupiListPagese"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiListPagese this[int index]
        {
            get { return base[index]; }
        }


        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>

        public colTrupiListPagese()
        {

        }
        /// <summary>
        /// krijon nje colTrupListPagese duke u bazuar tek dictionary i krijuar nga json i jqgrides
        /// </summary>
        /// <param name="trupDokumenti"></param>
        /// <param name="idNdermarrje"></param>

        /// <summary>
        /// konstruktori me nje parameter
        /// merr trupin e listpagese kur i jep id koken
        /// </summary>
        /// <param name="idkoka">id e kokes se list pages</param>
        public colTrupiListPagese(int idkoka, clsDatabazeListPagesa db) : base(db.ktheGjitheTrupListPageseNgaKokaList(idkoka).OrderBy(x => x.IdPunonjes))
        {
            //clsDatabazeListPagesa db = new clsDatabazeListPagesa();

            //mbushTrupat(db.ktheGjitheTrupiListPageseNgaKoka(idkoka));
            //db.Dispose();
        }
        public colTrupiListPagese(Dictionary<string, int> numraPersonalMeId, Dictionary<string, string>[] dokumenti, List<colKompListPagese> komponente, int idNdermarrje, HttpSessionState sessioni, int idGjuha, DateTime data)
        {
            mbushTrupatMeKomponentet(numraPersonalMeId, dokumenti, komponente, sessioni, idGjuha, data);
        }
        #endregion
        #region Metoda Publike
        /// <summary>
        /// mbush trupin e dokumentit me komponentet e punonjesit perkates
        /// </summary>
        /// <param name="nrPersonalMeEmra"></param>
        /// <param name="colKomp"></param>
        /// <param name="colKompCounter"></param>
        /// <param name="colKompMuajiCounter"></param>
        /// <param name="session"></param>
        public void mbushTrupatMeKomponentet(Dictionary<string, int> nrPersonalMeId, Dictionary<string, string>[] trupDokumenti, List<colKompListPagese> colKomp, HttpSessionState session, int idGjuha, DateTime data)
        {
            int colKompCounter = 0, colKompMuajiCounter = 0;
            int i = 1;
            Capacity = trupDokumenti.Length;
            foreach (var rreshtDoku in trupDokumenti)
            {

                string txtNrpersonal = rreshtDoku["txtNrPersonal"];
                if (!string.IsNullOrWhiteSpace(txtNrpersonal))
                {
                    var kyTrup = new clsTrupiListPagese
                    {
                        Paguar = decimal.Parse(rreshtDoku["txtPaguar"]),
                        Cost = decimal.Parse(rreshtDoku["txtCosto"]),
                        Shenime = rreshtDoku["txtShenime"],
                    };
                    int idPunonjesi;
                    if (nrPersonalMeId.TryGetValue(txtNrpersonal, out idPunonjesi))
                        kyTrup.IdPunonjes = idPunonjesi;//int.Parse(idPunonjesi.ToString());
                    else throw new MyException($"Punonjesi me nrPersonal  {txtNrpersonal} nuk ekziston!,Trupi i dokumentit nuk eshte i rregullt!");
                    kyTrup.IdTrupi = 0 - i;//u japim vlere negative sepse eshte shtim
                    kyTrup.OColKomp = colKomp[i - 1];
                    var vleratEMujave = Utils.MerrVleratMujoreTeNjePunonjesi(session, kyTrup.IdPunonjes);
                    kyTrup.OColKomp.VendosPaguarDheCosto(kyTrup);
                    kyTrup.OColKomp.LidhTrupatMeKomponenteMuaji(kyTrup.IdTrupi, ref colKompCounter, ref colKompMuajiCounter, vleratEMujave, kyTrup.IdPunonjes, idGjuha, data, txtNrpersonal);
                    if (this.Exists(x => x.IdPunonjes == kyTrup.IdPunonjes))
                        throw new MyException($"Punonjesi me nrPersonal  {txtNrpersonal} ekziston njehere ne dokument!,Trupi i dokumentit nuk eshte i rregullt!");
                    Add(kyTrup);
                    i++;
                }

            }
        }




        /// <summary>
        /// kthen listen e plote te komponeteve te cilat jane te lidhur me rreshtat e ketij trupi
        /// </summary>
        /// <returns></returns>
        public colKompListPagese MerrListenKompListPagese()
        {

            var colKomp = this.Select(x => x.OColKomp); //qendrojme ne inumeracion per te mos zene memorie dyfish
            int count = this.Sum(x => x.OColKomp.Count); //merr madhesine e listes per te bere allokimin e cila eshte  = me shumen e numrin te komp per cdo rresht trupi
            colKompListPagese colKompIbashkuar = new colKompListPagese { Capacity = count };
            foreach (var item in colKomp)
            {
                colKompIbashkuar.AddRange(item);
            }
            return colKompIbashkuar;
        }
        /// <summary>
        /// u vendos gjithe trupave te kesaj LP id e kokes pasi eshte bere ruajtja e kokes
        /// </summary>
        /// <param name="idKoka"></param>
        public void VendosIdKokeNeTrup(int idKoka)
        {
            ForEach(x => x.IdKoka = idKoka);
        }


        public Tuple<List<int>, colPunonjes, colPunesim, Dictionary<int, colKompListPagese>, object> MerrTrupDokumentiListPagese( DateTime dtLp, HttpSessionState session,  bool hiqPunonjesTelarguarKlonim, DateTime cmbData)
        {

            var currentContext = HttpContext.Current;
            MbushListatMeId();
            colPunonjes punonjesit = null;
            Dictionary<int, colKompListPagese> kompSipasPunonjesit = null;
            colPunesim punesimet = null;
            Parallel.Invoke(new ParallelOptions { MaxDegreeOfParallelism = 100 },
                () =>
                {
                    HttpContext.Current = currentContext;
                    punonjesit = new colPunonjes(punonjesitIds, hiqPunonjesTelarguarKlonim, cmbData);
                },
                () =>
                {
                    HttpContext.Current = currentContext;
                    kompSipasPunonjesit = KtheKomponentePerPunonjes(session);
                },
                () =>
                {
                    HttpContext.Current = currentContext; punesimet = colPunesim.MerrPunesimTeFunditPerPunonjesit(punonjesitIds, dtLp);
                }
                );

            var lp = from p in punonjesit
                     join komp in kompSipasPunonjesit on p.IdPunonjes equals komp.Key
                     orderby p.NrRendor, p.IdPunonjes
                     let komponentePunonjesi = kompSipasPunonjesit.FirstOrDefault(x => x.Key == p.IdPunonjes)
                     let rreshtLp = this.FirstOrDefault(x => x.IdPunonjes == p.IdPunonjes)
                     let punesim = punesimet.FirstOrDefault(x => x.IdPunonjes == p.IdPunonjes)
                     select new
                     {
                         IdPunonjesi = p.IdPunonjes,
                         Emri = $"{p.Emer} {p.Mbiemer}",
                         NrPersonal = p.NrPersonal,
                         Departamenti = punesim.Departament,
                         ColKomponente = komponentePunonjesi.Value,
                         Shenime = rreshtLp.Shenime,
                         Cost = rreshtLp.Cost,
                         Paguar = rreshtLp.Paguar,
                     };
            return new Tuple<List<int>, colPunonjes, colPunesim, Dictionary<int, colKompListPagese>, object>(punonjesitIds, punonjesit, punesimet, kompSipasPunonjesit, lp);
        }
        /// <summary>
        /// kthen komponentet perberese te trupit te nje listpagese
        /// </summary>
        /// <returns> nje liste me komponentet e secilit rresht te trupit</returns>
        public Dictionary<int, colKompListPagese> KtheKomponentePerPunonjes(HttpSessionState session)
        {
            var colKomponenteNew = new colKompListPagese(ImbTasks.MerrListAsParallel<int, clsKompListPagese>(16, "MerrKompListPagese", trupatIds));
            // var colKomponente = new colKompListPagese(trupatIds);//marrim collection me komponente 
            var kompLpIds = colKomponenteNew.Select(x => x.IdKompListPagese).ToList();//marrim gjithe id-te per ti perdorur ne leximin e kompmuaji

            //grupojme sipas trupit
            var lista = colKomponenteNew.GroupBy(x => x.IdTrupi).ToDictionary(x => x.Key, x => new colKompListPagese(x));

            var dicKompMuaji = ImbTasks.MerrDictionaryAsParallel<int, int, colKomponenteMuaji>(8, "MerrDicKomponenteMuaji", kompLpIds);
            var vleratPerMuajt = new Dictionary<int, Dictionary<string, colKomponenteMuaji>>();
            var dicNew = new Dictionary<int, colKompListPagese>(lista.Count);
            foreach (var item in lista)
            {
                var indexi = trupatIds.IndexOf(item.Key);
                var idPunonjesi = punonjesitIds[indexi];
                var vlerat = new Dictionary<string, colKomponenteMuaji>();
                foreach (var komp in item.Value)
                {
                    colKomponenteMuaji kompMuaji;
                    if (!dicKompMuaji.TryGetValue(komp.IdKompListPagese, out kompMuaji)) continue;
                    komp.OcolKomponenteMuaji = kompMuaji;
                    vlerat.Add(komp.KodKomponente, kompMuaji);
                }
                vleratPerMuajt[idPunonjesi] = vlerat;
                dicNew[idPunonjesi] = item.Value;
            }
            Utils.RuajGjitheVleratColKomponenteMuaji(session, vleratPerMuajt);
            return dicNew;
        }

        #endregion

        #region Metoda Private
        /// <summary>
        /// mbush dy listat me id e trupave dhe punonjesve sipas trupave te kesa listpagese
        /// </summary>
        /// <returns></returns>
        private void MbushListatMeId()
        {
            trupatIds = new List<int>(Count);
            punonjesitIds = new List<int>(Count);
            foreach (var trup in this)//marrim idte
            {
                trupatIds.Add(trup.IdTrupi);
                punonjesitIds.Add(trup.IdPunonjes);
            }
        }

        #endregion
    }
}
