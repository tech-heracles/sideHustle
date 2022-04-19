using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbOTC
{
    public class colOTCFatura : List<OTCFatura>,IDataBaseReader
    {


        public colOTCFatura() { }
        public colOTCFatura(int idPagesa)
        {
            using (clsDatabaseOTC db =new clsDatabaseOTC())
                db.MerrFaturaSipasIdPagese(idPagesa,this);
            
        }
     

        public void Mbush(IDataRecord record)
        {
            Add(new OTCFatura(record));
        }

       
        internal clsMesazh Ruaj()
        {

            foreach (var fatura in this)
            {
                var mesazh = fatura.Ruaj();
                if (!mesazh) return mesazh;
            }
            return new clsMesazh(true, "Ruajtja u krye me sukses!");

        }

        public colOTCFatura Clone()
        {
            colOTCFatura col = new colOTCFatura();
            this.ForEach(cls => col.Add(cls));
            return col;
        }
        
    }
}
