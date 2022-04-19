using System;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class clsEmail
    {
        private int idEmail;
        private int idNdermarrje;
        private int nrProcesi;
        private string marresEmail;
        private DateTime dateDergimi;
        private DateTime datePergjigje;
        private statusEmail status;
        private string subjektEmail;
        private string trupEmail;
        private DateTime dateKujtese;
        private string pathi;
        private int idPerdoruesi;
        //private bool perWF;

        #region
        public string Pathi
        {
            get { return pathi; }
        }
        public DateTime DateKujtese
        {
            get
            {
                return dateKujtese;
            }
            set
            {
                if (dateKujtese == value)
                    return;
                dateKujtese = value;
            }
        }
        public int IdEmail
        {
            get
            {
                return idEmail;
            }
        }
        public int IdNdermarrje
        {
            get
            {
                return idNdermarrje;
            }
        }
        public int NrProcesi
        {
            get
            {
                return nrProcesi;
            }
        }
        public string MarresEmail
        {
            get
            {
                return marresEmail;
            }
        }
        public DateTime DateDergimi
        {
            get
            {
                return dateDergimi;
            }
        }
        public DateTime DatePergjigje
        {
            get
            {
                return datePergjigje;
            }
            set
            {
                if (datePergjigje == value)
                    return;
                datePergjigje = value;
            }
        }
        public statusEmail Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status == value)
                    return;
                status = value;
            }
        }
        public string SubjektEmail
        {
            get
            {
                return subjektEmail;
            }
        }
        public string TrupEmail
        {
            get
            {
                return trupEmail;
            }
        }
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        #endregion

        public clsEmail(int idEmail)
        {
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                mbushEmail(data.ktheEmail(idEmail));
            }
        }
        public clsEmail(DataRow row)
        {
            mbushEmail(row);
        }
        public clsEmail(int idNdermarrje, int nrProcesi, string marresEmail, DateTime dateDergimi, string subjektEmail, string trupEmail, int idPerdoruesi)
        {
            this.idNdermarrje = idNdermarrje;
            this.nrProcesi = nrProcesi;
            this.marresEmail = marresEmail;
            this.dateDergimi = dateDergimi;
            this.subjektEmail = subjektEmail;
            this.trupEmail = trupEmail;
            this.status = statusEmail.perDergim;
            this.dateKujtese = dateDergimi;
            this.idPerdoruesi = idPerdoruesi;
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                data.krijoEmail(out this.idEmail, idNdermarrje, nrProcesi, marresEmail, dateDergimi, subjektEmail, trupEmail, this.status, this.dateKujtese, this.idPerdoruesi);
            }
            if (this.IdEmail == 0) throw new MyException("Gabim gjate loggimit te email-it");
        }

        /// <summary>
        /// konstruktori per dergim email me attach
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="marresEmail"></param>
        /// <param name="dateDergimi"></param>
        /// <param name="subjektEmail"></param>
        /// <param name="trupEmail"></param>
        /// <param name="pathi"></param>
        public clsEmail(int idNdermarrje, string marresEmail, DateTime dateDergimi, string subjektEmail, string trupEmail, string pathi, int idPerdoruesi)
        {
            this.idNdermarrje = idNdermarrje;
            this.marresEmail = marresEmail;
            this.dateDergimi = dateDergimi;
            this.subjektEmail = subjektEmail;
            this.trupEmail = trupEmail;
            this.status = statusEmail.perDergim;
            this.dateKujtese = dateDergimi;
            this.pathi = pathi;
            this.idPerdoruesi = idPerdoruesi;
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                data.krijoEmail(out this.idEmail, idNdermarrje, marresEmail, dateDergimi, subjektEmail, trupEmail, this.status, this.dateKujtese, pathi, idPerdoruesi);
            }
            if (this.IdEmail == 0) throw new MyException("Gabim gjate loggimit te email-it");
        }

        /// <summary>
        /// konstruktori per njoftim dokumentash
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="marresEmail"></param>
        /// <param name="dateDergimi"></param>
        /// <param name="subjektEmail"></param>
        /// <param name="trupEmail"></param>
        public clsEmail(int idNdermarrje, string marresEmail, DateTime dateDergimi, string subjektEmail, string trupEmail, int idPerdoruesi)
        {
            this.idNdermarrje = idNdermarrje;
            this.marresEmail = marresEmail;
            this.dateDergimi = dateDergimi;
            this.subjektEmail = subjektEmail;
            this.trupEmail = trupEmail;
            this.status = statusEmail.perDergim;
            this.dateKujtese = dateDergimi;
            this.idPerdoruesi = idPerdoruesi;
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                data.krijoEmail(out this.idEmail, idNdermarrje, marresEmail, dateDergimi, subjektEmail, trupEmail, this.status, this.dateKujtese, idPerdoruesi);
            }
            if (this.IdEmail == 0) throw new MyException("Gabim gjate loggimit te email-it");
        }

        public clsEmail()
        {
            
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idEmail"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool ndryshoStatus(int idEmail, statusEmail status, string mesazhPergjigjeEmail)
        {
            try
            {
                using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
                {
                    return data.ndryshoStatus(idEmail, status, mesazhPergjigjeEmail);
                }
            }
            catch (Exception)
            {

                return false;
            }

        }



        internal bool mbushEmail(DataRow dbRow)
        {
            if (dbRow != null)
            {
                try
                {
                    idEmail = Convert.ToInt32(dbRow["idEmail"]);
                    idNdermarrje = Convert.ToInt32(dbRow["idNdermarrje"]);
                    nrProcesi = Convert.ToInt32(dbRow["nrProcesi"]);
                    marresEmail = Convert.ToString(dbRow["marresEmail"]);
                    DateTime.TryParse(dbRow["dateDergimi"].ToString(), out dateDergimi);
                    DateTime.TryParse(dbRow["datePergjigje"].ToString(), out datePergjigje);
                    status = (statusEmail)Convert.ToInt32(dbRow["status"]);
                    subjektEmail = Convert.ToString(dbRow["subjektEmail"]);
                    trupEmail = Convert.ToString(dbRow["trupEmail"]);
                    DateTime.TryParse(dbRow["dateKujtese"].ToString(), out dateKujtese);
                    pathi = Convert.ToString(dbRow["pathi"]);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se etapes nga db-ja");
                }
            }
            else
                return false;
        }

        internal static string merrPathRaport(int idEmail)
        {
            try
            {
                using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
                {
                    return data.kthePathRaport(idEmail);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}


