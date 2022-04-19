using DbCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaWeb.Core.Messages
{
    public class MesazhBuilder : IMesazhBuilder
    {
        //sealed class
        //private static MesazhBuilder instance;
        //private static object syncRoot = new object();

        public MesazhBuilder()
        {

        }

        //public static MesazhBuilder Instance
        //{
        //    get
        //    {
        //        lock (syncRoot)
        //        {
        //            if (instance == null)
        //            {
        //                instance = new MesazhBuilder();
        //            }
        //        }

        //        return instance;
        //    }
        //}

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
