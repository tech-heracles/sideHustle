using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsArtikulliPerberesTemplateTrupi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
   public class colArtikulliPerberesTemplateTrupi : System.Collections.Generic.List<clsArtikulliPerberesTemplateTrupi>
   {
       public colArtikulliPerberesTemplateTrupi() { }
       public colArtikulliPerberesTemplateTrupi(DbCore.DbInventari.colArtikulliPerberes colArtikujtPerberes)
       {
           foreach (DbCore.DbInventari.clsArtikulliPerberes a in colArtikujtPerberes)
           {
               DbCore.DbInventari.clsArtikulliPerberesTemplateTrupi oTrupi = new DbCore.DbInventari.clsArtikulliPerberesTemplateTrupi(a.Lloji, a.IdLidheseArt,(int) a.Koeficienti, a.Scrap, a.IdLidheseAkt);
               this.Add(oTrupi);
           }
       }
       #region Metoda publike

       /// <summary>
       /// kthen objektin <see cref="DbCore.DbInventari.clsArtikulliPerberesTemplateTrupi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
       /// </summary>
       public new clsArtikulliPerberesTemplateTrupi this[int index]
        {
            get { return ((clsArtikulliPerberesTemplateTrupi)base[index]); }
        }

       /// <summary>
       /// mbush artikujt perberes me trup template sipas kokes
       /// </summary>
       /// <param name="idKoka">id e kokes</param>
       /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
       public bool mbushTrupTemplateSipasKokes(int idKoka)
       {
           clsDatabaseInventari dbArtikujPerberes = new clsDatabaseInventari();
           bool sukses = mbushArtikujPerberes(dbArtikujPerberes.ktheTrupTemplateSipasKokes(idKoka));
           dbArtikujPerberes.Dispose();
           return sukses;
       }

       #endregion

       #region Metoda Private
       /// <summary>
       /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
       /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 
       ///me konvertimet e nivelit
       ///  <see cref="DbCore.DbInventari.clsArtikulliPerberesTemplateTrupi"/> 
       /// </summary>
       /// 
       private bool mbushArtikujPerberes(DataTable dt)
       {
           //try
           //{

               foreach (DataRow rreshti in dt.Rows)
               {
                   //clsArtikulliPerberesTemplateTrupi art = new clsArtikulliPerberesTemplateTrupi();
                   //art.mbushArtikulliPerberesTemplateTrupi(rreshti);
                   this.Add(new clsArtikulliPerberesTemplateTrupi(rreshti));
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
       public colArtikulliPerberesTemplateTrupi mbushArrayListArtikujPerberes(DataSet ds)
        {
            colArtikulliPerberesTemplateTrupi artikuj = new colArtikulliPerberesTemplateTrupi();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsArtikulliPerberesTemplateTrupi art = new clsArtikulliPerberesTemplateTrupi();

                art.IdTrupi = int.Parse(rreshti[0].ToString());
                art.Lloji = int.Parse(rreshti[1].ToString());
                art.IdKoka = int.Parse(rreshti[2].ToString());
                art.IdLidheseArt = int.Parse(rreshti[3].ToString());
                art.Koeficienti = int.Parse(rreshti[4].ToString());
                art.Vlera = decimal.Parse(rreshti[5].ToString());
                artikuj.Add(art);
            }
            return artikuj;
        }
    }
}