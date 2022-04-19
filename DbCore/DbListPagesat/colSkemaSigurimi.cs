using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsSkemaSigurimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsSkemaSigurimi"/>
    public class colSkemaSigurimi : List<clsSkemaSigurimi>
    {
        private const string gabimskemasigurimi = "Skema e sigurimit nuk u ruajt!";
        private const string gabimpunonjesi = "Punonjesi nuk u ruajt!";
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parameter
        /// </summary>
        public colSkemaSigurimi()
        {
        }

        public colSkemaSigurimi(List<int> idPunonjesish, DateTime data):base(new clsDatabazeListPagesa().MerrSigurimetPerGjithePunonjesitSipasDates(idPunonjesish,data))
        {
            

        }

        /// <summary>
        ///  konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsSkemaSigurimi </param>
        public colSkemaSigurimi(IEnumerable<clsSkemaSigurimi> collection)
            : base(collection)
        {

        }

        /// <summary>
        /// konstruktroi qe implementon klasen baze duke i dhene madhesine e koleksionit
        /// </summary>
        /// <param name="capacity">madhesia e koleksionit</param>
        public colSkemaSigurimi(int capacity)
            : base(capacity)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsSkemaSigurimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsSkemaSigurimi this[int index]
        {
            get { return ((clsSkemaSigurimi)base[index]); }
        }

        internal clsMesazh modifikoSkeme(clsDatabazeListPagesa db, clsSkemaSigurimi skemaVjeter, int idPunonjes, int idPerdoruesi, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            clsMesazh mesazh = new clsMesazh();
            foreach (clsSkemaSigurimi skema in this)
            {
                string fushatmod = "";
                string mesazhmod = "";
                skema.IdPunonjes = idPunonjes;
                mesazhmod = clsSkemaSigurimi.kontrolloskema(skema, skemaVjeter, out fushatmod, db);
                if (skemaVjeter.IdSkemaSig == 0)
                    mesazh = skema.ruaj(db);
                else
                {
                    skema.IdSkemaSig = skemaVjeter.IdSkemaSig;
                    mesazh = skema.modifiko(db);
                }
                if (!mesazh.Status)
                    return new clsMesazh(false, rm.GetString("msgPunonjesSkemaSig", ci));
                if (mesazhmod != "U modifikuan fushat per skemat:")
                {
                    mesazh = clsPunonjes.ruajLog(idPunonjes, idPerdoruesi, 1, mesazhmod, fushatmod, db);
                    if (!mesazh.Status)
                        return new clsMesazh(false, rm.GetString("msgPunonjesRuajFail", ci));
                }
            }
            return new clsMesazh(true, "Skema u ruajt me sukses!");
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt"> data table me te dhena te tipit skema sigurimi</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushSkemaSigurimi(DataTable dt)
        {

            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsSkemaSigurimi(rreshti));
            }
            return true;
        }
        #endregion
    }
}
