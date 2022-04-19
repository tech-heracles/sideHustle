using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{/// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsArtikullPerberesTemplateKoka
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
  public  class colArtikulliPerberesTemplateKoka : System.Collections.Generic.List<clsArtikullPerberesTemplateKoka>
  {

      #region Metoda Publike

      /// <summary>
      /// kthen objektin <see cref="DbCore.DbInventari.clsArtikullPerberesTemplateKoka"/>  qe ndodhet ne nje index te caktuar te arraylist-es
      /// </summary>
      public new clsArtikullPerberesTemplateKoka this[int index]
        {
            get { return ((clsArtikullPerberesTemplateKoka)base[index]); }
        }

      /// <summary>
      /// mbush gjithe artikujt perberes
      /// </summary>
      /// <returns>kthen true nese mbushja eshte me sukses, ne te kundert false</returns>
      public bool mbushGjitheTemplatetArtikujvePerberes( int idndermarje)
      {
          clsDatabaseInventari dbArtikujPerberes = new clsDatabaseInventari();
          bool sukses = mbushArtikujPerberes(dbArtikujPerberes.ktheGjitheTemplatetArtikujvePerberes(idndermarje));
          dbArtikujPerberes.Dispose();
          return sukses;
      }

      #endregion

      #region Metoda private

      /// <summary>
      /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
      ///  <see cref="DbCore.DbInventari.clsArtikullPerberesTemplateKoka"/> 
      /// </summary>
      private bool mbushArtikujPerberes(DataTable dt)
      {
          //try
          //{

              foreach (DataRow rreshti in dt.Rows)
              {
                  //clsArtikullPerberesTemplateKoka art = new clsArtikullPerberesTemplateKoka();
                  //art.mbushArtikullPerberesTemplateKoka(rreshti);
                  this.Add(new clsArtikullPerberesTemplateKoka(rreshti));
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
      [Obsolete("Perdor: bool mbushArtikujPerberes(DataTable dt)", true)]
      public colArtikulliPerberesTemplateKoka mbushArrayListArtikujPerberes(DataSet ds)
        {
            colArtikulliPerberesTemplateKoka artikuj = new colArtikulliPerberesTemplateKoka();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsArtikullPerberesTemplateKoka art = new clsArtikullPerberesTemplateKoka();

                art.IdKoka = int.Parse(rreshti[0].ToString());
                art.Kodi = rreshti[1].ToString();
                art.Pershkrimi = rreshti[2].ToString();
                artikuj.Add(art);
            }
            return artikuj;
        }
    }
}