using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// Nuk ka dokumentim
    /// </summary>
    public class clsLlojPeriudhe
    {
        enum Periudha
        {
            Ditore = 0,
            Mujore = 1,
            Vjetore = 2,
            PaLimit = 3
        }

        #region Atribute 

        private int idLlojPeriudhe;
        private int llojPeriudheLloji;
        private int llojPeriudheSasia;
        private string llojPeriudhePershkrimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        public clsLlojPeriudhe(int idllojperiudhe, int llojperiudhelloji, int llojperiudhesasia, string llojperiudhepershkrimi)
        {
            idLlojPeriudhe = idllojperiudhe;
            llojPeriudheLloji = llojperiudhelloji;
            llojPeriudheSasia = llojperiudhesasia;
            llojPeriudhePershkrimi = llojperiudhepershkrimi;
        }

        public clsLlojPeriudhe()
        { 
        }

        public clsLlojPeriudhe(DataRow rreshti)
        {
            
            mbushLlojPeriudhe(rreshti);
        }

        #endregion

        #region Properties

        public int IdLlojPeriudhe
        {
            get { return idLlojPeriudhe; }
            set { idLlojPeriudhe = value; }
        }
        public int LlojPeriudheLloji
        {
            get { return llojPeriudheLloji; }
            set { llojPeriudheLloji = value; }
        }
        public int LlojPeriudheSasia
        {
            get { return llojPeriudheSasia; }
            set { llojPeriudheSasia = value; }
        }
        public string LlojPeriudhePershkrimi
        {
            get { return llojPeriudhePershkrimi; }
            set { llojPeriudhePershkrimi = value; }
        }

        #endregion

        public static clsLlojPeriudhe merrPeriudhenSipasId(int id)
        {
            clsLlojPeriudhe periudha = new clsLlojPeriudhe();
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            periudha.mbushLlojPeriudhe(data.ktheLlojPeriudheSipasId(id));
            data.Dispose();
            return periudha;
        }

        #region Metoda Internal

        internal bool mbushLlojPeriudhe(DataRow dbDataRowLlojPeriudhe)
        {
            if (dbDataRowLlojPeriudhe != null)
            {
                try
                {
                    idLlojPeriudhe = int.Parse(dbDataRowLlojPeriudhe["IDLLOJPERIUDHE"].ToString());
                    llojPeriudheLloji = int.Parse(dbDataRowLlojPeriudhe["LLOJPERIUDHELLOJI"].ToString());
                    llojPeriudheSasia = int.Parse(dbDataRowLlojPeriudhe["LLOJPERIUDHESASIA"].ToString());
                    if (idLlojPeriudhe == -3)
                    {
                        llojPeriudhePershkrimi = "(...)";
                    }
                    else
                        if (idLlojPeriudhe == 4)
                        {
                            llojPeriudhePershkrimi = "Pa Limit";
                        }
                    
                            else
                            {
                                llojPeriudhePershkrimi = Enum.GetName(typeof(Periudha), int.Parse(dbDataRowLlojPeriudhe["LLOJPERIUDHELLOJI"].ToString()));
                            }
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llojit te periudhes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
