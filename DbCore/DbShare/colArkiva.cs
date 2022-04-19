using System;
using System.IO;
using System.Data;
using System.Web.Hosting;
using System.Collections.Generic;
using IMB.Transactions.FileManager;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbShare
{
    public class colArkiva : System.Collections.Generic.List<clsArkiva>
    {
        #region Konstruktoret

        public colArkiva()
        { }

        public colArkiva(int count)
        {
            Capacity = count;
        }

        public colArkiva(int iddok, int idllojdok)
        {
            using (clsDatabaseShare db = new clsDatabaseShare())
            {
                foreach (DataRow rreshti in db.ktheArkivaSipasIDDOK(iddok, idllojdok).Rows)
                    Add(new clsArkiva(rreshti));
            }
        }

        public colArkiva(int iddok, int idllojdok, clsDatabaseShare db)
        {
            foreach (DataRow rreshti in db.ktheArkivaSipasIDDOK(iddok, idllojdok).Rows)
                Add(new clsArkiva(rreshti));
        }

        #endregion Konstruktoret


        /// <summary>
        /// Ruan objektin klient/furnitor ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns> Kthen true nese ruajtja perfundoi me sukses </returns>
        [Obsolete]
        public clsMesazh ruaj(int idKategoria, int iddok, int idPerdoruesi)
        {
            clsMesazh msg = new clsMesazh(true);
            clsDatabaseShare db = new clsDatabaseShare();
            db.beginTransaksion();
            foreach (clsArkiva arkiv in this)
            {
                msg = arkiv.ruajArkiva(db);
                if (!msg.Status)
                {
                    db.rollbackTransaksion();
                    return msg;
                }
            }
            if (idKategoria == 13)
            {
                DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin(db );
                msg = DbCore.DbInventari.clsArtikulli.updateDtModifikimi(iddok, idPerdoruesi, dbAdmin);
                if (!msg.Status)
                {
                    db.rollbackTransaksion();
                    return msg;
                }
            }
            db.commitTransaksion();
            return msg;
        }
        private bool MbushArkiven(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
                Add(new clsArkiva(rreshti));
            return true;
        }
        public void mbushImazheNgaArkiva(int iddok, int idllojdok)
        {
            using (clsDatabaseShare db = new clsDatabaseShare())
            {
                MbushArkiven(db.ktheImazheArkivaSipasIDDOK(iddok, idllojdok));
            }
        }


        /// <summary>
        /// modifikon arkiven e nje dokumenti ekzistues,ne riruajtje ose modifikim te tij,
        /// </summary>
        /// <param name="clsDatabaseShare"></param>
        /// <param name="idKokaMagazina"></param>
        /// <param name="idkokare"></param>
        /// <param name="idKategoria"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        internal static clsMesazh ModifikoArkiven(int idDokOld, int idDokNew, int idKategoria, int idNdermarrje, int idPerdoruesi)
        {
            try
            {
                clsMesazh mesazh = new clsMesazh(true, "Dokumenti u modifikua me sukses!");
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                string connString = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
                string pathiOld = String.IsNullOrWhiteSpace(connString) ? "~/Arkiva/" + idNdermarrje + "/" + idKategoria + "/" + idDokOld + "/" : "~/Arkiva/"+ connString + "/" + idNdermarrje + "/" + idKategoria + "/" + idDokOld + "/";
                string pathiNew = String.IsNullOrWhiteSpace(connString) ? "~/Arkiva/" + idNdermarrje + "/" + idKategoria + "/" + idDokNew + "/" : "~/Arkiva/"+ connString + "/" + idNdermarrje + "/" + idKategoria + "/" + idDokNew + "/";

                string pathiOldMapped = context.Request.MapPath(pathiOld);
                string pathiNewMapped = context.Request.MapPath(pathiNew);
                TxFileManager fileMgr = new TxFileManager();
                if (fileMgr.DirectoryExists(pathiOldMapped) && idDokNew != idDokOld)
                    fileMgr.MoveDirectory(pathiOldMapped, pathiNewMapped);//direktori e re
                if (fileMgr.DirectoryExists(pathiNewMapped))
                {
                    List<string> files = new List<string>();
                    clsFunksione.MerrGjitheFiletRekursiv(pathiNewMapped, ref files, "TeFshire");
                    if (files.Count > 0)
                    {
                        string rootDokFolder = String.IsNullOrWhiteSpace(connString) ? context.Request.PhysicalApplicationPath + "Arkiva\\" + idNdermarrje + "\\" + idKategoria + "\\" + idDokNew + "\\": context.Request.PhysicalApplicationPath + "Arkiva\\"+ connString+ "\\" + idNdermarrje + "\\" + idKategoria + "\\" + idDokNew + "\\";
                        return UpdatePathetNeDB(rootDokFolder, files, pathiOld, pathiNew, idDokNew, idPerdoruesi);
                    }
                }
                return mesazh;
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Ndodhi nje gabim gjate modifikimit te arkives", idDokOld, idDokNew, idKategoria, idPerdoruesi, idNdermarrje);
                return new clsMesazh(false, ex.Message);
            }
        }

        /// <summary>
        /// ruan arkiven per nje dokument te ri,brenda nje transaksioni me ruajtjen ne db te te dhenave
        /// </summary>
        /// <param name="dbShare"></param>
        /// <param name="idDok"></param>
        /// <param name="idKategoria"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal static clsMesazh RuajArkiven(int idDok, int idKategoria, int idPerdoruesi, int idNdermarrje, IDictionary<string, object> hfArkiva)
        {
            clsMesazh mesazh = new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
            if (hfArkiva == null || !hfArkiva.ContainsKey("rootFolder"))
                return mesazh;
            string connString = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
            string rootFolderTmp = (string)hfArkiva["rootFolder"];
            if (string.IsNullOrWhiteSpace(rootFolderTmp))
                return new clsMesazh(true, "Nuk ka arkive");
            try
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                string pathiOld = rootFolderTmp;
                string pathiNew = String.IsNullOrWhiteSpace(connString) ? "~/Arkiva/" + idNdermarrje + "/" + idKategoria + "/" + idDok + "/" : "~/Arkiva/" + connString + "/" + idNdermarrje + "/" + idKategoria + "/" + idDok + "/" ;
                string pathiOldMapped = context.Request.MapPath(pathiOld);
                string pathiNewMapped = context.Request.MapPath(pathiNew);
                if (hfArkiva.ContainsKey("kopjoArkiven") && bool.Parse(hfArkiva.ContainsKey("kopjoArkiven").ToString()))
                {
                    TxFileManager fileMgr = new TxFileManager();
                    if (fileMgr.DirectoryExists(pathiOldMapped) && !fileMgr.DirectoryExists(pathiNewMapped))
                        fileMgr.CopyDirectory(pathiOldMapped, pathiNewMapped, true);//direktori e re

                    {
                        List<string> files = new List<string>();
                        clsFunksione.MerrGjitheFiletRekursiv(pathiNewMapped, ref files, "TeFshire");


                        if (files.Count > 0)
                        {
                            string rootDokFolder = String.IsNullOrWhiteSpace(connString) ? context.Request.PhysicalApplicationPath + "Arkiva\\" + idNdermarrje + "\\" + idKategoria + "\\" + idDok + "\\" : context.Request.PhysicalApplicationPath + "Arkiva\\" + connString  + "\\" + idNdermarrje + "\\" + idKategoria + "\\" + idDok + "\\";
                            mesazh = KopjoPathetNeDB(rootDokFolder, files, pathiOld, pathiNew, idDok, idPerdoruesi);
                            if (!mesazh.Status)
                                return mesazh;
                            hfArkiva["kopjoArkiven"] = false;
                        }
                    }
                }
                else
                {
                    TxFileManager fileMgr = new TxFileManager();
                    if (fileMgr.DirectoryExists(pathiOldMapped) && !fileMgr.DirectoryExists(pathiNewMapped))
                        fileMgr.MoveDirectory(pathiOldMapped, pathiNewMapped);//direktori e re

                    {
                        List<string> files = new List<string>();
                        clsFunksione.MerrGjitheFiletRekursiv(pathiNewMapped, ref files, "TeFshire");


                        if (files.Count > 0)
                        {
                            string rootDokFolder = String.IsNullOrWhiteSpace(connString) ? context.Request.PhysicalApplicationPath + "Arkiva\\" + idNdermarrje + "\\" + idKategoria + "\\" + idDok + "\\": context.Request.PhysicalApplicationPath + "Arkiva\\" + connString + "\\" + idNdermarrje + "\\" + idKategoria + "\\" + idDok + "\\";
                            return UpdatePathetNeDB(rootDokFolder, files, pathiOld, pathiNew, idDok, idPerdoruesi);
                        }
                    }
                }

                return mesazh;
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Ndodhi nje gabim gjate ruajtjes se arkives", idDok, idKategoria, idPerdoruesi, idNdermarrje);
                return new clsMesazh(false, ex.Message);
            }
        }

        private static clsMesazh KopjoArkiven(string originalFolder,string copiedFolder,int idDokNew,int idNdermarrje,int idPerdoruesi)
        {
            return new clsMesazh();
        }
        private static clsMesazh ZhvendosArkivenNgaFolderiTmp()
        {
            return new clsMesazh();
        }
        /// <summary>
        /// updateton pathet ne db te fileve te ngarkuar,pasi ato jane zhvendosur ne nje direktori te re,gjithashtu vendos edhe id e re te dokumentit lidhes
        /// </summary>
        /// <param name="dbShare"></param>
        /// <param name="rootDokFolder"></param>
        /// <param name="files"></param>
        /// <param name="pathiOld"></param>
        /// <param name="pathiNew"></param>
        /// <param name="idDok"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        private static clsMesazh UpdatePathetNeDB(string rootDokFolder, List<string> files, string pathiOld, string pathiNew, int idDok, int idPerdoruesi)
        {
            DataTable dtArkiva = new DataTable();
            dtArkiva.Columns.Add("oldPath");
            dtArkiva.Columns.Add("newPath");
            string conn = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
            string pjesaDalluese = string.Empty;
            foreach (var file in files)
            {
                pjesaDalluese = file.Replace(rootDokFolder, string.Empty).Replace("\\", "/");

                DataRow row = dtArkiva.NewRow();
                row["oldPath"] = (pathiOld + pjesaDalluese).Replace("/", @"\");
                row["newPath"] = (pathiNew + pjesaDalluese).Replace("/", @"\");
                dtArkiva.Rows.Add(row);
            }
            using (clsDatabaseShare dbShare = new clsDatabaseShare())
                return dbShare.UpdateArkiva(dtArkiva, idDok, idPerdoruesi,conn);
        }

        private static clsMesazh KopjoPathetNeDB(string rootDokFolder, List<string> files, string pathiOld,string pathiNew, int idDok, int idPerdoruesi)
        {
            //perdorim te njejten strukture per mos krijuar dy tipe ne db
            DataTable dtArkiva = new DataTable();
            dtArkiva.Columns.Add("oldPath");
            dtArkiva.Columns.Add("newPath");
            string conn = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
            string pjesaDalluese = string.Empty;
            foreach (var file in files)
            {
                pjesaDalluese = file.Replace(rootDokFolder, string.Empty).Replace("\\", "/");

                DataRow row = dtArkiva.NewRow();
                row["oldPath"] = (pathiOld + pjesaDalluese).Replace("/", @"\");
                row["newPath"] = (pathiNew + pjesaDalluese).Replace("/", @"\");
                dtArkiva.Rows.Add(row);
            }
            using (clsDatabaseShare dbShare = new clsDatabaseShare())
                return dbShare.KopjoArkiva(dtArkiva, idDok, idPerdoruesi,conn);
        }

        /// <summary>
        /// shenon si te fshire nje dokument te arkives ne baze te id se dokumentit dhe id se llojit te tij,kjo metode nuk e fshin dokumentin nga folderi ku eshte vendosur
        /// </summary>
        /// <param name="dbShare"></param>
        /// <param name="idDok"></param>
        /// <param name="idKategoria"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public static clsMesazh UpdateStatusDokFshi(clsDatabaseShare dbShare, int idDok, int idKategoria, int idPerdoruesi)
        {
            return dbShare.UpdateStatusDokFshiPerArkiven(idDok, idKategoria, idPerdoruesi);
        }

        public static clsMesazh UpdateStatusDokFshi(int idDok, int idKategoria, int idPerdoruesi)
        {
            using (clsDatabaseShare dbShare = new clsDatabaseShare())
                return dbShare.UpdateStatusDokFshiPerArkiven(idDok, idKategoria, idPerdoruesi);

        }
    }
}

