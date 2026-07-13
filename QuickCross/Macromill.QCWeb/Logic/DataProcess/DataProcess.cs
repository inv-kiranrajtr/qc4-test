using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Macromill.QCWeb.Tabulation;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Common;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Diagnostics;
using Macromill.QCWeb.Question;
using System.IO;
using ExcelAddIn.DB;

namespace Macromill.QCWeb.DataProcess
{
    /// <summary>
    /// データ加工情報を保持するIDataProcessインターフェイスの実装クラスのインスタンスへの参照からなるコレクションクラス
    /// </summary>
    [ComVisible(false), Guid("1E5EA566-2BF0-4157-9C01-B150D0FF0EAF")]
    public class DataProcesses : List<IDataProcess>, IDisposable
    {
        #region 各データ加工クラス
        /// <summary>
        /// 各データ加工クラスのスーパークラス
        /// </summary>
        [ComVisible(false), Guid("E455FA27-CDCB-4d7a-BB4E-80AC8D027736")]
        public class DataProcess : IDataProcess
        {
            private DataProcesses collection = null;
            private DataProcessCode dataprocessCode = DataProcessCode.Integrate;
            private NewQuestions questions = null;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="collection">親コレクションのDataProcessesクラスのインスタンスへの参照</param>
            /// <param name="dataprocesscode">データ加工の種類を表すDataProcessCode列挙型の値</param>
            internal DataProcess(DataProcesses collection, DataProcessCode dataprocesscode)
            {
                this.collection = collection;
                if (Enum.IsDefined(typeof(DataProcessCode), dataprocesscode))
                {
                    dataprocessCode = dataprocesscode;
                }
                questions = new NewQuestions(this);
            }

            /// <summary>
            /// データ加工の種類を表すDataProcessCode列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public DataProcessCode DataProcessCode
            {
                get
                {
                    return dataprocessCode;
                }
            }

            private bool runFlag = true;
            /// <summary>
            /// データ加工の実行のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool RunFlag
            {
                get
                {
                    return runFlag;
                }
                set
                {
                    runFlag = value;
                }
            }


            private bool reverseIsTrue = false;
            /// <summary>
            /// IL Change for class supporting ! operator
            /// </summary>

            public bool ReverseIsTrue
            {
                get
                {
                    return reverseIsTrue;
                }
                set
                {
                    reverseIsTrue = value;
                }
            }

            private bool treatasZero = false;
            /// <summary>
            /// IL change for CLASS suppoting TreatAs 0
            /// </summary>
            public bool IsTreatasZero
            {
                get
                {
                    return treatasZero;
                }
                set
                {
                    treatasZero = value;
                }
            }


            private string description = null;
            /// <summary>
            /// 説明を取得/設定するプロパティ
            /// </summary>
            public string Description
            {
                get
                {
                    return description;
                }
                set
                {
                    description = value;
                }
            }

            /// <summary>
            /// 新アイテム群情報を保持したNewQuestionsクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public INewQuestions Questions
            {
                get
                {
                    return questions;
                }
            }

            /// <summary>
            /// 新アイテムのファイル名をフルパスで返すメソッド
            /// </summary>
            /// <param name="index">Questionsのインデックス</param>
            /// <param name="fileExtChange">新アイテムファイルの拡張子を.txtにする場合はfileExtension.txt、.dpの場合はfileExtension.dp
            ///                             .tmpの場合はfileExtension.tmp DefaultはfileExtension.dp
            /// </param>
            /// <returns>新アイテムのファイル名を含んだフルパスの文字列</returns>
            public string GetNewItemFilePath(int index, GlobalsCommonConstant.fileExtension fileExtChange = GlobalsCommonConstant.fileExtension.dp)
            {
                string extension = DataProcessCommon.GetExtension(fileExtChange);

                return System.IO.Path.Combine(DataProcessCommon.GetProcessIdPath() + @"\", Questions[index].ItemId + extension);
            }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public virtual void Regist()
            {
            }

            /// <summary>
            /// データ加工を実行するメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public virtual void Execute()
            {
            }

            /// <summary>
            /// 加工設定内容の登録の直後にデータ加工を実行するメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public void RegistAndExecute()
            {
                Regist();
                ParentCollection.Execute();
            }

            /// <summary>
            /// 親コレクションのDataProcessesクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public DataProcesses ParentCollection
            {
                get
                {
                    return collection;
                }
            }

            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                questions.Dispose();
                collection = null;
            }
            public static string ParseMconvertdata(char[] line)
            {
                string mconvert = "*";
                int j = 0;
                int i = line.Length;
                for (i = line.Length, j = 1; i >= 1; i--, j++)
                {
                    if (line[i - 1] == '1')
                    {
                        if (mconvert == "*")
                        {
                            mconvert = ",";
                        }
                        mconvert += j;
                        mconvert += ",";
                    }
                }
                return mconvert;
            }
            public Dictionary<string, string> GetAllValues(string paramvalue, int catcount, string itemid)
            {
                Dictionary<string, string> Join_Dict = new Dictionary<string, string>();
                if (paramvalue != null)
                {
                    //need to add new logic here for mixed symbols
                    //make list
                    bool isnot = false;
                    if (paramvalue.StartsWith("!") || paramvalue.StartsWith("<>")) isnot = true;
                    List<string> commasep = new List<string>();
                    List<string> barsep = new List<string>();
                    List<string> minsep = new List<string>();
                    List<int> exclidelist = new List<int>();
                    //split with ','
                    string[] criteriacommavalues = paramvalue.Split(',');
                    foreach (string str in criteriacommavalues)
                    {
                        commasep.Add(str);//add whole to  list
                    }
                    // for each nd split with '/'
                    foreach (string str in commasep)
                    {
                        if (str.Contains('/'))
                        {
                            string[] criteriabarvalues = str.Split('/');
                            foreach (string s in criteriabarvalues)
                            {
                                barsep.Add(s);//add whole to list
                            }
                        }
                        else
                            barsep.Add(str);
                    }

                    //chek for '-'
                    foreach (string str in barsep)
                    {
                        //if contains ! or <>
                        if (isnot)//str.StartsWith("!") || str.StartsWith("<>")
                        {
                            string notvalue = str;
                            //need to remove the items from list and add other category numbers
                            // criteriaValueDescription = criteriaValueDescription.TrimStart('!');
                            if (str.StartsWith("!")) notvalue = str.TrimStart('!');
                            else if (str.StartsWith("<>")) notvalue = str.Replace("<>", "");
                            //criteriaValueDescription = criteriaValueDescription.Replace("<>", "");//TrimStart('<>');
                            int criteriabeginning = 1;
                            int criteriaend = catcount;// Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                            if (str.Contains('-'))
                            {
                                int strt = 0, end = 0;
                                string[] criterisplitvals = notvalue.Split('-');

                                if (criterisplitvals.Length == 1)
                                {
                                    try
                                    {
                                        strt = Convert.ToInt32(criterisplitvals[0]);
                                    }
                                    catch (Exception e) { strt = 1; }
                                    end = strt;

                                }
                                else
                                {
                                    try
                                    {
                                        strt = Convert.ToInt32(criterisplitvals[0]);
                                    }
                                    catch (Exception e) { strt = 1; }
                                    try
                                    {
                                        end = Convert.ToInt32(criterisplitvals[1]);
                                    }
                                    catch (Exception e)
                                    {
                                        end = catcount;// Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);

                                    }
                                }
                                //List<int> exclidelist = new List<int>();
                                for (int ci = strt; ci <= end; ci++)
                                {
                                    exclidelist.Add(ci);
                                }

                            }
                            else
                            {
                                try
                                {
                                    exclidelist.Add(Convert.ToInt32(str));
                                }
                                catch { }
                            }


                        }
                        else
                        {
                            //else
                            if (str.Contains('-'))
                            {

                                int start = 0, limit = 0;
                                string[] criteriaminvalues = str.Split('-');
                                // foreach (string s in criteriaminvalues)
                                {

                                    try
                                    {

                                        if (criteriaminvalues.Length == 1)
                                        {
                                            try
                                            {
                                                start = Convert.ToInt32(criteriaminvalues[0]);
                                            }
                                            catch (Exception e) { start = 1; }
                                            limit = start;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                start = Convert.ToInt32(criteriaminvalues[0]);
                                            }
                                            catch (Exception e) { start = 1; }//actually get min value of answer
                                            try
                                            {
                                                limit = Convert.ToInt32(criteriaminvalues[1]);
                                            }
                                            catch (Exception e)
                                            {//actually get max value of answer;need to get max of choice no from item id and set limit
                                                limit = catcount;// Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);

                                            }
                                        }
                                        if (limit < start)//need to reverse if 9-7 comes
                                        {
                                            int temp = limit;
                                            limit = start;
                                            start = temp;
                                        }
                                    }
                                    catch { }

                                    for (int ci = start; ci <= limit; ci++)
                                    {
                                        minsep.Add(ci.ToString());//add whole to list
                                    }
                                }
                            }
                            else
                                minsep.Add(str);
                        }
                    }
                    if (isnot)
                    {
                        for (int ci = 1; ci <= catcount; ci++)
                        {
                            if (!exclidelist.Contains(ci))
                                minsep.Add(ci.ToString());
                        }
                    }
                    int dictcount = 0;
                    foreach (string str in minsep)
                    {
                        dictcount++;
                        if (!Join_Dict.ContainsKey(itemid + "-" + str))
                            Join_Dict.Add(itemid + "-" + str, dictcount.ToString());
                    }

                }
                return Join_Dict;

            }
        }

        #region INTEGRATEのデータ加工のスーパークラスとそのサブクラス
        /// <summary>
        /// INTEGRATEのデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("945CD551-755D-499d-B981-26789362DC88")]
        public class DataProcessIntegrate : DataProcess
        {
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="collection">親コレクションのDataProcessesクラスのインスタンスへの参照</param>
            /// <param name="dataprocesscode">データ加工の種類を表すDataProcessCode列挙型の値</param>
            protected DataProcessIntegrate(DataProcesses collection, DataProcessCode dataprocesscode) : base(collection, dataprocesscode) { }

            internal DataProcessIntegrate(DataProcesses collection) : base(collection, DataProcessCode.Integrate) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
                // INTEGRATEのDB登録処理
            }

