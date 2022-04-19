using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsGrupeKFPerWorkFlow
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colGrupeKFPerWorkFlow: System.Collections.Generic.List<clsGrupeKFPerWorkFlow>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colGrupeKFPerWorkFlow()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e klientit</param>
        public colGrupeKFPerWorkFlow(int idtrupi)
        {
            clsDatabaseAdmin dbKontaktet = new  clsDatabaseAdmin ();
            mbushListGrupe(dbKontaktet.ktheGrupeKfPerWorkFlowSipasIdTrupi(idtrupi));
            dbKontaktet.Dispose();
        }

        #endregion
        
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsKontaktiKlientFurnitor"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsGrupeKFPerWorkFlow this[int index]
        {
            get { return ((clsGrupeKFPerWorkFlow)base[index]); }
        }

  
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsKontaktiKlientFurnitor"/> 
        /// </summary>
        private bool mbushListGrupe(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupeKFPerWorkFlow kontakt = new clsGrupeKFPerWorkFlow();
                    //kontakt.mbushGrupekfPerWorkFlow(rreshti);
                    Add(new clsGrupeKFPerWorkFlow(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
      
    }
}


