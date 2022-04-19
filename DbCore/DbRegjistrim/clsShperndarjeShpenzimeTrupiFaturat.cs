using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupat e faturave ne nje dokument shperndarje shpenzimesh
    ///  (Te dhenat  merren nga tabela : T_SHPERNDARJESHPENZIMETRUPIFATURAT)
    /// </summary>
    public class clsShperndarjeShpenzimeTrupiFaturat
    {
        #region Atribute

        private int idTrupiFaturat;
        private int idShperndarjeShpenzTrupi;
        private int idArtikull;
        private int idTrupiShitje;
        private double vlera;
        private bool shperndaj;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idartikull"> id e artikullit</param>
        /// <param name="idshp">id e trupit te dokumentit shperndarje shpenzimesh</param>
        /// <param name="idtrupi">id ritese e trupit te fatures </param>
        /// <param name="idtrupishitje">id e trupit te dokumentit te shitje blerjes</param>
        /// <param name="vl"> vlefta </param>
        public clsShperndarjeShpenzimeTrupiFaturat(int idtrupi, int idshp, int idartikull, int idtrupishitje, double vl, bool shperndaj)
        {
            idTrupiFaturat = idtrupi;
            idShperndarjeShpenzTrupi = idshp;
            idArtikull = idartikull;
            idTrupiShitje = idtrupishitje;
            vlera = vl;
            shperndaj = shperndaj;
        }
          public clsShperndarjeShpenzimeTrupiFaturat( Dictionary<string, object> rreshtDokuKlient, object shpenzimi)
        {
            this.IdArtikull = int.Parse(rreshtDokuKlient["IdArtikulli"].ToString());
            this.IdTrupiShitje = int.Parse(rreshtDokuKlient["IdTrupiMagazina"].ToString());
            this.Vlera = double.Parse(shpenzimi.ToString());
            this.Shperndaj = bool.Parse(rreshtDokuKlient["Shperndaj"].ToString());
          
         }
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsShperndarjeShpenzimeTrupiFaturat()
        {
        }

        public clsShperndarjeShpenzimeTrupiFaturat(DataRow rreshti)
        {
            
            mbushShperndarjeShpenzTrupFat(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupiFaturat
        {
            get { return idTrupiFaturat; }
            set { idTrupiFaturat = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e trupit te dokumentit shperndarje shpenzimesh.
        /// </summary>
        public int IdShperndarjeShpenzTrupi
        {
            get { return idShperndarjeShpenzTrupi; }
            set { idShperndarjeShpenzTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit.
        /// </summary>
        public int IdArtikull
        {
            get { return idArtikull; }
            set { idArtikull = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e trupit te dokumentit te shitje blerje.
        /// </summary>
        public int IdTrupiShitje
        {
            get { return idTrupiShitje; }
            set { idTrupiShitje = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleften.
        /// </summary>
        public double Vlera
        {
            get { return vlera; }
            set { vlera = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren e Shperndaj.
        /// </summary>
        public bool Shperndaj
        {
            get { return shperndaj; }
            set { shperndaj = value; }
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e fatures se shperndarjes se shpenzimeve nga databaza
        /// </summary>
        /// <param name="dbDataRowShperndarjeShpenzTrupFat">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushShperndarjeShpenzTrupFat(DataRow dbDataRowShperndarjeShpenzTrupFat)
        {
            if (dbDataRowShperndarjeShpenzTrupFat != null)
            {
                try
                {
                    int.TryParse(dbDataRowShperndarjeShpenzTrupFat["IDSHPERNDARJESHPENZTRUPIFATURAT"].ToString(), out idTrupiFaturat);
                    int.TryParse(dbDataRowShperndarjeShpenzTrupFat["IDSHPERNDARJESHPENZ"].ToString(), out idShperndarjeShpenzTrupi);
                    int.TryParse(dbDataRowShperndarjeShpenzTrupFat["IDARTIKULL"].ToString(), out idArtikull);
                    int.TryParse(dbDataRowShperndarjeShpenzTrupFat["IDTRUPISHITJE"].ToString(),out idTrupiShitje);
                    double.TryParse(dbDataRowShperndarjeShpenzTrupFat["VLERA"].ToString(),out vlera);
                    bool.TryParse(dbDataRowShperndarjeShpenzTrupFat["SHPERNDAJ"].ToString(), out shperndaj);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te fatures se shperndarjes se shpenzimeve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}