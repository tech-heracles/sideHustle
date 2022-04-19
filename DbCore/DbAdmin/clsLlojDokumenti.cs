using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne
    ///  llojet e dokumentave (Te dhenat  merren nga tabela : T_LLOJDOKUMENTI)
    /// </summary>
    public class clsLlojDokumenti
    {
        #region Atribute

        private int idLlojDok;
        private String llojDokKod;
        private String llojdokPershk;
        private int moduli;
        private int idKomponente;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsLlojDokumenti(int id, String kod, String pershk, int mod, int idkomponente)
        {
            idLlojDok = id;
            llojDokKod = kod;
            llojdokPershk = pershk;
            moduli = mod;
            idKomponente = idkomponente;
        }

        public clsLlojDokumenti(int idLlojDok)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushLlojDokumenti(data.merrDokumentin(idLlojDok));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsLlojDokumenti()
        {
        }

        public clsLlojDokumenti(DataRow rreshti)
        {
            
            mbushLlojDokumenti(rreshti);
        }

        #endregion

        #region Properties

        //metodat per marrjen e te dhenave
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLlojDokumenti
        {
            get { return idLlojDok; }
            set { idLlojDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e llojit te dokumentit.
        /// </summary>
        public string LlojDokKodi
        {
            get { return llojDokKod; }
            set { llojDokKod = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e llojit te dokumentit.
        /// </summary>
        public string LlojDokPershkrimi
        {
            get { return llojdokPershk; }
            set { llojdokPershk = value; }
        }

        /// <summary>
        /// Nuk perdoret sepse ka ndryshuar menyra e konceptimit.Kthen/Vendos ID-ne e modulit te cilit i perket.
        /// </summary>
        public int IdModuli
        {
            get { return moduli; }
            set { moduli = value; }
        }

        /// <summary>
        /// Nuk perdoret sepse ka ndryshuar menyra e konceptimit.Kthen/Vendos ID-ne e komponentes te cilit i perket.
        /// </summary>
        public int IdKomponente
        {
            get { return idKomponente; }
            set { idKomponente = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Kthen nje collection me objekte te tipit <see cref="clsLlojDokumenti"/>.
        /// </summary>
        public colLlojDokumenti merriTeGjithe()
        {
            colLlojDokumenti data = new colLlojDokumenti(this.IdModuli);
            return data;

        }

        #endregion

        #region Metoda Internal

        internal bool mbushLlojDokumenti(DataRow dbDataRowLlojDokumenti)
        {
            if (dbDataRowLlojDokumenti != null)
            {
                try
                {
                    int.TryParse(dbDataRowLlojDokumenti["IDLLOJDOK"].ToString(), out idLlojDok);
                    llojDokKod = dbDataRowLlojDokumenti["KODI"].ToString();
                    llojdokPershk = dbDataRowLlojDokumenti["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowLlojDokumenti["MODULI"].ToString(), out moduli);
                    int.TryParse(dbDataRowLlojDokumenti["IDKOMPONENTE"].ToString(), out idKomponente);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llojit te dokumentit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
