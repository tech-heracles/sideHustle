using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje element te trupit te pasqyres financiare
    ///  (Te dhenat  merren nga tabela : T_PASQYRAFINANCIARETRUPI)
    ///</remarks>
    public class clsTrupPasqyreFinanciare
    {
        #region Atribute

        private int idTrupi;
        private int idKoka;
        private String pershkrimiZerit;
        private String prindiZerit;
        private int niveliZerit;
        private String llojiZerit;
        private int gjeneroTotal;
        private colLlogariaTrupiPasqyres oColLlogarite;
        private colBuxhetet oColBuxhetet;
        private string kodZeri;
        private bool shfaqBij;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        public  clsTrupPasqyreFinanciare( Dictionary<string, object> rreshtDokuKlient)
        {         
            pershkrimiZerit = rreshtDokuKlient["PershkrimiZerit"].ToString();
            prindiZerit = rreshtDokuKlient["PrindiZerit"].ToString();
            niveliZerit = Convert.ToInt32(rreshtDokuKlient["NiveliZerit"]);
            llojiZerit = rreshtDokuKlient["LlojiZerit"].ToString();
            kodZeri = rreshtDokuKlient["KodZeri"].ToString();
            gjeneroTotal = Convert.ToInt32(rreshtDokuKlient["GjeneroTotal"].ToString());   
            shfaqBij = Convert.ToBoolean(rreshtDokuKlient["ShfaqBij"].ToString());
            object[] llog=((object[])(rreshtDokuKlient["OColLlogarite"]));
            oColLlogarite = new colLlogariaTrupiPasqyres();
            for (int i = 0; i < llog.Length; i++)
            {
                clsLlogariaTrupiPasqyres llogtrup = new clsLlogariaTrupiPasqyres((Dictionary<string, object>)(llog[i]));
                if(llogtrup.IdLlogaria!=0)
                oColLlogarite.Add(llogtrup);
            }
            object[] buxh = ((object[])(rreshtDokuKlient["OColBuxhetet"]));
            OColBuxhetet = new  colBuxhetet ();
            for (int i = 1; i < buxh.Length; i++)
            {
                clsBuxheti buxheti = new clsBuxheti((Dictionary<string, object>)(buxh[i]), clsLlojBuxheti.mbushIDLlojBuxheti("PasqyreFinanciare"));
                OColBuxhetet.Add(buxheti);
            }
             
        }
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTrupPasqyreFinanciare(int idtrupi, int idkoka, String pershkrimizerit, String prindizerit, int nivelizerit, String llojizerit, int gjenerotot, bool shfaqbij, string kodzeri)
        {
            idTrupi = idtrupi;
            idKoka = idkoka;
            pershkrimiZerit = pershkrimizerit;
            prindiZerit = prindizerit;
            niveliZerit = nivelizerit;
            shfaqBij = shfaqbij;
            kodZeri = kodzeri;
            llojiZerit = llojizerit;
            gjeneroTotal = gjenerotot;
            oColLlogarite = new colLlogariaTrupiPasqyres();
            oColBuxhetet = new colBuxhetet();

        }
    
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsTrupPasqyreFinanciare(int idkoka, string pershkrimizerit, String prindizerit, int nivelizerit, String llojizerit, int gjenerotot, bool shfaqbij, string kodzeri)
        {
            idKoka = idkoka;
            pershkrimiZerit = pershkrimizerit;
            prindiZerit = prindizerit;
            niveliZerit = nivelizerit;
            llojiZerit = llojizerit;
            gjeneroTotal = gjenerotot;  
            shfaqBij = shfaqbij;
            kodZeri = kodzeri;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsTrupPasqyreFinanciare()
        {
        }

        public clsTrupPasqyreFinanciare(DataRow rreshti)
        {
            
            mbushTrupPasqyreFinanciare(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes te ciles i perket trupi
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }
        /// <summary>
        /// kodi i zerit qe del majtas tij neper raporte psh 1,2,3, i,ii,iii ,a ,b, c ect
        /// </summary>
        public string KodZeri
        {
            get
            {
                return kodZeri;
            }
            set
            {
                kodZeri = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e zerit
        /// </summary>
        public String PershkrimiZerit
        {
            get { return pershkrimiZerit; }
            set { pershkrimiZerit = value; }
        }

        /// <summary>
        /// Kthen/Vendos prindin e zerit (prindi mund te jete bosh ose te jete nje nga zerat e tjere te paqyres me nivel me te larte se biri)
        /// </summary>
        public String PrindiZerit
        {
            get { return prindiZerit; }
            set { prindiZerit = value; }
        }

        /// <summary>
        /// Kthen/Vendos nivelin e zerit (per te bere renditjen hierarkike)
        /// </summary>
        public int NiveliZerit
        {
            get { return niveliZerit; }
            set { niveliZerit = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin e zerit psh Aktive, Detyrime etj
        /// </summary>
        public String LlojiZerit
        {
            get { return llojiZerit; }
            set { llojiZerit = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsLlogariaTrupipasqyres"/>
        /// </summary>
        public colLlogariaTrupiPasqyres OColLlogarite
        {
            get { return oColLlogarite; }
            set { oColLlogarite = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsBuxheti"/>
        /// </summary>
        public colBuxhetet OColBuxhetet
        {
            get { return oColBuxhetet; }
            set { oColBuxhetet= value; }
        }

        /// <summary>
        /// Kthen/Vendos nese do gjenerohet apo jo totali per kete ze
        /// </summary>
        public int GjeneroTotal
        {
            get { return gjeneroTotal; }
            set { gjeneroTotal = value; }
        }
        /// <summary>
        /// tregon nese do shfaqen bijet ne raport apo jo
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
        /// mbush trupin e pasqyres financiare nga databaza
        /// </summary>
        /// <param name="dbDataRowTrupPasFinanc">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupPasqyreFinanciare(DataRow dbDataRowTrupPasFinanc)
        {
            if (dbDataRowTrupPasFinanc != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrupPasFinanc["IDPASQFINTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowTrupPasFinanc["IDPASQFINKOKA"].ToString(), out idKoka);
                    pershkrimiZerit = dbDataRowTrupPasFinanc["PERSHKRIMIZERIT"].ToString();
                    prindiZerit = dbDataRowTrupPasFinanc["PRINDI"].ToString();
                    int.TryParse(dbDataRowTrupPasFinanc["NIVELI"].ToString(), out niveliZerit);
                    llojiZerit = dbDataRowTrupPasFinanc["LLOJIZERIT"].ToString();
                    int.TryParse(dbDataRowTrupPasFinanc["GJENEROTOTAL"].ToString(), out gjeneroTotal);  
                    kodZeri = dbDataRowTrupPasFinanc["KODZERI"].ToString();
                    bool.TryParse(dbDataRowTrupPasFinanc["SHFAQBIJ"].ToString(), out shfaqBij);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te pasqyres financiare nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
