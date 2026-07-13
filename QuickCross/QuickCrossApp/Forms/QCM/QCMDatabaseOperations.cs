using Qc4Launcher.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Forms.QCM
{
    class QCMDatabaseOperations
    {
        public static void Insert_QuestionToDb(List<Util.Qc3Parse.QDataDetail> qDataDetails, String DB_path)
        {
            DBHelper.CreateDatabase(DB_path + "\\" + Qc4Launcher.Util.Constants.TemplateFile.DB_FIlE);
            QuestionSettingDao qSetting = new QuestionSettingDao(DBHelper.GetConnectionString(DB_path + "\\" + Qc4Launcher.Util.Constants.TemplateFile.DB_FIlE));
            qSetting.CreateQuestion();
            qSetting.InsertQuestions(qDataDetails);
        }

        public static void Insert_AnswerToDb(List<Util.Qc3Parse.QDataDetail> qDataDetails, String DB_path, List<String[]> parsedTSV, ProgressUpdate qcmtoqc4, double count, string sql, bool firstTime = true)
        {
            DBHelper.CreateDatabase(DB_path + "\\" + Qc4Launcher.Util.Constants.TemplateFile.DB_FIlE);
            DataSheetDao dSetting = new DataSheetDao(DBHelper.GetConnectionString(DB_path + "\\" + Qc4Launcher.Util.Constants.TemplateFile.DB_FIlE));
            if (firstTime)
                dSetting.CreateAnswer(qDataDetails);
            // dSetting.InsertAnswerData(parsedTSV, qDataDetails, qcmtoqc4, count, true);
            dSetting.InsertAnswerData2(parsedTSV, qDataDetails, qcmtoqc4, count, true, sql);
        }

        public static void Insert_WeightBack(Microsoft.Office.Interop.Excel.Workbook TargetWorkBook)
        {
            DBHelper.ExecuteQuery("CREATE TABLE IF NOT EXISTS `weight_back` (id INTEGER PRIMARY KEY AUTOINCREMENT,name VARCHAR(255) ,weight_back_table VARCHAR(255))", DBHelper.GetConnectionString(TargetWorkBook));
        }
    }
}
