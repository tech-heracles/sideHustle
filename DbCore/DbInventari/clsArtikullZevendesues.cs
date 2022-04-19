using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne artikujt zevendesues te nje artikulli
    ///  (Te dhenat  merren nga tabela : T_ARTIKULLIZEVENDESUES)
    /// </summary>
    public class clsArtikullZevendesues
    {
        #region Atributet
        private int idArtikulliZevendesues;//id ritese e tabeles
        private int idArtikulliKryesor;
        private int idArtikulliZevend;//id e artikullit qe e zevendeson
        private string prioriteti;
        private string kodArtikulli;
        private string pershkrimArtikulli;
        private DataRow rreshti;

        #endregion

        #region konstruktoret
        /// <summary>
        /// kontruktori me parametra
        /// </summary>
        /// <param name="idArtikulliZevendesues"> id ritese e artikullit zevendesues</param>
        /// <param name="idArtikulliKryesor"> id e artikullit kryesor</param>
        /// <param name="idArtikulliZevend"> id e artikullit qe e zevendeson</param>
        /// <param name="prioriteti"> prioriteti</param>
        public clsArtikullZevendesues(int idArtikulliZevendesues, int idArtikulliKryesor, int idArtikulliZevend, String prioriteti)
        {
            this.idArtikulliZevendesues = idArtikulliZevendesues;
            this.idArtikulliKryesor = idArtikulliKryesor;
            this.idArtikulliZevend = idArtikulliZevend;
            this.prioriteti = prioriteti;
        }
        /// <summary>
        /// kontruktori pa parametra
        /// </summary>


        public clsArtikullZevendesues()
        {
        }

        public clsArtikullZevendesues(DataRow rreshti)
        {
            
            mbushArtikullZevendesues(rreshti);
        }

        #endregion

        #region Properties
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdArtikulliZevendesues
        {
            get { return idArtikulliZevendesues; }
            set { idArtikulliZevendesues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit kryesor.
        /// </summary>
        public int IdArtikulliKryesor
        {
            get { return idArtikulliKryesor; }
            set { idArtikulliKryesor = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit qe e zevendeson.
        /// </summary>
        public int IdArtikulliZevend
        {
            get { return idArtikulliZevend; }
            set { idArtikulliZevend = value; }
        }
        /// <summary>
        /// Kthen/Vendos prioritetin.
        /// </summary>
        public String Prioriteti
        {
            get { return prioriteti; }
            set { prioriteti = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodin e artikullit.
        /// </summary>
        public string KodArtikulli
        {
            get
            {
                return kodArtikulli;
            }
            set
            {
                kodArtikulli = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e artikullit.
        /// </summary>
        public string PershkrimArtikulli
        {
            get
            {
                return pershkrimArtikulli ;
            }
            set
            {
                this.pershkrimArtikulli  = value;
            }

        }
        #endregion

        #region Metoda Publike
        /// <summary>
        /// Ruan objektin artikull zevendesues ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajArtikulliZevendesues"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            int id;
            clsMesazh u_ruajt = data.ruajArtikulliZevendesues(out id, this.IdArtikulliKryesor, this.IdArtikulliZevend, this.Prioriteti);
            data.Dispose();
            //clsMesazh u_ruajt = data.ruajArtikulliZevendesues(this);
            return u_ruajt;
        }
        /// <summary>
        /// Modifikon objektin artikull zevendesues ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoArtikulliZevendesues"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
      
        public clsMesazh modifiko()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_modifikua = data.modifikoArtikulliZevendesues(this.IdArtikulliZevendesues, this.IdArtikulliKryesor, this.IdArtikulliZevend, this.Prioriteti);
            //clsMesazh u_modifikua = data.modifikoArtikulliZevendesues(this);
            data.Dispose();
            return u_modifikua;
        }
        /// <summary>
        /// Fshin objektin artikull zevendesues ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiArtikulliZevendesues"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
      
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiArtikulliZevendesues(this.IdArtikulliZevendesues);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiArtikulliZevendesues(this);
            return u_fshi;
        }
        /// <summary>
        /// merr objektin artikull zevendesues ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.merrArtikulliZevendesues"/> 
        /// </summary>
      
        public void merr()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.merrArtikulliZevendesues(this.IdArtikulliZevendesues);
            data.Dispose();
            //data.merrArtikulliZevendesues(this);
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush nga databaza nje datarow me artikullin zevendesues
        /// </summary>
        /// <param name="dbDataRowArtikullZevendesues">datarow qe do mbushet</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthe false</returns>
        internal bool mbushArtikullZevendesues(DataRow dbDataRowArtikullZevendesues)
        {
            if (dbDataRowArtikullZevendesues != null)
            {

                try
                {
                    int.TryParse(dbDataRowArtikullZevendesues["IDARTIKULLIZEVENDESUES"].ToString(), out idArtikulliZevendesues);
                    int.TryParse(dbDataRowArtikullZevendesues["IDARTIKULLIKRYESOR"].ToString(), out idArtikulliKryesor);
                    int.TryParse(dbDataRowArtikullZevendesues["IDARTIKULLIZEVEND"].ToString(), out idArtikulliZevend);
                    prioriteti = dbDataRowArtikullZevendesues["PRIORITETI"].ToString();
                    kodArtikulli = dbDataRowArtikullZevendesues["kodArt"].ToString();
                    pershkrimArtikulli = dbDataRowArtikullZevendesues["pershArt"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se artikullit zevendesues nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}