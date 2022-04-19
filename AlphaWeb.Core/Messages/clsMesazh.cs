using System;
using System.Globalization;

namespace DbCore
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektin qe perfaqson vleren qe kthen nje funksion, kur funksioni kthen
    ///  vlera boolean,pervec vleres true/false , kjo permban edhe nje mesazh qe pershkruan statusin me te cilin
    ///  po perfundon funksioni.
    /// </summary>
    public  class clsMesazh:IMesazh
    {
        int kodMesazhi;
        bool status;
        TipMesazhi tipi;
        string pershkrimMesazhi;
         //konstruktoret
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsMesazh(int kodmesazhi, bool statusmesazhi, String pershkrimmesazhi)
        {
            kodMesazhi = kodmesazhi;
            status = statusmesazhi;
            pershkrimMesazhi = pershkrimmesazhi;
            this.tipi = statusmesazhi ? TipMesazhi.Sukses : TipMesazhi.Gabim;
        }
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsMesazh(bool statusmesazhi, String pershkrimmesazhi)
        {
            status = statusmesazhi;
            pershkrimMesazhi = pershkrimmesazhi;
            this.tipi = statusmesazhi ? TipMesazhi.Sukses : TipMesazhi.Gabim;
        }
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsMesazh(bool statusmesazhi)
        {
            status = statusmesazhi;
            pershkrimMesazhi = statusmesazhi ? "Ne rregull" : "Nje gabim i papritur ka ndodhur!";
            this.tipi = statusmesazhi ? TipMesazhi.Sukses : TipMesazhi.Gabim;
        }
        public clsMesazh(bool statusmesazhi, CultureInfo ci) //TOASSIGN PATI - perdor kete ne vend te clsMesazh(bool statusmesazhi)
        {
            status = statusmesazhi;
            pershkrimMesazhi = statusmesazhi ? "Ne rregull" : "Nje gabim i papritur ka ndodhur!";
            this.tipi = statusmesazhi ? TipMesazhi.Sukses : TipMesazhi.Gabim;
        }
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsMesazh(String pershkrimmesazhi)
        {
            pershkrimMesazhi = pershkrimmesazhi;
            status = false;
            this.tipi = TipMesazhi.Gabim;
        }
        public clsMesazh(TipMesazhi tip, String pershkrimmesazhi)
        {
            pershkrimMesazhi = pershkrimmesazhi;
            this.tipi = tip;
            switch (tip)
            {
                case TipMesazhi.Gabim:
                    status = false;
                    break;
                case TipMesazhi.Sukses:
                case TipMesazhi.Informim:
                    status = true;
                    break;
                default: throw new Exception("Tip mesazhi i panjohur");
            }
        }

        public clsMesazh(TipMesazhi tip, String pershkrimmesazhi,params object[]  args):this(tip,pershkrimmesazhi)
        {
            pershkrimMesazhi =string.Format(pershkrimMesazhi,args);
        }

        /// <summary>
        /// Konstruktori default i klases qe inicializohet si mesazh gabimi
        /// </summary>
        public clsMesazh()
        {
            status = false;
            this.tipi = TipMesazhi.Gabim;
            pershkrimMesazhi = "Nje gabim i papritur ka ndodhur!";
        }
        /// <summary>
        /// Kthen/Vendos kodin e mesazhit per kete objekt. Mund te perdoret ne rastin kur ne funksion ndodh nje gabim, dhe pervec
        /// statusit false, dhe pershkrimit te mesazhit, mund te perdoret edeh kodi i mesazhit si psh "ERR01". Kjo per te 
        /// lehtesuar identifikimin e gabimeve qe ndodhin.
        /// </summary>
        public int KodMesazhi
        {
            get { return kodMesazhi; }
            set { kodMesazhi = value; }
        }
        public TipMesazhi Tipi
        {
            get
            {
                return tipi;
            }
            set
            {
                if (tipi == value)
                    return;
                tipi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos statusin : true/false qe ka ky objekt.
        /// </summary>
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e mesazhit per kete objekt. 
        /// </summary>
        public string PershkrimMesazhi
        {
            get { return pershkrimMesazhi; }
            set { pershkrimMesazhi = value; }
        }

       
        public static bool operator true(clsMesazh mesazhi)
        {
            return mesazhi.status==true;
        }

        public static bool operator false(clsMesazh mesazhi)
        {
            return mesazhi.status==false;
        }
        public static bool operator!(clsMesazh mesazhi)
        {
            return !mesazhi.status;
        }



    }
}
