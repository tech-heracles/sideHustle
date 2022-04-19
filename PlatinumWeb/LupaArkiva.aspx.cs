using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DbCore.IMBUtils;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Web.Script.Serialization;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.DataBase;
using CacheLayer;
namespace PlatinumWeb
{
    public partial class LupaArkiva : MyPageBase
    {
        private string tmpRootFolder = "";
        private int idPerdoruesi;
        private int idNdermarrje;
        private int idKategoria;
        private int iddok;
        private int idVitNdermarrje;

        /// <summary>
        /// mbush te dhenat kur faqja ben loadim
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idVitNdermarrje = mySessionObjects.ktheIdVitNdermarrje(Session);
            var scopeID = Request.Params[ScopeManager.ScopeIdKey];
            if (!mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect($"{Paths.defaultLoginPath}arsye=FaqePaautorizuar");
            }

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            if (!IsPostBack)
            {
                hfIdPerdorues.Set("hfIdPerdorues", idPerdoruesi);
               
                var maxSizeArkiva = clsNdermarrje.merrMaxSizeArkive(idNdermarrje);
                if (maxSizeArkiva == 0)
                {
                    maxSizeArkiva = 10485760;
                }
                fileManager.SettingsUpload.ValidationSettings.MaxFileSize = maxSizeArkiva;
                fileManager.SettingsUpload.ValidationSettings.MaxFileSizeErrorText = $"File s'mund te jete me i madh se {maxSizeArkiva / 1048576} Mb!";
                inicializoTeDrejta();
                inicializoRootFolder();
                fileManager.SettingsToolbar.ShowPath = false;
                                                              
                shtoButtonFileManager(fileManager);
                shtoButtonImageSlider(fileManager);
                shtoButtonSaveThumbnail(fileManager);
                fileManager.SettingsToolbar.Items.CreateDefaultItems();
                fileManager.SettingsContextMenu.Items.CreateDefaultItems();
                fileManager.JSProperties.Add("cpOpenDocUrlPrefix", buildOpenDocUrlPrefix(fileManager.Settings.RootFolder, "ViewerJS/#.."));
                fileManager.JSProperties.Add("cpOpenImageUrlPrefix", buildOpenDocUrlPrefix(fileManager.Settings.RootFolder, ""));
                fileManager.JSProperties.Add("cpPathArkiva", "~" + buildOpenDocUrlPrefix(fileManager.Settings.RootFolder, "").Replace('/', '\\'));
            }
        }
       
        private void shtoButtonFileManager(ASPxFileManager fileManager)
        {
            var customButton = new FileManagerToolbarCustomButton();
            customButton.CommandName = "openDocument";
            customButton.ToolTip = "View Document";
            customButton.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsEditpagehf16x16;
            fileManager.SettingsToolbar.Items.Add(customButton);
            var customContextButton = new FileManagerToolbarCustomButton();
            customContextButton.CommandName = "openDocument";
            customContextButton.Text = "View Document";
            customContextButton.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsEditpagehf16x16;
            fileManager.SettingsContextMenu.Items.Add(customContextButton);
        }

        
        
        private void shtoButtonImageSlider(ASPxFileManager fileManager)
        {
            var customButton = new FileManagerToolbarCustomButton();
            customButton.CommandName = "openImages";
            customButton.ToolTip = "View Images";
            customButton.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ContentImage16x16;
            fileManager.SettingsToolbar.Items.Add(customButton);
            var customContextButton = new FileManagerToolbarCustomButton();
            customContextButton.CommandName = "openImages";
            customContextButton.Text = "View Images";
            customContextButton.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ContentImage16x16;
            fileManager.SettingsContextMenu.Items.Add(customContextButton);
        }
        private void shtoButtonSaveThumbnail(ASPxFileManager fileManager)
        {
            var customButton = new FileManagerToolbarCustomButton();
            customButton.CommandName = "setThumbnail";
            customButton.ToolTip = "Set thumbnail";
            customButton.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsEditpagehf16x16;
            fileManager.SettingsToolbar.Items.Add(customButton);
            var customContextButton = new FileManagerToolbarCustomButton();
            customContextButton.CommandName = "setThumbnail";
            customContextButton.Text = "Set thumbnail";
            customContextButton.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsEditpagehf16x16;
            fileManager.SettingsContextMenu.Items.Add(customContextButton);
        }
        string buildOpenDocUrlPrefix(string rootFolder, string pathViewerToArkiva)
        {

            var openDocUrlPrefix = pathViewerToArkiva + rootFolder.Replace("~", "");
            var arrayUrl = openDocUrlPrefix.Split('/').ToList();
            if (arrayUrl.Last() == "")
                arrayUrl.RemoveAt(arrayUrl.Count - 1);            
            arrayUrl[arrayUrl.Count - 1] = "";
            return String.Join("/", arrayUrl);
        }
        /// <summary>
        /// Shtimi i te drejtave ne ambjentin e Arkives.
        /// </summary>
        private void inicializoTeDrejta()
        {
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idVitNdermarrje, "LupaArkiva.aspx");