            /// <summary>
            /// データ加工を実行するメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Execute()//recode need to do criteria here
            {
                if (Questions == null) return;
                if (!RunFlag) return;
                if (ParentCollection == null || string.IsNullOrWhiteSpace(ParentCollection.DataDirectoryPath)) return;
                try
                {

                    for (int i = 0; i < Questions.Count; ++i)
                    {
                        var replacedata = new List<Data>();
                        //191 
                        bool iscriteria = false;
                        decimal orgItemId = decimal.Parse(Questions[i].ItemId);
                        Question.Questions.Question q = ParentCollection.GetQuestion(orgItemId);
                        if (!ParentCollection.fromSubTotal)
                        {
                            replacedata = ParentCollection.GetRawData(decimal.Parse(Questions[0].ItemId));//Questions[0].SourceItemId //came null so cahnged to item id
                        }//

                        GlobalsCommonConstant.fileExtension fileExt = GlobalsCommonConstant.fileExtension.dp;
                        bool unfitFlag = false;
                        int mabufsector = Questions[i].CategoryCount;
                        int sectorsCount = Questions[i].Sectors.Count;
                        mabufsector = mabufsector == 0 ? sectorsCount : mabufsector;
                        if (Questions[i].GetType() == typeof(NewQuestions.NewQuestion))
                        {
                            var newQuestion = Questions[i] as NewQuestions.NewQuestion;
                            if (newQuestion.UnfitFlag)
                            {
                                unfitFlag = true;
                                //INTEGRATE特別処理：「全てが非該当だった場合結果は非該当」
                                //MCONVERT特別処理：「元アイテムがすべて非該当のサンプルは新アイテムも非該当とする」
                                //Sectorsの最後に特別処理に該当する条件が設定されていることが前提
                                sectorsCount = Questions[i].Sectors.Count - 1;
                            }
                            fileExt = newQuestion.ChangeExtension;
                        }
                        bool isMA = (Questions[i].QuestionType & Tabulation.QuestionType.MA) == Tabulation.QuestionType.MA;
                        string path = GetNewItemFilePath(i, fileExt);
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, false, Encoding.UTF8))
                        {
                            for (int r = 0; r < ParentCollection.SamplesCount; ++r)
                            {
                                //191 
                                Data originalData = null;
                                if (!ParentCollection.fromSubTotal)
                                {
                                    originalData = ParentCollection.GetRawData(r, orgItemId);
                                }
                                int Sectorscount = Questions[i].Sectors.Count;
                                if (Questions[i].Sectors.Count > 1)//191  code 
                                {
                                    //check last sector criteria having sub criteria (for multiple criteria) or last sector having alias=1 for criteria presence
                                    // if (Questions[i].Sectors[Sectorscount - 1].Criteria != null)
                                    {
                                        INewVirtualQuestionSector s = (Questions[i].Sectors[Sectorscount - 1] as INewVirtualQuestionSector);
                                        if (s != null)
                                        {
                                            if (s.ModifyDataEdit == 0 && s.EditMethod == 0)
                                            {
                                                Sectorscount = Sectorscount - 1;//cannot do for add3 and other having more sectors
                                                iscriteria = true;
                                                if (this is DataProcessClass || this is DataProcessRecode)//somtimes no need this cheking only unfit is ok
                                                {
                                                    sectorsCount = Sectorscount;
                                                }
                                                else
                                                {
                                                    if (unfitFlag)//Redmine id : 174643
                                                    {
                                                        sectorsCount = Sectorscount - 1;// sectorsCount - 1; //Sectorscount;
                                                    }
                                                    else sectorsCount = Sectorscount;//Redmine id : 174643
                                                }

                                            }
                                        }
                                    }

                                    // GetQuestionIDforDP(Questions[Sectorscount].Sectors);
                                }

                                if (unfitFlag)
                                {
                                    //INTEGRATE特別処理：「全てが非該当だった場合結果は非該当」
                                    //MCONVERT特別処理：「元アイテムがすべて非該当のサンプルは新アイテムも非該当とする」
                                    //Sectorsの最後に特別処理に該当する条件が設定されていることが前提
                                    if (Questions[i].Sectors[sectorsCount].Criteria != null
                                        && Questions[i].Sectors[sectorsCount].Criteria.IsTrue(r))
                                    {
                                        //171313 fix  
                                        if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                        {
                                            writer.WriteLine(string.Empty);
                                            // Redmine id : 174639-174640-174641
                                            //if (originalData.IsIV)
                                            //{
                                            //    writer.WriteLine("*");
                                            //}
                                            //else if (originalData.IsNA)
                                            //{
                                            //    writer.WriteLine();
                                            //}
                                            //else if (originalData.GetType() == typeof(NData))
                                            //{
                                            //    writer.WriteLine((originalData as NData).Value);
                                            //}
                                            //else if (originalData.GetType() == typeof(FAData))
                                            //{
                                            //    writer.WriteLine((originalData as FAData).Value);
                                            //}
                                            //else if (originalData.GetType() == typeof(SAData))
                                            //{
                                            //    writer.WriteLine((originalData as SAData).Value);
                                            //}
                                            //else if (originalData.GetType() == typeof(MAData))
                                            //{

                                            //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                            //}
                                            originalData = null;
                                        }
                                        else
                                        {
                                            writer.WriteLine("*");
                                        }
                                        continue;
                                    }
                                }
                                char[] mabuf = null;
                                if (isMA)
                                {
                                    mabuf = new string('0', mabufsector).ToCharArray();
                                }
                                bool isNoAnswer = true;
                                for (int j = 0; j < sectorsCount; ++j)
                                {
                                    if (Questions[i].Sectors[j].Criteria != null && Questions[i].Sectors[j].Criteria.IsTrue(r))
                                    {
                                        isNoAnswer = false;
                                        Tabulation.DataType dType = Questions[i].Sectors[j].DataType;
                                        if (dType == Tabulation.DataType.NAData)
                                        { //adding criteria condition for matching QC3 --redmine:171313 //!this.IsTreatasZero && added in da begining 576,618 618,666,729,783-Redmine id : 178640
                                            if (!this.IsTreatasZero && iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                            {
                                                writer.WriteLine(string.Empty);

                                                // Redmine id : 174639-174640-174641
                                                //if (originalData.IsIV)
                                                //{
                                                //    writer.WriteLine("*");
                                                //}
                                                //else if (originalData.IsNA)
                                                //{
                                                //    writer.WriteLine();
                                                //}
                                                //else if (originalData.GetType() == typeof(NData))
                                                //{
                                                //    writer.WriteLine((originalData as NData).Value);
                                                //}
                                                //else if (originalData.GetType() == typeof(FAData))
                                                //{
                                                //    writer.WriteLine((originalData as FAData).Value);
                                                //}
                                                //else if (originalData.GetType() == typeof(SAData))
                                                //{
                                                //    writer.WriteLine((originalData as SAData).Value);
                                                //}
                                                //else if (originalData.GetType() == typeof(MAData))
                                                //{

                                                //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                //}
                                                originalData = null;
                                            }
                                            else
                                            {
                                                writer.WriteLine();
                                            }
                                            mabuf = null;
                                            break;
                                        }
                                        if (dType == Tabulation.DataType.IVData)
                                        {//adding criteria condition for matching QC3 --redmine:171313-//!this.IsTreatasZero && added in da begining 576,618 618,666,729,783-Redmine id : 178640
                                            if (!this.IsTreatasZero && iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                            {
                                                writer.WriteLine(string.Empty);
                                                // Redmine id : 174639-174640-174641
                                                //if (originalData.IsIV)
                                                //{
                                                //    writer.WriteLine("*");
                                                //}
                                                //else if (originalData.IsNA)
                                                //{
                                                //    writer.WriteLine();
                                                //}
                                                //else if (originalData.GetType() == typeof(NData))
                                                //{
                                                //    writer.WriteLine((originalData as NData).Value);
                                                //}
                                                //else if (originalData.GetType() == typeof(FAData))
                                                //{
                                                //    writer.WriteLine((originalData as FAData).Value);
                                                //}
                                                //else if (originalData.GetType() == typeof(SAData))
                                                //{
                                                //    writer.WriteLine((originalData as SAData).Value);
                                                //}
                                                //else if (originalData.GetType() == typeof(MAData))
                                                //{

                                                //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                //}
                                                originalData = null;
                                            }
                                            else
                                            {
                                                writer.WriteLine("*");
                                            }
                                            mabuf = null;
                                            break;
                                        }
                                        if (mabuf == null)  // SA
                                        {
                                            if (Questions[i].GetType() == typeof(NewQuestions.NewVirtualQuestion))
                                            {
                                                writer.WriteLine((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias);
                                            }
                                            else
                                            {
                                                //!this.IsTreatasZero && added in da begining 576,618 618,666,729,783-Redmine id : 178640
                                                if (!this.IsTreatasZero && iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                {
                                                    writer.WriteLine(string.Empty);
                                                    // Redmine id : 174639-174640-174641
                                                    //if (originalData.IsIV)
                                                    //{
                                                    //    writer.WriteLine("*");
                                                    //}
                                                    //else if (originalData.IsNA)
                                                    //{
                                                    //    writer.WriteLine();
                                                    //}
                                                    //else if (originalData.GetType() == typeof(NData))
                                                    //{
                                                    //    writer.WriteLine((originalData as NData).Value);
                                                    //}
                                                    //else if (originalData.GetType() == typeof(FAData))
                                                    //{ writer.WriteLine((originalData as FAData).Value); }
                                                    //else if (originalData.GetType() == typeof(SAData))
                                                    //{ writer.WriteLine((originalData as SAData).Value); }
                                                    //else if (originalData.GetType() == typeof(MAData))
                                                    //{

                                                    //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                    //}
                                                    originalData = null;
                                                }
                                                else
                                                {
                                                    //191 27-12-19 -integrate issue +recode  after newsecorcriteria commented
                                                    var data = ParentCollection.GetRawData(r, decimal.Parse(Questions[i].SourceItemId));
                                                    if (this is DataProcessRecode && data.IsIV || (this is DataProcessClass && data.IsIV))
                                                    {
                                                        writer.WriteLine("*");
                                                    }
                                                    else if (this is DataProcessRecode && data.IsNA || (this is DataProcessClass && data.IsIV))
                                                    {
                                                        writer.WriteLine();
                                                    }
                                                    //else if (this is DataProcessIntegrate && data.IsIV)////added for Integrate SA BAse 1--31-12-19
                                                    //{
                                                    //    writer.WriteLine("*--");
                                                    //}
                                                    else
                                                    {
                                                        writer.WriteLine((j + 1).ToString());//writing data here 
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            mabuf[mabuf.Length - j - 1] = '1';//mconvert
                                        }
                                    }
                                }
                                //新カテゴリのどの条件にもあてはまらない場合は、無回答
                                if (isNoAnswer)
                                {
                                    //for  RECODE getting blank for unmatched criteria adding criteria validation here and keeping original code in else case 191 code-on 22-11-19
                                    //191  criteria //!this.IsTreatasZero && added in da begining 666,729,783
                                    if (!this.IsTreatasZero && iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                    {
                                        writer.WriteLine(string.Empty);
                                        // Redmine id : 174639-174640-174641
                                        //if (originalData.IsIV)
                                        //{
                                        //    writer.WriteLine("*");
                                        //}
                                        //else if (originalData.IsNA)
                                        //{
                                        //    writer.WriteLine();
                                        //}
                                        //else if (originalData.GetType() == typeof(NData))
                                        //{
                                        //    writer.WriteLine((originalData as NData).Value);
                                        //}
                                        //else if (originalData.GetType() == typeof(FAData))
                                        //{ writer.WriteLine((originalData as FAData).Value); }
                                        //else if (originalData.GetType() == typeof(SAData))
                                        //{ writer.WriteLine((originalData as SAData).Value); }
                                        //else if (originalData.GetType() == typeof(MAData))
                                        //{

                                        //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                        //}
                                        originalData = null;
                                    }
                                    else
                                    {
                                        if (this is DataProcessRecode || this is DataProcessClass)
                                        {
                                            //RECODEおよびCLASSの場合は、どのカテゴリ条件にもあてはまらない場合で、元データが非該当の場合は新アイテムも非該当とする
                                            var data = ParentCollection.GetRawData(r, decimal.Parse(Questions[i].SourceItemId));
                                            if ((data.DataType & DataType.IVData) != 0)
                                            {
                                                writer.WriteLine("*");
                                            }
                                            else
                                            {
                                                writer.WriteLine();
                                            }
                                        }
                                        else
                                        {
                                            writer.WriteLine();
                                        }
                                    }
                                }
                                else
                                {
                                    if (mabuf != null)
                                    {
                                        //191  criteria //!this.IsTreatasZero && added in da begining 666,729,783-Redmine id : 178640
                                        if (!this.IsTreatasZero && iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                        {
                                            writer.WriteLine(string.Empty);
                                            // Redmine id : 174639-174640-174641
                                            //if (originalData.IsIV)
                                            //{
                                            //    writer.WriteLine("*");
                                            //}
                                            //else if (originalData.IsNA)
                                            //{
                                            //    writer.WriteLine();
                                            //}
                                            //else if (originalData.GetType() == typeof(NData))
                                            //{
                                            //    writer.WriteLine((originalData as NData).Value);
                                            //}
                                            //else if (originalData.GetType() == typeof(FAData))
                                            //{ writer.WriteLine((originalData as FAData).Value); }
                                            //else if (originalData.GetType() == typeof(SAData))
                                            //{ writer.WriteLine((originalData as SAData).Value); }
                                            //else if (originalData.GetType() == typeof(MAData))
                                            //{

                                            //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                            //}
                                            originalData = null;
                                        }
                                        else
                                        {
                                            //191 27-12-19 -integrate issue +recode  after newsecorcriteria commented

                                            var data = ParentCollection.GetRawData(r, decimal.Parse(Questions[i].SourceItemId));
                                            if (this is DataProcessRecode && data.IsIV)
                                            {
                                                writer.WriteLine("*");
                                            }
                                            else if (this is DataProcessRecode && data.IsNA)//|| (this is DataProcessMConvert && data.IsNA)|| (this is DataProcessMConvert && data.IsIV)//2-1-2020 added mconvert  chrcking for  writing empty for *,NA
                                            {
                                                writer.WriteLine();
                                            }
                                            else
                                            {
                                                writer.WriteLine(new string(mabuf));
                                            }
                                        }
                                    }
                                }
                            }
                            writer.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    System.Diagnostics.Debug.Indent();
                    System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                    System.Diagnostics.Debug.Unindent();
                    throw;
                }
            }
        }

        /// <summary>
        /// Recodeのデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("4EFAEE01-7D3F-4dce-B11D-9254D30F26E9")]
        public class DataProcessRecode : DataProcessIntegrate
        {
            internal DataProcessRecode(DataProcesses collection) : base(collection, DataProcessCode.Recode) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
            }
        }

        /// <summary>
        /// MCONVERTのデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("9A06E6F9-5AC9-4b82-B701-0917176AEFAA")]
        public class DataProcessMConvert : DataProcessIntegrate
        {
            internal DataProcessMConvert(DataProcesses collection) : base(collection, DataProcessCode.MConvert) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
            }
        }

        /// <summary>
        /// CLASSのデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("A5B13E73-D122-4c42-8CC4-61CA80608B8B")]
        public class DataProcessClass : DataProcessIntegrate
        {
            internal DataProcessClass(DataProcesses collection) : base(collection, DataProcessCode.Class) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
            }

        }

        #region COUNT(SA)のデータ加工関連クラス
        /// <summary>
        /// COUNT(SA)のデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("4C5AFA4B-D1DE-497b-9E1D-169F2F0DFEA8")]
        public class DataProcessCategorizeCount : DataProcess
        {
            internal DataProcessCategorizeCount(DataProcesses collection) : base(collection, DataProcessCode.CategorizeResponseCount) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
            }

            /// <summary>
            /// データ加工を実行するメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Execute()//count here SA
            {
                int cnt = 0;
                int rangeIdx = 0;
                if (Questions == null) return;
                if (!RunFlag) return;
                if (ParentCollection == null || string.IsNullOrWhiteSpace(ParentCollection.DataDirectoryPath)) return;
                try
                {
                    for (int i = 0; i < Questions.Count; ++i)
                    {
                        if ((Questions[i].QuestionType & Tabulation.QuestionType.SA) != Tabulation.QuestionType.SA)
                        {
                            //新回答タイプがSA以外の場合、スキップする。
                            continue;
                        }
                        //191 
                        decimal orgItemId = decimal.Parse(Questions[i].ItemId);
                        Question.Questions.Question q = ParentCollection.GetQuestion(orgItemId);
                        bool iscriteria = false;
                        var replacedata = ParentCollection.GetRawData(decimal.Parse(Questions[0].ItemId));//Questions[0].SourceItemId //came null so cahnged to item id
                        //

                        //string path = System.IO.Path.Combine(ParentCollection.DataDirectoryPath, Questions[i].ItemId + ".dp");
                        GlobalsCommonConstant.fileExtension fileExt = ((NewQuestions.NewQuestion)Questions[i]).ChangeExtension;
                        string path = GetNewItemFilePath(i, fileExt);

                        //新カテゴリ個数範囲値を取得する。
                        List<List<NData.ValueRange>> rangList = ((INewQuestion)Questions[i]).CountSectorRange;//QC4: Changed List<Tabulation.NData.ValueRange> to List<List> to support non range values
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, false, Encoding.UTF8))
                        {

                            for (int r = 0; r < ParentCollection.SamplesCount; ++r)
                            {
                                //191 
                                Data originalData = ParentCollection.GetRawData(r, orgItemId);
                                int Sectorscount = Questions[i].Sectors.Count;
                                if (Questions[i].Sectors.Count > 1)//191  code 
                                {
                                    //check last sector criteria having sub criteria (for multiple criteria) or last sector having alias=1 for criteria presence
                                    // if (Questions[i].Sectors[Sectorscount - 1].Criteria != null)
                                    {
                                        INewVirtualQuestionSector s = (Questions[i].Sectors[Sectorscount - 1] as INewVirtualQuestionSector);
                                        if (s != null)
                                        {
                                            if (s.ModifyDataEdit == 0 && s.EditMethod == 0)
                                            {
                                                Sectorscount = Sectorscount - 1;//cannot do for add3 and other having more sectors
                                                iscriteria = true;

                                            }
                                        }
                                    }

                                    // GetQuestionIDforDP(Questions[Sectorscount].Sectors);
                                }
                                for (int j = 0; j < Sectorscount; ++j)// Questions[i].Sectors.Count
                                {
                                    //フィルタ後のbinValueを取得する。
                                    string filteredVal = Questions[i].Sectors[j].Criteria.GetCountResult(r);

                                    if (filteredVal == "*")
                                    {
                                        if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                        {
                                            //Redmine id : 175385
                                            writer.WriteLine(string.Empty);
                                            //if (originalData.IsIV)
                                            //{
                                            //    writer.WriteLine("*");
                                            //}
                                            //else if (originalData.IsNA)
                                            //{
                                            //    writer.WriteLine();
                                            //}
                                            //else if (originalData.GetType() == typeof(NData))
                                            //{
                                            //    writer.WriteLine((originalData as NData).Value);
                                            //}
                                            //else if (originalData.GetType() == typeof(FAData))
                                            //{ writer.WriteLine((originalData as FAData).Value); }
                                            //else if (originalData.GetType() == typeof(SAData))
                                            //{ writer.WriteLine((originalData as SAData).Value); }
                                            //else if (originalData.GetType() == typeof(MAData))
                                            //{

                                            //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                            //}
                                            originalData = null;
                                        }
                                        else
                                            writer.WriteLine("*");
                                    }
                                    else
                                    {
                                        //回答の個数を文字列→数字へ変換
                                        int.TryParse(filteredVal, out cnt);

                                        for (rangeIdx = 0; rangeIdx < rangList.Count; rangeIdx++)//QC4 : changed to List<List> to support non range values
                                        {
                                            List<NData.ValueRange> innerList = rangList[rangeIdx];
                                            bool bfound = false;
                                            foreach (NData.ValueRange param in innerList)
                                            {
                                                if (cnt >= param.MinValue && cnt <= param.MaxValue)
                                                {
                                                    bfound = true;
                                                    break;
                                                }
                                            }
                                            if (bfound)
                                                break;
                                        }

                                        if (rangeIdx < rangList.Count)
                                        {
                                            //191  criteria
                                            if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                            {
                                                //Redmine id : 175385
                                                writer.WriteLine(string.Empty);
                                                //if (originalData.IsIV)
                                                //{
                                                //    writer.WriteLine("*");
                                                //}
                                                //else if (originalData.IsNA)
                                                //{
                                                //    writer.WriteLine();
                                                //}
                                                //else if (originalData.GetType() == typeof(NData))
                                                //{
                                                //    writer.WriteLine((originalData as NData).Value);
                                                //}
                                                //else if (originalData.GetType() == typeof(FAData))
                                                //{ writer.WriteLine((originalData as FAData).Value); }
                                                //else if (originalData.GetType() == typeof(SAData))
                                                //{ writer.WriteLine((originalData as SAData).Value); }
                                                //else if (originalData.GetType() == typeof(MAData))
                                                //{

                                                //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                //}
                                                originalData = null;
                                            }
                                            else
                                                writer.WriteLine((rangeIdx + 1).ToString());// criteria code here
                                        }
                                        else
                                        {
                                            //範囲を超えた場合、無回答とする

                                            //191  criteria
                                            ////if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                            ////{
                                            ////    //  writer.WriteLine(string.Empty);
                                            ////    if (originalData.IsIV)
                                            ////    {
                                            ////        writer.WriteLine("*");
                                            ////    }
                                            ////    else if (originalData.IsNA)
                                            ////    {
                                            ////        writer.WriteLine();
                                            ////    }
                                            ////    else if (originalData.GetType() == typeof(NData))
                                            ////    {
                                            ////        writer.WriteLine((originalData as NData).Value);
                                            ////    }
                                            ////    else if (originalData.GetType() == typeof(FAData))
                                            ////    { writer.WriteLine((originalData as FAData).Value); }
                                            ////    else if (originalData.GetType() == typeof(SAData))
                                            ////    { writer.WriteLine((originalData as SAData).Value); }
                                            ////    else if (originalData.GetType() == typeof(MAData))
                                            ////    {

                                            ////        writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                            ////    }
                                            ////    originalData = null;
                                            ////}
                                            ////else
                                            writer.WriteLine(string.Empty);
                                        }
                                    }
                                }
                            }
                            writer.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    System.Diagnostics.Debug.Indent();
                    System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                    System.Diagnostics.Debug.Unindent();
                    throw;
                }
            }

        }

        #endregion

        #region MTOSのデータ加工関連クラス
        /// <summary>
        /// MTOSのデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("2C11265C-557C-439b-9C55-084DEE0E00D3")]
        public class DataProcessMtoS : DataProcess
        {
            internal DataProcessMtoS(DataProcesses collection) : base(collection, DataProcessCode.MtoS) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
            }

            /// <summary>
            /// データ加工を実行するメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Execute()
            {
                int index = 0;
                int[] indexes = null;
                int seed = Environment.TickCount;   //ランダム用シード生成する。
                Random cRandom = new System.Random(seed++);
                string cateNo = string.Empty;
                if (Questions == null) return;
                if (!RunFlag) return;
                int maxcategorycount = 0;
                if (ParentCollection == null || string.IsNullOrWhiteSpace(ParentCollection.DataDirectoryPath)) return;
                try
                {
                    // GetQuestionIDforDP(Questions[Sectorscount].Sectors);

                    for (int i = 0; i < Questions.Count; ++i)
                    {
                        //191  getting max categry count and cheking wgilw writing
                        decimal orgItemId = decimal.Parse(Questions[i].ItemId);
                        Question.Questions.Question q = ParentCollection.GetQuestion(orgItemId);
                        maxcategorycount = Questions[i].CategoryCount;

                        //191  for adding item id colum to dictionary
                        ////GetRawData
                        //var replacedata = ParentCollection.GetRawData(decimal.Parse(Questions[0].SourceItemId));//no need to put old data  so commntd
                        ////var replacedata = ParentCollection.GetRawData(decimal.Parse(Questions[0].SourceItemId));
                        ////
                        ////string path = System.IO.Path.Combine(ParentCollection.DataDirectoryPath, Questions[i].ItemId + ".dp");
                        GlobalsCommonConstant.fileExtension fileExt = ((NewQuestions.NewQuestion)Questions[i]).ChangeExtension;
                        string path = GetNewItemFilePath(i, fileExt);
                        bool iscriteria = false;
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, false, Encoding.UTF8))
                        {
                            for (int r = 0; r < ParentCollection.SamplesCount; ++r)
                            {
                                Data originalData = ParentCollection.GetRawData(r, orgItemId);
                                if ((Questions[i].QuestionType & Tabulation.QuestionType.SA) != Tabulation.QuestionType.SA)
                                {
                                    //新回答タイプがSA以外の場合、スキップする。
                                    continue;
                                }
                                int Sectorscount = Questions[i].Sectors.Count;
                                if (Questions[i].Sectors.Count > 1)//191  code 
                                {
                                    //check last sector criteria having sub criteria (for multiple criteria) or last sector having alias=1 for criteria presence
                                    Sectorscount = Sectorscount - 1;//cannot do for add3 and other having more sectors
                                    iscriteria = true;

                                    // GetQuestionIDforDP(Questions[Sectorscount].Sectors);
                                }
                                for (int j = 0; j < Sectorscount; ++j)//191  edited here -- for (int j = 0; j < Questions[i].Sectors.Count; ++j) --original code
                                {
                                    //フィルタ後のbinValueを取得する。
                                    string filteredVal = Questions[i].Sectors[j].Criteria.GetMtoSResultBinValue(r);

                                    if (filteredVal == "*")
                                    {
                                        //Redmine id -171313 added criteria
                                        if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//codeby 191  this if not in original 
                                        {
                                            //Redmine id -175383
                                            writer.WriteLine(string.Empty);
                                            //if (originalData.IsIV)
                                            //{
                                            //    writer.WriteLine("*");
                                            //}
                                            //else if (originalData.IsNA)
                                            //{
                                            //    writer.WriteLine();
                                            //}
                                            //else if (originalData.GetType() == typeof(NData))
                                            //{
                                            //    writer.WriteLine((originalData as NData).Value);
                                            //}
                                            //else if (originalData.GetType() == typeof(FAData))
                                            //{ writer.WriteLine((originalData as FAData).Value); }
                                            //else if (originalData.GetType() == typeof(SAData))
                                            //{ writer.WriteLine((originalData as SAData).Value); }
                                            //else if (originalData.GetType() == typeof(MAData))
                                            //{

                                            //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                            //}
                                            originalData = null;
                                            //SAData bcs mtos is to sa type for others need to chek

                                            // writer.WriteLine(0000);//need raw data here of  original item id
                                        }
                                        else
                                        {
                                            writer.WriteLine("*");
                                        }
                                    }
                                    else if (string.IsNullOrEmpty(filteredVal))
                                    {
                                        //Redmine id -171313 added criteria
                                        if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//codeby 191  this if not in original 
                                        {
                                            //Redmine id -175383
                                            writer.WriteLine(string.Empty);
                                            //if (originalData.IsIV)
                                            //{
                                            //    writer.WriteLine("*");
                                            //}
                                            //else if (originalData.IsNA)
                                            //{
                                            //    writer.WriteLine();
                                            //}
                                            //else if (originalData.GetType() == typeof(NData))
                                            //{
                                            //    writer.WriteLine((originalData as NData).Value);
                                            //}
                                            //else if (originalData.GetType() == typeof(FAData))
                                            //{
                                            //    writer.WriteLine((originalData as FAData).Value);
                                            //}
                                            //else if (originalData.GetType() == typeof(SAData))
                                            //{
                                            //    writer.WriteLine((originalData as SAData).Value);
                                            //}
                                            //else if (originalData.GetType() == typeof(MAData))
                                            //{

                                            //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                            //}
                                            originalData = null;
                                            //SAData bcs mtos is to sa type for others need to chek

                                            // writer.WriteLine(0000);//need raw data here of  original item id
                                        }
                                        else
                                        {
                                            writer.WriteLine(string.Empty);
                                        }
                                    }//criteria part here or in else satmnt  //&& (Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias=="1"
                                    else if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//codeby 191  this if not in original 
                                    {
                                        //Redmine id -175383
                                        writer.WriteLine(string.Empty);
                                        //if (originalData.IsIV)
                                        //{
                                        //    writer.WriteLine("*");
                                        //}
                                        //else if (originalData.IsNA)
                                        //{
                                        //    writer.WriteLine();
                                        //}
                                        //else if (originalData.GetType() == typeof(NData))
                                        //{
                                        //    writer.WriteLine((originalData as NData).Value);
                                        //}
                                        //else if (originalData.GetType() == typeof(FAData))
                                        //{ writer.WriteLine((originalData as FAData).Value); }
                                        //else if (originalData.GetType() == typeof(SAData))
                                        //{ writer.WriteLine((originalData as SAData).Value); }
                                        //else if (originalData.GetType() == typeof(MAData))
                                        //{

                                        //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                        //}
                                        originalData = null;
                                        //SAData bcs mtos is to sa type for others need to chek

                                        // writer.WriteLine(0000);//need raw data here of  original item id
                                    }
                                    else
                                    {//if (Questions[i].Sectors[j].Criteria != null && Questions[i].Sectors[j].Criteria.IsTrue(r))//for subcriteria
                                        //前優先
                                        if (((INewQuestion)Questions[i]).SelectedMethod == Common.GlobalsCommonConstant.MtoS_SelectMethod.BEFORE)
                                        {
                                            index = filteredVal.LastIndexOf('1');
                                            if (index < 0)
                                            {
                                                cateNo = string.Empty;
                                            }
                                            else
                                            {
                                                cateNo = (filteredVal.Length - index) + string.Empty;
                                            }
                                            int catno = null == cateNo ? 0 : string.IsNullOrEmpty(cateNo) ? 0 : Convert.ToInt32(cateNo);
                                            if (catno > maxcategorycount) cateNo = string.Empty;
                                        }
                                        //後優先
                                        else if (((INewQuestion)Questions[i]).SelectedMethod == Common.GlobalsCommonConstant.MtoS_SelectMethod.AFTER)
                                        {
                                            //index = filteredVal.IndexOf('1');
                                            //if (index < 0)
                                            //{
                                            //    cateNo = string.Empty;
                                            //}
                                            //else
                                            //{
                                            //    cateNo = (filteredVal.Length - index) + string.Empty;
                                            //}
                                            int catno = 0;
                                            index = 0;
                                            int startIdx = 0;
                                            do
                                            {
                                                index = filteredVal.IndexOf('1', startIdx);
                                                if (index < 0)
                                                {
                                                    cateNo = string.Empty;
                                                }
                                                else
                                                {
                                                    cateNo = (filteredVal.Length - index) + string.Empty;
                                                }
                                                catno = null == cateNo ? 0 : string.IsNullOrEmpty(cateNo) ? 0 : Convert.ToInt32(cateNo);
                                                startIdx = index + 1;
                                            } while (catno > maxcategorycount && catno > 0);
                                        }
                                        //ランダム
                                        else if (((INewQuestion)Questions[i]).SelectedMethod == Common.GlobalsCommonConstant.MtoS_SelectMethod.RANDOM)
                                        {
                                            //初期化
                                            Array.Resize<int>(ref indexes, 0);
                                            index = 0;
                                            int startIdx = 0;
                                            //回答ありのすべてを配列に格納する。
                                            while (index >= 0)
                                            {
                                                index = filteredVal.IndexOf('1', startIdx);
                                                if (index >= 0)
                                                {
                                                    Array.Resize<int>(ref indexes, (indexes == null ? 1 : indexes.Length + 1));
                                                    indexes[indexes.Length - 1] = index;
                                                }
                                                startIdx = index + 1;
                                            }

                                            if (indexes.Length <= 0)
                                            {
                                                cateNo = string.Empty;
                                            }
                                            else
                                            {
                                                if (indexes.Length == 1)
                                                {
                                                    cateNo = (filteredVal.Length - indexes[0]) + string.Empty;
                                                }
                                                else
                                                {
                                                    int cr = cRandom.Next(indexes.Length);
                                                    cateNo = (filteredVal.Length - indexes[cr]) + string.Empty;
                                                }
                                            }
                                        }
                                        //  int catval = string.IsNullOrEmpty(cateNo) == true ? 0 : Convert.ToInt32(cateNo);
                                        //  if (catval > maxcategorycount) cateNo = ""; //= maxcategorycount.ToString();
                                        writer.WriteLine(cateNo);
                                    }

                                }
                            }
                            writer.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    System.Diagnostics.Debug.Indent();
                    System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                    System.Diagnostics.Debug.Unindent();
                    throw;
                }
            }
        }
        #endregion

        /// <summary>
        /// データ削除のデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("EFB51ECD-CBC3-4ffe-8765-8B510A99B3B3")]
        public class DataProcessDeleteData : DataProcessIntegrate
        {
            internal DataProcessDeleteData(DataProcesses collection) : base(collection, DataProcessCode.DeleteData) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
            }

            /// <summary>
            /// データ加工を実行するメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Execute()
            {
                if (Questions == null) return;
                if (!RunFlag) return;
                if (ParentCollection == null || string.IsNullOrWhiteSpace(ParentCollection.DataDirectoryPath)) return;
                int sectorcount = 0;
                bool isCriteria = false;
                try
                {
                    for (int i = 0; i < Questions.Count; ++i)
                    {
                        string path = System.IO.Path.Combine(ParentCollection.DataDirectoryPath, DataIoConstant.DELETE_FLAG_TEXT_FILE_NAME);
                        GlobalsCommonConstant.fileExtension fileExt = ((NewQuestions.NewQuestion)Questions[i]).ChangeExtension;
                        //  string path = GetNewItemFilePath(i, fileExt);
                        // 作成済み削除フラグファイルの読み込み
                        bool[] deleteFlagArray = null;
                        if (System.IO.File.Exists(path))
                        {
                            QCWebException ex = null;
                            deleteFlagArray = ReadTextFile.ReadDeleteFlag(path, out ex);
                        }
                        sectorcount = Questions[i].Sectors.Count;
                        if (sectorcount > 1)
                        { sectorcount = 1; isCriteria = true; }

                        //TODO #297101 ここで処理対象がLDEL（条件アイテムがSAMPLEIDのみかつ、=の条件のみ）であるか判定しフラグをたてる。
                        //TODO 補足：Sectorsのインデックスは上記でsectorcountを強制１にしている通り、下記でも１つ分しか参照していない。よってindex0固定でみる。
                        bool ldel_flag = true;
                        if (Questions[i].Sectors[0].Criteria.SubCriterias != null)
                        {
                            for (int subCidx = 0; subCidx < Questions[i].Sectors[0].Criteria.SubCriterias.Count; subCidx++)
                            {
                                if (Questions[i].Sectors[0].Criteria.SubCriterias[subCidx].CriteriaOperatorDescription != "=" ||
                                    Questions[i].Sectors[0].Criteria.SubCriterias[subCidx].QuestionIDforDP != 0 ||
                                    Questions[i].Sectors[0].Criteria.SubCriterias[subCidx].CriteriaValueDescription.Contains("-") ||    //範囲値指定
                                    Questions[i].Sectors[0].Criteria.SubCriterias[subCidx].CriteriaValueDescription.Contains(",") ||    //複数値指定
                                    Questions[i].Sectors[0].Criteria.SubCriterias[subCidx].CriteriaValueDescription.Contains("/") ||    //複数値指定
                                    Questions[i].Sectors[0].Criteria.SubCriterias[subCidx].CriteriaValueDescription.Contains("@"))      // データ加工シート側で条件値にアイテム名指定した場合に含まれる
                                {
                                    ldel_flag = false;
                                    break;
                                }
                            }
                        }

                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, false, Encoding.UTF8))
                        {
                            HashSet<string> set = new HashSet<string>();
                            bool first = true;
                            for (int r = 0; r < ParentCollection.SamplesCount; ++r)
                            {
                                bool wFlag = false;
                                for (int j = 0; j < sectorcount; ++j)//Questions[i].Sectors.Count
                                {
                                    // #268452 - (PR)データ加工「LDEL」の実行速度がQC3と比べて非常に遅い
                                    //   SubCriterias.Countが１より大きい、(#297101)かつ LDEL & 条件がSAMPLEID = の場合のみLDELシート・LDELファイル用の特殊処理を行う。
                                    //   条件1つの指定のDELETEなど、SubCriteriasがnullの場合もあるのでその場合はスルー,
                                    //   ProのLDELで条件が入力されている場合、条件をスルーすることで合意済み
                                    if (Questions[i].Sectors[j].Criteria.SubCriterias != null && Questions[i].Sectors[j].Criteria.SubCriterias.Count > 1 && ldel_flag == true)
                                    {
                                        if (first)
                                        {
                                            // 初回は検索用ハッシュセットを作る
                                            for (int subCidx = 0; subCidx < Questions[i].Sectors[j].Criteria.SubCriterias.Count; subCidx++)
                                            {
                                                set.Add(Questions[i].Sectors[j].Criteria.SubCriterias[subCidx].CriteriaValueDescription);
                                            }
                                            first = false;
                                        }
                                        // SAMPLEIDの対象行の値取得(N or FAなのでどちらにも対応)
                                        var targetData = Questions[i].Sectors[j].Criteria.ParentDataProcess.ParentCollection.GetRawData(r, 0);
                                        string sval = "";
                                        if (targetData is NData)
                                        {
                                            sval = Convert.ToString(((NData)targetData).Value);
                                        }
                                        else if (targetData is FAData)
                                        {
                                            sval = ((FAData)targetData).Value;
                                        }

                                        // SAMPLEIDの値がLDELの条件ハッシュセットに含まれる値と一致した箇所に削除フラグを立てる
                                        if (set.Contains(sval))
                                        {
                                            writer.WriteLine("1");
                                        }
                                        else
                                        {
                                            writer.WriteLine("0");
                                        }
                                        wFlag = true;
                                    }
                                    else if (Questions[i].Sectors[j].Criteria != null && Questions[i].Sectors[j].Criteria.IsTrue(r))
                                    //if (Questions[i].Sectors[j].Criteria != null && Questions[i].Sectors[j].Criteria.IsTrue(r))
                                    {
                                        if (Questions[i].Sectors.Count == 1)//delete and  ldel without criteria
                                        {
                                            writer.WriteLine((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias);//need to check second sector for criteria
                                        }
                                        else //ldel with criteria
                                        {
                                            if (Questions[i].Sectors[j + 1].Criteria != null && Questions[i].Sectors[j + 1].Criteria.IsTrue(r))
                                            {
                                                writer.WriteLine((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias);
                                            }
                                            else { writer.WriteLine("0"); }
                                        }
                                        wFlag = true;
                                        break;

                                    }
                                }

                                if (!wFlag)
                                {
                                    if (deleteFlagArray != null)
                                    {
                                        writer.WriteLine(deleteFlagArray[r] ? "1" : "0");
                                    }
                                    else
                                    {
                                        writer.WriteLine("0");
                                    }
                                }
                            }
                            writer.Close();
                        }
                    }
                    //filterResult情報を削除する。
                    ParentCollection.FilterResultDataClear();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    System.Diagnostics.Debug.Indent();
                    System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                    System.Diagnostics.Debug.Unindent();
                    throw;
                }
            }
        }

        /// <summary>
        /// データ修正のデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("B1B4169C-B787-4986-88BD-CA8337B551E4")]
        public class DataProcessModifyData : DataProcessIntegrate
        {
            internal DataProcessModifyData(DataProcesses collection) : base(collection, DataProcessCode.ModifyData) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
            }

            private bool IsAnVar(string itemid, System.Data.SQLite.SQLiteConnection con)
            {
                return DBHelper.GetDataTable("select question_flag from question where id = " + itemid, con).Rows[0][0].ToString() == "An";
            }
            /// <summary>
            /// データ加工を実行するメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Execute()
            {
                if (Questions == null) return;
                if (!RunFlag) return;
                if (ParentCollection == null || string.IsNullOrWhiteSpace(ParentCollection.DataDirectoryPath)) return;

                try
                {
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    //string[] sourceDivArray = new string[] { CDef.SourceDiv.Original.Code, CDef.SourceDiv.DataEdit.Code };
                    //Question.Questions questions = new Question.Questions(ParentCollection.QcWebId, sourceDivArray);
                    //System.Diagnostics.Debug.WriteLine("DataProcessModifyData#Questions " + sw.ElapsedMilliseconds + "ms");

                    //disctionary for join
                    Dictionary<string, string> Join_Dict = new Dictionary<string, string>();
                    Dictionary<string, string> add11_Dict = new Dictionary<string, string>();
                    List<string> joint_List = new List<string>();
                    string[,] joint_array = null;
                    for (int i = 0; i < Questions.Count; ++i)
                    {
                        string path = GetNewItemFilePath(i);
                        decimal orgItemId = decimal.Parse(Questions[i].ItemId);
                        //Question.Questions.Question q = (Question.Questions.Question)questions[orgItemId];
                        Question.Questions.Question q = ParentCollection.GetQuestion(orgItemId);

                        //191 
                        bool iscriteria = false;
                        int arraylength = 0;
                        var replacedata = ParentCollection.GetRawData(decimal.Parse(Questions[0].ItemId));//Questions[0].SourceItemId //came null so cahnged to item id

                        //最初の一回目は***.dpファイルが存在していないので、***.txtを先に読み込んでおく
                        //***.dpファイルが存在する場合も予め読み込んでおく
                        //StreamWriterとReadData2が同時に***.dpファイルにアクセスするのを防ぐため
                        /*
                        if (!System.IO.File.Exists(path)) {
                            var extTxtPath = GetNewItemFilePath(i, GlobalsCommonConstant.fileExtension.txt);
                            QCWebException exception;
                            List<Data> extTxtData = ReadTextFile.ReadData(extTxtPath, q.QuestionType, out exception);
                            ParentCollection.SetRawData(orgItemId, extTxtData);
                        } else {
                            QCWebException exception;
                            List<Data> extTxtData = ReadTextFile.ReadData(path, q.QuestionType, out exception);
                            ParentCollection.SetRawData(orgItemId, extTxtData);
                        }
                        */
                        string extTxtPath = path;
                        if (!System.IO.File.Exists(extTxtPath))
                        {
                            extTxtPath = GetNewItemFilePath(i, GlobalsCommonConstant.fileExtension.dp);
                        }
                        QCWebException exception;
                        List<Data> extTxtData = null;//ReadTextFile.ReadData(extTxtPath, QuestionType.MA, out exception);
                        using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(ParentCollection.ConnectionString))
                        {

                            con.Open();
                            System.Data.DataTable dataTble = null;
                            //where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + QcWebCommon.Common.Constants.MAX_ROW_COUNT.ToString()


                            path = Path.Combine(DataProcessCommon.GetProcessIdPath() + @"\", orgItemId.ToString() + ".dp");
                            //  var dpPath = Path.Combine(Path.GetTempPath(), @"QC4\" + orgItemId.ToString() + ".dp");
                            List<Data> dataList = new List<Data>();
                            if ((File.Exists(path) /*|| File.Exists(dpPath)*/))
                            {

                                string filePath = path;// File.Exists(path) ? path : dpPath;
                                FileInfo info = new FileInfo(filePath);
                                if (info.Length > 0)
                                {
                                    try
                                    {
                                        QuestionType QType = QuestionType.SA;

                                        {
                                            //con.Open();

                                            string ansType = DBHelper.GetAnswertype(orgItemId, con);
                                            switch (ansType)
                                            {
                                                case "MA":
                                                    QType = QuestionType.MA;
                                                    break;
                                                case "SA":
                                                    QType = QuestionType.SA;
                                                    break;
                                                case "N":
                                                    QType = QuestionType.N;
                                                    break;
                                                case "FA":
                                                    QType = QuestionType.FA;
                                                    break;

                                            }

                                        }
                                        extTxtData = ReadTextFile.ReadData(filePath, QType, null, out exception, false);

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }

                            }
                            else
                            {
                                if (orgItemId == 0)//191  added for sample id
                                {
                                    dataTble = DBHelper.GetDataTable("Select sample_id from data_after_process where sort_no > " + ParentCollection.SortNumber + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - ParentCollection.deletetedrowscount).ToString(), con);
                                }
                                else
                                {
                                    if (IsAnVar(orgItemId.ToString(), con))
                                        dataTble = DBHelper.GetDataTable("Select m.q_" + orgItemId.ToString() + " from multivariate_temp m join data_after_process a on m.sort_no = a.sort_no where m.sort_no > " + ParentCollection.SortNumber + " order by m.sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - ParentCollection.deletetedrowscount).ToString(), con);//191 
                                    else
                                        dataTble = DBHelper.GetDataTable("Select q_" + orgItemId.ToString() + " from data_after_process where sort_no > " + ParentCollection.SortNumber + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - ParentCollection.deletetedrowscount).ToString(), con);//191 

                                }
                                QuestionType questiontype = DBHelper.GetAnswertype(orgItemId, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(orgItemId, con) == "SA" ? QuestionType.SA : DBHelper.GetAnswertype(orgItemId, con) == "N" ? QuestionType.N : QuestionType.FA);
                                //try
                                //{
                                //    INewVirtualQuestionSector tempsector = (Questions[i].Sectors[0] as INewVirtualQuestionSector);
                                //    try { if (tempsector.EditMethod == EditMethod.SUBSTITUTION && questiontype != QuestionType.MA) questiontype = QuestionType.FA; }
                                //    catch { }
                                //}
                                //catch { }

                                extTxtData = ReadTextFile.ReadDataTable(dataTble, questiontype, null, out exception, false);
                                if (exception != null) throw exception;
                            }
                            con.Close();

                        }



                        if (extTxtData == null)
                        {
                            exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceParameterMessageIndex)
                                            , GlobalsCommonConstant.LogLevel.FATAL
                                            , "path");
                            throw exception;
                        }
                        ParentCollection.SetRawData(orgItemId, extTxtData);
                        Dictionary<string, string> temp_join_Dict = new Dictionary<string, string>();   //make temp dict here 
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, false, Encoding.UTF8))
                        {
                            for (int r = 0; r < ParentCollection.SamplesCount; ++r)
                            {
                                //191 
                                int Sectorscount = Questions[i].Sectors.Count;
                                if (Questions[i].Sectors.Count > 1)//191  code 
                                {
                                    //check last sector criteria having sub criteria (for multiple criteria) or last sector having alias=1 for criteria presence
                                    // if (Questions[i].Sectors[Sectorscount - 1].Criteria != null)
                                    {
                                        INewVirtualQuestionSector s = (Questions[i].Sectors[Sectorscount - 1] as INewVirtualQuestionSector);
                                        if (s != null)
                                        {
                                            if (s.ModifyDataEdit == 0 && s.EditMethod == 0)
                                            {
                                                Sectorscount = Sectorscount - 1;//cannot do for add3 and other having more sectors
                                                iscriteria = true;
                                            }
                                        }
                                    }




                                    // GetQuestionIDforDP(Questions[Sectorscount].Sectors);
                                }



                                //setting dictionary for joint
                                if (r < 1)
                                {
                                    int dictcount = 0;
                                    int row = 0;
                                    int dictarraylength = 0;
                                    //added for add3 issue 09-11-19
                                    try
                                    { dictarraylength = Questions[i].CategoryCount; }
                                    catch { }



                                    for (int j = 0; j < Sectorscount; ++j)
                                    {
                                        if (Questions[i].Sectors[j].Criteria != null)
                                        {
                                            dictarraylength += Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                        }
                                    }
                                    joint_array = new string[dictarraylength, 2];

                                    for (int j = 0; j < Sectorscount; ++j)
                                    {
                                        if (Questions[i].Sectors[j].Criteria != null)
                                        {
                                            //need to add new logic here for mixed symbols
                                            //make list
                                            bool isnot = false;
                                            List<string> commasep = new List<string>();
                                            List<string> barsep = new List<string>();
                                            List<string> minsep = new List<string>();
                                            List<int> exclidelist = new List<int>();
                                            //split with ','
                                            string[] criteriacommavalues = Questions[i].Sectors[j].Criteria.CriteriaValueDescription.Split(',');
                                            try
                                            {
                                                if (criteriacommavalues.Length >= 1)
                                                {
                                                    if (criteriacommavalues[0].StartsWith("!") || criteriacommavalues[0].StartsWith("<>"))
                                                    {
                                                        isnot = true;
                                                    }
                                                }
                                            }
                                            catch { }
                                            foreach (string str in criteriacommavalues)
                                            {
                                                commasep.Add(str);//add whole to  list
                                            }
                                            // for each nd split with '/'
                                            foreach (string str in commasep)
                                            {
                                                if (str.Contains('/'))
                                                {
                                                    string[] criteriabarvalues = str.Split('/');
                                                    foreach (string s in criteriabarvalues)
                                                    {
                                                        barsep.Add(s);//add whole to list
                                                    }
                                                }
                                                else
                                                    barsep.Add(str);
                                            }

                                            //chek for '-'
                                            foreach (string str in barsep)
                                            {
                                                //if contains ! or <>
                                                if (isnot)//str.StartsWith("!") || str.StartsWith("<>")
                                                {
                                                    string notvalue = str;
                                                    //need to remove the items from list and add other category numbers
                                                    // criteriaValueDescription = criteriaValueDescription.TrimStart('!');
                                                    if (str.StartsWith("!")) notvalue = str.TrimStart('!');
                                                    else if (str.StartsWith("<>")) notvalue = str.Replace("<>", "");
                                                    //criteriaValueDescription = criteriaValueDescription.Replace("<>", "");//TrimStart('<>');
                                                    int criteriabeginning = 1;
                                                    int criteriaend = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                                    if (notvalue.Contains('-'))//str.Contains('-')
                                                    {
                                                        int strt = 0, end = 0;
                                                        string[] criterisplitvals = notvalue.Split('-');

                                                        if (criterisplitvals.Length == 1)
                                                        {
                                                            try
                                                            {
                                                                strt = Convert.ToInt32(criterisplitvals[0]);
                                                            }
                                                            catch (Exception e) { strt = 1; }
                                                            end = strt;

                                                        }
                                                        else
                                                        {
                                                            try
                                                            {
                                                                strt = Convert.ToInt32(criterisplitvals[0]);
                                                            }
                                                            catch (Exception e) { strt = 1; }
                                                            try
                                                            {
                                                                end = Convert.ToInt32(criterisplitvals[1]);
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                end = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);

                                                            }
                                                        }

                                                        for (int ci = strt; ci <= end; ci++)
                                                        {
                                                            exclidelist.Add(ci);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            exclidelist.Add(Convert.ToInt32(notvalue));
                                                        }
                                                        catch { }
                                                    }


                                                }
                                                else
                                                {
                                                    //else
                                                    if (str.Contains('-'))
                                                    {

                                                        int start = 0, limit = 0;
                                                        string[] criteriaminvalues = str.Split('-');
                                                        // foreach (string s in criteriaminvalues)
                                                        {

                                                            try
                                                            {

                                                                if (criteriaminvalues.Length == 1)
                                                                {
                                                                    try
                                                                    {
                                                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                                                    }
                                                                    catch (Exception e) { start = 1; }
                                                                    limit = start;
                                                                }
                                                                else
                                                                {
                                                                    try
                                                                    {
                                                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                                                    }
                                                                    catch (Exception e) { start = 1; }//actually get min value of answer
                                                                    try
                                                                    {
                                                                        limit = Convert.ToInt32(criteriaminvalues[1]);
                                                                    }
                                                                    catch (Exception e)
                                                                    {//actually get max value of answer;need to get max of choice no from item id and set limit
                                                                        limit = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);

                                                                    }
                                                                }
                                                                if (limit < start)//need to reverse if 9-7 comes
                                                                {
                                                                    int temp = limit;
                                                                    limit = start;
                                                                    start = temp;
                                                                }
                                                            }
                                                            catch { }

                                                            for (int ci = start; ci <= limit; ci++)
                                                            {
                                                                minsep.Add(ci.ToString());//add whole to list
                                                            }
                                                        }
                                                    }
                                                    else
                                                        minsep.Add(str);
                                                }
                                            }
                                            if (isnot)
                                            {
                                                for (int ci = 1; ci <= Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount); ci++)
                                                {
                                                    if (!exclidelist.Contains(ci))
                                                        minsep.Add(ci.ToString());
                                                }
                                            }

                                            foreach (string str in minsep)
                                            {
                                                dictcount++;
                                                // int val = Convert.ToInt32(ci);
                                                if (!Join_Dict.ContainsKey((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + str))
                                                    Join_Dict.Add((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + str, dictcount.ToString());
                                                joint_array[row, 0] = (Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + str;
                                                joint_array[row, 1] = dictcount.ToString();
                                                row++;
                                            }
                                            isnot = false;
                                        }
                                    }
                                    arraylength = joint_array.GetLength(0);// Join_Dict.Count;
                                }
                                Data originalData = ParentCollection.GetRawData(r, orgItemId);

                                if (arraylength == 0)
                                    arraylength = Sectorscount;
                                string[] add3AppendArray = new string[arraylength];//array leng must be the no of count of all values  from sa,ma na fa variable.// string[] add3AppendArray = new string[Questions[i].Sectors.Count]; //original
                                for (int arrainit = 0; arrainit < arraylength; arrainit++)
                                { add3AppendArray[arrainit] = "0"; }
                                int add3pointr = 0;//looping array for add3 & joint
                                for (int j = 0; j < Sectorscount; ++j)// for (int j = 0; j < Questions[i].Sectors.Count; ++j)
                                {
                                    // add3AppendArray[j] = "0";//commented for add3     //191  added for join
                                    if ((Questions[i].Sectors[j].Criteria == null) ||
                                        (Questions[i].Sectors[j].Criteria != null && Questions[i].Sectors[j].Criteria.IsTrue(r)))
                                    {
                                        INewVirtualQuestionSector sector = (Questions[i].Sectors[j] as INewVirtualQuestionSector);


                                        string alias = sector.Alias;
                                        string[] categoryArray = null;
                                        switch (sector.ModifyDataEdit)
                                        {
                                            case ModifyDataEdit.CATEGORY:
                                                #region カテゴリ指定
                                                if ((q.QCAnswerType & Question.QCAnswerType.N) == Question.QCAnswerType.N)
                                                {
                                                    // Nアイテムに対してカテゴリ指定はできません
                                                    throw new QCWebException("QCCMN05010001", GlobalsCommonConstant.LogLevel.FATAL, null);
                                                }
                                                if ((q.QCAnswerType & Question.QCAnswerType.FA) == Question.QCAnswerType.FA)
                                                {
                                                    // FAアイテムに対してカテゴリ指定はできません
                                                    throw new QCWebException("QCCMN05010002", GlobalsCommonConstant.LogLevel.FATAL, null);
                                                }

                                                categoryArray = alias.Split(',');
                                                if (sector.EditMethod == EditMethod.SUBSTITUTION)
                                                {
                                                    #region 代入
                                                    if ((q.QuestionType & QuestionType.SA) == QuestionType.SA)
                                                    {
                                                        if (categoryArray.Length > 1)
                                                        {
                                                            // SAアイテムに対して修正方法が不正です。{0}
                                                            throw new QCWebException("QCCMN05010008"
                                                                                     , new string[] { sector.EditMethod.GetHashCode().ToString() }
                                                                                     , GlobalsCommonConstant.LogLevel.FATAL
                                                                                     , null);
                                                        }
                                                        //191  criteria
                                                        if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                        {
                                                            //writer.WriteLine(string.Empty);// changed for correct data custmr bug
                                                            if (originalData.IsIV)
                                                            {
                                                                writer.WriteLine("*");
                                                            }
                                                            else if (originalData.IsNA)
                                                            {
                                                                writer.WriteLine();
                                                            }
                                                            else if (originalData.GetType() == typeof(NData))
                                                            {
                                                                writer.WriteLine((originalData as NData).Value);
                                                            }
                                                            else if (originalData.GetType() == typeof(FAData))
                                                            { writer.WriteLine(System.Text.RegularExpressions.Regex.Escape((originalData as FAData).Value)); }//[Redmine id : 174859] -
                                                            else if (originalData.GetType() == typeof(SAData))
                                                            { writer.WriteLine((originalData as SAData).Value); }
                                                            else if (originalData.GetType() == typeof(MAData))
                                                            {

                                                                writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                            }
                                                            originalData = null;
                                                        }
                                                        else
                                                        {
                                                            if (categoryArray[0].Equals("*")) { writer.WriteLine("*"); }
                                                            else if (categoryArray[0].Equals("DK") || categoryArray[0].Equals(string.Empty)) { writer.WriteLine(); }
                                                            else writer.WriteLine(DataProcessSubstitutionSA(q, originalData, categoryArray[0]));

                                                            originalData = null;
                                                        }

                                                        break;
                                                    }

                                                    if ((q.QuestionType & QuestionType.MA) == QuestionType.MA)
                                                    {
                                                        //191  criteria
                                                        if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                        {
                                                            if (originalData.IsIV)
                                                            {
                                                                writer.WriteLine("*");
                                                            }
                                                            else if (originalData.IsNA)
                                                            {
                                                                writer.WriteLine();
                                                            }
                                                            else if (originalData.GetType() == typeof(NData))
                                                            {
                                                                writer.WriteLine((originalData as NData).Value);
                                                            }
                                                            else if (originalData.GetType() == typeof(FAData))
                                                            { writer.WriteLine(System.Text.RegularExpressions.Regex.Escape((originalData as FAData).Value)); }//[Redmine id : 174859] -// { writer.WriteLine((originalData as FAData).Value); }
                                                            else if (originalData.GetType() == typeof(SAData))
                                                            { writer.WriteLine((originalData as SAData).Value); }
                                                            else if (originalData.GetType() == typeof(MAData))
                                                            {

                                                                writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                            }
                                                            //  writer.WriteLine((originalData as FAData).Value); //191 customer need old value for correctdata// writer.WriteLine(string.Empty);
                                                            originalData = null;
                                                        }
                                                        else
                                                        {
                                                            if (categoryArray[0].Equals("*")) { writer.WriteLine("*"); }
                                                            else if (categoryArray[0].Equals("DK") || categoryArray[0].Equals(string.Empty)) { writer.WriteLine(); }
                                                            else writer.WriteLine(DataProcessSubstitutionMA(q, originalData, categoryArray));

                                                            originalData = null;
                                                        }

                                                        break;
                                                    }
                                                    #endregion
                                                }
                                                if (sector.EditMethod == EditMethod.APPEND)
                                                {
                                                    #region 追加
                                                    if ((q.QuestionType & QuestionType.SA) == QuestionType.SA)
                                                    {
                                                        // SAアイテムに対してカテゴリ指定はできません
                                                        throw new QCWebException("QCCMN05010003", GlobalsCommonConstant.LogLevel.FATAL, null);
                                                    }
                                                    //191 
                                                    if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                    {
                                                        //191  added code for keeping old data in cell and it is from MA
                                                        MAData mad = (originalData as MAData);
                                                        if (mad.IsNA)
                                                        {
                                                            writer.WriteLine();
                                                        }
                                                        else if (mad.IsIV)
                                                        {
                                                            writer.WriteLine("*");
                                                        }
                                                        else
                                                        {
                                                            writer.WriteLine(mad.BinValue(q.Sectors.Count));
                                                        }
                                                        // writer.WriteLine((replacedata[r] as SAData).Value.ToString());// writer.WriteLine(string.Empty);//need to write old data ////  writer.WriteLine((replacedata[r] as SAData).Value.ToString());
                                                        originalData = null;
                                                    }
                                                    else
                                                    {
                                                        MAData mad = (originalData as MAData);//added for ADD2 * Bug 191 code
                                                        //if (mad.IsNA)//commnted bcs new variable will b blank and cannot add value 
                                                        //{
                                                        //    writer.WriteLine();
                                                        //}
                                                        //else
                                                        if (mad.IsIV)//added for ADD2 * Bug 191 code

                                                        {
                                                            writer.WriteLine("*");
                                                        }
                                                        else
                                                        {
                                                            writer.WriteLine(DataProcessAppendMA(q, originalData, categoryArray));
                                                        }
                                                        originalData = null;
                                                    }
                                                    break;
                                                    #endregion
                                                }
                                                if (sector.EditMethod == EditMethod.REMOVE)
                                                {
                                                    #region 除外
                                                    if ((q.QuestionType & QuestionType.SA) == QuestionType.SA)
                                                    {
                                                        // SAアイテムに対して修正方法が不正です。{0}
                                                        throw new QCWebException("QCCMN05010008"
                                                                                 , new string[] { sector.EditMethod.GetHashCode().ToString() }
                                                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                                                 , null);
                                                    }
                                                    //191 
                                                    if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                    {
                                                        //191  added code for keeping old data in cell and it is from MA
                                                        MAData mad = (originalData as MAData);
                                                        if (mad.IsNA)
                                                        {
                                                            writer.WriteLine();
                                                        }
                                                        else if (mad.IsIV)
                                                        {
                                                            writer.WriteLine("*");
                                                        }
                                                        else
                                                        {
                                                            writer.WriteLine(mad.BinValue(q.Sectors.Count));
                                                        }
                                                        // writer.WriteLine((replacedata[r] as SAData).Value.ToString());// writer.WriteLine(string.Empty);//need to write old data ////  writer.WriteLine((replacedata[r] as SAData).Value.ToString());
                                                        originalData = null;
                                                    }
                                                    else
                                                        writer.WriteLine(DataProcessRemoveMA(q, originalData, categoryArray));
                                                    originalData = null;
                                                    break;
                                                    #endregion
                                                }
                                                #endregion
                                                break;
                                            case ModifyDataEdit.ITEM:
                                                #region アイテム指定
                                                decimal itemid = 0;
                                                if (!decimal.TryParse(alias, out itemid))
                                                {
                                                    // アイテムIDが不正です。{0} 
                                                    throw new QCWebException("QCCMN05010009"
                                                                             , new string[] { alias }
                                                                             , GlobalsCommonConstant.LogLevel.FATAL
                                                                             , null);
                                                }

                                                // 選択肢情報の取得
                                                Data replaceData = ParentCollection.GetRawData(r, itemid);
                                                if (replaceData.GetType() == typeof(SAData))
                                                {
                                                    //if (sector.EditMethod != EditMethod.SUBSTITUTION) {
                                                    //    // SAアイテムに対して修正方法が不正です。{0}
                                                    //    throw new QCWebException("QCCMN05010008"
                                                    //                             , new string[] { sector.EditMethod.GetHashCode().ToString() }
                                                    //                             , GlobalsCommonConstant.LogLevel.FATAL
                                                    //                             , null);
                                                    //}

                                                    //Mantis2533 add
                                                    switch (sector.EditMethod)
                                                    {
                                                        case EditMethod.SUBSTITUTION:
                                                            //Redmine id : 174576
                                                            //if (replaceData.IsIV)   //https://redmine.macromill.com/issues/173954 --need to remove isiv,isna
                                                            //{
                                                            //    writer.WriteLine("*");
                                                            //}
                                                            //else if (replaceData.IsNA)
                                                            //{
                                                            //    writer.WriteLine();
                                                            //}
                                                            //else
                                                            {
                                                                if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                                {
                                                                    //  writer.WriteLine(string.Empty);//need to fill old data ////  writer.WriteLine((replacedata[r] as SAData).Value.ToString());
                                                                    if (originalData.IsIV)
                                                                    {
                                                                        writer.WriteLine("*");
                                                                    }
                                                                    else if (originalData.IsNA)
                                                                    {
                                                                        writer.WriteLine();
                                                                    }
                                                                    else if (originalData.GetType() == typeof(NData))
                                                                    {
                                                                        writer.WriteLine((originalData as NData).Value);
                                                                    }
                                                                    else if (originalData.GetType() == typeof(FAData))
                                                                    { writer.WriteLine(System.Text.RegularExpressions.Regex.Escape((originalData as FAData).Value)); }//[Redmine id : 174859] -// { writer.WriteLine((originalData as FAData).Value); }
                                                                    else if (originalData.GetType() == typeof(SAData))
                                                                    { writer.WriteLine((originalData as SAData).Value); }
                                                                    else if (originalData.GetType() == typeof(MAData))
                                                                    {

                                                                        writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                                    }
                                                                    originalData = null;
                                                                }
                                                                else
                                                                {//need to chek here
                                                                 //Redmine id : 174576
                                                                    if (replaceData.IsIV)   //https://redmine.macromill.com/issues/173954 
                                                                    {
                                                                        writer.WriteLine("*");
                                                                    }
                                                                    else if (replaceData.IsNA)
                                                                    {
                                                                        writer.WriteLine();
                                                                    }
                                                                    else
                                                                        writer.WriteLine(DataProcessSubstitutionSA(q, originalData
                                                                                 , (replaceData as SAData).Value.ToString()));
                                                                }
                                                            }
                                                            originalData = null;
                                                            break;
                                                        case EditMethod.APPEND:
                                                            if ((!replaceData.IsIV && !replaceData.IsNA) || Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount) != -1)
                                                            {
                                                                if (Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount) == -1)//Sectorscount > 1//Questions[i].Sectors.Count //code by 191  only else was there
                                                                {
                                                                    if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                                    {
                                                                        //Redmine id: 175379
                                                                        writer.WriteLine(string.Empty);
                                                                        //originalData = null;
                                                                        //if (originalData.IsIV)
                                                                        //{
                                                                        //    writer.WriteLine("*");
                                                                        //}
                                                                        //else if (originalData.IsNA)
                                                                        //{
                                                                        //    writer.WriteLine();
                                                                        //}
                                                                        //else if (originalData.GetType() == typeof(NData))
                                                                        //{
                                                                        //    writer.WriteLine((originalData as NData).Value);
                                                                        //}
                                                                        //else if (originalData.GetType() == typeof(FAData))
                                                                        //{ writer.WriteLine((originalData as FAData).Value); }
                                                                        //else if (originalData.GetType() == typeof(SAData))
                                                                        //{ writer.WriteLine((originalData as SAData).Value); }
                                                                        //else if (originalData.GetType() == typeof(MAData))
                                                                        //{

                                                                        //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                                        //}
                                                                        originalData = null;
                                                                    }
                                                                    else
                                                                    {
                                                                        add3AppendArray[add3pointr] = (replaceData as SAData).Value.ToString();
                                                                        add3pointr++;
                                                                        if (j == Sectorscount - 1)// Questions[i].Sectors.Count - 1
                                                                        {
                                                                            int add3exclsett = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).Add3Exludesettings);
                                                                            bool isexclude = false; bool isalliv = true;
                                                                            for (int add3i = 0; add3i < add3AppendArray.Length; add3i++)
                                                                            {

                                                                                if (add3AppendArray[add3i] == "*" && (!string.IsNullOrEmpty(add3AppendArray[add3i]) && add3AppendArray[add3i] != ""))
                                                                                {
                                                                                    isexclude = true;
                                                                                }
                                                                                else if (add3AppendArray[add3i] != "0" && (!string.IsNullOrEmpty(add3AppendArray[add3i]) && add3AppendArray[add3i] != ""))
                                                                                {
                                                                                    isalliv = false;
                                                                                }
                                                                            }
                                                                            if (add3exclsett == 1 && isexclude == true && isalliv == true && Sectorscount == add3AppendArray.Where(s => s != null && s.Equals("*")).Count())//Redmine id:174489
                                                                            {
                                                                                writer.WriteLine("*");
                                                                            }
                                                                            else if (add3exclsett == 0 && isexclude == true && isalliv == true)
                                                                            {
                                                                                writer.WriteLine();
                                                                            }
                                                                            else
                                                                            {
                                                                                //[Redmine id : 175379] -
                                                                                writer.WriteLine(DataProcessSubstitutionMA(q, originalData
                                                                                         , add3AppendArray));//DataProcessAppendMA
                                                                            }
                                                                            originalData = null;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {

                                                                    if (r < 1)
                                                                    {
                                                                        int add1catcount = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                                                        add11_Dict = GetAllValues((Questions[i].Sectors[j] as INewVirtualQuestionSector).Add1paramvalue.ToString(), add1catcount, (Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias);
                                                                    }
                                                                    //191  addee if else.only else was der
                                                                    if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                                    {
                                                                        //191  added code for keeping old data in cell and it is from MA
                                                                        MAData mad = (originalData as MAData);
                                                                        if (mad.IsNA)
                                                                        {
                                                                            writer.WriteLine();
                                                                        }
                                                                        else if (mad.IsIV)
                                                                        {
                                                                            writer.WriteLine("*");
                                                                        }
                                                                        else
                                                                        {
                                                                            writer.WriteLine(mad.BinValue(q.Sectors.Count));
                                                                        }
                                                                        // writer.WriteLine((replacedata[r] as SAData).Value.ToString());// writer.WriteLine(string.Empty);//need to write old data ////  writer.WriteLine((replacedata[r] as SAData).Value.ToString());
                                                                        originalData = null;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (originalData.IsIV)//191 added if -only else was der- for UAT1 Redmine id -171315
                                                                        {
                                                                            writer.WriteLine("*");
                                                                            originalData = null;
                                                                        }
                                                                        else if (replaceData.GetType() == typeof(SAData))
                                                                        {
                                                                            string[] categoryAppendArray = new string[1];
                                                                            categoryAppendArray[0] = (replaceData as SAData).Value.ToString();
                                                                            if (add11_Dict.ContainsKey((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + categoryAppendArray[0].ToString()))
                                                                                writer.WriteLine(DataProcessAppendMA(q, originalData, categoryAppendArray));
                                                                            else writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));
                                                                            originalData = null;
                                                                        }
                                                                        //else if (replaceData.GetType() == typeof(MAData))
                                                                        //{
                                                                        //    int ad1catcount = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                                                        //    string[] categoryAppendArray = new string[ad1catcount];
                                                                        //    for (int add1i = 0; add1i < ad1catcount; add1i++)
                                                                        //    {
                                                                        //        categoryAppendArray[add1i] = "0";
                                                                        //    }
                                                                        //    //categoryAppendArray[0] = (replaceData as SAData).Value.ToString();
                                                                        //    string madata = ParseMconvertdata((originalData as MAData).BinValue(q.Sectors.Count).ToCharArray());
                                                                        //    string[] mavalues = madata.Split(',');
                                                                        //    int mavali = 0;
                                                                        //    foreach (string maval in mavalues)
                                                                        //    {
                                                                        //        if (add11_Dict.ContainsKey((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + maval))
                                                                        //        {
                                                                        //            mavali++;
                                                                        //            categoryAppendArray[mavali] = maval;
                                                                        //        }
                                                                        //    }

                                                                        //    if (categoryAppendArray.Length > 0)
                                                                        //        writer.WriteLine(DataProcessAppendMA(q, originalData, categoryAppendArray));
                                                                        //    // writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));
                                                                        //    else writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));
                                                                        //}




                                                                        //string[] categoryAppendArray = new string[1];
                                                                        //categoryAppendArray[0] = (replaceData as SAData).Value.ToString();//ADD1 logic here 
                                                                        //{
                                                                        //    //////////DataProcesses dproc = new DataProcesses();
                                                                        //    //////////IDataProcess dpOp;
                                                                        //    //////////dpOp= dproc.Add(DataProcessCode.ModifyData);
                                                                        //    //////////_INewQuestion add1ques;
                                                                        //    //////////add1ques = Questions[i];// dpOp.Questions.Add();
                                                                        //    //////////INewQuestionSectors add1sectors = add1ques.Sectors;
                                                                        //    //////////string add1query = (Questions[i].Sectors[j] as INewVirtualQuestionSector).Add1paramvalue.ToString();
                                                                        //    //////////var virtualadd1Sector = add1sectors.Add(add1query, true) as INewVirtualQuestionSector;
                                                                        //    //////////virtualadd1Sector.Alias = "1";
                                                                        //    //if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                                        //    //{
                                                                        //    //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));
                                                                        //    //}
                                                                        //    //else writer.WriteLine(DataProcessAppendMA(q, originalData
                                                                        //    //                 , categoryAppendArray)); 
                                                                        //    //originalData = null;
                                                                        //}
                                                                    }
                                                                }
                                                            }
                                                            else//else added for add3 10-11-19
                                                            {
                                                                if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                                {
                                                                    //Redmine id: 175379
                                                                    writer.WriteLine(string.Empty);
                                                                    //originalData = null;
                                                                    //if (originalData.IsIV)
                                                                    //{
                                                                    //    writer.WriteLine("*");
                                                                    //}
                                                                    //else if (originalData.IsNA)
                                                                    //{
                                                                    //    writer.WriteLine();
                                                                    //}
                                                                    //else if (originalData.GetType() == typeof(NData))
                                                                    //{
                                                                    //    writer.WriteLine((originalData as NData).Value);
                                                                    //}
                                                                    //else if (originalData.GetType() == typeof(FAData))
                                                                    //{
                                                                    //    writer.WriteLine((originalData as FAData).Value);
                                                                    //}
                                                                    //else if (originalData.GetType() == typeof(SAData))
                                                                    //{
                                                                    //    writer.WriteLine((originalData as SAData).Value);
                                                                    //}
                                                                    //else if (originalData.GetType() == typeof(MAData))
                                                                    //{

                                                                    //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                                    //}
                                                                    originalData = null;
                                                                }//if added by 191 21-11-19
                                                                else
                                                                {


                                                                    //add3AppendArray[add3pointr] = string.Empty;
                                                                    if (replaceData.IsIV) { add3AppendArray[add3pointr] = "*"; }//added new 16-11-19 for *,empty -Add3
                                                                    else if (replaceData.IsNA) { add3AppendArray[add3pointr] = string.Empty; }//added new 16-11-19 for *,empty -Add3
                                                                    add3pointr++;
                                                                    if (j == Sectorscount - 1)// Questions[i].Sectors.Count - 1
                                                                    {
                                                                        int add3exclsett = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).Add3Exludesettings);
                                                                        bool isexclude = false; bool isalliv = true;
                                                                        for (int add3i = 0; add3i < add3AppendArray.Length; add3i++)
                                                                        {

                                                                            if (add3AppendArray[add3i] == "*" && (!string.IsNullOrEmpty(add3AppendArray[add3i]) && add3AppendArray[add3i] != ""))
                                                                            {
                                                                                isexclude = true;
                                                                            }
                                                                            else if (add3AppendArray[add3i] != "0" && (!string.IsNullOrEmpty(add3AppendArray[add3i]) && add3AppendArray[add3i] != ""))
                                                                            { isalliv = false; }
                                                                        }
                                                                        if (add3exclsett == 1 && isexclude == true && isalliv == true && Sectorscount == add3AppendArray.Where(s => s != null && s.Equals("*")).Count())//Redmine id:174489
                                                                        {
                                                                            writer.WriteLine("*");
                                                                        }
                                                                        else if (add3exclsett == 0 && isexclude == true && isalliv == true)
                                                                        {
                                                                            writer.WriteLine();
                                                                        }
                                                                        else
                                                                        {
                                                                            //[Redmine id : 175379] -
                                                                            writer.WriteLine(DataProcessSubstitutionMA(q, originalData
                                                                                         , add3AppendArray));//DataProcessAppendMA
                                                                        }
                                                                        originalData = null;
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                        case EditMethod.REMOVE:
                                                            if (!replaceData.IsIV && !replaceData.IsNA)
                                                            {
                                                                if (r < 1)
                                                                {
                                                                    int add1catcount = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                                                    add11_Dict = GetAllValues((Questions[i].Sectors[j] as INewVirtualQuestionSector).Add1paramvalue.ToString(), add1catcount, (Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias);
                                                                }
                                                                if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                                {
                                                                    //191  added code for keeping old data in cell and it is from MA
                                                                    MAData mad = (originalData as MAData);
                                                                    if (mad.IsNA)
                                                                    {
                                                                        writer.WriteLine();
                                                                    }
                                                                    else if (mad.IsIV)
                                                                    {
                                                                        writer.WriteLine("*");
                                                                    }
                                                                    else
                                                                    {
                                                                        writer.WriteLine(mad.BinValue(q.Sectors.Count));
                                                                    }
                                                                    //  writer.WriteLine((replacedata[r] as SAData).Value.ToString());//  writer.WriteLine(string.Empty);//need to write old data ////  writer.WriteLine((replacedata[r] as SAData).Value.ToString());
                                                                    originalData = null;
                                                                }
                                                                else
                                                                {
                                                                    if (replaceData.GetType() == typeof(SAData))
                                                                    {
                                                                        //string[] categoryAppendArray = new string[1];
                                                                        //categoryAppendArray[0] = (replaceData as SAData).Value.ToString();
                                                                        string[] categoryRemoveArray = new string[1];
                                                                        categoryRemoveArray[0] = (replaceData as SAData).Value.ToString();

                                                                        if (add11_Dict.ContainsKey((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + categoryRemoveArray[0].ToString()))
                                                                            writer.WriteLine(DataProcessRemoveMA(q, originalData
                                                                                   , categoryRemoveArray));
                                                                        else writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));
                                                                        originalData = null;
                                                                    }


                                                                    originalData = null;


                                                                }
                                                            }
                                                            break;
                                                            //case EditMethod.JOIN:
                                                            //    #region join
                                                            //    //191  addee if else.only else was der
                                                            //    if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                            //    {
                                                            //        writer.WriteLine(string.Empty);
                                                            //        originalData = null;
                                                            //    }
                                                            //    else
                                                            //    {
                                                            //        // Data replaceDataa = ParentCollection.GetRawData(r, itemid);
                                                            //        if (replaceData.GetType() == typeof(SAData))
                                                            //        {
                                                            //            add3AppendArray[j] = (replaceData as SAData).Value.ToString();
                                                            //        }
                                                            //        else if (replaceData.GetType() == typeof(MAData))
                                                            //        {
                                                            //            add3AppendArray[j] = (replaceData as MAData).BinValue(q.Sectors.Count);// Value.ToString();
                                                            //        }
                                                            //        else if (replaceData.GetType() == typeof(FAData))
                                                            //        {
                                                            //            add3AppendArray[j] = (replaceData as FAData).Value.ToString();
                                                            //        }
                                                            //        else if (replaceData.GetType() == typeof(NData))
                                                            //        {
                                                            //            add3AppendArray[j] = (replaceData as NData).Value.ToString();
                                                            //        }
                                                            //        if (j == Sectorscount - 1)// Questions[i].Sectors.Count - 1
                                                            //        {
                                                            //            writer.WriteLine(DataProcessAppendMA(q, originalData
                                                            //                         , add3AppendArray));
                                                            //            originalData = null;
                                                            //        }
                                                            //    }
                                                            //    #endregion
                                                            //    break;
                                                    }

                                                    break;
                                                }
                                                else if (replaceData.GetType() == typeof(MAData))
                                                {
                                                    if (!replaceData.IsIV && !replaceData.IsNA)
                                                    {
                                                        int[] sectorbuf = (replaceData as MAData).SectorsArray;
                                                        int add3count = 0;
                                                        try
                                                        {
                                                            add3count = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                                        }
                                                        catch { }
                                                        if (add3count != -1)//added this line for add3 09-11-19
                                                        {
                                                            categoryArray = new string[sectorbuf.Length];

                                                            for (int x = 0; x < sectorbuf.Length; ++x)
                                                            {
                                                                categoryArray[x] = sectorbuf[x].ToString();

                                                            }
                                                        }
                                                        else//for add3
                                                        {

                                                            if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                            {
                                                                //Redmine id: 175379
                                                                writer.WriteLine(string.Empty);
                                                                //191  added code for keeping old data in cell and it is from MA
                                                                //MAData mad = (originalData as MAData);
                                                                //if (mad.IsNA)
                                                                //{
                                                                //    writer.WriteLine();
                                                                //}
                                                                //else if (mad.IsIV)
                                                                //{
                                                                //    writer.WriteLine("*");
                                                                //}
                                                                //else
                                                                //{
                                                                //    writer.WriteLine(mad.BinValue(q.Sectors.Count));
                                                                //}
                                                                //  writer.WriteLine((replacedata[r] as SAData).Value.ToString());//  writer.WriteLine(string.Empty);//need to write old data ////  writer.WriteLine((replacedata[r] as SAData).Value.ToString());
                                                                originalData = null;
                                                            }//21-11-19
                                                            else
                                                            {

                                                                for (int x = 0; x < sectorbuf.Length; ++x)
                                                                {
                                                                    add3AppendArray[add3pointr] = sectorbuf[x].ToString();
                                                                    add3pointr++;
                                                                }
                                                                if (j == Sectorscount - 1)// Questions[i].Sectors.Count - 1
                                                                {
                                                                    int add3exclsett = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).Add3Exludesettings);
                                                                    bool isexclude = false; bool isalliv = true;
                                                                    for (int add3i = 0; add3i < add3AppendArray.Length; add3i++)
                                                                    {

                                                                        if (add3AppendArray[add3i] == "*" && (!string.IsNullOrEmpty(add3AppendArray[add3i]) && add3AppendArray[add3i] != ""))
                                                                        {
                                                                            isexclude = true;
                                                                        }
                                                                        else if (add3AppendArray[add3i] != "0" && (!string.IsNullOrEmpty(add3AppendArray[add3i]) && add3AppendArray[add3i] != ""))
                                                                        {
                                                                            isalliv = false;
                                                                        }
                                                                    }
                                                                    if (add3exclsett == 1 && isexclude == true && isalliv == true && Sectorscount == add3AppendArray.Where(s => s != null && s.Equals("*")).Count())//Redmine id:174489
                                                                    {
                                                                        writer.WriteLine("*");
                                                                    }
                                                                    else if (add3exclsett == 0 && isexclude == true && isalliv == true)
                                                                    {
                                                                        writer.WriteLine();
                                                                    }
                                                                    else
                                                                    {
                                                                        //[Redmine id : 175379] -
                                                                        writer.WriteLine(DataProcessSubstitutionMA(q, originalData
                                                                                 , add3AppendArray));//DataProcessAppendMA
                                                                    }
                                                                    originalData = null;
                                                                }



                                                            }

                                                        }
                                                    }
                                                    //else if ((replaceData.IsIV || replaceData.IsNA) && Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount) == -1)
                                                    //{
                                                    //    if (replaceData.IsIV) { add3AppendArray[add3pointr] = "*"; }//added new 16-11-19 for *,empty -Add3
                                                    //    else if (replaceData.IsNA) { add3AppendArray[add3pointr] = string.Empty; }//added new 16-11-19 for *,empty -Add3
                                                    //    add3pointr++;
                                                    //    if (j == Sectorscount - 1)// Questions[i].Sectors.Count - 1
                                                    //    {


                                                    //        writer.WriteLine(DataProcessAppendMA(q, originalData
                                                    //                     , add3AppendArray));
                                                    //        originalData = null;
                                                    //    }
                                                    //}

                                                    if (sector.EditMethod == EditMethod.SUBSTITUTION)
                                                    {
                                                        #region 代入
                                                        // //Redmine id : 174576
                                                        //if (replaceData.IsIV)
                                                        //{
                                                        //    writer.WriteLine("*");
                                                        //}
                                                        //else if (replaceData.IsNA)
                                                        //{
                                                        //    writer.WriteLine();
                                                        //}
                                                        //else
                                                        {//ma category
                                                            //191 added ma :=  on 29-10-19
                                                            if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                            {
                                                                //  writer.WriteLine(string.Empty);//need to fill old data ////  writer.WriteLine((replacedata[r] as SAData).Value.ToString());
                                                                if (originalData.IsIV)
                                                                {
                                                                    writer.WriteLine("*");
                                                                }
                                                                else if (originalData.IsNA)
                                                                {
                                                                    writer.WriteLine();
                                                                }
                                                                else if (originalData.GetType() == typeof(NData))
                                                                {
                                                                    writer.WriteLine((originalData as NData).Value);
                                                                }
                                                                else if (originalData.GetType() == typeof(FAData))
                                                                {
                                                                    { writer.WriteLine(System.Text.RegularExpressions.Regex.Escape((originalData as FAData).Value)); }//[Redmine id : 174859] -//==  writer.WriteLine((originalData as FAData).Value);
                                                                }
                                                                else if (originalData.GetType() == typeof(SAData))
                                                                {
                                                                    writer.WriteLine((originalData as SAData).Value);
                                                                }
                                                                else if (originalData.GetType() == typeof(MAData))
                                                                {

                                                                    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                                }
                                                                originalData = null;
                                                            }
                                                            else
                                                            {
                                                                //Redmine id : 174576
                                                                if (replaceData.IsIV)
                                                                {
                                                                    writer.WriteLine("*");
                                                                }
                                                                else if (replaceData.IsNA)
                                                                {
                                                                    writer.WriteLine();
                                                                }
                                                                else
                                                                {
                                                                    writer.WriteLine(DataProcessSubstitutionMA(q, originalData, categoryArray));
                                                                }
                                                                originalData = null;
                                                            }
                                                        }
                                                        break;
                                                        #endregion
                                                    }
                                                    if (sector.EditMethod == EditMethod.APPEND)
                                                    {
                                                        #region 追加
                                                        if ((!replaceData.IsIV && !replaceData.IsNA) || Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount) != -1)
                                                        {
                                                            if (Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount) == -1)//&& j==Sectorscount-1 added by 191 for add3 //Sectorscount > 1//Questions[i].Sectors.Count //code by 191  only else was there
                                                            {
                                                                //writer.WriteLine(DataProcessAppendMA(q, originalData, categoryArray));//these code moved to MA type area  1862
                                                                //originalData = null;
                                                            }
                                                            else
                                                            {
                                                                if (r < 1)
                                                                {
                                                                    int add1catcount = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                                                    add11_Dict = GetAllValues((Questions[i].Sectors[j] as INewVirtualQuestionSector).Add1paramvalue.ToString(), add1catcount, (Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias);
                                                                }

                                                                if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                                {
                                                                    //writer.WriteLine(string.Empty);
                                                                    //originalData = null;
                                                                    if (originalData.IsIV)
                                                                    {
                                                                        writer.WriteLine("*");
                                                                    }
                                                                    else if (originalData.IsNA)
                                                                    {
                                                                        writer.WriteLine();
                                                                    }
                                                                    else if (originalData.GetType() == typeof(NData))
                                                                    {
                                                                        writer.WriteLine((originalData as NData).Value);
                                                                    }
                                                                    else if (originalData.GetType() == typeof(FAData))
                                                                    {
                                                                        writer.WriteLine((originalData as FAData).Value);
                                                                    }
                                                                    else if (originalData.GetType() == typeof(SAData))
                                                                    {
                                                                        writer.WriteLine((originalData as SAData).Value);
                                                                    }
                                                                    else if (originalData.GetType() == typeof(MAData))
                                                                    {

                                                                        writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                                    }
                                                                    originalData = null;
                                                                }//if added by 191 21-11-19
                                                                else
                                                                {

                                                                    int ad1catcount = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                                                    string[] categoryAppendArray = new string[ad1catcount];
                                                                    for (int add1i = 0; add1i < ad1catcount; add1i++)
                                                                    {
                                                                        categoryAppendArray[add1i] = "0";
                                                                    }
                                                                    //categoryAppendArray[0] = (replaceData as SAData).Value.ToString();
                                                                    if (originalData.IsIV)// if (replaceData.IsIV) //191 changed for UAT1 Redmine id -171315
                                                                    {
                                                                        writer.WriteLine("*");
                                                                    }
                                                                    //4295055497 commented for new internal issue glue id
                                                                    //else if (originalData.IsNA)// else if (replaceData.IsNA)//191 changed for UAT1 Redmine id -171315
                                                                    //{
                                                                    //    writer.WriteLine();
                                                                    //}
                                                                    else if (originalData.GetType() == typeof(MAData))
                                                                    {
                                                                        string madata = ParseMconvertdata((replaceData as MAData).BinValue(q.Sectors.Count).ToCharArray());
                                                                        string[] mavalues = madata.Split(',');
                                                                        int mavali = 0;
                                                                        foreach (string maval in mavalues)
                                                                        {
                                                                            if (!string.IsNullOrEmpty(maval))//index array out of bount 23-01-2020
                                                                            {
                                                                                if (add11_Dict.ContainsKey((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + maval))
                                                                                {
                                                                                    categoryAppendArray[mavali] = maval;
                                                                                    mavali++;//Redimne id : 188451
                                                                                }
                                                                            }
                                                                        }
                                                                        if (categoryAppendArray.Length > 0)
                                                                            writer.WriteLine(DataProcessAppendMA(q, originalData, categoryAppendArray));
                                                                        // writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));
                                                                        else writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));
                                                                    }


                                                                    originalData = null;
                                                                }
                                                            }
                                                            break;
                                                        }
                                                        else//else added for add3 10-11-19
                                                        {
                                                            if (Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount) == -1)//&& j==Sectorscount-1 added by 191 for add3 //Sectorscount > 1//Questions[i].Sectors.Count //code by 191  only else was there
                                                            {
                                                                if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                                {
                                                                    //Redmine id: 175379
                                                                    writer.WriteLine(string.Empty);
                                                                    //originalData = null;
                                                                    //if (originalData.IsIV)
                                                                    //{
                                                                    //    writer.WriteLine("*");
                                                                    //}
                                                                    //else if (originalData.IsNA)
                                                                    //{
                                                                    //    writer.WriteLine();
                                                                    //}
                                                                    //else if (originalData.GetType() == typeof(NData))
                                                                    //{
                                                                    //    writer.WriteLine((originalData as NData).Value);
                                                                    //}
                                                                    //else if (originalData.GetType() == typeof(FAData))
                                                                    //{
                                                                    //    writer.WriteLine((originalData as FAData).Value);
                                                                    //}
                                                                    //else if (originalData.GetType() == typeof(SAData))
                                                                    //{
                                                                    //    writer.WriteLine((originalData as SAData).Value);
                                                                    //}
                                                                    //else if (originalData.GetType() == typeof(MAData))
                                                                    //{

                                                                    //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                                    //}
                                                                    originalData = null;
                                                                }//if added by 191 21-11-19
                                                                else
                                                                {
                                                                    // add3AppendArray[add3pointr] = string.Empty;
                                                                    if (replaceData.IsIV) { add3AppendArray[add3pointr] = "*"; }//added new 16-11-19 for *,empty -Add3
                                                                    else if (replaceData.IsNA) { add3AppendArray[add3pointr] = string.Empty; }//added new 16-11-19 for *,empty -Add3
                                                                    add3pointr++;
                                                                    if (j == Sectorscount - 1)// Questions[i].Sectors.Count - 1
                                                                    {
                                                                        int add3exclsett = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).Add3Exludesettings);
                                                                        bool isexclude = false; bool isalliv = true;
                                                                        for (int add3i = 0; add3i < add3AppendArray.Length; add3i++)
                                                                        {

                                                                            if (add3AppendArray[add3i] == "*" && (!string.IsNullOrEmpty(add3AppendArray[add3i]) && add3AppendArray[add3i] != ""))
                                                                            {
                                                                                isexclude = true;
                                                                            }
                                                                            else if (add3AppendArray[add3i] != "0" && (!string.IsNullOrEmpty(add3AppendArray[add3i]) && add3AppendArray[add3i] != ""))
                                                                            {
                                                                                isalliv = false;
                                                                            }
                                                                        }
                                                                        if (add3exclsett == 1 && isexclude == true && isalliv == true && Sectorscount == add3AppendArray.Where(s => s != null && s.Equals("*")).Count()) //Redmine id:174489
                                                                        {
                                                                            writer.WriteLine("*");
                                                                        }
                                                                        else if (add3exclsett == 0 && isexclude == true && isalliv == true)
                                                                        {
                                                                            writer.WriteLine();
                                                                        }
                                                                        else
                                                                        {
                                                                            //[Redmine id : 175379] -
                                                                            writer.WriteLine(DataProcessSubstitutionMA(q, originalData
                                                                                     , add3AppendArray));//DataProcessAppendMA
                                                                        }
                                                                        originalData = null;
                                                                    }

                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                        /* */
                                                    }
                                                    if (sector.EditMethod == EditMethod.REMOVE)
                                                    {
                                                        if (r < 1)
                                                        {
                                                            int add1catcount = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                                            add11_Dict = GetAllValues((Questions[i].Sectors[j] as INewVirtualQuestionSector).Add1paramvalue.ToString(), add1catcount, (Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias);
                                                        }
                                                        #region 除外
                                                        if ((replaceData.IsIV && originalData.IsIV) || (replaceData.IsNA && originalData.IsNA))
                                                        {
                                                            // 非該当同士の場合は無回答に 無回答同士の場合は無回答に
                                                            //  writer.WriteLine();//commented by 191 for comming null for souce *
                                                            if (originalData.IsIV)//added code 191  28-11-19 minus1 issue,source * comes destination as noans
                                                            {
                                                                writer.WriteLine("*");
                                                            }
                                                            else if (originalData.IsNA)
                                                            {
                                                                writer.WriteLine();
                                                            }
                                                            originalData = null;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            //if (r < 1)//191 commented 25-12-19 for  --internal -4295055594
                                                            //{
                                                            //    int add1catcount = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                                            //    add11_Dict = GetAllValues((Questions[i].Sectors[j] as INewVirtualQuestionSector).Add1paramvalue.ToString(), add1catcount, (Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias);
                                                            //}
                                                            if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                            {
                                                                //191  added code for keeping old data in cell and it is from MA 28-11-19
                                                                MAData mad = (originalData as MAData);
                                                                if (mad.IsNA)
                                                                {
                                                                    writer.WriteLine();
                                                                }
                                                                else if (mad.IsIV)
                                                                {
                                                                    writer.WriteLine("*");
                                                                }
                                                                else
                                                                {
                                                                    writer.WriteLine(mad.BinValue(q.Sectors.Count));
                                                                }
                                                                //  writer.WriteLine((replacedata[r] as SAData).Value.ToString());//  writer.WriteLine(string.Empty);//need to write old data ////  writer.WriteLine((replacedata[r] as SAData).Value.ToString());
                                                                originalData = null;
                                                            }
                                                            else
                                                            {
                                                                int remvcatcount = Convert.ToInt32((Questions[i].Sectors[j] as INewVirtualQuestionSector).jointCategoryCount);
                                                                //string[] categoryAppendArray = new string[ad1catcount];
                                                                string[] categoryRemoveArray = new string[remvcatcount];
                                                                for (int remvIndex = 0; remvIndex < remvcatcount; remvIndex++)
                                                                {
                                                                    categoryRemoveArray[remvIndex] = "0";
                                                                }
                                                                //categoryAppendArray[0] = (replaceData as SAData).Value.ToString();
                                                                string madata = ParseMconvertdata((replaceData as MAData).BinValue(q.Sectors.Count).ToCharArray());
                                                                string[] mavalues = madata.Split(',');
                                                                int mavali = 0;
                                                                foreach (string maval in mavalues)
                                                                {
                                                                    if (!string.IsNullOrEmpty(maval))//index array out of bount 23-01-2020
                                                                    {
                                                                        if (add11_Dict.ContainsKey((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + maval))
                                                                        {

                                                                            categoryRemoveArray[mavali] = maval;
                                                                            mavali++;
                                                                        }
                                                                    }
                                                                }
                                                                if (categoryRemoveArray.Length > 0)
                                                                    writer.WriteLine(DataProcessRemoveMA(q, originalData
                                                                                , categoryRemoveArray));
                                                                // writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));
                                                                else writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));
                                                                originalData = null;
                                                            }
                                                        }
                                                        //else if (categoryArray != null)
                                                        //{
                                                        //    writer.WriteLine(DataProcessRemoveMA(q, originalData, categoryArray));
                                                        //    originalData = null;
                                                        //    break;
                                                        //}
                                                        #endregion
                                                    }
                                                }
                                                else if (replaceData.GetType() == typeof(NData))
                                                {
                                                    if (sector.EditMethod != EditMethod.SUBSTITUTION)
                                                    {
                                                        // Nアイテムに対して修正方法が不正です。{0}
                                                        throw new QCWebException("QCCMN05010010"
                                                                                 , new string[] { sector.EditMethod.GetHashCode().ToString() }
                                                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                                                 , null);
                                                    }
                                                    //Redmine id : 174576;174850
                                                    if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                    {
                                                        //  writer.WriteLine(string.Empty);//need to fill old data ////  writer.WriteLine((replacedata[r] as SAData).Value.ToString());
                                                        if (originalData.IsIV)
                                                        {
                                                            writer.WriteLine("*");
                                                        }
                                                        else if (originalData.IsNA)
                                                        {
                                                            writer.WriteLine();
                                                        }
                                                        else if (originalData.GetType() == typeof(NData))
                                                        {
                                                            writer.WriteLine((originalData as NData).Value);
                                                        }
                                                        else if (originalData.GetType() == typeof(FAData))
                                                        {
                                                            { writer.WriteLine(System.Text.RegularExpressions.Regex.Escape((originalData as FAData).Value)); }//[Redmine id : 174859] -// writer.WriteLine((originalData as FAData).Value);
                                                        }
                                                        else if (originalData.GetType() == typeof(SAData))
                                                        {
                                                            writer.WriteLine((originalData as SAData).Value);
                                                        }
                                                        else if (originalData.GetType() == typeof(MAData))
                                                        {

                                                            writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                        }
                                                        originalData = null;
                                                    }
                                                    else
                                                    {
                                                        if (replaceData.IsIV)
                                                        {
                                                            writer.WriteLine("*");
                                                        }
                                                        else if (replaceData.IsNA)
                                                        {
                                                            writer.WriteLine();
                                                        }
                                                        else
                                                        {
                                                            writer.WriteLine((replaceData as NData).Value);
                                                        }
                                                        originalData = null;

                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    if (q.QCAnswerType != Question.QCAnswerType.FA)
                                                    {
                                                        // FAアイテムでないためアイテム指定はできません
                                                        throw new QCWebException("QCCMN05010004", GlobalsCommonConstant.LogLevel.FATAL, null);
                                                    }

                                                    if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                    {

                                                        //need to get as FA and try

                                                        if (originalData.IsIV)
                                                        {
                                                            writer.WriteLine("*");
                                                        }
                                                        else if (originalData.IsNA)
                                                        {
                                                            writer.WriteLine();
                                                        }
                                                        else if (originalData.GetType() == typeof(NData))
                                                        {
                                                            writer.WriteLine((originalData as NData).Value);
                                                        }
                                                        else if (originalData.GetType() == typeof(FAData))//issue may com here for /r/n
                                                        {
                                                            // writer.WriteLine((originalData as FAData).Value);
                                                            string fadat = (replaceData as FAData).Value;
                                                            writer.WriteLine(System.Text.RegularExpressions.Regex.Escape(fadat));//issue may com here for /r/n    { writer.WriteLine(System.Text.RegularExpressions.Regex.Escape((originalData as FAData).Value)); }//[Redmine id : 174859] -
                                                        }
                                                        else if (originalData.GetType() == typeof(SAData))
                                                        {
                                                            writer.WriteLine((originalData as SAData).Value);
                                                        }
                                                        // writer.WriteLine(string.Empty);
                                                        originalData = null;
                                                        //(replaceData as SAData).Value.ToString()
                                                    }
                                                    else
                                                    {/*#*qc4*rn*  #*qc4*n**/
                                                        if (replaceData.IsIV)
                                                        {
                                                            writer.WriteLine("*");
                                                        }
                                                        else if (replaceData.IsNA)
                                                        {
                                                            writer.WriteLine();
                                                        }
                                                        else
                                                        {
                                                            string fadat = (replaceData as FAData).Value;
                                                            // writer.WriteLine((replaceData as FAData).Value);
                                                            writer.WriteLine(System.Text.RegularExpressions.Regex.Escape(fadat));//issue may com here for /r/n                                                           
                                                        }
                                                        originalData = null;
                                                    }
                                                    break;
                                                }
                                                #endregion
                                                break;
                                            case ModifyDataEdit.FREE:
                                                #region フリー
                                                if (q.QCAnswerType == Question.QCAnswerType.SA)
                                                {
                                                    // SAアイテムに対してフリー指定はできません
                                                    throw new QCWebException("QCCMN05010005", GlobalsCommonConstant.LogLevel.FATAL, null);
                                                }
                                                if (q.QCAnswerType == Question.QCAnswerType.MA)
                                                {
                                                    // MAアイテムに対してフリー指定はできません
                                                    throw new QCWebException("QCCMN05010006", GlobalsCommonConstant.LogLevel.FATAL, null);
                                                }
                                                //191  logic for criteria
                                                if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                {

                                                    //need to get as FA and try

                                                    if (originalData.IsIV)
                                                    {
                                                        writer.WriteLine("*");
                                                    }
                                                    else if (originalData.IsNA)
                                                    {
                                                        writer.WriteLine();
                                                    }
                                                    else if (originalData.GetType() == typeof(NData))
                                                    {
                                                        int dds = r;
                                                        writer.WriteLine((originalData as NData).Value);
                                                    }
                                                    else if (originalData.GetType() == typeof(FAData))//issue may com here for /r/n
                                                    {
                                                        //writer.WriteLine((originalData as FAData).Value);
                                                        string fadat = (originalData as FAData).Value;
                                                        writer.WriteLine(System.Text.RegularExpressions.Regex.Escape(fadat));//issue may com here for /r/n    
                                                    }
                                                    else if (originalData.GetType() == typeof(SAData))
                                                    { writer.WriteLine((originalData as SAData).Value); }
                                                    // writer.WriteLine(string.Empty);
                                                    originalData = null;
                                                    //(replaceData as SAData).Value.ToString()
                                                }
                                                else
                                                {
                                                    //[Redmine id :178260
                                                    if (originalData.GetType() == typeof(NData))
                                                    {
                                                        writer.WriteLine(alias);
                                                    }
                                                    else
                                                    {
                                                        writer.WriteLine(System.Text.RegularExpressions.Regex.Escape(alias));//[Redmine id : 174859] -
                                                    }
                                                    originalData = null;
                                                }
                                                #endregion
                                                break;
                                            case ModifyDataEdit.UNMATCH:
                                                #region 非該当
                                                writer.WriteLine("*");
                                                originalData = null;
                                                #endregion
                                                break;
                                            case ModifyDataEdit.DK:
                                                #region 無回答
                                                writer.WriteLine();
                                                originalData = null;
                                                #endregion
                                                break;
                                            case ModifyDataEdit.JOIN:
                                                #region join
                                                //191  addee if else.only else was der
                                                if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                                {
                                                    //Redmine id: 175382
                                                    writer.WriteLine(string.Empty);
                                                    //originalData = null;

                                                    //if (originalData.IsIV)
                                                    //{
                                                    //    writer.WriteLine("*");
                                                    //}
                                                    //else if (originalData.IsNA)
                                                    //{
                                                    //    writer.WriteLine();
                                                    //}
                                                    //else if (originalData.GetType() == typeof(NData))
                                                    //{
                                                    //    writer.WriteLine((originalData as NData).Value);
                                                    //}
                                                    //else if (originalData.GetType() == typeof(FAData))
                                                    //{ writer.WriteLine((originalData as FAData).Value); }
                                                    //else if (originalData.GetType() == typeof(SAData))
                                                    //{ writer.WriteLine((originalData as SAData).Value); }
                                                    //else if (originalData.GetType() == typeof(MAData))
                                                    //{

                                                    //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                                    //}
                                                    originalData = null;

                                                    break;//191  adeed for extra lines comming in file
                                                }
                                                else
                                                {
                                                    decimal itemId = 0;
                                                    if (!decimal.TryParse(alias, out itemId))
                                                    {
                                                        // アイテムIDが不正です。{0}
                                                        throw new QCWebException("QCCMN05010009"
                                                                                 , new string[] { alias }
                                                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                                                 , null);
                                                    }
                                                    Data replaceDataa = ParentCollection.GetRawData(r, itemId);
                                                    if (replaceDataa.GetType() == typeof(SAData))
                                                    {

                                                        for (int ti = 0; ti < joint_array.GetLength(0); ti++)
                                                        {
                                                            if ((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as SAData).Value.ToString() == joint_array[ti, 0])
                                                            {
                                                                if (!temp_join_Dict.ContainsKey(joint_array[ti, 1]))
                                                                {
                                                                    temp_join_Dict.Add(joint_array[ti, 1], joint_array[ti, 0]);
                                                                    add3AppendArray[add3pointr] = joint_array[ti, 1];// Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as SAData).Value.ToString()];
                                                                    add3pointr++;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else if (replaceDataa.GetType() == typeof(MAData))//issue for MA data need to  convert to decimal  and chek
                                                    {
                                                        int jj = j;
                                                        /*string mavalues = ParseMconvertdata((replaceDataa as MAData).BinValue(q.Sectors.Count).ToCharArray());
                                                        string[] s = mavalues.Split(',');*/
                                                        string[] s = (replaceDataa as MAData).CodeValue.Split(',');
                                                        for (int sim = 0; sim < s.Length; sim++)
                                                        {//itemNameIdList.ContainsKey(itemNamesList[j]
                                                            if (!string.IsNullOrEmpty(s[sim]))
                                                            {
                                                                for (int ti = 0; ti < joint_array.GetLength(0); ti++)
                                                                {
                                                                    if ((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + s[sim] == joint_array[ti, 0])
                                                                    {
                                                                        if (!temp_join_Dict.ContainsKey(joint_array[ti, 1]))
                                                                        {
                                                                            temp_join_Dict.Add(joint_array[ti, 1], joint_array[ti, 0]);
                                                                            add3AppendArray[add3pointr] = joint_array[ti, 1];// Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as SAData).Value.ToString()];
                                                                            jj++;
                                                                            add3pointr++;
                                                                        }
                                                                    }
                                                                }
                                                                //if (Join_Dict.ContainsKey((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + s[sim]))
                                                                //{
                                                                //    add3AppendArray[jj] = Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + s[sim]];
                                                                //    jj++;
                                                                //}

                                                            }
                                                        }
                                                        //int si = 0;
                                                        //foreach (KeyValuePair<string, string> entry in Join_Dict)
                                                        //{
                                                        //    s[si] = entry.Key.Split('-')[1].ToString();si++;
                                                        //}
                                                        // add3AppendArray[j] = DataProcessAppendMA(q, replaceDataa, s);
                                                        // add3AppendArray[j] = DataProcessAppendMA(q, replaceDataa,s);
                                                        // add3AppendArray[j] = Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as MAData).BinValue(q.Sectors.Count)]; ;// (replaceDataa as MAData).BinValue(q.Sectors.Count);// Value.ToString();
                                                    }
                                                    else if (replaceDataa.GetType() == typeof(FAData))
                                                    {
                                                        for (int ti = 0; ti < joint_array.GetLength(0); ti++)
                                                        {
                                                            if ((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as FAData).Value.ToString() == joint_array[ti, 0])
                                                            {
                                                                if (!temp_join_Dict.ContainsKey(joint_array[ti, 1]))
                                                                {
                                                                    temp_join_Dict.Add(joint_array[ti, 1], joint_array[ti, 0]);
                                                                    add3AppendArray[add3pointr] = joint_array[ti, 1];// Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as SAData).Value.ToString()];
                                                                    j++;
                                                                    add3pointr++;
                                                                }
                                                            }
                                                        }
                                                        // add3AppendArray[j] = Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as FAData).Value.ToString()]; ;// (replaceDataa as FAData).Value.ToString();
                                                    }
                                                    else if (replaceDataa.GetType() == typeof(NData))
                                                    {
                                                        for (int ti = 0; ti < joint_array.GetLength(0); ti++)
                                                        {
                                                            if ((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as NData).Value.ToString() == joint_array[ti, 0])
                                                            {
                                                                if (!temp_join_Dict.ContainsKey(joint_array[ti, 1]))
                                                                {
                                                                    temp_join_Dict.Add(joint_array[ti, 1], joint_array[ti, 0]);
                                                                    add3AppendArray[add3pointr] = joint_array[ti, 1];// Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as SAData).Value.ToString()];
                                                                    j++;
                                                                    add3pointr++;
                                                                }
                                                            }
                                                        }
                                                        //  add3AppendArray[j] = Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as NData).Value.ToString()]; ;// (replaceDataa as NData).Value.ToString();
                                                    }
                                                    if (j == Sectorscount - 1)// Questions[i].Sectors.Count - 1
                                                    {
                                                        //writer.WriteLine("111");
                                                        writer.WriteLine(DataProcessSubstitutionMA(q, originalData
                                                                     , add3AppendArray));//DataProcessAppendMA
                                                        originalData = null;
                                                        temp_join_Dict.Clear();
                                                    }
                                                }
                                                #endregion
                                                break;
                                        }
                                        // 書き込みがされた場合は、ループを抜ける
                                        if (originalData == null) break;//191  chnaged to continue   --original code is break

                                    }
                                    else if ((Questions[i].Sectors[Sectorscount - 1] as INewVirtualQuestionSector).ModifyDataEdit == ModifyDataEdit.JOIN)
                                    {
                                        //191  addee if else.only else was der
                                        if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                        {
                                            //Redmine id: 175382
                                            writer.WriteLine(string.Empty);
                                            //originalData = null;
                                            //if (originalData.IsIV)
                                            //{
                                            //    writer.WriteLine("*");
                                            //}
                                            //else if (originalData.IsNA)
                                            //{
                                            //    writer.WriteLine();
                                            //}
                                            //else if (originalData.GetType() == typeof(NData))
                                            //{
                                            //    writer.WriteLine((originalData as NData).Value);
                                            //}
                                            //else if (originalData.GetType() == typeof(FAData))
                                            //{ writer.WriteLine((originalData as FAData).Value); }
                                            //else if (originalData.GetType() == typeof(SAData))
                                            //{ writer.WriteLine((originalData as SAData).Value); }
                                            //else if (originalData.GetType() == typeof(MAData))
                                            //{

                                            //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                            //}
                                            originalData = null;
                                            break;//191  adeed for extra lines comming in file
                                        }
                                        else
                                        {
                                            decimal itemId = 0;
                                            if (!decimal.TryParse((Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias, out itemId))
                                            {
                                                // アイテムIDが不正です。{0}
                                                throw new QCWebException("QCCMN05010009"
                                                                         , new string[] { (Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias }
                                                                         , GlobalsCommonConstant.LogLevel.FATAL
                                                                         , null);
                                            }
                                            Data replaceDataa = ParentCollection.GetRawData(r, itemId);
                                            //if (replaceDataa.GetType() == typeof(SAData))
                                            //{
                                            //    add3AppendArray[j] = (replaceDataa as SAData).Value.ToString();
                                            //}
                                            //else if (replaceDataa.GetType() == typeof(MAData))
                                            //{
                                            //    add3AppendArray[j] = (replaceDataa as MAData).BinValue(q.Sectors.Count);// Value.ToString();
                                            //}
                                            //else if (replaceDataa.GetType() == typeof(FAData))
                                            //{
                                            //    add3AppendArray[j] = (replaceDataa as FAData).Value.ToString();
                                            //}
                                            //else if (replaceDataa.GetType() == typeof(NData))
                                            //{
                                            //    add3AppendArray[j] = (replaceDataa as NData).Value.ToString();
                                            //}
                                            if (replaceDataa.GetType() == typeof(SAData))
                                            {
                                                add3AppendArray[add3pointr] = "0";// Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as SAData).Value.ToString()];
                                            }
                                            else if (replaceDataa.GetType() == typeof(MAData))
                                            {
                                                add3AppendArray[add3pointr] = "0";//Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as MAData).BinValue(q.Sectors.Count)]; ;// (replaceDataa as MAData).BinValue(q.Sectors.Count);// Value.ToString();
                                            }
                                            else if (replaceDataa.GetType() == typeof(FAData))
                                            {
                                                add3AppendArray[add3pointr] = "0";//Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as FAData).Value.ToString()]; ;// (replaceDataa as FAData).Value.ToString();
                                            }
                                            else if (replaceDataa.GetType() == typeof(NData))
                                            {
                                                add3AppendArray[add3pointr] = "0";// Join_Dict[(Questions[i].Sectors[j] as INewVirtualQuestionSector).Alias + "-" + (replaceDataa as NData).Value.ToString()]; ;// (replaceDataa as NData).Value.ToString();
                                            }
                                            add3pointr++;
                                            if (j == Sectorscount - 1)// Questions[i].Sectors.Count - 1
                                            {
                                                // writer.WriteLine("111");
                                                writer.WriteLine(DataProcessSubstitutionMA(q, originalData
                                                            , add3AppendArray));
                                                originalData = null;
                                                temp_join_Dict.Clear();
                                            }
                                        }
                                    }
                                }
                                // 変換なし
                                if (originalData != null && (Questions[i].Sectors[Sectorscount - 1] as INewVirtualQuestionSector).ModifyDataEdit != ModifyDataEdit.JOIN)
                                {
                                    if (originalData.GetType() == typeof(SAData))
                                    {
                                        SAData sad = (originalData as SAData);
                                        if (sad.IsNA)
                                        {
                                            writer.WriteLine();
                                        }
                                        else if (sad.IsIV)
                                        {
                                            writer.WriteLine("*");
                                        }
                                        else
                                        {
                                            writer.WriteLine(sad.Value);
                                        }
                                    }
                                    else if (originalData.GetType() == typeof(MAData))
                                    {
                                        MAData mad = (originalData as MAData);
                                        if (mad.IsNA)
                                        {
                                            writer.WriteLine();
                                        }
                                        else if (mad.IsIV)
                                        {
                                            writer.WriteLine("*");
                                        }
                                        else
                                        {
                                            writer.WriteLine(mad.BinValue(q.Sectors.Count));
                                        }
                                    }
                                    else if (originalData.GetType() == typeof(NData))
                                    {
                                        NData nd = (originalData as NData);
                                        if (nd.IsNA)
                                        {
                                            writer.WriteLine();
                                        }
                                        else if (nd.IsIV)
                                        {
                                            writer.WriteLine("*");
                                        }
                                        else
                                        {
                                            writer.WriteLine(nd.Value);
                                        }
                                    }
                                    else
                                    {
                                        FAData fad = (originalData as FAData);
                                        if (fad.IsNA)
                                        {
                                            writer.WriteLine();
                                        }
                                        else if (fad.IsIV)
                                        {
                                            writer.WriteLine("*");
                                        }
                                        else
                                        {
                                            writer.WriteLine(fad.Value);
                                        }
                                    }
                                }
                            }
                            writer.Close();
                        }
                    }

                    // 作りなおしたファイルの再登録
                    for (int i = 0; i < Questions.Count; ++i)
                    {
                        decimal orgItemId = decimal.Parse(Questions[i].ItemId);
                        if (ParentCollection.IsRawData(orgItemId))
                        {
                            string path = null;
                            QuestionType questionType;
                            QCWebException exception = null;
                            Question.Questions.Question q = ParentCollection.GetQuestion(orgItemId);
                            List<Data> newDatas =
                                //ReadTextFile.ReadData2(orgItemId, ParentCollection.DataDirectoryPath, out path, out questionType, out exception, false, true);
                                ReadTextFile.ReadData2(q, ParentCollection.DataDirectoryPath, out path, out questionType, out exception, false, true);
                            if (exception != null) throw exception;
                            ParentCollection.SetRawData(orgItemId, newDatas);
                        }
                    }
                    //filterResult情報を削除する。
                    ParentCollection.FilterResultDataClear();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    System.Diagnostics.Debug.Indent();
                    System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                    System.Diagnostics.Debug.Unindent();
                    throw;
                }
            }

            /// <summary>
            /// SAアイテムの代入処理
            /// </summary>
            /// <param name="question">質問クラス</param>
            /// <param name="orgData">オリジナルデータ</param>
            /// <param name="category">除外選択肢番号</param>
            /// <returns>処理結果</returns>
            private string DataProcessSubstitutionSA(Question.Questions.Question question, Data orgData, string category)
            {
                if (question.QCAnswerType == QCAnswerType.N)
                {
                    return category;
                }
                else if (question.QCAnswerType == QCAnswerType.MA)
                {
                    char[] mabuf = new string('0', question.Sectors.Count).ToCharArray();
                    int count = int.Parse(category);
                    //Redmine id: 174849
                    bool isConverted = false;
                    if (question.Sectors.Count >= count)//https://redmine.macromill.com/issues/174849 if (question.Sectors.Count > count)
                    {
                        mabuf[mabuf.Length - count] = '1';
                        isConverted = true;
                    }
                    if (isConverted)
                    {
                        return new string(mabuf);
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    for (int i = 1; i <= question.Sectors.Count; ++i)
                    {
                        Question.Sectors.Sector sector = (Question.Sectors.Sector)question.Sectors[i];
                        if (sector.Number == int.Parse(category))
                        {
                            return category;
                        }
                    }
                }
                // 代入カテゴリが修正アイテムカテゴリにない場合は無回答とする
                return "";
            }

            /// <summary>
            /// MAアイテムの代入処理
            /// </summary>
            /// <param name="question"></param>
            /// <param name="orgData"></param>
            /// <param name="categoryArray"></param>
            /// <returns></returns>
            private string DataProcessSubstitutionMA(Question.Questions.Question question, Data orgData, string[] categoryArray)
            {
                List<int> newSectorsArray = new List<int>();
                for (int i = 1; i <= question.Sectors.Count; ++i)
                {
                    Question.Sectors.Sector sector = (Question.Sectors.Sector)question.Sectors[i];
                    int pos = Array.IndexOf(categoryArray, sector.Number.ToString());//new code added and below code commented for :Redmine id : 188486
                    if (pos > -1)
                    {
                        newSectorsArray.Add(sector.Number);
                    }
                    //for (int j = 0; j < categoryArray.Length; ++j)
                    //{
                    //    if (!string.IsNullOrEmpty(categoryArray[j]) && categoryArray[j] != "*")//&& added by 191
                    //    {
                    //        if (sector.Number == int.Parse(categoryArray[j]))
                    //        {
                    //            newSectorsArray.Add(sector.Number);
                    //            break;
                    //        }
                    //    }
                    //}
                }
                if (newSectorsArray.Count > 0)
                {
                    // BIT値に変換
                    char[] mabuf = new string('0', question.Sectors.Count).ToCharArray();
                    bool isConverted = false;
                    for (int x = 0; x < newSectorsArray.Count; ++x)
                    {
                        mabuf[mabuf.Length - newSectorsArray[x]] = '1';
                        isConverted = true;
                    }

                    return isConverted ? new string(mabuf) : "";
                }

                // 一致した選択肢がない場合は無回答とする
                return "";
            }

            /// <summary>
            /// 処理対象アイテムMAに対する除外処理
            /// 変換前が非該当 or 無回答の場合はそのまま出力する
            /// すべて除外された場合は無回答になる
            /// </summary>
            /// <param name="question"></param>
            /// <param name="orgData"></param>
            /// <param name="categoryArray"></param>
            /// <returns></returns>
            private string DataProcessRemoveMA(Question.Questions.Question question, Data orgData, string[] categoryArray)
            {
                if (orgData.IsIV)
                {
                    // 変換前が非該当の場合はそのまま出力する
                    return "*";
                }
                else if (orgData.IsNA)
                {
                    // 変換前が無回答の場合はそのまま出力する
                    return "";
                }
                else
                {
                    int[] sectorsArray = (orgData as MAData).SectorsArray;
                    List<int> newSectorsArray = new List<int>();

                    // 一致しない選択肢番号を抽出する
                    for (int x = 0; x < sectorsArray.Length; ++x)
                    {
                        bool matchFlg = false;
                        for (int y = 0; y < categoryArray.Length; ++y)
                        {
                            if (sectorsArray[x] == int.Parse(categoryArray[y]))
                            {
                                matchFlg = true;
                                break;
                            }
                        }
                        if (!matchFlg) newSectorsArray.Add(sectorsArray[x]);
                    }

                    if (newSectorsArray.Count > 0)
                    {
                        // BIT値に変換
                        char[] mabuf = new string('0', question.Sectors.Count).ToCharArray();
                        for (int x = 0; x < newSectorsArray.Count; ++x)
                        {
                            mabuf[mabuf.Length - newSectorsArray[x]] = '1';
                        }
                        return new string(mabuf);
                    }
                    // 全部除外された場合は無回答にする
                    return "";
                }
            }

            /// <summary>
            /// 処理対象アイテムMAに対する追加処理
            /// 変換前が非該当の場合はそのまま出力する
            /// 変換前が無回答の場合は代入になる
            /// </summary>
            /// <param name="question"></param>
            /// <param name="orgData"></param>
            /// <param name="categoryArray"></param>
            /// <returns></returns>
            private string DataProcessAppendMA(Question.Questions.Question question, Data orgData, string[] categoryArray)
            {
                //if (orgData.IsIV)
                //{
                //    // 変換前が非該当の場合はそのまま出力する
                //    return "*";
                //}
                if (orgData.IsNA || orgData.IsIV)//change by JIJ
                {
                    // 無回答の場合は代入になる
                    return DataProcessSubstitutionMA(question, orgData, categoryArray);
                }
                else
                {
                    int[] sectorsArray = (orgData as MAData).SectorsArray;
                    List<int> addSectorsArray = new List<int>();

                    for (int i = 1; i <= question.Sectors.Count; ++i)
                    {
                        Question.Sectors.Sector sector = (Question.Sectors.Sector)question.Sectors[i];
                        for (int j = 0; j < categoryArray.Length; ++j)
                        {
                            if (categoryArray[j] != null && categoryArray[j] != string.Empty && categoryArray[j] != "*" && sector.Number == int.Parse(categoryArray[j]))
                            {
                                addSectorsArray.Add(sector.Number);
                            }

                        }
                    }

                    if (addSectorsArray.Count > 0)
                    {
                        char[] mabuf = new string('0', question.Sectors.Count).ToCharArray();
                        for (int i = 0; i < sectorsArray.Length; ++i)
                        {
                            mabuf[mabuf.Length - sectorsArray[i]] = '1';
                        }

                        for (int i = 0; i < addSectorsArray.Count; ++i)
                        {
                            mabuf[mabuf.Length - addSectorsArray[i]] = '1';
                        }

                        return new string(mabuf);
                    }

                    // 変換なし
                    return (orgData as MAData).BinValue(question.Sectors.Count);
                }
            }
        }
        #endregion

        #region COUNT(N)のデータ加工関連クラス
        /// <summary>
        /// COUNT(N)のデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("0AA8C4E3-CAD9-435c-9423-B34DBBC27794")]
        public class DataProcessCount : DataProcess
        {
            internal DataProcessCount(DataProcesses collection) : base(collection, DataProcessCode.ResponseCount) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
            }

            /// <summary>
            /// データ加工を実行するメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Execute()//count here also    NTYPE
            {
                if (Questions == null) return;
                if (!RunFlag) return;
                if (ParentCollection == null || string.IsNullOrWhiteSpace(ParentCollection.DataDirectoryPath)) return;
                try
                {
                    for (int i = 0; i < Questions.Count; ++i)
                    {
                        //191 
                        bool iscriteria = false;
                        decimal orgItemId = decimal.Parse(Questions[i].ItemId);
                        Question.Questions.Question q = ParentCollection.GetQuestion(orgItemId);
                        //var replacedata = ParentCollection.GetRawData(decimal.Parse(Questions[0].ItemId));//Questions[0].SourceItemId //came null so cahnged to item id
                        //
                        //string path = System.IO.Path.Combine(ParentCollection.DataDirectoryPath, Questions[i].ItemId + ".dp");
                        string path = GetNewItemFilePath(i);
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, false, Encoding.UTF8))
                        {
                            for (int r = 0; r < ParentCollection.SamplesCount; ++r)
                            {
                                Data originalData = ParentCollection.GetRawData(r, orgItemId);
                                //191 
                                int Sectorscount = Questions[i].Sectors.Count;
                                if (Questions[i].Sectors.Count > 1)//191  code 
                                {
                                    //check last sector criteria having sub criteria (for multiple criteria) or last sector having alias=1 for criteria presence
                                    // if (Questions[i].Sectors[Sectorscount - 1].Criteria != null)
                                    {
                                        INewVirtualQuestionSector s = (Questions[i].Sectors[Sectorscount - 1] as INewVirtualQuestionSector);
                                        if (s != null)
                                        {
                                            if (s.ModifyDataEdit == 0 && s.EditMethod == 0)
                                            {
                                                Sectorscount = Sectorscount - 1;//cannot do for add3 and other having more sectors
                                                iscriteria = true;

                                            }
                                        }
                                    }

                                    // GetQuestionIDforDP(Questions[Sectorscount].Sectors);
                                }
                                if ((Questions[i].QuestionType & Tabulation.QuestionType.N) != Tabulation.QuestionType.N)
                                {
                                    //新回答タイプがN以外の場合、スキップする。
                                    continue;
                                }
                                for (int j = 0; j < Sectorscount; ++j)//Questions[i].Sectors.Count
                                {
                                    //191  criteria
                                    if (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//191  for criteria impl 
                                    {
                                        //Redmine id : 175385
                                        writer.WriteLine(string.Empty);
                                        //if (originalData.IsIV)
                                        //{
                                        //    writer.WriteLine("*");
                                        //}
                                        //else if (originalData.IsNA)
                                        //{
                                        //    writer.WriteLine();
                                        //}
                                        //else if (originalData.GetType() == typeof(NData))
                                        //{
                                        //    writer.WriteLine((originalData as NData).Value);
                                        //}
                                        //else if (originalData.GetType() == typeof(FAData))
                                        //{ writer.WriteLine((originalData as FAData).Value); }
                                        //else if (originalData.GetType() == typeof(SAData))
                                        //{ writer.WriteLine((originalData as SAData).Value); }
                                        //else if (originalData.GetType() == typeof(MAData))
                                        //{

                                        //    writer.WriteLine((originalData as MAData).BinValue(q.Sectors.Count));

                                        //}
                                        originalData = null;
                                    }
                                    else
                                    {
                                        //フィルタ後のresultValueを取得する。
                                        string filteredRstVal = Questions[i].Sectors[j].Criteria.GetCountResult(r);

                                        writer.WriteLine(filteredRstVal.ToString());
                                    }
                                }
                            }
                            writer.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    System.Diagnostics.Debug.Indent();
                    System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                    System.Diagnostics.Debug.Unindent();
                    throw;
                }
            }
        }
        #endregion

        #region COMPUTEのデータ加工関連クラス
        /// <summary>
        /// COMPUTEのデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("E63A2F72-E839-416a-B9CB-93BAC8A69341")]
        public class DataProcessCompute : DataProcess
        {
            /// <summary>アプリケーション環境設定クラス</summary>
            protected ApplicationConfig appConfig = null;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="collection">親コレクションのDataProcessesクラスのインスタンスへの参照</param>
            /// <param name="dataprocesscode">データ加工の種類を表すDataProcessCode列挙型の値</param>
            protected DataProcessCompute(DataProcesses collection, DataProcessCode dataprocesscode) : base(collection, dataprocesscode) { }

            internal DataProcessCompute(DataProcesses collection) : base(collection, DataProcessCode.Compute) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
                // COMPUTEのDB登録処理
            }

            /// <summary>
            /// データ加工を実行するメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Execute()
            {
                if (Questions == null) return;
                if (!RunFlag) return;
                if (ParentCollection == null || string.IsNullOrWhiteSpace(ParentCollection.DataDirectoryPath)) return;
                try
                {
                    QueryItemName query = new QueryItemName();
                    for (int i = 0; i < Questions.Count; ++i)
                    {
                        //string formulaStr = (Questions[i] as NewQuestions.NewQuestion).FormulaString;
                        //List<string> itemNamesList = new List<string>();
                        //bool allowStandardFunction = this is DataProcessGroup ? true : false;
                        ////// 式の構文チェック
                        //double retVal = GlobalMethodClass.ParseExpression(ParentCollection.QcWebId, formulaStr, allowStandardFunction, itemNamesList);
                        //if (double.IsNaN(retVal)) continue;

                        string path = GetNewItemFilePath(i);
                        string tmpFilePath = path + DataProcessCommon.GetExtension(GlobalsCommonConstant.fileExtension.tmp);
                        //using (System.IO.StreamWriter writer = new System.IO.StreamWriter(tmpFilePath, false, Encoding.UTF8))
                        //{
                        // アイテムIDとアイテム名のKey-Value作成
                        //Dictionary<string, decimal> itemNameIdList = new Dictionary<string, decimal>();
                        //for (int itemIndex = 0; itemIndex < itemNamesList.Count; ++itemIndex)
                        //{
                        //    decimal itemid = query.QuestionNameToID(
                        //        ParentCollection.QcWebId, QuestionType.SA | QuestionType.N, itemNamesList[itemIndex]);
                        //    if (!itemNameIdList.ContainsKey(itemNamesList[itemIndex]))
                        //    {
                        //        itemNameIdList.Add(itemNamesList[itemIndex], itemid);
                        //    }
                        //}

                        //Dictionary<string, double> itemValuNameList = new Dictionary<string, double>();
                        //// サンプルIDのループ
                        //for (int r = 0; r < ParentCollection.SamplesCount; ++r)
                        //{
                        //    bool isNoAnswer = false; // 計算結果を「DK:無回答」にする場合、trueにする
                        //    bool isAllNoAnswer = true; // 「DK:無回答」以外の値があったらfalseにする
                        //    bool isAllUnMatch = true; // 「*:非該当」以外の値があったらfalseにする

                        //    itemValuNameList.Clear();
                        //    // アイテム名とサンプル値のKey-Value作成
                        //    for (int j = 0; j < itemNamesList.Count; ++j)
                        //    {
                        //       // decimal itemid = itemNameIdList[itemNamesList[j]];
                        //        //Data orgData = ParentCollection.GetRawData(r, itemid);

                        //        // GROUP
                        //        //if (this is DataProcessGroup)
                        //        //{
                        //        //    // 元アイテム全てが「DK：無回答」の場合、結果は「DK：無回答」
                        //        //    isAllNoAnswer = !orgData.IsNA ? false : isAllNoAnswer;
                        //        //    // 元アイテム全てが「*：非該当」の場合、結果は「*：非該当」
                        //        //    isAllUnMatch = !orgData.IsIV ? false : isAllUnMatch;

                        //        //    // 元アイテムの一部が「DK：無回答」の場合、そのデータを除いて演算する
                        //        //    // 元アイテムの一部が「*：非該当」の場合、そのデータを除いて演算する
                        //        //    if (orgData.IsNA || orgData.IsIV) continue;
                        //        //}
                        //        //// COMPUTE
                        //        //else if (this is DataProcessCompute)
                        //        //{
                        //        //    // 演算に使用されているアイテムに「DK：無回答」が含まれている場合、結果は「DK:無回答」
                        //        //    // 演算に使用されているアイテムに「*：非該当」が含まれている場合、結果は「DK：無回答」
                        //        //    if (orgData.IsNA || orgData.IsIV)
                        //        //    {
                        //        //        isNoAnswer = true;
                        //        //        break;
                        //        //    }
                        //        //}

                        //        // Key-Valueにサンプル値を設定
                        //        //if (!itemValuNameList.ContainsKey(itemNamesList[j]))
                        //        //{
                        //        //    if (orgData.GetType() == typeof(SAData))
                        //        //    {
                        //        //        itemValuNameList.Add(itemNamesList[j], (orgData as SAData).Value);
                        //        //    }
                        //        //    else if (orgData.GetType() == typeof(NData))
                        //        //    {
                        //        //        itemValuNameList.Add(itemNamesList[j], (orgData as NData).Value);
                        //        //    }
                        //        //}
                        //    }

                        //    // 結果を「DK：無回答」とする																							
                        //    //if (isNoAnswer || (isAllNoAnswer && this is DataProcessGroup))
                        //    //{
                        //    //    writer.WriteLine(string.Empty);
                        //    //    continue;
                        //    //}

                        //    //// 結果を「DK：無回答」とする																							
                        //    //if (isAllUnMatch && this is DataProcessGroup)
                        //    //{
                        //    //    writer.WriteLine(string.Empty);
                        //    //    continue;
                        //    //}

                        //    //有効な数値が無い場合,(DKと*が混在のみ)、結果を「DK：無回答」とする	(GROUPとCOMPUTE通用)																				
                        //    //if (itemValuNameList.Count == 0)
                        //    //{
                        //    //    writer.WriteLine(string.Empty);
                        //    //    continue;
                        //    //}


                        //    // アイテム名を値に置換した数式を出力する
                        //    string formulaValue = GlobalMethodClass.BuildExpression(formulaStr, itemNamesList, itemValuNameList);
                        //    //↓のコードは性能面でComputeExpressionに劣ったので未使用
                        //    //string formulaValue = CommonExtensions.ParseExpression(formulaStr, ParentCollection.QcWebId, allowStandardFunction, null, itemValuNameList).ToString();
                        //    if (!string.IsNullOrEmpty(formulaValue))
                        //    {
                        //        writer.WriteLine(formulaValue);
                        //        continue;
                        //    }
                        //}
                        //writer.Close();
                        //}

                        // 外部プロセスで数式の計算を実行する
                        /*
                        QuillInjector.GetInstance().Inject(this);
                        string exe_path = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_COMPUTE_EXPRESSION_PATH);
                        string program = System.IO.Path.Combine(exe_path, GlobalsCommonConstant.computeExpressionExe);

                        // パス ファイル名
                        StringBuilder argsBuilder = new StringBuilder(ParentCollection.DataDirectoryPath);
                        argsBuilder.Append(" " + Questions[i].ItemId 
                            + DataProcessCommon.GetExtension(GlobalsCommonConstant.fileExtension.dp));

                        string arguments = argsBuilder.ToString();

                        System.Diagnostics.Process extProcess = new Process();
                        extProcess.StartInfo.CreateNoWindow = true;
                        extProcess.StartInfo.ErrorDialog = false;
                        extProcess.StartInfo.FileName = program;	//起動するファイル名
                        extProcess.StartInfo.Arguments = arguments;	//起動時の引数
                        extProcess.Start(); //プロセス開始
                        extProcess.WaitForExit();
                        extProcess.Close(); //プロセスクローズ
                         */

                        int result = GlobalMethodClass.ComputeProcess(tmpFilePath, path);
                        if (result != 0)
                        {
                            //throw new QCWebException("QCCMN05010101"
                            //                            , new string[] { result.ToString(), tmpFilePath, path }
                            //                            , GlobalsCommonConstant.LogLevel.FATAL
                            //                            , null);

                            throw new QCWebException(new Message(Constants.CommonMessageIndex.ComputeProcessFailedMessageIndex)
                                                        , GlobalsCommonConstant.LogLevel.FATAL
                                                        , result.ToString(), tmpFilePath, path);
                        }

                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    System.Diagnostics.Debug.Indent();
                    System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                    System.Diagnostics.Debug.Unindent();
                    throw;
                }
            }
        }
        #endregion


        #region GROUPのデータ加工関連クラス
        /// <summary>
        /// GROUPのデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("40EB3AE6-8E25-40cb-9EF7-EB541AB9D06F")]
        public class DataProcessGroup : DataProcessCompute
        {
            internal DataProcessGroup(DataProcesses collection) : base(collection, DataProcessCode.Group) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
            }
        }
        #endregion

        /// <summary>
        /// ウエイトバック設定のデータ加工情報を保持するクラス
        /// </summary>
        [ComVisible(false), Guid("0D95FAB3-1752-49c6-9937-CEF59DDC8852")]
        public class DataProcessWeightBack : DataProcess
        {
            internal DataProcessWeightBack(DataProcesses collection) : base(collection, DataProcessCode.SetWeightBack) { }

            /// <summary>
            /// 加工設定内容のDB登録処理を行うメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Regist()
            {
            }

            /// <summary>
            /// データ加工を実行するメソッド
            /// </summary>
            [Seasar.Quill.Attrs.Transaction]
            public override void Execute()
            {
            }
        }
        #endregion

        private int samplesCount = 0;
        /// <summary>
        /// サンプル数を取得/設定するプロパティ
        /// <note>設定は1回のみ可能</note>
        /// </summary>
        public int SamplesCount
        {
            get
            {
                return samplesCount;
            }
            set
            {//changed by 191  for LDEL ;samples count need to update when deleted other wise index out of bountd err
                samplesCount = value;// if (samplesCount == 0 && value > 0) samplesCount = value;
            }
        }

        protected int sortnumber = 0;
        protected int deletetedrowscount = 0;
        /// <summary>
        /// DP-SortNumber
        /// </summary>
        public int SortNumber
        {
            get
            {
                return sortnumber;
            }
            set
            {
                sortnumber = value;
            }
        }
        public int DeletedRowCount
        {
            get
            {
                return deletetedrowscount;
            }
            set
            {
                deletetedrowscount = value;
            }
        }
        protected string connectionString = string.Empty;
        private bool fromSubTotal = false;
        private string tableName = "answers";

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                if (string.IsNullOrEmpty(connectionString)) connectionString = value;
            }
        }

        private string dataDirectoryPath = null;
        /// <summary>
        /// データフォルダのパスを取得/設定するプロパティ
        /// <note>設定は1回のみ可能</note>
        /// </summary>
        public string DataDirectoryPath
        {
            get
            {
                return dataDirectoryPath;
            }
            set
            {
                if (dataDirectoryPath == null) dataDirectoryPath = value;
            }
        }

        private decimal qcwebid = 0;
        /// <summary>
        /// QCWEBIDを取得/設定するプロパティ
        /// <note>設定は1回のみ可能</note>
        /// </summary>
        public decimal QcWebId
        {
            get
            {
                return qcwebid;
            }
            set
            {
                if (qcwebid == 0) qcwebid = value;
            }
        }

        /// <summary>
        /// ローデータファイルを保持するプロパティ
        /// KeyにアイテムID、Valueにローデータを格納する
        /// </summary>
        private Dictionary<decimal, List<Tabulation.Data>> dataDict = new Dictionary<decimal, List<Tabulation.Data>>();

        /// <summary>
        /// filterキー、filter結果を保持するプロパティ
        /// Keyに(questionid+operator+CriteriaValueDescription)、Valueにfilteringの結果を格納する
        /// </summary>
        private Dictionary<string, bool[]> filterResultDict = new Dictionary<string, bool[]>();

        /// <summary>
        /// DataProcessクラスのインスタンスを生成して、要素に追加するメソッド
        /// </summary>
        /// <param name="dataprocesscode">データ加工の種類を表すDataProcessCode列挙型の値</param>
        /// <returns>生成したインスタンスへの参照</returns>
        public IDataProcess Add(DataProcessCode dataprocesscode)
        {
            if (!Enum.IsDefined(typeof(DataProcessCode), dataprocesscode)) return null;
            IDataProcess newitem = null;
            switch (dataprocesscode)
            {
                case DataProcessCode.Integrate:
                    newitem = new DataProcessIntegrate(this);
                    break;
                case DataProcessCode.Recode:
                    newitem = new DataProcessRecode(this);
                    break;
                case DataProcessCode.MConvert:
                    newitem = new DataProcessMConvert(this);
                    break;
                case DataProcessCode.Class:
                    newitem = new DataProcessClass(this);
                    break;
                case DataProcessCode.CategorizeResponseCount:
                    newitem = new DataProcessCategorizeCount(this);
                    break;
                case DataProcessCode.MtoS:
                    newitem = new DataProcessMtoS(this);
                    break;
                case DataProcessCode.DeleteData:
                    newitem = new DataProcessDeleteData(this);
                    break;
                case DataProcessCode.ModifyData:
                    newitem = new DataProcessModifyData(this);
                    break;
                case DataProcessCode.ResponseCount:
                    newitem = new DataProcessCount(this);
                    break;
                case DataProcessCode.Compute:
                    newitem = new DataProcessCompute(this);
                    break;
                case DataProcessCode.Group:
                    newitem = new DataProcessGroup(this);
                    break;
                case DataProcessCode.SetWeightBack:
                    newitem = new DataProcessWeightBack(this);
                    break;
                default:
                    return null;    // ここには来ない
            }
            // ウエイトバック設定は末尾位置で固定する
            if (this.Count > 0 && this[this.Count - 1].DataProcessCode == DataProcessCode.SetWeightBack)
            {
                this.Insert(this.Count - 1, newitem);
            }
            else
            {
                this.Add(newitem);
            }
            return newitem;
        }

        /// <summary>
        /// データ加工を実行するメソッド
        /// </summary>
        [Seasar.Quill.Attrs.Transaction]
        public void Execute()
        {
            if (string.IsNullOrWhiteSpace(dataDirectoryPath)) return;
            string[] backuppath = null;
            byte[][] backup = null;
            try
            {
                // ロールバック用データ確保
                //backuppath = System.IO.Directory.GetFiles(dataDirectoryPath, "*.dp*");
                //backup = new byte[backuppath.Length][];
                //for (int i = 0; i < backuppath.Length; ++i)
                //{
                //    backup[i] = System.IO.File.ReadAllBytes(backuppath[i]);
                //    System.IO.File.Delete(backuppath[i]);
                //}

                string[] sourceDivArray = new string[] { CDef.SourceDiv.Original.Code, CDef.SourceDiv.DataEdit.Code };

                //Questions = new Question.Questions(QcWebId, sourceDivArray);

                // 各データ加工
                foreach (IDataProcess dataprocess in this)
                {
                    //if (dataprocess is DataProcessIntegrate)
                    //{
                    //    (dataprocess as DataProcessIntegrate).Execute();
                    //}
                    dataprocess.Execute();
                }
            }
            catch (Exception e)
            {
                // 中途成果物の掃除
                try
                {
                    string[] paths = System.IO.Directory.GetFiles(dataDirectoryPath, "*.dp");
                    for (int i = 0; i < paths.Length; ++i)
                    {
                        try
                        {
                            System.IO.File.Delete(paths[i]);
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
                if (backuppath != null && backup != null)
                {
                    // ロールバック
                    for (int i = 0; i < backuppath.Length; ++i)
                    {
                        try
                        {
                            System.IO.File.WriteAllBytes(backuppath[i], backup[i]);
                        }
                        catch
                        {
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                System.Diagnostics.Debug.Indent();
                System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                System.Diagnostics.Debug.Unindent();
                throw;
            }
        }

        /// <summary>
        /// カテゴリ出力編集RAWDATA作成
        /// または該当数取得一時ファイル作成を実行するメソッド
        /// </summary>
        [Seasar.Quill.Attrs.Transaction]
        public void OtherExecute()
        {
            if (string.IsNullOrWhiteSpace(dataDirectoryPath)) return;
            List<string> newFilePath = new List<string>();
            try
            {
                // 新規のみのため、ロールバック用データ確保不要
                //データ加工生成対象ファイルパスのみ格納する。
                foreach (IDataProcess dataprocess in this)
                {
                    DataProcess dp = dataprocess as DataProcess;
                    if (dp.Questions == null) return;
                    if (!dp.RunFlag) return;
                    if (dp.ParentCollection == null || string.IsNullOrWhiteSpace(dp.ParentCollection.DataDirectoryPath)) return;
                    for (int i = 0; i < dp.Questions.Count; ++i)
                    {
                        var newQuestion = dp.Questions[i] as NewQuestions.NewQuestion;
                        GlobalsCommonConstant.fileExtension fileExt = newQuestion.ChangeExtension;
                        string path = dp.GetNewItemFilePath(i, fileExt);

                        //生成対象ファイルパスを格納
                        newFilePath.Add(path);
                    }
                }

                string[] sourceDivArray = new string[] { CDef.SourceDiv.Original.Code, CDef.SourceDiv.DataEdit.Code };
                Questions = new Question.Questions(QcWebId, sourceDivArray);

                // 各データ加工
                foreach (IDataProcess dataprocess in this)
                {
                    dataprocess.Execute();
                }
            }
            catch (Exception e)
            {
                // 中途成果物の掃除
                try
                {
                    foreach (string p in newFilePath)
                    {
                        try
                        {
                            System.IO.File.Delete(p);
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }

                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                System.Diagnostics.Debug.Indent();
                System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                System.Diagnostics.Debug.Unindent();
                throw;
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (IDataProcess dataprocess in this)
            {
                dataprocess.Dispose();
            }
            dataDict = null;
            filterResultDict = null;
        }

        /// <summary>
        /// 指定アイテムの対象行ローデータを取得する
        /// </summary>
        /// <param name="index">行</param>
        /// <param name="itemid">アイテムID</param>
        /// <returns>ローデータ</returns>
        public Data GetRawData(int index, decimal itemid)
        {
            List<Data> data = null;
            if (!dataDict.ContainsKey(itemid))
            {
                data = ReadRawData(itemid);
                dataDict.Add(itemid, data);
            }
            else
            {
                data = dataDict[itemid];
            }
            return data[index];
        }

        //code by 191 for getting  full column data of rrspective item id for  putting unprocessed data in file
        //public Data GetRawData(decimal itemid,)
        //{
        //    List<Data> data = null;
        //    if (!dataDict.ContainsKey(itemid))
        //    {
        //        data = ReadRawData(itemid);
        //        dataDict.Add(itemid, data);
        //    }
        //    else
        //    {
        //        data = dataDict[itemid];
        //    }
        //    return data[0];
        //}
        ////////////////

        /// <summary>
        /// 指定アイテムの対象行ローデータを取得する
        /// </summary>
        /// <param name="itemid">アイテムID</param>
        /// <returns>ローデータ</returns>
        public List<Data> GetRawData(decimal itemid)
        {
            List<Data> data = null;
            if (!dataDict.ContainsKey(itemid))
            {
                data = ReadRawData(itemid);
                dataDict.Add(itemid, data);
            }
            else
            {
                data = dataDict[itemid];
            }
            return data;
        }

        /// <summary>
        /// データ修正で作成したローデータを再登録する
        /// </summary>
        /// <param name="itemid">アイテムID</param>
        /// <param name="newDatas">Newローデータ</param>
        public void SetRawData(decimal itemid, List<Data> newDatas)
        {
            if (dataDict.ContainsKey(itemid))
            {
                dataDict.Remove(itemid);
            }
            dataDict.Add(itemid, newDatas);
        }

        /// <summary>
        /// ローデータファイルの読み込み
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private List<Data> ReadRawData(decimal itemid, QuestionType qt = QuestionType.SA)
        {
            string filepath = null;
            QuestionType questiontype = qt;
            QCWebException exception = null;
            if (questions == null)
            {
                //*****************_191  commented for sample id becomes 0 -> throws error_*************************

                // throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyDataMessageIndex), GlobalsCommonConstant.LogLevel.ERROR, "questions");
            }
            //List<Data> dataList = ReadTextFile.ReadData2(itemid, DataDirectoryPath, out filepath
            //                                            , out questiontype, out exception, false, true);
            //List<Data> dataList = ReadTextFile.ReadData2(GetQuestion(itemid), DataDirectoryPath, out filepath
            //                                            , out questiontype, out exception, false, true);
            var path = Path.Combine(DataProcessCommon.GetProcessIdPath() + @"\", itemid.ToString() + ".txt");
            var dpPath = Path.Combine(DataProcessCommon.GetProcessIdPath() + @"\", itemid.ToString() + ".dp");
            List<Data> dataList = new List<Data>();
            if ((File.Exists(path) || File.Exists(dpPath)) && !fromSubTotal)
            {
                string filePath = File.Exists(path) ? path : dpPath;
                FileInfo info = new FileInfo(filePath);
                if (info.Length > 0)
                {
                    try
                    {
                        QuestionType QType = QuestionType.SA;
                        using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(ConnectionString))
                        {
                            con.Open();

                            string ansType = DBHelper.GetAnswertype(itemid, con);
                            switch (ansType)
                            {
                                case "MA":
                                    QType = QuestionType.MA;
                                    break;
                                case "SA":
                                    QType = QuestionType.SA;
                                    break;
                                case "N":
                                    QType = QuestionType.N;
                                    break;
                                case "FA":
                                    QType = QuestionType.FA;
                                    break;

                            }
                            con.Close();
                        }
                        dataList = ReadTextFile.ReadData(filePath, QType, null, out exception, false, this[0].IsTreatasZero);

                        return dataList;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
            if (this[0] is DataProcessRecode)
            {
                QuestionType SourceQuestionType = QuestionType.SA;
                if (/*fromSubTotal &&*/ this[0].Questions[0] != null && this[0].Questions[0].SourceQuestionType != null)
                {
                    SourceQuestionType = this[0].Questions[0].SourceQuestionType;
                }
                //   dataList = ReadTextFile.ReadData(path, QuestionType.SA, null, out exception, false);
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(ConnectionString))
                {
                    con.Open();
                    System.Data.DataTable dataTble = null;
                    if (fromSubTotal)
                    {
                        if (itemid == 0)//191  added for sample id
                        {
                            dataTble = DBHelper.GetDataTable("Select sample_id from " + tableName + " order by sort_no ", con);
                        }
                        else
                        {
                            if (IsAnVar(itemid.ToString(), con))
                                dataTble = DBHelper.GetDataTable("Select m.q_" + itemid + " from multivariate_temp m join " + tableName + " a on a.sort_no = m.sort_no order by m.sort_no ", con);
                            else
                                dataTble = DBHelper.GetDataTable("Select q_" + itemid + " from " + tableName + " order by sort_no ", con);
                        }
                    }
                    else
                    {
                        if (itemid == 0)//191  added for sample id
                        {
                            dataTble = DBHelper.GetDataTable("Select sample_id from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                        }
                        else
                        {
                            if (IsAnVar(itemid.ToString(), con))
                                dataTble = DBHelper.GetDataTable("Select m.q_" + itemid + " from multivariate_temp m join data_after_process a on a.sort_no = m.sort_no where m.sort_no > " + sortnumber.ToString() + " order by m.sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                            else
                                dataTble = DBHelper.GetDataTable("Select q_" + itemid + " from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                        }
                    }
                    SourceQuestionType = DBHelper.GetAnswertype(itemid, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(itemid, con) == "FA" ? QuestionType.FA : (DBHelper.GetAnswertype(itemid, con) == "N" ? QuestionType.N : qt));//Redmine id:177634
                    dataList = ReadTextFile.ReadDataTable(dataTble, SourceQuestionType, null, out exception, false);
                    if (exception != null) throw exception;
                    return dataList;
                }
            }
            else if (this[0] is DataProcessMConvert || this[0] is DataProcessModifyData)
            {
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(ConnectionString))
                {

                    con.Open();
                    System.Data.DataTable dataTble;
                    if (itemid == 0)//191  added for sample id
                    { dataTble = DBHelper.GetDataTable("Select sample_id from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con); }
                    else
                    {
                        if (IsAnVar(itemid.ToString(), con))
                            dataTble = DBHelper.GetDataTable("Select m.q_" + itemid + " from multivariate_temp m join data_after_process a on a.sort_no = m.sort_no where m.sort_no > " + sortnumber.ToString() + " order by m.sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                        else
                            dataTble = DBHelper.GetDataTable("Select q_" + itemid + " from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                    }
                    questiontype = DBHelper.GetAnswertype(itemid, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(itemid, con) == "FA" ? QuestionType.FA : (DBHelper.GetAnswertype(itemid, con) == "SA" ? QuestionType.SA : (DBHelper.GetAnswertype(itemid, con) == "N" ? QuestionType.N : qt)));//191 added N type for := source decimal not showing issue
                    dataList = ReadTextFile.ReadDataTable(dataTble, questiontype, null, out exception, false);
                    if (exception != null) throw exception;
                    return dataList;
                }
            }
            else if (this[0] is DataProcessClass)
            {
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(ConnectionString))
                {

                    con.Open();
                    System.Data.DataTable dataTble;

                    if (itemid == 0)//191  added for sample id
                    { dataTble = DBHelper.GetDataTable("Select sample_id from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con); }
                    else
                    {
                        string column = "q_" + itemid;
                        bool isAn = IsAnVar(itemid.ToString(), con);
                        if (this[0].IsTreatasZero)
                        {
                            if (isAn)
                                column = "case when(m." + column + " == NULL or m." + column + " == '') THEN '0' else ifnull(m." + column + ",'0') END as m." + column;
                            else
                                column = "case when(" + column + " == NULL or " + column + " == '') THEN '0' else ifnull(" + column + ",'0') END as " + column;// "ifnull(" + column + ",'0') " + column;//"CAST(" + column + " AS INT) as " + column;
                                                                                                                                                               // "case when(" + column +" == NULL or " + column +" == '') THEN '0' else " + column + " END as "++ column 
                                                                                                                                                               //Select case when (q_1==NULL or q_1=='') THEN '0' else q_1 END as q_1

                        }
                        if (isAn)
                            dataTble = DBHelper.GetDataTable("Select " + column + " from multivariate_temp m join data_after_process a on a.sort_no = m.sort_no where m.sort_no > " + sortnumber.ToString() + " order by m.sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                        else
                            dataTble = DBHelper.GetDataTable("Select " + column + " from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);

                    }
                    questiontype = DBHelper.GetAnswertype(itemid, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(itemid, con) == "N" ? QuestionType.N : (DBHelper.GetAnswertype(itemid, con) == "FA" ? QuestionType.FA : qt));//191 
                    dataList = ReadTextFile.ReadDataTable(dataTble, questiontype, null, out exception, false);
                    if (exception != null) throw exception;
                    return dataList;
                }

            }
            else if (this[0] is DataProcessCount || this[0] is DataProcessCategorizeCount)
            {
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(ConnectionString))
                {

                    con.Open();
                    System.Data.DataTable dataTble;
                    if (itemid == 0)//191  added for sample id
                    { dataTble = DBHelper.GetDataTable("Select sample_id from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con); }
                    else
                    {
                        if (IsAnVar(itemid.ToString(), con))
                            dataTble = DBHelper.GetDataTable("Select m.q_" + itemid + " from multivariate_temp m join data_after_process a on a.sort_no = m.sort_no where m.sort_no > " + sortnumber.ToString() + " order by m.sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                        else
                            dataTble = DBHelper.GetDataTable("Select q_" + itemid + " from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                    }
                    questiontype = DBHelper.GetAnswertype(itemid, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(itemid, con) == "FA" ? QuestionType.FA : DBHelper.GetAnswertype(itemid, con) == "N" ? QuestionType.N : qt);//191 //DBHelper.GetAnswertype(itemid, con) == "N" ? QuestionType.N :
                    dataList = ReadTextFile.ReadDataTable(dataTble, questiontype, null, out exception, false);// QuestionType.MA
                    if (exception != null) throw exception;
                    return dataList;
                }

            }
            else if (this[0] is DataProcessMtoS)
            {
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(ConnectionString))
                {

                    con.Open();
                    System.Data.DataTable dataTble;
                    if (itemid == 0)
                    { dataTble = DBHelper.GetDataTable("Select sample_id from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con); }
                    else
                    {
                        if (IsAnVar(itemid.ToString(), con))
                            dataTble = DBHelper.GetDataTable("Select m.q_" + itemid + " from multivariate_temp m join data_after_process a on a.sort_no = m.sort_no where m.sort_no > " + sortnumber.ToString() + " order by m.sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                        else
                            dataTble = DBHelper.GetDataTable("Select q_" + itemid + " from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                    }
                    //  questiontype = DBHelper.GetAnswertype(itemid, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(itemid, con) == "FA" ? QuestionType.FA : qt);//191 
                    //Redmine id: 175557
                    questiontype = DBHelper.GetAnswertype(itemid, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(itemid, con) == "N" ? QuestionType.N : (DBHelper.GetAnswertype(itemid, con) == "FA" ? QuestionType.FA : qt));
                    dataList = ReadTextFile.ReadDataTable(dataTble, questiontype, null, out exception, false);//QuestionType.MA
                    if (exception != null) throw exception;
                    return dataList;
                }

            }
            else if (this[0] is DataProcessDeleteData)
            {
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(ConnectionString))
                {

                    con.Open();
                    System.Data.DataTable dataTble;
                    if (itemid == 0)//191  added for sample id
                    { dataTble = DBHelper.GetDataTable("Select sample_id from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con); }
                    else
                    {
                        if (IsAnVar(itemid.ToString(), con))
                            dataTble = DBHelper.GetDataTable("Select m.q_" + itemid + " from multivariate_temp m join data_after_process a on a.sort_no = m.sort_no where m.sort_no > " + sortnumber.ToString() + " order by m.sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                        else
                            dataTble = DBHelper.GetDataTable("Select q_" + itemid + " from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                    }
                    questiontype = DBHelper.GetAnswertype(itemid, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(itemid, con) == "FA" ? QuestionType.FA : DBHelper.GetAnswertype(itemid, con) == "N" ? QuestionType.N : qt);//191 
                    if (itemid == 0)
                    {
                        //questiontype = QuestionType.FA; // 191  added for sample id becomes 0, SA  Type throws error,so made FA Forcfuly
                    }
                    dataList = ReadTextFile.ReadDataTable(dataTble, questiontype, null, out exception, false);
                    int count = dataTble.Rows.Count;
                    if (exception != null) throw exception;
                    return dataList;
                }

            }
            else if (this[0] is DataProcessIntegrate)
            {
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(ConnectionString))
                {

                    con.Open();
                    System.Data.DataTable dataTble;
                    if (itemid == 0)//191  added for sample id
                    { dataTble = DBHelper.GetDataTable("Select sample_id from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con); }
                    else
                    {
                        if (IsAnVar(itemid.ToString(), con))
                            dataTble = DBHelper.GetDataTable("Select m.q_" + itemid + " from multivariate_temp m join data_after_process a on a.sort_no = m.sort_no where m.sort_no > " + sortnumber.ToString() + " order by m.sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                        else
                            dataTble = DBHelper.GetDataTable("Select q_" + itemid + " from data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (QcWebCommon.Common.Constants.MAX_ROW_COUNT - deletetedrowscount).ToString(), con);
                    }
                    questiontype = DBHelper.GetAnswertype(itemid, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(itemid, con) == "FA" ? QuestionType.FA : DBHelper.GetAnswertype(itemid, con) == "N" ? QuestionType.N : qt);//191  27-10-19
                    dataList = ReadTextFile.ReadDataTable(dataTble, questiontype, null, out exception, false);//  dataList = ReadTextFile.ReadDataTable(dataTble, QuestionType.SA, null, out exception, false);
                    if (exception != null) throw exception;
                    return dataList;
                }
            }

            //(Constants.MAX_ROW_COUNT - deletedrows)

            return dataList;
        }

        private bool IsAnVar(string itemid, System.Data.SQLite.SQLiteConnection con)
        {
            return DBHelper.GetDataTable("select question_flag from question where id = " + itemid, con).Rows[0][0].ToString() == "An";
        }

        /// <summary>
        /// ローデータ情報が登録済みかを判定する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public bool IsRawData(decimal itemid)
        {
            return dataDict.ContainsKey(itemid);
        }

        /// <summary>
        /// 指定対象行filterResult情報を取得する
        /// </summary>
        /// <param name="filterKey">filterKey</param>
        /// <returns>filter結果</returns>
        public bool[] GetFilterResultData(string filterKey)
        {
            if (filterResultDict.ContainsKey(filterKey))
            {
                return filterResultDict[filterKey];
            }
            return null;
        }

        /// <summary>
        /// データ修正で作成したfilterResult情報を再登録する
        /// </summary>
        /// <param name="filterKey">アイテムID</param>
        /// <param name="resultDatas">NewFilter結果</param>
        public void SetFilterResultData(string filterKey, bool[] resultDatas)
        {
            if (filterResultDict.ContainsKey(filterKey))
            {
                filterResultDict.Remove(filterKey);
            }

            filterResultDict.Add(filterKey, resultDatas);
        }

        /// <summary>
        /// filterResult情報が登録済みかを判定する
        /// </summary>
        /// <param name="filterKey"></param>
        /// <returns></returns>
        public bool IsFilterResultData(string filterKey)
        {
            return filterResultDict.ContainsKey(filterKey);
        }

        /// <summary>
        /// filterResult情報をクリアする。
        /// </summary>
        public void FilterResultDataClear()
        {
            filterResultDict.Clear();
        }


        private Question.Questions questions = null;
        private readonly object DictUpdate;

        /// <summary>
        /// Question.Questionsを取得/設定するプロパティ
        /// </summary>
        public Question.Questions Questions
        {
            set
            {
                if (questions == null) questions = value;
            }
            get
            {
                return questions;
            }
        }

        public bool FromSubTotal { get => fromSubTotal; set => fromSubTotal = value; }
        public string TableName { get => tableName; set => tableName = value; }

        /// <summary>
        /// 指定のアイテムのQuestionの参照を返す
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public Question.Questions.Question GetQuestion(decimal itemid)
        {
            return questions == null ? null : questions[itemid] as Question.Questions.Question;
        }
    }

    /// <summary>
    /// データ加工共通処理クラス
    /// </summary>
    [Implementation]
    public class DataProcessCommon
    {
        /// <summary>データ加工リストBhv</summary>
        protected TDataEditListBhv tDataEditListBhv = null;
        /// <summary>アプリ環境設定情報</summary>
        protected ApplicationConfig appConfig = null;

        /// <summary>
        /// データ加工処理を再実行する必要があるか判定する
        /// </summary>
        /// <param name="qcwebid">QCWEBID</param>
        /// <param name="dirpath">ファイル格納パス(省略可、既定値:null)</param>
        /// <returns>再実行の必要あり->true 再実行の必要なし->false</returns>
        public bool IsDataProcessReExecute(decimal qcwebid, string dirpath = null)
        {
            if (dirpath == null)
            {
                dirpath = System.IO.Path.Combine(
                                    appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_RAWDATA_PATH_AP)
                                    , qcwebid.ToString());
            }

            string[] dpfiles = System.IO.Directory.GetFiles(dirpath, "*.dp");

            //ウエイトバックのファイルは除く
            List<string> dpFilePaths = new List<string>();
            string wbpath = System.IO.Path.Combine(dirpath, DataIoConstant.WEIGHTBACK_TEXT_FILE_NAME);
            if (dpfiles.Length > 0)
            {
                foreach (string filepath in dpfiles)
                {
                    if (wbpath.Equals(filepath)) continue;
                    dpFilePaths.Add(filepath);
                }
            }

            bool isExistDPFile = (dpFilePaths.Count > 0);

            QuillInjector.GetInstance().Inject(this);

            TDataEditListCB cb = new TDataEditListCB();
            cb.Query().SetQcwebid_Equal(qcwebid);
            cb.Query().SetStatus_Equal("1");
            cb.Query().AddOrderBy_LastUpdateDatetime_Desc();
            ListResultBean<TDataEditList> list = tDataEditListBhv.SelectList(cb);

            //ケース１：.dpファイルが存在しない、かつ、データ加工リストテーブルに実行済のデータが存在しない
            if (!isExistDPFile && list.Count <= 0) return false;

            //ケース２：.dpファイルが存在しない、かつ、データ加工リストテーブルに実行済のデータが存在する
            if (!isExistDPFile && list.Count > 0) return true;

            //ケース３：.dpファイルが存在する、かつ、データ加工リストテーブルに実行済のデータが存在しない
            if (isExistDPFile && list.Count <= 0) return true;

            DateTime dbModifiedTime = (DateTime)list[0].LastUpdateDatetime;
            //DateTime fileCreated = System.IO.File.GetCreationTime(dpfiles[0]);
            //ファイルシステムのトンネリング機能によって、作成日時が引き継がれてしまうので更新日時を使い比較する
            DateTime fileCreated = System.IO.File.GetLastWriteTime(dpFilePaths[0]);

            //ケース５：.dpファイルが存在する、かつ、データ加工リストテーブルに実行済のデータが存在する、かつ、.dpファイルの作成日時がＤＢデータよりも小さい
            if (fileCreated < dbModifiedTime) return true;

            //ケース４：.dpファイルが存在する、かつ、データ加工リストテーブルに実行済のデータが存在する、かつ、.dpファイルの作成日時がＤＢデータよりも大きい
            return false;
        }

        /// <summary>
        /// ファイル拡張子の文字列を返す
        /// </summary>
        /// <param name="fileExtChange">ファイル拡張子を識別するEnum</param>
        /// <returns>ファイル拡張子の文字列</returns>
        public static string GetExtension(GlobalsCommonConstant.fileExtension fileExtChange)
        {
            string extension = string.Empty;
            switch (fileExtChange)
            {
                case GlobalsCommonConstant.fileExtension.txt:
                    extension = ".txt";
                    break;
                case GlobalsCommonConstant.fileExtension.tmp:
                    extension = ".tmp";
                    break;
                case GlobalsCommonConstant.fileExtension.dp2:
                    extension = ".dp2";
                    break;
                default:
                    extension = ".dp";
                    break;
            }
            return extension;
        }
        public static string GetProcessIdPath()//#212092 
        {
            string outputfilepath = System.IO.Path.Combine(Path.GetTempPath(), "QC4\\");
            ApplicationConfig appConfig = new ApplicationConfig();
            outputfilepath =
    System.IO.Path.Combine(
        appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP));
            GlobalMethodClass.GuaranteeDirectoryExist(outputfilepath);

            return outputfilepath;
        }

    }
}
