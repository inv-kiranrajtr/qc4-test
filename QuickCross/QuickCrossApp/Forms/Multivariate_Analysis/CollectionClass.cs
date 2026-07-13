using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Forms.Multivariate_Analysis
{
    public class CollectionClass
    {
        public class MyEventArgs : EventArgs
        {
            public MyEventArgs(bool enable)
            {
                Enable = enable;
            }

            public bool Enable { get; }
        }
        public class ProcessMethod
        {
            private string name;
            private string type;
            private string jtype;

            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }

            public string Type
            {
                get
                {
                    return type;
                }
                set
                {
                    type = value;
                }
            }
            public string JType
            {
                get
                {
                    return jtype;
                }
                set
                {
                    jtype = value;
                }
            }
        }

        public static class ProcessingMethod
        {
            public const string CLUSTER_ANALYSIS = "ClustorAnalysis";
            public const string FACTOR_ANALYSIS = "FactorAnalysis";
            public const string PSM_ANALYSIS = "PSMAnalysis";
            public const string BSA_ANALYSIS = "BSAAnalysis";
            public const string CORRESPONDENCE_ANALYSIS = "CorrespondenceAnalysis";
            public const string CSPORTFOLIO_ANALYSIS = "CS PortfolioAnalysis";
        }

        public class psmQuestions
        {
            public string Variable { get; set; }
            public string Question { get; set; }
            public List<string> Choices { get; set; }
            public string AnswerType { get; set; }
            public string Score { get; set; }
            public string AnswerTypeCount { get; set; }
            public string AnswerTypeBefore { get; set; }
            public int CategoryCount { get; set; }
            public int OrderNo { get; set; }

        }
        public class Choiceclass
        {
            public int orderId { get; set; }
            public string choice { get; set; }
        }
        public class MutiVariable
        {
            public string OnorOff { get; set; }
            public string ProcessName { get; set; }
            public string VariableName { get; set; }
            public int NumberofVariable { get; set; }
        }

    }
}
