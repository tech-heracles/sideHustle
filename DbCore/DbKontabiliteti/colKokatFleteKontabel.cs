using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaFleteKontabel
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokatFletetKontabel : System.Collections.Generic.List<clsKokaFleteKontabel>
    {
        #region Konstruktor

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colKokatFletetKontabel()
        {

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colKokatFletetKontabel(int idNdermVit)
        {
            clsDatabaseKontabilitet dbKokatFletetKontabel = new clsDatabaseKontabilitet();
            mbushKokaFleteveKontabel(dbKokatFletetKontabel.ktheGjitheFletetKontabelTePaKontabilizuara(idNdermVit));
            dbKokatFletetKontabel.Dispose();
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="iddok">id e dokumentit</param>
        /// <param name="idlloj">id e llojit</param>
        public colKokatFletetKontabel(int iddok, int idkategoria)
        {
            clsDatabaseKontabilitet dbKokaFleteKontabel = new clsDatabaseKontabilitet();
            mbushKokaFleteveKontabel(dbKokaFleteKontabel.ktheDataTableKokaFleteKontabelSipasIDGjeneruesAndIdKategoria(iddok, idkategoria));
            dbKokaFleteKontabel.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsKokaFleteKontabel"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKokaFleteKontabel this[int index]
        {
            get { return ((clsKokaFleteKontabel)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e objekti clsKokaFleteKontabel ne nje arraylist
        /// </summary>
        public bool shtoKokaFleteKontabel(clsKokaFleteKontabel kokaFleteKontabel)
        {
            base.Add(kokaFleteKontabel);
            if (base.Contains(kokaFleteKontabel))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per fshirjen e objekti clsKokaFleteKontabel ne nje arraylist
        /// </summary>
        public bool fshiKokaFleteKontabel(clsKokaFleteKontabel kokaFleteKontabel)
        {
            base.Remove(kokaFleteKontabel);
            if (base.Contains(kokaFleteKontabel))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per fshirjen e gjithe objekteve clsKokaFleteKontabel nga nje arraylist
        /// </summary>
        public bool fshiKokaFleteKontabel()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per fshirjen e nje objekti clsKokaFleteKontabel ne nje indeks te caktuar ne nje arraylist
        /// </summary>
        public void fshiKeteKokaFleteKontabel(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda per shtimin e objekti clsKokaFleteKontabel ne nje indeks te caktuar ne nje arraylist
        /// </summary>
        public void shtoKokaFleteKontabelNeIndeksin(int index, clsKokaFleteKontabel kokaFleteKontabel)
        {
            base.Insert(index, kokaFleteKontabel);
        }

        /// <summary>
        /// metoda kthen indeksin e objektit clsKokaFleteKontabel ne nje arraylist
        /// </summary>
        public int indeksiKokaFleteKontabel(clsKokaFleteKontabel kokaFleteKontabel)
        {
            return base.IndexOf(kokaFleteKontabel);
        }

        /// <summary>
        /// metoda kontrollon nese objekti clsKokaFleteKontabel ndodhet ne nje arraylist
        /// </summary>
        public bool ekzistonKokaFleteKontabel(clsKokaFleteKontabel kokaFleteKontabel)
        {
            if (base.Contains(kokaFleteKontabel))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda kthen numrin e objekteve clsKokaFleteKontabel ne nje arraylist
        /// </summary>
        public int numriKokaFleteveKontabel()
        {
            return base.Count;
        }

        /// <summary>
        /// Metoda kthen nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsKokaFleteKontabel"/> 
        /// duke filtruar sipas ID-se lidhese te ndermarrjes me vitin
        /// Thirret funksioni <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheGjitheFletetKontabelTePaKontabilizuara"/>
        /// </summary>
        public colKokatFletetKontabel merrGjitheFletetKontabelTePaKontabilizuara(int idNdermvit)
        {
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            //return db.merrGjitheFletetKontabelTePaKontabilizuara(idNdermvit);
            colKokatFletetKontabel koka = new colKokatFletetKontabel(idNdermvit);
            return koka;
        }
        public static DataTable merrFleteKontabelDT(int idndermvit, int idperdorues, string datanga, string dataderi)
        {
            clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            DataTable tabela = dbartikuj.merrFleteKontabelDT(idndermvit, idperdorues, datanga, dataderi);
            dbartikuj.Dispose();
            return tabela;
        }

        public static DataTable MerrFleteKontabelExport(int idNdermarrje, int idPerdorues, int idNdermarrjeViti, int lloji, string emerTabKoka, string emerFusheId, string idPerEksport)
        {
            using (var db = new clsDatabaseKontabilitet())
            {
                return db.MerrFleteKontabelExport(idNdermarrje, idPerdorues, idNdermarrjeViti, lloji, emerTabKoka, emerFusheId, idPerEksport);
            }
        }

        #endregion

        #region Metodat Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsKokaFleteKontabel"/> 
        /// </summary>
        private bool mbushKokaFleteveKontabel(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaFleteKontabel kokaFleteKontabel = new clsKokaFleteKontabel();
                    //kokaFleteKontabel.mbushKokaFleteKontabel(rreshti);
                    Add(new clsKokaFleteKontabel(rreshti));
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
       
    }
}

