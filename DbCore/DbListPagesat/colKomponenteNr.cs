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
    public class colKomponenteNr : System.Collections.Generic.List<clsKomponenteNr>
    {
        private List<int> _punonjesitIds;

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKomponenteNr"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKomponenteNr this[int index]
        {
            get { return ((clsKomponenteNr)base[index]); }
        }
        public colKomponenteNr(IEnumerable<clsKomponenteNr> collection) : base(collection) { }

        public colKomponenteNr(List<int> idpunonjesish, int muaji, int viti, int idKokaLp) : base(new clsDatabazeListPagesa().ktheGjitheKomponenteNrSipasPunonjesveDhePeriudhes(idpunonjesish, muaji, viti, idKokaLp))
        {
            _punonjesitIds = idpunonjesish;
        }

        public Dictionary<int, colKomponenteNr> GrupoOreShteseSipasPunonjesit()
        {
            var dicKompNr = new Dictionary<int, colKomponenteNr>(_punonjesitIds.Count);
            var kompTeGrupuar = this.GroupBy(x => x.IdPunonjesi);
            foreach (var komp in kompTeGrupuar)
            {
                dicKompNr[komp.Key] = new colKomponenteNr(komp);
            }
            return dicKompNr;
        }
        /// <summary>
        /// mbush gjithe kodifikimet e artikullit sipas ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool ktheGjitheKomponenteNrSipasNdermarrjes(int idndermarje)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            IEnumerable<clsKomponenteNr> komp = dbKodifikimKF.ktheGjitheKomponenteNrSipasNdermarrjes(idndermarje);
            this.AddRange(komp);
            dbKodifikimKF.Dispose();
            return komp != null;
        }
        public static DataTable ktheGjitheKomponenteNrExport(int idndermarje)
        {
            using (clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa())
            {
                return dbKodifikimKF.merrKomponenteNrPerExport(idndermarje);
            }

        }
        public bool ktheGjitheKomponenteNrSipasPunonjesit(int idpunonjesi)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            IEnumerable<clsKomponenteNr> komp = dbKodifikimKF.ktheGjitheKomponenteNrSipasPunonjesit(idpunonjesi);
            this.AddRange(komp);
            dbKodifikimKF.Dispose();
            return komp != null;
        }
        public static DataTable ktheGjitheKomponenteNrSipasPunonjesitDhePeriudhes(int idpunonjesi, string muaji, int viti, string kodkomponente)
        {
            using (clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa())
            {
                return dbKodifikimKF.ktheGjitheKomponenteNrSipasPunonjesitDhePeriudhes(idpunonjesi, muaji, viti, kodkomponente);

            }
        }



        public static void Import(DataTable dt, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, int pozicionkodi, int idPerdoruesi, int idNdermarrje, int VitNdermarrje,bool mbishkruajVleratEMeparshme, ref int kodMesazhi, bool importo)
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
                clsKomponenteNr list = new clsKomponenteNr();
                string kodi = "", emer = "", mbiemer = "", komponente = "", muaji = "", muajilp = "";
                decimal totali = 0;
                int viti = 0, vitilp = 0;



                foreach (clsTrupiFormatImporti trup in col)
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

                        case "Vlera":
                            totali = clsFunksione.vendosDecimal(trup, dr, out error);
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



                    list = list.krijoPerImport(clsFunksione.ktheStringunPaHapesira(kodi, true), DbCore.clsFunksione.ktheStringunPaHapesira(emer, false), mbiemer, totali, idPerdoruesi, idNdermarrje, 1, komponente, muaji, viti, muajilp, vitilp, VitNdermarrje, !mbishkruajVleratEMeparshme);


                    if (importo)
                        list.Ruaj();

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


        #region Metoda Private

        private bool mbushKomponenteNr(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsKomponenteNr grupKF = new clsKomponenteNr();
                //grupKF.mbushKomponenteNr(rreshti);
                //             this.Add(new clsKomponenteNr(rreshti));
            }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion

    }
}

