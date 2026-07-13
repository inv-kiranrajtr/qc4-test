using Macromill.QCWeb.Tabulation;
using QC4Common.Model;
using System.Collections.Generic;

namespace Qc4Launcher.Logic.MultiVariate
{
    public class CorrespondenceSettings
    {
        public CorrespondenceSettings()
        {
            tabulationType = 2;
            calcType = 2;
        }

        public int tabulationType { get; set; } // 1 - crosss, 2 - gt
        public int noOfDimension { get; set; }
        public int horizontalNo { get; set; }
        public int verticalNo { get; set; }

        public bool horizontalRevData { get; set; }
        public bool verticalRevData { get; set; }
        public bool HasFilter { get; set; }
        public bool HasvalidValue { get; set; }
        public List<FilterSettingsCr> Filters { get; set; }

        public int calcType { get; set; } // 1 - percentage, 2 - freequency
        public string crRowVar { get; set; }
        public string crColVar { get; set; }
        public int crRowChoiceCnt { get; set; }
        public int crColChoiceCnt { get; set; }
        public int gtChoiceCnt { get; set; }
        public List<string> gtVars { get; set; }

    }
}
