using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbShare
{
    public class clsNjoftimePerdorues : IDataBase
    {
        #region Atribute
        private int idnjofrimperdorues;
        private int njoftimid;
        private int idperdoruesi;
        #endregion

        #region Properties
        public int IdNjofrimPerdorues
        {
            get { return idnjofrimperdorues; }
            set { idnjofrimperdorues = value; }
        }
        public int Njoftimeid
        {
            get { return njoftimid; }
            set { njoftimid = value; }
        }
        public int IdPerdoruesi
        {
            get { return idperdoruesi; }
            set { idperdoruesi = value; }
        }
        #endregion

        #region Konstruktoret

        public clsNjoftimePerdorues()
        {

        }

        public clsNjoftimePerdorues(IDataRecord record)
        {
            Mbush(record);
        }


        #endregion

        #region MetodatPublike

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            idnjofrimperdorues = Convert.ToInt32(record["NJOFTIMEID"].ToString());
            njoftimid = Convert.ToInt32(record["NJOFTIMEID"].ToString());
            idperdoruesi = Convert.ToInt32(record["IDPERDORUES"].ToString());
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Ruaj()
        {
            try
            {
                using (clsDatabaseShare data = new clsDatabaseShare())
                {
                    idnjofrimperdorues = 0;
                    clsMesazh u_ruajt = data.ruajNjoftimPerdorues(out idnjofrimperdorues, njoftimid, idperdoruesi);
                    return u_ruajt;
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new MesazhGabimi($"Njofrimi me id {njoftimid} dhe me id perdoruesi {idperdoruesi} nuk u ruajt!");
            }
        }
        #endregion
    }
}
