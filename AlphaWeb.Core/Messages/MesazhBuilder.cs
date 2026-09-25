using DbCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaWeb.Core.Messages
{
    public class MesazhBuilder : IMesazhBuilder
    {
        //sealed class

        public MesazhBuilder()
        {

        }

        //public static MesazhBuilder Instance
        //    get


        public IMesazh CreateMesazhGabimi(string mesazhi = "")
        {
            return new MesazhGabimi(mesazhi);
        }

        public IMesazh CreateMesazhInformimi(string mesazhi = "")
        {
            return new MesazhInformimi(mesazhi);
        }

        public IMesazh CreateMesazhSuksesi(string mesazhi = "")
        {
            return new MesazhSuksesi(mesazhi);
        }
    }
}
