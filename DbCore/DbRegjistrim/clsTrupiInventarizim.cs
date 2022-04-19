using DbCore.DbRegjistrim;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti inventarizim
    ///  (Te dhenat  merren nga tabela : T_TRUPIINVENTARIZIM)
    /// </summary>
    public class clsTrupiInventarizim
    {
        #region Attributet

        private int idTrupi;
        private int idKoka;
        private string barkod;
        private string serial;
        private decimal sasi;
        private DataRow rreshti;
        private string shenime;
        private string kodi;
        private string pershkrimi;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="cm">cmimi i artikullit</param>
        /// <param name="dt"> data e dokumentit</param>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idKoka">id e kokes se dokumentit te magazines</param>
        /// <param name="idTrup">id ritese e trupit te dokumentit te magazines</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="KodArt">kodi i artikullit</param>
        /// <param name="cmimri">koeficienti midis dy njesive te artikullit</param>
        /// <param name="pershkArt">pershkrimi i artikullit</param>
        /// <param name="sas"> sasia e artikullit</param>
        /// <param name="sasire"> sasia progresive e artikullit</param>
        /// <param name="vl"> vlefta e artikullit</param>
        /// <param name="vlre"> vlefta progresive e artikullit</param>

        public clsTrupiInventarizim(string barkod, string serial, decimal sas, string shenime)
        {
            this.barkod = barkod;
            this.serial = serial;
            this.sasi = sas;
            this.shenime = shenime;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        public clsTrupiInventarizim(int idTrupi)
        {
            clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim();
            mbushTrupInventarizim(dbtrupMagazine.ktheTrupiInventarizimSipasID(idTrupi));
            dbtrupMagazine.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        /// <param name="dbtrupMagazine"></param>
        public clsTrupiInventarizim(int idTrupi, clsDatabaseRegjistrim dbtrupMagazine)
        {
            mbushTrupInventarizim(dbtrupMagazine.ktheTrupiInventarizimSipasID(idTrupi));
        }

        public bool krijoTrupInventarizmNgaGrida(string barkod, string serial, decimal sasia, string shenime)
        {
            this.barkod = barkod;
            this.serial = serial;
            this.sasi = sasia;
            this.shenime = shenime;
            return true;
        }


        /// <summary>
        /// Sherben per te krijuar trupMagazinen nga grida client
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="eshteTransferim"></param>
        /// <param name="rreshtDokuKlient"></param>
        public clsTrupiInventarizim(Dictionary<string, object> rreshtDokuKlient)
        {

            string barkodi = rreshtDokuKlient["txtKodbari"].ToString();
            string serial = rreshtDokuKlient["txtSerial"].ToString();
            string sasia = rreshtDokuKlient["txtSasia"].ToString();
            string shenime = rreshtDokuKlient["txtShenime"].ToString();

            if (barkodi != null && barkodi != "null")
                this.barkod = barkodi;

            if (serial != null && serial != "null")
                this.serial = serial;


            if (sasia != null && sasia != "null" && sasia != "")
                this.sasi = decimal.Parse(sasia);

            if (shenime != null && shenime != "null")
                this.shenime = shenime;
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiInventarizim()
        {
        }

        public clsTrupiInventarizim(DataRow rreshti)
        {
            mbushTrupInventarizim(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te ndryshim cmim sasi.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        
        /// <summary>
        /// Kthen/Vendos Barkodin.
        /// </summary>
        public String Barkod
        {
            get { return barkod; }
            set { barkod = value; }
        }

        /// <summary>
        /// Kthen/Vendos serialin.
        /// </summary>
        public String Serial
        {
            get { return serial; }
            set { serial = value; }
        }
        

        /// <summary>
        /// Kthen/Vendos sasia e artikullit.
        /// </summary>
        public decimal Sasi
        {
            get { return sasi; }
            set { sasi = value; }
        }

        public String Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        public String Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        #endregion

        #region Metoda Publike

        public static clsTrupiInventarizim krijoTrupInventarizimiNgaImporti(string kodi, string seriali, decimal sasia)
        {
            if (kodi == "" && seriali == "")
                throw new DbCore.MyException("Plotesoni barkodin e artikullit!");

            if (sasia <= 0)
                throw new DbCore.MyException("Sasia nuk duhet te jete me e vogel ose e barabarte me 0!");

            return new clsTrupiInventarizim(kodi, seriali, sasia, "");
        }

        /// <summary>
        /// Ruan objektin e  trupit te dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajTrupiMagazina"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            int id;
            clsMesazh u_ruajt = data.ruajTrupiInventarizim(out id, this.idKoka, this.barkod, this.sasi, this.serial, this.shenime);
            this.idTrupi = id;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// modifikon objektin e  trupit te dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoTrupiMagazina"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoInventarizim(this.idTrupi, this.idKoka, this.barkod, this.sasi, this.serial, this.shenime);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// fshin objektin e  trupit te dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiTrupiMagazinaSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiTrupiInventarizimSipasID(this.idTrupi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// fshin objektet e  trupit te dokumentit te magazines sipas id se kokes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiTrupiMagazinaSipasKoka"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiSipasKoka()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiTrupiInventarizimSipasKoka(this.idKoka);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektet e  trupit te dokumentit te magazines sipas kokes nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheTrupiMagazinaNgaKoka"/> 
        /// </summary>
        /// <returns > nje objekt  colTrupiMagazina me te gjithe trupat e nje dokumenti</returns>
        public colTrupiInventarizim merriSipasKoka()
        {
            colTrupiInventarizim data = new colTrupiInventarizim();
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            data.mbushGjitheTrupiInventarizimNgaKoka(this.idKoka);
            return data;
        }
        

        /// <summary>
        /// Merr objektin e  trupit te dokumentit te magazines sipas id nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheTrupiMagazinaSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsTrupiInventarizim me trupin e dokumentit te magazines te kerkuar</returns>
        public clsTrupiInventarizim merriSipasID()
        {
            clsTrupiInventarizim data = new clsTrupiInventarizim(this.idTrupi);
            return data;

        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e magazines nga databaza
        /// </summary>
        /// <param name="dbDataRowTrup">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupInventarizim(DataRow dbDataRowTrup)
        {
            if (dbDataRowTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrup["idTrupi"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowTrup["idKoka"].ToString(), out idKoka);
                    barkod = dbDataRowTrup["BARKOD"].ToString();
                    serial = dbDataRowTrup["SERIAL"].ToString();
                    decimal.TryParse(dbDataRowTrup["SASI"].ToString(), out sasi);
                    shenime = dbDataRowTrup["SHENIME"].ToString();
                    kodi = dbDataRowTrup["KODI"].ToString();
                    pershkrimi = dbDataRowTrup["PERSHKRIMI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te ndryshim cmim sasi nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}