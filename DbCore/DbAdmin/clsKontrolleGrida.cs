using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class clsKontrolleGrida
    {
         #region Attributet
        private int id;
        private int idKatDok;
        private String kodi;
     
        private int tipi;
        /// <summary>
        /// emri i cili do shfaqet ne ambjentin e importit
        /// </summary>
        private string emerImporti;
        /// <summary>
        /// tregon nese ky kontroll do shfaqet ne ambjentin e importit apo jo 
        /// </summary>
        private bool shfaqImporti;
        private DataRow rreshti;
        #endregion

        #region Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdKatDok
        {
            get { return idKatDok; }
            set { idKatDok = value; }
        }

        public String Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

    
        /// <summary>
        /// get set Tipin e kontrollit
        /// </summary>
        public int Tipi
        {
            get
            {
                return tipi;
            }
            set
            {
                if (tipi == value)
                    return;
                tipi = value;
            }
        }
        /// <summary>
        /// emri i cili do shfaqet ne ambjentin e importit
        /// </summary>
        public string EmerImporti
        {
            get
            {
                return emerImporti;
            }
            set
            {
                emerImporti = value;
            }
        }
        /// <summary>
        /// tregon nese ky kontroll do shfaqet ne ambjentin e importit apo jo 
        /// </summary>
        public bool ShfaqImporti
        {
            get
            {
                return shfaqImporti;
            }
            set
            {
                shfaqImporti = value;
            }
        }
        #endregion

        #region konstruktoret
        /// <summary>
        /// Nderton objektin nga db-ja me id-ne qe merr ne input
        /// </summary>
        /// <param name="idKontrolli">id-ja e kontrollit</param>
        public clsKontrolleGrida(int idKontrolli)
        {
            clsDatabaseAdmin sharedb = new clsDatabaseAdmin();
            mbushKontrollin(sharedb.merrKontrollin(idKontrolli));
            sharedb.Dispose();
        }

        /// <summary>
        /// konstruktori i plote i klases
        /// </summary>
        /// <param name="idkontrolli"></param>
        /// <param name="idkomponente"></param>
        /// <param name="kodkontrolli"></param>
        /// <param name="pershkrimikontrolli"></param>
        /// <param name="idTipiKontrollit"></param>
        /// <param name="emerimporti">emri i cili do shfaqet ne ambjentin e importit</param>
        /// <param name="shfaqimporti">tregon nese do shfaqet tek ambjenti i importit apo jo </param>
        public clsKontrolleGrida(int idkontrolli, int idkatdok, String kodi, int tipi, string emerimporti, bool shfaqimporti)
        {
            id= idkontrolli;
            idKatDok = idkatdok;
            this.kodi = kodi;
            this.tipi = tipi;
            emerImporti = emerimporti;
            shfaqImporti = shfaqimporti;
        }

        public clsKontrolleGrida()
        { 
        }

        public clsKontrolleGrida(DataRow rreshti)
        {
            
            mbushKontrollin(rreshti);
        }
        #endregion

        #region Metoda Publike

        public  clsKontrolleGrida merrKontrollSipasKoditKomponentes(string kodKontroll, int idkatdok)
        {
            using (clsDatabaseAdmin sharedb = new clsDatabaseAdmin())
            {
                mbushKontrollin(sharedb.merrKontrollinSipasKodit(kodKontroll, idkatdok));
                return this;
            }
    
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbDataRowKontrolli">DataRow me te dhenat e kontrollit</param>
        /// <returns></returns>
        internal bool mbushKontrollin(DataRow dbDataRowKontrolli)
        {
            if (dbDataRowKontrolli != null)
            {
                try
                {
                    int.TryParse(dbDataRowKontrolli["ID"].ToString(),out id);
                    int.TryParse(dbDataRowKontrolli["IDKATDOK"].ToString(), out idKatDok);
                    Kodi = dbDataRowKontrolli["KODI"].ToString();                  
                    int.TryParse(dbDataRowKontrolli["TIPI"].ToString(), out tipi); 
                    EmerImporti = dbDataRowKontrolli["EMERIMPORTI"].ToString();    
                    bool.TryParse(dbDataRowKontrolli["SHFAQIMPORTI"].ToString(), out shfaqImporti); 
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se tipit te kontrollit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
