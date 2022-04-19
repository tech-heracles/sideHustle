using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbBuxheti
{
    public class ColBKomponente : List<ClsBKomponente>, IDataBaseReader
    {
        private int idNdermarrja;

        public ColBKomponente() { }

        public ColBKomponente(int idNdermarrja)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.ktheKomponenteSipasNdermarrje(idNdermarrja, this);
        }

        public void Mbush(IDataRecord record) => Add(new ClsBKomponente(record));
    }
}
