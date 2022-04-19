using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbListPagesat
{
  public  class colKomponenteMuaji: List<clsKomponenteMuaji>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parameter
        /// </summary>
        public colKomponenteMuaji()
        {
        }

    
        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKomponenteMuaji</param>
        public colKomponenteMuaji(IEnumerable<clsKomponenteMuaji> collection)
            : base(collection)
        {

        }

      public static Dictionary<int, colKomponenteMuaji> MerrDicKomponenteMuaji(List<int> kompIds)
      {
          var komps = clsKomponenteMuaji.MerrListKomponenteshMuaji(kompIds);
          return komps.GroupBy(x => x.IdKompListPagese).ToDictionary(grp => grp.Key, grp => new colKomponenteMuaji(grp));
      }

      /// <summary>
        /// merr si parameter idKomponenteLispagese dhe vleren e fundit inkrementale te KomponenteMuaji dhe i vendos ne per secilin objekt
        /// </summary>
        /// <param name="idKomponente"></param>
        /// <param name="colKompMuajiCounter"></param>
      public void VendosIdKomponenteLP(int idKomponente, ref int colKompMuajiCounter)
      {
          foreach (clsKomponenteMuaji kompMuaji in this)
          {
              kompMuaji.Id = ++colKompMuajiCounter;
              kompMuaji.IdKompListPagese = idKomponente;
          }
      }

      /// <summary>
        /// kontrukutori me 1 parameter
        /// merr komponentet e list pageses sipas id se trupit te dokumentit te list pageses
        /// </summary>
        /// <param name="idtrupi"> idtrupi</param>
        public colKomponenteMuaji(int idKomponenteLp)
            :base(new clsDatabazeListPagesa().ktheKompMuajiSipasIdTrupi(idKomponenteLp))
        {

        }

        public colKomponenteMuaji(List<int> kompLpIds) : base(clsKomponenteMuaji.MerrListKomponenteshMuaji(kompLpIds))
        {

        }
        /// <summary>
        /// mbush objektin me te dhenat qe vijne nga grida 
        /// </summary>
        /// <param name="komp"> objekt qe permban te dhenat e komponenteve  pas deserializimit</param>
        public colKomponenteMuaji( object komp)
        {
            object[] trup = (object[])(komp);
            foreach (object t in trup)
            {
                if (t != null)
                {
                    clsKomponenteMuaji fat = new clsKomponenteMuaji((Dictionary<string, object>)t);
                    Add(fat);
                }
            }
        }

      #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKomponenteMuaji"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKomponenteMuaji this[int index]
        {
            get { return base[index]; }
            set { base[index] = value; }
        }
        public void ShtoMeIdNegative(clsKomponenteMuaji kompNgaImporti)
        {
            //bere +1 per te marr parasysh rastin kur count eshte 0
            kompNgaImporti.Id = -(GetNewId());
            Add(kompNgaImporti);
        }
        public int GetNewId()
        {
            if (Count == 0) return 1;
            return this.Max(x => Math.Abs(x.Id))  + 1;
        }
        /// <summary>
        /// merr komponente sipas idtrupit
        /// </summary>
        /// <param name="idtrupi"> id e trupit te dokumentit te list pageses</param>
        public void merrKomponenteSipasIdTrupit(int idtrupi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            this.AddRange(db.ktheKompMuajiSipasIdTrupi(idtrupi));
            db.Dispose();
        }


        public colKomponenteMuaji Clone()
        {
            var col = new colKomponenteMuaji { Capacity = this.Count };
            foreach (var cls in this)
            {
                col.Add(cls.Clone());
            }
           
            return col;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt"> data table me te dhenat e tipit komp list pagese</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        //private void mbushKompMuaji(DataTable dt)
        //{
        //    foreach (DataRow rreshti in dt.Rows)
        //          {
        //              clsKomponenteMuaji komp = new clsKomponenteMuaji();
        //              komp.mbushKomponenteMuaji(rreshti);
        //              Add(komp);
        //          }
        //}

        #endregion


    }
}



