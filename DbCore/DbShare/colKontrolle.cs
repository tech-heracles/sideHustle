using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
    public class colKontrolle : System.Collections.Generic.List<clsKontroll>
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        public colKontrolle()
        { 
            
        }


        public colKontrolle(int idGjuha, int idKomp, int idKonfigurim)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                mbushKontrollet(data.merrKontrolletKonfigurimit(idGjuha, idKomp, idKonfigurim));
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSpTrupi"></param>
        /// <returns></returns>
        public bool merrKontrolletParametrit(int idSpTrupi)
        {
            clsDatabaseShare shareDb = new clsDatabaseShare();
            bool mbush = mbushKontrollet(shareDb.merrKontrolleTeParametrit(idSpTrupi));
            shareDb.Dispose();
            return mbush;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idKomponente"></param>
        /// <returns></returns>
        public bool merrKontrolletKomponentes(int idKomponente)
        {
            clsDatabaseShare shareDb = new clsDatabaseShare();
            bool mbush = mbushKontrollet(shareDb.merrKontrolletKomponentes(idKomponente));
            shareDb.Dispose();
            return mbush;
        }

        public bool merrKontrollet(int idParameter)
        {
            try {
                using (clsDatabaseShare shareDb = new clsDatabaseShare())
                {
                    return mbushKontrollet(shareDb.merrKontrolletEParametrit(idParameter));
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Merr kontrollet e raportit
        /// </summary>
        public bool merrKontrolletRaporti(int idRaporti)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushKontrollet(data.merrKontrolletERaportit(idRaporti));
            data.Dispose();
            return mbush;
        }

        /// <summary>
        /// Merr kontrollet e raportit
        /// </summary>
        public bool merrKontrolletRaportiSipasIdSp(int idSp)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushKontrollet(data.merrKontrolletRaportiSipasIdSp(idSp));
            data.Dispose();
            return mbush;
        }

        public clsKontroll merrKontrollin(int idKontrolli)
        {
            foreach (clsKontroll kontrolli in this)
                if (kontrolli.IdKontrolli == idKontrolli)
                    return kontrolli;
            return null;
        }

        /// <summary>
        /// Kthen sa eshte numri i grupeve ne koleksion
        /// </summary>
        /// <returns></returns>
        public colGrupKontrolli merrGrupet()
        {
            colGrupKontrolli grupeKontrolli = new colGrupKontrolli();
            foreach (clsKontroll kontroll in this)
            {
                if (!grupeKontrolli.ekziston(kontroll.IdGrupi))
                    grupeKontrolli.Add(new clsGrupKontrolli(kontroll.IdGrupi));
            }
            return grupeKontrolli;
        }

        /// <summary>
        /// Kontrollon ne koleksion nese ekziston kontrolli me id-ne e dhene
        /// </summary>
        /// <param name="idKontrolli">id-ja e kontrollit</param>
        /// <returns>True nese kontrolli ekziston ne koleksion, false perndryshe</returns>
        public bool ekziston(int idKontrolli)
        {
            foreach (clsKontroll kontrolli in this)
                if (kontrolli.IdKontrolli == idKontrolli)
                    return true;
            return false;
        }

        #endregion

        #region Metoda Private

        private bool mbushKontrollet(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    this.Add(new clsKontroll(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
        [Obsolete("Nuk perdoret me: perdor mbushKontrollet(DataTable) dhe mbushkontrollin(DataRow)", true)]
        public colKontrolle mbushArrayListDokumentLidhesKoka(DataSet ds)
        {
            colKontrolle col = new colKontrolle();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKontroll koka = new clsKontroll();

                koka.IdKontrolli = int.Parse(rreshti[0].ToString());
                koka.IdKomponente = int.Parse(rreshti[1].ToString());
                koka.KodKontrolli= rreshti[2].ToString();
                koka.PershkrimKontrolli = rreshti[3].ToString();
               // koka.KontrollTipi = int.Parse(rreshti[4].ToString());
                try
                {
                    int tipi;
                    koka.IdTipiKontrollit = int.TryParse(rreshti[4].ToString(), out tipi) ? tipi : -1;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se tipit te kontrollit nga db-ja");
                }
                col.Add(koka);
            }
            return col;
        }
    }
}
