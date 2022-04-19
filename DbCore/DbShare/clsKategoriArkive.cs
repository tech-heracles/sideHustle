using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web.Script.Serialization;

namespace DbCore.DbShare
{
    public class clsKategoriArkive
    {
        #region Atribute

        private int idKategoriArkive;
        private int idNivel;
        private string kategoria;
        private int idNdermarrje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idKrijues;
        private int idModifikues;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKategoriArkive
        {
            get { return idKategoriArkive; }
            set { idKategoriArkive = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit.
        /// </summary>
        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit.
        /// </summary>
        public string Kategoria
        {
            get { return kategoria; }
            set { kategoria = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// Kthen/Vendos statusin: 1 e ruajtur, 2 e fshire.
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e krijuesit.
        /// </summary>
        public int IdKrijues
        {
            get { return idKrijues; }
            set { idKrijues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e modifikuesit.
        /// </summary>
        public int IdModifikues
        {
            get { return idModifikues; }
            set { idModifikues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e krijuesit.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e modifikuesit.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori pa parametra
        /// </summary>
        public clsKategoriArkive()
        {
        }

        public clsKategoriArkive(int idKategoriArkive, int idNivel, string kategoria, int idNdermarrje, int idStatusDok, DateTime dtKrijimi, DateTime dtModifikimi, int idKrijues, int idModifikues)
        {
            this.idKategoriArkive = idKategoriArkive;
            this.idNivel = idNivel;
            this.kategoria = kategoria;
            this.idNdermarrje = idNdermarrje;
            this.idStatusDok = idStatusDok;
            this.dtKrijimi = dtKrijimi;
            this.dtModifikimi = dtModifikimi;
            this.idKrijues = idKrijues;
            this.idModifikues = idModifikues;
        }

        public clsKategoriArkive(int idKategoriArkive)
        {
            clsDatabaseShare dbShare = new clsDatabaseShare();
            mbushKategoriArkive(dbShare.ktheKategoriArkiveSipasIdKategorie(idKategoriArkive));
            dbShare.Dispose();
        }

        public clsKategoriArkive(DataRow rreshti)
        {
            mbushKategoriArkive(rreshti);
        }

        #endregion

        #region Metoda Publike

        public static clsMesazh ruaj(int idNivel, string kategoria, int idNdermarrje, int idKrijues)
        {
            clsDatabaseShare db = new clsDatabaseShare();
            db.beginTransaksion();

            int id = 0;
            clsMesazh mesazh = db.ruajKategoriArkive(out id, idNivel, kategoria, idNdermarrje, idKrijues);
            //this.idKategoriArkive = id;

            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            db.commitTransaksion();
            return mesazh;
        }

        public static clsMesazh modifiko(int idKategoriArkive, string kategoria, int idModifikues)
        {
            clsDatabaseShare db = new clsDatabaseShare();
            db.beginTransaksion();            
            clsMesazh mesazh = db.modifikoKategoriArkive(idKategoriArkive, kategoria, idModifikues);          
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            db.commitTransaksion();
            return mesazh;
        }

        public static clsMesazh fshi(int idKategoriArkive, int idModifikues)
        {
            clsDatabaseShare db = new clsDatabaseShare();
            db.beginTransaksion();
            clsMesazh mesazh = db.fshiKategoriArkive(idKategoriArkive, idModifikues);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            db.commitTransaksion();
            return mesazh;
        }

        public static DataRow ktheKategoriArkiveSipasId(int idKategoriArkive)
        {
            using (clsDatabaseShare dbShare = new clsDatabaseShare())
            {
                return dbShare.ktheKategoriArkiveSipasIdKategorie(idKategoriArkive);
            }
        }

        public static bool ekzistonKategoriArkiveSipasNivelDheNdermarrje(int idNdermarrje, int idNivel, string kategoria)
        {
            using (clsDatabaseShare dbShare = new clsDatabaseShare())
            {
                return dbShare.ekzistonKategoriArkiveSipasNivelDheNdermarrje(idNdermarrje, idNivel, kategoria);
            }
        }

        public static bool ekzistonKategoriArkiveSipasNivelDheNdermarrjePervecVetes(int idNdermarrje, int idNivel, string kategoria, int idKategoriArkive)
        {
            using (clsDatabaseShare dbShare = new clsDatabaseShare())
            {
                return dbShare.ekzistonKategoriArkiveSipasNivelDheNdermarrjePervecVetes(idNdermarrje, idNivel, kategoria, idKategoriArkive);
            }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush arkivat nga databaza
        /// </summary>
        /// <param name="dbDataRowSkemaAzhornimi">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKategoriArkive(DataRow dbDataRowArkiva)
        {
            if (dbDataRowArkiva != null)
            {
                try
                {
                    int.TryParse(dbDataRowArkiva["IDKATEGORIARKIVE"].ToString(), out idKategoriArkive);
                    int.TryParse(dbDataRowArkiva["IDNIVEL"].ToString(), out idNivel);
                    kategoria = dbDataRowArkiva["KATEGORIA"].ToString();
                    int.TryParse(dbDataRowArkiva["IDNDERMARRJE"].ToString(), out idNdermarrje);                
                    int.TryParse(dbDataRowArkiva["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowArkiva["IDKRIJUES"].ToString(), out idKrijues);
                    int.TryParse(dbDataRowArkiva["IDMODIFIKUES"].ToString(), out idModifikues);
                    DateTime.TryParse(dbDataRowArkiva["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowArkiva["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se arkivave nga db-ja");
                }
            }
            else
                return false;
        }

        public static bool kaVeprimeMeKategoriArkive(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}