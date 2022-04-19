using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKPF
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKPFte : List<clsKPF>
    {
        #region Konstruktoret
        public colKPFte(List<clsKPF> collection)
                    : base(collection)
        {
        }

        public colKPFte() { }
        #endregion


        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsKPF"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKPF this[int index]
        {
            get { return ((clsKPF)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje objekti clsKPF ne nje arraylist
        /// </summary>
        public bool shtoKPF(clsKPF KPF)
        {
            base.Add(KPF);
            if (base.Contains(KPF))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per fshirjen e nje objekti clsKPF ne nje arraylist
        /// </summary>
        public bool fshiKPF(clsKPF KPF)
        {
            base.Remove(KPF);
            if (base.Contains(KPF))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per fshirjen e gjithe objekteve clsKPF ne nje arraylist
        /// </summary>
        public bool fshiGjitheKPFte()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per fshirjen e nje objekti clsKPF ne nje indeks te caktuar ne nje arraylist
        /// </summary>
        public void fshiKeteKPF(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda per shtimin e nje objekti clsKPF ne nje indeks te caktuar ne nje arraylist
        /// </summary>
        public void shtoKPFNeIndeksin(int index, clsKPF KPF)
        {
            base.Insert(index, KPF);
        }

        /// <summary>
        /// metoda kthen indeksin e nje objekti clsKPF ne nje arraylist
        /// </summary>
        public int indeksiKPFse(clsKPF KPF)
        {
            return base.IndexOf(KPF);
        }

        /// <summary>
        /// metoda kontrollon ekzistencen e nje objekti clsKPF ne nje arraylist
        /// </summary>
        public bool ekzistonKPF(clsKPF KPF)
        {
            if (base.Contains(KPF))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda kthn numrin e objekteve clsKPF ne nje arraylist
        /// </summary>
        public int numriKPF()
        {
            return base.Count;
        }

        public static DataRow merrSipasKPFNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idArtikull)
        {
            clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            DataRow rreshti = dbartikuj.ktheKPFNdermarrjesAndAutorizimeDR(idnderm, idperdorues, idArtikull);
            dbartikuj.Dispose();
            return rreshti;
        }
        public static DataTable merrSipasKPFNdermarrjesAndAutorizimeDTGrupit(int idgrupi, int idnderm, int idperdorues)
        {
            clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            DataTable tabela = dbartikuj.ktheKPFNdermarrjesAndAutorizimeDTGrupit(idnderm, idperdorues, idgrupi);
            dbartikuj.Dispose();
            return tabela;
        }

        public static DataTable merrSipasKPFNdermarrjesAndAutorizimePerLupeKPF(int idgrupi, int idnderm, int idperdorues)
        {
            clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            DataTable tabela = dbartikuj.ktheKPFNdermarrjesAndAutorizimePerLupeKPF(idnderm, idperdorues, idgrupi);
            dbartikuj.Dispose();
            return tabela;
        }
        /// <summary>
        /// mbush gjithe kpf-te sipas grupit
        /// </summary>
        /// <param name="grupiKpf">grupi i kpf-se</param>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKPFteSipasGrupit(int grupiKpf, int idndermarje)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            bool sukses = mbushKPFte(dbKPF.ktheGjitheKPFteSipasGrupit(grupiKpf, idndermarje));
            dbKPF.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush gjithe kpf-te pozitive sipas grupit
        /// </summary>
        /// <param name="grupiKpf">grupi i kpf-se</param>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKPFteSipasGrupitPozitive(int grupiKpf, int idNdermarje)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            bool sukses = mbushKPFte(dbKPF.ktheGjitheKPFteSipasGrupitPozitive(grupiKpf, idNdermarje));
            dbKPF.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush gjithe kpf-te positive sipas grupit dhe autorizimit
        /// </summary>
        /// <param name="grupiKpf">grupi i kpf-se</param>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKPFteSipasGrupitPozitiveAndAutorizime(int grupiKpf, int idNdermarje, int idperdoruesi)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            bool sukses = mbushKPFte(dbKPF.ktheGjitheKPFteSipasGrupitPozitiveAndAutorizime(grupiKpf, idNdermarje, idperdoruesi));
            dbKPF.Dispose();
            return sukses;
        }
        public bool ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeNiveli1Dhe2(int grupiKpf, int idNdermarje, int idperdoruesi)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            bool sukses = mbushKPFte(dbKPF.ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeNiveli1Dhe2(grupiKpf, idNdermarje, idperdoruesi));
            dbKPF.Dispose();
            return sukses;
        }
        
        /// <summary>
        /// mbush gjithe kpf-te pozitive sipas grupit dhe autorizimeve aktive
        /// </summary>
        /// <param name="grupiKpf">grupi i kpf-se</param>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(int grupiKpf, int idNdermarje, int idperdoruesi)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            bool sukses = mbushKPFte(dbKPF.ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(grupiKpf, idNdermarje, idperdoruesi));
            dbKPF.Dispose();
            return sukses;
        }
        public bool mbushGjitheKPFteSipasGrupitAndAutorizimeLike(int grupiKpf, int idNdermarje, int idperdoruesi, string kodi)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            bool sukses = mbushKPFte(dbKPF.ktheGjitheKPFteSipasGrupitAndAutorizimeLike(grupiKpf, idNdermarje, idperdoruesi, kodi));
            dbKPF.Dispose();
            return sukses;
        }

        public static DataTable mbushGjitheKPFteSipasGrupitAndAutorizimeLikeNew(int grupiKpf, int idNdermarje, int idperdoruesi, string kodi)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            DataTable tabela = dbKPF.ktheGjitheKPFteSipasGrupitAndAutorizimeLike(grupiKpf, idNdermarje, idperdoruesi, kodi);
            dbKPF.Dispose();
            return tabela;
        }
        public static DataTable mbushGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktiveBij(int grupiKpf, int idNdermarje, int idperdoruesi)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            DataTable tabela = (dbKPF.ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktiveBij(grupiKpf, idNdermarje, idperdoruesi));
            dbKPF.Dispose();
            return tabela;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsKPF"/> 
        /// </summary>
        private bool mbushKPFte(DataTable dt)
        {
            //try
            //{

            foreach (DataRow rreshti in dt.Rows)
            {
                //clsKPF KPF = new clsKPF();
                //KPF.mbushKPF(rreshti);
                Add(new clsKPF(rreshti));
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


