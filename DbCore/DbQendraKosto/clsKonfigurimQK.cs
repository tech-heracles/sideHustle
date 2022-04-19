using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje burim
    ///  (Te dhenat  merren nga tabela : T_KONFIGURIMQK)
    /// </summary>
    public class clsKonfigurimQK
    {
        /// <summary>
        /// mesazh kur konfigurimi u mbush me sukses
        /// </summary>
        public static string mbushjeSukses = "Konfigurimi qendra kosto u mbush me sukses";
        /// <summary>
        /// mesazh gabimi kur merren te dhenat
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se konfigurimit qendra kosto nga db-ja";
        /// <summary>
        /// mesazh gabimi kur nuk merret asnje e dhene
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id e konfigurimi qk
        /// </summary>
        private int idKonfigurimQK;
        /// <summary>
        /// prioritet 1
        /// </summary>
        private int prioriteti1;
        /// <summary>
        /// prioritet 2
        /// </summary>
        private int prioriteti2;
        /// <summary>
        /// prioritet 3
        /// </summary>
        private int prioriteti3;
        /// <summary>
        /// prioriteti 4
        /// </summary>
        private int prioriteti4; 
        /// <summary>
        /// prioriteti 5
        /// </summary>
        private int prioriteti5;
        /// <summary>
        /// id e menyre mesazhi
        /// </summary>
        private int idMenyreMesazhi;
        /// <summary>
        /// id e ndermarjes
        /// </summary>
        private int idNdermarje;
        /// <summary>
        /// id e perdoruesit
        /// </summary>
        private int idPerdoruesi;

        private int idKrijuesi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKonfigurimQK()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idkonfigurim">id e llogarise</param>
        /// <param name="idmenyremesazhi"> id e kpf</param>
        /// <param name="idPerdoruesi"> id e perdoruesit</param>
        /// <param name="idNdermarje">id e ndermarjes</param> 
        public clsKonfigurimQK(int idkonfigurim, int prioriteti1, int prioriteti2, int prioriteti3, int prioriteti4,int prioriteti5, int idmenyremesazhi, int idPerdoruesi, int idNdermarje, int idkrijuesi)
        {
            idKonfigurimQK = idkonfigurim;
            this.idMenyreMesazhi = idmenyremesazhi;
            this.prioriteti1 = prioriteti1;
            this.prioriteti2 = prioriteti2;
            this.prioriteti3 = prioriteti3;
            this.prioriteti4 = prioriteti4;
            this.prioriteti5 = prioriteti5;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idKrijuesi = idkrijuesi;

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id </param>
        public clsKonfigurimQK(int id)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushKonfigurim(db.ktheKonfigurimQKSipasId(id));
            db.Dispose();
        }
        public clsKonfigurimQK(int idndermarje, int idperdoruesi,clsDatabaseQendraKosto db)
        {
            mbushKonfigurim(db.TransCache.GetKonfigurimQKNdermarje(idndermarje, idperdoruesi, db));

        }
        public clsKonfigurimQK(DataRow rreshti)
        {
            
            mbushKonfigurim(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// id e ritese e konfigurimit qk
        /// </summary>
        public int IdKonfigurimQK
        {
            get
            {
                return
                    idKonfigurimQK;
            }
            set
            {
                idKonfigurimQK = value;
            }
        }
        public int IdKrijuesi
        {
            get
            {
                return idKrijuesi;
            }
            set
            {
                idKrijuesi = value;
            }
        }
        /// <summary>
        ///  id e menyre mesazhi
        /// </summary>
        public int IdMenyreMesazhi
        {
            get
            {
                return idMenyreMesazhi;
            }
            set
            {
                idMenyreMesazhi = value;
            }
        }
        /// <summary>
        /// id e ndermarjes
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
        /// id e perdoruesit qe ka kryer veprimin
        /// </summary>
        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }

        /// <summary>
        /// prioritet 1
        /// </summary>
        public int Prioriteti1
        {
            get
            {
                return prioriteti1;
            }
            set
            {
                prioriteti1 = value;
            }
        }
        /// <summary>
        /// prioritet 2
        /// </summary>
        public int Prioriteti2
        {
            get
            {
                return prioriteti2;
            }
            set
            {
                prioriteti2 = value;
            }
        }
        /// <summary>
        /// prioritet 3
        /// </summary>
        public int Prioriteti3
        {
            get
            {
                return prioriteti3;
            }
            set
            {
                prioriteti3 = value;
            }
        }
        /// <summary>
        /// prioriteti 4
        /// </summary>
        public int Prioriteti4
        {
            get
            {
                return prioriteti4;
            }
            set
            {
                prioriteti4 = value;
            }
        }

        public int Prioriteti5
        {
            get
            {
                return prioriteti5;
            }

            set
            {
                prioriteti5 = value;
            }
        }
        #endregion

        #region Metoda Publike


        /// <summary>
        /// ruan konfigurimin e qk nqs ekziston e fshin te vjetrin dhe ruan te riun
        /// </summary>
        /// <returns>cls mesazh qe tregon nqs eshte ruajtur apo jo </returns>
        public clsMesazh ruaj()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            db.beginTransaksion();
            clsMesazh mesazh = new clsMesazh(true);
            if (prioriteti1 == prioriteti2 || prioriteti1 == prioriteti3 || prioriteti1 == prioriteti4 || prioriteti2 == prioriteti3 || prioriteti2 == prioriteti4 || prioriteti3 == prioriteti4)
            {
                mesazh = new clsMesazh(false, "Dy ose me shume prioritete kane te njejten vlere!");
                db.rollbackTransaksion();
                return mesazh;
            }

            mesazh = fshi(this.idNdermarje, this.idPerdoruesi, db);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;

            }

            mesazh = ruaj(db);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;

            }

            db.commitTransaksion();
            return mesazh;
        }

        /// <summary>
        /// ruan konfigurimin
        /// </summary>
        /// <param name="db"> clsDatabaseQendraKosto per te qene pjese e trasaksionit</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo llogaria</returns>
        public clsMesazh ruaj(clsDatabaseQendraKosto db)
        {
            clsMesazh mesazh = new clsMesazh();
            int idllog = 0;
            mesazh = db.ruajKonfigurimQK(out idllog, prioriteti1, prioriteti2, prioriteti3, prioriteti4, idMenyreMesazhi, idNdermarje, idPerdoruesi, idKrijuesi,prioriteti5);
            idKonfigurimQK = idllog;
            return mesazh;
        }

        /// <summary>
        /// modifikon konfigurimin
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo llogaria</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mesazh = db.modifikoKonfigurimQK(idKonfigurimQK, prioriteti1, prioriteti2, prioriteti3, prioriteti4, idMenyreMesazhi, idNdermarje, idPerdoruesi, idKrijuesi, prioriteti5);
            db.Dispose();
            return mesazh;
        }

        public clsMesazh fshi()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            clsMesazh mesazh = fshi(db);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin konfigurim ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="db">db nqs ben pjese ne nje transaksion</param>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(clsDatabaseQendraKosto db)
        {

            clsMesazh u_fshi = db.fshiKonfigurimQK(idKonfigurimQK);
            return u_fshi;
        }
        public clsMesazh fshi(int idndermarje,int idperdoruesi, clsDatabaseQendraKosto db)
        {

            clsMesazh u_fshi = db.fshiKonfigurimQKSipasIdNdermarje(idndermarje, idperdoruesi);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin konfigurimin nga tabela perkatese ne databaze.
        /// </summary>
        public void merr(int id)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushKonfigurim(db.ktheKonfigurimQKSipasId(id));
            db.Dispose();
        }
        /// <summary>
        /// Merr objektin konfigurim nga tabela perkatese ne databaze sipas id ndermarje.
        /// </summary>
        public void merrSipasIdNdermarje(int idndermarje, int idperdoruesi)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            merrSipasIdNdermarje(idndermarje,idperdoruesi, db);
            db.Dispose();
        }
        public void merrSipasIdNdermarje(int idndermarje, int idperdoruesi, clsDatabaseQendraKosto db)
        {

            mbushKonfigurim(db.ktheKonfigurimQKSipasNdermarjes(idndermarje,idperdoruesi));

        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush Konfigurimin me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushKonfigurim(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDKONFIGURIMQK"].ToString(), out idKonfigurimQK);
                    int.TryParse(dbDataRow["IDMENYREMESAZHI"].ToString(), out idMenyreMesazhi);
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["PRIORITETI1"].ToString(), out prioriteti1);
                    int.TryParse(dbDataRow["PRIORITETI2"].ToString(), out prioriteti2);
                    int.TryParse(dbDataRow["PRIORITETI3"].ToString(), out prioriteti3);
                    int.TryParse(dbDataRow["PRIORITETI4"].ToString(), out prioriteti4);
                    int.TryParse(dbDataRow["PRIORITETI5"].ToString(), out prioriteti5);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDKRIJUESI"].ToString(), out idKrijuesi);

                    return new clsMesazh(true, clsKonfigurimQK.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsKonfigurimQK.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsKonfigurimQK.drbosh);
        }
        internal void mbushKonfigurim(clsKonfigurimQK konf)
        {
            this.idKonfigurimQK = konf.idKonfigurimQK;
            this.idMenyreMesazhi = konf.idMenyreMesazhi;
            this.IdPerdoruesi = konf.IdPerdoruesi;
            this.prioriteti1 = konf.prioriteti1;
            this.prioriteti2 = konf.prioriteti2;
            this.prioriteti3 = konf.prioriteti3;
            this.prioriteti4 = konf.prioriteti4;
            this.prioriteti5 = konf.prioriteti5;
            this.IdNdermarje = konf.IdNdermarje;
            this.idKrijuesi = konf.idKrijuesi;
                
        }

        #endregion
    }
}
