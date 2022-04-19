using AlphaWeb.Core.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbInventari
{
    public class clsOperator : IDataBase
    {
        #region Properties

        public int IdOperator { get; set; }

        public string Kodi { get; set; }

        public string Emri { get; set; }

        public string Mbiemri { get; set; }

        public bool Aktiv { get; set; }

        public int IdStatusDok { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdPerdorues { get; set; }

        public DateTime DtKrijimi { get; set; }

        public DateTime DtModifikimi { get; set; }

        #endregion

        #region Konstruktoret

        public clsOperator() { }

        public clsOperator(int id, string kodi, string emri, string mbiemri, bool aktiv, int idStatusDok, int idNdermarrje, int idPerdorues, bool shtim)
        {
            try
            {
                IdOperator = id;
                Kodi = kodi;
                Emri = emri;
                Mbiemri = mbiemri;
                Aktiv = aktiv;
                IdStatusDok = idStatusDok;
                IdNdermarrje = idNdermarrje;
                IdPerdorues = idPerdorues;
                clsMesazh mesazh = this.kontrolloOperator(shtim);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        #endregion

        #region Metoda Publike

        public clsMesazh Ruaj()
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                clsMesazh u_ruajt = db.RuajOperator(out int idOperator, Kodi, Emri, Mbiemri, Aktiv, IdStatusDok, IdNdermarrje, IdPerdorues);
                IdOperator = idOperator;
                return u_ruajt;
            }
        }
        private clsMesazh kontrolloOperator(bool shtim)
        {
            if (Kodi == "")
                return new clsMesazh(false, "Plotesoni emertimin e transportuesit!");
            clsDatabaseInventari db = new clsDatabaseInventari();
            if (shtim && db.ekzistonOperatori(Kodi, IdNdermarrje))
            {
                db.Dispose();
                return new clsMesazh(false, "Ekziston nje operatore me kete emertim!");
            }

            return new clsMesazh(true, "Kontrollet e operatorit u kaluan me sukses");
        }
        public clsMesazh Modifiko()
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.ModifikoOperator(IdOperator, Kodi, Emri, Mbiemri, Aktiv, IdStatusDok, IdNdermarrje, IdPerdorues);
            }
        }

        public clsMesazh Fshi()
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.FshiOperator(IdOperator);
            }
        }

        public void Mbush(IDataRecord record)
        {
            throw new NotImplementedException();
        }

        public static DataTable MerrOperatoretAktive(int idNdermarrje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.ktheGjitheOperatoret(idNdermarrje);
            }
        }
        public static DataRow MerrKodOperatoriSipasEmritDheMbiemrit(string emri, string mbiemri, int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.merrKodOperatoriSipasEmritDheMbiemrit(emri, mbiemri, idndermarje);
            }
        }
        public static DataRow MerrEmerDheMbiemerOperatoriSipasId(int id, int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.merrEmerDheMbiemerOperatoriSipasId(id, idndermarje);
            }
        }
        public static DataRow MerrIdOperatoriSipasKodOperatori(string KodOperatori, int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.merrIdOperatoriSipasKodOperatori(KodOperatori, idndermarje);
            }
        }
        public static int MerrEmerMbiemerOperatoriSipasKodOperatori(string KodOperatori, int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.merrEmerMbiemerOperatoriSipasKodOperatori(KodOperatori, idndermarje);
            }
        }
        public static DataRow MerrKodOperatoriSipasId(int id, int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.merrKodOperatoriSipasId(id, idndermarje);
            }
        }
        #endregion
    }
}
