using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne konfigurimet e grides,kolonat qe do 
    ///  permbaje grida, gjeresite e tyre etj..(Te dhenat  merren nga tabela : T_GRIDAKOKA)
    /// </summary>
    public class clsGridaKoka
    {
        #region Attribute

        private int idGridaKoka;
        private String emriGridaKoka;
        private int idKomponente;
        private int idKonfigurim;
        private int menyreFiltrimi;
        private int topRows;
        private colGridaTrupi oColGridaTrupi;
        private bool customCustomizationWindow;

        #endregion

        #region Konstruktoret
        /// <summary>
        /// nderton koken e grides nepermjet idkonfigurimi
        /// kujdes! nese idkonfig = 1 atehere ka me shume se nje grid the objekti nuk do ndertohet
        /// </summary>
        /// <param name="idGjuha"></param>
        public clsGridaKoka(int idGjuha, int idkonfigurim)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                data.ktheGridaKokaByKonfigurim(idkonfigurim, this);
            }
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="emriGrida">emri i grides</param>
        /// <param name="emriKomponente">emri i komponentes</param>
        /// <param name="idNdermarrje">id e ndemarrjes</param>
        public clsGridaKoka(int idGjuha, string emriGrida, string emriKomponente, int idNdermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                data.ktheGridaKokaByEmri(emriGrida, emriKomponente, idNdermarrje, this);
            }
        }

        /// <summary>
        /// konstruktor me 5 parametra
        /// </summary>
        /// <param name="idGjuha">id e gjuhes se perdoruesit</param>
        /// <param name="emriGrida">emri i grides</param>
        /// <param name="emriKomponente">emri i komponentes</param>
        /// <param name="idNdermarrje">id e ndemarrjes</param>
        ///  /// <param name="idkonf">id e konfigurimit</param>
        public clsGridaKoka(int idGjuha, string emriGrida, string emriKomponente, int idNdermarrje, int idkonf)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                data.ktheGridaKokaByEmrisipasIdKonfigurimi(emriGrida, emriKomponente, idNdermarrje, idkonf, this);
            }
        }
        public clsGridaKoka(string emriGrida, string emriKomponente, int idNdermarrje, int idkonf)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                data.ktheGridaKokaByEmrisipasIdKonfigurimi(emriGrida, emriKomponente, idNdermarrje, idkonf, this);
            }
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsGridaKoka()
        {
            oColGridaTrupi = new colGridaTrupi();
        }

        public clsGridaKoka(int idGridaKoka)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                dbAdmin.ktheGridaKoka(idGridaKoka, this);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdGridaKoka
        {
            get { return idGridaKoka; }
            set { idGridaKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin  grides qe duhet te jete i njejte me emrin qe i eshte vendosur grides
        /// nga design i faqes perkatese.
        /// </summary>
        public String EmriGridaKoka
        {
            get { return emriGridaKoka; }
            set { emriGridaKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e komponentes(komponentja perfaqson nje faqe .aspx) te ciles i perket 
        /// ky konfigurim i grides.
        /// </summary>
        public int IdKomponente
        {
            get { return idKomponente; }
            set { idKomponente = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te ambjentin.(Konfrigurimi i ambjentit perfaqson konfigurimin
        /// per nje komponente te caktuar)
        /// </summary>
        public int IdKonfigurim
        {
            get { return idKonfigurim; }
            set { idKonfigurim = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <c>clsGridaTrupi</c>. Cdo objekt i tipit
        /// <c>clsGridaKoka</c> permban nje collection te tille.
        /// </summary>
        public colGridaTrupi OColGridaTrupi
        {
            get { return oColGridaTrupi; }
            set { oColGridaTrupi = value; }
        }

        public int TopRows
        {
            get
            {
                return topRows;
            }

            set
            {
                topRows = value;
            }
        }
        /// <summary>
        /// Vlera 0 perfaqson filtrimin automatik,vlera 1 filtrim manual (btn ose enter)
        /// </summary>
        public int MenyreFiltrimi
        {
            get
            {
                return menyreFiltrimi;
            }

            set
            {
                menyreFiltrimi = value;
            }
        }

        public bool CustomCustomizationWindow
        {
            get
            {
                return customCustomizationWindow;
            }
            set
            {
                customCustomizationWindow = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// mbush koken e grides sipas konfigurimit
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idkonf">id e konfigurimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public void mbushGridaKokaByKonfigurim(int idkonf)
        {
            if (idkonf == 1)
                throw new Exception("ID konfigurimi i grides eshte 1,me kete id konfig nuk mund te lexohet grida sepse ka shume konfigurime");
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                data.ktheGridaKokaByKonfigurim(idkonf, this);
            }
        }
        #endregion

        #region Metoda Internal


        internal void mbushGridKok(IDataRecord dbDataRowGridKoke)
        {
            try
            {

                idGridaKoka = int.Parse(dbDataRowGridKoke["GRIDKOKAID"].ToString());
                emriGridaKoka = dbDataRowGridKoke["GRIDKOKAEMRI"].ToString();
                idKomponente = int.Parse(dbDataRowGridKoke["GRIDKOMPID"].ToString());
                idKonfigurim = int.Parse(dbDataRowGridKoke["IDKONFIGURIM"].ToString());
                int.TryParse(dbDataRowGridKoke["MENYREFILTRIMI"].ToString(), out menyreFiltrimi);
                int.TryParse(dbDataRowGridKoke["TOPROWS"].ToString(), out topRows);
                bool.TryParse(dbDataRowGridKoke["CUSTOMCUSTOMIZATIONWINDOW"].ToString(), out customCustomizationWindow);

                oColGridaTrupi = new colGridaTrupi();

            }
            catch (Exception ex)
            {
                throw new MyException($"Ndodhi nje gabim ne mbushje te grides me id {idGridaKoka}", ex);
            }
        }

        public clsMesazh Modifiko()
        {
            using (var dbAdmin = new clsDatabaseAdmin())
            {
                dbAdmin.RuajGridaKoka(idGridaKoka,topRows,menyreFiltrimi);
            }
            return new clsMesazh("Konfigurimi u ruajt me sukes");
            
        }


        #endregion
    }
}
