using DbCore.Raporte;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbShare
{
    public class clsRaportDesign
    {
        #region Attributet

        private clsReportDesigner reportDesigner = new clsReportDesigner();
        public const string _rap_portrait = "Portrait";
        public const string _rap_landscape = "Landscape";


        #endregion Attributet

        #region Properties

        public int IdRaportDesign { get; set; }

        public string FileName { get; set; }

        public string Pershkrim { get; set; }

        public string FileNamePortrait { get; set; }

        public int IdRaporti { get; set; }

        public bool IModifikueshem { get; set; }

        public int IdPrindi { get; set; }

        public bool Zgjedhur { get; set; }

        public bool RuajNeSession { get; set; }

        public bool RollPaper { get; set; }
        public bool AutoWidth { get; set; }
        public bool OldViewer { get; set; }
        #endregion Properties

        public clsRaportDesign()
        {
        }

        public clsRaportDesign(int id)
        {
            using (clsDatabaseShare dbshare = new clsDatabaseShare())
            {
                this.mbushDesign(dbshare.merrSipasRaportDesignId(id));
            }
        }
        public clsRaportDesign(string emri, int idndermarje)
        {
            using (clsDatabaseShare dbshare = new clsDatabaseShare())
                this.mbushDesign(dbshare.merrRaportSipasEmritReal(emri, idndermarje));
        }

        public clsRaportDesign(System.Data.DataRow rreshti)
        {
            mbushDesign(rreshti);
        }

        public string GetReportName(string orientimi)
        {
            var name = orientimi == _rap_portrait ? FileNamePortrait : FileName;
            if (name == "")
                name = orientimi == _rap_portrait ? FileName : FileNamePortrait;
            return name;
        }

        /// <summary>
        /// kthen designin i cili eshte ne projekt dhe nga i cili kane dale te gjithe designet e tjere
        /// </summary>
        /// <returns></returns>
        public clsRaportDesign MerrDesignOrigjinal()
        {
            if (IdPrindi == 0) return this;
            using (clsDatabaseShare dbshare = new clsDatabaseShare())
            {
                return new clsRaportDesign(dbshare.merrRaportDesignOrigjinalSipasIdDesign(IdRaportDesign));
            }
        }

        public XtraReport SaveAs(string pershkrimiNew, int idNdermarrje, string orientimi, byte[] reportLayout, bool zgjedhurDefault)
        {
            using (var scope = new MyTransactionScope())
            {
                var fileName = GetNewName(GetReportName(orientimi) == FileName ? FileName : FileNamePortrait);
                string folder = $"{ IMBUtils.DataBase.MyConnectionsManager.GetSelectedConNameServer()}/";
                var report = reportDesigner.SaveReportToFile(reportLayout, fileName, folder);

                var saveResult = RuajDizajnNeDb(pershkrimiNew, idNdermarrje, report.Landscape, fileName);

                if (!saveResult)
                    throw new MyException(saveResult.PershkrimMesazhi);

                if (Zgjedhur)
                    RuajZgjedhjePerNdermarrje(idNdermarrje, IdRaportDesign);

                scope.Complete();

                return report;
            }
        }

        public clsMesazh RuajDizajnNeDb(string pershkrimiNew, int idNdermarrje, bool landscape, string newDesignFileName)
        {
            if (string.IsNullOrWhiteSpace(pershkrimiNew))
                return new clsMesazh(false, "Pershkrimi per emrin e ri te design-it eshte bosh");
            Pershkrim = pershkrimiNew;
            IdPrindi = IdRaportDesign;
            IModifikueshem = true;
            if (landscape)
            {
                FileName = newDesignFileName;
                FileNamePortrait = string.Empty;
            }else
            {
                FileName = string.Empty;
                FileNamePortrait = newDesignFileName;
            }
            clsMesazh mesazh = eshteEmerUnikDisajniPerKeteRaport(pershkrimiNew, IdRaporti);
            if (!mesazh.Status)
                return mesazh;
            mesazh = Ruaj();

            if (!mesazh.Status)
                return mesazh;
            mesazh = InsertRapXDesign(idNdermarrje);
            return mesazh;

        }

        public clsMesazh eshteEmerUnikDisajniPerKeteRaport(string pershkrimiNew, int idRaporti)
        {
            using (var dbShare = new clsDatabaseShare())
                return new clsMesazh(!dbShare.ekzistonPershkrimiNeDB(pershkrimiNew, idRaporti), "Ekziston design me emrin '" + pershkrimiNew + "' per kete raport.");
        }
        /// <summary>
        /// krijon nje emer te ri duke u bazuar tek e
        /// </summary>
        /// <param name="emriOld"></param>
        /// <returns></returns>
        private string GetNewName(string emriOld)
        {
            const string separatorDesignFileName = "_SePDeFn_";
            var baseName = emriOld.IndexOf(separatorDesignFileName, StringComparison.Ordinal) == -1 
                ? emriOld : emriOld.Split(new [] { separatorDesignFileName }, StringSplitOptions.None)[0];

            return $"{baseName}{separatorDesignFileName}{DateTime.Now.ToFileTime()}";
        }

        private clsMesazh InsertRapXDesign(int idNdermarrje)
        {
            using (var dbShare = new clsDatabaseShare())
                return dbShare.ShtoRapxDesign(idNdermarrje, IdRaportDesign, Zgjedhur);
        }

        public clsMesazh Ruaj()
        {
            var dbShare = new clsDatabaseShare();
            IdRaportDesign = dbShare.RuajRaportDesign(IdRaporti, Pershkrim, FileName, FileNamePortrait, IModifikueshem, IdPrindi, RuajNeSession, AutoWidth, RollPaper, OldViewer);
            return new clsMesazh(true, $"Ruajtja e design-it me emer {Pershkrim} u krye me sukses!");
        }

        public void merrSipasNdermarjedheRaport(int idndermarje, int idraport)
        {
            using (clsDatabaseShare dbshare = new clsDatabaseShare())
            {
                this.mbushDesign(dbshare.merrSipasNdermarjedheRaport(idndermarje, idraport).Rows[0]);
            }
        }

        public void merrSipasNdermarjedheRaport(int idndermarje, string rapEmriReal)
        {
            using (clsDatabaseShare dbshare = new clsDatabaseShare())
            {
                this.mbushDesign(dbshare.merrSipasNdermarjedheEmerRaport(idndermarje, rapEmriReal).Rows[0]);
            }
        }

        internal void mbushDesign(System.Data.DataRow rreshti)
        {
            if (rreshti == null)
                return;
            try
            {
                IdRaportDesign = !IsDBNull(rreshti["IDRAPORTDESING"])
                    ? ToInt32(rreshti["IDRAPORTDESING"])
                    : 0;
                FileName = rreshti["FILENAME"].ToString();
                Pershkrim = rreshti["PERSHKRIM"].ToString();
                FileNamePortrait = rreshti["FILENAMEPORTRAIT"].ToString();
                IdRaporti = !IsDBNull(rreshti["IDRAPORTI"])
                    ? ToInt32(rreshti["IDRAPORTI"])
                    : 0;
                IModifikueshem = !IsDBNull(rreshti["IMODIFIKUESHEM"]) && ToBoolean(rreshti["IMODIFIKUESHEM"]);
                IdPrindi = !IsDBNull(rreshti["IDPRINDI"])
                    ? ToInt32(rreshti["IDPRINDI"])
                    : 0;
                Zgjedhur = !IsDBNull(rreshti["Zgjedhur"]) && ToBoolean(rreshti["Zgjedhur"]);
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se tipit te kontrollit nga db-ja");
            }
        }

        /// <summary>
        /// update fushen zgjedhur ne tbl  t_Rapxdesign per parametrat e dhene
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idDesign"></param>
        /// <returns></returns>
        public static clsMesazh RuajZgjedhjePerNdermarrje(int idNdermarrje, int idDesign)
        {
            try
            {
                using (var dbShare = new clsDatabaseShare())
                {
                    return dbShare.RuajZgjedhjeDizajniPerNdermarrje(idNdermarrje, idDesign);
                }
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }
        }

        public static (bool ruajNeSession, bool rollPaper, bool autoWidth, bool oldViewer) GetReportDesignSettings(int idReportDesign)
        {
            using (var dbShare = new clsDatabaseShare())
            {
                DataRow settings = dbShare.GetReportDesignSettings(idReportDesign);
                if(settings != null)
                {
                    bool RuajNeSession = !IsDBNull(settings["RuajNeSession"]) && ToBoolean(settings["RuajNeSession"]);
                    bool RollPaper = !IsDBNull(settings["RollPaper"]) && ToBoolean(settings["RollPaper"]);
                    bool AutoWidth = !IsDBNull(settings["AutoWidth"]) && ToBoolean(settings["AutoWidth"]);
                    bool OldViewer = !IsDBNull(settings["OldViewer"]) && ToBoolean(settings["OldViewer"]);
                    return (ruajNeSession: RuajNeSession, rollPaper: RollPaper, autoWidth: AutoWidth, oldViewer: OldViewer);
                }
                return (ruajNeSession: false, rollPaper: false, autoWidth: false, oldViewer: false);
            }
        }
    }
}