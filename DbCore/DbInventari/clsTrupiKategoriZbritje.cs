using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne trupin e kategorise se zbritjes
    ///  (Te dhenat  merren nga tabela : T_TRUPIKATEGORIZBRITJE)
    /// </summary>
    public class clsTrupiKategoriZbritje
    {  
        #region Atribute

        private int idTrupiKategoriZbritje;
        private int idKokaKategoriZbritje;
        private DateTime dateFillimi;
        private DateTime dateMbarimi;
        private decimal vleraMin;
        private decimal vleraMax;
        private int lloji;
        private decimal zbritja;
        private int idPerdoruesi;
        //private int idNderViti;
        private int prioriteti;
        private DataRow rreshti;
  
        #endregion 

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupiKategoriZbritje
        {
            get { return idTrupiKategoriZbritje   ; }
            set { idTrupiKategoriZbritje   = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se kategorise se zbritjes.
        /// </summary>
        public int IdKokaKategoriZbritje
        {
            get { return idKokaKategoriZbritje; }
            set { idKokaKategoriZbritje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
            public int IdPerdoruesi
        {
            get { return idPerdoruesi ; }
            set { idPerdoruesi  = value; }
        }
            /// <summary>
            /// Kthen/Vendos ID-ne e ndermarje vitit.
            /// </summary>
        //public int IdNderViti
        //{
        //    get { return idNderViti; }
        //    set { idNderViti = value; }
        //}
        /// <summary>
        /// Kthen/Vendos  prioriteti.
        /// </summary>
        public int Prioriteti
        {
            get { return prioriteti ; }
            set { prioriteti = value; }
        }
        /// <summary>
        /// Kthen/Vendos daten e fillimit.
        /// </summary>
        public DateTime DateFillimi
        {
            get
            { return dateFillimi; }
            set
            {
                dateFillimi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos daten e mbarimit.
        /// </summary>
        public DateTime DateMbarimi
        {
            get
            { return dateMbarimi; }
            set
            {
                dateMbarimi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos vleren minimale.
        /// </summary>
        public decimal VleraMin
        {
            get
            {
                return vleraMin;
            }
            set
            {
                vleraMin = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos vleren maksimale.
        /// </summary>
        public decimal VleraMax
        {
            get
            {
                return vleraMax;
            }
            set
            {
                vleraMax = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos llojin.
        /// </summary>
        public int Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }
        /// <summary>
        /// Kthen/Vendos zbritjen.
        /// </summary>
        public decimal Zbritja
        {
            get
            {
                return zbritja;
            }
            set
            {
                zbritja = value;
            }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idTrupiKategoriZbritje"> id ritese e trupit te kategori zbritje</param>
        /// <param name="idKokaKategoriZbritje"> id e kokes</param>
        /// <param name="dateFillimi"> data e fillimit</param>
        /// <param name="dateMbarimi"> data e mbarimit</param>
        /// <param name="vleraMin"> vlera minimale</param>
        /// <param name="vleraMax"> vlera maksimale</param>
        /// <param name="lloji"> lloji</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="prioriteti"> prioriteti</param>
        public clsTrupiKategoriZbritje(int idTrupiKategoriZbritje, int idKokaKategoriZbritje, DateTime dateFillimi, DateTime dateMbarimi, decimal vleraMin, decimal vleraMax, int lloji, decimal zbritja, int idPerdoruesi, int prioriteti)
        {
            this.idTrupiKategoriZbritje = idTrupiKategoriZbritje;

            this.idKokaKategoriZbritje = idKokaKategoriZbritje;
            this.dateFillimi = dateFillimi;
            this.dateMbarimi = dateMbarimi;
            this.vleraMin = vleraMin;
            this.vleraMax = vleraMax;
            this.lloji = lloji;
            this.zbritja = zbritja;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.prioriteti = prioriteti;
        }
        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idKokaKategoriZbritje"> id e kokes</param>
        /// <param name="dateFillimi"> data e fillimit</param>
        /// <param name="dateMbarimi"> data e mbarimit</param>
        /// <param name="vleraMin"> vlera minimale</param>
        /// <param name="vleraMax"> vlera maksimale</param>
        /// <param name="lloji"> lloji</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="prioriteti"> prioriteti</param>
        public clsTrupiKategoriZbritje(  int idKokaKategoriZbritje, DateTime dateFillimi, DateTime dateMbarimi, decimal vleraMin, decimal vleraMax, int lloji, decimal zbritja, int idPerdoruesi, int prioriteti)
        {

            this.idKokaKategoriZbritje = idKokaKategoriZbritje;
            
            this.dateFillimi = dateFillimi;
            this.dateMbarimi = dateMbarimi;
            this.vleraMin = vleraMin;
            this.vleraMax = vleraMax;
            this.lloji = lloji;
            this.zbritja = zbritja;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.prioriteti = prioriteti;
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idkatzbritje">id e trupit te katerise se zbritjes</param>
        public clsTrupiKategoriZbritje(int idkatzbritje)
        {
            clsDatabaseInventari dbTrupiKategoriZbritje = new clsDatabaseInventari();
            mbushTrupiKategoriZbritje(dbTrupiKategoriZbritje.merrTrupiKategoriZbritje(idkatzbritje));
            dbTrupiKategoriZbritje.Dispose();
        }
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsTrupiKategoriZbritje()
        { 
        }

        public clsTrupiKategoriZbritje(DataRow rreshti)
        {
            
            mbushTrupiKategoriZbritje(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbushja e trupit te kategotise se zbritjes nga databaza
        /// </summary>
        /// <param name="dbDataRowTrupiKategoriZbritje">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushTrupiKategoriZbritje(DataRow dbDataRowTrupiKategoriZbritje)
        {
            if (dbDataRowTrupiKategoriZbritje != null)
            {

                try
                {
                    int.TryParse(dbDataRowTrupiKategoriZbritje["IDTRUPIKATEGORIZBRITJE"].ToString(), out idTrupiKategoriZbritje);
                    int.TryParse(dbDataRowTrupiKategoriZbritje["IDKOKAKATEGORIZBRITJE"].ToString(), out idKokaKategoriZbritje);
                    DateTime.TryParse(dbDataRowTrupiKategoriZbritje["DATEFILLIMI"].ToString(), out dateFillimi);
                    DateTime.TryParse(dbDataRowTrupiKategoriZbritje["DATEMBARIMI"].ToString(), out dateMbarimi);
                    decimal.TryParse(dbDataRowTrupiKategoriZbritje["VLERAMIN"].ToString(), out vleraMin);
                    decimal.TryParse(dbDataRowTrupiKategoriZbritje["VLERAMAX"].ToString(), out vleraMax);
                    int.TryParse(dbDataRowTrupiKategoriZbritje["LLOJI"].ToString(), out lloji);
                    decimal.TryParse(dbDataRowTrupiKategoriZbritje["ZBRITJA"].ToString(), out zbritja);
                    int.TryParse(dbDataRowTrupiKategoriZbritje["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    //int.TryParse(dbDataRowTrupiKategoriZbritje["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowTrupiKategoriZbritje["PRIORITETI"].ToString(), out prioriteti);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te kategorise se zbritjes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
