using DbCore.DbAccessIntegration;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAccessIntegration.DbISKSH
{
    public class ClsIntegrimFaturaISKSH
    {
        /// <summary>
        /// Konstruktor pa parametra
        /// </summary>
        public ClsIntegrimFaturaISKSH() { }

        /// <summary>
        /// Funksion qe kryen transferimin e objekteve
        /// </summary>
        /// <param name="kokaFatura">Koka e fatures qe do te transferohet</param>
        /// <param name="trupiFatura">Trupi i fatures qe do te transferohet</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Mesazh gabimi nese deshton Transferimi, mesazh suksesi ne te kundert</returns>
        public clsMesazh Transfero(DataRow kokaFatura, DataTable trupiFatura, EnumLlojVeprimiISKSH veprimi, ref DbAccess dbAccess)
        {
            try
            {
                dbAccess.beginTransaksion(IsolationLevel.ReadCommitted);

                clsMesazh mesazh = TransferoKontrolloFaturaISKSH(kokaFatura, trupiFatura, veprimi, true, dbAccess);
                if (!mesazh)
                {
                    dbAccess.RollBackTransaction(ref dbAccess);
                    return mesazh;
                }

                dbAccess.CompleteTransaction(ref dbAccess);
                return new MesazhSuksesi("Transferimi u krye me sukses.");
            }
            catch (Exception ex)
            {
                dbAccess.RollBackTransaction(ref dbAccess);
                return new MesazhGabimi(ex.Message);
            }
        }

        /// <summary>
        /// Funksion qe kontrollon objektet
        /// </summary>
        /// <param name="kokaFatura">Koka e fatures qe do te transferohet</param>
        /// <param name="trupiFatura">Trupi i fatures qe do te transferohet</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Mesazh gabimi nese deshton Transferimi, mesazh suksesi ne te kundert</returns>
        public clsMesazh Kontrollo(DataRow kokaFatura, DataTable trupiFatura, EnumLlojVeprimiISKSH veprimi, ref DbAccess dbAccess)
        {
            try
            {
                dbAccess.beginTransaksion(IsolationLevel.ReadCommitted);

                clsMesazh mesazh = TransferoKontrolloFaturaISKSH(kokaFatura, trupiFatura, veprimi, false, dbAccess);
                if (!mesazh)
                {
                    dbAccess.RollBackTransaction(ref dbAccess);
                    return mesazh;
                }

                dbAccess.CompleteTransaction(ref dbAccess);
                return new MesazhSuksesi("Kontrolli u kalua me sukses.");
            }
            catch (Exception ex)
            {
                dbAccess.RollBackTransaction(ref dbAccess);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh FshiFaturaISKSH(EnumLlojVeprimiISKSH veprimi, DateTime dtFillimi, DateTime dtMbarimi, ref DbAccess dbAccess)
        {
            try
            {
                dbAccess.beginTransaksion(IsolationLevel.ReadCommitted);

                clsMesazh mesazh = new MesazhSuksesi();

                switch (veprimi)
                {
                    case EnumLlojVeprimiISKSH.Shitje:
                        mesazh = FshiFaturaShitjejeISKSH(dtFillimi, dtMbarimi, dbAccess);
                        break;
                    case EnumLlojVeprimiISKSH.Blerje:
                        mesazh = FshiFaturaBlerjeISKSH(dtFillimi, dtMbarimi, dbAccess);
                        break;
                }

                if (!mesazh)
                {
                    dbAccess.RollBackTransaction(ref dbAccess);
                    return mesazh;
                }

               dbAccess.CompleteTransaction(ref dbAccess);
                return new MesazhSuksesi("Fshirja perfundoi me sukses.");
            }
            catch (Exception ex)
            {
                dbAccess.RollBackTransaction(ref dbAccess);
                return new MesazhGabimi(ex.Message);
            }
        }

        /// <summary>
        /// Funksion qe fshin faturat e shitjes ne ISKSH sipas ranget te dates
        /// </summary>
        /// <param name="dtFillimi"></param>
        /// <param name="dtMbarimi"></param>
        /// <param name="dbAccess"></param>
        /// <returns></returns>
        /// 
        private clsMesazh FshiFaturaShitjejeISKSH(DateTime dtFillimi, DateTime dtMbarimi, DbAccess dbAccess)
        {
            clsMesazh mesazh = new clsMesazh();
            string condition = $"(SELECT ID FROM {TablesISKSH.FatureShitjejeISKSH} WHERE DateValue(Format(Data,'MM/dd/yyyy')) >= DateValue('{dtFillimi.ToString("MM/dd/yyyy")}') AND DateValue(Format(Data,'MM/dd/yyyy')) <= DateValue('{dtMbarimi.ToString("MM/dd/yyyy")}'))";

            List<FatureShitjejeDetajeISKSH> trupFature = MerrListeObjekteshNgaDbAccessSipasKushtit<FatureShitjejeDetajeISKSH>(EnumStructTypeISKSH.FatureShitjejeDetajeISKSH, TablesISKSH.FatureShitjejeDetajeISKSH, $" IDFaturaShitjeje in {condition}", dbAccess);

            foreach(FatureShitjejeDetajeISKSH rresht in trupFature)
            {
                mesazh = NdryshoGjendjeArtikujISKSH(EnumSqlCommand.Delete, EnumLlojVeprimiISKSH.Shitje, rresht.Sasia, rresht.KodiBarit, rresht.NrSerise, dbAccess);
                if (!mesazh)
                    return mesazh;
            }

            mesazh = dbAccess.DeleteObject<FatureShitjejeDetajeISKSH>(new FatureShitjejeDetajeISKSH(), TablesISKSH.FatureShitjejeDetajeISKSH, $" IDFaturaShitjeje in {condition}");

            if (!mesazh)
                return mesazh;

            mesazh = dbAccess.DeleteObject<FatureShitjejeISKSH>(new FatureShitjejeISKSH(), TablesISKSH.FatureShitjejeISKSH, $" ID in {condition}");
            if (!mesazh)
                return mesazh;

            return new MesazhSuksesi("Fshirja u krye me sukses.");
        }

        /// <summary>
        /// Funksion qe fshin faturat e blerjes ne ISKSH sipas ranget te dates
        /// </summary>
        /// <param name="dtFillimi"></param>
        /// <param name="dtMbarimi"></param>
        /// <param name="dbAccess"></param>
        /// <returns></returns>
        private clsMesazh FshiFaturaBlerjeISKSH(DateTime dtFillimi, DateTime dtMbarimi, DbAccess dbAccess)
        {
            clsMesazh mesazh = new clsMesazh();
            string condition = $"(SELECT ID FROM {TablesISKSH.FleteHyrjeISKSH} WHERE DateValue(Format(Data,'MM/dd/yyyy')) >= DateValue('{dtFillimi.ToString("MM/dd/yyyy")}') AND DateValue(Format(Data,'MM/dd/yyyy')) <= DateValue('{dtMbarimi.ToString("MM/dd/yyyy")}'))";


            List<FleteHyrjeDetajeISKSH> trupFature = MerrListeObjekteshNgaDbAccessSipasKushtit<FleteHyrjeDetajeISKSH>(EnumStructTypeISKSH.FleteHyrjeDetajeISKSH, TablesISKSH.FleteHyrjeDetajeISKSH, $" IDFleteHyrje in {condition}", dbAccess);

            foreach (FleteHyrjeDetajeISKSH rresht in trupFature)
            {
                mesazh = NdryshoGjendjeArtikujISKSH(EnumSqlCommand.Delete, EnumLlojVeprimiISKSH.Blerje, rresht.Sasia, rresht.KodiBarit, rresht.NrSerise, dbAccess);
                if (!mesazh)
                    return mesazh;
            }

            mesazh = dbAccess.DeleteObject<FleteHyrjeDetajeISKSH>(new FleteHyrjeDetajeISKSH(), TablesISKSH.FleteHyrjeDetajeISKSH, $" IDFleteHyrje in {condition}");

            if (!mesazh)
                return mesazh;

            mesazh = dbAccess.DeleteObject<FleteHyrjeISKSH>(new FleteHyrjeISKSH(), TablesISKSH.FleteHyrjeISKSH, $" ID in {condition}");
            if (!mesazh)
                return mesazh;

            return new MesazhSuksesi("Fshirja u krye me sukses.");
        }

        /// <summary>
        /// Funksion qe kryen transferimin e objekteve te ISKSH sipas llojeve te veprimit
        /// </summary>
        /// <param name="kokaFatura">Koka e fatures qe do te transferohen</param>
        /// <param name="trupiFatura">Trupi i fatures qe do te transferohen</param>
        /// <param name="veprimi">Lloji i veprimit sipas EnumLlojVeprimiISKSH</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Mesazh gabimi nese deshton Transferimi, mesazh suksesi ne te kundert</returns>
        private clsMesazh TransferoKontrolloFaturaISKSH(DataRow kokaFatura, DataTable trupiFatura, EnumLlojVeprimiISKSH veprimi, bool transfero, DbAccess dbAccess)
        {
            switch (veprimi)
            {
                case EnumLlojVeprimiISKSH.Shitje:
                    return TransferoKontrolloFatureShitjejeISKSH(kokaFatura, trupiFatura, transfero, dbAccess);
                case EnumLlojVeprimiISKSH.Blerje:
                    return TransferoKontrolloFatureBlerjeISKSH(kokaFatura, trupiFatura, transfero, dbAccess);
            }

            return new MesazhSuksesi("Transferimi u krye me sukses.");
        }

        /// <summary>
        /// Funksion qe kryen transferimin e faturave te shitjes te ISKSH
        /// </summary>
        /// <param name="kokaFatura">Koka e fatures qe do te transferohen</param>
        /// <param name="trupiFatura">Trupi i fatures qe do te transferohen</param>
        /// <param name="transfero">True kur eshte transferim, false kur eshte kontroll</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Mesazh gabimi nese deshton Transferimi, mesazh suksesi ne te kundert</returns>
        private clsMesazh TransferoKontrolloFatureShitjejeISKSH(DataRow kokaFatura, DataTable trupiFatura, bool transfero, DbAccess dbAccess)
        {
            FatureShitjejeISKSH fatura = FaturaFactoryISKSH.KrijoObjekt<FatureShitjejeISKSH>(EnumStructTypeISKSH.FatureShitjejeISKSH, kokaFatura);
            List<FatureShitjejeDetajeISKSH> colFaturaShitjeISKSH = FaturaFactoryISKSH.krijoListeObjekt<FatureShitjejeDetajeISKSH, DataRow>(EnumStructTypeISKSH.FatureShitjejeDetajeISKSH, trupiFatura.AsEnumerable().ToList());

            clsMesazh mesazh = TransferoKontrolloKokeFatureShitjejeISKSH(fatura, transfero, dbAccess);
            if (!mesazh)
                return mesazh;

            colFaturaShitjeISKSH.ForEach(x => x.IdFaturaShitjeje = fatura.Id);

            mesazh = TransferoKontrolloTrupFatureShitjejeISKSH(colFaturaShitjeISKSH, transfero, dbAccess);
            if (!mesazh)
                return mesazh;

            if(transfero)
                return new MesazhSuksesi("Transferimi u krye me sukses.");

            return new MesazhSuksesi("Kontrolli u kalua me sukses");
        }

        /// <summary>
        /// Funksion qe kryen transferimin e faturave te blerjes te ISKSH
        /// </summary>
        /// <param name="kokaFatura">Koka e fatures qe do te transferohen</param>
        /// <param name="trupiFatura">Trupi i fatures qe do te transferohen</param>
        /// <param name="transfero">True kur eshte transferim, false kur eshte kontroll</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Mesazh gabimi nese deshton Transferimi, mesazh suksesi ne te kundert</returns>
        private clsMesazh TransferoKontrolloFatureBlerjeISKSH(DataRow kokaFatura, DataTable trupiFatura, bool transfero, DbAccess dbAccess)
        {
            FleteHyrjeISKSH fatura = FaturaFactoryISKSH.KrijoObjekt<FleteHyrjeISKSH>(EnumStructTypeISKSH.FleteHyrjeISKSH, kokaFatura);
            List<FleteHyrjeDetajeISKSH> colFleteHyrjeISKSH = FaturaFactoryISKSH.krijoListeObjekt<FleteHyrjeDetajeISKSH, DataRow>(EnumStructTypeISKSH.FleteHyrjeDetajeISKSH, trupiFatura.AsEnumerable().ToList());

            clsMesazh mesazh = TransferoKontrolloKokeFleteHyrjeISKSH(fatura, transfero, dbAccess);
            if (!mesazh)
                return mesazh;

            colFleteHyrjeISKSH.ForEach(x => x.IdFleteHyrje = fatura.Id);

            mesazh = TransferoKontrolloTrupFleteHyrjeISKSH(colFleteHyrjeISKSH, transfero, dbAccess);
            if (!mesazh)
                return mesazh;

            if(transfero)
                return new MesazhSuksesi("Transferimi u krye me sukses.");

            return new MesazhSuksesi("Kontrolli u kalua me sukses");
        }

        /// <summary>
        /// Funksion qe kryen transferimin e kokes se fatures se shitjes te ISKSH
        /// </summary>
        /// <param name="objectsToTransfer">Objekti qe do te transferohet</param>
        /// <param name="transfero">True kur eshte transferim, false kur eshte kontroll</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Mesazh gabimi nese deshton Transferimi, mesazh suksesi ne te kundert</returns>
        private clsMesazh TransferoKontrolloKokeFatureShitjejeISKSH(FatureShitjejeISKSH fatura, bool transfero, DbAccess dbAccess)
        {

            clsMesazh mesazh = ValidoFatureShitjejeISKSH(fatura, dbAccess);
            if (!mesazh || !transfero)
                return mesazh;

            int idKoka = 0;
            mesazh = dbAccess.InsertObject<FatureShitjejeISKSH>(fatura, out idKoka, TablesISKSH.FatureShitjejeISKSH, "Id");
            fatura.Id = idKoka;

            return mesazh;
        }

        /// <summary>
        /// Funksion qe kryen transferimin e trupit te fatures se shitjes te ISKSH
        /// </summary>
        /// <param name="objectsToTransfer">Objekti qe do te transferohet</param>
        /// <param name="transfero">True kur eshte transferim, false kur eshte kontroll</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Mesazh gabimi nese deshton Transferimi, mesazh suksesi ne te kundert</returns>
        private clsMesazh TransferoKontrolloTrupFatureShitjejeISKSH(List<FatureShitjejeDetajeISKSH> colFaturaShitjeDetajeISKSH, bool transfero, DbAccess dbAccess)
        {
            clsMesazh mesazh = ValidoTrupFatureShitjejeISKSH(colFaturaShitjeDetajeISKSH, dbAccess);
            if (!mesazh || !transfero)
                return mesazh;

            MedikamentISKSH artikulli;
            int idKoka = Convert.ToInt32(dbAccess.SelectScalarByQueryString($"Select MAX(Id) FROM {TablesISKSH.FatureShitjejeISKSH}"));

            foreach (FatureShitjejeDetajeISKSH rresht in colFaturaShitjeDetajeISKSH)
            {
                rresht.IdFaturaShitjeje = idKoka;

                artikulli = MedikamentISKSH.getMedikamentISKSH(rresht.KodiBarit, dbAccess);

                rresht.EmertimiDheFuqiaBarit = artikulli.EmertimiDheFuqiaEBarit;
                rresht.Forma = artikulli.Forma;
                rresht.EmriTregtar = artikulli.EmriTregtar;
                rresht.FirmaFarmaceutike = artikulli.FirmaFarmaceutike;
                rresht.KodiAtc = artikulli.KodiAtc;
                rresht.CmimiPerNjesi = artikulli.Cm_shit_imp_dist;
                rresht.DataSkadences = VendosDateSkadencePerMedikament(rresht.KodiBarit, rresht.NrSerise, dbAccess);

                mesazh = NdryshoGjendjeArtikujISKSH(EnumSqlCommand.Insert, EnumLlojVeprimiISKSH.Shitje, rresht.Sasia, rresht.KodiBarit, rresht.NrSerise, dbAccess);
                if (!mesazh)
                    return mesazh;
            }

            return dbAccess.InsertObjectCollection<FatureShitjejeDetajeISKSH>(colFaturaShitjeDetajeISKSH, TablesISKSH.FatureShitjejeDetajeISKSH, "Id");
        }

        /// <summary>
        /// Funksion qe kryen transferimin e kokes se fatures se blerjes te ISKSH
        /// </summary>
        /// <param name="objectsToTransfer">Objekti qe do te transferohet</param>
        /// <param name="transfero">True kur eshte transferim, false kur eshte kontroll</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Mesazh gabimi nese deshton Transferimi, mesazh suksesi ne te kundert</returns>
        private clsMesazh TransferoKontrolloKokeFleteHyrjeISKSH(FleteHyrjeISKSH fatura, bool transfero, DbAccess dbAccess)
        {
            clsMesazh mesazh = ValidoFleteHyrjeISKSH(fatura, dbAccess);
            if (!mesazh || !transfero)
                return mesazh;

            int idKoka = 0;
            mesazh = dbAccess.InsertObject<FleteHyrjeISKSH>(fatura, out idKoka, TablesISKSH.FleteHyrjeISKSH, "Id");
            fatura.Id = idKoka;
            return mesazh;
        }

        /// <summary>
        /// Funksion qe kryen transferimin e trupit te fatures se blerjes te ISKSH
        /// </summary>
        /// <param name="objectsToTransfer">Objekti qe do te transferohet</param>
        /// <param name="transfero">True kur eshte transferim, false kur eshte kontroll</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Mesazh gabimi nese deshton Transferimi, mesazh suksesi ne te kundert</returns>
        private clsMesazh TransferoKontrolloTrupFleteHyrjeISKSH(List<FleteHyrjeDetajeISKSH> colFleteHyrjeDetajeISKSH, bool transfero, DbAccess dbAccess)
        {
            clsMesazh mesazh = ValidoTrupFleteHyrjeISKSH(colFleteHyrjeDetajeISKSH, dbAccess);
            if (!mesazh || !transfero)
                return mesazh;

            MedikamentISKSH artikulli;
            int idKoka = Convert.ToInt32(dbAccess.SelectScalarByQueryString($"Select MAX(Id) FROM {TablesISKSH.FleteHyrjeISKSH}"));
            foreach (FleteHyrjeDetajeISKSH rresht in colFleteHyrjeDetajeISKSH)
            {
                rresht.IdFleteHyrje = idKoka;

                artikulli = MedikamentISKSH.getMedikamentISKSH(rresht.KodiBarit, dbAccess);

                rresht.EmertimiDheFuqiaBarit = artikulli.EmertimiDheFuqiaEBarit;
                rresht.Forma = artikulli.Forma;
                rresht.EmriTregtar = artikulli.EmriTregtar;
                rresht.FirmaFarmaceutike = artikulli.FirmaFarmaceutike;
                rresht.KodiAtc = artikulli.KodiAtc;
                rresht.CmimiPerNjesi = artikulli.Cm_shit_imp_dist;
                rresht.DataSkadences = VendosDateSkadencePerMedikament(rresht.KodiBarit, rresht.NrSerise, dbAccess);

                mesazh = NdryshoGjendjeArtikujISKSH(EnumSqlCommand.Insert, EnumLlojVeprimiISKSH.Blerje, rresht.Sasia, rresht.KodiBarit, rresht.NrSerise, dbAccess);
                if (!mesazh)
                    return mesazh;
            }

            return dbAccess.InsertObjectCollection<FleteHyrjeDetajeISKSH>(colFleteHyrjeDetajeISKSH, TablesISKSH.FleteHyrjeDetajeISKSH, "Id");
        }

        /// <summary>
        /// Funksion qe ben ndryshimin e gjendjes se medikamenteve ne databazen e Access
        /// </summary>
        /// <param name="command">Lloji i commandes sql</param>
        /// <param name="llojiVeprimit">Lloji i veprimit qe po kryhet</param>
        /// <param name="sasia">Sasia e artikullit</param>
        /// <param name="kodiBarit">Kodi i barit</param>
        /// <param name="seria">Seria e artikullit</param>
        /// <param name="dbAccess"></param>
        /// <returns>Kthen mesazh gabimi nese deshton ndryshimi i gjendjes, mesazh suksesi ne te kundert</returns>
        private clsMesazh NdryshoGjendjeArtikujISKSH(EnumSqlCommand command, EnumLlojVeprimiISKSH llojiVeprimit, decimal sasia, string kodiBarit, string seria, DbAccess dbAccess)
        {
            string condition = $" KodiBarit = '{kodiBarit}' AND Seria = '{seria}'";
            string queryString = string.Empty;
            if ((command == EnumSqlCommand.Delete && llojiVeprimit == EnumLlojVeprimiISKSH.Shitje) || (command == EnumSqlCommand.Insert && llojiVeprimit == EnumLlojVeprimiISKSH.Blerje))
                queryString = $"UPDATE {TablesISKSH.GjendjeISKSH} SET GJENDJA = GJENDJA + {sasia} WHERE {condition}";
            else
                queryString = $"UPDATE {TablesISKSH.GjendjeISKSH} SET GJENDJA = GJENDJA - {sasia} WHERE {condition}";

            clsMesazh mesazh = dbAccess.UpdateByQueryString(queryString);
            if (!mesazh)
                return mesazh;

            return new MesazhSuksesi("Ndryshimi i gjendjes u krye me sukses!");
        }

        /// <summary>
        /// Funksion qe merr daten e skadences per medikamentet nga databaza e Access
        /// </summary>
        /// <param name="kodiBarit">Kodi i barit</param>
        /// <param name="nrSerise">Numri serial i medikamentit</param>
        /// <param name="dbAccess"></param>
        /// <returns>Kthen daten e skadences</returns>
        private DateTime VendosDateSkadencePerMedikament(string kodiBarit, string nrSerise, DbAccess dbAccess)
        {
            DataTable DataSkadence = dbAccess.SelectByQueryString($"select dt_skadences from serial_dt_skadence sds inner join medikamentet m on sds.kodibarit = m.kodibarit where sds.kodibarit='{kodiBarit}' and sds.seria = '{nrSerise}'");
            if (DataSkadence != null && DataSkadence.Rows.Count > 0)
                return Convert.ToDateTime(DataSkadence.Rows[0]["dt_skadences"]);

            DataSkadence = dbAccess.SelectByQueryString($"select dt_skadences from serial_dt_skadence sds inner join medikamentet m on sds.kodibarit = m.kodibarit where sds.kodibarit='{kodiBarit}'");
            if (DataSkadence != null && DataSkadence.Rows.Count > 0)
                return Convert.ToDateTime(DataSkadence.Rows[0]["dt_skadences"]);

            return Convert.ToDateTime("01/01/2008");

        }

        #region Validime per transferimin

        /// <summary>
        /// Funksion qe validon faturen e shitjes te ISKSH
        /// </summary>
        /// <param name="fatura">Koka e fatures se shitjes se ISKSH</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Kthen mesazh gabimi nese koka e fatures ka gabime, mesazh suksesi ne te kundert</returns>
        private clsMesazh ValidoFatureShitjejeISKSH(FatureShitjejeISKSH fatura, DbAccess dbAccess)
        {
            if (string.IsNullOrEmpty(fatura.KodiBleresit))
                return new MesazhGabimi($"Klienti nuk ka te plotesuar Kodin ISKSH!");

            if (string.IsNullOrEmpty(fatura.NiptBleresi))
                return new MesazhGabimi($"Fatura nuk ka te plotesuar Niptin!");

            if (fatura.NiptBleresi.Length > 10 || fatura.NiptBleresi.Contains(" "))
                return new MesazhGabimi($"Fusha Nipti ne fature nuk mund te permbaje hapesira dhe me shume se 10 karaktere!");

            if (EkzistonFaturaShitjesNeISKSH(fatura, dbAccess))
                return new MesazhGabimi($"Ekziston nje fature me numer dokumenti {fatura.NrDok} dhe date {fatura.Data}!");

            int llojSubjekti = 1;
            if (!EkzistonKlientNeISKSH(fatura.KodiBleresit, out llojSubjekti, dbAccess))
                return new MesazhGabimi($"Nuk ekziston klenti me kod {fatura.KodiBleresit}!");
            fatura.LlojiSubjektitBleresi = llojSubjekti;

            return new MesazhSuksesi();
        }

        /// <summary>
        /// Funksion qe validon trupin e fatures se shitjes te ISKSH
        /// </summary>
        /// <param name="trupFature">Trupi i fatures se shitjes se ISKSH</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Kthen mesazh gabimi nese koka e fatures ka gabime, mesazh suksesi ne te kundert</returns>
        private clsMesazh ValidoTrupFatureShitjejeISKSH(List<FatureShitjejeDetajeISKSH> trupFature, DbAccess dbAccess)
        {
            foreach(FatureShitjejeDetajeISKSH rresht in trupFature)
            {
                if(string.IsNullOrEmpty(rresht.KodiBarit))
                    return new MesazhGabimi($"Ne trup te fatures ka artikuj qe kane fushen Kodi i Barit te paplotesuar!");
                
                if (!EkzistonArtikullNeISKSH(rresht.KodiBarit, dbAccess))
                    return new MesazhGabimi($"Per faturen nuk ekziston artikulli me kod {rresht.KodiBarit}!");
            }
            return new MesazhSuksesi();
        }

        /// <summary>
        /// Funksion qe validon faturen e blerjes te ISKSH
        /// </summary>
        /// <param name="fatura">Koka e fatures se shitjes se ISKSH</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Kthen mesazh gabimi nese koka e fatures ka gabime, mesazh suksesi ne te kundert</returns>
        private clsMesazh ValidoFleteHyrjeISKSH(FleteHyrjeISKSH fatura, DbAccess dbAccess)
        {
            if (string.IsNullOrEmpty(fatura.KodiFurnitorit))
                return new MesazhGabimi($"Klienti nuk ka te plotesuar Kodin ISKSH!");

            if (string.IsNullOrEmpty(fatura.NiptFurnitori))
                return new MesazhGabimi($"Fatura nuk ka te plotesuar Niptin!");

            if (fatura.NiptFurnitori.Length > 10 || fatura.NiptFurnitori.Contains(" "))
                return new MesazhGabimi($"Fusha Nipti ne fature nuk mund te permbaje hapesira dhe me shume se 10 karaktere!");

            if (EkzistonFletaHyrjesNeISKSH(fatura, dbAccess))
                return new MesazhGabimi($"Ekziston nje fature me numer dokumenti {fatura.NrDok} dhe date {fatura.Data}!");

            int llojSubjekti = 1;
            if (!EkzistonKlientNeISKSH(fatura.KodiFurnitorit, out llojSubjekti, dbAccess))
                return new MesazhGabimi($"Nuk ekziston klenti me kod {fatura.KodiFurnitorit}!");
            fatura.LlojiSubjektitFurnitori = llojSubjekti;

            return new MesazhSuksesi();
        }

        /// <summary>
        /// Funksion qe validon trupin e fatures se blerjes te ISKSH
        /// </summary>
        /// <param name="trupFature">Trupi i fatures se shitjes se ISKSH</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Kthen mesazh gabimi nese koka e fatures ka gabime, mesazh suksesi ne te kundert</returns>
        private clsMesazh ValidoTrupFleteHyrjeISKSH(List<FleteHyrjeDetajeISKSH> trupFature, DbAccess dbAccess)
        {
            foreach (FleteHyrjeDetajeISKSH rresht in trupFature)
            {
                if (string.IsNullOrEmpty(rresht.KodiBarit))
                    return new MesazhGabimi($"Ne trup te fatures ka artikuj qe kane fushen Kodi i Barit te paplotesuar!");
                
                if (!EkzistonArtikullNeISKSH(rresht.KodiBarit, dbAccess))
                    return new MesazhGabimi($"Per faturen nuk ekziston artikulli me kod {rresht.KodiBarit}!");
            }
            return new MesazhSuksesi();
        }

        /// <summary>
        /// Funksion qe kontrollon ekzistencen e dokumentit te shitjes ne databaze Access
        /// </summary>
        /// <param name="fatura">Fatura per te cilen do behet kontrolli</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Kthen true nese koka e fatures ekziston, false ne te kundert</returns>
        private bool EkzistonFaturaShitjesNeISKSH(FatureShitjejeISKSH fatura, DbAccess dbAccess)
        {          
            string condition = $"NrDok = {fatura.NrDok} AND Data = DateValue('{fatura.Data}')";
            FatureShitjejeISKSH faturaEkzistuese = MerrObjektNgaDbAccessSipasKushtit<FatureShitjejeISKSH>(EnumStructTypeISKSH.FatureShitjejeISKSH, TablesISKSH.FatureShitjejeISKSH, condition, dbAccess);
            return faturaEkzistuese.Id > 0;
        }

        /// <summary>
        /// Funksion qe kontrollon ekzistencen e dokumentit te blerjes ne databaze Access
        /// </summary>
        /// <param name="fatura">Fatura per te cilen do behet kontrolli</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Kthen true nese koka e fatures ekziston, false ne te kundert</returns>
        private bool EkzistonFletaHyrjesNeISKSH(FleteHyrjeISKSH fatura, DbAccess dbAccess)
        {
            string condition = $"NrDok = {fatura.NrDok} AND Data = DateValue('{fatura.Data}')";
            FleteHyrjeISKSH faturaEkzistuese = MerrObjektNgaDbAccessSipasKushtit<FleteHyrjeISKSH>(EnumStructTypeISKSH.FleteHyrjeISKSH, TablesISKSH.FleteHyrjeISKSH, condition, dbAccess);
            return faturaEkzistuese.Id > 0;
        }

        /// <summary>
        /// Funksion qe kontrollon ekzistencen e klientit te shitjes ne databaze Access
        /// </summary>
        /// <param name="klienti">Kodi i klientit per te cilen do behet kontrolli</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Kthen true nese klienti ekziston, false ne te kundert</returns>
        private bool EkzistonKlientNeISKSH(string klienti, out int llojSubjekti, DbAccess dbAccess)
        {
            llojSubjekti = 1;
            DepoFarmaceutikeISKSH depo = DepoFarmaceutikeISKSH.getObject(klienti, dbAccess);
            if (!(string.IsNullOrEmpty(depo.KodiDepos)))
            {
                llojSubjekti = 2;
                return true;
            }

            FarmaciISKSH farmacia = FarmaciISKSH.getObject(klienti, dbAccess);
            if (!(string.IsNullOrEmpty(farmacia.KodFarmacise)))
            {
                llojSubjekti = 3;
                return true;
            }

            FirmeFarmaceutikeISKSH firme = FirmeFarmaceutikeISKSH.getObject(klienti, dbAccess);
            if (firme.Kodi > 0)
            {
                llojSubjekti = 1;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Funksion qe kontrollon ekzistencen e artikullit te shitjes ne databaze Access
        /// </summary>
        /// <param name="kodiBarit">Kodi i artikullit per te cilen do behet kontrolli</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Kthen true nese artikulli ekziston, false ne te kundert</returns>
        private bool EkzistonArtikullNeISKSH(string kodiBarit, DbAccess dbAccess)
        {
            MedikamentISKSH artikulli = MedikamentISKSH.getMedikamentISKSH(kodiBarit, dbAccess);
            return artikulli.Id > 0;
        }

        #endregion

        /// <summary>
        /// Funksion qe merr objektet nga databaza e access sipas kushteve
        /// </summary>
        /// <typeparam name="T">Tipi i objektit qe do kthehet</typeparam>
        /// <param name="structType">Enumeracion qe permban llojet e objekteve qe mund te krijohen</param>
        /// <param name="tabela">Emri i tabeles qe do perdoret ne veprim</param>
        /// <param name="condition">Kushtet sipas te cilave do merren objektet</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Kthen nje objekt te tipit T</returns>
        public static T MerrObjektNgaDbAccessSipasKushtit<T>(EnumStructTypeISKSH structType, string tabela, string condition, DbAccess dbAccess)
        {
            T newObject = Activator.CreateInstance<T>();
            DataTable table = dbAccess.SelectObject<T>(newObject, tabela, condition);
            if (table.Rows.Count <= 0)
                return newObject;
            return FaturaFactoryISKSH.KrijoObjekt<T>(structType, table.Rows[0]);
        }

        /// <summary>
        /// Funksion qe merr nje liste me objekte nga databaza e access sipas kushteve
        /// </summary>
        /// <typeparam name="T">Tipi i objektit qe do kthehet ne liste</typeparam>
        /// <param name="structType">Enumeracion qe permban llojet e objekteve qe mund te krijohen</param>
        /// <param name="tabela">Emri i tabeles qe do perdoret ne veprim</param>
        /// <param name="condition">Kushtet sipas te cilave do merren objektet</param>
        /// <param name="dbAccess">Objekti qe ben lidhjen me databazen ne access</param>
        /// <returns>Kthen nje liste me objekte te tipit T</returns>
        public static List<T> MerrListeObjekteshNgaDbAccessSipasKushtit<T>(EnumStructTypeISKSH structType, string tabela, string condition, DbAccess dbAccess)
        {
            List<T> newListObject = new List<T>();
            DataTable table = dbAccess.SelectObject<T>(Activator.CreateInstance<T>(), tabela, condition);
            if (table.Rows.Count <= 0)
                return new List<T>();
            return FaturaFactoryISKSH.krijoListeObjekt<T, DataRow>(structType, table.AsEnumerable().ToList());
        }
    }
}
