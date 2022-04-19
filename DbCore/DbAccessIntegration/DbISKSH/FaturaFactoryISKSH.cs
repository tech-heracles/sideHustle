using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.DbAccessIntegration.DbISKSH;
using DbCore.DbRegjistrim;
using DbCore.DbKontabiliteti;
using DbCore.DbInventari;
using System.Data;

namespace DbCore.DbAccessIntegration.DbISKSH
{
    public class FaturaFactoryISKSH
    {
        /// <summary>
        /// Funksion generic qe krijon objektet sipas tipeve
        /// </summary>
        /// <typeparam name="T">Tipi i objektit qe do te krijohet</typeparam>
        /// <param name="structType">Enumeracion qe permban llojet e objekteve qe mund te krijohen</param>
        /// <param name="objectToConvert">Objekti nga i cili do te merren te dhenat</param>
        /// <returns>Kthen nje objekt te tipit T qe i kalohet si typeparam</returns>
        public static T KrijoObjekt<T>(EnumStructTypeISKSH structType, object objectToConvert)
        {
            object objekti = new object();
            switch (structType)
            {
                case EnumStructTypeISKSH.FatureShitjejeISKSH:
                    objekti = krijoFatureShitjeISKSH(objectToConvert);
                    break;
                case EnumStructTypeISKSH.FatureShitjejeDetajeISKSH:
                    objekti = krijoFatureShitjeDetajeISKSH(objectToConvert);
                    break;
                case EnumStructTypeISKSH.FleteHyrjeISKSH:
                    objekti = krijoFleteHyrjeISKSH(objectToConvert);
                    break;
                case EnumStructTypeISKSH.FleteHyrjeDetajeISKSH:
                    objekti = krijoFleteHyrjeDetajeISKSH(objectToConvert);
                    break;
                case EnumStructTypeISKSH.DepoFarmaceutikeISKSH:
                    objekti = krijoDepoFarmaceutikeISKSH(objectToConvert);
                    break;
                case EnumStructTypeISKSH.FarmaciISKSH:
                    objekti = krijoFarmaciISKSH(objectToConvert);
                    break;
                case EnumStructTypeISKSH.FirmeFarmaceutikeISKSH:
                    objekti = krijoFirmeFarmaceutikeISKSH(objectToConvert);
                    break;
                case EnumStructTypeISKSH.MedikamentISKSH:
                    objekti = krijoMedikamentISKSH(objectToConvert);
                    break;
            }
            return (T)objekti;
        }

        /// <summary>
        /// Funksion generic qe krijon nje liste me objekte sipas tipeve
        /// </summary>
        /// <typeparam name="T">Tipi i objektit qe do te permbaje lista</typeparam>
        /// <typeparam name="S">Tipi i objektit qe i kalohet si parameter, nga i cili merren te dhenat</typeparam>
        /// <param name="structType">Enumeracion qe permban llojet e objekteve qe mund te krijohen</param>
        /// <param name="objectsToConvert">List me objektet nga te cilat do te merren te dhenat</param>
        /// <returns>Kthen nje liste me objekte te tipit T qe i kalohet si typeparam</returns>
        public static List<T> krijoListeObjekt<T,S>(EnumStructTypeISKSH structType, List<S> objectsToConvert)
        {
            List<T> collection = new List<T>();
            foreach(var item in objectsToConvert)
            {
                collection.Add(KrijoObjekt<T>(structType, item));
            }
            return collection;
        }

        /// <summary>
        /// Krijon Fature Shitje per ISKSH me te dhenat e objektit qe i kalohet si parameter
        /// </summary>
        /// <param name="objectToConvert">Objekti nga i cili do te merren te dhenat</param>
        /// <returns>Kthen nje objekt te tipit FatureShitjejeISKSH</returns>
        private static FatureShitjejeISKSH krijoFatureShitjeISKSH(object objectToConvert)
        {
            FatureShitjejeISKSH fatura = new FatureShitjejeISKSH();
            DataRow kokaShitje = (DataRow)objectToConvert;

            fatura.Id = 0;
            int nrDok = 0;
            if (!int.TryParse(Convert.ToString(kokaShitje["NrDok"]), out nrDok))
                throw new MyException("Numri i dokumentit duhet te jete vlere numerike midis 0 dhe 2,147,483,647!");
            fatura.NrDok = nrDok;
            fatura.Data                                     = Convert.ToDateTime(kokaShitje["Data"]);
            fatura.DataRegjistrimit                         = Convert.ToDateTime(kokaShitje["DataRegjistrimit"]);
            fatura.KodiBleresit                             = Convert.ToString(kokaShitje["KodiBleresit"]);
            fatura.Emertimi                                 = Convert.ToString(kokaShitje["Emertimi"]);
            fatura.Adresa                                   = Convert.ToString(kokaShitje["Adresa"]);
            fatura.NiptBleresi                              = Convert.ToString(kokaShitje["NiptBleresi"]);
            fatura.NrLicences                               = Convert.ToString(kokaShitje["NrLicences"]);
            fatura.Shuma                                    = Convert.ToDecimal(kokaShitje["Shuma"]);
            fatura.NrTotaliBarnave                          = Convert.ToInt32(kokaShitje["NrTotaliBarnave"]);
            fatura.Shenime                                  = Convert.ToString(kokaShitje["Shenime"]);
            fatura.RefKompjuterikePerFaturaShitjejeERuajtur = Convert.ToInt32(kokaShitje["RefKompjuterikePerFaturaShitjejeERuajtur"]);
            fatura.FatureOsePasqyre                         = Convert.ToInt32(kokaShitje["FatureOsePasqyre"]);
            fatura.Skonto                                   = Convert.ToDecimal(kokaShitje["Skonto"]);

            return fatura;
        }

