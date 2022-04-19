using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore
{
    public class clsShtoUserVodafone
    {

        #region Atribute

        private String emriPerdorues;
        private String mbiemriPerdorues;
        private String perdoruesUsername;
        private String perdoruesPassword;
        private String qyteti;
        private String perdoruesAdresa;
        private String perdoruesEmail;
        private String perdoruesFax;
        private String perdoruesTelefon;
        private String gjuha;
        private String roliPerdoruesit;
        private int status;
        private String dyqani;
        private String[] administrator;

        #endregion

        #region Properties

        public String EmriPerdorues
        {
            get { return emriPerdorues; }
            set { emriPerdorues = value; }
        }

        public String MbiemriPerdorues
        {
            get { return mbiemriPerdorues; }
            set { mbiemriPerdorues = value; }
        }

        public String PerdoruesUsername
        {
            get { return perdoruesUsername; }
            set { perdoruesUsername = value; }
        }

        public String PerdoruesPassword
        {
            get { return perdoruesPassword; }
            set { perdoruesPassword = value; }
        }

        public String Qyteti
        {
            get { return qyteti; }
            set { qyteti = value; }
        }

        public String PerdoruesAdresa
        {
            get { return perdoruesAdresa; }
            set { perdoruesAdresa = value; }
        }

        public String PerdoruesEmail
        {
            get { return perdoruesEmail; }
            set { perdoruesEmail = value; }
        }

        public String PerdoruesFax
        {
            get { return perdoruesFax; }
            set { perdoruesFax = value; }
        }

        public String PerdoruesTelefon
        {
            get { return perdoruesTelefon; }
            set { perdoruesTelefon = value; }
        }

        public String Gjuha
        {
            get { return gjuha; }
            set { gjuha = value; }
        }

        public String RoliPerdoruesit
        {
            get { return roliPerdoruesit; }
            set { roliPerdoruesit = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public String Dyqani
        {
            get { return dyqani; }
            set { dyqani = value; }
        }

        public String[] Administrator
        {
            get { return administrator; }
            set { administrator = value; }
        }

        #endregion

        #region Konstruktori

        public clsShtoUserVodafone()
        {
        }

        #endregion
    }
}
