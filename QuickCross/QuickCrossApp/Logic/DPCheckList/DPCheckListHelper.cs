using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Tabulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macromill.QCWeb.Common;
using static Macromill.QCWeb.Common.Constants;
using Microsoft.Office.Interop.Excel;
using QC4Common.DB;
using Qc4Launcher.Util;
using QC4Common.Model;
using static Qc4Launcher.Util.Constants;
using DataTable = System.Data.DataTable;
using QuestionType = Macromill.QCWeb.Tabulation.QuestionType;
using System.Data.SQLite;
using System.Data;

namespace Qc4Launcher.Logic.DPCheckList
{
    internal class DPCheckListHelper
    {
        internal static void SetCheckList(Questions.Question question, ref Outputs.Output other, SQLiteConnection con)
        {
            QCWebException exception = null;
            // 変更前データの読み込み
            // 新規アイテムの場合は、変更前データはNULL
            // 既存アイテムの場合は、データ修正の影響を受ける前の .txt ファイルを読み込む
            // サンプル削除の影響を受けないように、削除フラグの情報は読み込まないこと
            List<Data> originalData = null;
            List<Data> newData = null;
            //string path = string.Empty;
            //using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
            //{
            //    con.Open();

            QuestionSettings qstnDet;
            string columnName = null;
            string targetVariable = question.Name;
            qstnDet = Definiotion.VariableDictionary[targetVariable];
            if (targetVariable == QuestionVariableValue.QuestionVariableItem)
            {
                columnName = DBSettings.SampleIdColumnName;
            }
            else
            {
                columnName = DBSettings.ColumnNamePreText + qstnDet.Id;
            }

            DataTable dataTble;
            if (!question.IsNewItem)
            {
                //デフォルトの削除フラグを読み込むことで削除フラグ情報をデータ加工実行前に戻す
                //path = System.IO.Path.Combine(surveyRootPath, question.ID.ToString() + ".txt");
                //originalData = ReadTextFile.ReadData(path, question.QuestionType, delfilepath, out exception, true); 
                bool newItem = false;
                try
                {
                    if (DBHelper.GetDataTable("select question_flag from question where id = " + qstnDet.Id, con).Rows[0][0].ToString() == "An")
                        dataTble = DBHelper.GetDataTable("Select m." + columnName + " from multivariate_temp m join answers a on m.sort_no = a.sort_no order by m.sort_no;", con);
                    else
                        dataTble = DBHelper.GetDataTable("Select " + columnName + " from answers order by sort_no", con);
                }
                catch (Exception exc)
                {
                    if (exc.Message.Contains("no such column")) // If no such column, load null data
                    {
                        newItem = true;
                        dataTble = DBHelper.GetDataTable("Select Null As " + columnName + " from answers Order by sort_no", con);
                    }
                    else
                        throw;
                }
                try{
                    if(newItem){
                        setDeleteFlag(dataTble.Rows.Count);
                    }
                    originalData = ReadTextFile.ReadDataTable(dataTble, question.QuestionType, null, out exception);
                }finally{
                    if(newItem){
                        ReadTextFile.DeleteFlag = null;
                    }
                }
                if (exception != null)
                    throw exception;
            }

            // 最新データの読み込み
            //QuestionType questionType;
            try
            {
                if (DBHelper.GetDataTable("select question_flag from question where id = " + qstnDet.Id, con).Rows[0][0].ToString() == "An")
                    dataTble = DBHelper.GetDataTable("Select m." + columnName + " from multivariate_temp m join data_after_process a on m.sort_no = a.sort_no order by m.sort_no;", con);
                else
                    dataTble = DBHelper.GetDataTable("Select " + columnName + " from data_after_process order by sort_no", con);
            }
            catch (Exception exc)
            {
                if (exc.Message.Contains("no such column")) // If no such column, load null data
                {
                    dataTble = DBHelper.GetDataTable("Select Null As " + columnName + " from data_after_process Order by sort_no", con);
                }
                else
                    throw;
            }

            //Data Integration Check -- New Implementation 23-October-2019 // Adding dummy rows in the postion of deleted rows in case of LDEL
            string sSql = " Select ROWID as RowId from  answers  ";
            sSql += " Where ";
            sSql += " " + DBSettings.SampleIdColumnName + " Not In ";
            sSql += " ( ";
            sSql += " Select Distinct " + DBSettings.SampleIdColumnName + " From data_after_process ";
            sSql += " ) ";
            sSql += " Order By sort_no ";
            DataTable newDt = DBHelper.GetDataTable(sSql, con);

            bool[] deleteFlag = new bool[originalData.Count];
            foreach (DataRow drow in newDt.Rows)
            {
                int index = Convert.ToInt32(drow["RowId"]) - 1;
                deleteFlag[index] = true;
                DataRow dataRow = dataTble.NewRow();
                dataRow[columnName] = DBSettings.NotApplicableCharacter;
                if ((index + 1) > dataTble.Rows.Count)
                {
                    dataTble.Rows.Add(dataRow);
                }
                else
                {
                    dataTble.Rows.InsertAt(dataRow, index);
                }
            }
            try{
                if(newDt.Rows.Count > 0){
                    ReadTextFile.DeleteFlag = deleteFlag; 
                }
                //Data Integration Check

                newData = ReadTextFile.ReadDataTable(dataTble, question.QuestionType, null, out exception);
                //List<Data> newData = ReadTextFile.ReadData2(question, surveyRootPath, out path, out questionType, out exception, true);
            }finally{ 
                if(newDt.Rows.Count > 0){
                    ReadTextFile.DeleteFlag = null; 
                }
            }
            if (newData == null)
                throw exception;
            //}

            string[] sectorDescription = null;
            if (question.Sectors != null)
            {
                sectorDescription = new string[question.Sectors.Count];
                for (int j = 0; j < question.Sectors.Count; ++j)
                {
                    sectorDescription[j] = question.Sectors[j + 1].Description;
                }
            }

            // チェックリストGT表イメージ二次元配列を生成する
            string[,] cklistResultArray = null;
            TabulationDescriptions tabulationDescriptions = new TabulationDescriptions(Qc4Launcher.Util.CommonFunction.SetDescriptionString());
            QCWebException ex =
                CheckListTabulation.GetCheckListGTArray(question.QuestionType
                                                        , originalData, newData
                                                        , sectorDescription, out cklistResultArray, tabulationDescriptions);
            if (ex != null)
                throw ex;

            Tables.CheckListTable cklistTable =
                (Tables.CheckListTable)(other.Tables as Tables).Add(question.QuestionType
                                                                    , cklistResultArray
                                                                    , OutputType.CheckList);

            //cklistTable.SetQuestionInformation(question.Name
            //                                   , question.Description2()
            //                                   , question.QuestionType);

            cklistTable.SetQuestionInformation(
                question.Name
                , qstnDet.TableHeading != null ? (qstnDet.TableHeading + string.Empty + (question.Description != null ? "\n" + question.Description : string.Empty)) : question.Description
                , question.QuestionType);


            // 選択肢数
            if (question.Sectors != null)
                cklistTable.SectorsCount = question.Sectors.Count;
            // 新アイテム判定
            cklistTable.IsNewItem = question.IsNewItem;
            // 変更判定
            bool isChanged = false;
            if (originalData == null)
            {
                isChanged = true;
            }
            else
            {
                for (int r = 0; r < originalData.Count; ++r)
                {
                    if (originalData[r].IsDeleted != newData[r].IsDeleted)
                    {
                        isChanged = true;
                        break;
                    }
                    else if (originalData[r].IsNA != newData[r].IsNA)
                    {
                        isChanged = true;
                        break;
                    }
                    else if (originalData[r].IsIV != newData[r].IsIV)
                    {
                        isChanged = true;
                        break;
                    }
                    else
                    {
                        QuestionType qType = question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N);
                        switch (qType)
                        {
                            case QuestionType.SA:   // SA
                                if ((originalData[r] as SAData).Value != (newData[r] as SAData).Value)
                                {
                                    isChanged = true;
                                    break;
                                }
                                break;
                            case QuestionType.MA:   // MA
                                for (int m = 0; m < question.Sectors.Count; ++m)
                                {
                                    if ((originalData[r] as MAData).Value(m) != (newData[r] as MAData).Value(m))
                                    {
                                        isChanged = true;
                                        break;
                                    }
                                }
                                break;
                            case QuestionType.N:    // N
                                if ((originalData[r] as NData).Value != (newData[r] as NData).Value)
                                {
                                    isChanged = true;
                                    break;
                                }
                                break;
                        }
                    }
                    if (isChanged)
                        break;
                }
            }
            cklistTable.IsChanged = isChanged;
        }

