using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh colAQTSeriale (objekte per ruajtjen e serialeve te aqt-ve) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_AQTSERIALE.
    /// </summary>
    public class colAQTSeriale : List<clsAQTSeriale>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsAQTSeriale per serialet e aqt-ve.
        /// </summary>
        public colAQTSeriale()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve sipas id se artikullit qe kerkojme dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool merrAQTSerialSipasIDArtikulli(int idArtikulli, int idNdermarrje)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                bool pergjigja = merrAQTSerialSipasIDArtikulli(idArtikulli, idNdermarrje, moduliAsete);
                return pergjigja;
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve sipas id se artikullit qe kerkojme dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public static DataTable merrAQTSerialSipasIDArtikulliDt(int idArtikulli, int idNdermarrje)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return moduliAsete.ktheAQTSerialSipasIDArtikulliDt(idArtikulli, idNdermarrje);
            }
        }

        public bool merrAQTSerialSipasIDArtikulli(int idArtikulli, int idNdermarrje, clsDatabazeAsete moduliAsete)
        {

            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIDArtikulli(idArtikulli, idNdermarrje));

            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve sipas id se artikullit qe kerkojme vetem per artikuj me serial dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool merrAQTSerialSipasIDArtikulliArtikullMeSerial(int idArtikulli, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIDArtikulliPerArtikujMeSerial(idArtikulli, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve sipas id se artikullit qe kerkojme vetem per ato artikuj pa seriale dhe qe jane prind dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool merrAQTSerialSipasIDArtikulliArtikullPaSerial_VetemPrind(int idArtikulli, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIDArtikulliPerArtikujPaSerial_VetemPrind(idArtikulli, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve sipas id se ndermarrjes qe kerkojme dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool ktheAQTSerialSipasIDNdermarje(int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIDNdermarje(idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve sipas id se ndermarrjes qe kerkojme dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public static DataTable ktheAQTSerialSipasIDNdermarjeDt(int idNdermarrje)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return moduliAsete.ktheAQTSerialSipasIDNdermarjeDt(idNdermarrje);
            }
        }

        ///     <summary>
        /// MODULI ASETE:
        /// Serialet e paperdorura ne veprime te ketij artikulli
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool ktheAQTSerialSipasIDArtikulliTePaperdorura(int idArtikulli, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIDArtikulliTePaperdorura(idArtikulli, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve te perdorura sipas id se ndermarrjes dhe te artikullit qe kerkojme dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <param name="idmag">(int) Id e magazines.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool ktheAQTSerialSipasIDArtikulliDheMagazineTeperdorura(int idArtikulli, int idNdermarrje, int idmag, DateTime data)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIDArtikulliDheMagazineTeperdorura(idArtikulli, idNdermarrje, idmag, data));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve te perdorura sipas id se ndermarrjes dhe te artikullit qe kerkojme dhe mbush nje koleksion me keto objekte ne transaksion.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <param name="idmag">(int) Id e magazines.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool ktheAQTSerialSipasIDArtikulliDheMagazineTeperdorura(int idArtikulli, int idNdermarrje, int idmag, DateTime data, clsDatabazeAsete moduliAsete)
        {
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIDArtikulliDheMagazineTeperdorura(idArtikulli, idNdermarrje, idmag, data));

            return pergjigja;
        }

        public static DataTable KtheAqtSerialSipasIdArtikullDheMagazineTePerdoruraDraft(int idArtikulli, int idNdermarrje, int idmag)
        {
            using (var dbAsete = new clsDatabazeAsete())
                return dbAsete.KtheAqtSerialSipasIdArtikullDheMagazineTePerdoruraDraft(idArtikulli, idNdermarrje, idmag);
        }

        public bool ktheAQTSerialSipasIDArtikulliTeperdoruraTegjitha(int idArtikulli, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIDArtikulliTeperdoruraTegjitha(idArtikulli, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve sipas artikullit ne nje magazine te caktuar dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNjesiAdministrative">(int) Id e magazines per te cilen kerkojme serialet qe ndodhen ne te.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate  marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool merrAQTSerialSipasIDArtikulliIDMagazine(int idArtikulli, int idNjesiAdministrative, int idNdermarrje, DateTime data)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIDArtikulliIDMagazine(idArtikulli, idNjesiAdministrative, idNdermarrje, data));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve qe ndodhen ne nje magazine te caktuar dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e magazines per te cilen kerkojme serialet qe ndodhen ne te.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate  marrjes se serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool merrAQTSerialSipasIDMagazine(int idNjesiAdministrative, int idNdermarrje, DateTime data)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIDMagazine(idNjesiAdministrative, idNdermarrje, data));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr serialin sipas rreshtit dhe id se dokumentit.
        /// </summary>
        /// <param name="iddok">(int) Id e dokumentit te magazines.</param>
        /// <param name="idkonfigambjente">(int) Id e konfigurimit te ambjentit te amortizimit</param>
        /// <param name="nrrreshti">(int) Numri i rreshtit te serialit qe kerkohet.</param>
        /// <returns>Kthen True nese nuk ndodh ndonje problem gjate marrjes se serialeve te dokumentit, ne te kundert False.</returns>
        public bool ktheAQTSerialSipasIdDokumentiDheNrRreshtiNgaSerialeMagazine(int iddok, int idkonfigambjente, int nrrreshti)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasIdDokumentiDheNrRreshtiNgaSerialeMagazine(iddok, idkonfigambjente, nrrreshti));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe serialet e aqt-ve qe nuk jane perdorur akoma ne blerje sipas artikullit ne nje magazine te caktuar dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen kerkojme serialet e aqt-se.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate  marrjes se serialeve te aqt-ve qe nuk jane perdorur akoma ne blerje ose False ne te kundert.</returns>
        public bool merrAQTSerialSipasTePaHyre(int idArtikulli, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAQTSerialList(moduliAsete.ktheAQTSerialSipasTePaHyre(idArtikulli, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_AQTSERIALE ne nje list objektesh clsAQTSeriale.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushAQTSerialList(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsAQTSeriale(rreshti));
            }

            return true;
        }

        #endregion
    }
}
