using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colVeprimeKFKoka : List<clsVeprimeKFKoka>
    {
        public colVeprimeKFKoka()
        {

        }

        public void mbushVeprimeKFKoka(int idndermviti)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            mbushVeprimeKFKoka(data.merrGjitheVeprimeKFKoka(idndermviti));
            data.Dispose();
        }

        public new clsVeprimeKFKoka this[int index]
        {
            get { return ((clsVeprimeKFKoka)base[index]); }
        }

        public colVeprimeKFKoka merrVeprimeKFKoka(int idndermviti)
        {
            if (this.Count == 0)
                mbushVeprimeKFKoka(idndermviti);
            return this;
        }

        public static DataTable merrVeprimeKFSipasPeriudhesDheFiltritDT(int idndermvit, int idperdoruesi,string dataNga, string dataDeri,string filterString,string topRows)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrVeprimeKFDT(idndermvit, idperdoruesi, dataNga, dataDeri, filterString, topRows);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrDokVeprimeKFPerEksport(int idNdermarrje, int idPerdoruesi, int idNdermarrjeVit, string idPerEksport)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                DataTable dt = db.merrDokVeprimeKFPerEksport(idNdermarrje, idPerdoruesi, idNdermarrjeVit, idPerEksport);
                return dt;
            }
        }
        
        #region metoda private

        private bool mbushVeprimeKFKoka(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {

                    //clsVeprimeKFKoka koka = new clsVeprimeKFKoka();
                    //koka.mbushVeprimeKFKoka(rreshti);
                    this.Add(new clsVeprimeKFKoka(rreshti));
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

        #region metoda publike

        public bool merrVeprimeKFKokaSipasNrDok(string nrdok, int idNdermarrje)
        {
            clsDatabaseRegjistrim Db = new clsDatabaseRegjistrim();
            bool mbush = mbushVeprimeKFKoka(Db.merrVeprimeKFKokaSipasNrDok(nrdok, idNdermarrje));
            Db.Dispose();
            return mbush;
        }

        public bool merrVeprimeKFKokaSipasNrDokDtDokAndKF(string nrdok, DateTime dtdok, int idNdermarrje, int idkf)
        {
            clsDatabaseRegjistrim Db = new clsDatabaseRegjistrim();
            bool mbush = mbushVeprimeKFKoka(Db.merrVeprimeKFKokaSipasNrDokDtDokAndKF(nrdok, dtdok, idNdermarrje, idkf));
            Db.Dispose();
            return mbush;
        }

        public (clsMesazh mesazh, clsVeprimeKFKoka dokFundit) RuajVeprimeKF(IDictionary<string, object> hfNrAutoShitje)
        {
            return Ruaj(hfNrAutoShitje);
        }

        private (clsMesazh mesazh, clsVeprimeKFKoka dokFundit) Ruaj(IDictionary<string, object> hfNrAutoShitje)
        {
            bool kaNdryshimNumri;
            bool marreNrDokFirst = false;
            string nrDokFirst = "";
            clsVeprimeKFKoka dokFundit = new clsVeprimeKFKoka();
            try
            {
                using (MyTransactionScope scope = new MyTransactionScope())
                {
                    foreach (clsVeprimeKFKoka v in this)
                    {
                        if (marreNrDokFirst)
                        {
                            v.NrDok = nrDokFirst;
                            hfNrAutoShitje = null;
                        }
                        using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
                        {
                            clsMesazh kontrollo = v.KontrolloVeprimKf(out kaNdryshimNumri, data, hfNrAutoShitje);
                            if (!kontrollo.Status)
                                return (kontrollo, v);

                            clsMesazh ruaj = v.RuajVeprimKF(false, data);
                            if (!ruaj.Status)
                                return (ruaj, v);
                            if (!marreNrDokFirst)
                            {
                                nrDokFirst = v.NrDok;
                                marreNrDokFirst = true;
                            }
                        }
                        dokFundit = v;
                    }
                    scope.Complete();
                    return (new clsMesazh(true, "Ruajtja perfundoi me sukses"), dokFundit);
                }
            }
            catch (Exception ex)
            {
                return (new clsMesazh(false, ex.Message.ToString()), dokFundit);
            }
        }

        #endregion
    }
}