        public static void setDeleteFlag(int count)
        {
            bool[] deleteFlag = new bool[count];
            for(int i = 0; i < count; i++){ 
                deleteFlag[i] = true;
            }
            ReadTextFile.DeleteFlag = deleteFlag;
        }
    }

    internal class CheckListOptions
    {
        internal string Reportprefix { get; set; } = "Report";
        internal string Xlbooknameprefix { get; set; } = "比較GT";
        //internal TableType Tabletype { get; set; } = TableType.NPer | TableType.N | TableType.Per;
        //internal TableOrientation Tableorientation { get; set; } = TableOrientation.Landscape;
        //internal TableType Pagesetuptabletype { get; set; } = (TableType)0;
        //internal int Minsamplescountonmarking { get; set; } = 30;
        //internal XlPaperSize Papersize { get; set; } = XlPaperSize.xlPaperA4;
        //internal XlPageOrientation Paperorientation { get; set; } = XlPageOrientation.xlLandscape;
        //internal TablesOnOneSheet Tablesononesheet { get; set; } = TablesOnOneSheet.Multi;
        internal ShowCode ShowNACode1 { get; set; } = (ShowCode)3;
        internal ShowCode ShowIVCode1 { get; set; } = 0;
        //internal string FilteringExpression1 { get; set; } = "[性別]の値が無回答ではない";
        //internal TabulationDescriptions TabulationDescriptions { get; set; } = new TabulationDescriptions();
        //internal WBSettingCode WBOn1 { get; set; } = WBSettingCode.WBOff;
        //internal List<Data> WBDataList { get; set; } = null;
        //internal bool HasFilter { get; set; } = false;
        //internal List<FilterSettingsCr> Filters { get; set; } = null;
        //internal string LocalizedFilteringExpression1 { get; set; } = "";
        //internal string GroupFolderPath { get; set; } = null;
        //internal string GroupVariable { get; set; } = null;
        //internal bool TabulateFullQuantity1 { get; set; } = false;
        //internal bool OutputGraph { get; set; } = false;
        //internal bool PieChartHideChoice { get; set; } = false;
        //internal int HideChartDescriptionMaxPercent { get; set; } = -1;
        //internal bool VisibleUnfitFlagAsFlag { get; set; } = true;
        //internal bool NoAnswerDenominatorFlag { get; set; } = false;
        //internal bool TargetNoAnswerOnOff { get; set; } = true;
        //public bool PreWbBase { get; set; } = true;
        //public string OutputDirectory { get; set; } = @"C:\\Test\";


    }

}
