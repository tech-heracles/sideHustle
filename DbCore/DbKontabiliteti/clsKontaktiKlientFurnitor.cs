using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne te dhenat e kontaktit per nje klient/furnitor
    ///  (Te dhenat  merren nga tabela : T_KONTAKTIKLIENTFURNITOR)
    /// </remarks>
    public class clsKontaktiKlientFurnitor
    { 
        #region Konstruktoret
        
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsKontaktiKlientFurnitor()
        { 
        }

        public clsKontaktiKlientFurnitor(DataRow rreshti)
        {
            MbushKontaktiKlientFurnitor(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdKontaktiKlientFurnitor { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e klientit/furnitorit
        /// </summary>
        public int IdKlientFurnitor { get; set; }

        /// <summary>
        /// Kthen/Vendos emrin e kontaktit
        /// </summary>
        public string EmerKontakti { get; set; }

        /// <summary>
        /// Kthen/Vendos mbiemrin e kontaktit
        /// </summary>
        public string MbiemerKontakti { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin e telefonit te kontaktit
        /// </summary>
        public string TelKontakti { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin e fax-it te kontaktit
        /// </summary>
        public string FaxKontakti { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin e celularit te kontaktit
        /// </summary>
        public string CelKontakti { get; set; }

        /// <summary>
        /// Kthen/Vendos adresen e-mail te kontaktit
        /// </summary>
        public string EmailKontakti { get; set; }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kontaktet nga databaza
        /// </summary>
        /// <param name="dbDataRowKontKlieFurn">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void MbushKontaktiKlientFurnitor(DataRow dbDataRowKontKlieFurn)
        {
            if (dbDataRowKontKlieFurn != null)
            {
                try
                {
                    IdKontaktiKlientFurnitor = !IsDBNull(dbDataRowKontKlieFurn["IDKONTAKTIKLIENTFURNITOR"])
                        ? ToInt32(dbDataRowKontKlieFurn["IDKONTAKTIKLIENTFURNITOR"])
                        : 0;
                    IdKlientFurnitor = !IsDBNull(dbDataRowKontKlieFurn["IDKLIENTFURNITOR"])
                        ? ToInt32(dbDataRowKontKlieFurn["IDKLIENTFURNITOR"])
                        : 0;
                    EmerKontakti = dbDataRowKontKlieFurn["EMERKONTAKTI"].ToString();
                    MbiemerKontakti = dbDataRowKontKlieFurn["MBIEMERKONTAKTI"].ToString();
                    TelKontakti = dbDataRowKontKlieFurn["TELKONTAKTI"].ToString();
                    FaxKontakti = dbDataRowKontKlieFurn["FAXKONTAKTI"].ToString();
                    CelKontakti = dbDataRowKontKlieFurn["CELKONTAKTI"].ToString();
                    EmailKontakti = dbDataRowKontKlieFurn["EMAILKONTAKTI"].ToString();
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kontakteve nga db-ja");
                }
            }
        }

        #endregion

    }
}
