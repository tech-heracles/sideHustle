using DbCore.IMBUtils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{
    public class colKonfigurimEmailKI : List<clsKonfigurimEmailKI>, IDataBaseReader
    {

        #region Metoda Publike
        public clsMesazh Ruaj() { 
            if (this.Any())
            {                
                var dt = this.ToDataTable("Id", "IdTemplateImporti", "Statusi", "Lloji", "IdDestinacion", "Destinacion", "Email", "IdKrijuesi", "IdStatusDok");
                using (clsDatabaseAdmin shareDB = new clsDatabaseAdmin())
                    return shareDB.ruajKonfigurimeEmailImportDT(dt);
            }
            return new clsMesazh(true, "Nuk ka konfigurim per te ruajtur.");
        }
        public void merrKonfigurimeEmail(int idKonfigImport)
        {
            using (clsDatabaseAdmin shareDb = new clsDatabaseAdmin())
                shareDb.merrKonfigurimEmalSipasKonfigImporti(idKonfigImport, this);
        }
        #endregion

        #region Metoda Private
        
        public void Mbush(IDataRecord record)
        {
            Add(new clsKonfigurimEmailKI(record));
        }
        #endregion

    }
}
