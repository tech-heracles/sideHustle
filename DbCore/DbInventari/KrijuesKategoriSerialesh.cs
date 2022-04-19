using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbInventari
{
    public class KrijuesKategoriSerialesh
    {
        public static void MbushKategoriSerialesh(colSerialeUnikeKategori serialeKategori, colSerialeUnikeMagazina serialet, int idNdermarrje, clsArtikulli artikulli, clsArtikulli artikullSet, bool shfaqSerialKryesor, int idMag, bool sasiNegative, string serialiKryesore,  string serialiDytesor = "", string cardSerialNo = "", string phoneSerialNo = "",
                string usercode = "", string airtime = "", string shitBatch = "", string batchPerPack = "", string cardsPerBatch = "", string cardPartNo = "")
        {
            clsSerialeUnikeKategori kategoriSeriali;
            int idFormati = 0;
            if (string.IsNullOrEmpty(serialiDytesor))
                kategoriSeriali = serialeKategori.merrKategoriSipasSerialitUnik(serialiKryesore, out idFormati);
            else
                kategoriSeriali = serialeKategori.merrKategoriSipasSerialitUnik(serialiDytesor, out idFormati);

            clsSerialeUnikeMagazina seriali;

            if (kategoriSeriali.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString()))
                seriali = new clsSerialeUnikeAparate(idNdermarrje, kategoriSeriali.ID, idFormati, serialiKryesore, artikulli, artikullSet, shfaqSerialKryesor, idMag);
            else if (kategoriSeriali.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.KARTA.ToString()))
                seriali = new clsSerialeUnikeKarta(idNdermarrje, kategoriSeriali.ID, idFormati, serialiKryesore, artikulli, artikullSet, serialiDytesor, cardSerialNo, phoneSerialNo, usercode, airtime, shfaqSerialKryesor, idMag);
            else if (kategoriSeriali.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.RINGARKUES.ToString()))
            {
                var format = kategoriSeriali.ColFormateSeriali.Find(x => x.ID == idFormati);
                seriali = new clsSerialeUnikeRingarkues(idNdermarrje, kategoriSeriali.ID, idFormati, serialiKryesore, artikulli, artikullSet, "100", "10", "10", cardPartNo, shfaqSerialKryesor, idMag, format.ColSeriale.FirstOrDefault().FormuleSpecifike);
            }
            else
                seriali = new clsSerialeUnikeAparate(idNdermarrje, kategoriSeriali.ID, idFormati, serialiKryesore, artikulli, artikullSet, shfaqSerialKryesor, idMag);

            if (sasiNegative)
                seriali.Sasia *= -1;

            serialet.Add(seriali);
        }

        public static clsSerialeUnikeMagazina krijoKategoriSerialesh(colSerialeUnikeKategori serialeKategori, IDataRecord record)
        {
            string serialiDytesor = record["SERIALI_DYTESOR"].ToString();
            clsSerialeUnikeKategori kategoriSeriali;

            if (!string.IsNullOrEmpty(serialiDytesor))
                kategoriSeriali = serialeKategori.merrKategoriSipasSerialitUnik(serialiDytesor, out _);
            else
                kategoriSeriali = serialeKategori.merrKategoriSipasSerialitUnik(record["SERIALI_KRYESOR"].ToString(), out _);

            if (kategoriSeriali.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString()))
                return new clsSerialeUnikeAparate(record);

            if (kategoriSeriali.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.KARTA.ToString()))
                return new clsSerialeUnikeKarta(record);

            if (kategoriSeriali.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.RINGARKUES.ToString()))
                return new clsSerialeUnikeRingarkues(record);

            return new clsSerialeUnikeAparate(record);
        }
    
        public static colSerialeUnikeMagazina KrijoSeriale(colSerialeUnikeKategori serialeKategori, int idNdermarrje, clsArtikulli artikulliISerialit, DataTable serialet, colArtikulliPerberes artPer, bool eshteSerialKryesor, int idMag, bool sasiNegative)
        {
            var serialeTeReja = new colSerialeUnikeMagazina();
            var artCache = new colArtikujt
            {
                artikulliISerialit
            };
            foreach (DataRow row in serialet.Rows)
            {
                clsArtikulli artikulliSet = null;
                if (artPer != null)
                {
                    double sasi = Convert.ToDouble(row["SASI"]);
                    foreach (var aP in artPer)
                    {
                        if (aP.getIdMag() == idMag && aP.IdLidheseArt == artikulliISerialit.IdArtikulli && aP.getSasiENevojshme() >= sasi)
                        {
                            aP.shtoSasiPlotesuar((float)sasi);
                            artikulliSet = artCache.MerrArtikull(aP.IdArtikulliKryesor);
                            break;
                        }
                    }
                }

                MbushKategoriSerialesh(serialeKategori, serialeTeReja, idNdermarrje, artikulliISerialit, artikulliSet, eshteSerialKryesor, idMag, sasiNegative, row["SERIALI_KRYESOR"].ToString(), row["SERIALI_DYTESOR"].ToString());
            }

            return serialeTeReja;
        }
    
        public static void MbushKategoriSerialesh(colSerialeUnikeMagazina serialet, int idNdermarrje, Dictionary<string, string> fushat, clsSerialeUnikeKategori kategoria, clsArtikulli artikull, clsArtikulli artSet, int idMag)
        {
            string serialiKryesore;
            string serialiDytesor;
            string cardSerialNo;
            string phoneSerialNo;
            string usercode;
            string airtime;
            string shitBatch;
            string batchPerPack;
            string cardsPerBatch;
            string cardPartNo;
            int idFormati = artikull.IdFormatSeriali;
            fushat.TryGetValue("SerialiKryesore", out serialiKryesore);
            var formati = kategoria.ColFormateSeriali?.Find(formate => formate.ID == idFormati);
            if(formati == null)
                throw new MyException(MessagesResource.Messages["msgSerialiNukIPerketAsnjeFormati"].Replace("XXX", serialiKryesore));

            if (string.IsNullOrWhiteSpace(serialiKryesore))
                throw new MyException(MessagesResource.Messages["serialiKryesorNukMundTeJeteBOsh"]);
            

            if (!formati.ValidoSerialSipasFormatit(serialiKryesore, true))
                throw new MyException(MessagesResource.Messages["msgSerialiNukIPerketAsnjeFormati"].Replace("XXX", serialiKryesore));

            if (kategoria.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString()))
            {
                serialet.Add(new clsSerialeUnikeAparate(idNdermarrje, kategoria.ID, idFormati, serialiKryesore, artikull, artSet, true, idMag));
            }
            else if (kategoria.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.KARTA.ToString()))
            {
                if (!fushat.TryGetValue("SerialiDytesor", out serialiDytesor))
                    serialiDytesor = "";
                if (!fushat.TryGetValue("CardSerialNo", out cardSerialNo))
                    cardSerialNo = "";
                if (!fushat.TryGetValue("PhoneSerialNo", out phoneSerialNo))
                    phoneSerialNo = "";
                if (!fushat.TryGetValue("Usercode", out usercode))
                    usercode = "";
                if (!fushat.TryGetValue("Airtime", out airtime))
                    airtime = "";
                if (fushat.ContainsKey("SerialiDytesor") && string.IsNullOrWhiteSpace(serialiDytesor) && formati.ColSeriale.Count > 1)
                    throw new MyException(MessagesResource.Messages["msgSerialiDytesorNukMundTeJeteBosh"]);

                if (!string.IsNullOrWhiteSpace(serialiDytesor) && !formati.ValidoSerialSipasFormatit(serialiDytesor, false))
                {
                    throw new MyException(MessagesResource.Messages["msgSerialiNukIPerketAsnjeFormati"].Replace("XXX", serialiDytesor));
                }
                
                serialet.Add(new clsSerialeUnikeKarta(idNdermarrje, kategoria.ID, idFormati, serialiKryesore, artikull, artSet, serialiDytesor, cardSerialNo, phoneSerialNo, usercode, airtime, true, idMag));
            }
            else if (kategoria.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.RINGARKUES.ToString()))
            {
                if (!fushat.TryGetValue("ShitBatch", out shitBatch))
                    shitBatch = "100";
                if (!fushat.TryGetValue("BatchPerPack", out batchPerPack))
                    batchPerPack = "10";
                if (!fushat.TryGetValue("CardsPerBatch", out cardsPerBatch))
                    cardsPerBatch = "10";
                if (!fushat.TryGetValue("CardPartNo", out cardPartNo))
                    cardPartNo = "";
                serialet.Add(new clsSerialeUnikeRingarkues(idNdermarrje, kategoria.ID, idFormati, serialiKryesore, artikull, artSet, shitBatch, batchPerPack, cardsPerBatch, cardPartNo, true, 0, formati.ColSeriale.FirstOrDefault().FormuleSpecifike));
            }
            else
            {
                serialet.Add(new clsSerialeUnikeAparate(idNdermarrje, kategoria.ID, idFormati, serialiKryesore, artikull, artSet, true, idMag));
            }
        }
    }
}
