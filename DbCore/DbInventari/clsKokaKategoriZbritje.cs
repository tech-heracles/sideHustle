using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne kokat e kategorive te zbritjes
    ///  (Te dhenat  merren nga tabela : T_KOKAKATEGORIZBRITJE)
    /// </summary>
    public class clsKokaKategoriZbritje
    {
        #region Atribute

        private int idKokaKategoriZbritje;
        private string kodKategoriZbritje;
        private string pershkrimKategoriZbritje;
        private int idPerdoruesi;
        //private int idNderViti;
        private decimal zbritja;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idMonedha;
        private colTrupatKategoriteZbritjes oColTrupat;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
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
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes vitit.
        /// </summary>
        //public int IdNderViti
        //{
        //    get { return idNderViti; }
        //    set { idNderViti = value; }
        //}
        /// <summary>
        /// Kthen/Vendos kodin kategori zbritje.
        /// </summary>
        public string KodKategoriZbritje
        {
            get { return kodKategoriZbritje; }
            set { kodKategoriZbritje = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e kategori zbritjes.
        /// </summary>
        public string PershkrimKategoriZbritje
        {
            get
            {
                return pershkrimKategoriZbritje;
            }
            set
            {
                pershkrimKategoriZbritje = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos zbritja.
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
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nje koleksion me trupat e kategori zbritjes.
        /// </summary>
        public colTrupatKategoriteZbritjes OColTrupat
        {
            get
            {
                return oColTrupat;
            }
            set
            {
                oColTrupat = value;
            }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        /// <summary>
        /// kthen/vendos monedhen e kategorise
        /// </summary>
        public int IdMonedha
        {
            get
            {
                return idMonedha;
            }
            set
            {
                idMonedha = value;
            }
        }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idKokaKategoriZbritje"> id ritese e kokes se kategorise zbritje</param>
        /// <param name="kodKategoriZbritje"> kod kategori zbritje</param>
        /// <param name="pershkrimKategoriZbritje"> pershkrim kategori zbritje</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="idmonedha">id e monedhes</param>
        public clsKokaKategoriZbritje(int idKokaKategoriZbritje, string kodKategoriZbritje, string pershkrimKategoriZbritje, int idPerdoruesi, decimal zbritja, int idnderm, int idstatusdok, int idmonedha)
        {
            this.idKokaKategoriZbritje = idKokaKategoriZbritje;
            this.kodKategoriZbritje = kodKategoriZbritje;
            this.pershkrimKategoriZbritje = pershkrimKategoriZbritje;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.zbritja = zbritja;
            this.idNdermarje = idnderm;
            this.idStatusDok = idstatusdok;
            this.idMonedha = idmonedha;
        }
        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="kodKategoriZbritje"> kod kategori zbritje</param>
        /// <param name="pershkrimKategoriZbritje"> pershkrim kategori zbritje</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="idmonedha">id e monedhes</param>
        public clsKokaKategoriZbritje(string kodKategoriZbritje, string pershkrimKategoriZbritje, int idPerdoruesi, decimal zbritja, int idnderm, int idstatusdok, int idmonedha)
        {

            this.kodKategoriZbritje = kodKategoriZbritje;
            this.pershkrimKategoriZbritje = pershkrimKategoriZbritje;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.zbritja = zbritja;
            this.idNdermarje = idnderm;
            this.idStatusDok = idstatusdok;
            this.idMonedha = idmonedha;
        }
        /// <summary>
        /// konstruktori me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes kategori zbritje</param>
        public clsKokaKategoriZbritje(int id)
        {
            clsDatabaseInventari dbKokatKategoriteZbritje = new clsDatabaseInventari();
            mbushKokaKategoriZbritje(dbKokatKategoriteZbritje.merrKokaKategoriZbritje(id));
            dbKokatKategoriteZbritje.Dispose();
        }
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKokaKategoriZbritje()
        {
        }

        public clsKokaKategoriZbritje(DataRow rreshti)
        {
            
            mbushKokaKategoriZbritje(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan nje objekt kategori zbritje sebashku me trupin
        /// Nje objekt kategori zbritje ka nje koleksion me trupat, 
        /// ruajtja e nje kategori zbritje imponon ruajtjen edhe te nje colection-i me trupat
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe kategoria e zbritjes bashke me trupat konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje kategori zbritje sebashku me trupat
        /// </summary>
        /// <param name="idKokaKategoriZbritje"> id ritese e kokes se kategorise zbritje</param>
        /// <param name="kodKategoriZbritje"> kod kategori zbritje</param>
        /// <param name="pershkrimKategoriZbritje"> pershkrim kategori zbritje</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        ///  <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>     
        public clsMesazh ruajKategoriZbritje(int idKokaKategoriZbritje, string kodKategoriZbritje, string pershkrimKategoriZbritje, int idPerdoruesi, decimal zbritja, int idnderm, colTrupatKategoriteZbritjes oColTrupat, int idstatusdok, int idmonedha)
        {//ruan kategoriZbritje
            clsDatabaseInventari db = new clsDatabaseInventari();
            //clsKokaKategoriZbritje kategoriZbritje = new clsKokaKategoriZbritje(idKokaKategoriZbritje, kodKategoriZbritje, pershkrimKategoriZbritje, idPerdoruesi, idNderViti, zbritja, idnderm);
            bool statusVeprimi;



            clsMesazh mesazh = new clsMesazh();
            try
            {
                db.beginTransaksion();
                idKokaKategoriZbritje = db.ruajKokaKategoriZbritje(idKokaKategoriZbritje, kodKategoriZbritje, pershkrimKategoriZbritje, idPerdoruesi, zbritja, idnderm, idstatusdok, idmonedha);
                this.IdKokaKategoriZbritje = idKokaKategoriZbritje;
                if (idKokaKategoriZbritje == 0)
                    statusVeprimi = false;
                else statusVeprimi = true;

                if (statusVeprimi)
                {
                    foreach (clsTrupiKategoriZbritje o in oColTrupat)
                    {
                        if (statusVeprimi)
                        {
                            o.IdKokaKategoriZbritje = idKokaKategoriZbritje;
                            int idT;
                            mesazh = db.ruajTrupiKategoriZbritje(out idT, o.IdKokaKategoriZbritje, o.DateFillimi, o.DateMbarimi, o.VleraMin, o.VleraMax, o.Lloji,
                                o.Zbritja, o.IdPerdoruesi, o.Prioriteti);
                            //mesazh= ruajTrupiKategoriZbritje(o);
                        }
                        else
                        {
                            db.rollbackTransaksion();

                            return mesazh;
                        }
                    }
                    if (mesazh.Status)
                    {
                        db.commitTransaksion();

                        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                        return mesazh;
                    }
                    else
                    {
                        db.rollbackTransaksion();

                        return mesazh;
                    }
                }
                else
                {
                    db.rollbackTransaksion();

                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                db.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }
        /// <summary>
        /// Ruan objektin kategorine e zbritjes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajKategoriZbritje"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh ruajKategoriZbritje(int idKokaKategoriZbritje, string kodKategoriZbritje, string pershkrimKategoriZbritje, int idPerdoruesi, int idNderViti, decimal zbritja, int idnderm)", true)]
        //public clsMesazh ruaj()
        //{
        //    clsDatabaseInventari data = new clsDatabaseInventari();
        //    clsMesazh u_ruajt = data.ruajKategoriZbritje(this.IdKokaKategoriZbritje, this.KodKategoriZbritje, this.PershkrimKategoriZbritje, this.IdPerdoruesi, this.IdNderViti, this.Zbritja, this.IdNdermarje);
        //    //clsMesazh u_ruajt = data.ruajKategoriZbritje(this);
        //    return u_ruajt;
        //}

        /// <summary>
        /// Modifikon nje objekt kategori zbritje sebashku me trupin
        /// Nje objekt kategori zbritje ka nje koleksion me trupat, 
        /// modifikimi e nje kategori zbritje imponon modifikimin edhe te nje colection-i me trupat
        /// Mqs cdo rresht i ri qe modifikohet ne DB kerkon thirrjen e nje SP-je me parametra dhe kategoria e zbritjes bashke me trupat konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje kategori zbritje sebashku me trupat
        /// </summary>
        /// <param name="idKokaKategoriZbritje"> id ritese e kokes se kategorise zbritje</param>
        /// <param name="kodKategoriZbritje"> kod kategori zbritje</param>
        /// <param name="pershkrimKategoriZbritje"> pershkrim kategori zbritje</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        ///  <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>     
        public clsMesazh modifikoKategoriZbritje(int idKokaKategoriZbritje, string kodKategoriZbritje, string pershkrimKategoriZbritje, int idPerdoruesi, decimal zbritja, int idnderm, colTrupatKategoriteZbritjes oColTrupat, int idstatusdok, int idmonedha)
        {
            //modifikon KategoriZbritje
            //colTrupatKategoriteZbritjes trupat = merrTrupatKategoriZbritjeSipasKokes(kategoriZbritje.IdKokaKategoriZbritje);
            colTrupatKategoriteZbritjes trupat = new colTrupatKategoriteZbritjes();
            //clsKokaKategoriZbritje kategoriZbritje = new clsKokaKategoriZbritje(idKokaKategoriZbritje, kodKategoriZbritje, pershkrimKategoriZbritje, idPerdoruesi, idNderViti, zbritja, idnderm);
            clsDatabaseInventari db = new clsDatabaseInventari();


            trupat.mbushTrupatKategoriZbritjeSipasKokes(idKokaKategoriZbritje);
            clsMesazh mesazh = new clsMesazh();
            try
            {
                db.beginTransaksion();
                mesazh = db.modifikoKokaKategoriZbritje(idKokaKategoriZbritje, kodKategoriZbritje, pershkrimKategoriZbritje, idPerdoruesi, zbritja, idNdermarje, idstatusdok, idmonedha);
                //mesazh=  modifikoKokaKategoriZbritje(kategoriZbritje);

                if (mesazh.Status)
                {
                    if (trupat.Count < oColTrupat.Count)//rasti kur jane shtuar rreshta trupi
                    {
                        for (int i = 0; i < oColTrupat.Count; i++)
                        {
                            if (mesazh.Status)
                            {
                                oColTrupat[i].IdKokaKategoriZbritje = IdKokaKategoriZbritje;
                                if (i < trupat.Count)
                                {

                                    oColTrupat[i].IdTrupiKategoriZbritje = trupat[i].IdTrupiKategoriZbritje;
                                    mesazh = db.modifikoTrupiKategoriZbritje(oColTrupat[i].IdTrupiKategoriZbritje, oColTrupat[i].IdKokaKategoriZbritje, oColTrupat[i].DateFillimi,
                                        oColTrupat[i].DateMbarimi, oColTrupat[i].VleraMin, oColTrupat[i].VleraMax, oColTrupat[i].Lloji, oColTrupat[i].Zbritja,
                                        oColTrupat[i].IdPerdoruesi, oColTrupat[i].Prioriteti);
                                    //mesazh=  modifikoTrupiKategoriZbritje(kategoriZbritje.OColTrupat[i]);
                                }
                                else
                                {
                                    int idoC;
                                    mesazh = db.ruajTrupiKategoriZbritje(out idoC, oColTrupat[i].IdKokaKategoriZbritje, oColTrupat[i].DateFillimi,
                                        oColTrupat[i].DateMbarimi, oColTrupat[i].VleraMin, oColTrupat[i].VleraMax, oColTrupat[i].Lloji, oColTrupat[i].Zbritja,
                                        oColTrupat[i].IdPerdoruesi, oColTrupat[i].Prioriteti);
                                }
                                //mesazh= ruajTrupiKategoriZbritje(kategoriZbritje.OColTrupat[i]);
                            }
                            else
                            {
                                db.rollbackTransaksion();

                                return mesazh;
                            }
                        }
                    }
                    else//rasti kur jane fshire rreshta
                    {
                        int count = 0;
                        for (int i = 0; i < trupat.Count; i++)
                        {
                            if (mesazh.Status)
                            {
                                if (count < oColTrupat.Count)
                                {

                                    oColTrupat[i].IdKokaKategoriZbritje = idKokaKategoriZbritje;

                                    oColTrupat[i].IdTrupiKategoriZbritje = trupat[i].IdTrupiKategoriZbritje;
                                    mesazh = db.modifikoTrupiKategoriZbritje(oColTrupat[i].IdTrupiKategoriZbritje, oColTrupat[i].IdKokaKategoriZbritje, oColTrupat[i].DateFillimi,
                                        oColTrupat[i].DateMbarimi, oColTrupat[i].VleraMin, oColTrupat[i].VleraMax, oColTrupat[i].Lloji, oColTrupat[i].Zbritja,
                                        oColTrupat[i].IdPerdoruesi, oColTrupat[i].Prioriteti);
                                    //mesazh= modifikoTrupiKategoriZbritje(kategoriZbritje.OColTrupat[i]);
                                }
                                else
                                {
                                    //mesazh=  fshiTrupiKategoriZbritje(trupat[i]);
                                    mesazh = db.fshiTrupiKategoriZbritje(trupat[i].IdTrupiKategoriZbritje);
                                }
                                count++;
                            }
                            else
                            {
                                db.rollbackTransaksion();

                                return mesazh;
                            }
                        }
                    }

                    if (mesazh.Status)
                    {
                        db.commitTransaksion();

                        mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                        return mesazh;
                    }
                    else
                    {
                        db.rollbackTransaksion();

                        return mesazh;
                    }
                }
                else
                {
                    db.rollbackTransaksion();

                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                db.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }
        /// <summary>
        /// Modifikon objektin kategorine e zbritjes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoKategoriZbritje"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoKategoriZbritje(int idKokaKategoriZbritje, string kodKategoriZbritje, string pershkrimKategoriZbritje, int idPerdoruesi, int idNderViti, decimal zbritja, int idnderm)", true)]
        //public clsMesazh modifiko()
        //{
        //    clsDatabaseInventari data = new clsDatabaseInventari();
        //    clsMesazh u_modifikua = data.modifikoKategoriZbritje(this.IdKokaKategoriZbritje, this.KodKategoriZbritje, this.PershkrimKategoriZbritje, this.IdPerdoruesi, this.IdNderViti, this.Zbritja, this.IdNdermarje);
        //    //clsMesazh u_modifikua = data.modifikoKategoriZbritje(this);
        //    return u_modifikua;
        //}
        public bool KaVeprime()
        {
            using (var dbInventari = new clsDatabaseInventari())
                return dbInventari.kaVeprimeKategoriZbritje(IdKokaKategoriZbritje);
        }
        public clsMesazh fshiKategoriZbritje(int idKokaKategoriZbritje)
        {//fshin kategorizbritje

            //colTrupatKategoriteZbritjes  trupat = merrTrupatKategoriZbritjeSipasKokes(kategorizbritje.IdKokaKategoriZbritje);
            colTrupatKategoriteZbritjes trupat = new colTrupatKategoriteZbritjes();
            trupat.mbushTrupatKategoriZbritjeSipasKokes(idKokaKategoriZbritje);
            clsDatabaseInventari db = new clsDatabaseInventari();


            clsMesazh mesazh = new clsMesazh(true);
            try
            {
                db.beginTransaksion();
                foreach (clsTrupiKategoriZbritje o in trupat)
                {
                    if (mesazh.Status)
                    {
                        mesazh = db.fshiTrupiKategoriZbritje(o.IdTrupiKategoriZbritje);
                        //mesazh = fshiTrupiKategoriZbritje(o);
                    }
                    else
                    {
                        db.rollbackTransaksion();

                        return mesazh;
                    }
                }
                if (mesazh.Status)
                {
                    mesazh = db.fshiKokaKategoriZbritje(idKokaKategoriZbritje);
                    //mesazh =  fshiKokaKategoriZbritje(kategorizbritje);
                    if (mesazh.Status)
                    {
                        db.commitTransaksion();

                        mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                        return mesazh;
                    }
                    else
                    {
                        db.rollbackTransaksion();

                        return mesazh;
                    }
                }
                else
                {
                    db.rollbackTransaksion();

                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                db.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }
        /// <summary>
        /// Fshin objektin kategorine e zbritjes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiKategoriZbritje"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh fshiKategoriZbritje(int idKokaKategoriZbritje)", true)]
        //public clsMesazh fshi()
        //{
        //    clsDatabaseInventari data = new clsDatabaseInventari();
        //    clsMesazh u_fshi = data.fshiKategoriZbritje(this.IdKokaKategoriZbritje);
        //    //clsMesazh u_fshi = data.fshiKategoriZbritje(this);
        //    return u_fshi;
        //}
        /// <summary>
        /// Merr objektin kategorine e zbritjes nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.merrKokaKategoriZbritje"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            clsMesazh ufshi = db.fshiKokaKategoriZbritjeStatus(this.idKokaKategoriZbritje, this.idPerdoruesi);
            db.Dispose();
            return ufshi;
        }
        public void merr()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.merrKokaKategoriZbritjePaKthim(this.IdKokaKategoriZbritje);
            data.Dispose();
            //data.merrKokaKategoriZbritje(this);
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e kokes kategori zbritje sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kod">kodi i artikullit</param>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>id e kokes kategori zbritje</returns>
        public static int ktheIdKokaKategoriZbritje(string kod, int idnder)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            int idKoka = (dbartikuj.merrKokaKategoriZbritje(kod, idnder));
            dbartikuj.Dispose();
            return idKoka;
        }
        public static bool ekziston(string kod, int idnder)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool ekziston = db.ekzistonKategoriZbritje(kod, idnder);
            db.Dispose();
            return ekziston;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kokat sipas kategorive te zbritjes nga databaza
        /// </summary>
        /// <param name="dbDataRowKokaKategoriZbritje">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushKokaKategoriZbritje(DataRow dbDataRowKokaKategoriZbritje)
        {
            if (dbDataRowKokaKategoriZbritje != null)
            {

                try
                {
                    int.TryParse(dbDataRowKokaKategoriZbritje["IDKOKAKATEGORIZBRITJE"].ToString(), out idKokaKategoriZbritje);
                    kodKategoriZbritje = dbDataRowKokaKategoriZbritje["KODKATEGORIZBRITJE"].ToString();
                    pershkrimKategoriZbritje = dbDataRowKokaKategoriZbritje["PERSHKRIMKATEGORIZBRITJE"].ToString();
                    int.TryParse(dbDataRowKokaKategoriZbritje["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    //int.TryParse(dbDataRowKokaKategoriZbritje["IDNDERVITI"].ToString(), out idNderViti);
                    decimal.TryParse(dbDataRowKokaKategoriZbritje["ZBRITJA"].ToString(), out zbritja);
                    int.TryParse(dbDataRowKokaKategoriZbritje["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowKokaKategoriZbritje["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowKokaKategoriZbritje["IDMONEDHA"].ToString(), out idMonedha);
                    DateTime.TryParse(dbDataRowKokaKategoriZbritje["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKokaKategoriZbritje["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kategorive te zbritjes se kokave nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
