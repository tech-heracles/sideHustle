using System;
using System.Collections.Generic;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Extensions;
using static System.Convert;

namespace DbCore.DbInventari
{
    public abstract class clsSerialeUnikeMagazina : IDataBase
    {
        #region Atribute

        private float cmimi;
        private float sasia;

        #endregion

        #region Properties

        public int Id { get; set; }

        public int IdTrupiMagazine { get; set; }

        public int IdKokaMagazine { get; set; }

        public int IdLlojDokumentMagazine { get; set; }

        public int IdKategoriSeriali { get; set; }

        public int IdFormatSeriali { get; set; }

        public int IdArtikulli { get; set; }

        public int IdTVSH { get; set; }

        public float Cmimi
        {
            get
            {
                return cmimi;
            }

            set
            {
                cmimi = value;
            }
        }

        public float Sasia
        {
            get
            {
                return sasia;
            }

            set
            {
                sasia = value;
            }
        }

        public int IdSeti { get; set; }

        public string SerialiKryesore { get; set; }

        public int IdNdermarrje { get; set; }

        public bool ShfaqSerialKryesorNeGride { get; set; }

        public int IdMag { get; set; }

        public string KodArtikulli { get; set; }

        #endregion

        #region Konstruktore

        public clsSerialeUnikeMagazina()
        {
        }

        public clsSerialeUnikeMagazina(int idNdermarrje, int idKategoriSeriali, int idFormatSeriali, string artikulli, string artikullSet, string serialiKryesore)
        {
            IdKategoriSeriali = idKategoriSeriali;
            IdFormatSeriali = idFormatSeriali;
            IdNdermarrje = idNdermarrje;
            var art = new clsArtikulli(artikulli, idNdermarrje);
            IdArtikulli = art.IdArtikulli;
            IdTVSH = art.IdTvsh;
            SerialiKryesore = serialiKryesore;
            if (artikullSet != "")
                IdSeti = clsArtikulli.ktheIdArtikulli(artikullSet, idNdermarrje);

            ShfaqSerialKryesorNeGride = true;
            KodArtikulli = art.KodArtikulli;
        }

        public clsSerialeUnikeMagazina(int idNdermarrje, int idKategoriSeriali, int idFormatSeriali, string serialiKryesore, clsArtikulli artikull, clsArtikulli artSet, bool shfaqSerialKryesor, int idMag)
        {
            this.IdKategoriSeriali = idKategoriSeriali;
            this.IdFormatSeriali = idFormatSeriali;
            this.IdNdermarrje = idNdermarrje;
            this.IdArtikulli = artikull.IdArtikulli;
            this.IdTVSH = artikull.IdTvsh;
            this.SerialiKryesore = serialiKryesore;
            if (artSet != null)
                this.IdSeti = artSet.IdArtikulli;
            this.ShfaqSerialKryesorNeGride = shfaqSerialKryesor;
            this.IdMag = idMag;
            this.KodArtikulli = artikull.KodArtikulli;
        }

        public clsSerialeUnikeMagazina(IDataRecord record)
        {
            Mbush(record);
        }

        #endregion

        #region Metoda Publike

        public static DataTable MerrGjendjeSeriali(string seriali, bool serialKryesor, DateTime date, int idNdermarrje, string kategori, int idDok)
        {
            using (var db = new clsDatabaseInventari())
            {
                return !kategori.EqualsIgnoreCase(enumKategoriSerialesh.RINGARKUES.ToString())
                    ? db.MerrGjendjeSerialiUnik(seriali, serialKryesor, date, idNdermarrje, idDok)
                    : db.MerrGjendjeSerialiUnikRingarkues(seriali, serialKryesor, date, idNdermarrje, idDok);
            }
        }

        public static DataTable MerrSerialPerKthim(string seriali, string idtrupa, int idDok)
        {
            using (var db = new clsDatabaseInventari())
                return db.MerrSerialPerKthim(seriali, idtrupa, idDok);
        }
        public static DataTable MerrSerialPerKthimNgaDetajim(string seriali, int idDok)
        {
            using (var db = new clsDatabaseInventari())
                return db.MerrSerialPerKthimNgaDetajim(seriali, idDok);
        }

