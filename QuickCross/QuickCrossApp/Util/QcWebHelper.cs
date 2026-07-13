using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Macromill.QCWeb.Question.Questions;
using Macromill.QCWeb.Tabulation;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Dao.ExEntity;

namespace Qc4Launcher.Util
{
    public static class QcWebHelper
    {
        //public static List<Data> GetTargetData(Question q, string path)
        //{
        //    string outputPath;
        //    QuestionType qType;
        //    QCWebException exception = null;
        //    List<Data> dataList = ReadTextFile.ReadData2(q, path, out outputPath, out qType, out exception, true);
        //    if (dataList == null) throw exception;
        //    return dataList;
        //}

        public static List<Data> GetTargetData() // Dummy
        {
            List<Data> dataList = new List<Data>();
            for(int i = 1; i <= 100; i++)
            {
                NData targetData = new NData(DataType.NormalData,i, false);
                dataList.Add(targetData);
            }
            return dataList;
        }


       
        public static string[] GetWeightValues(IList<TCategoryInfo> categories)
        {
            string[] ret = (from c in categories select c.WeightValue).ToArray();
            if (ret.Length == 0) return null;
            return ret;
        }


    }
}
