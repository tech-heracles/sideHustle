using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje grup kontabilizimi
    ///  (Te dhenat  merren nga tabela : T_GRUPKONTABILIZIMI)
    ///</remarks>
    public class clsGrupKontabilizimi
    {
        #region Atribute

        private int idGrupKontabilizimi;
        private string nrGrupKontabilizimi;
        private string pershkrimGrupKontabilizimi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idgrupkontablizimi">Id e grupit te kontabilizimit</param>
        /// <param name="nrgrupkontabilizimi">Numri i grupit te kontabilizimit</param>
        /// <param name="pershkrimgrupkontabilizimi">Pershkrimi i grupit te kontabilizimit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <param name="idnderm">Id e ndermarrjes</param>
        public clsGrupKontabilizimi(int idgrupkontablizimi, string nrgrupkontabilizimi, string pershkrimgrupkontabilizimi, int idperdoruesi,int idnderm)
        {
            idGrupKontabilizimi = idgrupkontablizimi;
            nrGrupKontabilizimi = nrgrupkontabilizimi;
            pershkrimGrupKontabilizimi = pershkrimgrupkontabilizimi;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idnderm;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e grup kontabilizimi</param>
        public clsGrupKontabilizimi(int id)
        {
            clsDatabaseKontabilitet dbGrupKontabilizimi= new clsDatabaseKontabilitet();
            mbushGrupKontabilizime(dbGrupKontabilizimi.merrGrupKontabilizimiSipasId(id));
            dbGrupKontabilizimi.Dispose();
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsGrupKontabilizimi()
        { 
        }

        public clsGrupKontabilizimi(DataRow rreshti)
        {
            
            mbushGrupKontabilizime(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdGrupKontabilizimi
        {
            get { return idGrupKontabilizimi ; }
            set { idGrupKontabilizimi  = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e grupit te kontabilizimit
        /// </summary>
        public string NrGrupKontabilizimi
        {
            get { return nrGrupKontabilizimi; }
            set { nrGrupKontabilizimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e grupit te kontabilizimit
        /// </summary>
        public String PershkrimGrupKontabilizimi
        {
            get { return pershkrimGrupKontabilizimi; }
            set { pershkrimGrupKontabilizimi = value; }
        }

        /// <summary>
            /// Kthen/Vendos ID-ne e perdoruesit
        /// </summary>
        public int IdPerdoruesi
            {
                get { return idPerdoruesi; }
                set { idPerdoruesi = value; }
            }

        /// <summary>
            /// Kthen/Vendos ID-ne e ndermarrjes
        /// </summary>
        public int IdNdermarje
            {
                get { return idNdermarje; }
                set { idNdermarje = value; }
            }
        public int IdStatusDok
            {
            get { return idStatusDok; }
            set { idStatusDok = value; }
            }
        public DateTime DtKrijimi
            {
            get { return dtKrijimi; }

            }
        public DateTime DtModifikimi
            {
            get { return dtModifikimi; }

            }
        #endregion

        #region Metoda publike
            
        /// <summary>
            /// Ruan objektin e grupit te kontabilizimit ne tabelen perkatese ne databaze.Therret funksionin
            /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajGrupKontabilizimi"/>
            /// </summary>
            /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            int id;
            clsMesazh u_ruajt = data.ruajGrupKontabilizimi(out id, this.NrGrupKontabilizimi, this.PershkrimGrupKontabilizimi, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e grupit te kontabilizimit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoGrupKontabilizimi"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            clsMesazh u_modifikua = data.modifikoGrupKontabilizimi(this.IdGrupKontabilizimi, this.NrGrupKontabilizimi, this.PershkrimGrupKontabilizimi, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e grupit te kontabilizimit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiGrupKontabilizimi"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshi()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            clsMesazh u_fshi = data.fshiGrupKontabilizimiStatus(this.IdGrupKontabilizimi, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr nje objekt grup llogarie. Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrGrupKontabilizimi"/>
        /// </summary>
        public void merr()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            data.merrGrupKontabilizimi(this.IdGrupKontabilizimi);
            data.Dispose();
        }

        /// <summary>
        /// Merr nje collection me objekte grup kontabilizimi.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheGjitheGrupetKontabilizimi"/>
        /// </summary>
        public colGrupeKontabilizimi merriTeGjithe(int idndermarje)
        {
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrGjitheGrupetKontabilizimi(idndermarje);
            colGrupeKontabilizimi colgrupeKont = new colGrupeKontabilizimi(idndermarje);
            return colgrupeKont;

        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen id e grup kontabilizimit sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kodi">kodi i grupit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>id e grup kontabilizimit</returns>
        public static int mbushIDGrupKontabilizim(string kodi, int idnderm)
        {
            clsDatabaseKontabilitet dbGrupKontabilizim = new clsDatabaseKontabilitet();
            int id = (dbGrupKontabilizim.ktheGrupKontabilizimiSipasKodit(kodi, idnderm));
            dbGrupKontabilizim.Dispose();
            return id;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush grup kontabilizime nga databaza
        /// </summary>
        /// <param name="dbDataRowLlogari">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushGrupKontabilizime(DataRow dbDataRowGrupKontabilizime)
        {
            if (dbDataRowGrupKontabilizime != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupKontabilizime["IDGRUPKONTABILIZIMI"].ToString(), out idGrupKontabilizimi);
                    nrGrupKontabilizimi = dbDataRowGrupKontabilizime["NRGRUPKONTABILIZIMI"].ToString();
                    pershkrimGrupKontabilizimi = dbDataRowGrupKontabilizime["PERSHKRIMGRUPKONTABILIZIMI"].ToString();
                    int.TryParse(dbDataRowGrupKontabilizime["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowGrupKontabilizime["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowGrupKontabilizime["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowGrupKontabilizime["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowGrupKontabilizime["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                  
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se grupit te kontabilizimeve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
