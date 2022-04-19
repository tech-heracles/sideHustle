using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbAdmin;
using System.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbShare
{
    public class clsKusht
    {
        #region Atribute

        private int idKusht;
        private string kodi;
        private string pershkrimi;
        private int vlera;
        private int idKonfigAmbjente;
        private int idKushtTemplate;
        private DataRow rreshti;
        private List<clsKonfLlojRreshtiVlere> colKonfLlojRreshtiVlere;

        #endregion

        #region Konstruktoret

        public clsKusht(int idKu, string kod, string pershk, int vl, int idKonfAmbj, int idKushtTempl)
        {
            idKusht = idKu;
            kodi = kod;
            pershkrimi = pershk;
            vlera = vl;
            idKonfigAmbjente = idKonfAmbj;
            idKushtTemplate = idKushtTempl;
        }

        public clsKusht()
        {
        }

        public clsKusht(int idKonfigurim, string kodkusht)
        {
            using (clsDatabaseShare db = new clsDatabaseShare())
            {
                merrTeGjitheKushteKonfigurimiDheKodKushti(idKonfigurim, kodkusht, db);
            }
        }

        public clsKusht(int idKonfigurim, string kodkusht, clsDatabaseShare db)
        {
            mbushKusht(db.TransCache.getKusht(kodkusht, idKonfigurim, db));
        }

        public clsKusht(DataRow rreshti)
        {

            mbushKusht(rreshti);
        }

        #endregion

        #region Properties

        public int IdKusht
        {
            get { return idKusht; }
            set { idKusht = value; }
        }

        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        public int Vlera
        {
            get { return vlera; }
            set { vlera = value; }
        }

        public int IdKushtTemplate
        {
            get { return idKushtTemplate; }
            set { idKushtTemplate = value; }
        }

        public int IdKonfigurimAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }

        public List<clsKonfLlojRreshtiVlere> ColKonfLlojRreshtiVlere
        {
            get
            {
                return colKonfLlojRreshtiVlere;
            }
            set
            {
                colKonfLlojRreshtiVlere = value;
            }
        }

        #endregion

        #region Metoda Publike

        public static clsMesazh krijoKushtTemplate(int idKonfigurim, string kodkusht, int vlera, clsDatabaseShare db)
        {
            if (!db.krijoKushtTemplate(idKonfigurim, kodkusht, vlera))
                return new clsMesazh(false, "Ndodhi nje gabim gjate krijimit te kushtit template");
            return new clsMesazh(true, "Kushti Template u krijua me sukses");
        }

        public static int ktheVlereKushti(int idkonfig, string kodKusht, clsDatabaseShare dbShare)
        {
            return new clsKusht(idkonfig, kodKusht, dbShare).Vlera;            //eshte me cache
        }
        public void krijoKusht(Dictionary<string, object> rresht, string prioriteti)
        {
            IdKusht = int.Parse(rresht["IdKusht"].ToString());
            IdKonfigurimAmbjente = int.Parse(rresht["IdKonfigurimAmbjente"].ToString());
            if (rresht["Kodi"].ToString() == "LLD")
            {
                vlera = 0;
                ColKonfLlojRreshtiVlere = new List<clsKonfLlojRreshtiVlere>();
                var idte = prioriteti.Split(',');
                for (int i = 0; i < idte.Length; i++)
                {
                    if (String.IsNullOrEmpty(idte[i]))
                        continue;
                    clsKonfLlojRreshtiVlere konfRreshti = new clsKonfLlojRreshtiVlere();
                    konfRreshti.IdLlojRreshti = int.Parse(idte[i]);
                    konfRreshti.IdKushTemplate = int.Parse(rresht["IdKushtTemplate"].ToString());
                    konfRreshti.Rend = i + 1;
                    ColKonfLlojRreshtiVlere.Add(konfRreshti);
                    vlera = 1; //mjafton te kete nje vlere cfaredo ne kete rast, pasi konfigurimet ruhen ne tabele tjeter
                }
                //if (String.IsNullOrEmpty(prioriteti))
                //    vlera = 0;
                //else vlera = int.Parse(prioriteti.Replace(",", String.Empty));
            }
            else
                if (rresht["Vlera"] != null) int.TryParse(rresht["Vlera"].ToString(), out vlera);
            else Vlera = 0;
            Pershkrimi = rresht["Pershkrimi"].ToString();
            Kodi = rresht["Kodi"].ToString();
            IdKushtTemplate = int.Parse(rresht["IdKushtTemplate"].ToString());
        }

        /// <summary>
        /// ruan nje objekt te tipit kusht
        /// </summary>
        /// <returns>nje objekt clsMesazh me statusin e perfundimit te ruajtjes</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            int id = 0;
            clsMesazh u_ruajt = data.ruajKushtTemplate(out id, this.IdKonfigurimAmbjente, this.IdKusht, this.Vlera);
            this.idKushtTemplate = id;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// modifikon nje objekt te tipit kusht
        /// </summary>
        /// <returns>nje objekt clsMesazh me statusin e perfundimit te modifikimit</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_modifikua = data.modifikoKushtTemplate(this.IdKushtTemplate, this.Vlera);
            data.Dispose();
            return u_modifikua;
        }
        /// <summary>
        /// modifikon nje objekt te tipit kusht si pjese e nje trasaksioni
        /// </summary>
        /// <param name="data">clsDatabaseshare per transaksionin </param>
        /// <returns>nje objekt clsMesazh me statusin e perfundimit te modifikimit</returns>
        public clsMesazh modifiko(clsDatabaseShare data)
        {
            clsMesazh u_modifikua = data.modifikoKushtTemplate(this.IdKushtTemplate, this.Vlera);

            return u_modifikua;
        }

        public clsMesazh fshi()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_fshi = data.fshiKushtTemplate(this.IdKushtTemplate);
            data.Dispose();
            return u_fshi;
        }

        public colKusht merrTeGjitheKushteKonfigurimi(int idKonfigurim) //kalohet IDKONFIGAMBJENTE
        {
            //this.idKonfigAmbjente = idKonfigurim;
            colKusht data = new colKusht();
            data.mbushGjitheKushteKonfigurimi(this.IdKonfigurimAmbjente);
            return data;
        }

        public bool merrTeGjitheKushteKonfigurimiDheKodKushti(int idKonfigurim, string kodkusht, clsDatabaseShare db) //kalohet IDKONFIGAMBJENTE
        {
            return mbushKusht(db.ktheKushtTemplateSipasIDkonfigurimdheKodKushti(idKonfigurim, kodkusht));
        }

        public static int kthevlereSipasKushtitDheIdKonfig(int idKonfigurim, string kodKusht)
        {
            using (clsDatabaseShare db = new clsDatabaseShare())
            {
                return db.kthevlereSipasKushtitDheIdKonfig(idKonfigurim, kodKusht);
            }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushKusht(DataRow dbDataRowKusht)
        {
            if (dbDataRowKusht != null)
            {
                try
                {
                    idKusht = int.Parse(dbDataRowKusht["IDKUSHT"].ToString());
                    kodi = dbDataRowKusht["KODI"].ToString();
                    pershkrimi = dbDataRowKusht["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowKusht["VLERA"].ToString(), out vlera);
                    idKushtTemplate = int.Parse(dbDataRowKusht["IDKUSHTEMPLATE"].ToString());
                    idKonfigAmbjente = int.Parse(dbDataRowKusht["IDKONFIGAMBJENTE"].ToString());
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se konfigurim ambjenti nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se konfigurim ambjenti nga db-ja");
                }
            }
            else
                return false;
        }

        public clsMesazh mbushKusht(clsKusht kusht)
        {
            idKusht = kusht.IdKusht;
            kodi = kusht.Kodi;
            pershkrimi = kusht.Pershkrimi;
            vlera = kusht.Vlera;
            idKushtTemplate = kusht.IdKushtTemplate;
            idKonfigAmbjente = kusht.IdKonfigurimAmbjente;
            return new clsMesazh(true, $"Mbushja e kushtit {kusht.Kodi} per konfigurimin me id {kusht.IdKonfigurimAmbjente} u krye me sukses!");
        }

        #endregion

    }
}