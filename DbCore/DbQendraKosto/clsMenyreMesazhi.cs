using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne menyreMesazhi
    ///  (Te dhenat  merren nga tabela : T_MENYREMESAZHI)
    /// </summary>
    public class clsMenyreMesazhi
    {
               #region Atributet

        private int idMenyreMesazhi;
        private string menyra;
        private string pershkrimi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdMenyreMesazhi
        {
            get
            {
                return idMenyreMesazhi;
            }
            set
            {
                idMenyreMesazhi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos menyra
        /// </summary>
        public string Menyra
        {
            get
            {
                return menyra;
            }
            set
            {
                menyra = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin 
        /// </summary>
        public string Pershkrimi
        {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }
        }
      
        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idmenyremesazhi">idmenyremesazhi</param>
        /// <param name="menyra">menyra</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="shpjegimi">shpjegimi</param>
        public clsMenyreMesazhi(int idmenyremesazhi, string menyra, string pershkrimi, string shpjegimi)
        {
            this.idMenyreMesazhi = idmenyremesazhi;
            this.menyra = menyra;
            this.pershkrimi = pershkrimi;
        }
        /// <summary>
        /// kontruktori pa parametra
        /// </summary>
        public clsMenyreMesazhi()
        {
        }

        public clsMenyreMesazhi(DataRow rreshti)
        {
            
            mbushMenyreMesazhi(rreshti);
        }

        #endregion

        #region Metoda Internal


        /// <summary>
        ///   mbush menyre meszhi nga databaza
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        internal bool mbushMenyreMesazhi(DataRow db)
        {
            if (db != null)
            {

                try
                {

                    int.TryParse(db["IDMENYREMESAZHI"].ToString(), out idMenyreMesazhi);
                    menyra = db["MENYRA"].ToString();
                    pershkrimi = db["PERSHKRIMI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se menyre mesazhi nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

        #region metoda publike
        public bool ktheMenyreSipasId(int id, int idGjuha)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            bool sukses = mbushMenyreMesazhi(db.ktheMenyreMesazhiSipasId(id,idGjuha));
            db.Dispose();
            return sukses;
        }
        public bool ktheMenyreSipasKodit(string kodi)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            bool sukses = mbushMenyreMesazhi(db.ktheMenyreMesazhiSipasKodit(kodi));
            db.Dispose();
            return sukses;
        }
        #endregion
    }
}
