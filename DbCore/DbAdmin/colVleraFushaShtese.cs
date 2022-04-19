using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.Share;

namespace DbCore.DbAdmin
{
    public class colVleraFushaShtese : List<clsVleraFushaShtese>, IDataBase
    {
        #region Konstruktoret

        public colVleraFushaShtese()
        {

        }
        public colVleraFushaShtese(IEnumerable<clsVleraFushaShtese> vlerat) : base(vlerat)
        {

        }
        public colVleraFushaShtese(int id, DateTime? dtAktivizimi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                data.ktheVleratSipasIdLidhese(id, dtAktivizimi, this);
        }

        public colVleraFushaShtese(int id, int idmod, DateTime? dtAktivizimi) : this(id, idmod, dtAktivizimi, new clsDatabaseAdmin())
        {
        }
        public colVleraFushaShtese(int id, int idmod, DateTime? dtAktivizimi, clsDatabaseAdmin data)
        {
            data.ktheVleratSipasIdLidheseAndIdModeli(id, idmod, dtAktivizimi, this);
        }

        #endregion

        #region Metoda Publike

        public new clsVleraFushaShtese this[int index] => (base[index]);

        public clsMesazh Ruaj()
        {
            int i = 0;
            DateTime dtAktivizimiTemp = new DateTime();
            this.OrderBy(y => y.DtAktivizimi).ForEach(x =>
            {
                if (dtAktivizimiTemp != x.DtAktivizimi)
                    i = 0;
                x.Nr_Rendor = ++i;
                dtAktivizimiTemp = x.DtAktivizimi;
            });

            using (var dbAdmin = new clsDatabaseAdmin())
            {
                var dt = this.ToDataTable("IdVleraFushaShtese", "IdLidhese", "IdFushaShtese", "VleraFushaShtese", "IdModeliFushaShtese", "PershkrimiFushaShtese", "TipiFushaShtese", "Nr_Rendor", "DtAktivizimi", "IdPerdoruesi", "VleraFundit");
                return dbAdmin.ruajFushaShteseDt(dt);
            }
        }

        public colVleraFushaShtese Clone()
        {
            var newcol = new colVleraFushaShtese { Capacity = Count };
            ForEach(x => newcol.Add(x.Clone()));
            return newcol;
        }
        #endregion

