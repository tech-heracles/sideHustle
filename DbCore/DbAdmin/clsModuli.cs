using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje grupim llogjik te funksionalitetit
    ///  te disa faqeve. Perdoren tek caktimi i te drejtave te perdoruesit
    ///  (Te dhenat  merren nga tabela : T_MODULI)
    /// </summary>
    public class clsModuli
    {
        #region Atributet

        private int idModuli;
        private String kodiModuli;
        private String pershkrimiModuli;
        private int idGjuha;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        /// <param name="idGjuha"></param>
        public clsModuli(int idGjuha,   int idmoduli, String kodimoduli, String pershkrimimoduli)
        {
            idModuli = idmoduli;
            kodiModuli = kodimoduli;
            pershkrimiModuli = pershkrimimoduli;
            this.idGjuha = idGjuha;


        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        /// <param name="idGjuha"></param>
        public clsModuli(int idGjuha, String kodimoduli, String pershkrimimoduli)
        {
            kodiModuli = kodimoduli;
            pershkrimiModuli = pershkrimimoduli;
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        /// <param name="idGjuha"></param>
        public clsModuli(int idGjuha)
        {
            this.idGjuha = idGjuha;
        }

        public clsModuli(int idGjuha, int idModuli)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                mbushModul(idGjuha, data.ktheModulById(idModuli));
        }
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdModuli
        {
            get { return idModuli; }
            set { idModuli = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e modulit, qe eshte nje shkurtim llogjik i pershkrimit te tij.
        /// </summary>
        public String KodiModuli
        {
            get { return kodiModuli; }
            set { kodiModuli = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e modulit.
        /// </summary>
        public String PershkrimiModuli
        {
            get { return pershkrimiModuli; }
            set { pershkrimiModuli = value; } 
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbAdmin.clsModuli"/>
        /// </summary>
        /// <param name="idGjuha"></param>
        public colModulet merriTeGjithe(int idGjuha)
        {
            colModulet data = new colModulet();
            data.mbushGjitheModulet(idGjuha);
            return data;
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbAdmin.clsModuli"/> qe kane kane raporte
        /// </summary>
        /// <param name="idGjuha"></param>
        public colModulet merriTeGjitheeRaporteve(int idGjuha)
        {
            colModulet data = new colModulet();
            data.mbushGjitheModuleteRaporteve(idGjuha);
            return data;

        }
        public clsModuli merrModulByModulId(int idModuli)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushModul(idGjuha, data.ktheModulById(idModuli));
            data.Dispose();
            return this;
        }

        public clsModuli merrModulById(int idGjuha)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushModul(idGjuha, data.ktheModulById(this.idModuli));
            data.Dispose();
            return this;

        }

        public static string merrKodModuli(int gjuha, int idModuli)
        {
            clsModuli clsMod = new clsModuli(gjuha).merrModulByModulId(idModuli);
            return clsMod.KodiModuli;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushModul(int idgjuha, DataRow dbDataRowModuli)
        {
            if (dbDataRowModuli != null)
            {
                string kodGjuhe = MessagesResource.KtheKodGjuhe(idgjuha);
                try
                {
                    pershkrimiModuli = dbDataRowModuli["MODULIPERSHK_" + kodGjuhe].ToString();
                    idModuli = int.Parse(dbDataRowModuli["IDMODULI"].ToString());
                    kodiModuli = dbDataRowModuli["MODULIKODI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se modulit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
