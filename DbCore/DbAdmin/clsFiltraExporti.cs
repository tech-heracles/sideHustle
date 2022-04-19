using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne filtrin
    ///  qe i aplikohet nje gride(Te dhenat  merren nga tabela : T_KOKAFILTRAEXPORTI)
    /// </summary>
    public class clsFiltraExporti
    {
        #region Atributet

        private int id;
        private String kodi;
        private int formati;
        private string pershkrimi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private String  filterPerDataSet;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsFiltraExporti(int idfiltri, string filtrakodi, int formati, int idperdoruesi, int idnderm, int idstatusdok, string pershkrimi, string filterPerDataSet )
        {
            id = idfiltri;
            kodi = filtrakodi;
            this.formati = formati;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idnderm;
            idStatusDok = idstatusdok;
            this.pershkrimi=pershkrimi;
            this.filterPerDataSet = filterPerDataSet;
        }

      

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idFiltra">id e filtrit</param>
        public clsFiltraExporti(int idFiltra)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushFilterGrid(data.ktheFiltraExportiSipasId(idFiltra));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsFiltraExporti()
        {
        }

        public clsFiltraExporti(DataRow rreshti)
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
        /// Kthen/Vendos ID-ne e formatit te importit.
        /// </summary>
        public int Formati
        {
            get
            {
                return formati;
            }

            set
            {
                formati = value;
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
                get { return filterPerDataSet; }
                set { filterPerDataSet = value; }
            }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e filtrit ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajFiltraExporti(this.Id, this.Kodi, this.Formati, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok, this.Pershkrimi, this.filterPerDataSet);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e filtrit ne databaze.
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoFiltraExporti(this.Id, this.Kodi, this.Formati, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok, this.Pershkrimi, this.filterPerDataSet);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e filtrit nga databaza.
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiFiltraGridaStatus(this.Id, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Kthen nje collection me objekte te tipti <c>clsFiltraGrida</c> nga databaza
        /// sipas grides dhe ndermarrjes
        /// </summary>
        public colFiltraExporti merriTeGjitheSipasFormatit(int formati, int idnderm)
        {
            colFiltraExporti data = new colFiltraExporti(formati, idnderm);
            return data;

        }

        public clsFiltraGrida merrFilterSipasId()
        {
            clsFiltraGrida data = new clsFiltraGrida(this.Id);
            return data;

        }


        public bool mbushFiltraSipasFiltraKodi(string filtrakodi, int idnderm, int formati)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushFilterGrid(data.ktheFiltraExportiSipasFiltraKodi(filtrakodi, idnderm, formati));
            data.Dispose();
            return sukses;
        }

        public static bool ekzistonFilterSipasKodit(string kodi, int idNderm, int formati)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekziston = db.ekzistonFilterExporti(kodi, idNderm, formati);
            db.Dispose();
            return ekziston;
        } 
        public static bool kaVeprime(int id)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekziston = db.kaVeprimeFilterExporti(id);
            db.Dispose();
            return ekziston;
        }

        public static string ktheFilterPerDataSet(int id)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            string ktheFilterPerDataSet = db.ktheFilterPerDataSetSipasId(id);
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
                    int.TryParse(dbDataRowFilterGrid["FORMATI"].ToString(), out formati);
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
