using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne trupin e templateve te artikujve perberes
    ///  (Te dhenat  merren nga tabela : T_ARTIKULLIPERBERESTEMPLATETRUPI)
    /// </summary>
   public class clsArtikulliPerberesTemplateTrupi
   {

        #region Atribute

        private int idTrupi;
        private int lloji;
        private int idKoka;        
        private int idLidheseArt;
        private int koeficienti;
        private decimal vlera;
        private int idLidheseLlog;
        private DataRow rreshti;

       #endregion

        #region Konstruktoret

        /// <summary>
       /// kontruktori me parametra
       /// </summary>
       /// <param name="idtr"> id ritese e trupit</param>
       /// <param name="idko">id e kokes</param>
       /// <param name="lloj"> lloji</param>
       /// <param name="idlidh"> id e artikullit perberes</param>
       /// <param name="koef"> koeficienti</param>
       /// <param name="vl"> vlera</param>
        public clsArtikulliPerberesTemplateTrupi(int idtr,int idko, int lloj, int idlidhart, int koef, int vl, int idlidhllog)
        {
            idTrupi= idtr;
            lloji = lloj;
            idKoka = idko;
            idLidheseArt = idlidhart;
            koeficienti = koef;
            vlera = vl;
            idLidheseLlog = idlidhllog;
        }
       /// <summary>
       /// kontruktori pa parametra
       /// </summary>
        public clsArtikulliPerberesTemplateTrupi()
        { 
        }

        public clsArtikulliPerberesTemplateTrupi(int lloji, int idLidheseArt, int koeficienti, decimal vlera, int idLidheseLlog)
        {
            
            this.lloji = lloji;
            this.IdLidheseArt = idLidheseArt;
            this.koeficienti = koeficienti;
            this.vlera = vlera;
            this.idLidheseLlog = idLidheseLlog;
        }

        public clsArtikulliPerberesTemplateTrupi(DataRow rreshti)
        {
            
            mbushArtikulliPerberesTemplateTrupi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin
        /// </summary>
        public int Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit perberes.
        /// </summary>
        public int IdLidheseArt
        {
            get { return idLidheseArt; }
            set { idLidheseArt = value; }
        }
         /// <summary>
        /// Kthen/Vendos ID-ne e artikullit perberes.
        /// </summary>
        public int IdLidheseLlog
        {
            get { return idLidheseLlog; }
            set { idLidheseLlog = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne koeficientin.
        /// </summary>
        public int Koeficienti
        {
            get { return koeficienti; }
            set { koeficienti = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleften.
        /// </summary>
        public decimal Vlera
        {
            get { return vlera; }
            set { vlera = value; }
        }
        public int IdLidhese
            {
            get
                {
                if (lloji == 1)
                    return idLidheseArt;
                else return idLidheseLlog;
                }
            set
                {
                if (lloji == 1)
                    idLidheseArt = value;
                else idLidheseLlog = value;
                }
            }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// sherben per marrjen e artikujveperberes sipas nje template te trupit pas nje store procedure
        /// </summary>
        /// <param name="dbDataRowArtikullPerberesTemplateTrupi">merr nje parameter datarow qe eshte rreshti qe do mbushet</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushArtikulliPerberesTemplateTrupi(DataRow dbDataRowArtikullPerberesTemplateTrupi)
        {
            if (dbDataRowArtikullPerberesTemplateTrupi != null)
            {

                try
                {
                    int.TryParse(dbDataRowArtikullPerberesTemplateTrupi["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowArtikullPerberesTemplateTrupi["LLOJI"].ToString(), out lloji);
                    int.TryParse(dbDataRowArtikullPerberesTemplateTrupi["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRowArtikullPerberesTemplateTrupi["IDLIDHESEART"].ToString(), out idLidheseArt);
                    int.TryParse(dbDataRowArtikullPerberesTemplateTrupi["KOEFICIENTI"].ToString(), out koeficienti);
                    decimal.TryParse(dbDataRowArtikullPerberesTemplateTrupi["VLERA"].ToString(), out vlera);
                    int.TryParse(dbDataRowArtikullPerberesTemplateTrupi["IDLIDHESELLOG"].ToString(), out idLidheseLlog);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se artikullit perberes sipas template te trupit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

   }
}
