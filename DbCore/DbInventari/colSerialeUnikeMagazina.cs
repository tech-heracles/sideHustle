using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Extensions;
using Newtonsoft.Json;
using DbCore.DbRegjistrim;

namespace DbCore.DbInventari
{
    public class colSerialeUnikeMagazina : List<clsSerialeUnikeMagazina>, IDataBase
    {
        private colSerialeUnikeKategori serialeKategori;

        #region Konstruktor

        public colSerialeUnikeMagazina()
        {

        }
        public colSerialeUnikeMagazina(IEnumerable<clsSerialeUnikeMagazina> col)
        {
            AddRange(col);
        }

        public colSerialeUnikeMagazina(int idKokaMagazina, int idNdermarrje)
        {
            serialeKategori = new colSerialeUnikeKategori(idNdermarrje);
            using (clsDatabaseInventari serialUnikLidhjeMagazine = new clsDatabaseInventari())
                serialUnikLidhjeMagazine.mbushSerialeUnikeLidhjeMagSipasIdKokaMag(idKokaMagazina, this);
        }

        #endregion

        #region Metoda Publike

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            Add(KrijuesKategoriSerialesh.krijoKategoriSerialesh(serialeKategori, record));
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Ruaj(int idTrupi, int idKoka, int idLlojDokumentiMagazine, float cmimi)
        {
            foreach (clsSerialeUnikeMagazina rreshtMagazine in this)
                rreshtMagazine.shtoParametratKokaTrup(idTrupi, idKoka, idLlojDokumentiMagazine, cmimi);
            return Ruaj();
        }

        public clsMesazh Ruaj()
        {
            clsMesazh mesazh = new MesazhGabimi();
            foreach (clsSerialeUnikeMagazina rreshtMagazine in this)
            {
                mesazh = rreshtMagazine.Ruaj();
                if (!mesazh.Status)
                    return mesazh;
            }
            return mesazh;
        }

        public void shtoParametraMagazine(int idTrupi, int idKoka, int idLlojDokumentiMagazine, float cmimi)
        {
            foreach (clsSerialeUnikeMagazina rreshtMagazine in this)
                rreshtMagazine.shtoParametratKokaTrup(idTrupi, idKoka, idLlojDokumentiMagazine, cmimi);
        }

        public static string MerrSerialetEkzistues(colSerialeUnikeMagazina seriale, bool serialKryesor)
        {
            IEnumerable<string> serialeNeGride;
            if (serialKryesor)
                serialeNeGride = seriale.Select(s => s.MerrSerialKryesorDheSerialetETijPerberes()).Distinct();
            else
                serialeNeGride = seriale.Select(s => s.MerrSerialDytesor()).Distinct();

            string serialeEkzistuese = string.Empty;
            foreach (var s in serialeNeGride)
            {
                serialeEkzistuese = $"{serialeEkzistuese}'{s}',";
            }
            return serialeEkzistuese.TrimEnd(',');
        }

        public DataTable KtheNeDataTable()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(new[] {
                new DataColumn("ID", typeof(decimal)),
                new DataColumn("ID_TRUPI_MAGAZINE", typeof(decimal)),
                new DataColumn("ID_KOKA_MAGAZINE", typeof(decimal)),
                new DataColumn("ID_KATEGORI_SERIALI", typeof(decimal)),
                new DataColumn("ID_ARTIKULLI", typeof(decimal)),
                new DataColumn("IDLLOJDOKUMENTIMAGAZINE", typeof(decimal)),
                new DataColumn("CMIMI", typeof(float)),
                new DataColumn("ID_TVSH", typeof(decimal)),
                new DataColumn("SASIA", typeof(float)),
                new DataColumn("SERIALI_KRYESOR", typeof(string)),
                new DataColumn("SERIALI_DYTESOR", typeof(string)),
                new DataColumn("ID_SETI", typeof(decimal)),
                new DataColumn("CARDSERIALNO", typeof(string)),
                new DataColumn("PHONESERIALNO", typeof(string)),
                new DataColumn("USERCODE", typeof(string)),
                new DataColumn("AIRTIME", typeof(int)),
                new DataColumn("SHITBATCH", typeof(string)),
                new DataColumn("BATCHPERPACK", typeof(string)),
                new DataColumn("CARDSPERBATCH", typeof(string)),
                new DataColumn("CARDPARTNO", typeof(string)),
                new DataColumn("ID_FORMAT_SERIALI", typeof(decimal)),
                new DataColumn("SHFAQSERIALKRYESORNEGRIDE", typeof(bool))
            });
            ForEach(x =>
            {
                dt.Rows.Add(x.FillDataRow(dt.NewRow()));
            });
            return dt;
        }

