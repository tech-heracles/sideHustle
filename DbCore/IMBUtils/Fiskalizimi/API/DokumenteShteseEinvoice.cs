using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.IMBUtils.Fiskalizimi.API
{

    public class DokumentShteseEinvoice
    {
        public static DokumentShteseEinvoice Create(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            string DocumentiEinvoiece = "";

            Byte[] bytes = File.ReadAllBytes(filePath);
            string base64Data = Convert.ToBase64String(bytes);
            string mimeCodeType = "";
            var model = new DokumentShteseEinvoice
            {
                Base64Data = base64Data,
                FileName = fileInfo.Name,
                FileExtension = fileInfo.Extension.Trim('.'),
                MimeCodeType = mimeCodeType

            };


            if (model.FileExtension == "pdf")
            {
                model.MimeCodeType = "application";
            }
            else if (model.FileExtension == "png" || model.FileExtension == "jpeg")
            {
                model.MimeCodeType = "image";
            }
            else if (model.FileExtension == "csv")
            {
                model.MimeCodeType = "text";
            }
            return model;

        }

        public static int _id = 1;

        public string ID { get; } = _id + DateTime.Now.ToString("dd-MM-yyyy");
        public string Base64Data { get; private set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }
        public string MimeCodeType { get; private set; }

        public DokumentShteseEinvoice()
        {
            _id++;
        }

        public string ToXml()
        {
            var xml = $"<ns3:AdditionalDocumentReference><ID>{ID}</ID><ns3:Attachment><EmbeddedDocumentBinaryObject mimeCode=\"{MimeCodeType}/{FileExtension}\" filename=\"{FileName}\">{Base64Data}</EmbeddedDocumentBinaryObject></ns3:Attachment></ns3:AdditionalDocumentReference>";
            return xml;
        }
    }
}
