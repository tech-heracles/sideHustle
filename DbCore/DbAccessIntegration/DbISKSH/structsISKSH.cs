using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.DbAccessIntegration.DbISKSH;

namespace DbCore.DbAccessIntegration
{
    /// <summary>
    /// Class me Elementet qe permban ne databaze Fatura e Shitjes se ISKSH
    /// </summary>
    public class FatureShitjejeISKSH
    {
        public int Id{ get; set; }
        public int NrDok { get; set; }
        public DateTime Data{ get; set; }
        public DateTime DataRegjistrimit{ get; set; }
        public string KodiBleresit{ get; set; }
        public string Emertimi{ get; set; }
        public string Adresa{ get; set; }
        public int LlojiSubjektitBleresi{ get; set; }
        public string NiptBleresi{ get; set; }
        public string NrLicences{ get; set; }
        public decimal Shuma{ get; set; }
        public int NrTotaliBarnave{ get; set; }
        public string Shenime{ get; set; }
        public int RefKompjuterikePerFaturaShitjejeERuajtur{ get; set; }
        public int FatureOsePasqyre{ get; set; }
        public decimal Skonto{ get; set; }
    }

    /// <summary>
    /// Class me Elementet qe permban ne databaze Trupi i Fatures se Shitjes te ISKSH
    /// </summary>
    public class FatureShitjejeDetajeISKSH
    {
        public int Id{ get; set; }
        public int IdFaturaShitjeje{ get; set; }
        public string KodiBarit{ get; set; }
        public string EmertimiDheFuqiaBarit{ get; set; }
        public string Forma{ get; set; }
        public string EmriTregtar{ get; set; }
        public string FirmaFarmaceutike{ get; set; }
        public decimal Sasia{ get; set; }
        public string NrSerise{ get; set; }
        public DateTime DataSkadences{ get; set; }
        public decimal CmimiPerNjesi{ get; set; }
        public string FormaShitjes{ get; set; }
        public decimal KoeficientiTransformimit{ get; set; }
        public string KodiAtc{ get; set; }
    }

    /// <summary>
    /// Class me Elementet qe permban ne databaze Fleta e Hyrjes se ISKSH
    /// </summary>
    public class FleteHyrjeISKSH
    {
        public int Id{ get; set; }
        public int NrDok { get; set; }
        public DateTime Data{ get; set; }
        public DateTime DataRegjistrimit{ get; set; }
        public string KodiFurnitorit{ get; set; }
        public string Emertimi{ get; set; }
        public string Adresa{ get; set; }
        public int LlojiSubjektitFurnitori{ get; set; }
        public string NiptFurnitori{ get; set; }
        public string NrLicences{ get; set; }
        public decimal Shuma{ get; set; }
        public int NrTotaliBarnave{ get; set; }
        public string Shenime{ get; set; }
        public int FatureOsePasqyre{ get; set; }
        public decimal Skonto{ get; set; }
    }

    /// <summary>
    /// Class me Elementet qe permban ne databaze Trupi i Fletes se Hyrjes te ISKSH
    /// </summary>
    public class FleteHyrjeDetajeISKSH
    {
        public int Id{ get; set; }
        public int IdFleteHyrje{ get; set; }
        public string KodiBarit{ get; set; }
        public string EmertimiDheFuqiaBarit{ get; set; }
        public string Forma{ get; set; }
        public string EmriTregtar{ get; set; }
        public string FirmaFarmaceutike{ get; set; }
        public decimal Sasia{ get; set; }
        public string NrSerise{ get; set; }
        public DateTime DataSkadences{ get; set; }
        public decimal CmimiPerNjesi{ get; set; }
        public string FormaShitjes{ get; set; }
        public decimal KoeficientiTransformimit{ get; set; }
        public string KodiAtc{ get; set; }
    }

    /// <summary>
    /// Class me Elementet qe permban ne databaze Depoja Farmaceutike e ISKSH
    /// </summary>
    public class DepoFarmaceutikeISKSH
    {
        public string KodiDepos { get; set; }

        public static DepoFarmaceutikeISKSH getObject(string kodi, DbAccess dbAccess)
        {
            return dbAccess.TransCache.getKlientFurnitorISKSH<DepoFarmaceutikeISKSH>(kodi, EnumStructTypeISKSH.DepoFarmaceutikeISKSH, dbAccess);
        }
    }

    /// <summary>
    /// Class me Elementet qe permban ne databaze Farmacia e ISKSH
    /// </summary>
    public class FarmaciISKSH
    {
        public string KodFarmacise{ get; set; }

        public static FarmaciISKSH getObject(string kodi, DbAccess dbAccess)
        {
            return dbAccess.TransCache.getKlientFurnitorISKSH<FarmaciISKSH>(kodi, EnumStructTypeISKSH.FarmaciISKSH, dbAccess);
        }
    }

    /// <summary>
    /// Class me Elementet qe permban ne databaze Firma Farmaceutike e ISKSH
    /// </summary>
    public class FirmeFarmaceutikeISKSH
    {
        public int Kodi { get; set; }

        public static FirmeFarmaceutikeISKSH getObject(string kodi, DbAccess dbAccess)
        {
            return dbAccess.TransCache.getKlientFurnitorISKSH<FirmeFarmaceutikeISKSH>(kodi, EnumStructTypeISKSH.FirmeFarmaceutikeISKSH, dbAccess);
        }
    }

    /// <summary>
    /// Class me Elementet qe permban ne databaze Medikamenti i ISKSH
    /// </summary>
    public class MedikamentISKSH
    {
        public int Id{ get; set; }
        public string KodiBarit{ get; set; }
        public string EmertimiDheFuqiaEBarit { get; set; }
        public string Forma { get; set; }
        public string EmriTregtar { get; set; }
        public string FirmaFarmaceutike { get; set; }
        public string KodiAtc { get; set; }
        public decimal Cm_shit_imp_dist { get; set; }

        public static MedikamentISKSH getMedikamentISKSH(string kodiBarit, DbAccess dbAccess) { return dbAccess.TransCache.getMedikamentISKSH(kodiBarit, dbAccess); }
    }

}
