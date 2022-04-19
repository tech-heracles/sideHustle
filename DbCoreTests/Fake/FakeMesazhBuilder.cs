using AlphaWeb.Core.Messages;
using DbCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCoreTests.Fake
{
    public class FakeMesazhBuilder : IMesazhBuilder
    {
        public IMesazh CreateMesazhGabimi(string mesazhi = "")
        {
            return new FakeClsMesazh()
            {
                Tipi = TipMesazhi.Gabim,
                Status = false,
                PershkrimMesazhi = mesazhi
            };
        }

        public IMesazh CreateMesazhInformimi(string mesazhi = "")
        {
            return new FakeClsMesazh()
            {
                Tipi = TipMesazhi.Informim,
                Status = true,
                PershkrimMesazhi = mesazhi
            };
        }

        public IMesazh CreateMesazhSuksesi(string mesazhi = "")
        {
            return new FakeClsMesazh()
            {
                Tipi = TipMesazhi.Sukses,
                Status = true,
                PershkrimMesazhi = mesazhi
            };
        }
    }
}
