using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class DataReportCL
    {
        public int IdDataReportCL { get; set; }
        public string DataR { get; set; }
        public string Progress { get; set; }
        public ML.ReportCL ReportCL { get; set; }
    }
}
