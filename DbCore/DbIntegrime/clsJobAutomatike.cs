using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbIntegrime
{

    public class clsJobAutomatike : IDataBase
    {
        #region Atribute

        private int idSkeduleri;
        private string emriJob;
        private bool webServis;
        private string oraFillimitJob;
        private string oraPerfundimitJob;
        private DateTime dataFillimitJob;
        private DateTime dataPerfundimitJob;
        private string ditetEkzekutimi;
        private int perseritja;
        private int hereTeEkzekutuara;
        private bool aktive;
        private DateTime dataFunditEkzekutuar;
        private string userJob;
        private string ndermarrjaJob;
        private bool digest;

        #endregion

        #region Properties
        public int IdSkeduleri
        {
            get { return idSkeduleri; }
            set { idSkeduleri = value; }
        }

        public string EmriJob
        {
            get { return emriJob; }
            set { emriJob = value; }
        }

        public bool WebServis
        {
            get { return webServis; }
            set { webServis = value; }
        }

        public string OraFillimitJob
        {
            get { return oraFillimitJob; }
            set { oraFillimitJob = value; }
        }

        public string OraPerfundimitJob
        {
            get { return oraPerfundimitJob; }
            set { oraPerfundimitJob = value; }
        }

        public DateTime DataFillimitJob
        {
            get { return dataFillimitJob; }
            set { dataFillimitJob = value; }
        }

        public DateTime DataPerfundimitJob
        {
            get { return dataPerfundimitJob; }
            set { dataPerfundimitJob = value; }

        }

        public string DitetEkzekutimi
        {
            get { return ditetEkzekutimi; }
            set { ditetEkzekutimi = value; }
        }

        public int Perseritja
        {
            get { return perseritja; }
            set { perseritja = value; }
        }

        public int HereTeEkzekutuara
        {
            get { return hereTeEkzekutuara; }
            set { hereTeEkzekutuara = value; }
        }

        public bool Aktive
        {
            get { return aktive; }
            set { aktive = value; }
        }

        public DateTime DataFunditEkzekutuar
        {
            get { return dataFunditEkzekutuar; }
            set { dataFunditEkzekutuar = value; }
        }

        public string UserJob
        {
            get { return userJob; }
            set { userJob = value; }
        }

        public string NdermarrjaJob
        {
            get { return ndermarrjaJob; }
            set { ndermarrjaJob = value; }
        }

        public bool Digest
        {
            get { return digest; }
            set { digest = value; }
        }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsJobAutomatike()
        {
        }

        public clsJobAutomatike(IDataRecord record)
        {
            Mbush(record);
        }

        #endregion

        #region MetodatPublike
        public clsMesazh Ruaj()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Modifiko()
        {
            try
            {
                using (var myScope = new MyTransactionScope())
                {
                    using (clsDatabaseIntegrime data = new clsDatabaseIntegrime())
                    {
                        var u_modifikua = data.modifikoJobAutomatike(idSkeduleri, hereTeEkzekutuara, aktive, dataFunditEkzekutuar);
                        if (u_modifikua) myScope.Complete();

                        return u_modifikua;
                    }
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new MesazhGabimi($"Job me id {idSkeduleri} nuk u modifikua me sukses!");
            }
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            int.TryParse(record["ID_SKEDULERI"].ToString(), out idSkeduleri);
            emriJob = record["EMRI_JOB"].ToString();
            bool.TryParse(record["WEBSERVIS"].ToString(), out webServis);
            oraFillimitJob = record["ORAFILLIMIT_JOB"].ToString();
            oraPerfundimitJob = record["ORAPERFUNDIMIT_JOB"].ToString();
            DateTime.TryParse(record["DATAFILLIMIT_JOB"].ToString(), out dataFillimitJob);
            DateTime.TryParse(record["DATAPERFUNDIMIT_JOB"].ToString(), out dataPerfundimitJob);
            ditetEkzekutimi = Convert.ToString(record["DITET_EKZEKUTIMI"]);
            int.TryParse(record["PERSERITJA"].ToString(), out perseritja);
            int.TryParse(record["HERE_TE_EKZEKUTUARA"].ToString(), out hereTeEkzekutuara);
            bool.TryParse(record["AKTIVE"].ToString(), out aktive);
            DateTime.TryParse(record["DATAFUNDIT_EKZEKUTUAR"].ToString(), out dataFunditEkzekutuar);
            userJob = record["USER_JOB"].ToString();
            ndermarrjaJob = record["NDERMARRJA_JOB"].ToString();
            bool.TryParse(record["DIGEST"].ToString(), out digest);
        }

        #endregion
    }

}
