using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.DataBase;
using DbCore.DbAdmin;
using System.Text.RegularExpressions;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    public class clsSerialeUnikeFormate : IDataBase
    {
        #region Atribute

        private int id;
        private string kod;
        private string pershkrimi;
        private int idKategoriSeriali;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private bool aktiv;
        private colSerialeUnike colSeriale;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int ID {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodin e formatit te serialit unik.
        /// </summary>
        public string Kod {
            get
            {
                return kod;
            }
            set
            {
                kod = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e formatit te serialit unik.
        /// </summary>
        public string Pershkrimi {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }
        }

        public int IdKategoriSeriali {
            get
            {
                return idKategoriSeriali;
            }
            set
            {
                idKategoriSeriali = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }

        public colSerialeUnike ColSeriale {
            get
            {
                if (colSeriale == null)
                    colSeriale = new colSerialeUnike();
                return colSeriale;
            }
            set
            {
                colSeriale = value;
            }
        }

        public int IdStatusDok {
            get
            {
                return idStatusDok;
            }

            set
            {
                idStatusDok = value;
            }
        }

        public bool Aktiv {
            get
            {
                return aktiv;
            }

            set
            {
                aktiv = value;
            }
        }

        #endregion

        #region Konstruktor

        public clsSerialeUnikeFormate()
        {
            this.colSeriale = new colSerialeUnike();
        }
        public clsSerialeUnikeFormate(int id)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                db.mbushSerialeFormateSipasID(id, this);
        }
        public clsSerialeUnikeFormate(string kod, string pershkrimi, int idKategoriSeriali, int idPerdoruesi, int idNdermarje, int idstatusdok, bool aktiv)
        {
            this.kod = kod;
            this.pershkrimi = pershkrimi;
            this.idKategoriSeriali = idKategoriSeriali;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idstatusdok;
            this.aktiv = aktiv;
            this.colSeriale = new colSerialeUnike();
        }

        public clsSerialeUnikeFormate(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }

        public clsSerialeUnikeFormate(string kod, int idNdermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                db.merrSerialeFormate(kod, idNdermarje, this);
        }

        #endregion

        #region Metoda Publike

        public clsMesazh Ruaj()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (var scope = new MyTransactionScope())
            {
                if (ekziston())
                    return new MesazhGabimi("Ekziston nje format me kete kod. Ju lutem shenoni nje tjeter!");
                mesazh = kontrolloSerialetUnike(this.ColSeriale);
                if (mesazh.Status)
                    mesazh = RuajNeDB();
                if (mesazh)
                    scope.Complete();
            }
            return mesazh;
        }

        public clsMesazh Modifiko()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (var scope = new MyTransactionScope())
            {
                mesazh = kontrolloSerialetUnike(this.ColSeriale);
                if (mesazh.Status)
                    mesazh = ModifikoNeDB();
                if (mesazh)
                    scope.Complete();
            }
            return mesazh;
        }

        public clsMesazh Fshi()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (var scope = new MyTransactionScope())
            {
                mesazh = FshiNeDB();
                if (mesazh)
                    scope.Complete();
            }
            return mesazh;
        }

        public void Mbush(IDataRecord record)
        {
            int.TryParse(record["IDFORMATSERIALESH"].ToString(), out id);
            kod = record["KODFORMATSERIALESH"].ToString();
            pershkrimi = record["PERSHKRIMFORMATSERIALESH"].ToString();
            int.TryParse(record["IDKATEGORISERIALESH"].ToString(), out idKategoriSeriali);
            int.TryParse(record["IDNDERMARRJE"].ToString(), out idNdermarje);
            int.TryParse(record["IDPERDORUES"].ToString(), out idPerdoruesi);
            int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
            bool.TryParse(record["AKTIV"].ToString(), out aktiv);
            colSeriale = new colSerialeUnike();
        }

        public void shtoSerialUnik(string kodSeriali)
        {
            List<clsSerialeUnike> colNew = colSeriale.FindAll(r => r.Kodi == kodSeriali);
            if (colNew.Count == 0)
                colSeriale.Add(new clsSerialeUnike(kodSeriali, idNdermarje));
        }

        public void hiqSerialUnik(string kodSeriali)
        {
            colSeriale.RemoveAll(x => x.Kodi == kodSeriali);
        }

        public bool ekziston()
        {
            clsSerialeUnikeFormate serialiUnikFormat = new clsSerialeUnikeFormate(kod, idNdermarje);
            return serialiUnikFormat.ID != 0 ? true : false;
        }


        public bool ValidoSerialSipasFormatit(string serialiPerKontroll, bool serialKryesor)
        {
            foreach (clsSerialeUnike seriali in this.ColSeriale.Where(x => x.SerialKryesor == serialKryesor))
                if (seriali.Validate(serialiPerKontroll) && (string.IsNullOrWhiteSpace(seriali.FormuleSpecifike) || Regex.Match(serialiPerKontroll, seriali.FormuleSpecifike).Success))
                    return true;

            return false;
        }

        #endregion

        #region Metoda Private

        private clsMesazh RuajNeDB()
        {
            clsMesazh mesazh = new MesazhGabimi("Ndodhi nje gabim gjate ruajtjes se formatit te serialeve!");
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                mesazh = db.ruajSerialeUnikeFormat(out id, kod, pershkrimi, idKategoriSeriali, idNdermarje, idPerdoruesi, aktiv);
                if (mesazh.Status)
                    mesazh = colSeriale.Ruaj(id);
            }
            return mesazh;
        }

        private clsMesazh ModifikoNeDB()
        {
            clsMesazh mesazh = new MesazhGabimi("Ndodhi nje gabim gjate modifikimit te formatit te serialeve!");
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                mesazh = db.modifikoSerialeUnikeFormat(id, pershkrimi, idKategoriSeriali, idPerdoruesi, aktiv); // Po kategoria do te modifikohet? -- EGI asks herself 
                if (mesazh.Status)
                    mesazh = colSeriale.Modifiko(id);
            }
            if (mesazh.Status)
                mesazh.PershkrimMesazhi = MessagesResource.Messages["msgModifikimiMeSukses"];
            return mesazh;
        }

        private clsMesazh FshiNeDB()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                mesazh = db.fshiSerialeUnikeFormat(id);
            return mesazh;
        }

        private clsMesazh kontrolloSerialetUnike(colSerialeUnike ColSeriale)
        {
            clsMesazh mesazh = new clsMesazh(true, "Serialet e lidhura me formatin jane ne rregull!"); 
            if (this.ColSeriale.Count == 0)
                mesazh = new clsMesazh(false, "Nuk mund te ruani format pa seriale!");
            else if (this.ColSeriale.Count > 2)
                mesazh = new clsMesazh(false, "Nje format mund te kete maksimalisht 2 seriale!");
            else
            {
                int nrSerialeKryesore = 0;
                foreach (var id in ColSeriale)
                {
                    if (id.SerialKryesor)
                        nrSerialeKryesore++;
                }
                if (nrSerialeKryesore == 0)
                    mesazh = new clsMesazh(false, "Nje format duhet te kete patjeter nje serial kryesor!");
                else if (nrSerialeKryesore > 1)
                    mesazh = new clsMesazh(false, "Nje format nuk mund te kete me shume se 1 serial kryesor!");
            }
            return mesazh;
        }

        #endregion
    }
}
