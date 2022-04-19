using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    public class colSerialeUnike : List<clsSerialeUnike>, IDataBase
    {
        #region Konstruktor

        public colSerialeUnike()
        {

        }

        public colSerialeUnike(int idNdermarrja)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                serialUnike.mbushSerialeSipasNdermarrje(idNdermarrja, this);
        }

        #endregion

        #region Metoda Publike 

        public static DataTable merrSerialeNdermarjeDT(int idndermarje)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
            return    serialUnike.mbushSerialeSipasNdermarrjeDT(idndermarje);
        }
        public static DataRow merrSerialeNdermarjeDR(int id)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                return serialUnike.mbushSerialeSipasNdermarrjeDR(id);
        }
        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            Add(new clsSerialeUnike(record));
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Ruaj()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Ruaj(int idFormat)
        {
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            foreach (clsSerialeUnike seriali in this)
            {
                mesazh = seriali.RuajLidhje(idFormat);
                if (!mesazh.Status)
                    return mesazh;
            }
            return mesazh;
        }

        public clsMesazh Modifiko(int idFormat)
        {
            clsMesazh mesazh = new MesazhGabimi();
            mesazh = Fshi(idFormat);
            if (mesazh.Status)
            {
                foreach (clsSerialeUnike seriali in this)
                {
                    mesazh = seriali.RuajLidhje(idFormat);
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            return mesazh;
        }

        public clsMesazh Fshi(int idFormat)
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                mesazh = serialUnike.fshiGjitheLidhjeSerialeUnikeFormat(idFormat);
            return mesazh;
        }
        public void mbushSipasFormatit(int idformati)
        {
            merrSerialeSipasFormatit(idformati);
        }

        #endregion

        #region Metoda Internal

        internal void merrSerialeSipasFormatit(int id)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                serialUnike.mbushSerialeUnikeSipasFormatit(id, this);
        }

        #endregion

    }
}
