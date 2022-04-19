using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbIntegrime
{

    public class clsJobAutomatikeParametra : IDataBase
    {
        #region Atribute

        private int idSkeduleri;
        private int renditjaParametri;
        private string emriParametrit;
        private string vleraParametrit;

        #endregion

        #region Properties
        public int IdSkeduleri
        {
            get { return idSkeduleri; }
            set { idSkeduleri = value; }
        }

        public int RenditjaParamentri
        {
            get { return renditjaParametri; }
            set { renditjaParametri = value; }
        }

        public string EmriParametrit
        {
            get { return emriParametrit; }
            set { emriParametrit = value; }
        }

        public string VleraParametrit
        {
            get { return vleraParametrit; }
            set { vleraParametrit = value; }
        }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsJobAutomatikeParametra()
        {
        }

        public clsJobAutomatikeParametra(IDataRecord record)
        {
            Mbush(record);
        }

        #endregion

        #region MetodatPublike

        public clsMesazh Ruaj()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            int.TryParse(record["ID_SKEDULERI"].ToString(), out idSkeduleri);
            int.TryParse(record["RENDITJAPARAMETRI"].ToString(), out renditjaParametri);
            emriParametrit = record["EMRIPARAMETRIT"].ToString();
            vleraParametrit = Convert.ToString(record["VLERAPARAMETRIT"]);
        }

        #endregion
    }

}
