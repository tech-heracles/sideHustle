using DbCore.DbListPagesat;
using DbCore.DbOTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestApi.Models
{

    public class ModelKerkimi
    {
        public string KodInstance { get; set; }
        public string KodKlienti { get; set; }
        public string NrKontrate { get; set; }
        public string NrFature { get; set; }
        public OTCLlojSherbimi LlojFature { get; set; }
    }
}
