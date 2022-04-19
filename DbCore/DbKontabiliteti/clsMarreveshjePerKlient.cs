using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbKontabiliteti
{
    public class clsMarreveshjePerKlient
    {
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe vendoset automatikisht
        /// </summary>
        public int IdCross { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e klientit
        /// </summary>
        public int IdKlient { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e marreveshjes
        /// </summary>
        public int IdLlojMarreveshje { get; set; }

        public string Kodi { get; set; }

        public string Pershkrimi { get; set; }

        #endregion

        #region Konstruktoret

        public clsMarreveshjePerKlient(DataRow rreshti)
        {
            MbushMarreveshjePerKlient(rreshti);
        }

        public clsMarreveshjePerKlient(int idLlojMarreveshje, string pershkrimMarreveshje, int idKlient)
        {
            IdLlojMarreveshje = idLlojMarreveshje;
            Pershkrimi = pershkrimMarreveshje;
            IdKlient = idKlient;
        }

        #endregion
        
        #region Metoda Internal
        internal clsMesazh RuajMarreveshjePerKlient(clsDatabaseKontabilitet db)
        {
            return db.ruajMarreveshjePerKlient(IdLlojMarreveshje, IdKlient);
        }

        /// <summary>
        /// mbush marreveshjen per klientin nga databaza
        /// </summary>
        /// <param name="dbDataRowMarreveshjePerKlient">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void MbushMarreveshjePerKlient(DataRow dbDataRowMarreveshjePerKlient)
        {
            if (dbDataRowMarreveshjePerKlient != null)
            {
                try
                {
                    IdCross = !IsDBNull(dbDataRowMarreveshjePerKlient["IDCROSS"])
                        ? ToInt32(dbDataRowMarreveshjePerKlient["IDCROSS"])
                        : 0;
                    IdLlojMarreveshje = !IsDBNull(dbDataRowMarreveshjePerKlient["IDLLOJMARREVESHJE"])
                        ? ToInt32(dbDataRowMarreveshjePerKlient["IDLLOJMARREVESHJE"])
                        : 0;
                    IdKlient = !IsDBNull(dbDataRowMarreveshjePerKlient["IDKLIENTI"])
                        ? ToInt32(dbDataRowMarreveshjePerKlient["IDKLIENTI"])
                        : 0;
                    Kodi = dbDataRowMarreveshjePerKlient["KODI"].ToString();
                    Pershkrimi = dbDataRowMarreveshjePerKlient["PERSHKRIMI"].ToString();
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se marreveshjeve te klientit nga db-ja");
                }
            }
        }

        #endregion
    }
}