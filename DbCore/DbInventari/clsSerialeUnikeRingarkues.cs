using DbCore.IMBUtils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;


namespace DbCore.DbInventari
{
    public class clsSerialeUnikeRingarkues : clsSerialeUnikeMagazina
    {
        #region Atribute

        private string shitBatch;
        private string batchPerPack;
        private string cardsPerBatch;
        private string cardPartNo;

        #endregion

        #region Konstruktore
        public clsSerialeUnikeRingarkues() { }

        public clsSerialeUnikeRingarkues(int idNdermarrje, int idKategori, int idFormati, string artikulli, string artikullSet, string shitBatch, string batchPerPack, string cardsPerBatch, string serialiKryesore, string cardPartNo) :
            base(idNdermarrje, idKategori, idFormati, artikulli, artikullSet, serialiKryesore)
        {
            this.shitBatch = shitBatch;
            this.batchPerPack = batchPerPack;
            this.cardsPerBatch = cardsPerBatch;
            this.cardPartNo = cardPartNo;
            Sasia = PercaktoSasi();
        }

        public clsSerialeUnikeRingarkues(int idNdermarrje, int idKategori, int idFormati, string serialiKryesore, clsArtikulli artikull, clsArtikulli artSet, string shitBatch, string batchPerPack, string cardsPerBatch, string cardPartNo, bool shfaqSerialKryesor, int idMag, string formula) :
           base(idNdermarrje, idKategori, idFormati, serialiKryesore, artikull, artSet, shfaqSerialKryesor, idMag)
        {
            this.shitBatch = shitBatch;
            this.batchPerPack = batchPerPack;
            this.cardsPerBatch = cardsPerBatch;
            this.cardPartNo = cardPartNo;
            Sasia = PercaktoSasi();
        }

        public clsSerialeUnikeRingarkues(IDataRecord record) 
        {
            Mbush(record);
        }

        #endregion

        #region Metoda Publike

        public override clsMesazh Ruaj()
        {
            clsMesazh mesazh = new MesazhGabimi();
            int id = 0;
            using (clsDatabaseInventari serialMagazineDb = new clsDatabaseInventari())
            {
                mesazh = serialMagazineDb.ruajSerialeUnikeMagazine(out id, IdTrupiMagazine, IdKokaMagazine, IdLlojDokumentMagazine, IdKategoriSeriali, IdFormatSeriali, IdArtikulli, IdTVSH, Cmimi,
                    Sasia, IdSeti, SerialiKryesore, shitBatch, batchPerPack, cardsPerBatch, cardPartNo);
                Id = id;
                return mesazh;
            }
        }

        internal override void shtoParametratKokaTrup(int idTrupi, int idKoka, int idLlojDokumentiMagazine, float cmimi)
        {
            IdTrupiMagazine = idTrupi;
            IdKokaMagazine = idKoka;
            IdLlojDokumentMagazine = idLlojDokumentiMagazine;
            Cmimi = cmimi;
        }

        public override void Mbush(IDataRecord record)
        {
            base.Mbush(record);
            shitBatch = record["SHITBATCH"].ToString();
            batchPerPack = record["BATCHPERPACK"].ToString();
            cardsPerBatch = record["CARDSPERBATCH"].ToString();
            cardPartNo = record["CARDPARTNO"].ToString();
        }

        internal override DataRow FillDataRow(DataRow dtRow)
        {
            base.FillDataRow(dtRow);
            dtRow["SHITBATCH"] = shitBatch;
            dtRow["BATCHPERPACK"] = batchPerPack;
            dtRow["CARDSPERBATCH"] = cardsPerBatch;
            dtRow["CARDPARTNO"] = cardPartNo;
            return dtRow;
        }

        private float PercaktoSasi()
        {
            string[] serialParts = this.SerialiKryesore.Split('-');
            if (serialParts.Length != 3)
                throw new Exception("Format i gabuar");
            if (int.TryParse(serialParts[2], out int vlereNr) && vlereNr > 0)
                return 1;
            if (int.TryParse(serialParts[1], out vlereNr) && vlereNr > 0)
                return Convert.ToInt32(this.cardsPerBatch);

            return Convert.ToInt32(this.shitBatch);
        }

