using DbCore.IMBUtils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKlientFurnitor
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKlienteFurnitore : List<clsKlientFurnitor>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsKlientFurnitor"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKlientFurnitor this[int index] => base[index];

        public static DataTable ktheKoordinatatGjitheKlientFurnitoreDheShitjeBlerje(int idnderm, int idPerd, bool llojikf)
        {
            using (var dbRegj = new DbRegjistrim.clsDatabaseRegjistrim())
                return dbRegj.ktheKoordinatatGjitheKlientFurnitoreDheShitjeBlerje(idnderm, idPerd, llojikf);
        }

        public static DataTable ktheKoordinatatEKlienteve(int idNdermarrje, int idPerdorues)
        {
            using (var dbRegj = new DbRegjistrim.clsDatabaseRegjistrim())
                return dbRegj.ktheKoordinatatEKlienteve(idNdermarrje, idPerdorues);
        }

        public static DataTable ktheKoordinatatGjitheMarzhiShitje(int idnderm, int idPerd)
        {
            using (var dbRegj = new DbRegjistrim.clsDatabaseRegjistrim())
                return dbRegj.ktheKoordinatatGjitheMarzhiShitje(idnderm, idPerd);
        }

        public static DataRow merrSipasKFNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idkf)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheKFNdermarrjesAndAutorizimeDR(idnderm, idperdorues, idkf);
        }

        public static DataTable merrSipasKFNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheKFNdermarrjesAndAutorizimeDT(idnderm, idperdorues);
        }

        public static DataTable merrSipasKFNdermarrjesAndAutorizimeDTFiltered(int idnderm, int idperdorues, int idKonfig, string filter, string topRows, string sortOrder)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheKFNdermarrjesAndAutorizimeDTFiltered(idnderm, idperdorues, idKonfig, filter, topRows, sortOrder);
        }

        public static DataTable ktheKFNdermarrjesAndAutorizimeDTPerCrm(int idnderm, int idperdorues)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheKFNdermarrjesAndAutorizimeDTPerCrm(idnderm, idperdorues);
        }

        public static DataTable merrSipasKFNdermarrjesAndAutorizimeDTExport(int idnderm, int idperdorues, int lloji, string emerTabKoka, string emerFusheId)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheKFNdermarrjesAndAutorizimeDTExport(idnderm, idperdorues, lloji, emerTabKoka, emerFusheId);
        }

        public static colKlienteFurnitore MerrKlientFurnitorSipasIdKarte(int idKarta)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
            {
                var colKlientFurnitor = new colKlienteFurnitore();
                colKlientFurnitor.MbushKlientFurnitoresh(dbKlientFurnitore.ktheGjitheKlienteFurnitoreSipasIdKarte(idKarta));
                return colKlientFurnitor;
            }
        }

        /// <summary>
        /// meProspekt percakton nese do perfshihen klientet prospekt apo jo ne liste,default ka vleren false
        /// </summary>
        /// <param name="idnderm"></param>
        /// <param name="idperdorues"></param>
        /// <param name="meProspekt"></param>
        /// <returns></returns>
        public static DataTable merrSipasKFNdermarrjesAndAutorizimeDTLupe(int idnderm, int idperdorues, bool meProspekt, int idKonfig, string filter, string topRows)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheKFNdermarrjesAndAutorizimeDTLupe(idnderm, idperdorues, meProspekt, idKonfig, filter, topRows);
        }

        /// <summary>
        /// mbush klientet ose furnitoret
        /// </summary>
        /// <param name="lloji">lloji klient ose furnitore</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public static DataTable mbushKlienteOseFurnitore(bool lloji, int idnderm, int idperdorues, bool meProspekt = false, int kfkryesor = 0)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheKlienteOseFurnitore(lloji, idnderm, idperdorues, meProspekt, kfkryesor);
        }

        public static DataTable MbushKlienteOseFurnitore(string filter, long startIndex, long endIndex, bool lloji, int idnderm, int idperdorues, int kfkryesor = 0)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheKlienteOseFurnitore(filter, startIndex, endIndex, lloji, idnderm, idperdorues, kfkryesor);
        }

        public static DataTable MbushKlienteOseFurnitore(string filter, long startIndex, long endIndex, int idnderm, int idperdorues, int kfkryesor = 0)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheKlienteOseFurnitore(filter, startIndex, endIndex, idnderm, idperdorues, kfkryesor);
        }

        /// <summary>
        /// mbush klinet furnitoret sipas ndermarrjes
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public void mbushKlienteFurnitoreNdermarrjes(int idnderm)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                MbushKlientFurnitoresh(dbKlientFurnitore.ktheKlienteFurnitoreNdermarrjes(idnderm));
        }
        public void mbushKlienteFurnitoreNdermarrjesLinear(int idnderm)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                MbushKlientFurnitoreshLinear(dbKlientFurnitore.ktheKlienteFurnitoreNdermarrjes(idnderm));
        }

        public void MbushKlienteFurnitoreNdermarrjesAndAutorizimeMonHuajMeGjendje(int idnderm, int idperdorues, int idmonedha, DateTime dtDok)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                MbushKlientFurnitoreshAzhornimGjendje(dbKlientFurnitore.ktheKlienteFurnitoreNdermarrjesAndAutorizimeMonHuajMeGjendje(idnderm, idperdorues, idmonedha, dtDok));
        }

        public void MbushKlienteFurnitoreNdermarrjesAndAutorizimeGjendjeMonLlogDheMonBaze(int idnderm, int idperdorues, DateTime dtdok)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                MbushKlientFurnitoreshAzhornimGjendje(dbKlientFurnitore.ktheKlienteFurnitoreAktivNdermarrjesAndAutorizimeGjendjeMonLlogDheMonBaze(idnderm, idperdorues, dtdok));
        }

        public static DataTable mbushKlienteFurnitoreNdermarrjesAndAutorizimeLikeNew(int idNdermarje, int idperdorues, string kodKF, int tipKlientFurnitor)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheKlienteFurnitoreNdermarrjesAndAutorizimeLike(idNdermarje, idperdorues, kodKF, tipKlientFurnitor);
        }

        public static colKlienteFurnitore MbushKlienteFurnitoreSipasAutorizimeveLike(int idNdermarrje, int idPerdorues, string kodiKlientFurnitor, int tipKlientFurnitor,int idKlientFurnitorKryesor)
        {
            var klientet = new colKlienteFurnitore();
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                klientet.MbushKlientFurnitoresh(dbKlientFurnitore.KtheKlienteFurnitoreSipasAutorizimeveLike(idNdermarrje, idPerdorues, kodiKlientFurnitor, tipKlientFurnitor, idKlientFurnitorKryesor));

            return klientet;
        }

        /// <summary>
        /// mbush klientet qe kane ditelindjen sipas ndermarrjes dhe dates
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <param name="data">data e kerkuar</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public static colKlienteFurnitore mbushKlienteQeKaneDitelindjenSipasNdermarrjesDheDates(int idnderm, DateTime data)
        {
            var klientet = new colKlienteFurnitore();
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                klientet.MbushKlientFurnitoresh(dbKlientFurnitore.ktheKlientetQeKaneDitelindjenSipasNdermarrjesDheDates(idnderm, data));
            return klientet;
        }

        public static colKlienteFurnitore MerrKlientFurnitoreVartesSipasIdShitjeKoka(int idShitjeKoka)
        {
            var klientet = new colKlienteFurnitore();
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                klientet.MbushKlientFurnitoresh(dbKlientFurnitore.KtheKlientFurnitoreVartesSipasIdShitjeKoka(idShitjeKoka));
            return klientet;
        }

        public static colKlienteFurnitore MerrKlienteFurnitoreVartesSipasKodeve(string kodetKlienteFurnitoreVartes, int idNdermarrja, clsDatabaseKontabilitet db)
        {
            var klientet = new colKlienteFurnitore();
            klientet.MbushKlientFurnitoresh(db.MerrKlienteFurnitoreVartesSipasKodeve(kodetKlienteFurnitoreVartes, idNdermarrja));
            return klientet;
        }

        public static colKlienteFurnitore MerrKlienteFurnitoreVartesIdve(string ids)
        {
            var klientet = new colKlienteFurnitore();
            using (var db = new clsDatabaseKontabilitet())
                klientet.MbushKlientFurnitoresh(db.MerrKlienteFurnitoreVartesIdve(ids));
            return klientet;
        }
        public static colKlienteFurnitore KontrolloEkzistenceKlientFurnitoreVartes(string klientFurnitorVartes, int idNder)
        {
            using (var db = new clsDatabaseKontabilitet())
                return KontrolloEkzistenceKlientFurnitoreVartes(klientFurnitorVartes, idNder, db);
        }
        public static colKlienteFurnitore KontrolloEkzistenceKlientFurnitoreVartes(string klientFurnitorVartes, int idNder, clsDatabaseKontabilitet db)
        {
            var kodeKlienteFurnitoreVartes = klientFurnitorVartes.Split(',').ToList();
            kodeKlienteFurnitoreVartes.ForEach(x => x.RemoveSpaces());
            var klientetQeEkzistojne = colKlienteFurnitore.MerrKlienteFurnitoreVartesSipasKodeve(klientFurnitorVartes, idNder, db);
            if (kodeKlienteFurnitoreVartes.Count != klientetQeEkzistojne.Count)
            {
                var klientetQeNukEkzistojne = string.Join(", ", kodeKlienteFurnitoreVartes.Except(klientetQeEkzistojne.Select(x => x.KodKlientFurnitor)));
                throw new MyException(IMBUtils.Messages.MessagesResource.Messages["msgKlientiVartesNukExiston"].Replace("#XX", klientetQeNukEkzistojne));
            }
            return klientetQeEkzistojne;
        }


        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsKlientFurnitor"/> 
        /// </summary>
        private void MbushKlientFurnitoresh(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsKlientFurnitor(rreshti));
            }
        }
        private void MbushKlientFurnitoreshLinear(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsKlientFurnitor(rreshti,true));
            }
        }

        private void MbushKlientFurnitoreshAzhornimGjendje(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                var klientfurnitor = new clsKlientFurnitor();
                klientfurnitor.MbushKlientFurnitorAzhornimGjendje(rreshti);
                Add(klientfurnitor);
            }
        }

        #endregion
    }
}
