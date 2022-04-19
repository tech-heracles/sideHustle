using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne llogarite qe perfshihen ne nje ze te caktuar te pasqyres financiare
    ///  (Te dhenat  merren nga tabela : T_LLOGARIATRUPIPASQ)
    /// </remarks>
    public class clsLlogariaTrupiPasqyres
    {
        #region Atribute

        private int idLlogariaTrupi;
        private int idTrupi;
        private int idLlogaria;
        private string emertimi;
        private String gjendja;
        private String shenja;
        private int idPerdoruesi;
        private string lloji;
        private bool shfaqBij;
        private DataRow rreshti;

        #endregion

        #region Konstruktor
           public  clsLlogariaTrupiPasqyres( Dictionary<string, object> rreshtDokuKlient)
        {
            emertimi = rreshtDokuKlient["Emertimi"].ToString();
            gjendja = rreshtDokuKlient["Gjendja"].ToString();
            int.TryParse(rreshtDokuKlient["IdLlogaria"].ToString(), out idLlogaria);
            shenja = rreshtDokuKlient["Shenja"].ToString();
            lloji = rreshtDokuKlient["Lloji"].ToString();
             bool.TryParse(rreshtDokuKlient["ShfaqBij"].ToString(), out shfaqBij);   
                 
        }
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsLlogariaTrupiPasqyres(int idllogariatrupi, int idtrupi, int idllogaria, 
                                        String emertimillog, String gjendjallog, String shenjallog, int idperdoruesi, string lloji, bool shfaqbij)
        {
            idLlogariaTrupi = idllogariatrupi;
            idTrupi = idtrupi;
            idLlogaria = idllogaria;
            emertimi = emertimillog;
            gjendja = gjendjallog;
            shenja = shenjallog;
            idPerdoruesi=idperdoruesi;
            shfaqBij = shfaqbij;
            this.lloji = lloji;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsLlogariaTrupiPasqyres(int idtrupi, int idllogaria,
                                        String emertimillog, String gjendjallog, String shenjallog,int idperdoruesi, string lloji, bool shfaqbij)
        {
            idTrupi = idtrupi;
            idLlogaria = idllogaria;
            emertimi = emertimillog;
            gjendja = gjendjallog;
            shenja = shenjallog;
            idPerdoruesi=idperdoruesi ; 
            shfaqBij = shfaqbij;
            this.lloji = lloji;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsLlogariaTrupiPasqyres()
        {
        }

        public clsLlogariaTrupiPasqyres(DataRow rreshti)
        {
            
            mbushLlogariTrup(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdLlogariaTrupi
        {
            get { return idLlogariaTrupi; }
            set { idLlogariaTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e trupit te pasqyres financiare
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise
        /// </summary>
     
        public int IdLlogaria
        {
            get { return idLlogaria; }
            set { idLlogaria = value; }
        }

        /// <summary>
        /// Kthen/Vendos gjendjen e llogarise
        /// </summary>
        public String Gjendja
        {
            get { return gjendja; }
            set { gjendja = value; }
        }

        /// <summary>
        /// Kthen/Vendos emertimin
        /// </summary>
        public String Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos shenjen
        /// </summary>
        public String Shenja
        {
            get { return shenja; }
            set { shenja = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe po kryen veprimin
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi ; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin
        /// </summary>
        public String Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }
        /// <summary>
        /// tregon nese do shfaqen bijet e zerit ne raport apo jo
        /// </summary>
        public bool ShfaqBij
        {
            get
            {
                return shfaqBij;
            }
            set
            {
                shfaqBij = value;
            }
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush llogarite e trupit te pasqyres nga databaza
        /// </summary>
        /// <param name="dbDataRowLlogariTrup">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushLlogariTrup(DataRow dbDataRowLlogariTrup)
        {
            if (dbDataRowLlogariTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowLlogariTrup["IDLLOGARIATRUPIPASQ"].ToString(), out idLlogariaTrupi);
                    int.TryParse(dbDataRowLlogariTrup["IDTRUPIPASQFIN"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowLlogariTrup["IDLLOGARI"].ToString(), out idLlogaria);
                    emertimi = dbDataRowLlogariTrup["EMERTIMI"].ToString();
                    gjendja = dbDataRowLlogariTrup["GJENDJA"].ToString();
                    shenja = dbDataRowLlogariTrup["SHENJA"].ToString();
                    int.TryParse(dbDataRowLlogariTrup["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    lloji = dbDataRowLlogariTrup["LLOJI"].ToString();
                    bool.TryParse(dbDataRowLlogariTrup["SHFAQBIJ"].ToString(), out shfaqBij);
                  

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llogarive te trupit te pasqyres nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
