using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace DbCore.DbShare
{
    public class clsKonfLlojRreshti
    {
        private int idKushTemplate;
        private List<clsKonfLlojRreshtiVlere> colKonfLlojRreshtiVlere;
        public int IdKushTemplate
        {
            get
            {
                return idKushTemplate;
            }
            set
            {
                if (idKushTemplate == value)
                    return;
                idKushTemplate = value;
            }
        }
        public List<clsKonfLlojRreshtiVlere> ColKonfLlojRreshtiVlere
        {
            get
            {
                return colKonfLlojRreshtiVlere;
            }
            set
            {
                if (colKonfLlojRreshtiVlere == value)
                    return;
                colKonfLlojRreshtiVlere = value;
            }
        }

        public clsKonfLlojRreshti(int idKushTemplate, string lloji)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            if (!mbushKonfLlojRreshti(data.merrKonfLlojRreshti(idKushTemplate)))
                if (!mbushKonfLlojRreshti(data.merrKonfLlojRreshti(lloji)))
                    throw new Exception("Gabim gjate leximit te konfigurimit te llojit te rreshtit nga db-ja");
            data.Dispose();
        }
        public clsKonfLlojRreshti() { }
        
        private bool mbushKonfLlojRreshti(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
                return false;
            int tempIdKushTemplate = 0;
            DataRowCollection rows = dataTable.Rows;
            colKonfLlojRreshtiVlere = new List<clsKonfLlojRreshtiVlere>();
            for (int i = 0; i < rows.Count; i++)
            {
                if (tempIdKushTemplate != 0 && tempIdKushTemplate != idKushTemplate)
                    return false; //brenda te njejtit konfigurim idKushTemplate duhet te jete e njejte
                clsKonfLlojRreshtiVlere rreshtVlere = new clsKonfLlojRreshtiVlere();
                if (!rreshtVlere.mbushKofLlojRreshtiVlere(rows[i]))
                    return false;
                if (rreshtVlere.Rend == -1)
                    rreshtVlere.Rend = i + 1;
                colKonfLlojRreshtiVlere.Add(rreshtVlere);
                if (tempIdKushTemplate == 0)
                    tempIdKushTemplate = idKushTemplate = rreshtVlere.IdKushTemplate;
                else
                    tempIdKushTemplate = rreshtVlere.IdKushTemplate;
            }
            return true;
        }

        public static clsKonfLlojRreshti getKonfLlojRreshti(DataTable kushtAlternativa, int kategoria, string kodi)
        {
            int idVleraKushti = clsAlternativaKushti.ktheVlereAlternativaKushti(kushtAlternativa, kodi);
            if (idVleraKushti == -1)
                return new clsKonfLlojRreshti();

            if (kategoria == 1 || kategoria == 2)
                return new clsKonfLlojRreshti(idVleraKushti, "Shitje");
            else
                return new clsKonfLlojRreshti(idVleraKushti, "ArkaBanka");
        }

    }
}
