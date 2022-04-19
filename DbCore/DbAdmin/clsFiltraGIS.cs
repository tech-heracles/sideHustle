using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne filtrin
    ///  qe i aplikohet nje gride(Te dhenat  merren nga tabela : T_FILTRAGIS)
    /// </summary>
    public class clsFiltraGIS
    {
        #region Atributet

        private int id;
        private String kodi;
        private string idLayerType;
        private string pershkrimi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private String  filterExpression;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsFiltraGIS(int idfiltri, string filtrakodi, string idLayerType, int idperdoruesi, int idnderm, int idstatusdok, string pershkrimi, string filterPerDataSet )
        {
            id = idfiltri;
            kodi = filtrakodi;
           this.idLayerType = idLayerType;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idnderm;
            idStatusDok = idstatusdok;
            this.pershkrimi=pershkrimi;
            filterExpression = filterPerDataSet;
        }

      

       

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsFiltraGIS()
        {
        }

        public clsFiltraGIS(DataRow rreshti)
        {
            
            mbushFilterGrid(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e filtrit.
        /// </summary>
        public String Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos filtrin .
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

      
        /// <summary>
        /// Kthen/Vendos ID-ne e tipi te layerit.
        /// </summary>
        public string IdLayerType
        {
            get
            {
                return idLayerType;
            }

            set
            {
                idLayerType = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit i cili e ka krijuar kete filter.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes se ciles i perket ky filter.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        } 
        public int IdStatusDok
            {
            get { return idStatusDok; }
            set { idStatusDok = value; }
            }
         public DateTime DtKrijimi
            {
            get { return dtKrijimi; }
            
            }
         public DateTime DtModifikimi
            {
            get { return dtModifikimi; }
           
            }

         public String FilterPerDataSet
            {
                get { return filterExpression; }
                set { filterExpression = value; }
            }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e filtrit ne databaze.
        /// </summary>
         public clsMesazh ruaj()
         {
             clsDatabaseAdmin data = new clsDatabaseAdmin();
             clsMesazh u_ruajt = data.ruajFiltraGIS(Id, Kodi, IdLayerType, IdPerdoruesi, IdNdermarje, idStatusDok, Pershkrimi, filterExpression);
             data.Dispose();
             return u_ruajt;
         }

        /// <summary>
        /// Modifikon objektin e filtrit ne databaze.
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoFiltraGIS(Id, Kodi, IdLayerType, IdPerdoruesi, IdNdermarje, idStatusDok, Pershkrimi, filterExpression);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e filtrit nga databaza.
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiFilterGisStatus(Id, idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Kthen nje collection me objekte te tipti <c>clsFiltraGrida</c> nga databaza
        /// sipas grides dhe ndermarrjes
        /// </summary>
        public colFiltraExporti merriTeGjitheSipasidLayerType(int idLayerType, int idnderm)
        {
            colFiltraExporti data = new colFiltraExporti(idLayerType, idnderm);
            return data;

        }

        public clsFiltraGrida merrFilterSipasId()
        {
            clsFiltraGrida data = new clsFiltraGrida(Id);
            return data;

        }


        public bool mbushFiltraSipasFiltraKodi(string filtrakodi, int idnderm, int idLayerType)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushFilterGrid(data.ktheFiltraGISSipasFiltraKodi(filtrakodi, idnderm, idLayerType));
            data.Dispose();
            return sukses;
        }

        public static bool ekzistonFilterSipasKodit(string kodi, int idNderm, string idLayerType)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekziston = db.ekzistonFilterGIS(kodi, idNderm, idLayerType);
            db.Dispose();
            return ekziston;
        } 
        //public static bool kaVeprime(int id)
        //{
        //    clsDatabaseAdmin db = new clsDatabaseAdmin();
        //    bool ekziston = db.kaVeprimeFilterExporti(id);
        //    db.Dispose();
        //    return ekziston;
        //}

        public static string ktheFilterPerDataSet(int id)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            string ktheFilterPerDataSet = db.ktheFilterPerGISSipasId(id);
            db.Dispose();
            return ktheFilterPerDataSet;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushFilterGrid(DataRow dbDataRowFilterGrid)
        {
            if (dbDataRowFilterGrid != null)
            {
                try
                {
                    int.TryParse(dbDataRowFilterGrid["ID"].ToString(), out id);
                    kodi = dbDataRowFilterGrid["KODI"].ToString();
                    pershkrimi = dbDataRowFilterGrid["PERSHKRIMI"].ToString();
                    idLayerType=dbDataRowFilterGrid["IDLAYERTYPE"].ToString();
                    int.TryParse(dbDataRowFilterGrid["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowFilterGrid["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowFilterGrid["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowFilterGrid["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowFilterGrid["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                  return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se filtrit te grides nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
