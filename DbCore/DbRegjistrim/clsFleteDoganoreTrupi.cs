using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne trupin e fletes doganore
    ///  (Te dhenat  merren nga tabela : T_FLETEDOGANORETRUPI)
    /// </summary>
    public class clsFleteDoganoreTrupi
    {
        #region Atribute

        private int idFleteDoganoreTrupi;
        private int idFleteDoganoreKoka;
        private int idFatura;
        private String nrDok;
        private DateTime dtDok;
        private String kodFurnitor;
        private int idFurnitor;
        private decimal vlera;
        private string kodMonedha;
        private colFleteDoganoreDetajim oColDetajim;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtdok"> data e dokumentit</param>
        /// <param name="idfatura">id e fatures</param>
        /// <param name="idkoka"> id e kokes se fletes kontabel</param>
        /// <param name="idtrupi">id ritese e trupit te fletes kontabel</param>
        /// <param name="nrdok"> nr i dokumentit</param>
        public clsFleteDoganoreTrupi(int idtrupi, int idkoka, int idfatura, string nrdok, DateTime dtdok)
        {
            idFleteDoganoreTrupi = idtrupi;
            idFleteDoganoreKoka = idkoka;
            idFatura = idfatura;
            nrDok = nrdok;
            dtDok = dtdok;
            oColDetajim = new colFleteDoganoreDetajim();          
        }
        public clsFleteDoganoreTrupi(int idNdermarrje, Dictionary<string, object> rreshtDokuKlient, object transport, object siguracion, object tjera, object vldog, object taksa, List<Dictionary<string, object>> vlerasub, decimal marzhiGabimit)
        {
            if (rreshtDokuKlient["NrDok"]!=null)
            {
                this.DtDok = DateTime.Parse(rreshtDokuKlient["DtDok"].ToString());
                this.IdFatura = int.Parse(rreshtDokuKlient["IdShitjeKoka"].ToString());
                this.NrDok = rreshtDokuKlient["NrDok"].ToString();
                this.OColDetajim = new colFleteDoganoreDetajim(transport,siguracion,tjera,vldog,taksa,vlerasub, marzhiGabimit);
            }
            else IdFatura = -1;
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e trupit</param>
        public clsFleteDoganoreTrupi(int id)
        {
            clsDatabaseRegjistrim dbFleteDoganoreTrupi = new clsDatabaseRegjistrim();
            mbushFleteDoganoreTrupi(dbFleteDoganoreTrupi.ktheFleteDoganoreTrupiSipasId(id));
            dbFleteDoganoreTrupi.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsFleteDoganoreTrupi()
        {
        }

        public clsFleteDoganoreTrupi(DataRow rreshti)
        {
            
            mbushFleteDoganoreTrupi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdFleteDoganoreTrupi
        {
            get { return idFleteDoganoreTrupi; }
            set { idFleteDoganoreTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se fletes doganore.
        /// </summary>
        public int IdFleteDoganoreKoka
        {
            get { return idFleteDoganoreKoka; }
            set { idFleteDoganoreKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e fatures.
        /// </summary>
        public int IdFatura
        {
            get { return idFatura; }
            set { idFatura = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e dokumentit
        /// </summary>
        public String NrDok
        {
            get { return nrDok; }
            set { nrDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e dokumentit.
        /// </summary>
        public DateTime DtDok
        {
            get { return dtDok; }
            set { dtDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje koleksion me detajimet e fletes doganore.
        /// </summary>
        public String KodFurnitor
        {
            get { return kodFurnitor; }
            set { kodFurnitor = value; }
        }

        public int IdFurnitor
        {
            get { return idFurnitor; }
            set { idFurnitor = value; }
        }

        public Decimal Vlera
        {
            get { return vlera; }
            set { vlera = value; }
        }

        public String KodMonedha
        {
            get { return kodMonedha; }
            set { kodMonedha = value; }
        }

        public colFleteDoganoreDetajim OColDetajim
        {
            get { return oColDetajim; }
            set { oColDetajim = value; }
        }

        #endregion

        #region  Metoda Publike

        /// <summary>
        /// Ruan nje objekt flete doganore trupi sebashku me detajimet perkatese
        /// Nje objekt trupi fleta doganore ka nje koleksion me detajime , 
        /// ruajtja e nje trupi flete doganore imponon ruajtjen edhe te nje colection-i me detajime
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe trupi i fletes doganore sebashku me detajimet konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje trupi flete doganore sebashku me detajimet
        /// </summary>
        /// <param name="dtdok"> data e dokumentit</param>
        /// <param name="idfatura">id e fatures</param>
        /// <param name="idkoka"> id e kokes se fletes kontabel</param>
        /// <param name="idtrupi">id ritese e trupit te fletes kontabel</param>
        /// <param name="nrdok"> nr i dokumentit</param>
        /// <param name="oColDetajim"> kolektion detajimesh</param>
        ///  <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public clsMesazh ruajFleteDoganoreTrupiDheDetajim(int idtrupi, int idkoka, int idfatura, string nrdok, DateTime dtdok, colFleteDoganoreDetajim oColDetajim, clsDatabaseRegjistrim dbRegj)
        {
            clsMesazh mesazh = new clsMesazh();
         
                mesazh = dbRegj.ruajFleteDoganoreTrupi(out idtrupi, idkoka, idfatura, nrdok, dtdok);
            if (!dbRegj.ktheKokaShitjeEkzistonDoksipasID(idfatura))
                return new clsMesazh(false, String.Format("Fatura me nr: {0} eshte modifikuar, rihapeni edhe njehere dokumentin doganor", nrdok));
            if (mesazh.Status)
            {
                foreach (clsFleteDoganoreDetajim o in oColDetajim)
                {
                    o.IdFleteDoganoreTrupi = idtrupi;
                    int id;
                    mesazh = dbRegj.ruajFleteDoganoreDetajim(out id, o.IdFleteDoganoreTrupi, o.VlTransport, o.VlSiguracion, o.VlTjera, o.VlDoganim, o.VlTaksa);
                    if (!mesazh.Status) return mesazh;
                    o.IdFleteDoganoreDetajim = id;
                    foreach (clsFleteDoganoreDetajimSub sub in o.OColSub)
                    {
                        sub.IdFleteDoganoreDetajim = id;
                        int idsub;
                        mesazh = dbRegj.ruajFleteDoganoreDetajimSub(out idsub, sub.IdFleteDoganoreDetajim, sub.VlTransport, sub.VlSiguracion, sub.VlTjera, sub.VlDoganim, sub.VlTaksa, sub.Rreshti);
                        if (!mesazh.Status) return mesazh;
                        sub.IdSubGride = idsub;

                    }
                }
            }
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            

        }
        public bool eshteZhdoganuarFatura(int id)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool sukses = db.eshteZhdoganuarFatura(id);
            db.Dispose();
            return sukses;
        }
        /// <summary>
        /// Ruan objektin e  trupin se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajFleteDoganoreTrupi"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            int id;
            clsMesazh u_ruajt = data.ruajFleteDoganoreTrupi(out id, this.IdFleteDoganoreKoka, this.IdFatura, this.NrDok, this.DtDok);
            this.IdFleteDoganoreTrupi = id;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e  trupit se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoFleteDoganoreTrupi"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoFleteDoganoreTrupi(this.IdFleteDoganoreTrupi, this.IdFleteDoganoreKoka, this.IdFatura, this.NrDok, this.DtDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e  kokes se fletes doganore ne tabelen perkatese ne databaze sipas idse se trupit.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiFleteDoganoreTrupiSipasId"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiSipasId()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiFleteDoganoreTrupiSipasId(this.IdFleteDoganoreTrupi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Fshin objektet e  kokes se fletes doganore ne tabelen perkatese ne databaze sipas idse se kokes.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiFleteDoganoreTrupiSipasKoka"/> 
        /// </summary>
        /// <param name="id">id e kokes se fletes kontabel</param>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiSipasKoka(int id)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiFleteDoganoreTrupiSipasKoka(id);
            data.Dispose();
            return u_fshi;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e fleteve doganore nga databaza
        /// </summary>
        /// <param name="dbDataRowFleteDoganore">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushFleteDoganoreTrupi(DataRow dbDataRowFleteDoganore)
        {
            if (dbDataRowFleteDoganore != null)
            {
                try
                {
                    idFleteDoganoreTrupi = int.Parse(dbDataRowFleteDoganore["IDFLETEDOGANORETRUPI"].ToString());
                    idFleteDoganoreKoka = int.Parse(dbDataRowFleteDoganore["IDFLETEDOGANORE"].ToString());
                    idFatura = int.Parse(dbDataRowFleteDoganore["IDFATURA"].ToString());
                    nrDok = dbDataRowFleteDoganore["NRDOK"].ToString();
                    dtDok = DateTime.Parse(dbDataRowFleteDoganore["DTDOK"].ToString());
                    oColDetajim = new colFleteDoganoreDetajim();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te fleteve doganore nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}