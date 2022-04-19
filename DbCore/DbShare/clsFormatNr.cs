using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
    public class clsFormatNr
    {
        #region Atribute

        private int idFormatNr;
        private String kodFormat;
        private String vleraFormat;
        private DataRow rreshti;

        #endregion

        #region Konstruktor

        public clsFormatNr(int idForm, String kodForm, String vleraForm)
        {
            idFormatNr = idForm;
            kodFormat = kodForm;
            vleraFormat = vleraForm;
        }

        public clsFormatNr()
        {
        }

        public clsFormatNr(DataRow rreshti)
        {
            
            mbushFormatNr(rreshti);
        }

        #endregion

        #region Properties

        public int IdFormatNr
        {
            get { return idFormatNr; }
            set { idFormatNr = value; }
        }

        public String KodFormati
        {
            get { return kodFormat; }
            set { kodFormat = value; }
        }

        public String VlereFormati
        {
            get { return vleraFormat; }
            set { vleraFormat = value; }
        }

        public colFormatNr merrTeGjithe()
        {
            colFormatNr data = new colFormatNr();
            data.mbushFormatNr();
            return data;
        }

        #endregion

        #region Metoda Publike

        public static int mbushFormatNrSipasVlera(string vlera)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            int nr = data.ktheFormatNrSipasVlera(vlera);
            data.Dispose();
            return nr;
        }

        public bool mbushFormatNrSipasId(int idForamti)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool sukses = mbushFormatNr(data.ktheFormatNrSipasId(idForamti));
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushFormatNr(DataRow dbDataRowFormatNr)
        {
            if (dbDataRowFormatNr != null)
            {
                try
                {
                    int.TryParse(dbDataRowFormatNr["IDFORMAT"].ToString(), out idFormatNr);
                    kodFormat = dbDataRowFormatNr["KODIFORMAT"].ToString();
                    vleraFormat = dbDataRowFormatNr["VLERAFORMAT"].ToString();  
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se formatit te numrit nga db-ja");
                }
            }
            else
                return false;   
        }

        #endregion
    }
}

