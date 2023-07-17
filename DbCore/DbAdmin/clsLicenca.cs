using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Resources;
using System.Globalization;
using System.Security.Cryptography;
using DbCore.IMBUtils.Security;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.Net;
using System.Web.Configuration;
using DevExpress.CodeParser;

namespace DbCore.DbAdmin
{
    public class clsLicenca
    {
        #region Atribute

        private int idLicenca;
        private string kodLicenca;
        private DateTime dateRegjistrimi;
        private int nrNdermarjesh;
        private int nrPerdoruesish;
        private int idLlojLicence;
        private DateTime dateSkadimi;
        private int diteTolerance;
        private byte[] code;
        private bool blockMultipleLogin;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// kontruktori me parametra
        /// </summary>
        /// <param name="idLicenca">id e licences</param>
        /// <param name="kodLicenca">kodi</param>
        /// <param name="nrNdermarjesh">nr ndermarjesh</param>
        /// <param name="nrPerdoruesish">nr perdoruesish</param>
        /// <param name="dateRegjistrimi">data e regjistrimit</param>
        public clsLicenca(int idLicenca, string kodLicenca, DateTime dateRegjistrimi,
                            int nrNdermarjesh, int nrPerdoruesish, int idllojlicence)
        {
            this.idLicenca = idLicenca;
            this.kodLicenca = kodLicenca;
            this.dateRegjistrimi = dateRegjistrimi;
            this.nrNdermarjesh = nrNdermarjesh;
            this.nrPerdoruesish = nrPerdoruesish;
            this.idLlojLicence = idllojlicence;
        }




        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idLicenca">idLicenca</param>
        public clsLicenca(int idLicenca)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                mbushLicence(db.merrLicenceSipasId(idLicenca));
            }
        }
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsLicenca()
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// Kthen/Vendos daten e mbarimit te licences
        /// </summary>
        public DateTime DateSkadimi
        {
            get
            {
                return dateSkadimi;
            }
            set
            {
                dateSkadimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos numrin e diteve te tolerances se perfundimit te licences
        /// </summary>
        public int DiteTolerance
        {
            get
            {
                return diteTolerance;
            }
            set
            {
                diteTolerance = value;
            }
        }

        /// <summary>
        /// Kthen numrin e diteve te mbetura te licences nga data e skadimit
        /// Kthen null nqs nuk ka vlere ne fushen dataMbarimit te tabela T_Licenca, perndryshe kthen numrin e diteve nga data e skadimit deri sot
        /// </summary>
        public int? nrDiteTeMbetura
        {
            get
            {
                DateTime sot = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                return this.dateSkadimi == DateTime.MinValue ? null : (int?)(this.dateSkadimi.Subtract(sot)).TotalDays;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdLicenca
        {
            get
            {
                return idLicenca;
            }
            set
            {
                idLicenca = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodin e licences
        /// </summary>
        public string KodLicenca
        {
            get
            {
                return kodLicenca;
            }
            set
            {
                kodLicenca = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos dateregjistrimi
        /// </summary>
        public DateTime DateRegjistrimi
        {
            get
            {
                return dateRegjistrimi;
            }
            set
            {
                dateRegjistrimi = value;
            }
        }



        /// <summary>
        /// Kthen/Vendos nr e ndermarjeve
        /// </summary>
        public int NrNdermarjesh
        {
            get
            {
                return nrNdermarjesh;
            }

            set
            {
                nrNdermarjesh = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nrPerdoruesish
        /// </summary>
        public int NrPerdoruesish
        {
            get
            {
                return nrPerdoruesish;
            }

            set
            {
                nrPerdoruesish = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos llojin e licences
        /// </summary>
        public int IdLlojLicenca
        {
            get
            {
                return idLlojLicence;
            }

            set
            {
                idLlojLicence = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nese duhet te lejohet qe i njejti user te logohet me shume se nje here ne program. True nese duhet te bllokohet, false perndryshe.
        /// </summary>
        public bool BlockMultipleLogin
        {
            get { return blockMultipleLogin; }
            set { blockMultipleLogin = value; }
        }

        public bool ChatAktiv { get; set; }

        public bool GoogleAnalytics { get; private set; }

        public string GoogleAnalyticsTrackingId { get; private set; }

        public string ChatLink { get; set; }
        public string ChatPortHttp { get; set; }
        public string ChatPortHttps { get; set; }
        #endregion

        #region Metoda Publike


        public bool isValid()
        {

            return this.code.SequenceEqual(PasswordHelper.HashValidData(this.kodLicenca + this.dateRegjistrimi.ToShortDateString() + this.nrNdermarjesh + this.nrPerdoruesish + this.IdLlojLicenca + this.dateSkadimi.ToShortDateString() + this.diteTolerance + (this.blockMultipleLogin ? "1" : "0") + "ImbKycje"));
        }
        /// <summary>
        /// Merr nje objekt llogarie duke filtruar sipas ID-se. Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrLlogari"/>
        /// </summary>
        public static clsLicenca merrLicenceSipasId(int idLicenca)
        {
            return new clsLicenca(idLicenca);
        }
        /// <summary>
        /// Mbush licencen sipas idPerdoreusi
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public bool mbushLicencen(int idPerdoruesi)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return mbushLicence(db.merrLicenceSipasIdPerdoruesi(idPerdoruesi));
            }
        }
        public static DataTable merrLicencaAktive()
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.merrLicenceAktive();
            }

        }
        /// <summary>
        /// Merr idLicencen e perdoruesit
        /// </summary>
        /// <param name="idPerdoruesi">id-ja perdoruesit</param>
        /// <returns>int idLicence, -1 perndryshe</returns>
        public static int merrIdLicencePerdoruesi(int idPerdoruesi)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            int idLicence = dbAdmin.merrIdLicencePerdoruesi(idPerdoruesi);
            dbAdmin.Dispose();
            return idLicence;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush licencen nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushLicence(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDLICENCA"].ToString(), out idLicenca);
                    kodLicenca = dbDataRow["KODLICENCA"].ToString();
                    DateTime.TryParse(dbDataRow["DATEREGJISTRIMI"].ToString(), out dateRegjistrimi);
                    int.TryParse(dbDataRow["NRNDERMARJESH"].ToString(), out nrNdermarjesh);
                    int.TryParse(dbDataRow["NRPERDORUESISH"].ToString(), out nrPerdoruesish);
                    int.TryParse(dbDataRow["IDLLOJLICENCE"].ToString(), out idLlojLicence);
                    if (!(dbDataRow["DataMbarimit"] is System.DBNull))
                        dateSkadimi = Convert.ToDateTime(dbDataRow["DataMbarimit"]);
                    int.TryParse(dbDataRow["DiteTolerance"].ToString(), out diteTolerance);
                    code = (byte[])dbDataRow["code"];
                    blockMultipleLogin = Convert.ToBoolean(dbDataRow["BLOCKMULTIPLELOGIN"]);
                    ChatAktiv = Convert.ToBoolean(dbDataRow["CHATAKTIV"]);
                    GoogleAnalytics = Convert.ToBoolean(dbDataRow["GOOGLEANALYTICS"]);
                    GoogleAnalyticsTrackingId = !Convert.IsDBNull(dbDataRow["GOOGLEANALYTICSTRACKINGID"])
                        ? dbDataRow["GOOGLEANALYTICSTRACKINGID"].ToString()
                        : string.Empty;
                    ChatLink = Convert.ToString(dbDataRow["CHATLINK"]);
                    ChatPortHttp = Convert.ToString(dbDataRow["CHATPORTHTTP"]);
                    ChatPortHttps = Convert.ToString(dbDataRow["CHATPORTHTTPS"]);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se liences nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion


        /// <summary>
        /// merr numrin e diteve qe do shfaqet mesazhi i perfundimit te licences te perdoruesi
        /// </summary>
        /// <returns></returns>
        public static int merrLimitDiteTeMbetura()
        {
            using (DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin())
            {
                return dbAdmin.ktheLimitDiteTeMbeturaPerLicencen();
            }
        }

        /// <summary>
        /// merr mesazhin e perfundimit te licences ne baze te numrit te diteve te mbetura nga data e skadimit si dhe te gjuhes se perdoruesit
        /// </summary>
        /// <param name="nrDiteTeMbetura"></param>
        /// <param name="diteTolerance">ka vleren 0 nqs fusha korresponduese ne db eshte null, perndr mban numrin e diteve qe do tolerohet hapja e programit</param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static string merrMesazhPerfundimLicence(int nrDiteTeMbetura, int diteTolerance, ResourceManager rm, CultureInfo ci)
        {
            string mesazhi = "";
            if (nrDiteTeMbetura == 0)
                mesazhi = rm.GetString("labelDiteTeMbeturaLicencaSkaduarSot", ci);
            if (nrDiteTeMbetura >= 1)
            {
                mesazhi = rm.GetString("labelDiteTeMbeturaTeLicenca", ci);
                mesazhi = mesazhi.Replace("#nrDite#", nrDiteTeMbetura.ToString());
            }
            if (diteTolerance > 0 && nrDiteTeMbetura < 0)//rasti kur licenca ka perfunduar por ka dite tolerance ende aktive
                mesazhi = rm.GetString("labelDiteTeMbeturaLicencaSkaduar", ci);
            return mesazhi;
        }


        /// <summary>
        /// kontrollon nese licenca ka skaduar
        /// </summary>
        /// <param name="idPerdoruesi">idPerodruesi per te cilin po kontrollojme nese i ka skaduar licenca perkatese</param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <returns>kthen false nepermjet objektit clsMesazh ne rastin kur licenca ka skaduar; kthen true me mesazhin perkates nqs i skadon per disa dite; kthen true pa mesazh kur nuk eshte ne ditet e fundit te licences</returns>
        public static clsMesazh KontrolloSkadiminLicences(int idPerdoruesi, ResourceManager rm, CultureInfo ci)
        {
            //int limitDiteTeMbetura = DbCore.DbAdmin.clsLicenca.merrLimitDiteTeMbetura();
            //DbCore.DbAdmin.clsLicenca licence = new DbCore.DbAdmin.clsLicenca();
            //licence.mbushLicencen(idPerdoruesi);
            //if (licence.IdLicenca == 0)
            //    return new clsMesazh("Problem ne leximin e licences!");
            //if (!licence.isValid())
            //    return new clsMesazh("Problem ne validimin e licences!");
            //if (!licence.nrDiteTeMbetura.HasValue)
            //    return new clsMesazh(true, "");
            //int nrDiteTeMbetura = licence.nrDiteTeMbetura.Value;
            //int nrDiteTolerance = 0;
            //if (nrDiteTeMbetura + nrDiteTolerance <= 0)
            //    return new clsMesazh(false, "Ka mbaruar afati bashke me tolerance!!!");
            //if (limitDiteTeMbetura != -1)   //kontrollon nqs DataMbarimit dhe limitDiteTeMbetura nuk jane null ne Db
            //{
            //    if (nrDiteTeMbetura <= limitDiteTeMbetura)
            //        return new clsMesazh(true, DbCore.DbAdmin.clsLicenca.merrMesazhPerfundimLicence(nrDiteTeMbetura, nrDiteTolerance, rm, ci));
            //}
            
            try
            {
                string dtSkadence = "";
                bool superUser = false;
                clsPerdorues perdorues = new clsPerdorues(idPerdoruesi);
                foreach (var role in perdorues.OColRolPerdoruesi)
                {
                    clsRoli rol = new clsRoli(role.IdRoli);
                    if (rol.KodRoli == "RSU")
                    {
                        superUser = true;
                        break;
                    }
                }
                if(superUser) return new clsMesazh(true, String.Empty);
                string orgEndDateURL = WebConfigurationManager.AppSettings["orgEndDateUrl"];
                var webRequest = clsFunksione.CreateGetWebRequestLicence(orgEndDateURL, clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar()); 
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        string ServiceResult = rd.ReadToEnd();
                        var dbObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(ServiceResult);
                        //json.GetType().GetProperty("allUrl").GetValue(json,null)
                        dbObject.TryGetValue("endDate", out dtSkadence);
                    }

                }
                if (DateTime.Parse(dtSkadence) < DateTime.Now && !superUser) return new clsMesazh(false, "Ka mbaruar afati bashke me tolerance!!!");
            }
            catch(Exception ex)
            {
                return new clsMesazh(false, "Problem ne validimin e licences!");
            }
            return new clsMesazh(true, String.Empty);
        }

        /// <summary>
        /// kthen datatable me databazat dhe licencen perkatese
        /// </summary>
        /// <returns></returns>
        public static DataTable MerrLicencatMeDB(string connectionName)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(connectionName))
            {
                return dbAdmin.merrLicencaMeDb();
            }
        }
        public static DataTable MerrLicencatMeDBMeFilter(string connectionName,string e,long start,long end )
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(connectionName))
            {
                return dbAdmin.merrLicencaMeDb(e,start,end);
            }
        }
        public static DataTable MerrLicencatMeDBMeID(string connectionName, int value )
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(connectionName))
            {
                return dbAdmin.merrLicencaMeDb(value);
            }
        }
        

    }
}
