using System;
using System.Data;
using System.Linq;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbBuxheti
{
    public class ClsBTrupiBuxheti : IDataBase
    {
        #region Atributes 
        public int IdBuxhetiTrupi { get; set; }
        public int IdBuxhetiKoka { get; set; }
        public int IdNdermarrje { get; set; }
        public int IdKategoriBuxhetimi { get; set; }
        public decimal VleraPlanifikuar { get; set; }
        public decimal Vlera { get; set; }
        public int IdTrupiKonvertimiNga { get; set; }
        public string LlojKonvertimiNga { get; set; }
        public decimal VleraKategorise { get; set; }
        public int IdBuxheti { get; set; }
        public bool LlogaritNeGjendje { get; set; }
        public int IdLlojPeriudhe { get; set; }
        public int Periudha { get; set; }
        public int IdObjekti { get; set; }
        public int LlojObjekti { get; set; }
        public int IdTvsh { get; set; }
        public decimal VleraPaTvsh { get; set; }
        public decimal? Cmimi { get; set; }
        public decimal? Sasia { get; set; }
        #endregion

        #region Konstruktoret
        public ClsBTrupiBuxheti()
        {
        }

        public ClsBTrupiBuxheti(int idBuxhetiTrupi, int idBuxhetiKoka, int idNdermarrje, int idKategoriBuxhetimi, decimal vleraPlanifikuar, decimal vlera, int idTrupiKonvertimiNga, string llojKonvertimiNga, decimal vleraKategorise, int idBuxheti, bool llogaritNeGjendje, int idLlojPeriudhe, int periudha, int idObjekti, int llojObjekti, int idTvsh, decimal vleraPaTvsh, decimal? cmimi, decimal? sasia)
        {
            IdBuxhetiTrupi = idBuxhetiTrupi;
            IdBuxhetiKoka = idBuxhetiKoka;
            IdNdermarrje = idNdermarrje;
            IdKategoriBuxhetimi = idKategoriBuxhetimi;
            VleraPlanifikuar = vleraPlanifikuar;
            Vlera = vlera;
            IdTrupiKonvertimiNga = idTrupiKonvertimiNga;
            LlojKonvertimiNga = llojKonvertimiNga;
            VleraKategorise = vleraKategorise;
            IdBuxheti = idBuxheti;
            LlogaritNeGjendje = llogaritNeGjendje;
            IdLlojPeriudhe = idLlojPeriudhe;
            Periudha = periudha;
            IdObjekti = idObjekti;
            LlojObjekti = llojObjekti;
            IdTvsh = idTvsh;
            VleraPaTvsh = vleraPaTvsh;
            Cmimi = cmimi;
            Sasia = sasia;
        }

        public ClsBTrupiBuxheti(int idBuxhetiTrupi)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.ktheBTrupiBuxhetiSipasId(idBuxhetiTrupi, this);
        }

        public ClsBTrupiBuxheti(IDataRecord record)
        {
            Mbush(record);
        }
        #endregion

        #region Private
        private clsMesazh RuajTrupiBuxheti()
        {
            var mesazh = new clsMesazh();

            using (var db = new ClsDatabaseBuxheti())
                mesazh = db.RuajTrupiBuxheti(this);

            return mesazh;
        }
        #endregion

        #region Public
        public void Mbush(IDataRecord record)
        {
            IdBuxhetiTrupi = !Convert.IsDBNull(record["IDBUXHETITRUPI"]) ? Convert.ToInt32(record["IDBUXHETITRUPI"]) : 0;
            IdBuxhetiKoka = !Convert.IsDBNull(record["IDBUXHETIKOKA"]) ? Convert.ToInt32(record["IDBUXHETIKOKA"]) : 0;
            IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0;
            IdKategoriBuxhetimi = !Convert.IsDBNull(record["IDKATEGORIBUXHETIMI"]) ? Convert.ToInt32(record["IDKATEGORIBUXHETIMI"]) : 0;
            VleraPlanifikuar = !Convert.IsDBNull(record["VLERAPLANIFIKUAR"]) ? Convert.ToDecimal(record["VLERAPLANIFIKUAR"]) : 0;
            Vlera = !Convert.IsDBNull(record["VLERA"]) ? Convert.ToDecimal(record["VLERA"]) : 0;
            IdTrupiKonvertimiNga = !Convert.IsDBNull(record["IDTRUPIKONVERTIMINGA"]) ? Convert.ToInt32(record["IDTRUPIKONVERTIMINGA"]) : 0;
            LlojKonvertimiNga = !Convert.IsDBNull(record["LLOJKONVERTIMINGA"]) ? Convert.ToString(record["LLOJKONVERTIMINGA"]) : String.Empty; 
            VleraKategorise = !Convert.IsDBNull(record["VLERAKATEGORISE"]) ? Convert.ToDecimal(record["VLERAKATEGORISE"]) : 0; 
            IdBuxheti = !Convert.IsDBNull(record["IDBUXHETI"]) ? Convert.ToInt32(record["IDBUXHETI"]) : 0; 
            LlogaritNeGjendje = !Convert.IsDBNull(record["LLOGARITNEGJENDJE"]) ? Convert.ToBoolean(record["LLOGARITNEGJENDJE"]) : false;
            IdLlojPeriudhe = !Convert.IsDBNull(record["IDLLOJPERIUDHE"]) ? Convert.ToInt32(record["IDLLOJPERIUDHE"]) : 0;
            Periudha = !Convert.IsDBNull(record["PERIUDHA"]) ? Convert.ToInt32(record["PERIUDHA"]) : 0; 
            IdObjekti = !Convert.IsDBNull(record["IDOBJEKTI"]) ? Convert.ToInt32(record["IDOBJEKTI"]) : 0;
            LlojObjekti = !Convert.IsDBNull(record["LLOJOBJEKTI"]) ? Convert.ToInt32(record["LLOJOBJEKTI"]) : 0;
            IdTvsh = !Convert.IsDBNull(record["IDTVSH"]) ? Convert.ToInt32(record["IDTVSH"]) : 0;
            VleraPaTvsh = !Convert.IsDBNull(record["VLERAPATVSH"]) ? Convert.ToDecimal(record["VLERAPATVSH"]) : 0;
            Cmimi = !Convert.IsDBNull(record["CMIMI"]) ? Convert.ToDecimal(record["CMIMI"]) : (decimal?)null;
            Sasia = !Convert.IsDBNull(record["SASIA"]) ? Convert.ToDecimal(record["SASIA"]) : (decimal?)null;
        }

        public clsMesazh Ruaj()
        {
            try
            {
                var mesazh = RuajTrupiBuxheti();
                return mesazh;
            } catch (Exception ex)
            {
                return new clsMesazh(false, $"Ndodhi nje gabim gjate ruajtjes se trupit dokumentit te buxhetit me id {IdBuxhetiTrupi}.\n" + ex.Message);
            }
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Internal
        
        internal clsMesazh krijoTrupDokumentBuxhetiNgaImporti(string lloji, string zeri, string buxheti, string tvsh, decimal vleraPaTVSH, decimal vleraMeTVSH, string artikullBuxhetimi, ColBKategoriBuxhetimi colKategoriBuxhetimi, ref object[] nivele, int idNdermarrje, int idPerdorues, int index, int kategoria, decimal? sasia, decimal? cmimi, string muaji, string ndermarrjeNiveli3, string ndermarrjeNiveli2, clsDatabaseAdmin dbAdmin)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, lloji, zeri, buxheti, tvsh, vleraPaTVSH, vleraMeTVSH, artikullBuxhetimi, colKategoriBuxhetimi, nivele, idNdermarrje, idPerdorues, index, kategoria, sasia, cmimi, muaji, ndermarrjeNiveli3, ndermarrjeNiveli2, dbAdmin);

            if (String.IsNullOrEmpty(lloji) && kategoria != 175 && kategoria != 170) throw new Exception("Fusha Lloji nuk duhet te jete bosh!");
            if (String.IsNullOrEmpty(zeri) && kategoria != 175 && kategoria != 170) throw new Exception("Fusha Zeri nuk duhet te jete bosh!");
            if (String.IsNullOrEmpty(buxheti)) throw new Exception("Fusha Buxheti nuk duhet te jete bosh!");
            if (String.IsNullOrEmpty(tvsh) && kategoria != 175 && kategoria != 170) throw new Exception("Fusha TVSH nuk duhet te jete bosh!");
            if (vleraPaTVSH == 0 && vleraMeTVSH == 0 && kategoria != 175 && kategoria != 170) throw new Exception("Duhet te plotesohet te pakten njera nga fushat Vlera Pa TVSH dhe Vlera Me TVSH.");
            if (vleraPaTVSH == 0 && kategoria == 175) throw new Exception("Fusha Vlera nuk duhet te jete bosh ose zero!");
            if (!(lloji.ToLower() == "llogari" || lloji.ToLower() == "artikull") && kategoria != 175 && kategoria != 170) throw new Exception("Fusha Lloji nuk eshte e sakte.");

            this.IdNdermarrje = idNdermarrje;
            this.LlogaritNeGjendje = true;
            
            this.LlojObjekti = (lloji.ToLower() == "llogari") ? 3 : (lloji.ToLower() == "artikull") ? 1 : 0;
            if (this.LlojObjekti == 3)
            {
                if (!DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(zeri, idNdermarrje))
                    throw new Exception("Llogaria me kod " + zeri + " nuk ekziston!");
                this.IdObjekti = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(zeri, idNdermarrje);
            }
            DbCore.DbInventari.clsArtikulli art = new DbInventari.clsArtikulli();
            if (this.LlojObjekti == 1)
            {
                art = new DbCore.DbInventari.clsArtikulli(zeri, idNdermarrje);
                if (art.KodArtikulli == null)
                    throw new Exception("Artikulli me kod " + zeri + " nuk ekziston!");
                if (art.Klasa != 3 && kategoria == 177)
                    throw new Exception("Artikulli me kod " + zeri + " nuk i perket klases Sherbim!");
                this.IdObjekti = art.IdArtikulli;
            }

            if (kategoria == 170)
            {
                if (string.IsNullOrEmpty(ndermarrjeNiveli3)) throw new Exception("Fusha Kodi Ndermarrje nuk duhet te jete bosh!");
                if (string.IsNullOrEmpty(ndermarrjeNiveli2)) throw new Exception("Fusha Kodi Prindi nuk duhet te jete bosh!");
                clsNdermarrje ndermPrind = new clsNdermarrje(ndermarrjeNiveli2, dbAdmin);
                if (ndermPrind.IdNdermarrje <= 0)
                    throw new Exception($"Ndermarrja me kod {ndermarrjeNiveli2} nuk ekziston!");

                clsNdermarrje ndermBije = new clsNdermarrje(ndermarrjeNiveli3, dbAdmin);
                if (ndermBije.IdNdermarrje <= 0)
                    throw new Exception($"Ndermarrja me kod {ndermarrjeNiveli3} nuk ekziston!");

                if (ndermBije.Raportuesi != ndermPrind.IdNdermarrje && artikullBuxhetimi.ToLower() != "tepacaktuara")
                    throw new Exception($"Ndermarrja me kod {ndermarrjeNiveli3} nuk ka si prind ndermarrjen me kod {ndermarrjeNiveli2}");

                this.IdNdermarrje = ndermBije.IdNdermarrje;
            }

            if (kategoria == 181 || kategoria == 179)
            {
                var kategoriBuxhetimi = colKategoriBuxhetimi.Find(x => x.Kodi == artikullBuxhetimi);
                if (kategoriBuxhetimi != null)
                    this.IdKategoriBuxhetimi = kategoriBuxhetimi.IdKategoriBuxhetimi;
                else if (this.LlojObjekti == 3)
                {
                    var kategoriPerLlogari = colKategoriBuxhetimi.FindAll(x => x.IdLlogaria == this.IdObjekti);
                    if (kategoriPerLlogari.Count == 1)
                        this.IdKategoriBuxhetimi = kategoriPerLlogari.First().IdKategoriBuxhetimi;
                }
                else if (this.LlojObjekti == 1)
                {
                    var kategoriPerLlogari = colKategoriBuxhetimi.FindAll(x => x.IdLlogaria == (art.LlojiArt == false ? art.IdLlogariBlerje : art.IdLlogariTeTrete));
                    if (kategoriPerLlogari.Count == 1)
                        this.IdKategoriBuxhetimi = kategoriPerLlogari.First().IdKategoriBuxhetimi;
                }
            }

            if (kategoria == 175 || kategoria == 170)
            {
                if (string.IsNullOrEmpty(artikullBuxhetimi)) throw new Exception("Fusha Analize/Artikull Buxheti nuk duhet te jete bosh!");
                var kategoriBuxhetimi = colKategoriBuxhetimi.Find(x => x.Kodi == artikullBuxhetimi && x.IdNdermarrje == this.IdNdermarrje);
                if (kategoriBuxhetimi == null || kategoriBuxhetimi.IdKategoriBuxhetimi <= 0)
                    throw new Exception($"Analiza/Artikulli i buxhetit me kod {artikullBuxhetimi} nuk ekziston!");

                this.IdKategoriBuxhetimi = kategoriBuxhetimi.IdKategoriBuxhetimi;
            }

            if (kategoria == 175)
            {
                if (string.IsNullOrEmpty(muaji)) throw new Exception("Fusha Muaji nuk duhet te jete bosh!");
                int periudha = ClsBMuajt.MuajiNr(muaji);
                if (periudha <= 0)
                    throw new Exception($"Muaji {muaji} nuk ekziston!");
                this.Periudha = periudha;
            }

            DbCore.DbBuxheti.ClsBLlojBuxheti llojBuxheti = new ClsBLlojBuxheti(IMBUtils.Messages.MessagesResource.Messages, buxheti, idNdermarrje);
            if(llojBuxheti.IdLlojBuxheti <= 0)
                throw new Exception($"Buxheti me kod {buxheti} nuk ekziston!");

            this.IdBuxheti = llojBuxheti.IdLlojBuxheti;
           
            decimal normatvsh = 0;
            if (tvsh.ToLower() != "pa tvsh"){
                this.IdTvsh = DbCore.DbRegjistrim.clsTaksa.ktheIdTakse(tvsh, idNdermarrje);
                normatvsh = DbCore.DbRegjistrim.clsTaksa.ktheNormePerqindjeMeId(this.IdTvsh);
            }

            if (vleraPaTVSH != 0 && vleraMeTVSH == 0){
                this.Vlera = vleraPaTVSH * (1 + normatvsh / 100);
                this.VleraPaTvsh = vleraPaTVSH;
            }
            else if (vleraPaTVSH == 0 && vleraMeTVSH != 0){
                this.Vlera = vleraMeTVSH;
                this.VleraPaTvsh = vleraMeTVSH / (1 + normatvsh / 100);
            }
            else {
                if (vleraPaTVSH != vleraMeTVSH / (1 + normatvsh / 100))
                    throw new Exception("Fusha Vlera Me TVSH nuk eshte e sakte.");
                else {
                    this.Vlera = vleraMeTVSH;
                    this.VleraPaTvsh = vleraPaTVSH;
                }
            }

            if (kategoria == 177 || kategoria == 181 || kategoria == 179)
            {
                if (sasia * cmimi != this.VleraPaTvsh)
                    throw new Exception($"Fushat Sasia dhe Cmimi nuk perkojne me Vleren pa TVSH.");

                this.Sasia = sasia;
                this.Cmimi = cmimi;
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, lloji, zeri, buxheti, tvsh, vleraPaTVSH, vleraMeTVSH, artikullBuxhetimi, colKategoriBuxhetimi, nivele, idNdermarrje, idPerdorues, index, kategoria, sasia, cmimi, muaji, ndermarrjeNiveli3, ndermarrjeNiveli2, dbAdmin);
            return new clsMesazh(true);
        }

        #endregion
    }
}