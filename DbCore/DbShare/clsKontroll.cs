using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
    public class clsKontroll
    {
        #region Attributet
        private int idKontrolli;
        private int idGrupi;
        private int idKomponente;
        private String kodKontrolli;
        private String pershkrimKontrolli;
        private int kontrollTipi;
        private int idTipiKontrollit;
        /// <summary>
        /// emri i cili do shfaqet ne ambjentin e importit
        /// </summary>
        private string emerImporti;
        /// <summary>
        /// tregon nese ky kontroll do shfaqet ne ambjentin e importit apo jo 
        /// </summary>
        private bool shfaqImporti;
        #endregion

        #region Properties
        public int IdKontrolli
        {
            get { return idKontrolli; }
            set { idKontrolli = value; }
        }

        public int IdKomponente
        {
            get { return idKomponente; }
            set { idKomponente = value; }
        }

        public String KodKontrolli
        {
            get { return kodKontrolli; }
            set { kodKontrolli = value; }
        }

        public String PershkrimKontrolli
        {
            get { return pershkrimKontrolli; }
            set { pershkrimKontrolli = value; }
        }
        public int IdGrupi
        {
            get
            {
                return idGrupi;
            }
            set
            {
                if (idGrupi == value)
                    return;
                idGrupi = value;
            }
        }
        [Obsolete("Perdor TipiKontrollit ne vend te kesaj",true)]
        public int KontrollTipi
        {
            get { return kontrollTipi; }
            set { kontrollTipi = value; }
        }
        /// <summary>
        /// get set Tipin e kontrollit
        /// </summary>
        public int IdTipiKontrollit
        {
            get
            {
                return idTipiKontrollit;
            }
            set
            {
                if (idTipiKontrollit == value)
                    return;
                idTipiKontrollit = value;
            }
        }
        /// <summary>
        /// emri i cili do shfaqet ne ambjentin e importit
        /// </summary>
        public string EmerImporti
        {
            get
            {
                return emerImporti;
            }
            set
            {
                emerImporti = value;
            }
        }
        /// <summary>
        /// tregon nese ky kontroll do shfaqet ne ambjentin e importit apo jo 
        /// </summary>
        public bool ShfaqImporti
        {
            get
            {
                return shfaqImporti;
            }
            set
            {
                shfaqImporti = value;
            }
        }
        #endregion

        #region konstruktoret
        /// <summary>
        /// Nderton objektin nga db-ja me id-ne qe merr ne input
        /// </summary>
        /// <param name="idKontrolli">id-ja e kontrollit</param>
        public clsKontroll(int idKontrolli)
        {
            using (clsDatabaseShare sharedb = new clsDatabaseShare())
            {
                mbushKontrollin(sharedb.merrKontrollin(idKontrolli));
            }
        }

        /// <summary>
        /// konstruktori i plote i klases
        /// </summary>
        /// <param name="idkontrolli"></param>
        /// <param name="idkomponente"></param>
        /// <param name="kodkontrolli"></param>
        /// <param name="pershkrimikontrolli"></param>
        /// <param name="idTipiKontrollit"></param>
        /// <param name="emerimporti">emri i cili do shfaqet ne ambjentin e importit</param>
        /// <param name="shfaqimporti">tregon nese do shfaqet tek ambjenti i importit apo jo </param>
        public clsKontroll(int idkontrolli,int idGrupi, int idkomponente, String kodkontrolli, String pershkrimikontrolli, int idTipiKontrollit, string emerimporti, bool shfaqimporti)
        {
            idKontrolli = idkontrolli;
            this.idGrupi = idGrupi;
            idKomponente = idkomponente;
            kodKontrolli = kodkontrolli;
            pershkrimKontrolli = pershkrimikontrolli;
            this.idTipiKontrollit = idTipiKontrollit;
            emerImporti = emerimporti;
            shfaqImporti = shfaqimporti;
        }

        public clsKontroll()
        { 
        }
        public clsKontroll(DataRow dbDataRowKontrolli)
        {
            mbushKontrollin(dbDataRowKontrolli);
        }
        #endregion

        #region Metoda Publike
        public clsMesazh ruaj()
        {
            return new clsMesazh();
        }

        public clsMesazh modifiko()
        {
            return new clsMesazh();
        }

        public clsMesazh fshi()
        {
            return new clsMesazh();
        }

        public static clsKontroll merrKontrollSipasKoditKomponentes(string kodKontroll, int idKomponente)
        {
            clsDatabaseShare sharedb = new clsDatabaseShare();
            clsKontroll k = new clsKontroll();
            k.mbushKontrollin(sharedb.merrKontrollinSipasKodit(kodKontroll, idKomponente));
            sharedb.Dispose();
            return k;
        }

        public static int ktheTipKontrolli(int idKontroll, clsDatabaseShare sharedb)
        {            
            return sharedb.ktheTipKontrolli(idKontroll);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbDataRowKontrolli">DataRow me te dhenat e kontrollit</param>
        /// <returns></returns>
        internal bool mbushKontrollin(DataRow dbDataRowKontrolli)
        {
            if (dbDataRowKontrolli != null)
            {
                try
                {
                    int.TryParse(dbDataRowKontrolli["IDKONTROLL"].ToString(),out idKontrolli);
                    int.TryParse(dbDataRowKontrolli["IDGRUP"].ToString(), out idGrupi);
                    int.TryParse(dbDataRowKontrolli["IDKOMPONENTE"].ToString(), out idKomponente);
                    KodKontrolli = dbDataRowKontrolli["KODKONTROLL"].ToString();
                    PershkrimKontrolli = dbDataRowKontrolli["PERSHKRIMKONTROLL"].ToString();                    
                    int.TryParse(dbDataRowKontrolli["TIPI"].ToString(), out idTipiKontrollit); 
                    EmerImporti = dbDataRowKontrolli["EMERIMPORTI"].ToString();    
                    bool.TryParse(dbDataRowKontrolli["SHFAQIMPORTI"].ToString(), out shfaqImporti); 
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se tipit te kontrollit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
        
    }
}