        /// <summary>
        /// Kontrollon nese seriali eshte i barabarte me serialin qe permban objekti
        /// Ne rastin konkret seriali mund te mos jete plotesisht i barabarte, por nese ai perfaqeson nje pako ose nje batch, atehere mjafton qe nje pjese e serialeve te jete e barabarte
        /// </summary>
        /// <param name="seriali"></param>
        /// <returns></returns>
        public override bool EshteSerialINjejte(string seriali, bool kontrollAnasjellte)
        {
            if (!kontrollAnasjellte)
                return KontrolloSerialet(seriali, this.SerialiKryesore);
            return KontrolloSerialet(seriali, this.SerialiKryesore) || KontrolloSerialet(this.SerialiKryesore, seriali);
        }
        private  bool KontrolloSerialet(string serialiIPare, string serialiIDyte)
        {
            string[] serialParts = serialiIPare.Split('-');
            string[] thisSerialParts = serialiIDyte.Split('-');

            if (serialParts.Length != 3 || thisSerialParts.Length != 3)
                return serialiIDyte == serialiIPare;//kontrolli behet dhe per seriale te tjere qe nuk jane domosdoshmerisht ringarkues

            int vlereNr = 0;

            if (int.TryParse(thisSerialParts[2], out vlereNr) && vlereNr > 0)
                return serialiIDyte == serialiIPare;
            if (int.TryParse(thisSerialParts[1], out vlereNr) && vlereNr > 0)
                return thisSerialParts[0] == serialParts[0] && thisSerialParts[1] == serialParts[1];

            return thisSerialParts[0] == serialParts[0];
        }
        public override string MerrSerialKryesorDheSerialetETijPerberes()
        {
            List<string> serialet = MerrSerialKryesorDheSerialetETijPerberesSiListe();
            
            string serialeEkzistuese = "";
            foreach (var s in serialet)
            {
                serialeEkzistuese = $"{serialeEkzistuese}'{s}',";
            }

            return serialeEkzistuese.TrimEnd(',').Trim('\'');
        }
        public override List<string> MerrSerialKryesorDheSerialetETijPerberesSiListe()
        {
            List<string> serialet = new List<string>();

            if (this.Sasia == 1)
                serialet.Add(this.SerialiKryesore);
            else if (this.Sasia == 10)
                serialet.AddRange(clsSerialeUnikeRingarkues.GjeneroPaketen(this.SerialiKryesore, true));
            else
                serialet.AddRange(clsSerialeUnikeRingarkues.GjeneroBatch(this.SerialiKryesore, true, false));

            return serialet;
        }

        public static List<string> GjeneroPaketen(string seriali, bool shtoVetenNeListe)
        {
            List<string> serialet = new List<string>();
            string[] serialParts = seriali.Split('-');
            if(shtoVetenNeListe)
                serialet.Add(seriali);
            string zero = "0";
            for(int i = 1; i <= 10; i++)
            {
                string pjesaERe = zero.Replicate(serialParts[2].Length - (i < 10 ? 1 : 2)) + i.ToString();
                serialet.Add($"{serialParts[0]}-{serialParts[1]}-{pjesaERe}");
            }

            return serialet;
        }
        public static List<string> GjeneroBatch(string seriali, bool shtoVetenNeListe, bool shtoVetemSerialetPerberes)
        {
            List<string> serialet = new List<string>();
            string[] serialParts = seriali.Split('-');
            if(shtoVetenNeListe)
                serialet.Add(seriali);
            string zero = "0";
            for (int i = 1; i <= 10; i++)
            {
                string pjesaERe = zero.Replicate(serialParts[1].Length - (i < 10 ? 1 : 2)) + i.ToString();
                if (shtoVetenNeListe && !shtoVetemSerialetPerberes)
                    serialet.AddRange(clsSerialeUnikeRingarkues.GjeneroPaketen($"{serialParts[0]}-{pjesaERe}-{serialParts[2]}", shtoVetenNeListe));
                else if(!shtoVetenNeListe && shtoVetemSerialetPerberes)
                    serialet.AddRange(clsSerialeUnikeRingarkues.GjeneroPaketen($"{serialParts[0]}-{pjesaERe}-{serialParts[2]}", false));
                else
                    serialet.Add($"{serialParts[0]}-{ pjesaERe}-{ serialParts[2]}");
            }

            return serialet;
        }

        public static float gjejSasine(string serialiKryesor, string formula)
        {
            return new clsSerialeUnikeRingarkues()
            {
                SerialiKryesore = serialiKryesor,
                shitBatch = "100",
                cardsPerBatch = "10"
            }.PercaktoSasi();
            
        }

        public static string updateSerialetEkzistuesNeGride(string serialetNeGride, DataTable dt)
        {

            foreach(DataRow row in dt.Rows){
                var ser = new clsSerialeUnikeRingarkues()
                {
                    SerialiKryesore = row["SERIALI_KRYESOR"].ToString(),
                    Sasia = Convert.ToInt32(row["SASI"])
                }.MerrSerialKryesorDheSerialetETijPerberes();

                serialetNeGride = $"{serialetNeGride},'{ser}'";
            }

            return serialetNeGride.TrimStart(',');
        }

        public static string updateSerialetEkzistuesNeGride(string serialetNeGride, string seriali, int sasia)
        {
            var ser = new clsSerialeUnikeRingarkues()
            {
                SerialiKryesore = seriali,
                Sasia = sasia
            }.MerrSerialKryesorDheSerialetETijPerberes();

            serialetNeGride = $"{serialetNeGride},'{ser}'";
            return serialetNeGride.TrimStart(',');
        }
        #endregion
    }
}
