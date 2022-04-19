using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
    public class clsInfoArt
    {
        #region Atribute
        private int idInfoArtikulliTrupi;
        private string periudha;
        private double kostoMagazine;
        private double sasiaMagazine;
        private double kostoTotale;
        private double sasiaTotale;
        private double cmimiShtijesFundit;
        private DateTime dataShitjesFundit;
        private double cmimiBlerjesFundit;
        private DateTime dataBlerjesFundit;
        private double sasiaMin;
        private double sasiaMax;
        private string njesia;

        #endregion
        #region konstruktoret
        public clsInfoArt(int idinfo, string periudha, double kostomag, double sasimag, double kostotot, double sasitot, double cmimsh, DateTime dtshitje, double cmimb, DateTime dtblerje, double sasimin, double sasimax, string njesia)
        {
            this.idInfoArtikulliTrupi = idinfo;
            this.periudha = periudha;
            kostoMagazine = kostomag;
            sasiaMagazine = sasimag;
            this.kostoTotale = kostotot;
            this.sasiaTotale = sasitot;
            this.cmimiShtijesFundit = cmimsh;
            this.dataShitjesFundit = dtshitje;
            this.cmimiBlerjesFundit = cmimb;
            this.dataBlerjesFundit = dtblerje;
            this.sasiaMin = sasimin;
            this.sasiaMax = sasimax;
            this.njesia = njesia;

        }
        public clsInfoArt()
        {
        }
        #endregion
        #region Properties
        public int IdInfoArtikulliTrupi { get { return idInfoArtikulliTrupi; } set { idInfoArtikulliTrupi = value; } }
        public string Periudha { get { return periudha; } set { periudha = value; } }
        public double KostoMagazine { get { return kostoMagazine; } set { kostoMagazine = value; } }
        public double SasiaMagazine { get { return sasiaMagazine; } set { sasiaMagazine = value; } }
        public double KostoTotale { get { return kostoTotale; } set { kostoTotale = value; } }
        public double SasiaTotale { get { return sasiaTotale; } set { sasiaTotale = value; } }
        public double CmimiShtijesFundit { get { return cmimiShtijesFundit; } set { cmimiShtijesFundit = value; } }
        public DateTime DataShitjesFundit { get { return dataShitjesFundit; } set { dataShitjesFundit = value; } }
        public double CmimiBlerjesFundit { get { return cmimiBlerjesFundit; } set { cmimiBlerjesFundit = value; } }
        public DateTime DataBlerjesFundit { get { return dataBlerjesFundit; } set { dataBlerjesFundit = value; } }
        public double SasiaMin { get { return sasiaMin; } set { sasiaMin = value; } }
        public double SasiaMax { get { return sasiaMax; } set { sasiaMax = value; } }
        public string Njesia { get { return njesia; } set { njesia = value; } }
        #endregion

    }
}
