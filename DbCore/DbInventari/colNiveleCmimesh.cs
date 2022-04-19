using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsNivelCmimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colNiveleCmimesh : System.Collections.Generic.List<clsNivelCmimi >
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colNiveleCmimesh()
        {
        }

        /// <summary>
        /// konstruktori me 2 parametra
        /// </summary>
        /// <param name="pershkrim">pershkrimi</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsNivelCmimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsNivelCmimi this[int index]
        {
            get { 
                return ((clsNivelCmimi)base[index]); 
                }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsNivelCmimi ne nje arraylist
        /// </summary>
        public bool shtoNivelCmimi(clsNivelCmimi nivelCmimi)
        {
            base.Add(nivelCmimi);
            if (base.Contains(nivelCmimi))
                return true;
            else return false;
        }

        public static DataTable GetNiveleCmimeshLookupSimpleTable(int idNdermarrje, int shitjeApoBlerje, int idPerdoruesi)
       {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                return db.GetNiveleCmimeshLookupSimpleTable(idNdermarrje, shitjeApoBlerje, idPerdoruesi);
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsNivelCmimi ne nje arraylist
        /// </summary>
        public bool fshiNivelCmimi(clsNivelCmimi nivelCmimi)
        {
            base.Remove(nivelCmimi);
            if (base.Contains(nivelCmimi))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsNivelCmimi ne nje arraylist
        /// </summary>
        public bool fshiGjitheNiveleCmimesh()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsNivelCmimi ne nje arraylist
        /// </summary>
        public void fshiKeteNivelCmimi(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoNivelCmimiNeIndeksin(int index, clsNivelCmimi nivelCmimi)
        {
            base.Insert(index, nivelCmimi);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiNivelCmimi(clsNivelCmimi nivelCmimi)
        {
            return base.IndexOf(nivelCmimi);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonNivelCmimi(clsNivelCmimi nivelCmimi)
        {
            if (base.Contains(nivelCmimi))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriNiveleveCmimi()
        {
            return base.Count;
        }

        public static DataRow merrNivelCmimiSipasNdermarjesDR(int idnderm, int idnivel)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataRow rreshti = dbartikuj.merrNivelCmimiSipasNdermarjesDR(idnderm, idnivel);
            dbartikuj.Dispose();
            return rreshti;
        }

        public static DataTable merrNiveleNdermarjeDT(int idnderm, int idperd)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.merrNiveleNdermarjeDT(idnderm, idperd);
            dbartikuj.Dispose();
            return tabela;
        }

        public static DataTable merrNiveleNdermarjeDTBlerjeShitje(int idnderm, int llojnivel, int idperd)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.merrNiveleNdermarjeDTBlerjeShitje(idnderm, llojnivel, idperd);
            dbartikuj.Dispose();
            return tabela;
        }

        
        public static DataTable merrNiveleNdermarjeDTBlerjeShitjeMeAutorizime(int idnderm, int llojnivel, int idPerdorues)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.merrNiveleNdermarjeDTBlerjeShitjeMeAutorizime(idnderm, llojnivel, idPerdorues);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable merrNiveleNdermarjeDTBlerjeDheShitjeMeAutorizime(int idnderm, int idPerdorues)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.merrNiveleNdermarjeDTBlerjeDheShitjeMeAutorizime(idnderm, idPerdorues);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable merrNiveleNdermarjeDTBlerjeShitjeMeAutorizimePerAutocompleteCmimesh(int idnderm, int llojnivel, int idPerdorues)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.merrNiveleNdermarjeDTBlerjeShitjeMeAutorizimePerAutocompleteCmimesh(idnderm, llojnivel, idPerdorues);
            dbartikuj.Dispose();
            return tabela;
        }

        /// <summary>
        /// mbush te gjitha nivelet e cmimeve sipas ndermarrjes
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheNiveleCmimeshSipasNdermarjes(int idnder)
        {
            clsDatabaseInventari dbNiveleCmimesh = new clsDatabaseInventari();
            bool sukses = mbushNiveleCmimesh(dbNiveleCmimesh.ktheGjitheNiveleCmimeshSipasNdermarjes(idnder));
            dbNiveleCmimesh.Dispose();
            return sukses;
        }
        public bool mbushGjitheNiveleCmimeshSipasNdermarjesDheLlojit(int idnder,int lloji,int idPerdoruesi)
        {
            clsDatabaseInventari dbNiveleCmimesh = new clsDatabaseInventari();
            bool sukses = mbushNiveleCmimesh(dbNiveleCmimesh.merrNiveleNdermarjeDTBlerjeShitje(idnder, lloji, idPerdoruesi));
            dbNiveleCmimesh.Dispose();
            return sukses;
        }
        /// <summary>
        /// kthen nivelet e cmimeve sipas filtrit
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="lloji"></param>
        /// <param name="idnderm"></param>
        /// <param name="idperdorues"></param>
        /// <returns></returns>
        public static DataTable ktheNiveleCmimeshMeFilter(string filter, long startIndex, long endIndex, int idnderm, int lloji)
        {
            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            DataTable tabela = dbInv.ktheNiveleCmimeshMeFilter(filter, startIndex, endIndex, idnderm, lloji);
            dbInv.Dispose();
            return tabela;
        }

        /// <summary>
        /// mbush te gjitha nivelet e cmimeve prind sipas ndermarrjes
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheNiveleCmimeshPrindiSipasNdermarjes(int idnder)
        {
            clsDatabaseInventari dbNiveleCmimesh = new clsDatabaseInventari();
            bool sukses = mbushNiveleCmimesh(dbNiveleCmimesh.ktheGjitheNiveleCmimeshPrindiSipasNdermarjes(idnder));
            dbNiveleCmimesh.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush te gjitha nivelet e cmimeve prind dhe monedhat sipas ndermarrjes
        /// </summary>
        /// <param name="idnder"></param>
        /// <returns></returns>
        public bool mbushGjitheNiveleCmimeshPrindiMeMonedheSipasNdermarjes(int idnder)
        {
            clsDatabaseInventari dbNiveleCmimesh = new clsDatabaseInventari();
            bool sukses = mbushNiveleCmimesh(dbNiveleCmimesh.ktheGjitheNiveleCmimeshMeMonedhePrindiSipasNdermarjes(idnder));
            dbNiveleCmimesh.Dispose();
            return sukses;   
        }

        /// <summary>
        /// mbush nivelin e cmimit sipas prindit
        /// </summary>
        /// <param name="idprindi">id e prindit</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushNivelSipasPrindit(int idprindi)
        {
            clsDatabaseInventari dbNiveleCmimesh = new clsDatabaseInventari();
            bool sukses = mbushNivelSipasPrindit(idprindi, dbNiveleCmimesh);            
            dbNiveleCmimesh.Dispose();
            return sukses;
        }

        public bool mbushNivelSipasPrindit(int idprindi, clsDatabaseInventari dbNiveleCmimesh)
        {
            bool sukses = mbushNiveleCmimesh(dbNiveleCmimesh.ktheNivelSipasPrindit(idprindi));
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsNivelCmimi"/> 
        /// </summary>
        private bool mbushNiveleCmimesh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsNivelCmimi nivelCmimi = new clsNivelCmimi(rreshti);
                    //nivelCmimi.mbushNivelCmimi(rreshti);
                    this.Add(new clsNivelCmimi(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }
        #endregion
        [Obsolete("Perdor: bool mbushNiveleCmimesh(DataTable dt)", true)]
        public colNiveleCmimesh mbushArrayListNiveleCmimesh(DataSet ds)
        {
            colNiveleCmimesh niveleCmimesh = new colNiveleCmimesh();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsNivelCmimi nivelCmimi = new clsNivelCmimi();

                nivelCmimi.IdNivelCmimi  = int.Parse(rreshti[0].ToString());
                nivelCmimi.KodNivelCmimi  = rreshti[1].ToString();
                nivelCmimi.PershkrimNivelCmimi  = rreshti[2].ToString();
                nivelCmimi.IdPrindi =int.Parse(rreshti[3].ToString());
                nivelCmimi.LlojiNivelCmimi =int.Parse(rreshti[4].ToString());
                nivelCmimi.IdMonedha =int.Parse(rreshti[5].ToString());
                nivelCmimi.BrutoNetoNivelCmimi =int.Parse(rreshti[6].ToString());
                nivelCmimi.PrioritetiNivelCmimi =int.Parse(rreshti[7].ToString());
                nivelCmimi.IdPerdoruesi =int.Parse(rreshti[8].ToString());
                //nivelCmimi.IdNderViti =int.Parse(rreshti[9].ToString());
                nivelCmimi.IdNdermarje = int.Parse(rreshti[9].ToString());

                niveleCmimesh.Add(nivelCmimi);
            }
            return niveleCmimesh;
        }
    }
}