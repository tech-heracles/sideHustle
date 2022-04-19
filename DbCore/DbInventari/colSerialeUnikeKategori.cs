using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    public class colSerialeUnikeKategori : List<clsSerialeUnikeKategori>, IDataBase
    {
        #region Konstruktor
        public colSerialeUnikeKategori() { }
        public colSerialeUnikeKategori(int idNdermarrje)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                serialUnike.mbushSerialeKategoriSipasNdermarrje(idNdermarrje, this);
            foreach (clsSerialeUnikeKategori kategoria in this)
            {
                kategoria.ColFormateSeriali.mbushSipasKategorise(kategoria.ID);
            }
        }

        #endregion

        #region Metoda Publike
        public static DataTable merrSerialeNdermarjeDT(int idndermarje)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                return serialUnike.mbushSerialeKategoriSipasNdermarrjeDT(idndermarje);
        }
        public static DataRow merrSerialeNdermarjeDR(int id)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                return serialUnike.mbushSerialeKategoriSipasNdermarrjeDR(id);
        }
        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            Add(new clsSerialeUnikeKategori(record));
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Ruaj()
        {
            throw new NotImplementedException();
        }



        public clsSerialeUnikeKategori merrKategoriSipasSerialitUnik(string serialiPerKontroll, out int formati)
        {
            (clsSerialeUnikeKategori kategori, clsSerialeUnike seriali, int idFormati) = MerrKategoriDheSerialKryesorSipasSerialitUnik(serialiPerKontroll);
            
            if (kategori == null)
                throw new MyException(MessagesResource.Messages["msgSerialiNukIPerketAsnjeFormati"].Replace("XXX", serialiPerKontroll));
            formati = idFormati;
            return kategori;
        }
        public (clsSerialeUnikeKategori kategori, clsSerialeUnike seriali, int idFormati) MerrKategoriDheSerialKryesorSipasSerialitUnik(string serialiPerKontroll)
        {
            foreach (clsSerialeUnikeKategori kategoria in this)
            {
                var seriali = kategoria.GjejSerialinUnik(serialiPerKontroll, out int idFormati);
                if (seriali != null)
                    return (kategoria, seriali, idFormati);

            }
            return (null, null, 0);
        }


        public clsSerialeUnikeKategori MerrKategoriSipasIdFormatit(int idFormati)
        {
            return this.FirstOrDefault(k => k.ColFormateSeriali.Exists(f => f.ID == idFormati));
        }
        #endregion
    }
}
