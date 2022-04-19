using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;

namespace PlatinumWeb.Templates
{

    public class MyIntSpinTemplate :MyIntBaseTemplate
    {

        public MyIntSpinTemplate(bool fillonMenje):base(fillonMenje, new ASPxSpinEdit { NumberType = SpinEditNumberType.Integer })
        {

        }
    }
}