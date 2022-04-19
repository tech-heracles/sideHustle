using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbOTC
{
    public abstract class OTCLogData
    {
        protected int idPerdoruesiAlphaWeb;
        protected int idPerdoruesiMPESA;
        protected int idNdermarrje;
        protected DateTime dtKrijimi;
        protected DateTime dtModifikimi;
        protected string mesazhTransaksioni;
        protected StatusOTC _statusTransaksioni;
        public string MesazhTransaksioni
        {
            get { return mesazhTransaksioni; }
            set { mesazhTransaksioni = value; }
        }

        public int IdPerdoruesiAlphaWeb
        {
            get { return idPerdoruesiAlphaWeb; }
            set { idPerdoruesiAlphaWeb = value; }
        }

        public int IdPerdoruesiMpesa
        {
            get { return idPerdoruesiMPESA; }
            set { idPerdoruesiMPESA = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        public StatusOTC StatusTransaksioni
        {
            get { return _statusTransaksioni; }
            set { _statusTransaksioni = value; }
        }
        public abstract clsMesazh Ruaj();
      
    }
}