            // Perdoruesi ka te drejte ne ambjent: Lejohet download i dokumentave
            fileManager.SettingsEditing.AllowDownload = tedrejtaInfo.DAmb;
            fileManager.SettingsToolbar.ShowDownloadButton = tedrejtaInfo.DAmb;

            // Perdoruesi ka te drejte shtimi ne ambjent: Lejohet shfaqja e panelit Upload
            fileManager.SettingsUpload.Enabled = tedrejtaInfo.DShtim;

            // Perdoruesi ka te drejte te modifikoje: Mund te beje rename ose te ndryshoje vendndodhjen
            fileManager.SettingsEditing.AllowMove = tedrejtaInfo.DMod;
            fileManager.SettingsEditing.AllowRename = tedrejtaInfo.DMod;

            // Perdoruesi ka te drejte te fshije: Shfaqet butoni fshirjes.
            fileManager.SettingsEditing.AllowDelete = tedrejtaInfo.DFsh;

            // Perdoruesi ka te drejte te shohe gjithe dokumentat: Shikon folderin kryesor dhe folderat bij te tij
            fileManager.SettingsEditing.AllowCreate = tedrejtaInfo.DGjitheDok && tedrejtaInfo.DShtim;
            fileManager.SettingsFolders.Visible = tedrejtaInfo.DGjitheDok;
            fileManager.SettingsToolbar.ShowPath = tedrejtaInfo.DGjitheDok;
        }

        protected void merrVleratNgaQueryString()
        {
            string strIdDok = Request.QueryString["idDok"];

            if (string.IsNullOrWhiteSpace(strIdDok) || strIdDok == "null")
            {
                iddok = 0;
                //merr tmpRootFolder vetem nese eshte dokument per shtim
                tmpRootFolder = Request.QueryString["tmpfolder"];
                tmpRootFolder = tmpRootFolder == "undefined" ? null : tmpRootFolder;

            }
            else
                iddok = int.Parse(strIdDok);
            switch (Request.QueryString["veprimi"])
            {
                case "shitje":
                    idKategoria = 1;
                    break;

                case "blerje":
                    idKategoria = 2;
                    break;

                case "arketim":
                case "pagese":
                    idKategoria = 3;
                    break;
                
                case "terheqje":
                case "derdhje":
                    idKategoria = 4;
                    break;

                case "artikull":
                    idKategoria = 13;
                    break;

                case "kf":
                    idKategoria = 12;
                    break;

                case "grupArtikull":
                    idKategoria = 67;
                    break;

                case "magazina":
                    idKategoria = 6;
                    break;

                case "njesiadmin":
                    idKategoria = 23;
                    break;

                case "seriale":
                    idKategoria = 116;
                    break;

                case "gis":
                    idKategoria = 142;
                    break;

                case "listpagesa":
                    idKategoria = 38;
                    break;

                case "hierarki":
                    idKategoria = 105;
                    break;

                case "punonjes":
                    idKategoria = 37;
                    break;
              
                default:
                    idKategoria = 0;
                    break;
            }
        }

        private void KrijoFolderatSipasKategoriveTeCelura(string uploadDirectoryMapped)
        {
            colKategoriArkive kategorite = new colKategoriArkive(idNdermarrje, idKategoria);
            string newFolder = "";
            foreach (var kategori in kategorite)
            {
                newFolder = MapPath(uploadDirectoryMapped + "/" + kategori.Kategoria);
                if (!Directory.Exists(newFolder))
                    Directory.CreateDirectory((newFolder));
            }
        }

        private void inicializoRootFolder()
        {
            merrVleratNgaQueryString();
            var UploadDirectory = "~/Arkiva/";
            var connString = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());

            if (!Directory.Exists(MapPath(UploadDirectory)))
            {
                Directory.CreateDirectory(MapPath(UploadDirectory));
            }
            UploadDirectory += String.IsNullOrWhiteSpace(connString) ? $"{idNdermarrje}/" : $"{ connString}/{ idNdermarrje}/"; 
            //UploadDirectory +=connString+"/"+ idNdermarrje + "/";
            if (!Directory.Exists(MapPath(UploadDirectory)))
            {
                Directory.CreateDirectory(MapPath(UploadDirectory));
            }
            if (idKategoria != 0)
            {
                UploadDirectory += $"{ idKategoria}/";
                if (!Directory.Exists(MapPath(UploadDirectory)))
                {
                    Directory.CreateDirectory(MapPath(UploadDirectory));
                }
            }
            if (iddok != 0)
            {
                UploadDirectory += $"{ iddok}/";
                if (!Directory.Exists(MapPath(UploadDirectory)))
                {
                    Directory.CreateDirectory(MapPath(UploadDirectory));
                }
            }
            else
            {
                //hap ambjnetin e regjistrimit,hap arkiven dhe ngarkon nje dok,
                //me pas  hap ambjentin e regjistrimit perseri , dhe hap arkiven,ne kete rast duhet te jete i njejti folder tmp

                //ne kete rast i bie qe te jete hapur per here te pare lupa nga ambjenti i shtimit te nje dokumenti
                //keshtu qe krijojme folderin tmp dhe e ruajme ne hidden field,me qellim qe kur te hapet heren tjeter lupa e arkives te kemi nje tmpFolder
                string tmpPattern = "TMP" + idPerdoruesi + "_";
                if (string.IsNullOrWhiteSpace(tmpRootFolder))
                {
                    FshiFolderaTmpNeseKa(MapPath(UploadDirectory), tmpPattern);
                    UploadDirectory += tmpPattern + DateTime.Now.ToFileTime() + "/";
                    Directory.CreateDirectory(MapPath(UploadDirectory));
                }
                else
                    UploadDirectory = tmpRootFolder;


            }
            //vendoset ketu sepse mund te ndodhi qe perdoruesi ka nje dokument,dhe ska kategori,por me vone krijon kategori dhe do qe ti shfaqen
            KrijoFolderatSipasKategoriveTeCelura(UploadDirectory);
            fileManager.Settings.RootFolder = UploadDirectory;
            hfMyArkiva.Set("rootFolder", UploadDirectory);
        }

        private void FshiFolderaTmpNeseKa(string path, string searchPattern)
        {
            if (Directory.Exists(path))
            {
                IEnumerable<string> directories = Directory.GetDirectories(path).Where(x => x.Contains(searchPattern));

                foreach (string dir in directories)
                {
                    if (Directory.Exists(dir))
                    {
                        Directory.Delete(dir, true);
                    }
                }
            }
        }

        private void ZhvendosFileNeFolderinTeFshire(string fullFileName)
        {
            string folderiTeFshire = MapPath(fileManager.Settings.RootFolder + "/" + "TeFshire/");
            if (!Directory.Exists(folderiTeFshire))
            {
                Directory.CreateDirectory(folderiTeFshire);
            }
            string newFileName = folderiTeFshire + Path.GetFileName(fullFileName);

            if (!File.Exists(newFileName))
                File.Copy(MapPath(fullFileName), newFileName);
        }
        #region EVENTET

        protected void fileManager_FileUploading(object sender, FileManagerFileUploadEventArgs e)
        {
            try
            {
                var conn = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
                merrVleratNgaQueryString();
                clsArkiva arkivaDok = new clsArkiva(0, iddok, idKategoria, e.File.Extension, e.File.FullName, e.FileName, "", (int)StatusDokumenti.Ruajtur, idPerdoruesi, idPerdoruesi, 0, conn);
                clsMesazh mesazh = arkivaDok.ruaj();
                if (!mesazh.Status)
                {
                    e.ErrorText = mesazh.PershkrimMesazhi;
                    e.Cancel = true;
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                e.ErrorText = err.Message;
                e.Cancel = true;
            }
        }

        //nese eshte dok i ri te krijoj nje folder tmp+tick ,else krijo folder sipas id dok nese nuk ekziston bashke me kategorite e arkives

        protected void fileManager_ItemRenaming(object sender, FileManagerItemRenameEventArgs e)
        {
            string conn = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
            clsMesazh mesazhi = clsArkiva.NdryshoPath(e.Item.FullName, e.Item.FullName, e.Item.Name, idPerdoruesi,conn);
            if (!mesazhi.Status)
            {
                e.Cancel = true;
                e.ErrorText = mesazhi.PershkrimMesazhi;
            }
        }

        protected void fileManager_ItemMoving(object sender, FileManagerItemMoveEventArgs e)
        {
            string conn = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
            clsMesazh mesazhi = clsArkiva.NdryshoPath(e.Item.FullName, e.DestinationFolder.FullName, e.Item.Name, idPerdoruesi,conn);
            if (!mesazhi.Status)
            {
                e.Cancel = true;
                e.ErrorText = mesazhi.PershkrimMesazhi;
            }
        }

        protected void fileManager_ItemDeleting(object sender, FileManagerItemDeleteEventArgs e)
        {
            try
            {
                merrVleratNgaQueryString();
                clsMesazh mesazh = new clsMesazh();

                clsArkiva arkivaDok = new clsArkiva(iddok, idKategoria, DateTime.Now, e.Item.FullName, (int)StatusDokumenti.Fshire, idPerdoruesi);
                if (iddok == 0)
                {
                    //meqe eshte dokument ekzistues ath per te ruajtur historikun i kalojme filet e fshire ne folderin dokumenta te fshire
                    mesazh = arkivaDok.fshiArkiven();
                }
                else
                {
                    mesazh = arkivaDok.fshiUpdateStatus();
                    if (mesazh.Status)
                        ZhvendosFileNeFolderinTeFshire(e.Item.FullName);
                    else
                    {
                        e.Cancel = true;
                        e.ErrorText = mesazh.PershkrimMesazhi;
                    }
                }
                if (!mesazh.Status)
                {
                    e.ErrorText = mesazh.PershkrimMesazhi;
                    e.Cancel = true;
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                e.ErrorText = err.Message;
                e.Cancel = true;
            }
        }



        protected void fileManager_FolderCreating(object sender, FileManagerFolderCreateEventArgs e)
        {
            //ne varese te te drejtave mund te kufizojme edhe krijimin e folderave

        }

        protected void fileManager_ItemCopying(object sender, FileManagerItemCopyEventArgs e)
        {
        }

        protected void fileManager_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
        {
            fileManager.JSProperties["cpRootFolder"] = fileManager.Settings.RootFolder;
        }
        protected void fileManager_CustomCallback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                var conn = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
                merrVleratNgaQueryString();
                string[] parametrat= e.Parameter.Split(';'); // [ Extension, FullName, FileName]
                clsArkiva arkivaDok = new clsArkiva(0, iddok, idKategoria, parametrat[0], parametrat[1].Replace('/','\\'), parametrat[2],"", (int)StatusDokumenti.Ruajtur, idPerdoruesi, idPerdoruesi, 0, conn,true);
                arkivaDok.ruaj();
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            
            }
        }
        protected void fileManager_PreRender(object sender, EventArgs e)
        {
            string path = clsArkiva.merrThumbnailDefault(iddok);
            fileManager.JSProperties["cpthumbnailPhotoPath"] = path;
        }
        #endregion

    }
}