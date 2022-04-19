using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
    class clsAmbientModuli
    {
        #region Atributet

        private int idAmbientModuli;
        private String ambientKodi;
        private String ambientPershkrimi;
        private int idModuli;

        #endregion

        #region Properties

        /// <summary>
        /// id-ja autoincrement e ambjentit te modulit
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

        /// <summary>
        /// kodi i ambientit
        /// </summary>
        public String AmbientKodi
        {
            get
            {
                return ambientKodi;
            }
            set
            {
                if (ambientKodi == value)
                    return;
                ambientKodi = value;
            }
        }

        /// <summary>
        /// pershkrimi i ambientit
        /// </summary>
        public String AmbientPershkrimi
        {
            get
            {
                return ambientPershkrimi;
            }
            set
            {
                if (ambientPershkrimi == value)
                    return;
                ambientPershkrimi = value;
            }
        }

        /// <summary>
        /// id-ja modulit qe i perket
        /// </summary>
        public int IdModuli
        {
            get
            {
                return idModuli;
            }
            set
            {
                if (idModuli == value)
                    return;
                idModuli = value;
            }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor Bosh
        /// </summary>
        public clsAmbientModuli()
        { 

        }

        /// <summary>
        /// konstruktore i plote qe i jep vlere te gjitha fushave
        /// </summary>
        /// <param name="idAmbientModuli"></param>
        /// <param name="ambientKodi"></param>
        /// <param name="ambientPershkrimi"></param>
        /// <param name="idModuli"></param>
        public clsAmbientModuli(int idAmbientModuli, string ambientKodi, string ambientPershkrimi, int idModuli) 
        {
            this.idAmbientModuli = idAmbientModuli;
            this.ambientKodi = ambientKodi;
            this.ambientPershkrimi = ambientPershkrimi;
            this.idModuli = idModuli;
        }

        #endregion
    }
}
