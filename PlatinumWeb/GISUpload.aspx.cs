using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

using DbCore;
using DbCore.DbGIS;
using System.Collections;
using PlatinumWeb.ApplicationUtils.Pages;


namespace PlatinumWeb
{
    public partial class GISUpload : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clsMesazh result = new clsMesazh();

            HttpContext context = HttpContext.Current;
            if (context.Request.Files.Count > 0)
            {
                string fileId="", filePrefix="", filePath="";
              //  fileEx="",
                ArrayList filesExt = new ArrayList();

                if (context.Request.Files["filenameShp"] != null)
                {
                    fileId = "filenameShp";
                    filePrefix = "ShapeFile";
                  //  fileEx = ".shp";
                    filesExt.Add(".shp");
                    filePath = "~/UploadFiles/shapeFiles";
                    string fileEx = ".shp";
                    result = clsFunksioneGIS.eshteUpload(result, context, Server, fileId, filePrefix, filesExt, fileEx, filePath);
                }
                else if (context.Request.Files["filenameGpx"] != null)
                {
                    fileId = "filenameGpx";
                    filePrefix = "Gpx";
                    filesExt.Add(".gpx");
                    filesExt.Add(".GPX");
                    filesExt.Add(".Gpx");
                    filePath = "~/UploadFiles/gpxFiles";
                    var fileName =Path.GetFileName(context.Request.Files[fileId].FileName);
                    string fileEx = Path.GetExtension(fileName);
                

                    result = clsFunksioneGIS.eshteUpload(result, context, Server, fileId, filePrefix, filesExt, fileEx, filePath);

                    if (result.Status)
                    {
                        clsSkedaretGIS tempSkedaret = new clsSkedaretGIS();
                        tempSkedaret.IDSKEDARET = -1;
                        tempSkedaret.LLOJI = "gpx";
                        tempSkedaret.FILETYPE = "text/xml";
                        tempSkedaret.PATH = "UploadFiles/gpxFiles/";
                        tempSkedaret.FILENAME = result.PershkrimMesazhi;
                        tempSkedaret.SHENIME = Request["emriRiSkedarit"];
                        tempSkedaret.IDSTATUSDOK = 1;
                        tempSkedaret.DTKRIJIMI = DateTime.Now;
                        tempSkedaret.IDKRIJUESI = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                        tempSkedaret.DTMODIFIKIMI = DateTime.Now;
                        tempSkedaret.IDPERDORUESI = 0;
                        tempSkedaret.PRAPASHTESESKEDAR = fileEx;
                        tempSkedaret.IDDYTESORE = "";

                        result = tempSkedaret.ruaj();
                        if (result.Status)
                        {
                            result.PershkrimMesazhi = clsJSONHelper.Serialize<clsSkedaretGIS>(tempSkedaret);
                        }
                    }
                }

                else if (context.Request.Files[context.Request["idFushaPostSkedar"]] != null)
                {
                    fileId = context.Request["idFushaPostSkedar"];
                    filePrefix = "DokGeo";
                    filesExt.Add(".GPX");
                    filesExt.Add(".gpx");
                    filesExt.Add(".gpx");
                    filesExt.Add(".pdf");
                    filesExt.Add(".png");
                    filesExt.Add(".gif");
                    filesExt.Add(".jpeg");
                    filesExt.Add(".xls");
                    filesExt.Add(".xlsx");
                    filesExt.Add(".docx");
                    filesExt.Add(".doc");
                    filesExt.Add(".ppt");
                    filesExt.Add(".pptx");
                    filesExt.Add(".txt");
                    filesExt.Add(".jpg");
                    filesExt.Add(".PDF");
                    filesExt.Add(".JPG");
                    filesExt.Add(".JPEG");
                    filesExt.Add(".gif");
                    filesExt.Add(".GIF");
                    filePath = "~/UploadFiles/dokGeo";
                    var fileName =Path.GetFileName(context.Request.Files[fileId].FileName);
                     string fileEx = Path.GetExtension(fileName);

                     result = clsFunksioneGIS.eshteUpload(result, context, Server, fileId, filePrefix, filesExt, fileEx, filePath);

                    if (result.Status)
                    {
                        clsSkedaretGIS tempSkedaret = new clsSkedaretGIS();
                        tempSkedaret.IDSKEDARET = -1;
                        tempSkedaret.LLOJI = "dokGeo";
                        tempSkedaret.FILETYPE = MimeMapping.GetMimeMapping(fileName) != "" ? MimeMapping.GetMimeMapping(fileName) : "text/xml";
                        tempSkedaret.PATH = "UploadFiles/dokGeo/";
                        tempSkedaret.FILENAME = result.PershkrimMesazhi;
                        tempSkedaret.SHENIME = context.Request["emriSkedarit"];
                        tempSkedaret.IDSTATUSDOK = 1;
                        tempSkedaret.DTKRIJIMI = DateTime.Now;
                        tempSkedaret.IDKRIJUESI = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);                         
                        tempSkedaret.DTMODIFIKIMI = DateTime.Now;
                        tempSkedaret.IDPERDORUESI = 0;
                        tempSkedaret.PRAPASHTESESKEDAR = fileEx;
                        tempSkedaret.IDDYTESORE = context.Request["vleraUnikeIdDytesore"];
                        result = tempSkedaret.ruaj();
                        if (result.Status)
                        {
                            result.PershkrimMesazhi = clsJSONHelper.Serialize<clsSkedaretGIS>(tempSkedaret);
                        }
                    }
                }
            }
            else
            {
                result.Status = false;
                result.PershkrimMesazhi = "KUJDES: Nuk keni zgjedhur file!";
            }

            clsMesazhPerExtResult resultPerExt = new clsMesazhPerExtResult(result);

            Response.Clear();
            Response.Write(clsJSONHelper.Serialize<clsMesazhPerExtResult>(resultPerExt));
            Response.End();
        }

    }

    public class clsMesazhPerExtResult
    {
        public clsMesazhPerExtResult()
        {
        }
        public bool success { get; set; }
        public string message { get; set; }

        public clsMesazhPerExtResult(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }
        public clsMesazhPerExtResult(clsMesazh ishtePergj)
        {
            this.success = ishtePergj.Status;
            this.message = ishtePergj.PershkrimMesazhi;
        }
    }
}