        /// <summary>
        /// Krijon Trup Fature Shitje per ISKSH me te dhenat e objektit qe i kalohet si parameter
        /// </summary>
        /// <param name="objectToConvert">Objekti nga i cili do te merren te dhenat</param>
        /// <returns>Kthen nje objekt te tipit FatureShitjejeDetajeISKSH</returns>
        private static FatureShitjejeDetajeISKSH krijoFatureShitjeDetajeISKSH(object objectToConvert)
        {
            FatureShitjejeDetajeISKSH detaje = new FatureShitjejeDetajeISKSH();
            DataRow trupShitje = (DataRow)objectToConvert;

            detaje.Id = 0;
            detaje.IdFaturaShitjeje         = 0;
            detaje.KodiBarit                = Convert.ToString(trupShitje["KodiBarit"]);
            detaje.Sasia                    = Convert.ToDecimal(trupShitje["Sasia"]);
            detaje.NrSerise                 = Convert.ToString(trupShitje["NrSerise"]);
            detaje.FormaShitjes             = Convert.ToString(trupShitje["FormaShitjes"]);
            detaje.KoeficientiTransformimit = Convert.ToDecimal(trupShitje["KoeficientiTransformimit"]);

            return detaje;
        }

        /// <summary>
        /// Krijon Flete Hyrje per ISKSH me te dhenat e objektit qe i kalohet si parameter
        /// </summary>
        /// <param name="objectToConvert">Objekti nga i cili do te merren te dhenat</param>
        /// <returns>Kthen nje objekt te tipit FleteHyrjeISKSH</returns>
        private static FleteHyrjeISKSH krijoFleteHyrjeISKSH(object objectToConvert)
        {
            FleteHyrjeISKSH fatura = new FleteHyrjeISKSH();
            DataRow kokaBlerje = (DataRow)objectToConvert;

            fatura.Id = 0;
            int nrDok = 0;
            if (!int.TryParse(Convert.ToString(kokaBlerje["NrDok"]), out nrDok))
                throw new MyException("Numri i dokumentit duhet te jete vlere numerike midis 0 dhe 2,147,483,647!");
            fatura.NrDok = nrDok;
            fatura.Data = Convert.ToDateTime(kokaBlerje["Data"]);
            fatura.DataRegjistrimit = Convert.ToDateTime(kokaBlerje["DataRegjistrimit"]);
            fatura.KodiFurnitorit = Convert.ToString(kokaBlerje["KodiFurnitorit"]);
            fatura.Emertimi = Convert.ToString(kokaBlerje["Emertimi"]);
            fatura.Adresa = Convert.ToString(kokaBlerje["Adresa"]);
            fatura.NiptFurnitori = Convert.ToString(kokaBlerje["NiptFurnitori"]);
            fatura.NrLicences = Convert.ToString(kokaBlerje["NrLicences"]);
            fatura.Shuma = Convert.ToDecimal(kokaBlerje["Shuma"]);
            fatura.NrTotaliBarnave = Convert.ToInt32(kokaBlerje["NrTotaliBarnave"]);
            fatura.Shenime = Convert.ToString(kokaBlerje["Shenime"]);
            fatura.FatureOsePasqyre = Convert.ToInt32(kokaBlerje["FatureOsePasqyre"]);
            fatura.Skonto = Convert.ToDecimal(kokaBlerje["Skonto"]);

            return fatura;
        }

