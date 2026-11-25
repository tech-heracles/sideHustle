using System;
using System.Collections.Generic;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colEmail : List<clsEmail>
    {
        public colEmail(string subjekti, DateTime dateDergimi)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsEmail email = new clsEmail();
            mbushEmailet(data.merrEmail(subjekti, dateDergimi));
        }
        private bool mbushEmailet(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {

                Add(new clsEmail(rreshti));
            }
            return true;
        }
    }
}
