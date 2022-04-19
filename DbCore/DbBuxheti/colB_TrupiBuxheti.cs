using System.Collections.Generic;
using System.Data;
using System;
using System.Linq;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWebCommon.TreeStructure;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbBuxheti
{
    public class ColBTrupiBuxheti:List<ClsBTrupiBuxheti>,IDataBaseReader
    {
        public ColBTrupiBuxheti()
        {
        }

        public ColBTrupiBuxheti(int idKoka)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.ktheBTrupiBuxhetiSipasIdKoka(idKoka, this);
        }

        public clsMesazh Ruaj()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

                //WHAT THE FUCK????Seriously?
                //if (this == null)
                //    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Collectioni me te rreshtat e trupit te buxhetit eshte null, ruajta nuk mund te kryhet!");

                if (this.FirstOrDefault() == null)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Nuk ka asnje rresht te ndryshuar ne trup!");
                
                clsMesazh mesazh = RuajTrupat();

                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return mesazh;
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh RuajTrupat()
        {
            return RuajTeGjitheMeDT(this);
        }

        public static clsMesazh RuajTeGjitheMeDT(ColBTrupiBuxheti rreshtaPerTuRuajtur)
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, rreshtaPerTuRuajtur);

                var dt = rreshtaPerTuRuajtur.ToDataTable("IdBuxhetiTrupi", "IdBuxhetiKoka", "IdNdermarrje", "IdKategoriBuxhetimi", "VleraPlanifikuar", "Vlera", "IdTrupiKonvertimiNga", "LlojKonvertimiNga", "VleraKategorise", "IdBuxheti", "LlogaritNeGjendje", "IdLlojPeriudhe", "Periudha", "IdObjekti", "LlojObjekti", "IdTvsh", "VleraPaTvsh", "Cmimi", "Sasia");

                using (var db = new ClsDatabaseBuxheti())
                    db.ruajTrupBuxhetiDT(dt);

                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, rreshtaPerTuRuajtur);
                return new MesazhSuksesi(IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);

            } catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new clsMesazh(false, ex.Message);
            }
        }

        public IEnumerable<TreeStructure<Dictionary<string, object>>> krijoStruktureTrupi(ColBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            var childNodes = new List<Dictionary<string, object>>();
            var node = new Dictionary<string, object>();
            var kategoria = new ClsBKategoriBuxhetimi();
            foreach (var rreshti in this)
            {
                node = new Dictionary<string, object>();
                kategoria = kategoriBuxhetimi.FirstOrDefault(kat => kat.IdKategoriBuxhetimi == rreshti.IdKategoriBuxhetimi);
                if (kategoria == null || !(kategoria.IdKategoriBuxhetimi > 0))
                    continue;
                node.Add("idKategoriBuxhetimi", kategoria.IdKategoriBuxhetimi);
                node.Add("kodi", kategoria.Kodi);
                node.Add("idKategoriBuxhetimiPrind", kategoria.IdPrindi);
                node.Add("idLlogaria", kategoria.IdLlogaria);
                node.Add("niveli", kategoria.NivelKategorie);
                node.Add("objekti", rreshti);
                childNodes.Add(node);
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return childNodes.GenerateTree(c => Convert.ToInt32(c["idKategoriBuxhetimi"]), c => Convert.ToInt32(c["idKategoriBuxhetimiPrind"]));
        }

        public void Mbush(IDataRecord record) => Add(new ClsBTrupiBuxheti(record));

        internal clsMesazh krijoTrupSipasMuajveAlokimBuxhetiNgaImporti(int idNdermarrje, decimal miratuar, string pershkrimi, string buxheti, string prindi, string analize, string[] muajt)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, miratuar, pershkrimi, buxheti, prindi, analize, muajt);

            #region Rastet Exception
            if (String.IsNullOrEmpty(buxheti) || String.IsNullOrWhiteSpace(buxheti)) throw new Exception("Fusha Buxheti nuk duhet te jete bosh.");
            if (String.IsNullOrEmpty(analize) || String.IsNullOrWhiteSpace(analize)) throw new Exception("Fusha Analize nuk duhet te jete bosh.");
            ClsDatabaseBuxheti db = new ClsDatabaseBuxheti();
            int idprindi = (!String.IsNullOrEmpty(prindi))?db.MerrIdKategoriSipasKodit(prindi, idNdermarrje):-1;
            if (!String.IsNullOrEmpty(prindi) && idprindi == -1) throw new Exception($"Prindi me kod {prindi} nuk ekziston.");
            int idKategoria = db.MerrIdKategoriSipasKodit(analize, idNdermarrje);
            if (idKategoria == -1) throw new Exception($"Analiza me kod {analize} nuk ekziston.");
            var kategoria = new ClsBKategoriBuxhetimi(MessagesResource.Messages, idKategoria);
            if (!(String.IsNullOrEmpty(pershkrimi) || String.IsNullOrWhiteSpace(pershkrimi)) && kategoria.Pershkrimi != pershkrimi) throw new Exception($"Pershkrimi {prindi} nuk i perket analizes {analize}.");
            if (kategoria.IdPrindi != 0 && kategoria.IdPrindi != idprindi) throw new Exception($"Prindi i analizes {analize} nuk eshte i sakte.");
            if (kategoria.IdPrindi != 0 && miratuar != 0) throw new Exception($"Analiza {analize} nuk eshte prind fundor dhe nuk mund te kete vlere Miratuar.");
            #endregion
                decimal vlerakateg = 0;
            for (int i = 1; i <= 12; i++)
            {
                ClsBTrupiBuxheti trupi = new ClsBTrupiBuxheti();
                trupi.IdNdermarrje = idNdermarrje;
                trupi.IdKategoriBuxhetimi = kategoria.IdKategoriBuxhetimi;
                trupi.IdBuxheti = 0;
                trupi.LlogaritNeGjendje = true;
                trupi.IdLlojPeriudhe = 1;
                trupi.Periudha = i;
                trupi.VleraPlanifikuar = miratuar;
                trupi.Vlera = Convert.ToDecimal(muajt[i - 1]);
                trupi.VleraPaTvsh = trupi.Vlera;
                vlerakateg += trupi.Vlera;
                this.Add(trupi);
            }
            foreach (ClsBTrupiBuxheti tr in this)
                tr.VleraKategorise = vlerakateg;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, miratuar, pershkrimi, buxheti, prindi, analize, muajt);
            return new clsMesazh(true);
        }
    }
}
