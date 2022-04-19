using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsPlanifikimRealizim
    {
        #region atribute
        private int rowNum;
        private int prId;
        private int idArtikulli;
        private string kodArtikulli;
        private string emertimi;
        private string njesia;
        private float sasiaMiratuar;
        private float sasiaKerkuar;
        private float sasiaTerhequr;
        private float sasiaMbetur;
        private int idNdermarrje;
        private int idNdermVit;
        private int idKrijuesi;
        private int idModifikuesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private float janar;
        private float shkurt;
        private float mars;
        private float prill;
        private float maj;
        private float qershor;
        private float korrik;
        private float gusht;
        private float shtator;
        private float tetor;
        private float nentor;
        private float dhjetor;
        #endregion atribute

        #region properties
        public int RowNum {
            get { return rowNum; } }
        public int IdArtikulli
        {
            get { return idArtikulli; }
            set { idArtikulli = value; }
        }
        public int PrId
        {
            get { return prId; }
            set { prId = value; }
        }
        public string KodArtikulli
        {
            get { return kodArtikulli; }
            set { kodArtikulli = value; }
        }
        public string Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }
        public string Njesia
        {
            get { return njesia; }
            set { njesia = value; }
        }
        public float SasiaKerkuar
        {
            get { return sasiaKerkuar; }
            set { sasiaKerkuar = value; }
        }
        public float SasiaMiratuar
        {
            get { return sasiaMiratuar; }
            set { sasiaMiratuar = value; }
        }
        public float SasiaTerhequr
        {
            get { return sasiaTerhequr; }
            set { sasiaTerhequr = value; }
        }
        public float SasiaMbetur
        {
            get { return sasiaMbetur; }
            set { sasiaMbetur = value; }
        }
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }
        public int IdModifikuesi
        {
            get { return idModifikuesi; }
            set { idModifikuesi = value; }
        }
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }
        public int IdNdermVit
        {
            get { return idNdermVit; }
            set { idNdermVit = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }
        public float Janar
        {
            get { return janar; }
            set { janar = value; }
        }
        public float Shkurt
        {
            get { return shkurt; }
            set { shkurt = value; }
        }
        public float Mars
        {
            get { return mars; }
            set { mars = value; }
        }
        public float Prill
        {
            get { return prill; }
            set { prill = value; }
        }
        public float Maj
        {
            get { return maj; }
            set { maj = value; }
        }
        public float Qershor
        {
            get { return qershor; }
            set { qershor = value; }
        }
        public float Korrik
        {
            get { return korrik; }
            set { korrik = value; }
        }
        public float Gusht
        {
            get { return gusht; }
            set { gusht = value; }
        }
        public float Shtator
        {
            get { return shtator; }
            set { shtator = value; }
        }
        public float Tetor
        {
            get { return tetor; }
            set { tetor = value; }
        }
        public float Nentor
        {
            get { return nentor; }
            set { nentor = value; }
        }
        public float Dhjetor
        {
            get { return dhjetor; }
            set { dhjetor = value; }
        }
        #endregion properties

        #region metoda internal
        internal void Mbush(IDataRecord record)
        {
            try
            {
                int.TryParse(record["IdArtikulli"].ToString(), out idArtikulli);
                int.TryParse(record["RowNum"].ToString(), out rowNum);
                int.TryParse(record["PrId"].ToString(), out prId);
                KodArtikulli = record["KodArtikulli"].ToString();
                Emertimi = record["Emertimi"].ToString();
                Njesia = record["Njesia"].ToString();
                float.TryParse(record["SasiaKerkuar"].ToString(), out sasiaKerkuar);
                float.TryParse(record["SasiaMbetur"].ToString(), out sasiaMbetur);
                float.TryParse(record["SasiaMiratuar"].ToString(), out sasiaMiratuar);
                float.TryParse(record["SasiaTerhequr"].ToString(), out sasiaTerhequr);
                int.TryParse(record["IdKrijuesi"].ToString(), out idKrijuesi);
                int.TryParse(record["IdModifikuesi"].ToString(), out idModifikuesi);
                int.TryParse(record["IdNdermarrje"].ToString(), out idNdermarrje);
                int.TryParse(record["IdNdermVit"].ToString(), out idNdermVit);
                int.TryParse(record["IdStatusDok"].ToString(), out idStatusDok);
                DateTime.TryParse(record["DtKrijimi"].ToString(), out dtKrijimi);
                DateTime.TryParse(record["DtModifikimi"].ToString(), out dtKrijimi);

                float.TryParse(record["Janar"].ToString(), out janar);
                float.TryParse(record["Shkurt"].ToString(), out shkurt);
                float.TryParse(record["Mars"].ToString(), out mars);
                float.TryParse(record["Prill"].ToString(), out prill);
                float.TryParse(record["Maj"].ToString(), out maj);
                float.TryParse(record["Qershor"].ToString(), out qershor);
                float.TryParse(record["Korrik"].ToString(), out korrik);
                float.TryParse(record["Gusht"].ToString(), out gusht);
                float.TryParse(record["Shtator"].ToString(), out shtator);
                float.TryParse(record["Tetor"].ToString(), out tetor);
                float.TryParse(record["Nentor"].ToString(), out nentor);
                float.TryParse(record["Dhjetor"].ToString(), out dhjetor);
            }
            catch (InvalidCastException ex)
            {
                ImbLogger.Info($"Error ne marrjen e te dhenave nga databaza: {ex.Message}");
            }
        }

        internal static clsPlanifikimRealizim Krijo(IDataRecord record)
        {
            clsPlanifikimRealizim pr = new clsPlanifikimRealizim();
            pr.Mbush(record);
            return pr;

        }
        #endregion metoda internal
    }
}
