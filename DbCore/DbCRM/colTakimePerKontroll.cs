using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbCRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;


    public class colTakimePerKontroll : List<clsTakimePerKontroll>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colTakimePerKontroll()
        {
        }

        public colTakimePerKontroll(string kodNdermarrje,DateTime data )
            : base(new DbCore.DbCRM.clsDatabaseCRM().MerrListenETakimevePerKontroll(data, kodNdermarrje))
        {
            //clsDatabaseAdmin data = new clsDatabaseAdmin();
            //mbushGridaTrupa(data.merrGridenKonfigurimitKomponentesSipasGrides(emriGrides, idKomponente, idKonfigurim, idGjuha));
            //data.Dispose();
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsTakimePerKontroll"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTakimePerKontroll this[int index]
        {
            get { return ((clsTakimePerKontroll)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsTakimePerKontroll ne nje arraylist
        /// </summary>
        public bool shtoDetyre(clsTakimePerKontroll TakimePerKontroll)
        {
            base.Add(TakimePerKontroll);
            if (base.Contains(TakimePerKontroll))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTakimePerKontroll ne nje arraylist
        /// </summary>
        public bool fshiDetyre(clsTakimePerKontroll TakimePerKontroll)
        {
            base.Remove(TakimePerKontroll);
            if (base.Contains(TakimePerKontroll))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e  objekteve clsTakimePerKontroll ne nje arraylist
        /// </summary>
        public bool fshiGjitheTakimePerKontrollt()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTakimePerKontroll ne nje arraylist
        /// </summary>
        public void fshiKeteDetyre(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoTakimePerKontrollNeIndeksin(int index, clsTakimePerKontroll TakimePerKontroll)
        {
            base.Insert(index, TakimePerKontroll);

        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiTakimePerKontroll(clsTakimePerKontroll TakimePerKontroll)
        {
            return base.IndexOf(TakimePerKontroll);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonTakimePerKontroll(clsTakimePerKontroll TakimePerKontroll)
        {
            if (base.Contains(TakimePerKontroll))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriTakimePerKontroll()
        {
            return base.Count;
        }

        


        #endregion

    }
}