        public static DataTable MerrSerialetPerKthimNeRradhe(string seriali, string idtrupa, int idDok, int sasia, string serialetNeGride)
        {
            using (var db = new clsDatabaseInventari())
                return db.MerrSerialetPerKthimNeRradhe(seriali, idtrupa, idDok, sasia, serialetNeGride);
        }

        public static string MerrSerialinEPareNeRradhe(int idArtikulli, bool serialKryesor, DateTime date, int idNdermarrje, int idDok)
        {
            using (var db = new clsDatabaseInventari())
                return db.MerrSerialinEPareNeRradhe(idArtikulli, serialKryesor, date, idNdermarrje, idDok);
        }

        public static clsMesazh KontrolloSerialIPare(clsSerialeUnikeMagazina serialiIRi, string kodArtikulli, int idArtikulli, bool serialKryesor, DateTime date, int idNdermarrje, int idDok)
        {
            var serialiIPare = MerrSerialinEPareNeRradhe(idArtikulli, serialKryesor, date, idNdermarrje, idDok);

            if (serialiIRi == null)
                throw new Exception("Seriali nuk ekziston tek grupi i serialeve te reja");

            if (!((serialKryesor && serialiIRi.EshteSerialINjejte(serialiIPare, true)) || (!serialKryesor && serialiIRi.EshteSerialINjejteDytesor(serialiIPare))))
                return new MesazhInformimi(MessagesResource.Messages["serialiIPareNeRradhe"]
                    .Replace("kodArtikulli", kodArtikulli).Replace("serialiIPare", serialiIPare));

            return null;
        }

        public static DataTable MerrSerialetNeRradhe(int idArtikulli, int idNdermarrje, DateTime date, bool serialKryesor, string seriali, int sasia, string serialetNeGride, int idMag, int idDok)
        {
            using (var db = new clsDatabaseInventari())
            {
                return db.MerrSerialetNeRradhe(idArtikulli, serialKryesor, date.ToShortDateString(), idNdermarrje, seriali, sasia, serialetNeGride, idMag, idDok);
            }
        }

