using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbInventari
{
    public class colSerialeUnikeFormate : List<clsSerialeUnikeFormate>, IDataBase
    {
        #region Konstruktor

        public colSerialeUnikeFormate()
        {
        }
        public colSerialeUnikeFormate(int idNdermarrje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                db.mbushSerialeFormateSipasNdermarrje(idNdermarrje, this);
            foreach (clsSerialeUnikeFormate formatSeriali in this)
            {
                formatSeriali.ColSeriale.mbushSipasFormatit(formatSeriali.ID);
            }
        }

        #endregion

        #region Metoda Publike
        public static DataTable merrSerialeNdermarjeDT(int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                return db.mbushSerialeFormateSipasNdermarrjeDT(idndermarje);
        }
        public static DataRow merrSerialeNdermarjeDR(int id)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                return db.mbushSerialeFormateSipasNdermarrjeDR(id);
        }

        public void Mbush(IDataRecord record)
        {
            Add(new clsSerialeUnikeFormate(record));
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Ruaj()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public clsSerialeUnikeFormate merrFormatSipasSerialitUnik(string serialiPerKontroll)
        {
            var result = merrFormatDheSerialKryesorSipasSerialitUnik(serialiPerKontroll);
            if (result == null)
                throw new MyException($"Seriali {serialiPerKontroll} nuk i perket asnje formati te regjistruar me pare!");

            return result.Item1;
        }

        public Tuple<clsSerialeUnikeFormate, bool> merrFormatDheSerialKryesorSipasSerialitUnik(string serialiPerKontroll)
        {
            foreach (clsSerialeUnikeFormate formati in this)
            {
                foreach (clsSerialeUnike seriali in formati.ColSeriale)
                {
                    if (seriali.Validate(serialiPerKontroll) && Regex.Match(serialiPerKontroll, seriali.FormuleSpecifike).Success)
                        return new Tuple<clsSerialeUnikeFormate, bool>(formati, seriali.SerialKryesor);
                }

            }
            return null;
        }

        public void mbushSipasKategorise(int idkategori)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                db.mbushFormateSipasKategorise(idkategori, this);
            foreach (clsSerialeUnikeFormate formatSeriali in this)
            {
                formatSeriali.ColSeriale.mbushSipasFormatit(formatSeriali.ID);
            }
        }

        #endregion
    }
}
