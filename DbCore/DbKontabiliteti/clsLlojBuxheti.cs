using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje lloj buxheti - lloji llogari, KPF, pasqyre financiare etj
    ///  (Te dhenat  merren nga tabela : T_LLOJBUXHETI)
    /// </remarks>
    public class clsLlojBuxheti
    {
        #region Atribute

        private int idLlojBuxheti;
        private string kodLlojBuxheti;
        private bool perAutorizim;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsLlojBuxheti(int idllojbuxheti, String kodllojbuxheti, bool perAutorizim)
        {
            idLlojBuxheti = idllojbuxheti;
            kodLlojBuxheti = kodllojbuxheti;
            this.perAutorizim = perAutorizim;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsLlojBuxheti(int idllojbuxheti, String kodllojbuxheti)
        {
            idLlojBuxheti = idllojbuxheti;
            kodLlojBuxheti = kodllojbuxheti;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="kodllojbuxheti"></param>
        public clsLlojBuxheti(String kodllojbuxheti)
        {
            kodLlojBuxheti = kodllojbuxheti;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsLlojBuxheti()
        {
        }

        public clsLlojBuxheti(DataRow rreshti)
        {
            
            mbushLlojBuxheti(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdLlojBuxheti
        {
            get { return idLlojBuxheti; }
            set { idLlojBuxheti = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e llojit te buxhetit
        /// </summary>
        public String KodLlojBuxheti
        {
            get { return kodLlojBuxheti; }
            set { kodLlojBuxheti = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese lloji i buxhetit eshte per autorizim
        /// </summary>
        public bool PerAutorizim
        {
            get { return perAutorizim; }
            set { perAutorizim = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e llojit te buxhetit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajLlojBuxheti"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            int id;
            clsMesazh u_ruajt = data.ruajLlojBuxheti(out id, this.KodLlojBuxheti, this.PerAutorizim);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e llojit te buxhetit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoLlojBuxheti"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            clsMesazh u_modifikua = data.modifikoLlojBuxheti(this.IdLlojBuxheti, this.KodLlojBuxheti, this.PerAutorizim);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e llojit te buxhetit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiLlojBuxheti"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshi()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            clsMesazh u_fshi = data.fshiLlojBuxheti(this.IdLlojBuxheti);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Kthen/Vendos nje objekt lloj buxheti. Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrLlojBuxheti"/>
        /// </summary>
        public void merr()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            data.merrLlojBuxheti(this.IdLlojBuxheti);
            data.Dispose();
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit lloj buxheti. Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheGjitheLlojeBuxhetesh"/>
        /// </summary>
        public colLlojeBuxhetesh merriTeGjithe()
        {
            colLlojeBuxhetesh data = new colLlojeBuxhetesh();
            data.mbushGjitheLlojeBuxhetesh();
            return data;
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrGjitheLlojeBuxhetesh();

        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e llojit te buxhetit sipas kodit
        /// </summary>
        /// <param name="kodi">kodi i llojit te buxhetit</param>
        /// <returns>id e llojit te buxhetit</returns>
        public static int mbushIDLlojBuxheti(String kodi)
        {
            using (clsDatabaseKontabilitet dbLlojBuxheti = new clsDatabaseKontabilitet())
            {
                return (dbLlojBuxheti.ktheLlojBuxhetiSipasKodit(kodi));
            }
        }

        public static int mbushIDLlojBuxheti(String kodi, clsDatabaseKontabilitet dbLlojBuxheti)
        {
            return dbLlojBuxheti.ktheLlojBuxhetiSipasKodit(kodi);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush llojet e buxhetit nga databaza
        /// </summary>
        /// <param name="dbDataRowLlogari">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushLlojBuxheti(DataRow dbDataRowLlojBuxheti)
        {
            if (dbDataRowLlojBuxheti != null)
            {
                try
                {
                    int.TryParse(dbDataRowLlojBuxheti["IDLLOJBUXHETI"].ToString(), out idLlojBuxheti);
                    kodLlojBuxheti = dbDataRowLlojBuxheti["KODLLOJBUXHETI"].ToString();
                    perAutorizim = Convert.ToBoolean(dbDataRowLlojBuxheti["PERAUTORIZIM"]);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llojit te buxhetit nga db-ja");
                }
            }
            else
                return false;
        }
        #endregion
    }
}
