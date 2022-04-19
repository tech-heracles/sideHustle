using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;

namespace PlatinumWeb.Templates
{
    public class MyDoubleSpinTemplate : MyDoubleBaseTemplate
    {
        private static ASPxSpinEdit GetSpinEdit(string vlereDefault)
        {
            var spin = new ASPxSpinEdit
            {
                NumberType = SpinEditNumberType.Float,
                AllowMouseWheel = true,
                NullText = vlereDefault,
                AllowUserInput = true,
                Enabled = true
            };
            return spin;

        }

        public MyDoubleSpinTemplate(bool paformat, int shifraPasPresjes, string vlereDefault)
            : base(paformat, shifraPasPresjes, vlereDefault, GetSpinEdit(vlereDefault))
        {

        }
    }
}