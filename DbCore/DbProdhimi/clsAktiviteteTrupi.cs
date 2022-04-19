using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje aktivitet trupi
    ///  (Te dhenat  merren nga tabela : T_AKTIVITETETRUPI)
    /// </summary>
    public class clsAktiviteteTrupi
    {
        /// <summary>
        /// mesazh gabimi per kohen
        /// </summary>
        private const string STR_KohaDuhetTeJeteNumerPozitiv = "Koha duhet te jete numer pozitiv!";
        /// <summary>
        /// mesazh gabimi per te plotesuar burimin
        /// </summary>
        private const string STR_PlotesoniBurimit = "Plotesoni burimit!";
        /// <summary>
        /// mesazh gabimi kur burimi nuk ekziston
        /// </summary>
        private const string STR_BurimiNukEkziston = "Burimi nuk ekziston!";
        /// <summary>
        /// mesazh kur kontrollet kalohen me sukses
        /// </summary>
        private const string STR_KontrolletEAktivitetitUKaluanMeSukses = "Kontrollet e Aktivitetit u kaluan me sukses";
        /// <summary>
        /// mesazh kur mbushja perfundon me sukses
        /// </summary>
        public static string mbushjeSukses = "Aktivitet trupi u mbush me sukses";
        /// <summary>
        /// mesazh gabimi gjate marrjes se te dhenave
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se aktivitete trupi nga db-ja";
        /// <summary>
        /// mesazh gabimi kur nuk merren te dhenat
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye, sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id  ritese e trupit
        /// </summary>
        private int idTrupi;
        /// <summary>
        /// id e kokes
        /// </summary>
        private int idKoka;
        /// <summary>
        /// id e burimit
        /// </summary>
        private int idBurimi;
        /// <summary>
        /// koha e ekzekutimit te burimit
        /// </summary>
        private decimal koha;
        /// <summary>
        /// kodi i burimit
        /// </summary>
        private string kodBurimi;
        /// <summary>
        /// pershkrimi i burimit
        /// </summary>
        private string pershkrimBurimi;
        /// <summary>
        /// kostoja e burimit ne nje ore
        /// </summary>
        private decimal kostoBurimi;
        /// <summary>
        /// kostoja e burimit per kohen e ekzekutimit
        /// </summary>
        private decimal kosto;
        /// <summary>
        /// data e aktivizimit te ketij burimi
        /// </summary>
        private DateTime dtNdryshimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsAktiviteteTrupi()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idTrupi">id e trupit</param>
        /// <param name="idKoka"> id e kokes</param>
        /// <param name="idBurimi"> id e burimit</param>
        /// <param name="koha">koha e planifikuar</param>
        /// <param name="kosto">kosto e burimit per kete kohe</param>
        /// <param name="dtndryshimi">data e ndryshimit </param>
        public clsAktiviteteTrupi(int idTrupi, int idKoka, int idBurimi, decimal koha, decimal kosto, DateTime dtndryshimi)
        {
            this.idTrupi = idTrupi;
            this.idKoka = idKoka;
            this.idBurimi = idBurimi;
            this.koha = koha;
            this.kosto = kosto;
            dtNdryshimi = dtndryshimi;

        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idtrupi">id e trupit</param>
        /// <param name="idkoka"> id e kokes</param>
        /// <param name="idburimi"> id e burimit</param>
        /// <param name="koha">koha e planifikuar</param>
        /// <param name="kodburimi"> kodi i burimit </param>
        /// <param name="pershkrimburimi">pershkrimi i burimit</param>
        /// <param name="kostoburimi"> kostoja e burimit ne nje ore </param>
        /// <param name="kosto"> kostoja e burimit ne kohen e planifikuar</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="dtndryshimi"> data e ndryshimit</param>
        public clsAktiviteteTrupi(int idtrupi, int idkoka, int idburimi, decimal koha, string kodburimi, string pershkrimburimi, decimal kostoburimi, decimal kosto, int idndermarje, DateTime dtndryshimi)
        {
            try
            {
                idTrupi = idtrupi;
                idKoka = idkoka;
                idBurimi = idburimi;
                this.koha = koha;
                kodBurimi = kodburimi;
                pershkrimBurimi = pershkrimburimi;
                kostoBurimi = kostoburimi;
                this.kosto = kosto;
                dtNdryshimi = dtndryshimi;

                clsMesazh mesazh = kontrolloAktivitet(idndermarje);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idtrupi">id e trupit</param>
        public clsAktiviteteTrupi(int idtrupi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushAktivitetTrupi(db.ktheAktivitetTrupi(idtrupi));
            db.Dispose();
        }

        public clsAktiviteteTrupi(DataRow rreshti)
        {
            
            mbushAktivitetTrupi(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// id e trupit
        /// </summary>
        public int IdTrupi
        {
            get
            {
                return
                    idTrupi;
            }
            set
            {
                idTrupi = value;
            }
        }

        /// <summary>
        /// kodi i burimit
        /// </summary>
        public string KodBurimi
        {
            get
            {
                return kodBurimi;
            }
            set
            {
                kodBurimi = value;
            }

        }

        /// <summary>
        /// id e kokes
        /// </summary>
        public int IdKoka
        {
            get
            {
                return idKoka;
            }
            set
            {
                idKoka = value;
            }
        }

        /// <summary>
        /// id e burimit
        /// </summary>
        public int IdBurimi
        {
            get
            {
                return idBurimi;
            }
            set
            {
                idBurimi = value;
            }
        }

        /// <summary>
        /// kostoja e burimit ne kohen e planifikuar
        /// </summary>
        public decimal Kosto
        {
            get
            {
                return kosto;
            }
            set
            {
                kosto = value;
            }
        }

        /// <summary>
        /// kostoja e burimit ne nje ore
        /// </summary>
        public decimal KostoBurimi
        {
            get
            {
                return kostoBurimi;
            }
            set { kostoBurimi = value; }

        }

        /// <summary>
        /// koha e planifikuar e perdorimit te burimit gjate aktivitetit
        /// </summary>
        public decimal Koha
        {
            get
            {
                return koha;
            }
            set
            {
                koha = value;
            }
        }

        /// <summary>
        /// pershkrimi i burimit
        /// </summary>
        public string PershkrimBurimi
        {
            get
            {
                return pershkrimBurimi;
            }
            set
            {
                pershkrimBurimi = value;
            }
        }

        /// <summary>
        /// data e aktivizimit te ketij burimi
        /// </summary>
        public DateTime DtNdryshimi
        {
            get
            {
                return dtNdryshimi;
            }
            set
            {
                dtNdryshimi = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kontroll nese objekti i aktivitietit i ka te dhenat e sakta 
        /// </summary>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <returns> clsMesazh  me statusin nese te dhenat jane te sakta apo jo</returns>
        private clsMesazh kontrolloAktivitet(int idndermarje)
        {
            if (kodBurimi == "")
                return new clsMesazh(false, STR_PlotesoniBurimit);
            if (koha < 0)
                return new clsMesazh(false, STR_KohaDuhetTeJeteNumerPozitiv);
            if (!clsBurime.ekzistonBurim(kodBurimi, idndermarje))
                return new clsMesazh(false, STR_BurimiNukEkziston);
            return new clsMesazh(true, STR_KontrolletEAktivitetitUKaluanMeSukses);
        }

        public clsMesazh ruaj()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            clsMesazh mesazh = ruaj(db);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// ruan aktivitet trupin
        /// </summary>
        /// <param name="db"> clsDatabazeProdhimi per te qene pjese e trasaksionit</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo aktiviteti</returns>
        public clsMesazh ruaj(clsDatabazeProdhimi db)
        {
            clsMesazh mesazh = new clsMesazh();
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            int idtrupi = 0;
            mesazh = db.ruajAktiviteteTrupi(out idtrupi, idKoka, idBurimi, koha, dtNdryshimi);
            idTrupi = idtrupi;
            return mesazh;
        }

        /// <summary>
        /// modifikon aktivitetin
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo aktiviteti</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mesazh = db.modifikoAktiviteteTrupi(idTrupi, idKoka, idBurimi, koha, dtNdryshimi);
            db.Dispose();
            return mesazh;
        }

        public clsMesazh fshi()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            clsMesazh mesazh = fshi(db);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin aktivitet trupin ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="db">db nqs ben pjese ne nje transaksion</param>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(clsDatabazeProdhimi db)
        {
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            clsMesazh u_fshi = db.fshiAktiviteteTrupi(idTrupi);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin aktivitet trupi nga tabela perkatese ne databaze.
        /// </summary>
        public void merr()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();            
            db.ktheAktivitetTrupi(idTrupi);
            db.Dispose();
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush aktivitet trupi me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushAktivitetTrupi(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRow["IDBURIMI"].ToString(), out idBurimi);
                    decimal.TryParse(dbDataRow["KOHA"].ToString(), out     koha);
                    decimal.TryParse(dbDataRow["KOSTOPLAN"].ToString(), out kostoBurimi);
                    decimal.TryParse(dbDataRow["KOSTO"].ToString(), out kosto);
                    kodBurimi = dbDataRow["KODI"].ToString();
                    pershkrimBurimi = dbDataRow["EMERTIMI"].ToString();
                    DateTime.TryParse(dbDataRow["DTNDRYSHIMI"].ToString(), out dtNdryshimi);

                    return new clsMesazh(true, clsAktiviteteTrupi.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsAktiviteteTrupi.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsAktiviteteTrupi.drbosh);
        }

        #endregion
    }
}

