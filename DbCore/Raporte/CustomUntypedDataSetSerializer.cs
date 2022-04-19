using System.Data;
using System.IO;
using System.Xml;
using DevExpress.XtraReports.Native;

namespace DbCore.Raporte
{
    /// <summary>
    /// Custom serializer per te serializuar DataSet qe marrin raportet
    /// </summary>
    public class CustomUntypedDataSetSerializer : IDataSerializer {
        public const string Name = "CustomUntypedDataSetSerializer";

        public bool CanSerialize(object data, object extensionProvider) {
            return (data is DataSet);
        }

        public string Serialize(object data, object extensionProvider) {
            if (data is DataSet) {
                DataSet ds = data as DataSet;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                XmlWriter writer = XmlWriter.Create(sb);
                ds.WriteXml(writer, XmlWriteMode.WriteSchema);
                return sb.ToString();
            }
            return string.Empty;
        }

        public bool CanDeserialize(string value, string typeName, object extensionProvider) {
            return typeName == "System.Data.DataSet";
        }

        public object Deserialize(string value, string typeName, object extensionProvider) {
            DataSet ds = new DataSet("DataSource");
            using(XmlReader reader = XmlReader.Create(new StringReader(value))) {
                ds.ReadXml(reader, XmlReadMode.ReadSchema);
            }
            return ds;
        }
    }
}
