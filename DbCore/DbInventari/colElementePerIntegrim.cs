using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbInventari
{
    public class colElementePerIntegrim: List<clsElementePerIntegrim>
    {
        public colElementePerIntegrim(int idNdermarje) : base(new clsDatabaseInventari().MerrElementePerIntegrim(idNdermarje)) { }

        public colElementePerIntegrim(int idNdermarje, int idLloji) : base(new clsDatabaseInventari().MerrElementePerIntegrimTeLidhura(idNdermarje, idLloji)) { }
        public colElementePerIntegrim(int idNdermarje, int idLloji, bool teLidhur) : base(new clsDatabaseInventari().MerrElementePerIntegrimSipasLlojit(idNdermarje, idLloji, teLidhur)) { }
    }
}
