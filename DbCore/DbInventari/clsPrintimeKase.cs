using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbInventari
{
    public class clsPrintimeKase
    {
        #region Atribute

        private int idKasa;
        private int idShitje;
        private bool printuar;
        private bool derguar;
        private string ip;
        private int idShop;
        private string pershkrimi;
        private DateTime dataPrintimit;
        private int idUser;
        private int idBanka;
        private DataRow rreshti;

        #endregion

        #region Properties

        public int IdBanka
        {
            get
            {
                return idBanka;
            }
            set
            {
                idBanka = value;
            }
        }
        public int IdKasa
        {
            get { return idKasa; }
            set { idKasa = value; }
        }

        public int IdShitje
        {
            get { return idShitje; }
            set { idShitje = value; }
        }

        public bool Printuar
        {
            get { return printuar; }
            set { printuar = value; }
        }

        public bool Derguar
        {
            get { return derguar; }
            set { derguar = value; }
        }

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public int IdShop
        {
            get { return idShop; }
            set { idShop = value; }
        }

        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        public DateTime DataPrintimit
        {
            get { return dataPrintimit; }
            set { dataPrintimit = value; }
        }

        public int IdUser
        {
            get { return idUser; }
            set { idUser = value; }
        }

        #endregion

        #region Konstruktori

        public clsPrintimeKase()
        {
        }

        public clsPrintimeKase(int idShitje, bool printuar, bool derguar, string ip, int idShop, string pershkrimi, DateTime dataPrintimit, int idUser, int idbanka)
        {
            this.idShitje = idShitje;
            this.printuar = printuar;
            this.derguar = derguar;
            this.ip = ip;
            this.idShop = idShop;
            this.pershkrimi = pershkrimi;
            this.dataPrintimit = dataPrintimit;
            this.idUser = idUser;
            this.idBanka = idbanka;
        }

        public clsPrintimeKase(DataRow rreshti)
        {
            mbushPrintimNeKase(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// Mbushja me dokumentat e printuar ose jo ne kase
        /// </summary>
        /// <param name="dbDataRowPrintimeKase">DataRow qe duhet mbushur nga databaza</param>
        /// <returns>Kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushPrintimNeKase(DataRow dbDataRowPrintimeKase)
        {

            if (dbDataRowPrintimeKase != null)
            {

                try
                {

                    int.TryParse(dbDataRowPrintimeKase["ID_KASA"].ToString(), out idKasa);
                    int.TryParse(dbDataRowPrintimeKase["SHITJE_ID"].ToString(), out idShitje);
                    int.TryParse(dbDataRowPrintimeKase["BANKAID"].ToString(), out idBanka);
                    bool.TryParse(dbDataRowPrintimeKase["PRINTUAR"].ToString(), out printuar);
                    bool.TryParse(dbDataRowPrintimeKase["DERGUAR"].ToString(), out derguar);
                    ip = dbDataRowPrintimeKase["IP"].ToString();
                    int.TryParse(dbDataRowPrintimeKase["ID_SHOP"].ToString(), out idShop);
                    pershkrimi = dbDataRowPrintimeKase["PERSHKRIMI"].ToString();
                    DateTime.TryParse(dbDataRowPrintimeKase["DATA_PRINTIMIT"].ToString(), out dataPrintimit);
                    int.TryParse(dbDataRowPrintimeKase["ID_USER"].ToString(), out idUser);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se dokumentave te printuar ne kase nga db-ja.");
                }
            }
            else
                return false;

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Kryen ruajtjen e nje regjistrimi te ri qe dergohet per kase
        /// </summary>
        /// <returns>Kthen nje mesazh nese eshte kryer regjistrimi apo jo dhe nje pershkrim mbi errorin ose suksesin</returns>
        public clsMesazh ruajPrintimKase()
        {
            idKasa = -1;
            clsDatabaseInventari dbPrintimKase = new clsDatabaseInventari();
            dbPrintimKase.merrManager();
            dbPrintimKase.beginTransaksion();
            try
            {
                this.IdKasa = dbPrintimKase.ruajDergimiKase(idKasa, this.IdShitje, this.Printuar, this.Derguar, this.Ip, this.IdShop, this.Pershkrimi, this.DataPrintimit, this.IdUser, this.idBanka);
                if (this.IdKasa == -1)
                {
                    dbPrintimKase.rollbackTransaksion();
                    return new clsMesazh(false, "Gabim gjate ruajtjes se printimit ne kase!");
                }
                dbPrintimKase.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
            catch (Exception ce)
            {
                dbPrintimKase.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Kryen ruajtjen e nje regjistrimi te ri qe dergohet per kase
        /// </summary>
        /// <returns>Kthen nje mesazh nese eshte kryer regjistrimi apo jo dhe nje pershkrim mbi errorin ose suksesin</returns>
        public clsMesazh modifikoPrintimKase()
        {
            clsDatabaseInventari dbPrintimKase = new clsDatabaseInventari();
            dbPrintimKase.merrManager();
            dbPrintimKase.beginTransaksion();
            try
            {
                clsMesazh mesazhi = dbPrintimKase.modifikoDergimiKase(this.IdKasa, this.IdShitje, this.Printuar, this.Derguar, this.Ip, this.IdShop, this.Pershkrimi, this.DataPrintimit, this.IdUser);
                if (!mesazhi.Status)
                {
                    dbPrintimKase.rollbackTransaksion();
                    return new clsMesazh(false, "Gabim gjate modifikimit te printimit ne kase!");
                }
                dbPrintimKase.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]); 
            }
            catch (Exception ce)
            {
                dbPrintimKase.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Merr regjistrimin sipas nje id te shitjes.
        /// </summary>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <returns>Kthen true nese e mbush nje objekt dhe anasjelltas</returns>
        public bool merrDergimeKaseSipasIdShitje(int idShitje)
        {
            clsDatabaseInventari dbPrintime = new clsDatabaseInventari();
            DataRow tabela = dbPrintime.ktheDergimeKaseSipasIdShitje(idShitje);
            dbPrintime.Dispose();
            return mbushPrintimNeKase(tabela);
        }

        public bool ktheDergimeKaseSipasIdBanka(int idBanka)
        {
            clsDatabaseInventari dbPrintime = new clsDatabaseInventari();
            DataRow tabela = dbPrintime.ktheDergimeKaseSipasIdBanka(idBanka);
            dbPrintime.Dispose();
            return mbushPrintimNeKase(tabela);
        }
        public bool merrDergimeKaseSipasIdDok(int idDok,string lloji)
        {

            using (clsDatabaseInventari dbPrintime = new clsDatabaseInventari())
            {
                DataRow dr;
                switch (lloji)
                {
                    case "shitje":
                        dr = dbPrintime.ktheDergimeKaseSipasIdShitje(idDok);
                        break;
                    case "arka":
                        dr = dbPrintime.ktheDergimeKaseSipasIdBanka(idDok);
                        break;
                    default: throw new MyException("lloj i papercaktuar");
                }
                return mbushPrintimNeKase(dr);
            }
        }
        /// <summary>
        /// Merr regjistrimin sipas id se dokumentit te shitjes dhe id se dyqanit
        /// </summary>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <param name="idShop">Id e dyqanit</param>
        /// <returns>Kthen true nese e mbush nje objekt dhe anasjelltas</returns>
        public bool merrDergimeKaseSipasIdShitjeDheIdShop(int idShitje, int idShop)
        {
            clsDatabaseInventari dbPrintime = new clsDatabaseInventari();
            DataRow tabela = dbPrintime.ktheDergimeKaseSipasIdShitjeDheIdShop(idShitje, idShop);
            dbPrintime.Dispose();
            return mbushPrintimNeKase(tabela);
        }

        /// <summary>
        /// Merr regjistrimin sipas id se dokumentit te shitjes dhe statusit te printimit
        /// </summary>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <param name="statusiPrintimi">Nese kerkojme te printuar(true), ose nese kerkojme te paprintuarat(false) ne kase</param>
        /// <returns>Kthen true nese e mbush nje objekt dhe anasjelltas</returns>
        public bool merrDergimeKaseSipasIdShitjeDheStatus(int idShitje, bool statusiPrintimi)
        {
            clsDatabaseInventari dbPrintime = new clsDatabaseInventari();
            DataRow tabela = dbPrintime.ktheDergimeKaseSipasIdShitjeDheStatus(idShitje, statusiPrintimi);
            dbPrintime.Dispose();
            return mbushPrintimNeKase(tabela);
        }

        /// <summary>
        /// Merr regjistrimin sipas id se dokumentit te shitjes ne nje dyqan dhe me nje status printimi te caktuar
        /// </summary>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <param name="idShop">Id e dyqanit</param>
        /// <param name="statusiPrintimi">Nese kerkojme te printuar(true), ose nese kerkojme te paprintuarat(false) ne kase</param>
        /// <returns>Kthen true nese e mbush nje objekt dhe anasjelltas</returns>
        public bool merrDergimeKaseSipasIdShitjeDheIdShopDheStatus(int idShitje, int idShop, bool statusiPrintimi)
        {
            clsDatabaseInventari dbPrintime = new clsDatabaseInventari();
            DataRow tabela = dbPrintime.ktheDergimeKaseSipasIdShitjeDheIdShopDheStatus(idShitje, idShop, statusiPrintimi);
            dbPrintime.Dispose();
            return mbushPrintimNeKase(tabela);
        }

        #endregion
    }
}
