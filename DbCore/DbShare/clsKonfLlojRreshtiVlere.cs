using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
    public class clsKonfLlojRreshtiVlere
    {
        #region Attributes
        private int idKushTemplate;
        private int idLlojRreshti;
        private string kodLlojRreshti;
        private int rend;
        #endregion

        #region Properties
        public int IdKushTemplate
        {
            get
            {
                return idKushTemplate;
            }
            set
            {
                if (idKushTemplate == value)
                    return;
                idKushTemplate = value;
            }
        }
        public int IdLlojRreshti
        {
            get
            {
                return idLlojRreshti;
            }
            set
            {
                if (idLlojRreshti == value)
                    return;
                idLlojRreshti = value;
            }
        }
        public string KodLlojRreshti
        {
            get
            {
                return kodLlojRreshti;
            }
            set
            {
                if (kodLlojRreshti == value)
                    return;
                kodLlojRreshti = value;
            }
        }
        public int Rend
        {
            get
            {
                return rend;
            }
            set
            {
                if (rend == value)
                    return;
                rend = value;
            }
        }
        #endregion
        public clsKonfLlojRreshtiVlere() { }
        public clsKonfLlojRreshtiVlere(int idLlojRreshti)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            if (!mbushKofLlojRreshtiVlere(data.merrKonfLlojRreshtiVlere(idLlojRreshti)))
                throw new Exception("Gabim gjate leximit te llojit te rreshtit nga db-ja");
            data.Dispose();
        }
        /// <summary>
        /// kthen id-ne perkatese sipas kodit
        /// </summary>
        /// <param name="kodLlojRreshti"></param>
        /// <returns></returns>
        public static int ktheIdLlojRreshtiVlere(string kodLlojRreshti, string lloji)
        {
            if (lloji == "Shitje")
                return (int)(llojRreshtiShitje)System.Enum.Parse(typeof(llojRreshtiShitje), kodLlojRreshti);
            if (lloji == "ArkaBanka")
                return (int)(llojRreshtiArkeBanke)System.Enum.Parse(typeof(llojRreshtiArkeBanke), kodLlojRreshti);
            return 0;
            //using (clsDatabaseShare data = new clsDatabaseShare())
            //{
            //    return data.merrKonfLlojRreshtiVlere(kodLlojRreshti, lloji);                
            //}
        }
        public clsMesazh ruaj()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh mesazh = data.ruajKonfLlojRreshti(this.idKushTemplate, this.idLlojRreshti, this.rend);
            data.Dispose();
            return mesazh;
        }
        public clsMesazh fshi()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            return data.fshikonfllojreshti(this.idKushTemplate);
        }
        internal bool mbushKofLlojRreshtiVlere(DataRow row)
        {

            idKushTemplate = int.TryParse(row["idkushtemplate"].ToString(), out idKushTemplate) ? idKushTemplate : 0;
            if (!int.TryParse(row["IDLLOJRRESHTI"].ToString(), out idLlojRreshti))
                return false;
            kodLlojRreshti = row["kodllojrreshti"].ToString();
            rend = int.TryParse(row["rend"].ToString(), out rend) ? rend : -1;
            return true;
        }        
    }
}
