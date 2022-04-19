using System;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colNdermarrjet : System.Collections.Generic.List<clsNdermarrje>
    {
        #region Konstruktoret

        public colNdermarrjet()
        {
        }

        public colNdermarrjet(int idRoli)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushNdermarrjet(data.ktheNdermarjeTeRolit(idRoli));
            data.Dispose();
        }        

        public colNdermarrjet(int idRoli, clsDatabaseAdmin data)
        {
            if (data == null)
                data = new clsDatabaseAdmin();
            mbushNdermarrjet(data.ktheNdermarjeTeRolit(idRoli));
        }

        #endregion

        #region Metoda Publike

        public new clsNdermarrje this[int index]
        {
            get { return ((clsNdermarrje)base[index]); }
        }

        /// <summary>
        /// Merr listen e ndermarrjeve nga db-ja dhe mbush colection-in
        /// </summary>
        public void merrNdermarrjet(int idperdoruesi, int idlicenca)
        {
            clsDatabaseAdmin dbadmin = new clsDatabaseAdmin();
            DataTable dt = dbadmin.merrNdermarrjet(idperdoruesi, idlicenca);
            if (!mbushNdermarrjet(dt))
            {
                dbadmin.Dispose();
                throw new Exception("ERROR: Colection-i i ndermarrjeve nuk u mbush dot");
            }
            dbadmin.Dispose();
        }

        public void merrNdermarrjetMeme(int idlicenca)
        {
            clsDatabaseAdmin dbadmin = new clsDatabaseAdmin();
            DataTable dt = dbadmin.merrNdermarrjeMeme(idlicenca);
            if (!mbushNdermarrjet(dt))
            {
                dbadmin.Dispose();
                throw new Exception("ERROR: Colection-i i ndermarrjeve nuk u mbush dot");
            }
            dbadmin.Dispose();
        }

        public bool shtoNdermarrje(clsNdermarrje ndermarrje)
        {
            base.Add(ndermarrje);
            if (base.Contains(ndermarrje))
                return true;
            else return false;
        }

        public bool fshiNdermarrje(clsNdermarrje ndermarrje)
        {
            base.Remove(ndermarrje);
            if (base.Contains(ndermarrje))
                return false;
            else return true;
        }

        public bool fshiGjitheNdermarrjet()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteNdermarrje(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoNdermarrjeNeIndeksin(int index, clsNdermarrje ndermarrje)
        {
            base.Insert(index, ndermarrje);
        }

        public int indeksiNdermarrjes(clsNdermarrje ndermarrje)
        {
            return base.IndexOf(ndermarrje);
        }

        public bool ekzistonNdermarrja(clsNdermarrje ndermarrje)
        {
            if (base.Contains(ndermarrje))
                return true;
            else return false;
        }

        public int numriNdermarrjeve()
        {
            return base.Count;
        }
        public static DataRow merrSipasNdermarjeDR(int idndermarje)
        {
            using (clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin())
            {
                return dbartikuj.ktheNdermarjeDR(idndermarje);
            }
        }
        public static DataTable merrNdermarjetDT(int idperdoruesi, int idlicenca)
        {
            using (clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin())
            {
                return dbartikuj.ktheNdermarjetDT(idperdoruesi, idlicenca);
            }
        }
        public static DataTable ktheNdermarjeBijSipasMemeDT(int idnderma)
        {
            using (clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin())
            {
                return dbartikuj.ktheNdermarjeBijSipasMemeDT(idnderma);
            }
        }

        public static DataTable ktheNdermarjeBijSipasNivelitDT(int idNdermarrje, int nivelStrukture)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ktheNdermarjeBijSipasNivelitDT(idNdermarrje, nivelStrukture);
            }
        }

        public static DataTable ktheNdermarjeBijSipasMemeDheOwnDT(int idnderma)
        {
            using (clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin())
            {
                return dbartikuj.ktheNdermarjeBijSipasMemeDheOwnDT(idnderma);
            }
        }
        public static DataTable ktheNdermarjeBijSipasNdermRaportueseDT(int idnderm)
        {
            using (clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin())
            {
                return dbartikuj.ktheNdermarjeBijSipasNdermRaportueseDT(idnderm);
            }
        }
        public static DataRow merrSipasNdermarjeDheViteDR(int idnderviti)
        {
            using (clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin())
            {
                return dbartikuj.ktheNdermarjeDheViteDR(idnderviti);
            }
        }
        public static DataTable merrNdermarjetDheViteDT(int idperdoruesi, int idlicenca)
        {
            using (clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin())
            {
                return dbartikuj.ktheNdermarjetDheViteDT(idperdoruesi, idlicenca);
            }
        }
        public bool mbushGjitheNdermarrjet(int idperdoruesi, int idlicenca)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushNdermarrjet(data.ktheGjitheNdermarrjet(idperdoruesi, idlicenca));
            }
        }

        public bool mbushNdermarrjeDefault()
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushNdermarrjet(data.merrNdermarrjeDefault());
            }
        }

        /// <summary>
        /// Merr gjithe ndermarrjet e llojeve te licensave ''Zyre'' ose ''SAGIS''
        /// </summary>
        /// <param name="tePalidhura">True/False kerkon per te gjithe ndermarrjet apo vetem ato qe nuk kane akoma nje workspace te lidhur me te</param>
        /// <returns>true ose false nese mbushja e klases u krye me sukses apo jo</returns>
        public bool mbushNdermarrjePerGIS(bool tePalidhura)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushNdermarrjetList(data.merrNdermarrjePerGIS(tePalidhura));
            }
        }

        public bool mbushNdermarrjeList(DataTable dt)
        {
            return mbushNdermarrjetList(dt);
        }

        public bool mbushNdermarrjetPerdoruesit(int idPerdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushNdermarrjet(data.merrNdermarrjetEPerdoruesitDataTable(idPerdorues));
            }
        }

        public static DataTable merrNdermarrjetIdRoli(int idRoli, int idPerdoruesi)
        {
            using (var data = new clsDatabaseAdmin())
            {
                return data.merrNdermarrjetIdRoli(idRoli, idPerdoruesi);
            }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Metode private qe do perdoret vetem brenda klases per te mbushur colectionin nga nje dataTable
        /// </summary>
        /// <param name="dt">dataTable-i me te dhenat qe do mbushet colection-i</param>
        private bool mbushNdermarrjet(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsNdermarrje ndermarrje = new clsNdermarrje();
                //ndermarrje.mbushNdermarrja(rreshti);
                Add(new clsNdermarrje(rreshti));
            }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// Metode private qe do perdoret vetem brenda klases per te mbushur colectionin nga nje dataTable
        /// </summary>
        /// <param name="dt">dataTable-i me te dhenat qe do mbushet colection-i</param>
        private bool mbushNdermarrjetList(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                clsNdermarrje ndermarrje = new clsNdermarrje();
                ndermarrje.mbushNdermarrjaList(rreshti);
                Add(ndermarrje);
            }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushNdermarrjet(DataTable dt)", true)]
        public colNdermarrjet mbushArrayListNdermarrjet(DataSet ds)
        {
            colNdermarrjet ndermarrjet = new colNdermarrjet();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsNdermarrje ndermarrje = new clsNdermarrje();

                ndermarrje.IdNdermarrje = int.Parse(rreshti[0].ToString());
                ndermarrje.NdermarrjeKodi = rreshti[1].ToString();
                ndermarrje.NdermarrjePershkrimi = rreshti[2].ToString();
                ndermarrje.NdermarrjeVendi = rreshti[3].ToString();
                ndermarrje.NdermarrjeNipt = rreshti[4].ToString();
                ndermarrje.NdermarrjeMonedha = int.Parse(rreshti[5].ToString());
                ndermarrje.NdermarrjeQyteti = int.Parse(rreshti[6].ToString());
                ndermarrje.NdermarrjeTel = rreshti[7].ToString();
                ndermarrje.NdermarrjeFax = rreshti[8].ToString();
                ndermarrje.NdermarrjeEMail = rreshti[9].ToString();
                ndermarrje.NdermarrjeLicenca = rreshti[11].ToString();
                ndermarrje.NdermarrjeKodiFiskal = rreshti[12].ToString();
                ndermarrje.IdPerdoruesi = int.Parse(rreshti[13].ToString());
                ndermarrje.IdViti = int.Parse(rreshti[14].ToString());
                ndermarrje.LlojNdermarje = int.Parse(rreshti[15].ToString());

                ndermarrjet.Add(ndermarrje);
            }
            return ndermarrjet;
        }
        [Obsolete("Perdor: bool mbushNdermarrjetList(DataTable dt)", true)]
        public colNdermarrjet mbushArrayListNdermarrjetList(DataSet ds)
        {
            colNdermarrjet ndermarrjet = new colNdermarrjet();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsNdermarrje ndermarrje = new clsNdermarrje();

                ndermarrje.IdNdermarrje = int.Parse(rreshti[0].ToString());
                ndermarrje.NdermarrjeKodi = rreshti[1].ToString();
                ndermarrje.NdermarrjePershkrimi = rreshti[2].ToString();
                ndermarrjet.Add(ndermarrje);
            }
            return ndermarrjet;
        }
    }
}