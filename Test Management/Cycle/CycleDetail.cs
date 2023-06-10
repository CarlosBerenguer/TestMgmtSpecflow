using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJamaTestMgmt.Test_Management.Cycle
{
    public class CycleDetail
    {
        public string name { get; set; }
        public string description { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }

        public CycleDetail() 
        {
            name = string.Empty;
            description = string.Empty;
            startDate = string.Empty;
            endDate = string.Empty;
        }
    }
}