        public static DataTable MerrSerialetRingarkuesNeRradhe(int idArtikulli, int idNdermarrje, DateTime date, bool serialKryesor, string seriali, int sasia, string serialetNeGride, int idMag, DataTable dt, int idDok)
        {
            using (var db = new clsDatabaseInventari())
            {
                serialetNeGride = clsSerialeUnikeRingarkues.updateSerialetEkzistuesNeGride(serialetNeGride, dt);

                DataTable serialetDt = new DataTable();

                if (sasia >= 100)
                {

                    serialetDt = db.MerrSerialetNeRradheRingarkues(idArtikulli, serialKryesor, date.ToShortDateString(), idNdermarrje, seriali, sasia, serialetNeGride, idMag, 100, idDok);

                    serialetNeGride = clsSerialeUnikeRingarkues.updateSerialetEkzistuesNeGride(serialetNeGride, serialetDt);

                    sasia -= MerrSasine(serialetDt);
                    dt.Merge(serialetDt);
                }

                var perjashtuar = serialetNeGride;

                while (sasia >= 10)
                {
                    serialetDt = db.MerrSerialetNeRradheRingarkues(idArtikulli, serialKryesor, date.ToShortDateString(), idNdermarrje, seriali, 10, perjashtuar, idMag, 10, idDok);
                    if (serialetDt.Rows.Count == 0)
                        break;

                    var paketa = clsSerialeUnikeRingarkues.GjeneroBatch(serialetDt.Rows[0]["SERIALI_KRYESOR"].ToString(), false, false);
                    foreach (var pack in paketa)
                    {
                        if (serialetNeGride.Contains(pack) || string.Compare(pack, seriali, StringComparison.InvariantCultureIgnoreCase) <= 0)
                            continue;
                        var serialGjendje = db.MerrGjendjeSerialiUnikRingarkues(pack, true, date, idNdermarrje, idDok);
                        if (serialGjendje.Rows.Count > 0 && Convert.ToInt32(serialGjendje.Rows[0]["SASI"]) == 10 && Convert.ToInt32(serialGjendje.Rows[0]["IDMAG"]) == idMag)
                        {
                            dt.Merge(serialGjendje);
                            sasia -= 10;
                            serialetNeGride = clsSerialeUnikeRingarkues.updateSerialetEkzistuesNeGride(serialetNeGride, serialGjendje);
                            if (sasia < 10)
                                break;
                        }
                        perjashtuar = clsSerialeUnikeRingarkues.updateSerialetEkzistuesNeGride(perjashtuar, pack, 10);
                    }
                    perjashtuar = $"{perjashtuar},'{serialetDt.Rows[0]["SERIALI_KRYESOR"].ToString()}'".TrimStart(',');
                    //marrja e serialeve me 10 
                }

                perjashtuar = serialetNeGride;
                while (sasia > 0)
                {
                    serialetDt = db.MerrSerialetNeRradheRingarkues(idArtikulli, serialKryesor, date.ToShortDateString(), idNdermarrje, seriali, 1, perjashtuar, idMag, 1, idDok);
                    if (serialetDt.Rows.Count == 0)
                        break;

                    var paketa = clsSerialeUnikeRingarkues.GjeneroBatch(serialetDt.Rows[0]["SERIALI_KRYESOR"].ToString(), true, false);
                    foreach (var pack in paketa)
                    {
                        if (serialetNeGride.Contains(pack) || string.Compare(pack, seriali, StringComparison.InvariantCultureIgnoreCase) <= 0)
                            continue;
                        var serialGjendje = db.MerrGjendjeSerialiUnikRingarkues(pack, true, date, idNdermarrje, idDok);
                        if (serialGjendje.Rows.Count > 0 && Convert.ToInt32(serialGjendje.Rows[0]["SASI"]) == 1 && Convert.ToInt32(serialGjendje.Rows[0]["IDMAG"]) == idMag)
                        {
                            dt.Merge(serialGjendje);
                            sasia -= 1;
                            serialetNeGride = clsSerialeUnikeRingarkues.updateSerialetEkzistuesNeGride(serialetNeGride, serialGjendje);
                            if (sasia == 0)
                                break;
                        }
                        perjashtuar = clsSerialeUnikeRingarkues.updateSerialetEkzistuesNeGride(perjashtuar, pack, 1);
                    }
                    perjashtuar = $"{perjashtuar},'{serialetDt.Rows[0]["SERIALI_KRYESOR"].ToString()}'".TrimStart(',');

                    //marrja e serialeve teke
                }

                return dt;
            }
        }

