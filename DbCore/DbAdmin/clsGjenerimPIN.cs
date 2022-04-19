using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAdmin
{
    public class clsGjenerimPIN
    {
        #region Atributet

        private int idPIN;
        private string kodiPIN;
        private DateTime dataAktivizimit;
        private DateTime dataPerfundimit;
        private int idPerdoruesi;
        private int idNdermarrja;
        private bool perdorur;
        private DateTime dataPerdorimit;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsGjenerimPIN()
        {

        }

        /// <summary>
        /// Konstruktor per gjenerimin automatik te pinit
        /// </summary>
        /// <param name="idPerdorues">(int) Id e perdoruesit per te cilen gjenerohet PIN-i</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen eshte gjeneruar PIN-i</param>
        public clsGjenerimPIN(int idPerdorues, int idNdermarrje)
        {
            const string karakteret = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            Random pin = new Random();
            kodiPIN = new string(Enumerable.Repeat(karakteret, 5).Select(s => s[pin.Next(s.Length)]).ToArray());
            dataAktivizimit = DateTime.Now;
            dataPerfundimit = dataAktivizimit.AddMinutes(3);
            this.idPerdoruesi = idPerdorues;
            this.idNdermarrja = idNdermarrje;
            perdorur = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdPIN
        {
            get
            {
                return idPIN;
            }
            set
            {
                idPIN = value;
            }
        }

        /// <summary>
        /// Kthen kodin e PIN te gjeneruar.
        /// </summary>
        public string KodiPIN
        {
            get
            {
                return kodiPIN;
            }
            set
            {
                kodiPIN = value;
            }
        }

        /// <summary>
        /// Data kur pini eshte bere aktiv
        /// </summary>
        public DateTime DataAktivizimit
        {
            get
            {
                return dataAktivizimit;
            }

        }

        /// <summary>
        /// Data deri kur pini mund te perdoret
        /// </summary>
        public DateTime DataPerfundimit
        {
            get
            {
                return dataPerfundimit;
            }

        }

        /// <summary>
        /// ID e perdoruesit per te cilen eshte gjeneruar PIN-i
        /// </summary>
        public int IdPerdoruesi
        {
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
        /// ID e ndermarrjes ku eshte gjeneruar PIN-i.
        /// </summary>
        public int IdNdermarrja
        {
            get
            {
                return idNdermarrja;
            }
            set
            {
                idNdermarrja = value;
            }
        }

        /// <summary>
        /// Tregon nese eshte perdorur apo jo PIN-i
        /// </summary>
        public bool Perdorur
        {
            get
            {
                return perdorur;
            }
            set
            {
                perdorur = value;
            }
        }

        /// <summary>
        /// Data kur eshte perdorur PIN
        /// </summary>
        public DateTime DataPerdorimi
        {
            get
            {
                return dataPerdorimit;
            }
            set
            {
                dataPerdorimit = value;
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e PIN-it per tu autentifikuar
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin  data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajPINGjeneruar(out idPIN, kodiPIN, dataAktivizimit, dataPerfundimit, idPerdoruesi, idNdermarrja, perdorur, dataPerdorimit);
            return u_ruajt;
        }

        /// <summary>
        /// modifikon perdorimin e PIN-it
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.modifikoPINGjeneruar(idPIN, perdorur, dataPerdorimit);
            return u_ruajt;
        }

        /// <summary>
        /// Funksion per kontrollin e vlefshmerise se PIN-it
        /// </summary>
        /// <param name="kodiPIN">(string) Kodi i pinit te gjeneruar</param>
        /// <param name="idPerdoruesi">(int) Id e perdorur</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes</param>
        /// <param name="dataPerdorur">(DateTime) Nese eshte perdorur PIN-i data e perdorimit te tij</param>
        /// <returns>(bool) Kontrollon nese PIN-i eshte i vlefshem apo jo. Kthen true nese eshte i vlefshem dhe anasjelltas</returns>
        public static bool eshteIVlefshemPIN(string kodiPIN, int idPerdoruesi, int idNdermarrja, DateTime dataPerdorur)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            return data.kontrolloVlefshmeriPin(kodiPIN, idPerdoruesi, idNdermarrja, dataPerdorur);
        }

        #endregion
    }
}
