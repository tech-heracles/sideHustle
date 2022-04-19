using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// Kjo klase perfaqeson klasat e objekteve. psh. Monedha, Ndermarrje, llogari etj. Keto do te perdoren per te caktuar te drejtat ne keto klasa
    /// </summary>
    class clsKlaseObjektesh
    {
        #region Atributet
        /// <summary>
        /// id-ja e klases se objekteve
        /// </summary>
        private int idKlaseObjektesh;
        /// <summary>
        /// pershkrimi i klases se objekteve
        /// </summary>
        private String pershkrimKlaseObjektesh;
        /// <summary>
        /// id-ja e ambientmodulit perkates
        /// </summary>
        private int idAmbientModuli;
        #endregion

        #region Properties

        /// <summary>
        /// get dhe set id-ne e klases se objekteve
        /// </summary>
        public int IdKlaseObjektesh
        {
            get
            {
                return idKlaseObjektesh;
            }
            set
            {
                if (idKlaseObjektesh == value)
                    return;
                idKlaseObjektesh = value;
            }
        }

        /// <summary>
        /// get dhe set pershkrimin e klases se objekteve
        /// </summary>
        public String PershkrimKlaseObjektesh
        {
            get
            {
                return pershkrimKlaseObjektesh;
            }
            set
            {
                if (pershkrimKlaseObjektesh == value)
                    return;
                pershkrimKlaseObjektesh = value;
            }
        }

        /// <summary>
        /// get dhe set id-ne e ambientmodulit perkates
        /// </summary> 
        public int IdAmbientModuli
        {
            get
            {
                return idAmbientModuli;
            }
            set
            {
                if (idAmbientModuli == value)
                    return;
                idAmbientModuli = value;
            }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor Bosh
        /// </summary>
        public clsKlaseObjektesh()
        { 

        }

        /// <summary>
        /// konstruktore i plote qe i jep vlere te gjitha fushave
        /// </summary>
        /// <param name="idAmbientModuli"></param>
        /// <param name="ambientKodi"></param>
        /// <param name="ambientPershkrimi"></param>
        /// <param name="idModuli"></param>
        public clsKlaseObjektesh(int idKlaseObjektesh, string pershkrimKlaseObjektesh, int idAmbientModuli) 
        {
            this.idKlaseObjektesh = idKlaseObjektesh;
            this.pershkrimKlaseObjektesh = pershkrimKlaseObjektesh;
            this.idAmbientModuli = idAmbientModuli;        
        }

        #endregion
    }
}
