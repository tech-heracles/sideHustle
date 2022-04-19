using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// Kjo klase permban metodat e nevojshme per te perdorur te dhenat e tabeles T_TRUPIFORMATIMPORTI
    /// </summary>
    public class clsTrupiFormatImporti
    {
        #region Atribute

        private int idTrupi;
        private int idKoka;
        private int idKontroll;
        private string emerImporti;
        private string vleraDefault;
        private bool visible;
        private int rendi;
        private bool detyrueshme;
        private bool shfaq;
        /// <summary>
        /// tregon nese id kontrolli do merret nga tabela e kontrolleve 1 apo nga ajo e gridave 2
        /// </summary>
        private int tipi;
        private string kodKontrolli;
        private int tipKontrolli;
        private bool detyrueshmeDefault;
        private int fusheKokeApoTrupi;
        private string fusheType;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori me parametra i klases
        /// </summary>
        /// <param name="idTrupi">Id e trupit </param>
        /// <param name="idKoka">Id e kokes </param>
        /// <param name="emri">emri i kolones</param>
        /// <param name="vlera">vlera default</param>
        /// <param name="visible">visibility i kolones</param>
        /// <param name="rendiKolona">rendi i kolones</param>
        public clsTrupiFormatImporti(int idTrupi, int idKoka, int idkontroll, string emri, string vlera, bool visible, int rendiKolona, bool detyrueshme, bool shfaq, int tipi, bool detyrueshmedefault, int fusheKokeApoTrupi, string fusheType)
        {
            this.idTrupi = idTrupi;
            this.idKoka = idKoka;
            this.emerImporti = emri;
            this.vleraDefault = vlera;
            this.visible = visible;
            this.rendi = rendiKolona;
            this.idKontroll = idkontroll;
            this.detyrueshme = detyrueshme;
            this.shfaq = shfaq;
            this.tipi = tipi;
            this.detyrueshmeDefault = detyrueshmedefault;
            this.fusheKokeApoTrupi = fusheKokeApoTrupi;
            this.fusheType = fusheType;
        }

        /// <summary>
        /// Konstruktori qe krijon objektin clsInfoTrupi me id qe i kalohet si parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        public clsTrupiFormatImporti(int idTrupi)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            mbushFormatImportiTrupi(dbAdmin.merrFormatImportiTrupiSipasID(idTrupi));
            dbAdmin.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsTrupiFormatImporti()
        {

        }

        public clsTrupiFormatImporti(DataRow rreshti)
        {
            
            mbushFormatImportiTrupi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin  qe perdoruesi i vendos kolones
        /// </summary>
        public string EmerImporti
        {
            get { return emerImporti; }
            set { emerImporti = value; }
        }

        /// <summary>
        /// id e kontrollit me te cilen eshte i lidhur importi
        /// </summary>
        public int IdKontroll
        {
            get
            {
                return idKontroll;
            }
            set
            {
                idKontroll = value;
            }
        }

        /// <summary>
        /// kodi i kontrollit
        /// </summary>
        public string KodKontrolli
        {
            get
            {
                return kodKontrolli;
            }
            set
            {
                kodKontrolli = value;
            }
        }

        /// <summary>
        /// tipi i kontrollit label,textbox,combobox ect
        /// </summary>
        public int TipKontrolli
        {
            get
            {
                return tipKontrolli;
            }
            set
            {
                tipKontrolli = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos vleren default te ketij kontrolli
        /// </summary>
        public string VleraDefault
        {
            get { return vleraDefault; }
            set { vleraDefault = value; }
        }

        /// <summary>
        /// tregon nese kjo fushe do te jete fushe e detyrueshme gjate importit
        /// </summary>
        public bool Detyrueshme
        {
            get
            {
                return detyrueshme;
            }
            set
            {
                detyrueshme = value;
            }
        }

        /// <summary>
        /// tregon qe kjo fushe e detyrueshme dhe nuk mund te hiqet nga perdoruesi
        /// </summary>
        public bool DetyrueshmeDefault
        {
            get
            {
                return detyrueshmeDefault;
            }
            set
            {
                detyrueshmeDefault = value;
            }
        }

        /// <summary>
        /// tregon nese kjo fushe do te shfaqet tek dokumenti i excelit 
        /// </summary>
        public bool Shfaq
        {
            get
            {
                return shfaq;
            }
            set
            {
                shfaq = value;
            }
        }

        /// <summary>
        /// tregon nese id kontrolli do merret nga tabela e kontrolleve 1 apo nga ajo e gridave 2
        /// </summary>
        public int Tipi
        {
            get
            {
                return tipi;
            }
            set
            {
                tipi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos vleren true ose false qe percakton per secilen nga fushat dhe percakton nese do meret parasysh ne import apo jo
        /// </summary>
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }


        /// <summary>
        /// Kthen/Vendos renditjen e shfaqjes se fushave 
        /// </summary>
        public int Rendi
        {
            get { return rendi; }
            set { rendi = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese fusha eshte fushe koke apo trupi. Perdoret per formatet e importit te dokumenteve te cilet kane tabela koke dhe trupi. Merr vleren 1 nqs eshte fushe koke dhe 2 nqs eshte fushe trupi
        /// </summary>
        public int FusheKokeApoTrupi
        {
            get { return fusheKokeApoTrupi; }
            set { fusheKokeApoTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin e fushes (int, varchar, numeric, bit) sic jane llojet e te dhenave ne databaze.
        /// </summary>
        public string FusheType
        {
            get { return fusheType; }
            set { fusheType = value; }
        }
        
        #endregion

        #region Metodat publike

        #endregion

        #region Metodat internal

        internal bool mbushFormatImportiTrupi(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRow["IDKONTROLL"].ToString(), out idKontroll);
                    int.TryParse(dbDataRow["TIPI"].ToString(), out tipi);
                    emerImporti = dbDataRow["EMERIMPORTI"].ToString();
                    vleraDefault = dbDataRow["VLERADEFAULT"].ToString();
                    kodKontrolli = dbDataRow["KODKONTROLL"].ToString();
                    visible = Convert.ToBoolean(dbDataRow["VISIBLE"]);
                    detyrueshme = Convert.ToBoolean(dbDataRow["DETYRUESHME"]);
                    shfaq = Convert.ToBoolean(dbDataRow["SHFAQ"]);
                    int.TryParse(dbDataRow["RENDI"].ToString(), out rendi); 
                    int.TryParse(dbDataRow["tipkontrolli"].ToString(), out tipKontrolli);
                    detyrueshmeDefault = Convert.ToBoolean(dbDataRow["DETYRUESHMEDEFAULT"]);
                    int.TryParse(dbDataRow["FUSHEKOKEAPOTRUPI"].ToString(), out fusheKokeApoTrupi);
                    fusheType = dbDataRow["FUSHETYPE"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes trupit te format importi nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
