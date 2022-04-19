using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje skeme kontabiliteti
    ///  (Te dhenat  merren nga tabela : T_SKEMEKONTABILITETI)
    ///</remarks>
    public class clsSkemaKontabelNew
    {
        #region Atributet
        
        private string idSkemeKont;
        private string kodSkemeKont;
        private string pershkrimSkemeKont;
        private string idLlojDok;
        private colSkemaKontabelTrupiNew oColSkemakontabelTrupiNew;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaKontabelNew(string idskemekont,  String kodskemekont,string pershkrimskemekont,string idllojdok)
        {
            idSkemeKont=idskemekont;
            kodSkemeKont=kodskemekont;
            pershkrimSkemeKont = pershkrimskemekont;
            idLlojDok = idllojdok;
        }

        public clsSkemaKontabelNew(int id)
        {
            using (clsDatabaseKontabilitet dbSkemaKontNew = new clsDatabaseKontabilitet())
            {
                mbushSkemaKontNew(dbSkemaKontNew.ktheSkemaKontabelNewSipasID(id));
            }
        }

        public static clsSkemaKontabelNew getSkemaKontabelNew(int id, clsDatabaseKontabilitet dbSkemaKontNew)
        {
            return dbSkemaKontNew.TransCache.getSkemaKontabelNew(id, dbSkemaKontNew);
        }
        public clsSkemaKontabelNew(int id, clsDatabaseKontabilitet dbSkemaKontNew)
        {
          
            mbushSkemaKontNew(dbSkemaKontNew.ktheSkemaKontabelNewSipasID(id));
        }
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaKontabelNew()
        { 
        }

        public clsSkemaKontabelNew(DataRow rreshti)
        {
            
            mbushSkemaKontNew(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public string IdSkemeKont
        {
            get { return idSkemeKont; }
            set { idSkemeKont = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e skemes kontabel
        /// </summary>
        public String KodSkemeKont
        {
            get { return kodSkemeKont; }
            set { kodSkemeKont = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e skemes kontabel
        /// </summary>
        public String PershkrimSkemeKont
        {
            get { return pershkrimSkemeKont; }
            set { pershkrimSkemeKont = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llojit te dokumentit qe perdor kete skeme kontabel
        /// </summary>
        public String IdLlojDok
        {
            get { return idLlojDok; }
            set { idLlojDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsSkemakontabelTrupiNew"/>
        /// </summary>
        public colSkemaKontabelTrupiNew OColSkemakontabelTrupiNew
        {
            get { return oColSkemakontabelTrupiNew; }
            set { oColSkemakontabelTrupiNew = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e skemes se re kontabel sipas kodit
        /// </summary>
        /// <param name="kodi">kodi i skemes se re kontabel</param>
        /// <returns>id e skemes se re kontabel</returns>
        public static int mbushIDSkemaKontabelNewSipasKod(string kodi)
        {
            clsDatabaseKontabilitet dbSkemaKontNew = new clsDatabaseKontabilitet();
            int id = (dbSkemaKontNew.ktheIDSkemaKontabelNewSipasKodi(kodi));
            dbSkemaKontNew.Dispose();
            return id;
        }

        /// <summary>
        /// Metoda kthen nje klase me objekt te tipit <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelNew"/> 
        /// duke filtruar sipas ID-se se skemes kontabel
        /// </summary>
        public clsSkemaKontabelNew mbushSkemeKontabelNewSipasID(int id)
        {
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrSkemaKontabelNewSipasID(id);
            clsSkemaKontabelNew data = new clsSkemaKontabelNew(id);
            return data;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush skemat e reja kontabel nga databaza
        /// </summary>
        /// <param name="dbDataRowSkemaKontNew">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushSkemaKontNew(DataRow dbDataRowSkemaKontNew)
        {
            if (dbDataRowSkemaKontNew != null)
            {
                try
                {
                    idSkemeKont = dbDataRowSkemaKontNew["IDSKEMKONT"].ToString();
                    kodSkemeKont = dbDataRowSkemaKontNew["KODSKEMEKONT"].ToString();
                    pershkrimSkemeKont = dbDataRowSkemaKontNew["PERSHKRIMSKEMEKONT"].ToString();
                    idLlojDok = dbDataRowSkemaKontNew["IDLLOJDOK"].ToString();
                    oColSkemakontabelTrupiNew = new colSkemaKontabelTrupiNew();
                    oColSkemakontabelTrupiNew = oColSkemakontabelTrupiNew.merrSkemaTrupiNew(idSkemeKont);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se skemave kontabel te reja nga db-ja");
                }
            }
            else
                return false;
        }

        internal clsSkemaKontabelNew ShallowCopy()
        {
            return (clsSkemaKontabelNew)this.MemberwiseClone();
        }

        #endregion
    }
}
