using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class clsKonfigurimKase
    {
        #region Attribute

        private int idKonfigurimi;
        private string pershkrimi;
        private int idNdermarje;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string kodi;
        private int lloji;
        private string skema;
        private colVleratKonfigurimiKasa oColVlerat;
        private DataRow rreshti;

        #endregion

        #region Properties

        public int IdKonfigurimi
        {
            get
            {
                return idKonfigurimi;
            }
            set
            {
                idKonfigurimi = value;
            }
        }

        public string Pershkrimi
        {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }
        }

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

        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        public colVleratKonfigurimiKasa OColVlerat
        {
            get
            {
                return oColVlerat;
            }
            set
            {
                oColVlerat = value;
            }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        public int Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }

        public string Skema
        {
            get { return skema; }
            set { skema = value; }
        }

        #endregion

        #region konstruktoret
        public clsKonfigurimKase()
        {
        }
        public clsKonfigurimKase(int idkonfigurimi, string pershkrimi, int idndermarje, int idstatusdok, int idperdorues, string kodi, int lloji, string skema)
        {
            this.idKonfigurimi = idkonfigurimi;
            this.pershkrimi = pershkrimi;
            this.idNdermarje = idndermarje;
            this.idStatusDok = idstatusdok;
            this.idPerdoruesi = idperdorues;
            this.kodi = kodi;
            this.lloji = lloji;
            this.skema = skema;
        }

        public clsKonfigurimKase(int idkonfigurimi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            if (!this.mbushKonfigurimKase(data.merrKonfigurimKase(idkonfigurimi)))
            {
                data.Dispose();
                return;
            }
            oColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa(idkonfigurimi);
            data.Dispose();
        }

        public clsKonfigurimKase(DataRow rreshti)
        {
            
            mbushKonfigurimKase(rreshti);
        }

        #endregion
        
        #region Metoda Internal
        #region komentuar
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //internal DataRow mbushKonfigurimKase()
        //{
        //    try
        //    {
        //        DataTable kasa = new DataTable("kasa");

        //        kasa.Columns.Add("IDKONFIGURIMI", (new System.Decimal()).GetType());
        //        kasa.Columns.Add("PERSHKRIMI", ("").GetType());
        //        kasa.Columns.Add("IDNDERMARJE", (new System.Decimal()).GetType());
        //        kasa.Columns.Add("IDPERDORUESI", (new System.Decimal()).GetType());
        //        kasa.Columns.Add("IDSTATUSDOK", (new System.Decimal()).GetType());
        //        kasa.Columns.Add("DTKRIJIMI", (new System.DateTime()).GetType());
        //        kasa.Columns.Add("DTMODIFIKIMI", (new System.DateTime()).GetType());
        //        DataRow rreshti = kasa.NewRow();

        //        rreshti["IDKONFIGURIMI"] = this.idKonfigurimi;
        //        rreshti["PERSHKRIMI"] = this.pershkrimi;
        //        rreshti["IDNDERMARJE"] = this.idNdermarje;
        //        rreshti["IDPERDORUESI"] = this.idPerdoruesi;
        //        rreshti["IDSTATUSDOK"] = this.idStatusDok;
        //        rreshti["DTKRIJIMI"] = this.dtKrijimi;
        //        rreshti["DTMODIFIKIMI"] = this.dtModifikimi;

        //        return rreshti;
        //    }
        //    catch (Exception)
        //    {
        //        return null;

        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="roli"></param>
        ///// <returns></returns>
        //internal DataRow mbushKonfigurimKase(clsKonfigurimKase kasat)
        //{

        //    try
        //    {
        //        DataTable kasa = new DataTable("kasa");
        //        kasa.Columns.Add("IDKONFIGURIMI", (new System.Decimal()).GetType());
        //        kasa.Columns.Add("PERSHKRIMI", ("").GetType());
        //        kasa.Columns.Add("IDNDERMARJE", (new System.Decimal()).GetType());
        //        kasa.Columns.Add("IDPERDORUESI", (new System.Decimal()).GetType());
        //        kasa.Columns.Add("IDSTATUSDOK", (new System.Decimal()).GetType());
        //        kasa.Columns.Add("DTKRIJIMI", (new System.DateTime()).GetType());
        //        kasa.Columns.Add("DTMODIFIKIMI", (new System.DateTime()).GetType());

        //        DataRow rreshti = kasa.NewRow();
        //        rreshti["IDKONFIGURIMI"] = kasat.idKonfigurimi;
        //        rreshti["PERSHKRIMI"] = kasat.pershkrimi;
        //        rreshti["IDNDERMARJE"] = kasat.idNdermarje;
        //        rreshti["IDPERDORUESI"] = kasat.idPerdoruesi;
        //        rreshti["IDSTATUSDOK"] = kasat.idStatusDok;
        //        rreshti["DTKRIJIMI"] = kasat.dtKrijimi;
        //        rreshti["DTMODIFIKIMI"] = kasat.dtModifikimi;

        //        return rreshti;
        //    }
        //    catch (Exception)
        //    {
        //        return null;

        //    }
        //}
        #endregion
        /// <summary>
        /// mbush objektin nga nje datarow i marr nga db-ja
        /// </summary>
        /// <param name="rreshti">datarow me te dhenat e te drejtes</param>
        /// <returns>True nese te dhenat merren me sukses, False perndryshe</returns>
        internal bool mbushKonfigurimKase(DataRow rreshti)
        {
            try
            {
                this.idKonfigurimi = Convert.ToInt32(rreshti["IDKONFIGURIMI"]);
                this.pershkrimi = Convert.ToString(rreshti["PERSHKRIMI"]);
                this.idNdermarje = Convert.ToInt32(rreshti["IDNDERMARJE"]);
                int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                this.kodi = rreshti["KODI"].ToString();
                int.TryParse(rreshti["LLOJI"].ToString(), out lloji);
                this.skema = rreshti["SKEMA"].ToString();
            }
            catch (Exception)
            {
                return false;

            }
            return true;

        }
        #endregion

        #region Metoda Publike

        public void merrKonfiguriminSipasNdermarjes(int idNdermarrje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            if (this.mbushKonfigurimKase(data.merrKonfigurimKaseSipasNdermarjes(idNdermarrje)))
                oColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa(idKonfigurimi);
            data.Dispose();
        }

        public static clsMesazh fshikonfigurimkaseStatus(int idKonfigurimi, int idperdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.fshiKonfigurimKaseStatus(idKonfigurimi, idperdoruesi);
            }
        }

        public clsMesazh fshiKonfigurimKase(int idndermarje, clsDatabaseAdmin data)
        {
            return data.fshiKonfigurimKase(idndermarje);
        }

        public clsMesazh ruajKonfigurimKase(clsDatabaseAdmin data)
        {

            return data.ruajKonfigurimKase(out idKonfigurimi, this.pershkrimi, this.idNdermarje, this.idStatusDok, this.idPerdoruesi, this.kodi, this.lloji, this.skema);
        }
        public clsMesazh ruajKonfigurimKase(clsDatabaseAdmin data, int idkonfigVjeter)
        {
            clsMesazh mesazh = new clsMesazh();
            mesazh = data.ruajKonfigurimKase(out idKonfigurimi, this.pershkrimi, this.idNdermarje, this.idStatusDok, this.idPerdoruesi, this.kodi, this.lloji, this.skema);
            mesazh = data.modifikoVlereDefaultKonfigKasa(idKonfigurimi, idkonfigVjeter, this.idNdermarje);
            mesazh = data.modifikoIdKonfigurimKaseTePerdoruesit(idKonfigurimi, idkonfigVjeter);
            return mesazh;
        }
        public clsMesazh modifikoKonfigurimKase(clsDatabaseAdmin data)
        {
            return data.modifikoKonfigurimKase(this.idKonfigurimi, this.pershkrimi, this.idNdermarje, this.idStatusDok, this.idPerdoruesi, this.kodi, this.lloji, this.skema);
        }
        
        public static bool ekzistonKonfigurimPerNdermarrjen(int idndermarje, String kodi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = data.ekzistonKonfigurimKaseMeKeteKodPerKeteNdermarje(idndermarje, kodi);
            data.Dispose();
            return sukses;
        }

        public clsMesazh ruajKonfigurim(bool eshteShtim, int idKonfigurimi = 0)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            data.beginTransaksion();
            clsMesazh mesazh = new clsMesazh(true);
            if (!eshteShtim)
                mesazh = fshikonfigurimkaseStatus(idKonfigurimi, this.IdPerdoruesi);
            if (!mesazh.Status)
            {
                data.rollbackTransaksion();
                return mesazh;
            }
            mesazh = eshteShtim ? this.ruajKonfigurimKase(data) : this.ruajKonfigurimKase(data, idKonfigurimi);
            if (!mesazh.Status)
            {
                data.rollbackTransaksion();
                return mesazh;
            }
            foreach (clsVleraKonfigurimiKasa v in oColVlerat)
            {
                v.IdKonfigurimi = this.idKonfigurimi;
                mesazh = v.ruajVleraKonfigurimKase(data);
                if (!mesazh.Status)
                {
                    data.rollbackTransaksion();
                    return mesazh;
                }
            }
            data.commitTransaksion();
            return mesazh;
        }

        public clsMesazh modifikoKonfigurim()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            data.beginTransaksion();
            clsMesazh mesazh = modifikoKonfigurimKase(data);
            if (!mesazh.Status)
            {
                data.rollbackTransaksion();
                return mesazh;
            }
            foreach (clsVleraKonfigurimiKasa v in oColVlerat)
            {
                v.IdKonfigurimi = this.idKonfigurimi;
                mesazh = v.ruajVleraKonfigurimKase(data);
                if (!mesazh.Status)
                {
                    data.rollbackTransaksion();
                    return mesazh;
                }
            }
            data.commitTransaksion();
            return mesazh;
        }

        /// <summary>
        /// Kthen skemen e barkodit per peshoren e pare qe gjendet te konfigurimet e peshoreve
        /// </summary>
        /// <param name="idNDermarrje"></param>
        /// <returns></returns>
        public static string merrSkemeBarkodiPershoreje(int idNdermarrje)
        {
            using(clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.ktheSkemeBarkodiPershoreje(idNdermarrje);
            }
        }
        
        #endregion
    }
}
