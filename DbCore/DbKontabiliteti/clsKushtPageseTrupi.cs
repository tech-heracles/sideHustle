using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne trupin e nje kushti pagese
    ///  (Te dhenat  merren nga tabela : T_KUSHTPAGESETRUPI)
    /// </remarks>
    public class clsKushtPageseTrupi
    {
        #region Atribute

        private int idTrupi;
        private int idKoka;
        private String intervali;
        private String periudha;
        private int dite;
        private int zbritje;
        private int kushtPagese;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsKushtPageseTrupi(int idtrupi, int idkoka, String inter, String per, int d, int z, int perq, int kusht)
        {
            idTrupi = idtrupi;
            idKoka = idkoka;
            intervali = inter;
            periudha = per;
            dite = d;
            zbritje = z;
            kushtPagese = kusht;
        }

        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsKushtPageseTrupi()
        { 
        }

        public clsKushtPageseTrupi(DataRow rreshti)
        {
            
            mbushKushtPagTrup(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se kushtit te pageses
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos intervalin - Dite, Jave, Muaj
        /// </summary>
        public String Intervali
        {
            get { return intervali ; }
            set { intervali = value; }
        }

        /// <summary>
        /// Kthen/Vendos periudhen = Dite e caktuar, Fillim muaji. Mbarim muaji
        /// </summary>
        public String Periudha
        {
            get { return periudha; }
            set { periudha = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e diteve
        /// </summary>
        public int Dite
        {
            get { return dite; }
            set { dite = value; }
        }

        /// <summary>
        /// Kthen/Vendos zbritjen qe aplikohet nese plotesohet kushti i pageses
        /// </summary>
        public int Zbritje
        {
            get { return zbritje; }
            set { zbritje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kushtit te pageses ( qe mund te zgjidhet kur krijohet/modifikohet nje kusht tjeter pagese)
        /// </summary>
        public int KushtPagese
        {
            get { return kushtPagese; }
            set { kushtPagese = value; }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e kushtit te pageses nga databaza
        /// </summary>
        /// <param name="dbDataRowKushtPagTrup">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKushtPagTrup(DataRow dbDataRowKushtPagTrup)
        {
            if (dbDataRowKushtPagTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowKushtPagTrup["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowKushtPagTrup["IDKOKA"].ToString(), out idKoka);
                    intervali = dbDataRowKushtPagTrup["INTERVALI"].ToString();
                    periudha = dbDataRowKushtPagTrup["PERIUDHA"].ToString();
                    int.TryParse(dbDataRowKushtPagTrup["DITE"].ToString(), out dite);
                    int.TryParse(dbDataRowKushtPagTrup["ZBRITJE"].ToString(), out zbritje);
                    int.TryParse(dbDataRowKushtPagTrup["KUSHTPAGESE"].ToString(), out kushtPagese);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te kushtit te pageses nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}

