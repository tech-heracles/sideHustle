using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsShpenzimeOperative
    {
        #region atributet
        public int ShoId { get; set; }

        public int ShokId { get; set; }

        public decimal NgaBuxhetiParaardhes { get; set; }

        public decimal NgaTeArdhuratParaardhes { get; set; }

        public decimal TotalParaardhes { get; set; }

        public decimal NjesiaAktuale { get; set; }

        public decimal NgaBuxhetiAktuale { get; set; }

        public decimal NgaTeArdhuratAktuale { get; set; }

        public decimal TotalAktuale { get; set; }

        public decimal NjesiaArdhme { get; set; }

        public decimal KostoPerNjesiArdhme { get; set; }

        public decimal ShpenzimePlanifikuarArdhme { get; set;}

        public decimal LimitiArdhme { get; set; }

        public decimal KerkesaGjykatesArdhme { get; set; }

        public decimal DiferencaKerkeseLimitArdhme { get; set; }

        public decimal ShpenzimeTePlanifikuarTeArdhuraArdhme { get; set; }

        public decimal TotalKerkesaTeArdhuraArdhme { get; set; }

        public decimal VlersimiZyresArdhme { get; set; }

        public decimal LimitiArdhmePlus1 { get; set; }

        public decimal KerkesaGjykatesArdhmePlus1 { get; set; }

        public decimal DiferencaKerkeseLimitArdhmePlus1 { get; set; }

        public decimal VlersimiZyresArdhmePlus1 { get; set; }

        public decimal LimitiArdhmePlus2 { get; set; }

        public decimal KerkesaGjykatesArdhmePlus2 { get; set; }

        public decimal DiferencaKerkeseLimitArdhmePlus2 { get; set; }

        public decimal VlersimiZyresArdhmePlus2 { get; set; }


        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public int IdNdermarrje { get; set; }

        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public int IdStatusDok { get; set; }

        public string Pershkrimi { get; set; }

        public int Niveli { get; set; }

        public int IdPrindi { get; set; }

        public string Kodi { get; set; }
        public int KokaId { get; set; }

        public int IdNdermVit { get; set; }

        #endregion atributet

        public clsShpenzimeOperative() { }

        public clsShpenzimeOperative(int shoId, int shokId, decimal ngaBuxhetiParaardhes, decimal ngaTeArdhuratParaardhes, decimal totalParaardhes,
             decimal njesiaAktuale, decimal ngaBuxhetiAktuale, decimal ngaTeArdhuratAktuale, decimal totalAktuale,
             decimal njesiaArdhme, decimal kostoPerNjesiArdhme, decimal shpenzimePlanifikuarArdhme, decimal limitiArdhme,
             decimal kerkesaGjykatesArdhme, decimal diferencaKerkeseLimitArdhme, decimal shpenzimeTePlanifikuarTeArdhuraArdhme,
             decimal totalKerkesaTeArdhuraArdhme, decimal vlersimiZyresArdhme, decimal limitiArdhmePlus1, decimal kerkesaGjykatesArdhmePlus1,
             decimal diferencaKerkeseLimitArdhmePlus1, decimal vlersimiZyresArdhmePlus1, decimal limitiArdhmePlus2, decimal kerkesaGjykatesArdhmePlus2,
             decimal diferencaKerkeseLimitArdhmePlus2, decimal vlersimiZyresArdhmePlus2, 
            int idModifikuesi, int idNderrmarje, int idStatusDok,int kokaId, int idNdermVit)
        {
            ShoId = shoId;
            ShokId = shokId;
            NgaBuxhetiParaardhes = ngaBuxhetiParaardhes;
            NgaTeArdhuratParaardhes = ngaTeArdhuratParaardhes;
            TotalParaardhes = totalParaardhes;
            NjesiaAktuale = njesiaAktuale;
            NgaBuxhetiAktuale = ngaBuxhetiAktuale;
            NgaTeArdhuratAktuale = ngaTeArdhuratAktuale;
            TotalAktuale = totalAktuale;
            NjesiaArdhme = njesiaArdhme;
            KostoPerNjesiArdhme = kostoPerNjesiArdhme;
            ShpenzimePlanifikuarArdhme = shpenzimePlanifikuarArdhme;
            LimitiArdhme = limitiArdhme;
            KerkesaGjykatesArdhme = kerkesaGjykatesArdhme;
            DiferencaKerkeseLimitArdhme = diferencaKerkeseLimitArdhme;
            ShpenzimeTePlanifikuarTeArdhuraArdhme = shpenzimeTePlanifikuarTeArdhuraArdhme;
            TotalKerkesaTeArdhuraArdhme = totalKerkesaTeArdhuraArdhme;
            VlersimiZyresArdhme = vlersimiZyresArdhme;
            LimitiArdhmePlus1 = limitiArdhmePlus1;
            KerkesaGjykatesArdhmePlus1 = kerkesaGjykatesArdhmePlus1;
            DiferencaKerkeseLimitArdhmePlus1 = diferencaKerkeseLimitArdhmePlus1;
            VlersimiZyresArdhmePlus1 = vlersimiZyresArdhmePlus1;
            LimitiArdhmePlus2 = limitiArdhmePlus2;
            KerkesaGjykatesArdhmePlus2 = kerkesaGjykatesArdhmePlus2;
            DiferencaKerkeseLimitArdhmePlus2 = diferencaKerkeseLimitArdhmePlus2;
            VlersimiZyresArdhmePlus2 = vlersimiZyresArdhmePlus2;
            IdModifikuesi = idModifikuesi;
            IdNdermarrje = idNderrmarje;
            IdStatusDok = idStatusDok;
            KokaId = kokaId;
            IdNdermVit = idNdermVit;
        }

        public static clsShpenzimeOperative Krijo(IDataRecord record)
        {
            return new clsShpenzimeOperative
            {
                ShoId = !Convert.IsDBNull(record["SHOID"]) ? Convert.ToInt32(record["SHOID"]) : 0,
                ShokId = !Convert.IsDBNull(record["SHOKID"]) ? Convert.ToInt32(record["SHOKID"]) : 0,
                NgaBuxhetiParaardhes = !Convert.IsDBNull(record["NGABUXHETI_PARAARDHES"]) ? Convert.ToDecimal(record["NGABUXHETI_PARAARDHES"]) : 0,
                NgaTeArdhuratParaardhes = !Convert.IsDBNull(record["NGATEARDHURAT_PARAARDHES"]) ? Convert.ToDecimal(record["NGATEARDHURAT_PARAARDHES"]) : 0,
                TotalParaardhes = !Convert.IsDBNull(record["TOTAL_PARAARDHES"]) ? Convert.ToDecimal(record["TOTAL_PARAARDHES"]) : 0,
                NjesiaAktuale = !Convert.IsDBNull(record["NJESIA_AKTUALE"]) ? Convert.ToDecimal(record["NJESIA_AKTUALE"]) : 0,
                NgaBuxhetiAktuale = !Convert.IsDBNull(record["NGABUXHETI_AKTUALE"]) ? Convert.ToDecimal(record["NGABUXHETI_AKTUALE"]) : 0,
                NgaTeArdhuratAktuale = !Convert.IsDBNull(record["NGATEARDHURAT_AKTUALE"]) ? Convert.ToDecimal(record["NGATEARDHURAT_AKTUALE"]) : 0,
                TotalAktuale = !Convert.IsDBNull(record["TOTAL_AKTUALE"]) ? Convert.ToDecimal(record["TOTAL_AKTUALE"]) : 0,
                NjesiaArdhme = !Convert.IsDBNull(record["NJESIA_ARDHME"]) ? Convert.ToDecimal(record["NJESIA_ARDHME"]) : 0,
                KostoPerNjesiArdhme = !Convert.IsDBNull(record["KOSTO_PER_NJESI_ARDHME"]) ? Convert.ToDecimal(record["KOSTO_PER_NJESI_ARDHME"]) : 0,
                ShpenzimePlanifikuarArdhme = !Convert.IsDBNull(record["SHPENZIME_PLANIFIKUAR_ARDHME"]) ? Convert.ToDecimal(record["SHPENZIME_PLANIFIKUAR_ARDHME"]) : 0,
                LimitiArdhme = !Convert.IsDBNull(record["LIMITI_ARDHME"]) ? Convert.ToDecimal(record["LIMITI_ARDHME"]) : 0,
                KerkesaGjykatesArdhme = !Convert.IsDBNull(record["KERKESA_GJYKATES_ARDHME"]) ? Convert.ToDecimal(record["KERKESA_GJYKATES_ARDHME"]) : 0,
                DiferencaKerkeseLimitArdhme = !Convert.IsDBNull(record["DIFERENCA_KERKESE_LIMIT_ARDHME"]) ? Convert.ToDecimal(record["DIFERENCA_KERKESE_LIMIT_ARDHME"]) : 0,
                ShpenzimeTePlanifikuarTeArdhuraArdhme = !Convert.IsDBNull(record["SHPENZIME_TEPLANIFIKUAR_TEARDHURA_ARDHME"]) ? Convert.ToDecimal(record["SHPENZIME_TEPLANIFIKUAR_TEARDHURA_ARDHME"]) : 0,
                TotalKerkesaTeArdhuraArdhme = !Convert.IsDBNull(record["TOTALKERKESA_TEARDHURA_ARDHME"]) ? Convert.ToDecimal(record["TOTALKERKESA_TEARDHURA_ARDHME"]) : 0,
                VlersimiZyresArdhme = !Convert.IsDBNull(record["VLERSIMIZYRES_ARDHME"]) ? Convert.ToDecimal(record["VLERSIMIZYRES_ARDHME"]) : 0,
                LimitiArdhmePlus1 = !Convert.IsDBNull(record["LIMITI_ARDHME_PLUS1"]) ? Convert.ToDecimal(record["LIMITI_ARDHME_PLUS1"]) : 0,
                KerkesaGjykatesArdhmePlus1 = !Convert.IsDBNull(record["KERKESA_GJYKATES_ARDHME_PLUS1"]) ? Convert.ToDecimal(record["KERKESA_GJYKATES_ARDHME_PLUS1"]) : 0,
                DiferencaKerkeseLimitArdhmePlus1 = !Convert.IsDBNull(record["DIFERENCA_KERKESE_LIMIT_ARDHME_PLUS1"]) ? Convert.ToDecimal(record["DIFERENCA_KERKESE_LIMIT_ARDHME_PLUS1"]) : 0,
                VlersimiZyresArdhmePlus1 = !Convert.IsDBNull(record["VLERSIMIZYRES_ARDHME_PLUS1"]) ? Convert.ToDecimal(record["VLERSIMIZYRES_ARDHME_PLUS1"]) : 0,
                LimitiArdhmePlus2 = !Convert.IsDBNull(record["LIMITI_ARDHME_PLUS2"]) ? Convert.ToDecimal(record["LIMITI_ARDHME_PLUS2"]) : 0,
                KerkesaGjykatesArdhmePlus2 = !Convert.IsDBNull(record["KERKESA_GJYKATES_ARDHME_PLUS2"]) ? Convert.ToDecimal(record["KERKESA_GJYKATES_ARDHME_PLUS2"]) : 0,
                DiferencaKerkeseLimitArdhmePlus2 = !Convert.IsDBNull(record["DIFERENCA_KERKESE_LIMIT_ARDHME_PLUS2"]) ? Convert.ToDecimal(record["DIFERENCA_KERKESE_LIMIT_ARDHME_PLUS2"]) : 0,
                VlersimiZyresArdhmePlus2 = !Convert.IsDBNull(record["VLERSIMIZYRES_ARDHME_PLUS2"]) ? Convert.ToDecimal(record["VLERSIMIZYRES_ARDHME_PLUS2"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                Pershkrimi = !Convert.IsDBNull(record["PERSHKRIMI"]) ? Convert.ToString(record["PERSHKRIMI"]) : string.Empty,
                Niveli = !Convert.IsDBNull(record["NIVELI"]) ? Convert.ToInt32(record["NIVELI"]) : 0,
                IdPrindi = !Convert.IsDBNull(record["IDPRINDI"]) ? Convert.ToInt32(record["IDPRINDI"]) : 0,
                Kodi = !Convert.IsDBNull(record["KODI"]) ? Convert.ToString(record["KODI"]) : string.Empty,
                KokaId=!Convert.IsDBNull(record["KOKAID"])?Convert.ToInt32(record["KOKAID"]):0,
                IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0,
            };
        }

        public clsMesazh Modifiko()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit");

            try
            {
                dbAB.beginTransaksion();
                mesazhi = dbAB.fshiUpdateStatusDokShpenzimeOperative(ShoId, IdModifikuesi);
                if (mesazhi.Status)
                {
                    int shoId = -1;
                    mesazhi = dbAB.RuajShpenzimeOperative(out shoId, ShokId, NgaBuxhetiParaardhes, NgaTeArdhuratParaardhes,
                        TotalParaardhes, NjesiaAktuale, NgaBuxhetiAktuale,
                        NgaTeArdhuratAktuale, TotalAktuale, NjesiaArdhme, KostoPerNjesiArdhme, ShpenzimePlanifikuarArdhme,
                        LimitiArdhme, KerkesaGjykatesArdhme, DiferencaKerkeseLimitArdhme, ShpenzimeTePlanifikuarTeArdhuraArdhme, TotalKerkesaTeArdhuraArdhme,
                        VlersimiZyresArdhme, LimitiArdhmePlus1, KerkesaGjykatesArdhmePlus1, DiferencaKerkeseLimitArdhmePlus1,
                        VlersimiZyresArdhmePlus1, LimitiArdhmePlus2, KerkesaGjykatesArdhmePlus2, DiferencaKerkeseLimitArdhmePlus2,
                        VlersimiZyresArdhmePlus2, IdNdermarrje, IdModifikuesi,KokaId, IdNdermVit);

                    if (mesazhi.Status)
                    {
                        ShoId = shoId;
                        dbAB.commitTransaksion();
                    }
                    else
                        dbAB.rollbackTransaksion();
                }
                else
                    dbAB.rollbackTransaksion();


            }
            catch (Exception err)
            {

                dbAB.rollbackTransaksion();
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te parashikimit te te ardhurave \n" + err.Message);
            }
            return mesazhi;
        }

        /// <summary>
        /// fshin nga tabela rreshtin e caktuar
        /// </summary>
        /// <param name="rreshtiID"></param>
        /// <param name="dbAB"></param>
        /// <returns></returns>
        internal static clsMesazh Fshi(int shokId, clsDatabaseAnalizeBuxheti dbAB)
        {
            return dbAB.fshiRreshtNgaShpenzimeOperative(shokId);
        }

        public static clsMesazh FshiUpdateShpenzimeOperative(int shokId, int idModifikuesi)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.fshiUpdateStatusDokShpenzimeOperativeSipasShokId(shokId, idModifikuesi);
            }
            
        }
    }
}

