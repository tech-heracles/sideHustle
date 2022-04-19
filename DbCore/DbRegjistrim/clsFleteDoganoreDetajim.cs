using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbKontabiliteti;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje detajim te nje   flete doganore
    ///  (Te dhenat  merren nga tabela : T_FLETEDOGANOREDETAJIM)
    /// </summary>
    public class clsFleteDoganoreDetajim
    {
        #region Atributet

        private int idFleteDoganoreDetajim;     
        private int idFleteDoganoreTrupi;
        private decimal vlTransport;
        private decimal vlSiguracion;
        private decimal vlTjera;        
        private decimal vlDoganim;
        private decimal vlTaksa;
        private colFleteDoganoreDetajimSub oColSub;
        private DataRow rreshti;
        // private colFleteDoganoreTrupi oColTrupi;       

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="id"> id ritese e detajimit te fletes doganore</param>
        /// <param name="idTrupi"> id e trupit te fletes doganore me te cilen lidhet</param>
        /// <param name="vlDog"> vlera e doganimit</param>
        /// <param name="vlSig"> vlera e siguracionit</param>
        /// <param name="vlTak"> vlera e takses</param>
        /// <param name="vlTj">vlera te tjera</param>
        /// <param name="vlTransp"> vlera e transportit</param>
        public clsFleteDoganoreDetajim(int id, int idTrupi, decimal vlTransp, decimal vlSig, decimal vlTj, decimal vlDog, decimal vlTak)
        {
            idFleteDoganoreDetajim = id;
            idFleteDoganoreTrupi = idTrupi;           
            vlTransport = vlTransp;
            vlSiguracion = vlSig;
            vlTjera = vlTj;
            vlDoganim= vlDog;
            vlTaksa = vlTak;
            oColSub = new colFleteDoganoreDetajimSub();
           // oColTrupi = new colFleteDoganoreTrupi();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsFleteDoganoreDetajim()
        {
        }

        public clsFleteDoganoreDetajim(DataRow rreshti)
        {
            
            mbushFleteDoganoreDetajim(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdFleteDoganoreDetajim
        {
            get { return idFleteDoganoreDetajim; }
            set { idFleteDoganoreDetajim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e trupit te fletes doganore me te cilen lidhet.
        /// </summary>
        public int IdFleteDoganoreTrupi
        {
            get { return idFleteDoganoreTrupi; }
            set { idFleteDoganoreTrupi = value; }
        }

        public colFleteDoganoreDetajimSub OColSub
        {
            get
            {
                return oColSub;
            }
            set
            {
                oColSub = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos  vleren e transportit.
        /// </summary>
        public Decimal VlTransport
        {
            get { return vlTransport; }
            set { vlTransport = value; }
        }

        /// <summary>
        /// Kthen/Vendos  vleren e siguracionit.
        /// </summary>
        public Decimal VlSiguracion
        {
            get { return vlSiguracion; }
            set { vlSiguracion = value; }
        }

        /// <summary>
        /// Kthen/Vendos  vlera te tjera.
        /// </summary>
        public Decimal VlTjera
        {
            get { return vlTjera; }
            set { vlTjera = value; }
        }

        /// <summary>
        /// Kthen/Vendos  vleren e doganimit.
        /// </summary>
        public Decimal VlDoganim
        {
            get { return vlDoganim; }
            set { vlDoganim = value; }
        }

        /// <summary>
        /// Kthen/Vendos  vleren e taksave.
        /// </summary>
        public Decimal VlTaksa
        {
            get { return vlTaksa; }
            set { vlTaksa = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e detajimit te fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajFleteDoganoreDetajim"/> 
        /// </summary>   
        /// <returns> clsMesazh qe jep nje mesazh nqs ruajtja ka perfunduar ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            int idfletDog;
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_ruajt = data.ruajFleteDoganoreDetajim(out idfletDog, this.IdFleteDoganoreTrupi, this.VlTransport, this.VlSiguracion, this.VlTjera, this.VlDoganim, this.VlTaksa);
            this.IdFleteDoganoreDetajim = idfletDog;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// modifikon objektin e detajimit te fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoFleteDoganoreDetajim"/> 
        /// </summary>   
        /// <returns> clsMesazh qe jep nje mesazh nqs modifikimi ka perfunduar ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoFleteDoganoreDetajim(this.IdFleteDoganoreDetajim, this.IdFleteDoganoreTrupi, this.VlTransport, this.VlSiguracion, this.VlTjera, this.VlDoganim, this.VlTaksa);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// fshin objektin e detajimit te fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiFleteDoganoreDetajim"/> 
        /// </summary>   
        /// <returns> clsMesazh qe jep nje mesazh nqs fshirja ka perfunduar ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiFleteDoganoreDetajim(this.IdFleteDoganoreDetajim);
            data.Dispose();
            return u_fshi;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush detajimin e fleteve doganore nga databaza
        /// </summary>
        /// <param name="dbDataRowFDDetajim">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushFleteDoganoreDetajim(DataRow dbDataRowFDDetajim)
        {
            if (dbDataRowFDDetajim != null)
            {
                try
                {
                    idFleteDoganoreDetajim = int.Parse(dbDataRowFDDetajim["IDFLETEDOGANOREDETAJIM"].ToString());
                    idFleteDoganoreTrupi = int.Parse(dbDataRowFDDetajim["IDFLETEDOGANORETRUPI"].ToString());
                    vlTransport = decimal.Parse(dbDataRowFDDetajim["VLTRANSPORT"].ToString());
                    vlSiguracion = decimal.Parse(dbDataRowFDDetajim["VLSIGURACION"].ToString());
                    vlTjera = decimal.Parse(dbDataRowFDDetajim["VLTJERA"].ToString());
                    vlDoganim = decimal.Parse(dbDataRowFDDetajim["VLDOGANE"].ToString());
                    vlTaksa = decimal.Parse(dbDataRowFDDetajim["VLTAKSA"].ToString());
                    oColSub = new colFleteDoganoreDetajimSub();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se detajimeve te fleteve doganore nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