        /// <summary>
        /// fshin gjithe vlerat e vjetra per fushat shtese te nje entiteti te caktuar
        /// </summary>
        /// <param name="idLidhese">id lidhese e fushave shtese me nje entitet</param>
        /// <param name="kodModeli">pershkrimi i modelit te fushave shtese</param>
        /// <param name="idNdermarje">idndermarrje</param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="dbAdmin"></param>
        /// <returns></returns>
        internal static clsMesazh fshiFushaShtese(int idLidhese, string kodModeli, int idNdermarje, int idPerdoruesi, clsDatabaseAdmin dbAdmin)
        {
            return dbAdmin.fshiVleraFushaShtese(idLidhese, kodModeli, idNdermarje, idPerdoruesi);
        }
        public static List<DateTime> MerrDataAktivizmi(int idLidhese, int idModeli)
        {
            using (var dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.MerrDataAktivizimi(idLidhese, idModeli);
            }
        }
        /// <summary>
        /// krijon nje collection me vlera ne baze te fushave shtese per nje model te caktuar
        /// </summary>
        /// <param name="idModeli"></param>
        /// <param name="vleratSipasDates"></param>
        /// <param name="idGjuha"></param>
        /// <param name="fushatEKetijModeli"></param>
        /// <returns></returns>
        public static colVleraFushaShtese KrijoVleratSipasFushave(colVleraFushaShtese vleratSipasDates, int idGjuha, colFushatShtese fushatEKetijModeli, int idPerdoruesi)
        {
            var colVlerat = new colVleraFushaShtese();
            bool fushPrind = false;
            foreach (var f in fushatEKetijModeli)
            {
                if (f.TipiFushaShtese == 6)
                {
                    var prindi = fushatEKetijModeli.Find(x => x.IdFushaShtese == f.AtiTipiFushaShtese);
                    fushPrind = !(prindi != null && prindi.TipiFushaShtese == 6);
                }
                else
                    fushPrind = true;

                if (fushPrind)//lejon te shtohen vetem ato fusha qe jane prind
                {
                    var vl = new DbCore.DbAdmin.clsVleraFushaShtese();
                    vl.IdModeliFushaShtese = f.IdModeliFushaShtese;
                    vl.IdFushaShtese = f.IdFushaShtese;
                    vl.TipiFushaShtese = f.TipiFushaShtese;
                    vl.Shenime = f.Shenime;
                    vl.PershkrimiFushaShtese = idGjuha == 0 ? f.PershkrimiFushaShtese : f.PershkrimiEng;
                    vl.IdPerdoruesi = idPerdoruesi;
                    vl.VleraFushaShtese = vleratSipasDates.FirstOrDefault(x => x.IdFushaShtese == f.IdFushaShtese)?.VleraFushaShtese;
                    colVlerat.Add(vl);
                }
            }
            return colVlerat;
        }
        public static DataTable MerrVleraFushaShteseSipasModelitDheFushavePerGride(int idNdermarrje, int idModeli, string kolonaGride, string filter, int gjuha, int subjekti)
        {
            using (var db = new clsDatabaseAdmin())
            {
                return db.MerrVleraFushaShteseSipasModelitDheFushave(idNdermarrje, idModeli, kolonaGride, filter, gjuha, subjekti);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idKonfigurimi">idKonfig</param>
        /// <param name="vleraTeModifikuaraSipasDates">vlera te modifikuara</param>
        /// <param name="vleraOrigjinialeSipasDates">vlerat e pa modifikuara</param>
        /// <returns></returns>
        public static clsMesazh EshteModifikimIVlefshem(int idKonfigurimi, Dictionary<string, colVleraFushaShtese> vleraTeModifikuaraSipasDates, Dictionary<string, colVleraFushaShtese> vleraOrigjinialeSipasDates)
        {
            //fushat po plotesohen per here te pare
            if (vleraOrigjinialeSipasDates == null || vleraOrigjinialeSipasDates.Count == 0) return new MesazhSuksesi();


            if (clsAlternativaKushti.getAlternativa(idKonfigurimi, KushteFushaShtese.NFSHVDFH) == AlternativaKushtesh.Po)
            {

                foreach (var vleraEModifikuar in vleraTeModifikuaraSipasDates)
                {
                    colVleraFushaShtese vleraOrigjinale = null;
                    if (vleraOrigjinialeSipasDates.TryGetValue(vleraEModifikuar.Key, out vleraOrigjinale))
                    {
                        //ekzistojne vlera te meparshme per kete date

                        foreach (var vleraMod in vleraEModifikuar.Value)
                        {
                            //nese vlera e ketij objekti eshte e fundit ath nuk ka rendesi nese eshte modifikuar apo jo 
                            if (vleraMod.VleraFundit) break;

                            var vleraKoresponduese = vleraOrigjinale.Find(x => x.IdFushaShtese == vleraMod.IdFushaShtese);

                            //kontrollojme nese per kete date eshte modifikuar ndonje vlere
                            if (vleraMod.VleraFushaShtese != vleraKoresponduese.VleraFushaShtese)
                                return new MesazhGabimi("Nuk lejohet te modifikohen vlerat per datat e hershme!");
                        }
                    }
                }
            }


            return new MesazhSuksesi();
        }
        public static colVleraFushaShtese ZbertheTeGrupuarat(Dictionary<int, Dictionary<string, colVleraFushaShtese>> vleraTeGrupuara)
        {
            return new colVleraFushaShtese(vleraTeGrupuara.SelectMany(x => x.Value).SelectMany(v => v.Value));
        }
        public static Dictionary<string, colVleraFushaShtese> GrupoSipasDates(colVleraFushaShtese vleraTeGrupuara)
        {
            return vleraTeGrupuara.GroupBy(x => x.DtAktivizimi)
                   .ToDictionary(k => k.Key.ToString("dd/MM/yyyy"), v => new colVleraFushaShtese(v.ToList()));
        }
        /// <summary>
        /// krijon nje list me ClsVleraFushaShtese duke u bazuar te fushat e modelit
        /// </summary>
        /// <param name="idModeli"></param>
        /// <param name="vleratSipasDates"></param>
        /// <param name="idGjuha"></param>
        /// <param name="fushatEKetijModeli"></param>
        /// <returns></returns>
        public static colVleraFushaShtese MerrVleratSipasFushave(int idModeli, colVleraFushaShtese vleratSipasDates, int idGjuha, colFushatShtese fushatEKetijModeli,int idPerdoruesi)
        {
            if (vleratSipasDates == null) throw new MyException("VleratSipasDates nuk duhet te jete null!");
            var dtAktivizimi = vleratSipasDates?.FirstOrDefault()?.DtAktivizimi;
            var colVlerat = new colVleraFushaShtese();
            var idNew = -1;
            if (vleratSipasDates.Count != 0)
            {
                foreach (var vleraRe in vleratSipasDates)
                {
                    vleraRe.IdVleraFushaShtese = Math.Abs(vleraRe.IdVleraFushaShtese);
                }
            }
            foreach (var f in fushatEKetijModeli)
            {
                //nese nuk eshte prind nuk shtohet ne listen e vlerave
                if (!fushatEKetijModeli.EshteFushePrind(f))
                    continue;

                var vl = new clsVleraFushaShtese
                {
                    IdModeliFushaShtese = idModeli,
                    IdFushaShtese = f.IdFushaShtese,
                    TipiFushaShtese = f.TipiFushaShtese,
                    Shenime = f.Shenime
                };

                if (dtAktivizimi.HasValue)
                    vl.DtAktivizimi = dtAktivizimi.Value;

                vl.PershkrimiFushaShtese = idGjuha == 0 ? f.PershkrimiFushaShtese : f.PershkrimiEng;

                var vleraVjeter = vleratSipasDates.FirstOrDefault(x => x.IdFushaShtese == f.IdFushaShtese);
                if (vleraVjeter?.IdVleraFushaShtese > 0)
                {
                    vl.VleraFushaShtese = vleraVjeter.VleraFushaShtese;
                    vl.IdVleraFushaShtese = vleraVjeter.IdVleraFushaShtese;
                    vl.IdPerdoruesi = vleraVjeter.IdPerdoruesi;
                }
                else
                {
                    //i vendosim id negative per ta dalluar qe nuk jane ekzistuese
                    vl.IdVleraFushaShtese = (idNew--);
                    vl.IdPerdoruesi = idPerdoruesi;
                }
                colVlerat.Add(vl);
            }
            return colVlerat;
        }
        /// <summary>
        /// vendos ne collection vlerat e reja te marra nga nderfaqja
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="vleraFundit"></param>
        /// <param name="dokumenti"></param>
        /// <param name="vleratPerDatenEAktivizimit"></param>
        public void PerditesoVlerat(DateTime dt, bool vleraFundit, object[] dokumenti, int idPerdoruesi, int idLidhese)
        {
            int j = 0;
            foreach (var vlera in this.OrderBy(x => x.IdFushaShtese))
            {
                if (dokumenti.Length > j && dokumenti[j] != null && "specialValue_fshehur" != dokumenti[j].ToString())
                    vlera.VleraFushaShtese = dokumenti[j]?.ToString();
                vlera.VleraFundit = vleraFundit;
                vlera.DtAktivizimi = dt.Date;
                vlera.DtKrijimi = DateTime.Now;
                vlera.IdPerdoruesi = idPerdoruesi;
                vlera.IdLidhese = idLidhese;
                j++;
            }
        }
        public static bool EshteDataMeEfundit(int idLidhese, int idModeli, DateTime dtAktivizimi)
        {
            using (var dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.EshteDataMeEfundit(idLidhese, idModeli, dtAktivizimi);
            }
        }

        public clsMesazh Ruaj(int idLidhese)
        {
            if (Count > 0)
            {
                ForEach(x => x.IdLidhese = idLidhese);
                return Ruaj();
            }
            return new MesazhSuksesi();
        }
        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();

        }

        public void Mbush(IDataRecord record)
        {
            Add(new clsVleraFushaShtese(record));
        }
    }
}

