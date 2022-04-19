using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbShare
{
    public class colFormatKonfig : System.Collections.Generic.List<clsFormatiKonfig>
    {
        #region Konstruktoret

        public colFormatKonfig()
        {
        }

        public colFormatKonfig(int idNdermarrja)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            mbushKonfigurimFormatesh(data.ktheGjitheFormate(idNdermarrja));
            data.Dispose();
        }

        public colFormatKonfig(int idKat, int idNderm)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            mbushKonfigurimFormatesh(data.ktheFormatNrSipasKatNderm(idKat, idNderm));
            data.Dispose();
        }

        #endregion

        #region Metoda Publike

        public static DataTable merrGjitheFormatetSipasNdermarrjes(int idNdermarrje)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            DataTable table = data.ktheGjitheFormate(idNdermarrje);
            data.Dispose();
            return table;
        }

        public static DataTable merrGjitheFormatetSipasKategoriseDheNdermarrjes(int idNdermarrje, int idKategori)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            DataTable table = data.ktheFormatNrSipasKatNderm(idKategori, idNdermarrje);
            data.Dispose();
            return table;
        }

        

        public new clsFormatiKonfig this[int index]
        {
            get { return ((clsFormatiKonfig)base[index]); }
        }

        public clsMesazh ruajFormatet(colFormatKonfig formatet)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseShare dbshare = new clsDatabaseShare();
          
            try
            {
                dbshare.beginTransaksion();
                int idNdermarrja = formatet[0].IdNdermarrja;
                mesazh = dbshare.fshiGjitheFormate(idNdermarrja);
                if (mesazh.Status)
                {
                    foreach (clsFormatiKonfig o in formatet)
                    {
                        int idFormatKonfig = -1;
                        if (mesazh.Status)
                            mesazh = dbshare.ruajFormat(out idFormatKonfig, o.IdKategoria, o.IdNdermarrja, o.IdStatusDok, o.Kodi, o.Emertimi, o.IdKrijuesi);
                        //, o.IdMonedha, o.IdFormatSasia, o.IdFormatCmimi, o.IdFormatVlefta, o.IdFormatZbritja
                        else
                        {                            
                            dbshare.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                    if (mesazh.Status)
                    {
                        dbshare.commitTransaksion();                        
                        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                        return mesazh;
                    }
                    else
                    {
                        
                        dbshare.rollbackTransaksion();
                        return mesazh;
                    }
                }
                else
                {
                    
                    dbshare.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
            }
            catch (Exception ce)
            {
                
                dbshare.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        #endregion 

        #region Metoda Private

        private bool mbushKonfigurimFormatesh(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFormatiKonfig konfig = new clsFormatiKonfig();
                    //konfig.mbushFormatKonfig(rreshti);
                    Add(new clsFormatiKonfig(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushKonfigurimFormatesh(DataTable dt)", true)]
        public colFormatKonfig mbushArrayListKonfigurimFormatesh(DataSet ds)
        {
            colFormatKonfig col = new colFormatKonfig();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsFormatiKonfig konfig = new clsFormatiKonfig();

                konfig.IdFormatKonfig = int.Parse(rreshti[0].ToString());
                konfig.IdKategoria = int.Parse(rreshti[1].ToString());
                //konfig.IdMonedha = int.Parse(rreshti[2].ToString());
                //konfig.IdFormatSasia = int.Parse(rreshti[3].ToString());
                //konfig.IdFormatCmimi = int.Parse(rreshti[4].ToString());
                //konfig.IdFormatVlefta = int.Parse(rreshti[5].ToString());
                //konfig.IdFormatZbritja = int.Parse(rreshti[6].ToString());
                konfig.IdNdermarrja = int.Parse(rreshti[7].ToString()); 
                //konfig.KodiMonedha = rreshti[8].ToString();
                //konfig.FormatSasi = rreshti[9].ToString();
                //konfig.FormatCmimi = rreshti[10].ToString();
                //konfig.FormatVlefta = rreshti[11].ToString();
                //konfig.FormatZbritja = rreshti[12].ToString();
                konfig.Kategoria = rreshti[13].ToString();
                col.Add(konfig);
            }
            return col;
        }
    }
}
