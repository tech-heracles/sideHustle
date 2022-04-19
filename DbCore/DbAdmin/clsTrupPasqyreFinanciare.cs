using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje element te trupit te pasqyres financiare
    ///  (Te dhenat  merren nga tabela : T_PASQYRAFINANCIARETRUPI)
    /// </summary>
    public class clsTrupPasqyreFinanciare
    {
        // properties
        private int idTrupi;
        private int idKoka;
        private String kodiZerit;
        private String pershkrimiZerit;
        private int prindiZerit;
        private int niveliZerit;
        private String llojiZerit;


        //konstruktoret
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTrupPasqyreFinanciare(int idtrupi, int idkoka, String kodizerit, String pershkrimizerit,int prindizerit,int nivelizerit, String llojizerit)
        {
            idTrupi = idtrupi;
            idKoka = idkoka;
            kodiZerit = kodizerit;
            pershkrimiZerit = pershkrimizerit;
            prindiZerit = prindizerit;
            niveliZerit = nivelizerit;
            llojiZerit = llojizerit;

        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTrupPasqyreFinanciare(int idkoka, String kodizerit, string pershkrimizerit,int prindizerit,int nivelizerit, String llojizerit)
        {
            idKoka = idkoka;
            kodiZerit = kodizerit;
            pershkrimiZerit = pershkrimizerit;
            prindiZerit = prindizerit;
            niveliZerit = nivelizerit;
            llojiZerit = llojizerit;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTrupPasqyreFinanciare()
        {
        }

        //metodat per marrjen e te dhenave
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se pasqyres financiare te ciles i perket ky element i trupit
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e zerit
        /// </summary>
        public String KodiZerit
        {
            get { return kodiZerit; }
            set { kodiZerit = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e zerit
        /// </summary>
        public String PershkrimiZerit
        {
            get { return pershkrimiZerit; }
            set { pershkrimiZerit = value; }
        }

        /// <summary>
        /// Kthen/Vendos prindin e zerit nese ka prind
        /// </summary>
        public int PrindiZerit
        {
            get { return prindiZerit; }
            set { prindiZerit = value; }
        }

        /// <summary>
        /// Kthen/Vendos nivelin e zerit (hierarkine)
        /// </summary>
        public int NiveliZerit
        {
            get { return niveliZerit; }
            set { niveliZerit = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin e zerit (aktive, detyrime etj)
        /// </summary>
        public String LlojiZerit
        {
            get { return llojiZerit; }
            set { llojiZerit = value; }
        }

        //public bool ruaj()
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    bool u_ruajt = data.ruajTrupPasqyreFinaciare(this);
        //    return true;
        //}

        //public bool modifiko()
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    bool u_modifikua = data.modifikoTrupPasqyreFinaciare(this);
        //    return u_modifikua;
        //}

        //public bool fshi()
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    bool u_fshi = data.fshiTrupPasqyreFinaciare(this);
        //    return u_fshi;
        //}
    }
}
