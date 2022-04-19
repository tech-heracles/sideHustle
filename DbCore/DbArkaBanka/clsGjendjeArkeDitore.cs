using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbArkaBanka
{
    public class clsGjendjeArkeDitore : IDataBase
    {
        #region Atribute

        private int idGjendjeDitore;
        private int arka;
        private DateTime data;
        private double vlera;
        private int idNdermarrje;
        private int idStatusDok;
        private DateTime dateKrijimi;
        private DateTime dateModifikimi;
        private int idPerdoruesi;
        #endregion

        #region Properties

        public int IdGjendjeDitore
        {
            get { return idGjendjeDitore; }
            set { idGjendjeDitore = value; }
        }

        public int Arka
        {
            get { return arka; }
            set { arka = value; }
        }

        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public double Vlera
        {
            get { return vlera; }
            set { vlera = value; }
        }

        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public DateTime DateKrijimi
        {
            get { return dateKrijimi; }
            set { dateKrijimi = value; }
        }

        public DateTime DateModifikimi
        {
            get { return dateModifikimi; }
            set { dateModifikimi = value; }
        }

        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        #endregion

        #region Konstruktoret

        public clsGjendjeArkeDitore()
        {

        }

        public clsGjendjeArkeDitore(IDataRecord record)
        {
            Mbush(record);
        }

        public clsGjendjeArkeDitore(int idGjendjeDitore, int arka, DateTime data, double vlera, int idNdermarrje, int idStatusDok, int idPerdoruesi)
        {
            this.idGjendjeDitore = idGjendjeDitore;
            this.arka = arka;
            this.data = data;
            this.vlera = vlera;
            this.idNdermarrje = idNdermarrje;
            this.idStatusDok = idStatusDok;
            this.idPerdoruesi = idPerdoruesi;
        }

        public clsGjendjeArkeDitore(int idGjendjeDitore, double vlera, int idNdermarrje, int idStatusDok, int idPerdoruesi)
        {
            this.idGjendjeDitore = idGjendjeDitore;
            this.vlera = vlera;
            this.idNdermarrje = idNdermarrje;
            this.idStatusDok = idStatusDok;
            this.idPerdoruesi = idPerdoruesi;
        }
        #endregion

        #region Metoda Publike
        public clsMesazh Ruaj()
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                int id;
                clsMesazh u_ruajt = db.ruajGjendjeArkeDitore(out id, arka, data, vlera, idNdermarrje, idStatusDok, idPerdoruesi);
                this.idGjendjeDitore = id;
                return u_ruajt;
            }
        }

        public clsMesazh Modifiko()
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                clsMesazh u_modifikua = db.modifikoGjendjeArkeDitore(idGjendjeDitore, vlera, idNdermarrje, idStatusDok, idPerdoruesi);
                return u_modifikua;
            }
        }

        public clsMesazh Fshi()
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                clsMesazh u_fshi = db.fshiGjendjeArkeDitore(idGjendjeDitore);
                return u_fshi;
            }
        }

        public static DataRow merrGjendjeDitoreSipasIdDR(int idGjendjeDitore)
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                DataRow dataRow = db.ktheGjendjeDitoreSipasIdDR(idGjendjeDitore);
                return dataRow;
            }
        }

        public static DataTable merrGjendjeArkeDitoreSipasNderm(int idNdermarrje)
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                DataTable dataTable = db.ktheGjendjeArkeDitoreSipasNdermarrje(idNdermarrje);

                return dataTable;
            }
        }

        public void Mbush(IDataRecord record)
        {
            try
            {
                int.TryParse(record["IDGJENDJEDITORE"].ToString(), out idGjendjeDitore);
                int.TryParse(record["ARKA"].ToString(), out arka);
                DateTime.TryParse(record["DATA"].ToString(), out data);
                double.TryParse(record["VLERA"].ToString(), out vlera);
                int.TryParse(record["IDNDERMARRJE"].ToString(), out idNdermarrje);
                int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
                DateTime.TryParse(record["DATEKRIJIMI"].ToString(), out dateKrijimi);
                DateTime.TryParse(record["DATEMODIFIKIMI"].ToString(), out dateModifikimi);
                int.TryParse(record["IDPERDORUESI"].ToString(), out idPerdoruesi);
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                throw new MyException("ERROR: Gabim gjate marrjes se te dhenave nga databaza!");
            }
        }
        public static bool merrGjendjeDitoreSipasIdArkeDheDitesSot(int idArke, DateTime dataSot)
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                DataRow dataRow = db.ktheGjendjeDitoreSipasIdArke(idArke, dataSot);
                string data = "";
                if (dataRow != null)
                    data = dataRow.ItemArray[2].ToString();
                var dataTani = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).ToString();
                if (dataRow == null || (data != dataSot.ToString()))
                    return false;
                else
                    return true;
            }
        }
        #endregion
    }
}
