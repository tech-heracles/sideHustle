using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Resources;

namespace DbCore.DbShare
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne formatet e numrave
    ///  (Te dhenat  merren nga tabela : T_FORMATNRKONFIG)
    /// </summary>
    public class clsFormatiKonfig
    {
        #region Atribute

        private int idFormatKonfig;
        private int idKategoria;
        private int idNdermarrje;
        private string kategoria;
        private int idStatusDok;
        private string kodi;
        private string emertimi;
        private int idkrijuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colFormatKonfigTrupi konfigTrupi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        public clsFormatiKonfig(int idFormKonf, int idKat, int idNderm, string kateg, int idstatusdok, string kodi, string emertimi, int idKrijuesi)
        {
            this.idFormatKonfig = idFormKonf;
            this.idKategoria = idKat;
            this.idNdermarrje = idNderm;
            this.kategoria = kateg;
            this.idStatusDok = idstatusdok;
            this.kodi = kodi;
            this.emertimi = emertimi;
            this.idkrijuesi = idKrijuesi;
        }

        public clsFormatiKonfig(int idKat, int idNderm, string kateg, int idstatusdok, string kodi, string emertimi, int idKrijuesi, colFormatKonfigTrupi trupi, bool shtim, CultureInfo cultinf, ResourceManager rm)
        {
            try
            {
                //this.idFormatKonfig = idFormKonf;
                this.idKategoria = idKat;
                this.idNdermarrje = idNderm;
                this.kategoria = kateg;
                this.idStatusDok = idstatusdok;
                this.kodi = kodi;
                this.emertimi = emertimi;
                this.idkrijuesi = idKrijuesi;
                this.konfigTrupi = trupi;
                clsMesazh mesazh = this.kontrolloFormatKonfig(shtim, cultinf, rm);

                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private clsMesazh kontrolloFormatKonfig(bool shtim, CultureInfo cultinf, ResourceManager rm)
        {    
                if (kodi == "")
                    return new clsMesazh(false,rm.GetString("msgPlotesoniKodin", cultinf));
                if (emertimi == "")
                    return new clsMesazh(false, rm.GetString("msgPlotesoniEmertimin", cultinf));
                if (idKategoria == 0 || idKategoria == -1)
                    return new clsMesazh(false, rm.GetString("msgZgjidhKategorine", cultinf));
                if (shtim && ekzistonKonfigSipasKodit(kodi, idNdermarrje))
                    return new clsMesazh(false,rm.GetString("msgEkzistonNjeFormatMeKod", cultinf));
                if (konfigTrupi == null || konfigTrupi.Count == 0)
                    return new clsMesazh(false,rm.GetString("msgFormatJoBosh", cultinf));
                    return new clsMesazh(true, rm.GetString("msgKontrolliFormatitMeSukses", cultinf));

        }

        public clsFormatiKonfig()
        {
            konfigTrupi = new colFormatKonfigTrupi();
        }

        public clsFormatiKonfig(DataRow rreshti)
        {
            
            mbushFormatKonfig(rreshti);
        }

        public clsFormatiKonfig(int idKonfig, clsDatabaseShare db)
        {
            mbushFormatKonfigMeTrup(db.TransCache.getFormatKonfig(idKonfig, db));
        }

        #endregion

        #region Properties

        public int IdFormatKonfig
        {
            get { return idFormatKonfig; }
            set { idFormatKonfig = value; }
        }

        public int IdKategoria
        {
            get { return idKategoria; }
            set { idKategoria = value; }
        }

        public int IdNdermarrja
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        public String Kategoria
        {
            get { return kategoria; }
            set { kategoria = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public String Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        public String Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }

        public int IdKrijuesi
        {
            get { return idkrijuesi; }
            set { idkrijuesi = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        public colFormatKonfigTrupi KonfigTrupi
        {
            get { return konfigTrupi; }
            set { konfigTrupi = value; }
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh sukses = new clsMesazh();
            int idFormati = -1;
            try
            {
                data.beginTransaksion();
                sukses = data.ruajFormat(out idFormati, this.IdKategoria, this.IdNdermarrja, this.idStatusDok, this.kodi, this.emertimi, this.idkrijuesi);
                this.IdFormatKonfig = idFormati;
                if (!sukses.Status)
                {
                    data.rollbackTransaksion();
                    return sukses;
                }
                if (this.konfigTrupi != null && this.konfigTrupi.Count > 0)
                {
                    foreach (clsFormatKonfigTrup tr in this.konfigTrupi)
                    {
                        tr.IdFormatKonfig = idFormati;
                        sukses = tr.ruaj(data);
                        if (!sukses.Status)
                        {
                            data.rollbackTransaksion();
                            return sukses;
                        }
                    }
                }
                data.commitTransaksion();
                return sukses;                
            }
            catch
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            }
        }

        public clsMesazh modifiko()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh sukses = new clsMesazh();
            try
            {
                data.beginTransaksion();                
                sukses = data.modifikoFormat(this.idFormatKonfig, this.idKategoria, this.kodi, this.emertimi, this.idkrijuesi);
                if (!sukses.Status)
                {
                    data.rollbackTransaksion();
                    return sukses;
                }

                sukses = clsFormatKonfigTrup.fshiSipasKoka(data, this.idFormatKonfig);
                if (this.konfigTrupi != null && this.konfigTrupi.Count > 0)
                {
                    foreach (clsFormatKonfigTrup tr in this.konfigTrupi)
                    {
                        tr.IdFormatKonfig = this.idFormatKonfig;
                        sukses = tr.ruaj(data);
                        if (!sukses.Status)
                        {
                            data.rollbackTransaksion();
                            return sukses;
                        }
                    }
                }
                data.commitTransaksion();
                return sukses;
            }
            catch
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            }
        }

        public clsMesazh fshi()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_fshi = data.fshiFormat(this.IdFormatKonfig);
            data.Dispose();
            return u_fshi;
        }

        public clsMesazh fshiFormat()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_fshi = data.fshiFormatin(this.IdFormatKonfig);
            data.Dispose();
            return u_fshi;
        }

        public colFormatKonfig merrTeGjithe()
        {
            colFormatKonfig data = new colFormatKonfig(this.IdNdermarrja);
            return data; //i kalohet idNdermarrje
        }
        
        public bool mbushFormatNrKonfigSipasKodit(string kodi, int idNdermarrje)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushFormatKonfig(data.ktheFormatNrKonfigSipasKodit(kodi, idNdermarrje));
            data.Dispose();
            return mbush;
        }

        public bool mbushFormatNrKonfigSipasId(int id)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushFormatKonfig(data.ktheFormatNrKonfigSipasId(id));
            data.Dispose();
            return mbush;
        }

        public static DataRow merrFormatNrKonfigSipasId(int id)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            DataRow dr = data.ktheFormatNrKonfigSipasId(id);
            data.Dispose();
            return dr;
        }

        public static bool ekzistonKonfigSipasKodit(string kodi, int idNdermarrje)
        {
            clsDatabaseShare dbShare = new clsDatabaseShare();
            bool ekziston = dbShare.ekzistonKonfigSipasKodit(kodi, idNdermarrje);
            dbShare.Dispose();
            return ekziston;
        }

        public static bool eshteLidhurFormatNrMeKonfigurim(int idFormati)
        {
            clsDatabaseShare dbShare = new clsDatabaseShare();
            bool ekziston = dbShare.eshteLidhurFormatNrMeKonfigurim(idFormati);
            dbShare.Dispose();
            return ekziston;
        }

        public bool mbushFormatNrKonfigSipasIdKonfigAmbjente(int idKonfigurimi)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushFormatNrKonfigSipasIdKonfigAmbjente(data, idKonfigurimi);
            data.Dispose();
            return mbush;
        }

        public bool mbushFormatNrKonfigSipasIdKonfigAmbjente(clsDatabaseShare data, int idKonfigurimi)
        {
            return mbushFormatKonfigMeTrup(data.ktheFormatNrKonfigSipasIdKonfigAmbjente(idKonfigurimi));
        }

        #endregion

        #region Metoda Internal

        internal bool mbushFormatKonfig(DataRow dbDataRowFormatKonfig)
        {
            if (dbDataRowFormatKonfig != null)
            {
                try
                {
                    int.TryParse(dbDataRowFormatKonfig["IDFORMATKONFIG"].ToString(), out idFormatKonfig);
                    int.TryParse(dbDataRowFormatKonfig["IDKATEGORIA"].ToString(), out idKategoria);
                    int.TryParse(dbDataRowFormatKonfig["IDNDERMARRJA"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowFormatKonfig["IDSTATUSDOK"].ToString(), out idStatusDok);
                    kategoria = dbDataRowFormatKonfig["KATEGORIA"].ToString();
                    kodi = dbDataRowFormatKonfig["KODI"].ToString();
                    emertimi = dbDataRowFormatKonfig["EMERTIMI"].ToString();
                    int.TryParse(dbDataRowFormatKonfig["IDKRIJUESI"].ToString(), out idkrijuesi);
                    DateTime.TryParse(dbDataRowFormatKonfig["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowFormatKonfig["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate cast-it!");
                }
                catch (Exception)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se formatit te konfigurimit nga db-ja!");
                }
            }
            else
                return false;
        }

        internal bool mbushFormatKonfigMeTrup(DataRow dbDataRowFormatKonfig)
        {
            if (dbDataRowFormatKonfig != null)
            {
                try
                {
                    int.TryParse(dbDataRowFormatKonfig["IDFORMATKONFIG"].ToString(), out idFormatKonfig);
                    int.TryParse(dbDataRowFormatKonfig["IDKATEGORIA"].ToString(), out idKategoria);
                    int.TryParse(dbDataRowFormatKonfig["IDNDERMARRJA"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowFormatKonfig["IDSTATUSDOK"].ToString(), out idStatusDok);
                    kategoria = dbDataRowFormatKonfig["KATEGORIA"].ToString();
                    kodi = dbDataRowFormatKonfig["KODI"].ToString();
                    emertimi = dbDataRowFormatKonfig["EMERTIMI"].ToString();
                    int.TryParse(dbDataRowFormatKonfig["IDKRIJUESI"].ToString(), out idkrijuesi);
                    DateTime.TryParse(dbDataRowFormatKonfig["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowFormatKonfig["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    colFormatKonfigTrupi trupi = new colFormatKonfigTrupi(int.Parse(dbDataRowFormatKonfig["IDFORMATKONFIG"].ToString()));
                    if (trupi == null)
                        konfigTrupi = new colFormatKonfigTrupi();
                    else
                        konfigTrupi = trupi;
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate cast-it!");
                }
                catch (Exception)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se formatit te konfigurimit nga db-ja!");
                }
            }
            else
                return false;
        }

        public clsMesazh mbushFormatKonfigMeTrup(clsFormatiKonfig formatKonfig)
        {
            idFormatKonfig = formatKonfig.IdFormatKonfig;
            idKategoria = formatKonfig.IdKategoria;
            idNdermarrje = formatKonfig.IdNdermarrja;
            idStatusDok = formatKonfig.IdStatusDok;
            kategoria = formatKonfig.Kategoria;
            kodi = formatKonfig.Kodi;
            emertimi = formatKonfig.Emertimi;
            idkrijuesi = formatKonfig.IdKrijuesi;
            dtKrijimi = formatKonfig.DtKrijimi;
            dtModifikimi = formatKonfig.DtModifikimi;
            konfigTrupi = formatKonfig.KonfigTrupi;
            return new clsMesazh(true, $"Mbushja e formatit te konfigurimit {formatKonfig.Kodi} u be me sukses!");
        }

        #endregion
    }
}