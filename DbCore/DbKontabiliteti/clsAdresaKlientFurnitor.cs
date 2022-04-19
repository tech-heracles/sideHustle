using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje adrese te klientit/furnitorit
    ///  (Te dhenat  merren nga tabela : T_ADRESAKLIENTFURNITOR)
    /// </remarks>
    public class clsAdresaKlientFurnitor
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idadresaklientfurnitor">Id e adreses</param>
        /// <param name="idklientfurnitor">Id e klientit/furnitorit</param>
        /// <param name="idtipadrese">Id e tipit te adreses. Merret nga tabela: T_TIPADRESE</param>
        /// <param name="adr"></param>
        /// <param name="kodiPostar"></param>
        public clsAdresaKlientFurnitor(int idadresaklientfurnitor, int idklientfurnitor, int idtipadrese, string adr, string kodiPostar)
        {
            IdAdresaKlientFurnitor = idadresaklientfurnitor;
            IdKlientFurnitor = idklientfurnitor;
            IdTipAdrese = idtipadrese;
            Adresa = adr;
            KodiPostar = kodiPostar;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsAdresaKlientFurnitor()
        {
        }

        public clsAdresaKlientFurnitor(DataRow rreshti)
        {
            MbushAdresaKlientFurnitore(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdAdresaKlientFurnitor { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e klientit/furnitorit
        /// </summary>
        public int IdKlientFurnitor { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e tipit te adreses
        /// <seealso cref="DbCore.DbKontabiliteti.clsTipAdrese"/>
        /// </summary>
        public int IdTipAdrese { get; set; }

        /// <summary>
        /// Kthen/Vendos adresen.
        /// </summary>
        public string Adresa { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin postar.
        /// </summary>
        public string KodiPostar { get; set; }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush adresa klient furnitore nga databaza
        /// </summary>
        /// <param name="dbDataRowAdresaKlientFurnitore">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void MbushAdresaKlientFurnitore(DataRow dbDataRowAdresaKlientFurnitore)
        {
            if (dbDataRowAdresaKlientFurnitore != null)
            {
                try
                {
                    IdAdresaKlientFurnitor = !IsDBNull(dbDataRowAdresaKlientFurnitore["IDADRESAKLIENTFURNITOR"])
                        ? ToInt32(dbDataRowAdresaKlientFurnitore["IDADRESAKLIENTFURNITOR"])
                        : 0;
                    IdKlientFurnitor = !IsDBNull(dbDataRowAdresaKlientFurnitore["IDKLIENTFURNITOR"])
                        ? ToInt32(dbDataRowAdresaKlientFurnitore["IDKLIENTFURNITOR"])
                        : 0;
                    IdTipAdrese = !IsDBNull(dbDataRowAdresaKlientFurnitore["IDTIPADRESE"])
                        ? ToInt32(dbDataRowAdresaKlientFurnitore["IDTIPADRESE"])
                        : 0;
                    Adresa = dbDataRowAdresaKlientFurnitore["ADRESA"].ToString();
                    KodiPostar = dbDataRowAdresaKlientFurnitore["KODIPOSTAR"].ToString();
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se adresave te klienteve furnitore nga db-ja");
                }
            }
        }

        #endregion
    }
}
