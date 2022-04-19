using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colInfoArt : System.Collections.Generic.List<clsInfoArt>
    {
        public new clsInfoArt this[int index]
        {
            get { return ((clsInfoArt)base[index]); }
        }
     

    }
}
