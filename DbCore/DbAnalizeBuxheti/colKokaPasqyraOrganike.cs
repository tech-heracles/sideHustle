using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAnalizeBuxheti
{
    public class colKokaPasqyraOrganike : List<clsKokaPasqyraOrganike>
    {

        public colKokaPasqyraOrganike()
        {

        }
        public colKokaPasqyraOrganike(int idNdermarrje, int idNdermVit):base(new clsDatabaseAnalizeBuxheti().MerrListPasqyraOrganike(idNdermarrje, idNdermVit))
        {

        }

    }
}
