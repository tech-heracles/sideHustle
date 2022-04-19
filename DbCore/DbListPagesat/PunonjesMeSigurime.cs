using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbListPagesat
{
    public class PunonjesMeSigurime
    {
        private int _idPunonjesi;
        private int _idSigurime;
        private DateTime _dtAktivizimi;
        public PunonjesMeSigurime() { }
        public PunonjesMeSigurime(int idPunonjesi, int idSigurimi, DateTime dtAktivizimi)
        {
            _idPunonjesi = idPunonjesi;
            _idSigurime = idSigurimi;
            _dtAktivizimi = dtAktivizimi;
        }
        public PunonjesMeSigurime(IDataRecord record)
        {

            int.TryParse(record["idPunonjesi"].ToString(), out _idPunonjesi);
            int.TryParse(record["idSigurime"].ToString(), out _idSigurime);
            DateTime.TryParse(record["dtAktivizimi"].ToString(), out _dtAktivizimi);
        }


        public int IdSigurime
        {
            get
            {
                return _idSigurime;
            }

            set
            {
                _idSigurime = value;
            }
        }

        public int IdPunonjesi
        {
            get
            {
                return _idPunonjesi;
            }

            set
            {
                _idPunonjesi = value;
            }
        }

        public DateTime DtAktivizimi
        {
            get
            {
                return _dtAktivizimi;
            }
            set
            {
                _dtAktivizimi = value;
            }
        }
        public static PunonjesMeSigurime Krijo(IDataRecord record)
        {
            return new PunonjesMeSigurime(record);
        }

        internal static PunonjesMeSigurime MerrSigurimeSipasDatesMeTeAfert(int idNdermarrje, DateTime dtAktivizimiSkema)
        {
            clsSigurimet sigDefault = new clsSigurimet();
            sigDefault.ktheSigurimeSipasDatesMeTeAfert(idNdermarrje, dtAktivizimiSkema);
            return new PunonjesMeSigurime(0, sigDefault.IdSigurime, dtAktivizimiSkema);
        }
    }
}
