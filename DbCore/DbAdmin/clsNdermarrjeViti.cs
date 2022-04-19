using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using DbCore.IMBUtils.Logging;
using Newtonsoft.Json;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne lidhjen e ndermarrjes me nje vit
    ///  te caktuar.(Te dhenat  merren nga tabela : T_NDERMARJEVITI)
    /// </summary>
    public class clsNdermarrjeViti
    {
        #region Atributet

        private int idNderViti;
        private int idViti;
        private int viti;
        private int idNdermarrje;
        private bool ndermarrjeVitiMbyllur;
        private DateTime ndermarrjeVitiFillim;
        private DateTime ndermarrjeVitiFund;
        private int idPerdoruesi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsNdermarrjeViti(int idNderViti, int idViti, int viti, int idNdermarrje,
                                 bool ndermarrjeVitiMbyllur, DateTime ndermarrjeVitiFillim,
                                DateTime ndermarrjeVitiFund, int idPerdoruesi)
        {
            this.idNderViti = idNderViti;
            this.idViti = idViti;
            this.viti = viti;
            this.idNdermarrje = idNdermarrje;
            this.ndermarrjeVitiMbyllur = ndermarrjeVitiMbyllur;
            this.ndermarrjeVitiFillim = ndermarrjeVitiFillim;
            this.ndermarrjeVitiFund = ndermarrjeVitiFund;
            this.idPerdoruesi = idPerdoruesi;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsNdermarrjeViti(int viti, int idviti, int idndermarrje,
                                 bool ndermarrjevitimbyllur, DateTime ndermarrjevitifillim, DateTime ndermarrjevitifund, int idperdoruesi)
        {
            this.viti = viti;
            idViti = idviti;
            idNdermarrje = idndermarrje;
            ndermarrjeVitiMbyllur = ndermarrjevitimbyllur;
            ndermarrjeVitiFillim = ndermarrjevitifillim;
            ndermarrjeVitiFund = ndermarrjevitifund;
            idPerdoruesi = idperdoruesi;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsNdermarrjeViti()
        {
            ImbLogger.LogTraceShitje("U krijua nje klase e re clsNdermarrjeViti.");
        }

        public clsNdermarrjeViti(int idNderViti)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                if (!mbushNdermarrjeViti(data.merrNdermarrjeVit(idNderViti)))
                {
                    ImbLogger.LogErrorShitje("Mbushja e objektit lsNdermarrjeViti nuk u krye me sukses!");
                    throw new Exception("Mbushja e objektit clsNdermarrjeViti nuk u krye me sukses");
                }
            }
        }

        public clsNdermarrjeViti(int idNderViti, clsDatabaseAdmin data)
        {
            if (!mbushNdermarrjeViti(data.merrNdermarrjeVit(idNderViti)))
            {
                throw new Exception("Mbushja e objektit clsNdermarrjeViti nuk u krye me sukses");
            }
        }

        public clsNdermarrjeViti(int idViti, int idNdermarrje, clsDatabaseAdmin data)
        {
            mbushNdermarrjeViti(data.TransCache.getNdermarrjeViti(idViti, idNdermarrje, data));
        }

        public clsNdermarrjeViti(DataRow rreshti)
        {
            
            mbushNdermarrjeViti(rreshti);
        }

        #endregion

        public bool mbushNdermarrjeViti(int idNderViti)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushNdermarrjeViti(data.merrNdermarrjeVit(idNderViti));
            data.Dispose();
            return sukses;
        }

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdNderViti
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e vitit.
        /// </summary>
        public int IdViti
        {
            get { return idViti; }
            set { idViti = value; }
        }

        /// <summary>
        /// Kthen/Vendos vitin.
        /// </summary>
        public int Viti
        {
            get { return viti; }
            set { viti = value; }
        }

        public int NdermarrjeViti
        {
            get { return viti; }
            set { viti = value; }
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
        /// Kthen/Vendos statusin nese eshte mbyllur ky vit per kete ndermarrjes apo jo.
        /// </summary>
        public bool NdermarrjeVitiMbyllur
        {
            get { return ndermarrjeVitiMbyllur; }
            set { ndermarrjeVitiMbyllur = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e fillimit per kete vit per kete ndermarrje.
        /// </summary>
        public DateTime NdermarrjeVitiFillim
        {
            get { return ndermarrjeVitiFillim; }
            set { ndermarrjeVitiFillim = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e perfundimit per kete vit per kete ndermarrje.
        /// </summary>
        public DateTime NdermarrjeVitiFund
        {
            get { return ndermarrjeVitiFund; }
            set { ndermarrjeVitiFund = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe ka bere lidhjen e ketij viti me kete ndermarrje.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e lidhjes se vitit me ndermarrjen ne tabelen perkatese ne databaze. Thirret funksioni
        /// <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ruajNdermarrjeVit"/> 
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            int idNV;
            clsMesazh u_ruajt = data.ruajNdermarrjeVit(out idNV, this.IdViti, this.Viti, this.IdNdermarrje, this.NdermarrjeVitiMbyllur, this.NdermarrjeVitiFillim, this.NdermarrjeVitiFund, this.IdPerdoruesi);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e lidhjes se vitit me ndermarrjen ne tabelen perkatese ne databaze. Thirret funksioni
        /// <see cref="DbCore.DbAdmin.clsDatabaseAdmin.modifikoNdermarrjeVit"/> 
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoNdermarrjeVit(this.IdNderViti, this.IdViti, this.Viti, this.IdNdermarrje, this.NdermarrjeVitiMbyllur, this.NdermarrjeVitiFillim, this.NdermarrjeVitiFund, this.IdPerdoruesi);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e lidhjes se vitit me ndermarrjen ne tabelen perkatese ne databaze. Thirret funksioni
        /// <see cref="DbCore.DbAdmin.clsDatabaseAdmin.fshiNdermarrjeVit"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiNdermarrjeVit(this.IdNderViti);
            data.Dispose();
            return u_fshi;
        }

        //public void merr()
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    data.merrNdermarrjeVit(this);
        //}

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbAdmin.clsNdermarrjeViti"/> . Thirret funksioni
        /// <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ktheGjitheNdermarrjeVitet"/> 
        /// </summary>
        public colNdermarrjeVitet merriTeGjithe()
        {
            colNdermarrjeVitet data = new colNdermarrjeVitet();
            data.mbushGjitheNdermarrjeVitet();
            return data;

        }



        public bool mbushNdermarrjeVitiSipasNdermarjesDheVitit(int idndermarje, int idviti)
        {
            ImbLogger.LogTraceShitje("Po mbushet ndermarrje viti sipas ndermarrjes dhe vitit  sipas idndermarrje: " +Convert.ToString(idndermarje)+ " dhe idvitit: " + Convert.ToString(idviti));
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                
                return mbushNdermarrjeViti(data.merrNdermarrjeVitSipasNdermarjesDheVitit(idndermarje, idviti));
           
        }

        public static int ktheIdNdermarrjeVitiSipasNdermarjesDheVitit(int idndermarje, int idviti)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return ktheIdNdermarrjeVitiSipasNdermarjesDheVitit(idndermarje, idviti, data);
            }
        }

        public static int ktheIdNdermarrjeVitiSipasNdermarjesDheVitit(int idndermarje, int idviti, clsDatabaseAdmin data)
        {
            return data.merrIdNdermarrjeVitSipasNdermarjesDheVitit(idndermarje, idviti);
        }

        public static int ktheIdNdermarrjeVitiSipasNdermarjesDheKodVitit(int idndermarje, int kodViti, clsDatabaseAdmin data)
        {
            return data.merrIdNdermarrjeVitSipasNdermarjesDheKodVitit(idndermarje, kodViti);
        }
        public static int ktheIdNdermarrjeVitiSipasNdermarjesDheKodVitit(int idndermarje, int kodViti)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrIdNdermarrjeVitSipasNdermarjesDheKodVitit(idndermarje, kodViti);
            }
        }

        /// <summary>
        /// mbush objektin clsNdermarrjeViti me vitin fillim dhe fund te te gjitha viteve per ndermarrjen
        /// </summary>
        /// <param name="idndermarje"></param>
        /// <returns></returns>
        public bool mbushNdermVitFillimFundPerGjitheVitetSipasNdermarjes(int idndermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushNdermarrjeViti(data.merrNdermVitFillimFundGjitheVitetSipasNdermarjes(idndermarje));
            data.Dispose();
            return sukses;
        }

        public int merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(int idndermarje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                bool sukses = mbushNdermarrjeViti(data.merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(idndermarje));
            }
            return this.idNderViti;
        }

        public static int ktheKodVitiSipasIdNdermViti(int idNdermViti)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrKodVitiSipasIdNdermViti(idNdermViti);
            }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushNdermarrjeViti(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(rreshti["IDVITI"].ToString(), out idViti);
                    int.TryParse(rreshti["VITI"].ToString(), out viti);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarrje);
                    bool.TryParse(rreshti["NDERMVITMBYLLUR"].ToString(), out ndermarrjeVitiMbyllur);
                    DateTime.TryParse(rreshti["NDERMVITFILLIM"].ToString(), out ndermarrjeVitiFillim);
                    DateTime.TryParse(rreshti["NDERMVITFUND"].ToString(), out ndermarrjeVitiFund);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    ImbLogger.LogTraceShitje("Mbushja e ndermarrjes nga DB u krye me sukses");
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se lidhjes mes ndermarrjes dhe vitit nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se lidhjes mes ndermarrjes dhe vitit nga db-ja");
                }
            }
            else
                return false;
        }

        public clsMesazh mbushNdermarrjeViti(clsNdermarrjeViti ndermarrjeViti)
        {
            idNderViti = ndermarrjeViti.IdNderViti;
            idViti = ndermarrjeViti.IdViti;
            viti = ndermarrjeViti.Viti;
            idNdermarrje = ndermarrjeViti.IdNdermarrje;
            ndermarrjeVitiMbyllur = ndermarrjeViti.NdermarrjeVitiMbyllur;
            ndermarrjeVitiFillim = ndermarrjeViti.NdermarrjeVitiFillim;
            ndermarrjeVitiFund = ndermarrjeViti.NdermarrjeVitiFund;
            idPerdoruesi = ndermarrjeViti.IdPerdoruesi;
            return new clsMesazh(true, $"Mbushja e vitit {ndermarrjeViti.Viti} te ndermarrjes u be me sukses!");

        }

        #endregion
    }
}
