using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje detajim regjistrim artikulli ne magazine
    ///  (Te dhenat  merren nga tabela :  T_DETAJIMREGJISTRIMMAGAZINE)
    /// </summary>

    public class clsDetajimArtikulliRegjistrim
    {

        #region Atribute

        private int idDetArtRegjistrim;
        private int idTrupiShitje;
        private int idDetajim;
        private double sasia;
        private double vlefta;
        private double cmimi;
        private double sasiaProgresive;
        private double vleftaProgresive;
        private int magazina;
        private DateTime data;
        private DataRow rreshti;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="cm"> cmimi i detajimit</param>
        /// <param name="dt"> data  e  regjistrimit</param>
        /// <param name="iddet">iddetajimi me te cilin lidhet</param>
        /// <param name="idregj"> idDetArtRegjistrim  id ritese</param>
        /// <param name="idtrupi"> idtrupishitje  me te cilin lidhet</param>
        /// <param name="mag"> magazina me te cilen lidhet</param>
        /// <param name="sas">sasia e regjistruar</param>
        /// <param name="sasiaP"> sasiaprogresive  pas ketij regjistrimi</param>
        /// <param name="vl"> vlefta e regjistruar</param>
        /// <param name="vleftP"> vleftaProgresive pas ketij regjistrimi</param>
        public clsDetajimArtikulliRegjistrim(int idregj, int idtrupi, int iddet, double sas, double vl, double cm, double sasiaP, double vleftP, int mag, DateTime dt)
        {
            idDetArtRegjistrim = idregj;
            idTrupiShitje = idtrupi;
            idDetajim = iddet;
            sasia = sas;
            vlefta = vl;
            cmimi = cm;
            sasiaProgresive = sasiaP;
            vleftaProgresive = vleftP;
            magazina = mag;
            data = dt;
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsDetajimArtikulliRegjistrim()
        {
        }

        public clsDetajimArtikulliRegjistrim(DataRow rreshti)
        {
            
            mbushDetajimArtikullRegjistrim(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdDetArtRegjistrim
        {
            get { return idDetArtRegjistrim; }
            set { idDetArtRegjistrim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e trupit te shitjes me te cilen lidhet ky regjistrim.
        /// </summary>
        public int IdTrupiShitje
        {
            get { return idTrupiShitje; }
            set { idTrupiShitje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit me te cilen lidhet ky regjistrim.
        /// </summary>
        public int IdDetajim
        {
            get { return idDetajim; }
            set { idDetajim = value; }
        }

        /// <summary>
        /// Kthen/Vendos sasine e regjistruar.
        /// </summary>
        public double Sasia
        {
            get { return sasia; }
            set { sasia = value; }
        }

        /// <summary>
        /// Kthen/Vendos  vleften e regjistruar.
        /// </summary>
        public double Vlefta
        {
            get { return vlefta; }
            set { vlefta = value; }
        }

        /// <summary>
        /// Kthen/Vendos cmimin e regjistruar.
        /// </summary>
        public double Cmimi
        {
            get { return cmimi; }
            set { cmimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos sasia progresive  e te gjithe regjistrimeve.
        /// </summary>
        public double SasiaProgresive
        {
            get { return sasiaProgresive; }
            set { sasiaProgresive = value; }
        }

        /// <summary>
        /// Kthen/Vendos  vleften progresive e te gjithe regjistrimeve
        /// </summary>
        public double VleftaProgresive
        {
            get { return vleftaProgresive; }
            set { vleftaProgresive = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e magazines me te cilen lidhet.
        /// </summary>
        public int Magazina
        {
            get { return magazina; }
            set { magazina = value; }
        }

        /// <summary>
        /// Kthen/Vendos data e regjistrimit.
        /// </summary>
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush detajimet e artikullit nga databaza
        /// </summary>
        /// <param name="dbDataRowDetajimArtikull">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushDetajimArtikullRegjistrim(DataRow dbDataRowDetajimArtikull)
        {
            if (dbDataRowDetajimArtikull != null)
            {
                try
                {
                    int.TryParse(dbDataRowDetajimArtikull["IDDETAJIMREGJISTRIM"].ToString(), out idDetArtRegjistrim);
                    int.TryParse(dbDataRowDetajimArtikull["IDTRUPISHITJE"].ToString(), out idTrupiShitje);
                    int.TryParse(dbDataRowDetajimArtikull["IDDETAJIM"].ToString(), out idDetajim);
                    double.TryParse(dbDataRowDetajimArtikull["SASIA"].ToString(), out sasia);
                    double.TryParse(dbDataRowDetajimArtikull["VLEFTA"].ToString(), out vlefta);
                    double.TryParse(dbDataRowDetajimArtikull["CMIMI"].ToString(), out cmimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se detajimeve te artikullit nga db-ja");
                }
            }
            else
                return false;
        }

        /// <summary>
        /// mbush detajimet e regjistrimeve ne magazine nga databaza
        /// </summary>
        /// <param name="dbDataRowRegjistrimMagazine">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushDetajimRegjistrimMagazine(DataRow dbDataRowRegjistrimMagazine)
        {
            if (dbDataRowRegjistrimMagazine != null)
            {
                try
                {
                    int.TryParse(dbDataRowRegjistrimMagazine["IDDETAJIMREGJISTRIM"].ToString(), out idDetArtRegjistrim);
                    int.TryParse(dbDataRowRegjistrimMagazine["IDTRUPISHITJE"].ToString(), out idTrupiShitje);
                    int.TryParse(dbDataRowRegjistrimMagazine["IDDETAJIM"].ToString(), out idDetajim);
                    double.TryParse(dbDataRowRegjistrimMagazine["SASIA"].ToString(), out sasia);
                    double.TryParse(dbDataRowRegjistrimMagazine["VLEFTA"].ToString(), out vlefta);
                    double.TryParse(dbDataRowRegjistrimMagazine["CMIMI"].ToString(), out cmimi);
                    double.TryParse(dbDataRowRegjistrimMagazine["SASIAPROGRESIVE"].ToString(), out sasiaProgresive);
                    double.TryParse(dbDataRowRegjistrimMagazine["VLEFTAPROGRESIVE"].ToString(), out vleftaProgresive);
                    int.TryParse(dbDataRowRegjistrimMagazine["MAGAZINA"].ToString(), out magazina);
                    DateTime.TryParse(dbDataRowRegjistrimMagazine["DATA"].ToString(), out data);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se detajimeve ne regjistrimin e magazinave nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
