using Macromill.QCWeb.Tabulation;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Logic.MultiVariate
{
    public class PSMSettings
    {
        public PSMSettings()
        {
            effValHighStart = -1;
            effValHighEnd = -1;
            effValCheapStart = -1;
            effValCheapEnd = -1;
            effValTooHighStart = -1;
            effValTooHighEnd = -1;
            effValTooCheapStart = -1;
            effValTooCheapEnd = -1;
            questionType = QuestionType.N;
        }

        public string high { get; set; }
        public string cheap { get; set; }
        public string tooHigh { get; set; }
        public string tooCheap { get; set; }

        public bool HasFilter { get; set; }
        public bool HasvalidValue { get; set; }
        public List<FilterSettingsCr> Filters { get; set; }

        public double effValHighStart { get; set; }
        public double effValHighEnd { get; set; }
        public double effValCheapStart { get; set; }
        public double effValCheapEnd { get; set; }
        public double effValTooHighStart { get; set; }
        public double effValTooHighEnd { get; set; }
        public double effValTooCheapStart { get; set; }
        public double effValTooCheapEnd { get; set; }

        public bool invertHighAndCheap { get; set; }

        public double minPrice { get; set; }
        public double maxPrice { get; set; }
        public double scaleInterval { get; set; }
        public QuestionType questionType { get; set; }
        

    }
}
