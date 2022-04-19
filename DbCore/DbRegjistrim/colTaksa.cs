using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTaksa
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTaksa : System.Collections.Generic.List<clsTaksa>,IDataBaseReader
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colTaksa()
        {
        }
        /// <summary>
        /// Konstruktor qe merr parameter id e fatures
        /// </summary>
        /// <param name="idkokashitje"></param>
        public colTaksa(int idkokashitje)
        {if(idkokashitje>0)
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                db.merrTaksatFature(idkokashitje, this);
            }
        
        }
        public colTaksa(int idkokashitje, colTaksa colTaksat, int idNderm, int IdPerdorues)
        {if(idkokashitje>0)
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                mbushTaksa(db.merrTaksatFatureDT(idkokashitje), colTaksat);
            }
        
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        ///<param name="idperdorues"> id e perdoruesit</param>
        ///<param name="idNderm">id e ndermarjes</param>
        public colTaksa(int idNderm, int idperdorues)
        {
            clsDatabaseRegjistrim dbTaksa = new clsDatabaseRegjistrim();
            dbTaksa.ktheGjitheTaksa(idNderm, idperdorues, this);
            dbTaksa.Dispose();
        }

        public static DataTable GetTaksaLookupSimpleTable(int idNdermarrje, LlojTakse nivel_Tvsh, int idPerdoruesi)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.GetTaksaLookupSimpleTable(idNdermarrje, nivel_Tvsh, idPerdoruesi);
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        ///<param name="idNderm">id e ndermarjes</param>
        ///<param name="idllojtakse">id e llojit te takses</param>
        ///<param name="idperdorues"> id e perdoruesit</param>
        public colTaksa(int idNderm, LlojTakse llojTakse, int idperdorues)
        {
            clsDatabaseRegjistrim dbTaksa = new clsDatabaseRegjistrim();
            mbushTaksa(dbTaksa.ktheGjitheTaksaSipasNdermarjesAndLlojitDT(idNderm, llojTakse, idperdorues, this));
            dbTaksa.Dispose();
        }
        public colTaksa(int idNderm, LlojTakse llojTakse, int idperdorues, clsDatabaseRegjistrim dbTaksa)
        {
            mbushTaksa(dbTaksa.ktheGjitheTaksaSipasNdermarjesAndLlojitDT(idNderm, llojTakse, idperdorues, this));
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsTaksa"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTaksa this[int index]
        {
            get { return ((clsTaksa)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsTaksa ne nje arraylist
        /// </summary>
        public bool shtoTaksa(clsTaksa t)
        {
            base.Add(t);
            if (base.Contains(t))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTaksa ne nje arraylist
        /// </summary>
        public bool fshiTaksa(clsTaksa t)
        {
            base.Remove(t);
            if (base.Contains(t))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTaksa ne nje arraylist
        /// </summary>
        public bool fshiGjitheTaksa()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTaksa ne nje arraylist
        /// </summary>
        public void fshiKeteTakse(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoTakseNeIndeksin(int index, clsTaksa t)
        {
            base.Insert(index, t);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiNivelRegjistrimi(clsTaksa t)
        {
            return base.IndexOf(t);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonTaksa(clsTaksa t)
        {
            if (base.Contains(t))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriTaksa()
        {
            return base.Count;
        }
        public static DataRow merrTaksaSipasNdermarjesDR(int idnderm, int idnivel)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataRow dr = dbRegj.merrTaksaSipasNdermarjesDR(idnderm, idnivel);
            dbRegj.Dispose();
            return dr;
        }
        public static DataTable merrTaksaNdermarjeDT(int idnderm, int idperdorues)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable dt = dbRegj.merrTaksaNdermarjeDT(idnderm, idperdorues);
            dbRegj.Dispose();
            return dt;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsTaksa"/> 
        /// </summary>
        public void Mbush(IDataRecord rreshti)
        {

            Add(new clsTaksa(rreshti));


        }
        private bool mbushTaksa(DataTable dt)
        {

            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsTaksa(rreshti));
            }
            return true;
        }
        private bool mbushTaksa(DataTable dt,colTaksa colTaksat)
        {

            foreach (DataRow rreshti in dt.Rows)
            {
                if (rreshti["IDTAKSA"].ToString() != "")
                    Add(colTaksat.Where(x => x.IdTaksa == int.Parse(rreshti["IDTAKSA"].ToString())).FirstOrDefault());
                else
                    Add(new clsTaksa());
            }
            return true;
        }

        #endregion

    }
}