        public object NdertoTrupinEGrides(int idNdermarrje)
        {
            return NdertoTrupinEGrides(new colSerialeUnikeKategori(idNdermarrje), "");
        }

        public object NdertoTrupinEGrides(colSerialeUnikeKategori kategori, string serialPerTuPerjashtuar)
        {
            var idArtikuj = this.Where(x => x.IdArtikulli > 0).Select(x => x.IdArtikulli).Union(this.Where(x => x.IdSeti > 0).Select(y => y.IdSeti)).Distinct();

            colArtikujt artikujt = new colArtikujt(idArtikuj.ToList());

            return from seriale in this.Where(s => (s.ShfaqSerialKryesorNeGride ? s.SerialiKryesore : s.MerrSerialDytesor()) != serialPerTuPerjashtuar)
                   join kat in kategori on seriale.IdKategoriSeriali equals kat.ID
                   let art = artikujt.FirstOrDefault(x => x.IdArtikulli == seriale.IdArtikulli)
                   let set = artikujt.FirstOrDefault(x => x.IdArtikulli == seriale.IdSeti)
                   orderby art.KodArtikulli, seriale.SerialiKryesore
                   select new
                   {
                       Seriali = seriale.ShfaqSerialKryesorNeGride ? seriale.SerialiKryesore : seriale.MerrSerialDytesor(),
                       Artikulli = art?.KodArtikulli,
                       Seti = set?.KodArtikulli,
                       Kategoria = kat.Kategori,
                       Sasia = seriale.Sasia
                   };
        }

        public bool GjendetSerialiNeTrup(string seriali)
        {
            return this.Exists(s => s.EshteSerialINjejte(seriali, true) || s.EshteSerialINjejteDytesor(seriali));


        }