        public static int MerrSasine(DataTable dt)
        {
            int sasia = 0;
            foreach (DataRow row in dt.Rows)
                sasia += Convert.ToInt32(row["SASI"]);

            return sasia;
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public virtual void Mbush(IDataRecord record)
        {
            Id = !IsDBNull(record["ID"])
                ? ToInt32(record["ID"])
                : 0;
            IdTrupiMagazine = !IsDBNull(record["ID_TRUPI_MAGAZINE"])
                ? ToInt32(record["ID_TRUPI_MAGAZINE"])
                : 0;
            IdKokaMagazine = !IsDBNull(record["ID_KOKA_MAGAZINE"])
                ? ToInt32(record["ID_KOKA_MAGAZINE"])
                : 0;
            IdKategoriSeriali = !IsDBNull(record["ID_KATEGORI_SERIALI"])
                ? ToInt32(record["ID_KATEGORI_SERIALI"])
                : 0;
            IdFormatSeriali = !IsDBNull(record["ID_FORMAT_SERIALI"])
                ? ToInt32(record["ID_FORMAT_SERIALI"])
                : 0;
            IdArtikulli = !IsDBNull(record["ID_ARTIKULLI"])
                ? ToInt32(record["ID_ARTIKULLI"])
                : 0;
            IdMag = !IsDBNull(record["IDMAG"])
                ? ToInt32(record["IDMAG"])
                : 0;
            IdLlojDokumentMagazine = !IsDBNull(record["IDLLOJDOKUMENTIMAGAZINE"])
                ? ToInt32(record["IDLLOJDOKUMENTIMAGAZINE"])
                : 0;
            float.TryParse(record["CMIMI"].ToString(), out cmimi);
            IdTVSH = !IsDBNull(record["ID_TVSH"])
                ? ToInt32(record["ID_TVSH"])
                : 0;
            float.TryParse(record["SASIA"].ToString(), out sasia);
            SerialiKryesore = record["SERIALI_KRYESOR"].ToString();
            IdSeti = !IsDBNull(record["ID_SETI"])
                ? ToInt32(record["ID_SETI"])
                : 0;
            ShfaqSerialKryesorNeGride = !IsDBNull(record["SHFAQSERIALKRYESORNEGRIDE"]) && ToBoolean(record["SHFAQSERIALKRYESORNEGRIDE"]);
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public abstract clsMesazh Ruaj();

        /// <summary>
        /// Kontrollon nese seriali i kaluar si parameter eshte i njejte me serialin e ketij objekti
        /// </summary>
        /// <param name="seriali"></param>
        /// <returns></returns>
        public virtual bool EshteSerialINjejte(string seriali, bool kontrollAnasjellte)
        {
            return SerialiKryesore == seriali;
        }

        /// <summary>
        /// Kontrollon seriali i kaluar si parameter eshte i njejte me serialin dytesor ketij objekti. 
        /// Duke qene se vetem kartat kane serial dytesor, vetem ne ate kategori behet kontroll nderkohe qe kategorite e tjera kthejne false
        /// </summary>
        /// <param name="seriali"></param>
        /// <returns></returns>
        public virtual bool EshteSerialINjejteDytesor(string seriali)
        {
            return false;
        }

        public virtual string MerrSerialDytesor() { return string.Empty; }

        public virtual string MerrSerialKryesorDheSerialetETijPerberes() { return SerialiKryesore; }

        public virtual List<string> MerrSerialKryesorDheSerialetETijPerberesSiListe()
        {
            return new List<string> { SerialiKryesore };
        }

        #endregion

        #region Metoda Abstrakte Internal

        internal abstract void shtoParametratKokaTrup(int idTrupi, int idKoka, int idLlojDokumentiMagazine, float cmimi);

        internal virtual DataRow FillDataRow(DataRow dtRow)
        {
            dtRow["ID"] = Id;
            dtRow["ID_TRUPI_MAGAZINE"] = IdTrupiMagazine;
            dtRow["ID_KOKA_MAGAZINE"] = IdKokaMagazine;
            dtRow["ID_KATEGORI_SERIALI"] = IdKategoriSeriali;
            dtRow["ID_FORMAT_SERIALI"] = IdFormatSeriali;
            dtRow["ID_ARTIKULLI"] = IdArtikulli;
            dtRow["IDLLOJDOKUMENTIMAGAZINE"] = IdLlojDokumentMagazine;
            dtRow["CMIMI"] = Cmimi;
            dtRow["ID_TVSH"] = IdTVSH;
            dtRow["SASIA"] = Sasia;
            dtRow["SERIALI_KRYESOR"] = SerialiKryesore;
            dtRow["ID_SETI"] = IdSeti;
            dtRow["SHFAQSERIALKRYESORNEGRIDE"] = ShfaqSerialKryesorNeGride;
            return dtRow;
        }

        internal virtual clsSerialeUnikeMagazina Clone()
        {
            return (clsSerialeUnikeMagazina)MemberwiseClone();
        }

        #endregion
    }
}
