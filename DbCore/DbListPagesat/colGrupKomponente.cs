using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;

namespace DbCore.DbListPagesat
{
  public  class colGrupKomponente: List<clsGrupKomponente>
    {
        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colGrupKomponente()
        {

        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// merr grupet e punonjesve te nje ndermarje
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        public colGrupKomponente(int idnderm)
        {
            using (clsDatabazeListPagesa data = new clsDatabazeListPagesa())
            {
                mbushGrupeKomponente(data.ktheGjitheGrupetKomponenteSipasNdermarjes(idnderm));
            }
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsGrupKomponente</param>
        public colGrupKomponente(IEnumerable<clsGrupKomponente> collection)
            : base(collection)
        {
            
        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="clsGrupKomponente"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsGrupKomponente this[int index]
        {
            get { return ((clsGrupKomponente)base[index]); }
        }


        #endregion

        #region Metoda Private
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipi grup punonjes</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushGrupeKomponente(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupKomponente grup = new clsGrupKomponente();
                    //grup.mbushGrupKomponente(rreshti);
                    Add(new clsGrupKomponente(rreshti));
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
