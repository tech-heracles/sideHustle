using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using DbCore.IMBUtils.Extensions;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///     Kjo eshte klasa qe mban nje liste me objekte te tipit clsPagaShtesa
    ///     dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///     (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsPagaShtesa" />
    public class colPagaShtesa : List<clsPagaShtesa>
    { 

        public new clsPagaShtesa this[int index]
        {
            get { return base[index]; }
        }

        #region Konstruktoret

        /// <summary>
        ///     konstruktor pa parameter
        /// </summary>
        public colPagaShtesa()
        {
        }

        /// <summary>
        ///     konstruktroi qe implementon klasen baze duke i dhene madhesine e koleksionit
        /// </summary>
        /// <param name="capacity">madhesia e koleksionit</param>
        public colPagaShtesa(int capacity) : base(capacity)
        {
        }
        /// <summary>
        ///     konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsPagaShtesa</param>
        public colPagaShtesa(IEnumerable<clsPagaShtesa> collection) : base(collection)
        {
        }

        /// <summary>
        ///     kontrukutori me dy parametra
        ///     merr paga dhe shtesa te nje punonjesi ne nje date te caktuar aktivizimi
        /// </summary>
        /// <param name="idpunonjes"> idpunonjes</param>
        /// <param name="data">data e aktivizimit</param>
        public colPagaShtesa(int idpunonjes, DateTime data) : base(new clsDatabazeListPagesa().kthePagaShtesaSipasPunonjesitDheDates(idpunonjes, data))
        {
        }

        public colPagaShtesa(int idpunonjes, DateTime data, int idkomponente) : base(new clsDatabazeListPagesa().kthePagaShtesaSipasPunonjesitDheDatesDheKompoenetes(idpunonjes, data, idkomponente))
        {
        }

        /// <summary>
        ///     kontrukutori me 3 parametra
        ///     merr paga dhe shtesa te nje punonjesi ne nje date te caktuar aktivizimi sipas llojit te komponente pages fillestare
        ///     pra vlerat e komponentes jane zero
        /// </summary>
        /// <param name="idndermarje"> idndermarje</param>
        /// <param name="lloji">lloji i komponentes</param>
        /// <param name="data">data e aktivizimit</param>
        public colPagaShtesa(bool lloji, DateTime data, int idndermarje) : base(new clsDatabazeListPagesa().kthePagaShtesaFillestare(lloji, data, idndermarje))
        {
        }

        public colPagaShtesa(bool lloji, DateTime data, int idndermarje, DateTime dtndryshimi, int idpunonjes) : base(new clsDatabazeListPagesa().kthePagaShtesaSipasPunonjesitDatesLlojitNdermarjes(lloji, data, idndermarje, dtndryshimi, idpunonjes))

        {
        }

        #endregion Konstruktoret

        #region Metoda Publike
        public clsMesazh Ruaj(int idPunonjes, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            foreach (var pagashtesa in this)
            {
                pagashtesa.IdPunonjes = idPunonjes;
                var mesazh = pagashtesa.ruaj();
                if (!mesazh) return new MesazhGabimi(rm.GetString("msgPunonjesKompPage",ci));
            }
            return new MesazhSuksesi();
        }
        public clsMesazh modifikoPagaDheShtesa(clsDatabazeListPagesa db, clsPunonjes punonjesvjeter, colPagaShtesa pagaefundit, int idPunonjes, int idPerdoruesi, System.Resources.ResourceManager rm ,System.Globalization.CultureInfo ci)
        {
            var fushatmod = "";
            var mesazhmod = "";
            var mesazh = new clsMesazh();
            var modifikuar = false;
            var dataKomponentesh = colPagaShtesa.merrDataKomponente(idPunonjes, false);
            if (punonjesvjeter.OColPagaShtesa.Count > 0)
            {
                mesazhmod = clsPagaShtesa.kontrolloPaga(this, punonjesvjeter.OColPagaShtesa, out fushatmod, out modifikuar, db);

                foreach (var pagashtesa in punonjesvjeter.OColPagaShtesa)
                {
                    mesazh = pagashtesa.fshi(db);
                    if (!mesazh.Status)
                        return new clsMesazh(false, rm.GetString("msgPunonjesKompPage",ci));
                }
            }
            else if (pagaefundit.Count > 0)
                mesazhmod = clsPagaShtesa.kontrolloPaga(this, pagaefundit, out fushatmod, out modifikuar, db);
            else
            {
                mesazhmod = "U shtua paga dhe shtesa";
                modifikuar = true;
            }
            if (modifikuar)
            {
                mesazh = EshteModifikimVlefshem(idPunonjes, dataKomponentesh,ci,rm);
                if (!mesazh) return mesazh;
            }
            mesazh = Ruaj(idPunonjes,rm,ci);
            if (!mesazh) return mesazh;
            if (mesazhmod != "U modifikuan fushat per pagat dhe shtesat:")
            {
                mesazh = clsPunonjes.ruajLog(idPunonjes, idPerdoruesi, 1, mesazhmod, fushatmod, db);
                if (!mesazh.Status)
                    return new clsMesazh(false, rm.GetString("msgPunonjesRuajFail",ci));
            }
            return new clsMesazh(true, "Paga dhe shtesa u ruajten me sukses!");
        }

        /// <summary>
        ///     merr datat ne te cilat kemi paga shtesa
        /// </summary>
        /// <param name="idpunonjesi">id e punonjes</param>
        /// <returns>liste me datat</returns>

        public static List<string> merrDataPagaShtesa(int idpunonjesi, bool klonim)
        {
            return merrDataKomponente(idpunonjesi, klonim)?.OrderByDescending(x=>x).Select(dt => dt.ToShortDateString()).ToList();
        }
        public static List<DateTime> merrDataKomponente(int idpunonjesi, bool klonim)
        {
            var list = new List<DateTime>();
            using (var db = new clsDatabazeListPagesa())
            {
                var dt = db.merrDataPagaShtesaSipasIdPunonjes(idpunonjesi);
                foreach (DataRow rreshti in dt.Rows)
                {
                    list.Add(DateTime.Parse(rreshti["DTAKTIVIZIMI"].ToString()).Date);
                    if (klonim) break;
                }
            }
            return list;
        }

        public static DataTable merrPagaDheShtesaPerExport(int idndermarje)
        {
            using (var dbKodifikimKF = new clsDatabazeListPagesa())
            {
                return dbKodifikimKF.merrPagaDheShtesaPerExport(idndermarje);
            }
        }

        /// <summary>
        ///     merr paga dhe shtesa  te nje punonjesi me te fundit ne kete date
        /// </summary>
        /// <param name="dtndryshimi">dt e ndryshimit</param>
        /// <param name="idpunonjes">idpunonjes</param>
        public static colPagaShtesa merrPagaShtesaSipasPunonjesiDheDatesMeTeFundit(DateTime dtndryshimi, int idpunonjes)
        {
            using (var db = new clsDatabazeListPagesa())
                return new colPagaShtesa(db.kthePagaShtesaSipasPunonjesitDheDatesMeTefundit(idpunonjes, dtndryshimi));
        }

        /// <summary>
        /// kthen te gjitha pagashtesa te grupuara sipas punonjesit
        /// </summary>
        /// <param name="dtndryshimi"></param>
        /// <param name="idpunonjesish"></param>
        /// <returns></returns>
        public static Dictionary<int, colPagaShtesa> merrPagaShtesaSipasPunonjesveDheDatesMeTeFundit(int nrPunonjesish, IEnumerable<PunonjesMeData> punjesitMeDatat)
        {
            var pagaShtesaPerPunonjesit = new Dictionary<int, colPagaShtesa>(nrPunonjesish);
            var pagaShtesaAll = MerrColPagaShtesaSipasPunonjesve(punjesitMeDatat);
            foreach (var pagaShtesa in pagaShtesaAll.GroupBy(x => x.IdPunonjes))
                pagaShtesaPerPunonjesit[pagaShtesa.Key] = new colPagaShtesa(pagaShtesa);
            return pagaShtesaPerPunonjesit;
        }

        public static colPagaShtesa MerrColPagaShtesaSipasPunonjesve(IEnumerable<PunonjesMeData> punjesitMeDatat)
        {
            using (var db = new clsDatabazeListPagesa())
            {
                return new colPagaShtesa(db.kthePagaShtesaSipasPunonjesveDheDatesMeTefundit(punjesitMeDatat.ToDataTable("IdPunonjesi", "Data")));

            }
        }

        public colPagaShtesa Clone()
        {
            var clonedCollection = new colPagaShtesa { Capacity = this.Count };
            ForEach(paga =>
            {
                clonedCollection.Add(paga.Clone());
            });
            return clonedCollection;
        }
        public bool Equals(colPagaShtesa pagaShtesa)
        {
            foreach (var paga in this)
            {
                var pagaPerkatese = pagaShtesa.FirstOrDefault(newPaga => paga.IdKomponentePage == newPaga.IdKomponentePage);
                if (pagaPerkatese.Vlera != paga.Vlera || pagaPerkatese.VleraParam != paga.VleraParam)
                    return false;
            }
            return true;
        }

        public clsMesazh EshteModifikimVlefshem(int idPunonjesi, List<DateTime> dataKomponentesh, System.Globalization.CultureInfo ci,ResourceManager rm)
        {
           
            //marrim te paren ne radhe
            string paga = rm.GetString("kompPaga", ci);
            var dtAktivizimi = this[0].DtAktivizimi;
            return Utils.validoVlefshmerineEModifikimitTeKomponenteve(dtAktivizimi, idPunonjesi, dataKomponentesh,paga, ci,rm);
        }
        #endregion Metoda Publike
    }
}