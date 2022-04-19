using DbCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaWeb.Core.Messages
{
    public interface IMesazhBuilder
    {
        IMesazh CreateMesazhSuksesi(string mesazhi = "");
        IMesazh CreateMesazhGabimi(string mesazhi = "");
        IMesazh CreateMesazhInformimi(string mesazhi = "");
    }
}