        public void ShtoSeriale(int idNdermarrje, DataTable table, colSerialeUnikeFusha Fushat, clsSerialeUnikeKategori kategoria)
        {
            colArtikujt ArtikujtSet = new colArtikujt();
            clsArtikulli artSet = new clsArtikulli();
            var emertimiArtikullit = Fushat.Find(cls => cls.Fusha == "Artikulli").Emertimi;
            var emertimiArtikullSet = Fushat.Find(cls => cls.Fusha == "ArtikullSet");

            //grupon artikujt
            var Artikujt = grupoArtikujSerialeUnike(table, idNdermarrje, emertimiArtikullit);

            KontrolloKategoriSerialeshArtikulli(Artikujt, kategoria);

            //grupon artikujt set 
            if (emertimiArtikullSet != null)
                ArtikujtSet = grupoArtikujSerialeUnike(table, idNdermarrje, emertimiArtikullSet.Emertimi);


            foreach (DataRow row in table.Rows)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                Fushat.ForEach(fush =>
                {
                    dictionary.Add(fush.Fusha, row[fush.Emertimi].ToString());
                });

                var art = Artikujt.Find(cls => cls.KodArtikulli.EqualsIgnoreCase(dictionary["Artikulli"]));
                if (emertimiArtikullSet != null)
                    artSet = ArtikujtSet.Find(cls => cls.KodArtikulli.EqualsIgnoreCase(dictionary["ArtikullSet"]));
                KrijuesKategoriSerialesh.MbushKategoriSerialesh(this, idNdermarrje, dictionary, kategoria, art, artSet, 0);
            }
        }

        public void ShtoSerialNgaImporti(DataTable data, Dictionary<string, string> fushaSerialesh, colSerialeUnikeKategori kategori, int idNdermarrje, bool meMagazine)
        {
            var serialet = MerrSerialet(data, fushaSerialesh);
            var Artikujt = grupoArtikujSerialeUnike(data, idNdermarrje, fushaSerialesh["Kodi"]);
            colArtikujt ArtikujtSet = new colArtikujt();
            clsArtikulli artSet = new clsArtikulli();
            if (fushaSerialesh.ContainsKey("Artikulli Set"))
                ArtikujtSet = grupoArtikujSerialeUnike(data, idNdermarrje, fushaSerialesh["Artikulli Set"]);

            KontrolloKategoriSerialeshArtikulli(Artikujt, kategori);
            colNjesiAdministrative njesite = new colNjesiAdministrative(serialet.Select(x => x.Magazina).Distinct().ToList(), idNdermarrje);

            foreach (var kategoria in kategori)
            {
                var ArtikujPerKategori = Artikujt.Where(x => kategoria.ColFormateSeriali.Find(format => format.ID == x.IdFormatSeriali) != null).ToList();

                var serialePerKategori = serialet.Where(x => ArtikujPerKategori.Exists(art => art.KodArtikulli.EqualsIgnoreCase(x.Artikulli)));

                colSerialeUnikeFusha Fushat = new colSerialeUnikeFusha(kategoria.ID);
                foreach (var serial in serialePerKategori)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();

                    Fushat.ForEach(fush =>
                    {
                        switch (fush.Fusha)
                        {
                            case "SerialiKryesore":
                                dictionary.Add(fush.Fusha, serial.SerialiUnikKryesor);
                                break;
                            case "SerialiDytesor":
                                dictionary.Add(fush.Fusha, serial.SerialiUnikDytesor);
                                break;
                            default:
                                dictionary.Add(fush.Fusha, "");
                                break;
                        }

                    });
                    var art = Artikujt.Find(cls => cls.KodArtikulli.EqualsIgnoreCase(serial.Artikulli));
                    if (fushaSerialesh.ContainsKey("Artikulli Set"))
                        artSet = ArtikujtSet.Find(cls => cls.KodArtikulli.EqualsIgnoreCase(serial.ArtikulliSet));
                    var njesia = njesite.Find(x => x.Kodi == serial.Magazina);
                    KrijuesKategoriSerialesh.MbushKategoriSerialesh(this, idNdermarrje, dictionary, kategoria, art, artSet, meMagazine || njesia == null ? 0 : njesia.IdNjesiAdministrative);
                }

            }
        }

        public static DataTable MerrRaportinGjendjaEMagazines(string DtDok = "", string DtDok1 = "", string DtDok2 = "", string DtRegjistrimi = "", string NrLlogari = "", string KlientFurnitor = "", string KodifikimArtP = "", string KodifikimArtD = "", string KodifikimArtT = "", string Magazina = "", string Kartela = "", string IdNdermarrje = "", string IdRaport = "", string NjesiArtikulli = "", string FurnitorArt = "", string DegeAdministrative = "", string SipasGjendjes = "", string PershkrimArt = "", string LlojArt = "", string Kodbar = "", string ArtikullAktiv = "", string GrupoSipasArtikullit = "", string IdPerdoruesi = "")
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.MerrRaportinGjendjaEMagazines(DtDok,DtDok1, DtDok2, DtRegjistrimi, NrLlogari, KlientFurnitor, KodifikimArtP, KodifikimArtD, KodifikimArtT, Magazina, Kartela, IdNdermarrje, IdRaport, NjesiArtikulli, FurnitorArt, DegeAdministrative, SipasGjendjes, PershkrimArt, LlojArt, Kodbar, ArtikullAktiv, GrupoSipasArtikullit, IdPerdoruesi);
            }
        }

        private List<ArtikujMeSeriale> MerrSerialet(DataTable data, Dictionary<string, string> fushaSerialesh)
        {
            var lista = new List<ArtikujMeSeriale>();
            foreach (DataRow row in data.Rows)
            {
                var Art = new ArtikujMeSeriale();
                Art.Artikulli = Art.GetValue(row, fushaSerialesh, "Kodi");
                Art.ArtikulliSet = Art.GetValue(row, fushaSerialesh, "Artikulli Set");
                Art.Magazina = Art.GetValue(row, fushaSerialesh, "Magazina");
                Art.SerialiUnikKryesor = Art.GetValue(row, fushaSerialesh, "Seriali Unik Kryesor");
                Art.SerialiUnikDytesor = Art.GetValue(row, fushaSerialesh, "Seriali Unik Dytesor");
                lista.Add(Art);
            }

            return lista;
        }

        private colArtikujt grupoArtikujSerialeUnike(DataTable table, int idNdermarrje, string emertimi)
        {
            var objArt = from row in table.AsEnumerable()
                         group row by row.Field<string>(emertimi) into grp
                         select new
                         {
                             Artikulli = grp.Key
                         };
            objArt = objArt.Where(x => x.Artikulli != null);
            colArtikujt Artikujt = new colArtikujt(objArt.Select(x => x.Artikulli).ToList(), idNdermarrje);
            if (Artikujt.Count != objArt.Count())
            {
                foreach (var art in objArt.Select(x => x.Artikulli).ToList())
                    if (!Artikujt.Exists(x => x.KodArtikulli == art))
                        throw new Exception($"Artikulli me kod {art} nuk ekziston!");
            }
            return Artikujt;
        }

        private void KontrolloKategoriSerialeshArtikulli(colArtikujt Artikujt, colSerialeUnikeKategori kategori)
        {
            foreach (var art in Artikujt)
            {
                if (art.IdFormatSeriali == 0)
                    continue;
                bool exists = false;
                foreach (var kategoria in kategori)
                {
                    if (kategoria.ColFormateSeriali.Exists(format => format.ID == art.IdFormatSeriali))
                    {
                        exists = true;
                        break;
                    }

                }
                if (!exists)

                    throw new MyException(MessagesResource.Messages["artikulliJoILidhurMeNdonjeFormat"].Replace("#KODARTIKULLI", art.KodArtikulli));
            }
        }

        private void KontrolloKategoriSerialeshArtikulli(colArtikujt Artikujt, clsSerialeUnikeKategori kategoria)
        {
            foreach (var art in Artikujt)
            {
                if (!kategoria.ColFormateSeriali.Exists(format => format.ID == art.IdFormatSeriali))
                    throw new MyException(MessagesResource.Messages["artikulliJoILidhurMeFormat"].Replace("#KODARTIKULLI", art.KodArtikulli).Replace("#KATEGORIA", kategoria.Kategori));
            }
        }

        public clsMesazh KontrolloSerialeTePerseritur(int idKategoriRingarkues)
        {
            var serialeDyfish = this.Where(x => x.IdKategoriSeriali != idKategoriRingarkues).GroupBy(ac => ac.SerialiKryesore).Select(ac => new { Seriali = ac.Key, Sasia = ac.Count() }).Where(s => s.Sasia > 1);
            if (serialeDyfish.Count() > 0)
                return new MesazhGabimi(MessagesResource.Messages["serialiNdodhetNeDokument"].Replace("XXX", serialeDyfish.FirstOrDefault().Seriali));

            var serialeDytesoreDyfish = this.Where(x => !string.IsNullOrEmpty(x.MerrSerialDytesor())).GroupBy(ac => ac.MerrSerialDytesor())
                .Select(ac => new { Seriali = ac.Key, Sasia = ac.Count() }).Where(s => s.Sasia > 1);
            if (serialeDytesoreDyfish.Count() > 0)
                return new MesazhGabimi(MessagesResource.Messages["serialiNdodhetNeDokument"].Replace("XXX", serialeDytesoreDyfish.FirstOrDefault().Seriali));

            var ringarkuesdyfish = this.Where(x => x.IdKategoriSeriali == idKategoriRingarkues).Select(x => x.MerrSerialKryesorDheSerialetETijPerberesSiListe()).Merge().GroupBy(x => x).Select(ac => new { Seriali = ac.Key, Sasia = ac.Count() }).Where(s => s.Sasia > 1);

            if (ringarkuesdyfish.Count() > 0)
                return new MesazhGabimi(MessagesResource.Messages["serialiNdodhetNeDokument"].Replace("XXX", ringarkuesdyfish.FirstOrDefault().Seriali));




            return new MesazhSuksesi();
        }

        public List<clsArtikujMeSasi> MerrArtikujMeSasi(string artikujMeSasi, bool KontrolloSasi, bool ngaImportSerialesh, bool merrSete)
        {
            var artikuj = clsArtikujMeSasi.Grupo(JsonConvert.DeserializeObject<List<clsArtikujMeSasi>>(artikujMeSasi));
            if (!KontrolloSasi)
                return artikuj;

            colNjesiAdministrative col = new colNjesiAdministrative(this.Select(x => x.IdMag).Distinct().ToList());
            foreach (var serial in this)
            {
                clsArtikujMeSasi art;
                if (ngaImportSerialesh)
                    art = artikuj.Find(x => x.IdArtikulli == serial.IdArtikulli && serial.IdSeti == 0);
                //else if (merrSete)
                //    art = artikuj.Find(x => x.IdArtikulli == serial.IdArtikulli && x.Mag == col.Find(m => m.IdNjesiAdministrative == serial.IdMag)?.Kodi);
                else
                    art = artikuj.Find(x => x.IdArtikulli == serial.IdArtikulli && x.Mag == col.Find(m => m.IdNjesiAdministrative == serial.IdMag)?.Kodi && serial.IdSeti == 0);
                if (art == null)
                    if (ngaImportSerialesh)
                        throw new DbCore.MyException(MessagesResource.Messages["artikulliISerialitNukGjendetNeGride"].Replace("#SERIALI", serial.SerialiKryesore).Replace("#ARTIKULL", clsArtikulli.ktheKodArtikulliSipasId(serial.IdArtikulli)));
                    else continue;
                art.Sasi -= serial.Sasia;
                if (art.Sasi < 0 && ngaImportSerialesh)
                    throw new DbCore.MyException(MessagesResource.Messages["nukMundTeTejkaloshsAsine"].Replace("#ARTIKULLI", clsArtikulli.ktheKodArtikulliSipasId(art.IdArtikulli)));
            }

            return artikuj;
        }

        public colSerialeUnikeMagazina Clone()
        {
            var clone = new colSerialeUnikeMagazina();
            foreach (var serial in this)
                clone.Add(serial.Clone());

            return clone;
        }

        public clsSerialeUnikeMagazina GjejSerial(string seriali, bool kryesor)
        {
            return this.Find(s => (kryesor && s.SerialiKryesore == seriali) || (!kryesor && s.MerrSerialDytesor() == seriali));
        }

        public bool KontrolloSerialet(List<Tuple<int, string>> detajime, bool strict)
        {

            foreach (var serial in this)
            {
                if (!strict && !detajime.Exists(x => x.Item1 == serial.IdArtikulli))
                    continue;
                bool uGjet = false;
                foreach (var imei in detajime)
                {
                    if (imei.Item2 == serial.SerialiKryesore && serial.IdArtikulli == imei.Item1)
                    {
                        uGjet = true;
                        break;
                    }


                }
                if (!uGjet)
                    return false;
            }
            return true;
        }

        public List<int> MerrIdArtikujt()
        {
            return this.Select(x => x.IdArtikulli).Union(this.Where(x => x.IdSeti > 0).Select(x => x.IdSeti)).Distinct().ToList();
        }

        public List<Tuple<int, int>> MerrIdArtikujMeArtikujSet()
        {
            return this.Select(x => new { IdArtikulli = x.IdArtikulli, IdSeti = x.IdSeti }).Distinct().Select(x => new Tuple<int, int>(x.IdArtikulli, x.IdSeti)).ToList();
        }

        public static DataTable MerrRaportinGjendjaEArtikujveMeSeriale(int idNdermarrje, string dtMbarimi, string filter, string filterNrSeriale)
        {
            using (clsDatabaseInventari serialUnikLidhjeMagazine = new clsDatabaseInventari())
                return serialUnikLidhjeMagazine.MerrRaportinGjendjaEArtikujveMeSeriale(idNdermarrje, dtMbarimi, filter, filterNrSeriale);
        }

        #endregion
    }
}
