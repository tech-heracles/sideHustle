using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne trupin e nje skeme kontabiliteti
    ///  (Te dhenat  merren nga tabela : T_SKEMAKONTABELTRUPI)
    ///</remarks>
    public class clsSkemaKontabelTrupiNew
    {
        #region Atributet

        private string idSkemeKontTrupi;
        private string kodSkemeKontTrupi;
        private string pershkrimSkemeKontTrupi;
        private string formulaSkemeKontTrupi;
        private int debikrediSkemeKontTrupi;
        private string idLlojLlogarise;
        private int kushtiSkemeKontTrupi;
        private string idNenLlojLlogarie;
        private string idLlogarieFikse;
        private string idSkemeKont;
        private int indeksGrupimi;
        private DataRow rreshti;
        
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaKontabelTrupiNew(string idskemekonttrupi, string kodskemekonttrupi, string pershkrimskemekonttrupi,
                                        string formulaskemekonttrupi, int debikrediskemekonttrupi,string idllojllogarise,
                                        int kushtiskemekonttrupi, string idnenllojllogarie, string idllogariefikse, 
                                        string idskemekont,int indeksgrupimi)
        {
            idSkemeKontTrupi = idskemekonttrupi;
            kodSkemeKontTrupi =kodskemekonttrupi;
            pershkrimSkemeKontTrupi =pershkrimskemekonttrupi;
            formulaSkemeKontTrupi = formulaskemekonttrupi;
            debikrediSkemeKontTrupi =debikrediskemekonttrupi;
            idLlojLlogarise=idllojllogarise;
            kushtiSkemeKontTrupi=kushtiskemekonttrupi;
            idNenLlojLlogarie=idnenllojllogarie;
            idLlogarieFikse = idllogariefikse;
            idSkemeKont = idskemekont;
            indeksGrupimi = indeksgrupimi;
        }
        
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaKontabelTrupiNew()
        { 
        }

        public clsSkemaKontabelTrupiNew(DataRow rreshti)
        {
            
            mbushSkemKontTrupiNew(rreshti);
        }

        #endregion

        #region Metoda Publike

        public static int ktheDebiKredi(clsSkemaKontabelTrupiNew tempSkemaKontTrupi, string veprimTrupiDebiKredi)
        {
            int debiKredi = 0;
            if (tempSkemaKontTrupi.DebikrediSkemeKontTrupi == 0)
            {
                if (veprimTrupiDebiKredi == "Debi")
                    debiKredi = 1;
                else
                    debiKredi = 2;
            }
            else
                debiKredi = tempSkemaKontTrupi.DebikrediSkemeKontTrupi;
            return debiKredi;
        }

        public static int ktheDebiKredi(clsSkemaKontabelTrupiNew tempSkemaKontTrupi, int veprimTrupiDebiKredi)
        {
            int debiKredi = 0;
            if (tempSkemaKontTrupi.DebikrediSkemeKontTrupi == 0)
                debiKredi = veprimTrupiDebiKredi;
            else
                debiKredi = tempSkemaKontTrupi.DebikrediSkemeKontTrupi;
            return debiKredi;
        }

        public static int ktheDebiKrediKunderParti(clsSkemaKontabelTrupiNew tempSkemaKontTrupi, int veprimTrupiDebiKredi)
        {
            int debiKredi = 0;
            if (tempSkemaKontTrupi.DebikrediSkemeKontTrupi == 0)
            {
                if (veprimTrupiDebiKredi == 1)   //debi per llogarine e klient furnitor
                    debiKredi = 2; //kredi per llogarine kunderparti
                else
                    debiKredi = 1; //debi per llogarine kunderparti
            }
            else
                debiKredi = tempSkemaKontTrupi.DebikrediSkemeKontTrupi;
            return debiKredi;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public string IdSkemeKontTrupi
        {
            get { return idSkemeKontTrupi; }
            set { idSkemeKontTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e vleres
        /// </summary>
        public String KodSkemeKontTrupi
        {
            get { return kodSkemeKontTrupi; }
            set { kodSkemeKontTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e vleres
        /// </summary>
        public String PershkrimSkemeKontTrupi
        {
            get { return pershkrimSkemeKontTrupi; }
            set { pershkrimSkemeKontTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos formulen per te llogaritur vleren
        /// </summary>
        public String FormulaSkemeKontTrupi
        {
            get { return formulaSkemeKontTrupi; }
            set { formulaSkemeKontTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese llogaria preket ne debi apo ne kredi
        /// </summary>
        public int DebikrediSkemeKontTrupi
        {
            get { return debikrediSkemeKontTrupi; }
            set { debikrediSkemeKontTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llojit te llogarise
        /// </summary>
        public String IdLlojLlogarise
        {
            get { return idLlojLlogarise; }
            set { idLlojLlogarise = value; }
        }

        /// <summary>
        /// Kthen/Vendos kushtin
        /// </summary>
        public int KushtiSkemeKontTrupi
        {
            get { return kushtiSkemeKontTrupi; }
            set { kushtiSkemeKontTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nenllojit te llogarise
        /// </summary>
        public String IdNenLlojLlogarie
        {
            get { return idNenLlojLlogarie; }
            set { idNenLlojLlogarie = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise fikse (nese llogaria qe po preket eshte llogari fikse psh 4457 - TVSH)
        /// </summary>
        public String IdLlogarieFikse
        {
            get { return idLlogarieFikse; }
            set { idLlogarieFikse = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se skemes kontabel te ciles i perket trupi
        /// </summary>
        public string IdSkemeKont
        {
            get { return idSkemeKont; }
            set { idSkemeKont = value; }
        }

        /// <summary>
        /// Kthen/Vendos indeksin e grupimit (per te renditur rreshtat e trupit)
        /// </summary>
        public int IndeksGrupimi
        {
            get { return indeksGrupimi; }
            set { indeksGrupimi = value; }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupat e rinj te skemas se kontabilitetit nga databaza
        /// </summary>
        /// <param name="dbDataRowSkemTrupiNew">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushSkemKontTrupiNew(DataRow dbDataRowSkemTrupiNew)
        {
            if (dbDataRowSkemTrupiNew != null)
            {
                try
                {
                    int.TryParse(dbDataRowSkemTrupiNew["DKSKEMAKONTTRUPI"].ToString(), out debikrediSkemeKontTrupi);
                    formulaSkemeKontTrupi = dbDataRowSkemTrupiNew["FORMULASKEMEKONTTRUPI"].ToString();
                    idLlogarieFikse = dbDataRowSkemTrupiNew["IDLLOGARIFIKSE"].ToString();
                    idLlojLlogarise = dbDataRowSkemTrupiNew["IDLLOJILLOGARISE"].ToString();
                    int idnenlloj = 0;
                     int.TryParse(dbDataRowSkemTrupiNew["IDNENLLOJLLOGARIE"].ToString(),out idnenlloj);
                     idNenLlojLlogarie = idnenlloj.ToString();
                    idSkemeKontTrupi = dbDataRowSkemTrupiNew["IDSKEMKONTTRUPI"].ToString();
                    kodSkemeKontTrupi = dbDataRowSkemTrupiNew["KODSKEMEKONTTRUPI"].ToString();
                    int.TryParse(dbDataRowSkemTrupiNew["KUSHTI"].ToString(), out kushtiSkemeKontTrupi);
                    pershkrimSkemeKontTrupi = dbDataRowSkemTrupiNew["PERSHKRIMSKEMKONTTRUPI"].ToString();
                    idSkemeKont = dbDataRowSkemTrupiNew["IDSKEMKONT"].ToString();
                    int.TryParse(dbDataRowSkemTrupiNew["INDEKSGRUPIMI"].ToString(), out indeksGrupimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se Trupave te rinj te skemes se kontabilitetit nga db-ja");
                }
            }
            else
                return false;
        }
        internal virtual clsSkemaKontabelTrupiNew Clone()
        {
            return (clsSkemaKontabelTrupiNew)this.MemberwiseClone();
        }
        #endregion
    }
}
