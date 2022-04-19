using DbCore.IMBUtils.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbInventari
{
    public class colSerialeUnikeFusha : List<clsSerialeUnikeFusha> , IDataBase
    {
        #region Konstruktor


        public colSerialeUnikeFusha()
        {
         
        }
        public colSerialeUnikeFusha(int idKategori)
        {
            this.mbushFushaSipasKategorise(idKategori);
        }
        #endregion

        #region Metoda Publike

        public void merrTegjithaFushat()
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
               db.mbushSerialeUnikeFusha(this);
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            Add(new clsSerialeUnikeFusha(record));
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Modifiko(int idKategori)
        {

            clsMesazh mesazh = new MesazhGabimi();
            mesazh = Fshi(idKategori);
            if (mesazh.Status)
            {
                foreach (clsSerialeUnikeFusha seriali in this)
                {
                    mesazh = seriali.RuajLidhjeFushaKategori(idKategori);
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            return mesazh;
        }

        
        public clsMesazh Ruaj()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Ruaj(int idKategori)
        {
            clsMesazh mesazh = new clsMesazh(true, "Ruajtja perfundoi me sukse!");
            foreach (clsSerialeUnikeFusha serialiFusha in this)
            {
                mesazh = serialiFusha.RuajLidhjeFushaKategori(idKategori);
                if (!mesazh.Status)
                    return mesazh;
            }
            return mesazh;
        }

        
        public clsMesazh Fshi(int idKategori)
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                mesazh = db.fshiGjitheLidhjeSerialeFushaImportiKategori(idKategori);
            return mesazh;
        }

        public void mbushFushaSipasKategorise(int idkategori)
        {
            using (clsDatabaseInventari serialUnikeFusha = new clsDatabaseInventari())
                serialUnikeFusha.mbushFushaImportiSipasKategori(idkategori, this);
        }
        #endregion

        #region Metoda Internal


        #endregion


    }
}
