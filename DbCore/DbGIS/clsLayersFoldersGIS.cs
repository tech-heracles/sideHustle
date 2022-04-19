using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsLayersFoldersGIS
    {
        #region Atribute

        private int     varIdFolder;
        private string  varFolderNameSq;
        private string  varFolderNameEn;
        private string  varFolderNameIt;
        private float   varRenditja;
        private string  varFolderKod;
        private int     varIdPrindi;
        private int     varExpand;

        private string  varFolderName;

        #endregion

        #region Konstruktoret
        public clsLayersFoldersGIS()
        {
        }

        public clsLayersFoldersGIS(DataRow db)
        {
            mbushLayersFolders(db);
        }
        public clsLayersFoldersGIS(int idFolder, string nameSq, string nameEn, string nameIt, float renditja, string kodi, int idPrindi, int expand, string name)
        {
            this.IdFolder = idFolder;
            this.FolderNameSq = nameSq;
            this.FolderNameEn = nameEn;
            this.FolderNameIt = nameIt;
            this.Renditja = renditja;
            this.FolderKod = kodi;
            this.IdPrindi = idPrindi;
            this.Expand = expand;
            this.FolderName = name;
        }
        #endregion

        #region Properties
        public int IdFolder
        {
            get { return varIdFolder; }
            set { varIdFolder = value; }
        }
        public string FolderNameSq
        {
            get { return varFolderNameSq; }
            set { varFolderNameSq = value; }
        }
        public string FolderNameEn
        {
            get { return varFolderNameEn; }
            set { varFolderNameEn = value; }
        }
        public string FolderNameIt
        {
            get { return varFolderNameIt; }
            set { varFolderNameIt = value; }
        }
        public float Renditja
        {
            get { return varRenditja; }
            set { varRenditja = value; }
        }
        public string FolderKod
        {
            get { return varFolderKod; }
            set { varFolderKod = value; }
        }
        public int IdPrindi
        {
            get { return varIdPrindi; }
            set { varIdPrindi = value; }
        }
        public int Expand
        {
            get { return varExpand; }
            set { varExpand = value; }
        }
        public string FolderName
        {
            get { return varFolderName; }
            set { varFolderName = value; }
        }

        #endregion

        #region Metoda Publike
        public clsLayersFoldersGIS findLayerByName(colLayersFoldersGIS layeratFol, string fusha)
        {
            if (layeratFol.Exists(x => (x.varFolderName == fusha)))
            {
                return layeratFol.Find(x => (x.varFolderName == fusha));
            }
            else
            {
                return new clsLayersFoldersGIS();
            }
        }
        
        #endregion

        #region Metoda Internal
        internal bool mbushLayersFolders(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["id_folder"].ToString(), out varIdFolder);
                    varFolderNameSq = db["folder_name_sq"].ToString();
                    varFolderNameEn = db["folder_name_en"].ToString();
                    varFolderNameIt = db["folder_name_it"].ToString();
                    float.TryParse(db["renditja"].ToString(), out varRenditja);
                    varFolderKod = db["kodi_folderit"].ToString();
                    int.TryParse(db["IDPRIND"].ToString(), out varIdPrindi);
                    int.TryParse(db["EXPANDED"].ToString(), out varExpand);

                    varFolderName = db["folder_name"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se skedareve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