        /// <summary>
        /// Krijon Trup Flete Hyrje per ISKSH me te dhenat e objektit qe i kalohet si parameter
        /// </summary>
        /// <param name="objectToConvert">Objekti nga i cili do te merren te dhenat</param>
        /// <returns>Kthen nje objekt te tipit FleteHyrjeDetajeISKSH</returns>
        private static FleteHyrjeDetajeISKSH krijoFleteHyrjeDetajeISKSH(object objectToConvert)
        {
            FleteHyrjeDetajeISKSH detaje = new FleteHyrjeDetajeISKSH();
            DataRow trupBlerje = (DataRow)objectToConvert;

            detaje.Id = 0;
            detaje.IdFleteHyrje = 0;
            detaje.KodiBarit = Convert.ToString(trupBlerje["KodiBarit"]);
            detaje.Sasia = Convert.ToDecimal(trupBlerje["Sasia"]);
            detaje.NrSerise = Convert.ToString(trupBlerje["NrSerise"]);
            detaje.FormaShitjes = Convert.ToString(trupBlerje["FormaShitjes"]);
            detaje.KoeficientiTransformimit = Convert.ToDecimal(trupBlerje["KoeficientiTransformimit"]);

            return detaje;
        }

        /// <summary>
        /// Krijon Depo Farmaceutike per ISKSH me te dhenat e objektit qe i kalohet si parameter
        /// </summary>
        /// <param name="objectToConvert">Objekti nga i cili do te merren te dhenat</param>
        /// <returns>Kthen nje objekt te tipit DepoFarmaceutikeISKSH</returns>
        private static DepoFarmaceutikeISKSH krijoDepoFarmaceutikeISKSH(object objectToConvert)
        {
            DepoFarmaceutikeISKSH depo = new DepoFarmaceutikeISKSH();
            DataRow klienti = (DataRow)objectToConvert;
            depo.KodiDepos = klienti["KodiDepos"] != null ? (string)klienti["KodiDepos"] : string.Empty;
            return depo;
        }

        /// <summary>
        /// Krijon Farmaci per ISKSH me te dhenat e objektit qe i kalohet si parameter
        /// </summary>
        /// <param name="objectToConvert">Objekti nga i cili do te merren te dhenat</param>
        /// <returns>Kthen nje objekt te tipit FarmaciISKSH</returns>
        private static FarmaciISKSH krijoFarmaciISKSH(object objectToConvert)
        {
            FarmaciISKSH farmaci = new FarmaciISKSH();
            DataRow klienti = (DataRow)objectToConvert;
            farmaci.KodFarmacise = klienti["KodFarmacise"] != null ? (string)klienti["KodFarmacise"] : string.Empty;
            return farmaci;
        }

        /// <summary>
        /// Krijon Firme Farmaceutike per ISKSH me te dhenat e objektit qe i kalohet si parameter
        /// </summary>
        /// <param name="objectToConvert">Objekti nga i cili do te merren te dhenat</param>
        /// <returns>Kthen nje objekt te tipit FirmeFarmaceutikeISKSH</returns>
        private static FirmeFarmaceutikeISKSH krijoFirmeFarmaceutikeISKSH(object objectToConvert)
        {
            FirmeFarmaceutikeISKSH firme = new FirmeFarmaceutikeISKSH();
            DataRow klienti = (DataRow)objectToConvert;
            firme.Kodi = klienti["Kodi"] != null ? (int)klienti["Kodi"] : 0;
            return firme;
        }

        /// <summary>
        /// Krijon Medikament per ISKSH me te dhenat e objektit qe i kalohet si parameter
        /// </summary>
        /// <param name="objectToConvert">Objekti nga i cili do te merren te dhenat</param>
        /// <returns>Kthen nje objekt te tipit MedikamentISKSH</returns>
        private static MedikamentISKSH krijoMedikamentISKSH(object objectToConvert)
        {
            MedikamentISKSH medikament = new MedikamentISKSH();
            DataRow artikulli = (DataRow)objectToConvert;
            medikament.Id        = artikulli["Id"] != null ? (int)artikulli["Id"] : 0;
            medikament.KodiBarit = artikulli["KodiBarit"] != null ? (string)artikulli["KodiBarit"] : string.Empty;
            medikament.EmertimiDheFuqiaEBarit = artikulli["EmertimiDheFuqiaEBarit"] != null ? (string)artikulli["EmertimiDheFuqiaEBarit"] : string.Empty;
            medikament.Forma = artikulli["Forma"] != null ? (string)artikulli["Forma"] : string.Empty;
            medikament.EmriTregtar = artikulli["EmriTregtar"] != null ? (string)artikulli["EmriTregtar"] : string.Empty;
            medikament.FirmaFarmaceutike = artikulli["FirmaFarmaceutike"] != null ? (string)artikulli["FirmaFarmaceutike"] : string.Empty;
            medikament.KodiAtc = artikulli["KodiAtc"] != null ? (string)artikulli["KodiAtc"] : string.Empty;
            medikament.Cm_shit_imp_dist = artikulli["Cm_shit_imp_dist"] != null ? Convert.ToDecimal(artikulli["Cm_shit_imp_dist"]) : 0;
            return medikament;
        }

    }
}
