using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Globalization;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKomponentePage
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsKomponentePage"/>
    public class colKomponentePage : List<clsKomponentePage>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKomponentePage()
        {
        }
        public colKomponentePage(List<int> kompPageIds) : base(MerrKomponentetSipasId(kompPageIds))
        {

        }
        /// <summary>
        /// konstruktori me 2 parametra
        /// merr komponentet e pagese te ndermarjes sipas llojit
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="lloj">lloji komponente apo listpagese</param>
        /// <see cref="LlojKomponentePage"/>
        public colKomponentePage(int idndermarje, bool lloj)
            : base(new clsDatabazeListPagesa().ktheGjitheKomponentePageSipasNdermarjesDheLlojit(idndermarje, lloj))
        {

        }
        /// <summary>
        /// konstruktori me 3 parametra
        /// merr komponente page te ndermarjes sipas llojit ne nje date te caktuar aktivizimi
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="lloj">lloji komponente apo listpagese</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <see cref="LlojKomponentePage"/>
        public colKomponentePage(int idndermarje, bool lloj, DateTime data)
            : base(new clsDatabazeListPagesa().ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDates(idndermarje, lloj, data))
        {

        }


        /// <summary>
        ///  konstruktroi qe implementon klasen baze duke i dhene madhesine e koleksionit
        /// </summary>
        /// <param name="capacity">madhesia e koleksionit</param>
        public colKomponentePage(int capacity)
            : base(capacity)
        {

        }
        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKomponentePage</param>
        public colKomponentePage(IEnumerable<clsKomponentePage> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKomponentePage"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKomponentePage this[int index]
        {
            get
            {
                return ((clsKomponentePage)base[index]);
            }
        }

        /// <summary>
        /// merr komponenten sipas id ne formen e data row
        /// </summary>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="idkomponente"> id komponente</param>
        /// <returns> kthen data row me kete komponente page</returns>
        public static DataRow merrKomponentePageSipasNdermarjesDR(int idnderm, int idkomponente)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataRow dr = db.merrKomponentePageSipasNdermarjesDR(idnderm, idkomponente);
            db.Dispose();
            return dr;
        }
        public colKomponentePage ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfert(int idndermarje, bool lloj, DateTime data)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                this.AddRange(db.ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfert(idndermarje, lloj, data));

            }
            return this;
        }
        public colKomponentePage ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfertFormule(int idndermarje, bool lloj, DateTime data)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                this.AddRange(db.ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfertFormule(idndermarje, lloj, data));

            }
            return this;
        }
        public colKomponentePage ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfertNr(int idndermarje, bool lloj, DateTime data)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                this.AddRange(db.ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfertNr(idndermarje, lloj, data));

            }
            return this;
        }
        /// <summary>
        /// merr komponente sipas ndermarje dhe llojit ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <param name="lloji">lloji</param>
        /// <see cref="LlojKomponentePage"/>
        /// <returns> kthen data table me keto komponente</returns>
        public static DataTable merrKomponentePageNdermarjeDTSipasLlojit(int idnderm, bool lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrKomponentePageDTSipasLlojit(idnderm, lloji);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// merr komponente sipas ndermarje dhe llojit dhe dates se aktivizimit ne formene e data table 
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <param name="lloji">lloji</param>
        /// <param name="data">data e aktivizimit</param>
        /// <see cref="LlojKomponentePage"/>
        /// <returns> kthen data table me keto komponente</returns>
        public static DataTable merrKomponentePageNdermarjeDTSipasLlojitDheDates(int idnderm, bool lloji, DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrKompnentePageNdermarjeDTSipasLlojitDheDates(idnderm, lloji, data);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// mbush te gjitha komponentet sipas ndermarrjes dhe llojit
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <param name="lloji">lloji</param>
        /// <see cref="LlojKomponentePage"/>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKomponentePageSipasNdermarjesDheLlojit(int idnder, bool lloji)
        {
            IEnumerable<clsKomponentePage> komponPage;
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                komponPage = (db.ktheGjitheKomponentePageSipasNdermarjesDheLlojit(idnder, lloji));

            }
            this.AddRange(komponPage);
            return komponPage != null;

        }
        public static List<DateTime> merrDataKomponente(int idnderm, bool lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            var list = new List<DateTime>();
            DataTable dt = db.merrDataKomponentePageSipasNdermarjesDheLlojit(idnderm, lloji);
            foreach (DataRow rreshti in dt.Rows)
            {
                list.Add(DateTime.Parse(rreshti["Data"].ToString()).Date);
            }
            db.Dispose();
            return list;
        }

        /// <summary>
        /// mbush te gjitha komponentet sipas ndermarrjes dhe llojit aktive
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <param name="lloji">lloji</param>
        /// <see cref="LlojKomponentePage"/>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKomponentePageSipasNdermarjesDheLlojitAktive(int idnder, bool lloji)
        {
            IEnumerable<clsKomponentePage> komponPage;
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                komponPage = (db.ktheGjitheKomponentePageSipasNdermarjesDheLlojitAktiv(idnder, lloji));

            }
            this.AddRange(komponPage);
            return komponPage != null;
        }

        public static colKomponentePage MerrKomponentetSipasId(List<int> komponentePageIds)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {

                //te gjitha komponentet per gjithe punonjesit
                return new colKomponentePage(db.ktheKomponentePagePerKetoId(komponentePageIds));
            }
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit komponente page</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushKomponente(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsKomponentePage komp = new clsKomponentePage();
                //komp.mbushKomponente(rreshti);
                Add(new clsKomponentePage(rreshti));
            }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion


    }
}
