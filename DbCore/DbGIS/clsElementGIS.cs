using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore;

namespace DbCore.DbGIS
{
    public class clsElementGIS
    {
        private int gid;
        private colVleraFushaShtese fushatShtese;

        public colVleraFushaShtese FushatShtese
        {
            get
            {
                return fushatShtese;
            }

            set
            {
                fushatShtese = value;
            }
        }

        public int Gid
        {
            get
            {
                return gid;
            }

            set
            {
                gid = value;
            }
        }


        public clsElementGIS() { }

        public clsMesazh Ruaj()
        {
            if (fushatShtese == null)
                throw new MyException("Nuk ka asgje per te ruajtur!fushat shtese eshte bosh");
            FushatShtese.ForEach(fusha => fusha.IdLidhese = gid);

            return FushatShtese.Ruaj();
        }
    }
}
