using DbCore.DbAdmin;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
    public class colOreShtese : System.Collections.Generic.List<clsOreShtese>
    {
        private List<int> _punonjesitIds;

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsOreShtese"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsOreShtese this[int index]
        {
            get { return ((clsOreShtese)base[index]); }
        }

        public colOreShtese()
        {

        }
        public colOreShtese(IEnumerable<clsOreShtese> collection) : base(collection) { }
        public colOreShtese(List<int> idpunonjesish, int muaji, int viti, int idKokaLp) : base(new clsDatabazeListPagesa().ktheGjitheOreShteseSipasPunonjesveDhePeriudhesAll(idpunonjesish, muaji, viti, idKokaLp))
        {
            _punonjesitIds = idpunonjesish;
        }

        public Dictionary<int, colOreShtese> GrupoOreShteseSipasPunonjesit()
        {
            var dicOreShtese = new Dictionary<int, colOreShtese>(_punonjesitIds.Count);
            var oreShteseTeGrupuara = this.GroupBy(x => x.IdPunonjesi);
            foreach (var oreShtese in oreShteseTeGrupuara)
            {
                dicOreShtese[oreShtese.Key] = new colOreShtese(oreShtese);
            }
            return dicOreShtese;
        }

        /// <summary>
        /// mbush gjithe kodifikimet e artikullit sipas ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool ktheGjitheOreShteseSipasNdermarrjes(int idndermarje)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            bool sukses = mbushOreShtese(dbKodifikimKF.ktheGjitheOreShteseSipasNdermarrjes(idndermarje));
            dbKodifikimKF.Dispose();
            return sukses;
        }
        public static DataTable ktheGjitheOreShteseExport(int idndermarje)
        {
            using (clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa())
            {
                return dbKodifikimKF.merrOreShtesePerExport(idndermarje);
            }

        }

        [Obsolete]
        public bool ktheGjitheOreShteseSipasPunonjesit(int idpunonjesi)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            bool sukses = mbushOreShtese(dbKodifikimKF.ktheGjitheOreShteseSipasPunonjesit(idpunonjesi));
            dbKodifikimKF.Dispose();
            return sukses;
        }

        [Obsolete]
        public static DataTable ktheGjitheOreShteseSipasPunonjesitDhePeriudhes(int idpunonjesi, string muaji, int viti, string kodikomponente)
        {
            using (clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa())
            {
                return dbKodifikimKF.ktheGjitheOreShteseSipasPunonjesitDhePeriudhes(idpunonjesi, muaji, viti, kodikomponente);

            }
        }

        [Obsolete]
        public static DataTable ktheGjitheOreShteseSipasPunonjesitDhePeriudhesAll(int idpunonjes, string muaji, int viti, string[] arrKodeKomp)
        {
            using (var dbKodifikimKf = new clsDatabazeListPagesa())
            {
                return dbKodifikimKf.ktheGjitheOreShteseSipasPunonjesitDhePeriudhesAll(idpunonjes, muaji, viti, arrKodeKomp);

            }
        }

        #endregion


        #region Metoda Private

        private bool mbushOreShtese(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsOreShtese grupKF = new clsOreShtese();
                //grupKF.mbushOreShtese(rreshti);
                this.Add(new clsOreShtese(rreshti));
            }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        public static void Import(DataTable dt, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, int pozicionkodi, int idPerdoruesi, int idNdermarrje, int VitNdermarrje, bool mbishkruajVleratEMeparshme, ref int kodMesazhi, bool importo)
        {

            if (!importo)
            {
                KontrolloDheImporto(dt, col, gabime, tePaImportuara, pozicionkodi, idPerdoruesi, idNdermarrje, VitNdermarrje, mbishkruajVleratEMeparshme, ref kodMesazhi, importo);
                return;
            }
            using (var scope = new MyTransactionScope())
            {
                if (KontrolloDheImporto(dt, col, gabime, tePaImportuara, pozicionkodi, idPerdoruesi, idNdermarrje, VitNdermarrje, mbishkruajVleratEMeparshme, ref kodMesazhi, importo))
                {
                    scope.Complete();
                }
            }
            


        }

        private static bool KontrolloDheImporto(DataTable dt, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, int pozicionkodi, int idPerdoruesi, int idNdermarrje, int VitNdermarrje, bool mbishkruajVleratEMeparshme, ref int kodMesazhi, bool importo)
        {
            string error = "";
            bool sukses = true;
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                var list = new clsOreShtese();
                string kodi = "", emer = "", mbiemer = "", komponente = "", muaji = "", muajilp = "";
                decimal totali = 0;
                int viti = 0, vitilp = 0;
                DateTime ngaora = new DateTime();
                DateTime neora = new DateTime();
                DateTime data = new DateTime();


                foreach (var trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Kod punonjesi":
                            kodi = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Emer":
                            emer = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Mbiemer":
                            mbiemer = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Nga ora":
                            ngaora = clsFunksione.vendosOre(trup, dr, out error);
                            break;
                        case "Ne ore":
                            neora = clsFunksione.vendosOre(trup, dr, out error);
                            break;
                        case "Data":
                            data = clsFunksione.vendosDate(trup, dr, out error);
                            break;
                        case "Totali":
                            totali = DbCore.clsFunksione.vendosDecimal(trup, dr, out error);
                            break;
                        case "Komponente":
                            komponente = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Muaji":
                            muaji = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Muaji Listpagesa":
                            muajilp = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Viti":
                            viti = clsFunksione.vendosInt(trup, dr, out error);
                            break;
                        case "Viti Listpagesa":
                            vitilp = clsFunksione.vendosInt(trup, dr, out error);
                            break;
                    }
                    if (error != "")
                    {
                        object[] arr = { dr[pozicionkodi], error, i };// trup.EmerImporti + " duhet te jete numer!", i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                        break;
                    }
                }
                if (error != "")
                    continue;

                try
                {
                    list = list.krijoPerImport(clsFunksione.ktheStringunPaHapesira(kodi, true), clsFunksione.ktheStringunPaHapesira(emer, false), mbiemer, data, ngaora, neora, totali, idPerdoruesi, idPerdoruesi, idNdermarrje, 1, komponente, muaji, viti, muajilp, vitilp, VitNdermarrje, !mbishkruajVleratEMeparshme);


                    if (importo)
                    {
                        clsMesazh mesazhinv = list.ruaj();
                        if (!mesazhinv.Status)
                        {

                            object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                            gabime.Rows.Add(arr);
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            sukses = false;
                            break;
                        }
                    }

                }
                catch (MyException myex)
                {
                    if (kodMesazhi == 0)
                        kodMesazhi = Converter.MerrVlereOseDefault<bool>(myex.Data?["EkzistonRekordi"]) ? 100 : -1;
                    error = myex.Message;
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex);
                    error = ex.Message;
                    sukses = false;
                    kodMesazhi = 200;
                    break;
                }

                finally
                {

                    if (error != "")
                    {
                        object[] arr = { dr[pozicionkodi], error, i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                    }
                    i++;
                }

            }


            return sukses;
        }

        #endregion

    }
}

