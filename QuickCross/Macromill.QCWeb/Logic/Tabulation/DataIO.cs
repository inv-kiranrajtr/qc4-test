#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：EnumeratedType.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/7/24
 * 作　成　者：井川はるき
 * 更　新　日：2012/8/3
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion

#define AFTER_2ND_PHASE
#define IS_2ND_PHASE
#undef AFTER_2ND_PHASE
#undef IS_2ND_PHASE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Exceptions;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Macromill.QCWeb.Logic.Common;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.ExDao.PmBean;
using Macromill.QCWeb.Dao.ExEntity.Customize;
using Macromill.QCWeb.DataProcess;
using Seasar.Quill.Database.Tx;
using Seasar.Quill.Util;
using Seasar.Quill.Database.Tx.Impl;
using Seasar.Extension.Tx;
//using Oracle.DataAccess.Client;
using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.DBReader;
using System.Text;
using System.Data;

namespace Macromill.QCWeb.Tabulation
{
    #region ReadDBInfoクラス
    /// <summary>
    /// データベースアクセスクラス
    /// </summary>
    [Implementation, ComVisible(false), Guid("1FBB978B-6006-401e-B3F6-792F853B9C39")]
    public class ReadDBInfo
    {
        /// <summary>QCWeb調査管理Bhv</summary>
        protected TQcwebSurveyInfoBhv tQcwebSurveyInfoBhv = null;
        /// <summary>アイテム情報Bhv</summary>
        protected TItemInfoBhv tItemInfoBhv = null;
        /// <summary>ローデータ情報Bhv</summary>
        protected TSurveyDataBhv tSurveyDataBhv = null;
        /// <summary>ローデータ管理クラス</summary>
        protected IPhysicalTableController physicalTableController = null;
        /// <summary>アプリケーション環境設定クラス</summary>
        protected ApplicationConfig appConfig = null;
        /// <summary>ウエイトバックカテゴリ設定値Bhv</summary>
        protected TWeightbackValueBhv tWeightbackValueBhv = null;
        /// <summary>シナリオBhv</summary>
        protected TScenarioTotalizationBhv tScenarioTotalizationBhv = null;

        /// <summary>
        /// アイテム情報の検索
        /// </summary>
        /// <param name="itemInfoId"></param>
        /// <returns></returns>
        public TItemInfo GetItemInfo(decimal itemInfoId)
        {
            TItemInfoCB tItemInfoCB = new TItemInfoCB();
            tItemInfoCB.SetupSelect_TQcwebSurveyInfo().WithTTableControl();
            tItemInfoCB.Query().SetItemInfoId_Equal(itemInfoId);

            return tItemInfoBhv.SelectEntityWithDeletedCheck(tItemInfoCB);
        }

        /// <summary>
        /// ローデータ情報の検索
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public List<string> GetRawdata(string tableName, string columnName)
        {
            TCategoryInfoRawDataPmb pmb = new TCategoryInfoRawDataPmb();
            pmb.FromTable = tableName;
            pmb.FromField = columnName;
            ListResultBean<CategoryInfoRawDataEntity> resultList =
                tSurveyDataBhv.OutsideSql().SelectList<CategoryInfoRawDataEntity>(TSurveyDataBhv.PATH_SelectRawData, pmb);

            List<string> retList = new List<string>();
            foreach (CategoryInfoRawDataEntity entity in resultList)
            {
                retList.Add(entity.Rawdata);
            }
            return retList;
        }

        /// <summary>
        /// ローデータに登録
        /// </summary>
        /// <param name="sampleId"></param>
        /// <param name="wb"></param>
        /// <param name="tableName"></param>
        [Transaction]
        public virtual void UpdateOrInsertWeightBack(string sampleId, string wb, string tableName)
        {
            TSurveyDataUpdateRawdataPmb pmb = new TSurveyDataUpdateRawdataPmb();
            pmb.TableName = tableName;
            pmb.ColumnName = "WeightBack";
            pmb.SampleId = sampleId;
            pmb.UpdValue = wb;
            tSurveyDataBhv.OutsideSql().Execute(TSurveyDataBhv.PATH_UpdateRawdata, pmb);
        }

        /// <summary>
        /// ローデータTXTパスを取得する
        /// </summary>
        /// <returns></returns>
        public string GetRawdataPath()
        {
            return appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_RAWDATA_PATH_AP);
        }

        /// <summary>
        /// アプリ環境設定クラスの取得
        /// </summary>
        public ApplicationConfig AppConfig
        {
            get { return appConfig; }
        }

        /// <summary>
        /// ウエイトバック情報の取得
        /// </summary>
        /// <param name="qcwebid"></param>
        /// <returns></returns>
        public ListResultBean<TWeightbackValue> GetWeightBackValues(decimal qcwebid)
        {
            TWeightbackValueCB cb = new TWeightbackValueCB();
            cb.SetupSelect_TWeightback();
            cb.Query().QueryTWeightback().InnerJoin();
            cb.Query().QueryTWeightback().SetQcwebid_Equal(qcwebid);
            cb.Query().AddOrderBy_WeightbackValueId_Asc();

            return tWeightbackValueBhv.SelectList(cb);
        }

        /// <summary>
        /// ウエイトバックコードの取得
        /// </summary>
        /// <param name="scenarioid"></param>
        /// <returns></returns>
        public decimal GetWeightBackItemid(decimal scenarioid)
        {
            TScenarioTotalization entity = tScenarioTotalizationBhv.SelectByPKValueWithDeletedCheck(scenarioid);
            return (decimal)entity.WeightbackCode;
        }

        /// <summary>
        /// 通常アイテムのリストを取得する
        /// ※マトリクスの親とデータ加工は除く
        /// </summary>
        /// <param name="qcwebid"></param>
        /// <returns></returns>
        public ListResultBean<TItemInfo> GetNormalItemList(decimal qcwebid)
        {
            TItemInfoCB cb = new TItemInfoCB();
            cb.Query().SetQcwebid_Equal(qcwebid);
            cb.Query().SetSourceDiv_Equal_Original();
            List<int?> matrixDivList = new List<int?>();
            matrixDivList.Add(int.Parse(CDef.MatrixType.NormalItem.Code));
            matrixDivList.Add(int.Parse(CDef.MatrixType.FirstChild.Code));
            matrixDivList.Add(int.Parse(CDef.MatrixType.MatrixChild.Code));
            matrixDivList.Add(int.Parse(CDef.MatrixType.SubFA.Code));
            cb.Query().SetMatrixDiv_InScope(matrixDivList);
            cb.Query().AddOrderBy_SortNumber_Asc();

            return tItemInfoBhv.SelectList(cb);
        }

    }
    #endregion

    /// <summary>
    /// IO関係のConst値
    /// </summary>
    public static class DataIoConstant
    {
        /// <summary>
        /// 削除フラグのファイル名（データ加工のサンプル削除で作成するファイル）
        /// </summary>
        public const string DELETE_FLAG_TEXT_FILE_NAME = "DELFLG.dp";
        /// <summary>
        /// 削除フラグのファイル名（デフォルト）
        /// </summary>
        public const string DELETE_FLAG_TEXT_FILE_NAME_DEFAULT = "DELFLG.txt";

        /// <summary>
        /// ウエイトバック値のファイル名
        /// </summary>
        public const string WEIGHTBACK_TEXT_FILE_NAME = "WEIGHTBACK.dp";

        /// <summary>
        /// ウエイトバック値のファイル名
        /// </summary>
        public const string WEIGHTBACK_TEXT_FILE_NAME2 = "WEIGHTBACK{0}.dp";

        /// <summary>
        /// サンプルIDのファイル名
        /// </summary>
        public const string SAMPLE_ID_TEXT_FILE_NAME = "SAMPLE_ID.txt";
    }

    #region ReadTextFileクラス
    /// <summary>
    /// データのテキストファイルからの読み込みを行うメソッドを定義する静的クラス
    /// </summary>
    [ComVisible(false), Guid("EE1DC5A6-2CA6-4bff-8169-E776E3F14BB4")]
    public static class ReadTextFile
    {
        #region メンバ変数
        private static bool[] deleteFlag = null;
        public static bool[] DeleteFlag { get => deleteFlag; set => deleteFlag = value; }
        private static bool IsTreatasZero = false;
        #endregion

        #region 削除フラグの読み込み関連
        /// <summary>
        /// 削除フラグのテキストファイルの内容を取得して、メンバ変数deleteFlagの設定を行う
        /// </summary>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <param name="reRead">既に削除フラグ情報を保持しているときに、再度削除ファイルの読み込みを行うかどうかを示すフラグ (省略可、既定値false)</param>
        internal static void ReadDeleteFlag(string deleteFlagFilePath, out QCWebException exception, bool reRead = false)
        {
            exception = null;
            if (deleteFlagFilePath == null) return; // null指定は意図的に読まないことを示すものとする
            if (string.IsNullOrWhiteSpace(deleteFlagFilePath))
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceParameterMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , "deleteFlagFilePath");
                return;
            }
            if (!System.IO.File.Exists(deleteFlagFilePath))
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NotExistFileMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , deleteFlagFilePath);
                return;
            };
            if (deleteFlag != null && !reRead) return;
            System.IO.StreamReader reader = null;
            try
            {
                reader = new System.IO.StreamReader(deleteFlagFilePath, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                deleteFlag = null;
                exception = new QCWebException(new Message(e.Message), e);
                return;
            }
            using (reader)
            {
                try
                {
                    deleteFlag = null;
                    for (int i = 1; !reader.EndOfStream; ++i)
                    {
                        Array.Resize(ref deleteFlag, i);
                        deleteFlag[i - 1] = reader.ReadLine().Equals("1");
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    exception = new QCWebException(new Message(e.Message), e);
                    deleteFlag = null;
                }
            }
        }

        /// <summary>
        /// 削除フラグのテキストファイルの内容を取得して、削除情報を取得する
        /// </summary>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>削除フラグ</returns>
        internal static bool[] ReadDeleteFlag(string deleteFlagFilePath, out QCWebException exception)
        {
            ReadDeleteFlag(deleteFlagFilePath, out exception, true);
            return deleteFlag;
        }
        #endregion

        #region データの読み込み関連
        /// <summary>
        /// SA質問データのテキストファイルの内容を取得して、
        /// SADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を返す
        /// </summary>
        /// <param name="path">SAデータのテキストファイルのパス</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>各データ情報を格納したSADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</returns>
        private static List<Data> ReadSAData(string path, out QCWebException exception)
        {
            exception = null;
            List<Data> readData = new List<Data>();
            System.IO.StreamReader reader = null;
            try
            {
                reader = new System.IO.StreamReader(path);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                exception = new QCWebException(new Message(e.Message), e);
                return null;
            }
            using (reader)
            {
                try
                {
                    // 1行ずつ読み込み(分割のオーバーヘッドをカット、大量データの一括読み込みでのメモリ不足エラー、またはハングアップを回避)
                    for (int i = 0; !reader.EndOfStream; ++i)
                    {
                        string buf = reader.ReadLine();
                        SAData data = null;
                        bool isDeleted = false;
                        try
                        {
                            if (deleteFlag != null) isDeleted = deleteFlag[i];
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                            Debug.Indent();
                            Debug.WriteLine("Type:{0}", e.GetType().ToString());
                            Debug.WriteLine("Description:{0}", e.Message);
                            Debug.Unindent();
                        }
                        if (string.IsNullOrWhiteSpace(buf))    // 無回答
                        {
                            data = new SAData(DataType.NAData, isDeleted);
                        }
                        else if (buf.Equals("*") || buf.Equals("**"))   // 非該当
                        {
                            data = new SAData(DataType.IVData, isDeleted);
                        }
                        else    // 標準データ
                        {
                            int n = 0;
                            if (int.TryParse(buf, out n))
                            {
                                data = new SAData(n, isDeleted);
                            }
                            else
                            {
                                data = new SAData(DataType.NAData, isDeleted);  // 無回答扱い
                            }
                        }
                        readData.Add(data);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    exception = new QCWebException(new Message(e.Message), e);
                    readData = null;
                }
                reader.Close();
            }
            return readData;
        }

        private static List<Data> ReadSAData(DataTable dTable, out QCWebException exception)
        {
            exception = null;
            List<Data> readData = new List<Data>();

            //System.IO.StreamReader reader = null;
            //try
            //{
            //    reader = new System.IO.StreamReader(path);
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
            //    Debug.Indent();
            //    Debug.WriteLine("Type:{0}", e.GetType().ToString());
            //    Debug.WriteLine("Description:{0}", e.Message);
            //    Debug.Unindent();
            //    exception = new QCWebException(new Message(e.Message), e);
            //    return null;
            //}
            //using (reader)
            //{
            try
            {
                int i = 0;
                // 1行ずつ読み込み(分割のオーバーヘッドをカット、大量データの一括読み込みでのメモリ不足エラー、またはハングアップを回避)
                foreach (DataRow row in dTable.Rows)
                {
                    string buf = row[0].ToString();
                    SAData data = null;
                    bool isDeleted = false;
                    try
                    {
                        if (deleteFlag != null) isDeleted = deleteFlag[i++];
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();
                    }
                    if (string.IsNullOrWhiteSpace(buf))    // 無回答
                    {
                        data = new SAData(DataType.NAData, isDeleted);
                    }
                    else if (buf.Equals("*") || buf.Equals("**"))   // 非該当
                    {
                        data = new SAData(DataType.IVData, isDeleted);
                    }
                    else    // 標準データ
                    {
                        int n = 0;
                        if (int.TryParse(buf, out n))
                        {
                            data = new SAData(n, isDeleted);
                        }
                        else
                        {
                            data = new SAData(DataType.NAData, isDeleted);  // 無回答扱い
                        }
                    }
                    readData.Add(data);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                exception = new QCWebException(new Message(e.Message), e);
                readData = null;
            }
            // reader.Close();
            // }
            return readData;

        }

        /// <summary>
        /// MA質問データのテキストファイルの内容を取得して、
        /// MADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を返す
        /// </summary>
        /// <param name="path">MAデータのテキストファイルのパス</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>各データ情報を格納したMADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</returns>
        private static List<Data> ReadMAData(string path, out QCWebException exception)
        {
            exception = null;
            List<Data> readData = new List<Data>();
            System.IO.StreamReader reader = null;
            try
            {
                reader = new System.IO.StreamReader(path);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                exception = new QCWebException(new Message(e.Message), e);
                return null;
            }
            using (reader)
            {
                try
                {
                    int sectorsCount = 0;
                    // 1行ずつ読み込み(分割のオーバーヘッドをカット、大量データの一括読み込みでのメモリ不足エラー、またはハングアップを回避)
                    for (int i = 0; !reader.EndOfStream; ++i)
                    {
                        string buf = reader.ReadLine();
                        MAData data = null;
                        bool isDeleted = false;
                        try
                        {
                            if (deleteFlag != null) isDeleted = deleteFlag[i];
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                            Debug.Indent();
                            Debug.WriteLine("Type:{0}", e.GetType().ToString());
                            Debug.WriteLine("Description:{0}", e.Message);
                            Debug.Unindent();
                        }
                        if (string.IsNullOrWhiteSpace(buf))    // 無回答
                        {
                            data = new MAData(DataType.NAData, isDeleted);
                        }
                        else if (buf.Equals("*") || buf.Equals("**"))   // 非該当
                        {
                            data = new MAData(DataType.IVData, isDeleted);
                        }
                        else    // 通常データ
                        {
                            if (sectorsCount == 0) sectorsCount = buf.Length;
                            int n = (sectorsCount - 1) / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                            data = new MAData(++n, isDeleted);
                            int x = sectorsCount;
                            int y = GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                            for (int j = 0; j < n; ++j)
                            {
                                x -= GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                if (x < 0)
                                {
                                    y = GlobalTabulation.SECTORS_COUNT_PER_4BITE + x;
                                    x = 0;
                                }
                                data.setValue(j, Convert.ToInt32(buf.Substring(x, y), 2));
                            }
                        }
                        readData.Add(data);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    exception = new QCWebException(new Message(e.Message), e);
                    readData = null;
                }
                reader.Close();
            }
            return readData;
        }

        private static List<Data> ReadMAData(DataTable dTable, out QCWebException exception)
        {
            exception = null;
            List<Data> readData = new List<Data>();

            //System.IO.StreamReader reader = null;
            //try
            //{
            //    reader = new System.IO.StreamReader(path);
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
            //    Debug.Indent();
            //    Debug.WriteLine("Type:{0}", e.GetType().ToString());
            //    Debug.WriteLine("Description:{0}", e.Message);
            //    Debug.Unindent();
            //    exception = new QCWebException(new Message(e.Message), e);
            //    return null;
            //}
            //using (reader)
            //{
            try
            {
                int sectorsCount = 0;
                int i = 0;
                // 1行ずつ読み込み(分割のオーバーヘッドをカット、大量データの一括読み込みでのメモリ不足エラー、またはハングアップを回避)
                foreach (DataRow row in dTable.Rows)
                {
                    string buf = row[0].ToString();//if all values of buf comes zero ; it is treating as Normal data and value comes as empty;causing error in Ma conversion;Need to solve in addcolum;needded here also
                    MAData data = null;
                    bool isDeleted = false;
                    try
                    {
                        if (deleteFlag != null) isDeleted = deleteFlag[i++];
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();
                    }
                    if (string.IsNullOrWhiteSpace(buf))    // 無回答
                    {
                        data = new MAData(DataType.NAData, isDeleted);
                    }
                    else if (buf.Equals("*") || buf.Equals("**"))   // 非該当
                    {
                        data = new MAData(DataType.IVData, isDeleted);
                    }
                    else    // 通常データ
                    {
                        if (sectorsCount == 0) sectorsCount = buf.Length;
                        int n = (sectorsCount - 1) / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                        data = new MAData(++n, isDeleted);
                        int x = sectorsCount;
                        int y = GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                        for (int j = 0; j < n; ++j)
                        {
                            x -= GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                            if (x < 0)
                            {
                                y = GlobalTabulation.SECTORS_COUNT_PER_4BITE + x;
                                x = 0;
                            }
                            data.setValue(j, Convert.ToInt32(buf.Substring(x, y), 2));
                        }
                    }
                    readData.Add(data);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                exception = new QCWebException(new Message(e.Message), e);
                readData = null;
                //}
                //reader.Close();
            }
            return readData;

        }


        /// <summary>
        /// N質問データまたはWB値データのテキストファイルの内容を取得して、
        /// NDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を返す
        /// </summary>
        /// <param name="path">NデータまたはWB値のテキストファイルのパス</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>各データ情報を格納したNDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</returns>
        private static List<Data> ReadNData(string path, out QCWebException exception)
        {
            exception = null;
            List<Data> readData = new List<Data>();
            System.IO.StreamReader reader = null;
            try
            {
                reader = new System.IO.StreamReader(path);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                exception = new QCWebException(new Message(e.Message), e);
                return null;
            }
            using (reader)
            {
                try
                {
                    // 1行ずつ読み込み(分割のオーバーヘッドをカット、大量データの一括読み込みでのメモリ不足エラー、またはハングアップを回避)
                    for (int i = 0; !reader.EndOfStream; ++i)
                    {
                        string buf = reader.ReadLine();
                        NData data = null;
                        bool isDeleted = false;
                        try
                        {
                            if (deleteFlag != null) isDeleted = deleteFlag[i];
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                            Debug.Indent();
                            Debug.WriteLine("Type:{0}", e.GetType().ToString());
                            Debug.WriteLine("Description:{0}", e.Message);
                            Debug.Unindent();
                        }
                        if (string.IsNullOrWhiteSpace(buf))    // 無回答
                        {
                            if (IsTreatasZero)
                                data = new NData(0.0, isDeleted);//#247847-Replaced DK with 0 for DK when Treataszero is on
                            else
                                data = new NData(DataType.NAData, isDeleted);
                        }
                        else if (buf.Equals("*") || buf.Equals("**"))   // 非該当
                        {
                            data = new NData(DataType.IVData, isDeleted);
                        }
                        else    // 通常データ
                        {
                            double n = 0.0;
                            if (double.TryParse(buf, out n))
                            {
                                data = new NData(n, isDeleted);
                            }
                            else
                            {
                                data = new NData(DataType.NAData, isDeleted);   // 無回答扱い
                            }
                        }
                        readData.Add(data);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    exception = new QCWebException(new Message(e.Message), e);
                    readData = null;
                }
                reader.Close();
            }
            return readData;
        }

        private static List<Data> ReadNData(DataTable dTable, out QCWebException exception)
        {

            exception = null;
            List<Data> readData = new List<Data>();
            System.IO.StreamReader reader = null;
            //try
            //{
            //    reader = new System.IO.StreamReader(path);
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
            //    Debug.Indent();
            //    Debug.WriteLine("Type:{0}", e.GetType().ToString());
            //    Debug.WriteLine("Description:{0}", e.Message);
            //    Debug.Unindent();
            //    exception = new QCWebException(new Message(e.Message), e);
            //    return null;
            //}
            //using (reader)
            //{
            try
            {
                // 1行ずつ読み込み(分割のオーバーヘッドをカット、大量データの一括読み込みでのメモリ不足エラー、またはハングアップを回避)
                int i = 0;
                foreach (DataRow row in dTable.Rows)
                {
                    string buf = row[0].ToString();
                    NData data = null;
                    bool isDeleted = false;
                    try
                    {
                        if (deleteFlag != null) isDeleted = deleteFlag[i++];
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();
                    }
                    if (string.IsNullOrWhiteSpace(buf))    // 無回答
                    {
                        data = new NData(DataType.NAData, isDeleted);
                    }
                    else if (buf.Equals("*") || buf.Equals("**"))   // 非該当
                    {
                        data = new NData(DataType.IVData, isDeleted);
                    }
                    else    // 通常データ
                    {
                        double n = 0.0;
                        if (double.TryParse(buf, out n))
                        {
                            data = new NData(n, isDeleted);
                        }
                        else
                        {
                            data = new NData(DataType.NAData, isDeleted);   // 無回答扱い
                        }
                    }
                    readData.Add(data);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                exception = new QCWebException(new Message(e.Message), e);
                readData = null;
                //}
                //reader.Close();
            }
            return readData;

        }

        /// <summary>
        /// FA質問データのテキストファイルの内容を取得して、
        /// FADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を返す
        /// </summary>
        /// <param name="path">FAデータのテキストファイルのパス</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>各データ情報を格納したFADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</returns>
        private static List<Data> ReadFAData(string path, out QCWebException exception)
        {
            exception = null;
            List<Data> readData = new List<Data>();
            System.IO.StreamReader reader = null;
            try
            {
                reader = new System.IO.StreamReader(path);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                exception = new QCWebException(new Message(e.Message), e);
                return null;
            }
            using (reader)
            {
                try
                {
                    // 1行ずつ読み込み(分割のオーバーヘッドをカット、大量データの一括読み込みでのメモリ不足エラー、またはハングアップを回避)
                    for (int i = 0; !reader.EndOfStream; ++i)
                    {
                        string buf = reader.ReadLine();
                        bool isDeleted = false;
                        try
                        {
                            if (deleteFlag != null) isDeleted = deleteFlag[i];
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                            Debug.Indent();
                            Debug.WriteLine("Type:{0}", e.GetType().ToString());
                            Debug.WriteLine("Description:{0}", e.Message);
                            Debug.Unindent();
                        }
                        // 現状FAでは、非該当はない
                        FAData data = null;
                        if (string.IsNullOrEmpty(System.Text.RegularExpressions.Regex.Unescape(buf)))
                        {
                            data = new FAData(DataType.NAData, isDeleted);
                        }
                        else
                        {
                            //buf = System.Text.RegularExpressions.Regex.Unescape(buf);
                            data = new FAData(System.Text.RegularExpressions.Regex.Unescape(buf), isDeleted);
                        }
                        readData.Add(data);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    exception = new QCWebException(new Message(e.Message), e);
                    readData = null;
                }
                reader.Close();
            }
            return readData;
        }

        /// <summary>
        /// FA質問データのテキストファイルの内容を取得して、
        /// FADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を返す
        /// </summary>
        /// <param name="dTable">FAデータのテキストファイルのパス</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>各データ情報を格納したFADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</returns>
        private static List<Data> ReadFAData(DataTable dTable, out QCWebException exception)
        {
            exception = null;
            List<Data> readData = new List<Data>();
            //System.IO.StreamReader reader = null;
            //try
            //{
            //    reader = new System.IO.StreamReader(dTable);
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
            //    Debug.Indent();
            //    Debug.WriteLine("Type:{0}", e.GetType().ToString());
            //    Debug.WriteLine("Description:{0}", e.Message);
            //    Debug.Unindent();
            //    exception = new QCWebException(new Message(e.Message), e);
            //    return null;
            //}

            try
            {
                // 1行ずつ読み込み(分割のオーバーヘッドをカット、大量データの一括読み込みでのメモリ不足エラー、またはハングアップを回避)
                int i = 0;
                foreach (DataRow row in dTable.Rows)
                {
                    string buf = row[0].ToString();
                    bool isDeleted = false;
                    try
                    {
                        if (deleteFlag != null) isDeleted = deleteFlag[i++];
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();
                    }
                    // 現状FAでは、非該当はない
                    FAData data = null;
                    if (string.IsNullOrEmpty(buf))
                    {
                        data = new FAData(DataType.NAData, isDeleted);
                    }
                    else
                    {
                        //buf = System.Text.RegularExpressions.Regex.Unescape(buf);
                        data = new FAData(buf, isDeleted);
                    }
                    readData.Add(data);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                exception = new QCWebException(new Message(e.Message), e);
                readData = null;
            }
            return readData;
        }


        private delegate List<Data> ReadDataDelegate(string path, out QCWebException exception);
        private delegate List<Data> ReadDataTableDelegate(DataTable path, out QCWebException exception);


        /// <alias>ReadData00</alias>
        /// <summary>
        /// <para>エイリアス:ReadData00</para>
        /// 削除フラグ情報を更新してから、
        /// 質問タイプに応じて適切な内部ルーチンをコールして、質問データのテキストファイルの内容を取得する
        /// ReadDataオーバーロードメソッド群の根幹ロジック
        /// </summary>
        /// <param name="dTable">データのテキストファイルのパス</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス<br />nullのときには削除フラグ情報を更新しない</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <param name="reRead">既に削除フラグ情報を保持しているときに、再度削除ファイルの読み込みを行うかどうかを示すフラグ (省略可、既定値false)</param>
        /// <returns>各データ情報を格納したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// 
        public static List<Data> ReadDataTable(DataTable dTable, QuestionType questionType
                    , string deleteFlagFilePath, out QCWebException exception, bool reRead = false)
        {

            if (null == dTable)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceParameterMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , "path");
                return null;
            }
            //if (!System.IO.File.Exists(dTable))
            //{
            //    exception = new QCWebException(new Message(Constants.CommonMessageIndex.NotExistFileMessageIndex)
            //                                 , GlobalsCommonConstant.LogLevel.FATAL
            //                                 , dTable);
            //    return null;
            //}
            ReadDeleteFlag(deleteFlagFilePath, out exception, reRead);
            ReadDataTableDelegate readMethod = null;
            switch (questionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA))
            {
                case QuestionType.SA:
                    readMethod = ReadSAData;
                    break;
                case QuestionType.MA:
                    readMethod = ReadMAData;
                    break;
                case QuestionType.N:
                    readMethod = ReadNData;
                    break;
                case QuestionType.FA:
                    readMethod = ReadFAData;
                    break;
                default:
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustQuestionTypeMessageIndex));
                    return null;
            }
            QCWebException ex = null;
            List<Data> res = readMethod(dTable, out ex);
            if (ex != null) exception = ex;
            return res;
        }

        /// <alias>ReadData00</alias>
        /// <summary>
        /// <para>エイリアス:ReadData00</para>
        /// 削除フラグ情報を更新してから、
        /// 質問タイプに応じて適切な内部ルーチンをコールして、質問データのテキストファイルの内容を取得する
        /// ReadDataオーバーロードメソッド群の根幹ロジック
        /// </summary>
        /// <param name="path">データのテキストファイルのパス</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス<br />nullのときには削除フラグ情報を更新しない</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <param name="reRead">既に削除フラグ情報を保持しているときに、再度削除ファイルの読み込みを行うかどうかを示すフラグ (省略可、既定値false)</param>
        /// <returns>各データ情報を格納したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// 
        public static List<Data> ReadData(string path, QuestionType questionType
                    , string deleteFlagFilePath, out QCWebException exception, bool reRead = false, bool isTreatasZero = false)
        {
            IsTreatasZero = isTreatasZero;
            if (string.IsNullOrWhiteSpace(path))
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceParameterMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , "path");
                return null;
            }
            if (!System.IO.File.Exists(path))
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NotExistFileMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , path);
                return null;
            }
            ReadDeleteFlag(deleteFlagFilePath, out exception, reRead);
            ReadDataDelegate readMethod = null;
            switch (questionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA))
            {
                case QuestionType.SA:
                    readMethod = ReadSAData;
                    break;
                case QuestionType.MA:
                    readMethod = ReadMAData;
                    break;
                case QuestionType.N:
                    readMethod = ReadNData;
                    break;
                case QuestionType.FA:
                    readMethod = ReadFAData;
                    break;
                default:
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustQuestionTypeMessageIndex));
                    return null;
            }
            QCWebException ex = null;
            List<Data> res = readMethod(path, out ex);
            if (ex != null) exception = ex;
            return res;
        }

        /// <alias>ReadData01</alias>
        /// <summary>
        /// <para>エイリアス:ReadData01</para>
        /// 質問タイプに応じて適切な内部ルーチンをコールして、質問データのテキストファイルの内容を取得する<br />
        /// 引数deleteFlagFilePathにnullを指定してReadData00に仲介
        /// </summary>
        /// <param name="path">データのテキストファイルのパス</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>各データ情報を格納したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</returns>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.ReadTextFile.ReadData(System.String,Macromill.QCWeb.Tabulation.QuestionType,System.String)">ReadData00</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        public static List<Data> ReadData(string path, QuestionType questionType, out QCWebException exception)
        {
            return ReadData(path, questionType, null, out exception);
        }

        /// <summary>
        /// ReadDataメソッドの拡張版<br />
        /// ファイルがない場合や更新時には、DBからの読み込みおよびテキストファイルの作成も行う
        /// </summary>
        /// <param name="question">Questionクラスのインスタンスへの参照</param>
        /// <param name="dirpath">データファイルのディレクトリパス</param>
        /// <param name="path">データテキストファイルのパス (戻り値)</param>
        /// <param name="questiontype">質問タイプ (戻り値)</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <param name="reReadDeleteFlag">既に削除フラグ情報を保持しているときに、再度削除ファイルの読み込みを行うかどうかを示すフラグ (省略可、既定値false)</param>
        /// <param name="isUseDataProcess">データ加工で利用する場合はtrue、集計で利用する場合はfalse（省略可、既定値false）</param>
        /// <returns>各データ情報を格納したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</returns>
        public static List<Data> ReadData2(Questions.Question question, string dirpath
                                         , out string path, out QuestionType questiontype, out QCWebException exception
                                         , bool reReadDeleteFlag = false, bool isUseDataProcess = false)
        {
            exception = null;
            // 命名規則にしたがってパス生成
            if (string.IsNullOrWhiteSpace(dirpath))
            {
                path = null;
                questiontype = QuestionType.SA;
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceParameterMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , "exception");
                return null;
            }

            path = System.IO.Path.Combine(dirpath, question.RawdataTextFileName);

            //UI側で再実行判定を行うのでコメントアウト
            //// データ加工再実行判定
            //DataProcessCommon dpCommon = new DataProcessCommon();
            //QuillInjector.GetInstance().Inject(dpCommon);
            //if (dpCommon.IsDataProcessReExecute(question.QCWebID, dirpath)) {
            //    //データ加工を再実行する
            //}

            questiontype = question.QuestionType;  //QuestionType.SA;
            string tablename = question.TableName;  //null;
            string columnname = question.ColumnName;  //questionid.ToString();
            string topTableName = question.TopTableName;

            if ((questiontype & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustQuestionTypeMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , questiontype.ToString());
                return null;
            }
            QuestionType qType = questiontype & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA);
            bool isFA = qType == QuestionType.FA;
            if (!Enum.IsDefined(typeof(QuestionType), qType))
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustQuestionTypeMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , questiontype.ToString());
                return null;
            }

            string deleteFlagFilePath = System.IO.Path.Combine(dirpath, DataIoConstant.DELETE_FLAG_TEXT_FILE_NAME);
            if (!System.IO.File.Exists(deleteFlagFilePath))
            {
                deleteFlagFilePath = System.IO.Path.Combine(dirpath, DataIoConstant.DELETE_FLAG_TEXT_FILE_NAME_DEFAULT);
            }

            bool createNewFile = !System.IO.File.Exists(path);
            if(createNewFile)
            {
                path = System.IO.Path.Combine(dirpath, question.RawdataTextFileName);
                path = path.Replace("txt", "dp");
                createNewFile = !System.IO.File.Exists(path);
            }
            //ローデータの持ち方の変更により以下コメントアウト
            //if (!createNewFile) {
            //    // ローデータテーブルの更新日時を取得
            //    //DateTime dbModifiedTime = (DateTime)entity.LastUpdateDatetime;  //new DateTime(2012, 3, 27);
            //    DateTime dbModifiedTime = question.LastUpdateDateTime;  //new DateTime(2012, 3, 27);
            //    // ファイルの作成日時を取得
            //    //DateTime fileCreated = System.IO.File.GetCreationTime(path);
            //    //ファイルシステムのトンネリング機能によって、作成日時が引き継がれてしまうので更新日時を使い比較する
            //    DateTime fileCreated = System.IO.File.GetLastWriteTime(path);
            //    if (createNewFile = fileCreated < dbModifiedTime)
            //    {
            //        try {
            //            System.IO.File.Delete(path);
            //        } catch (Exception e) {
            //            Debug.WriteLine("StackTrace:{0}", e.StackTrace);
            //            Debug.Indent();
            //            Debug.WriteLine("Type:{0}", e.GetType().ToString());
            //            Debug.WriteLine("Description:{0}", e.Message);
            //            Debug.Unindent();
            //            exception = new QCWebException(new Message(e.Message), e);
            //            return null;
            //        }
            //    }
            //}
            // 新規にデータテキストファイルを作成する必要がない場合
            if (!createNewFile)
            {
                if (isUseDataProcess)
                {
                    return ReadData(path, questiontype, null, out exception, reReadDeleteFlag);
                }
                else
                {
                    return ReadData(path, questiontype, deleteFlagFilePath, out exception, reReadDeleteFlag);
                }
            }

            // カテゴリ出力設定の再実行判定
            if (question.IsTemporatyItem &&
            question.TemporaryDataProcess == GlobalsCommonConstant.TemporaryDataProcess.CategoryEdit)
            {
                // カテゴリ出力設定を再実行する処理はUI側で既に実施されている前提。
                // よって、この分岐に来た場合は、前提が崩れたのでエラーとする。
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NotExistFileMessageIndex)
                                        , GlobalsCommonConstant.LogLevel.ERROR
                                        , path);
            }

            // 削除フラグは無視してすべてのデータをDBから取得
            List<string> datavalue = null;
            ReadDBInfo readDBInfo = new ReadDBInfo();
            QuillInjector.GetInstance().Inject(readDBInfo);
            // Mantis#2242調査用例外
            if (string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(columnname))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("ローデータテーブル名またはカラム名の取得に失敗しました。");
                sb.Append(" [QCWebID]" + question.QCWebID);
                sb.Append(" [アイテムID]" + question.ID);
                sb.Append(" [アイテム名]" + question.Name);
                sb.Append(" [回答タイプ]" + question.QCAnswerType.ToString());
                sb.Append(" [属性質問]" + question.IsPropertyItem.ToString());
                sb.Append(" [通常アイテム]" + question.IsNormalItem.ToString());
                sb.Append(" [一時アイテム]" + question.IsTemporatyItem.ToString());
                sb.Append(" [データ加工アイテム]" + question.IsNewItem.ToString());
                sb.Append(" [データ修正実施有無]" + question.IsDataEdit.ToString());
                sb.Append(" [ファイル拡張子]" + question.RawdataTextFileExtension);
                sb.Append(" [ファイル名]" + question.RawdataTextFileName);
                sb.Append(" [ファイルパス]" + path);
                sb.Append(" [createNewFile]" + createNewFile.ToString());
                exception = new QCWebException(sb.ToString(), GlobalsCommonConstant.LogLevel.ERROR, null);
                return null;
            }

            datavalue = readDBInfo.GetRawdata(tablename, columnname);

            if (!isUseDataProcess)
            {
                //if (!CreateTextFile.CreateDeleteFlag(tablename, dirpath, out deleteFlagFilePath)) return null;
                if (!CreateTextFile.CreateDeleteFlag(topTableName, dirpath, out deleteFlagFilePath, out exception)) return null;
                ReadTextFile.ReadDeleteFlag(deleteFlagFilePath, out exception, reReadDeleteFlag);
                if (exception != null) return null;
            }
            List<Data> readData = new List<Data>();
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true, System.Text.Encoding.UTF8);
                using (writer)
                {
                    try
                    {
                        int sectorscount = 0;   // MA用
                        for (int i = 0; i < datavalue.Count; ++i)
                        {
                            string buf = datavalue[i];
                            if (isFA)
                            {
                                if (buf == null) buf = string.Empty;
                                // writer.WriteLine(System.Text.RegularExpressions.Regex.Escape(buf));
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(buf)) buf = string.Empty;
                                // writer.WriteLine(buf);
                            }
                            bool isDeleted = false;
                            Data data = null;
                            try
                            {
                                if (deleteFlag != null) isDeleted = deleteFlag[i];
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                                Debug.Indent();
                                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                                Debug.WriteLine("Description:{0}", e.Message);
                                Debug.Unindent();
                            }
                            // if (isFA && string.IsNullOrEmpty(buf) || !isFA && string.IsNullOrWhiteSpace(buf))   // 無回答
                            /*
                            if (buf.Length == 0)
                            {
                                data = new Data(DataType.NAData, isDeleted);
                            }
                            else if (!isFA && buf.Equals("*"))  // 非該当
                            {
                                data = new Data(DataType.IVData, isDeleted);
                            }
                            else    // 標準データ
                            {
                            */
                            switch (qType)
                            {
                                case QuestionType.SA:
                                    {
                                        int n = 0;
                                        if (int.TryParse(buf, out n))
                                        {
                                            data = new SAData(n, isDeleted);
                                        }
                                        /*
                                        else
                                        {
                                            data = new SAData(DataType.NAData, isDeleted);  // 無回答扱い
                                        }
                                        */
                                        else if (buf.Equals("*")|| buf.Equals("**"))
                                        {
                                            data = new SAData(DataType.IVData, isDeleted);
                                        }
                                        else
                                        {
                                            buf = string.Empty;
                                            data = new SAData(DataType.NAData, isDeleted);
                                        }
                                    }
                                    break;
                                case QuestionType.MA:
                                    {
                                        if (buf.Length == 0)
                                        {
                                            data = new MAData(DataType.NAData, isDeleted);
                                        }
                                        else if (buf.Equals("*") || buf.Equals("**"))
                                        {
                                            data = new MAData(DataType.IVData, isDeleted);
                                        }
                                        else
                                        {
                                            if (sectorscount == 0) sectorscount = buf.Length;
                                            int n = (sectorscount - 1) / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            data = new MAData(++n, isDeleted);
                                            int x = sectorscount;
                                            int y = GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            for (int j = 0; j < n; ++j)
                                            {
                                                x -= GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                                if (x < 0)
                                                {
                                                    y = GlobalTabulation.SECTORS_COUNT_PER_4BITE + x;
                                                    x = 0;
                                                }
                                                (data as MAData).setValue(j, Convert.ToInt32(buf.Substring(x, y), 2));
                                            }
                                            if ((data as MAData).SectorsArray == null)
                                            {
                                                buf = string.Empty;
                                                data = new MAData(DataType.NAData, isDeleted);  // オールゼロ→無回答扱い
                                            }
                                        }
                                    }
                                    break;
                                case QuestionType.N:
                                    {
                                        double n = 0.0;
                                        if (double.TryParse(buf, out n))
                                        {
                                            data = new NData(n, isDeleted);
                                        }
                                        else if (buf.Equals("*") || buf.Equals("**"))
                                        {
                                            data = new NData(DataType.IVData, isDeleted);
                                        }
                                        else
                                        {
                                            buf = string.Empty;
                                            data = new NData(DataType.NAData, isDeleted);
                                        }
                                    }
                                    break;
                                case QuestionType.FA:
                                    if (buf.Length == 0)
                                    {
                                        data = new FAData(DataType.NAData, isDeleted);
                                    }
                                    else
                                    {
                                        data = new FAData(buf, isDeleted);
                                    }
                                    break;
                            }
                            /*
                            }
                            */
                            if (isFA)
                            {
                                writer.WriteLine(System.Text.RegularExpressions.Regex.Escape(buf));
                            }
                            else
                            {
                                writer.WriteLine(buf);
                            }
                            readData.Add(data);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();
                        try
                        {
                            writer.Close();
                            System.IO.File.Delete(path);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                            Debug.Indent();
                            Debug.WriteLine("Type:{0}", ex.GetType().ToString());
                            Debug.WriteLine("Description:{0}", ex.Message);
                            Debug.Unindent();
                        }
                        exception = new QCWebException(new Message(e.Message), e);
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                // エラーをスローするかどうかは検討
                exception = new QCWebException(new Message(e.Message), e);
                return null;
            }
            return readData;
        }

        /// <summary>
        /// ReadDataメソッドの拡張版<br />
        /// ファイルがない場合や更新時には、DBからの読み込みおよびテキストファイルの作成も行う
        /// </summary>
        /// <param name="questionid">アイテムID</param>
        /// <param name="dirpath">データファイルのディレクトリパス</param>
        /// <param name="path">データテキストファイルのパス (戻り値)</param>
        /// <param name="questiontype">質問タイプ (戻り値)</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <param name="reReadDeleteFlag">既に削除フラグ情報を保持しているときに、再度削除ファイルの読み込みを行うかどうかを示すフラグ (省略可、既定値false)</param>
        /// <param name="isUseDataProcess">データ加工で利用する場合はtrue、集計で利用する場合はfalse（省略可、既定値false）</param>
        /// <returns>各データ情報を格納したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</returns>
        public static List<Data> ReadData2(decimal questionid, string dirpath
                                         , out string path, out QuestionType questiontype, out QCWebException exception
                                         , bool reReadDeleteFlag = false, bool isUseDataProcess = false)
        {
            // DB接続して質問タイプ、テーブル名、カラム名を取得
            Question.Questions questions = new Question.Questions(0, questionid);
            Questions.Question question = (Questions.Question)questions[questionid];
            return ReadData2(question, dirpath, out path, out questiontype, out exception, reReadDeleteFlag, isUseDataProcess);
        }


        /// <summary>
        /// WBファイルが存在した場合はファイルを読み込む
        /// 存在しなかった場合はNullを返却する
        /// </summary>
        /// <param name="qcwebid"></param>
        /// <param name="scenarioid"></param>
        /// <returns></returns>
        public static List<Data> ReadWBDataFile(decimal qcwebid, decimal scenarioid)
        {
            ReadDBInfo readDBInfo = new ReadDBInfo();
            QuillInjector.GetInstance().Inject(readDBInfo);
            string path = System.IO.Path.Combine(readDBInfo.GetRawdataPath(), qcwebid.ToString());
            decimal itemid = readDBInfo.GetWeightBackItemid(scenarioid);

            string filepath = null;
            QCWebException exception = null;
            List<Data> dataList = null;
            QuestionType qType = QuestionType.N;
            if (itemid == -1)
            {
                dataList = CreateTextFile.CreateWBData(qcwebid, path, out filepath, out exception);
            }
            else
            {
                dataList = ReadTextFile.ReadData2(itemid, path, out filepath, out qType, out exception);
            }
            if (exception != null) throw exception;
            return dataList;
        }

		public static List<Data> ReadDataTable(DataTable dtReadTable1, QuestionType qType, object p, out object ex)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
    #endregion

    #region CreateTextFileクラス
    /// <summary>
    /// データのテキストファイルを作成するメソッドを定義する静的クラス
    /// </summary>
    [ComVisible(false), Guid("6604A7BA-2E0F-4FA4-B5C4-7AD776FFFBE4")]
    public static class CreateTextFile
    {
        private static bool CreateDataFile(string path, string dbtablename, string dbcolumnname, DateTime dbModifiedTime, out QCWebException exception, bool isFA = false)
        {
            exception = null;
            // 既に作成済みの場合のチェック (同名ディレクトリの存在時は考慮しない)
            if (System.IO.File.Exists(path))
            {
                // DBローデータテーブルの更新日時を取得
                //DateTime dbModifiedTime = new DateTime(2012, 3, 27);
                // ファイルの作成日時を取得
                //DateTime fileCreated = System.IO.File.GetCreationTime(path);
                //ファイルシステムのトンネリング機能によって、作成日時が引き継がれてしまうので更新日時を使い比較する
                DateTime fileCreated = System.IO.File.GetLastWriteTime(path);
                if (fileCreated >= dbModifiedTime)
                {
                    return true;
                }
                else
                {
                    try
                    {
                        System.IO.File.Delete(path);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();
                        exception = new QCWebException(new Message(e.Message), e);
                        return false;
                    }
                }
            }
            // 削除フラグは無視してすべてのデータをDBから取得
            // これは仮実装のためのもの、実際には一旦この変数に入れる必要はなし
            // (エラー処理的に一旦入れた方がよければ、その方向で)
            //List<string> datavalue = new List<string>();
            ReadDBInfo readDBInfo = new ReadDBInfo();
            QuillInjector.GetInstance().Inject(readDBInfo);
            List<string> datavalue = readDBInfo.GetRawdata(dbtablename, dbcolumnname);

            try
            {
                string dirpath = System.IO.Path.GetDirectoryName(path);
                Common.GlobalMethodClass.GuaranteeDirectoryExist(dirpath);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true, System.Text.Encoding.UTF8);
                using (writer)
                {
                    try
                    {
                        for (int i = 0; i < datavalue.Count; ++i)
                        {
                            string val = datavalue[i] == null ? "" : datavalue[i];
                            if (isFA)
                            {
                                //writer.WriteLine(System.Text.RegularExpressions.Regex.Escape(datavalue[i]));
                                writer.WriteLine(System.Text.RegularExpressions.Regex.Escape(val));
                            }
                            else
                            {
                                //writer.WriteLine(datavalue[i]);
                                writer.WriteLine(val);
                            }
                        }
                        writer.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();
                        try
                        {
                            writer.Close();
                            System.IO.File.Delete(path);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                            Debug.Indent();
                            Debug.WriteLine("Type:{0}", ex.GetType().ToString());
                            Debug.WriteLine("Description:{0}", ex.Message);
                            Debug.Unindent();
                        }
                        exception = new QCWebException(new Message(e.Message), e);
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                exception = new QCWebException(new Message(e.Message), e);
                return false;
            }
        }

        /// <summary>
        /// 質問データのテキストファイルを作成する静的メソッド
        /// </summary>
        /// <param name="question">集計する質問のQuestionクラスのインスタンスへの参照</param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <param name="path">生成するファイルのパス (戻り値)</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>成功した場合true、失敗した場合false</returns>
        public static bool CreateData(Questions.Question question, string dirpath, out string path, out QCWebException exception)
        {
            // 命名規則にしたがってパス生成
            //path = System.IO.Path.Combine(dirpath, question.ID.ToString() + ".txt");
            path = System.IO.Path.Combine(dirpath, question.RawdataTextFileName);
            QuestionType questionType = question.QuestionType;
            if ((questionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustQuestionTypeMessageIndex)
                                                         , GlobalsCommonConstant.LogLevel.FATAL
                                                        , questionType.ToString());
                return false;
            }
            bool isFA = (questionType & QuestionType.FA) == QuestionType.FA;
            return CreateDataFile(path, question.TableName, question.ColumnName, question.LastUpdateDateTime, out exception, isFA);
        }

        /// <summary>
        /// 質問データのテキストファイルを作成する静的メソッド
        /// </summary>
        /// <param name="questionid">アイテムID</param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <param name="path">生成するファイルのパス (戻り値)</param>
        /// <param name="questionType">質問タイプ (戻り値)</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>成功した場合true、失敗した場合false</returns>
        public static bool CreateData(decimal questionid, string dirpath
                        , out string path, out QuestionType questionType, out QCWebException exception)
        {
            Question.Questions questions = new Question.Questions(0, questionid);
            Questions.Question question = questions[questionid] as Question.Questions.Question;
            questionType = question.QuestionType;  //QuestionType.SA;
            return CreateData(question, dirpath, out path, out exception);
        }

        /// <summary>
        /// 質問データのテキストファイルを作成する静的メソッド
        /// </summary>
        /// <param name="questionid">アイテムID</param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <param name="path">生成するファイルのパス (戻り値)</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>成功した場合true、失敗した場合false</returns>
        public static bool CreateData(decimal questionid, string dirpath, out string path, out QCWebException exception)
        {
            QuestionType questionType = QuestionType.SA;
            return CreateData(questionid, dirpath, out path, out questionType, out exception);
        }

        /// <summary>
        /// 削除フラグのテキストファイルを作成する静的メソッド
        /// </summary>
        /// <param name="tablename">削除フラグデータが入っているテーブル名</param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <param name="path">生成するファイルのパス (戻り値)</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>成功した場合true、失敗した場合false</returns>
        public static bool CreateDeleteFlag(string tablename, string dirpath, out string path, out QCWebException exception)
        {
            exception = null;

            path = System.IO.Path.Combine(dirpath, DataIoConstant.DELETE_FLAG_TEXT_FILE_NAME);
            if (System.IO.File.Exists(path)) return true;

            path = System.IO.Path.Combine(dirpath, DataIoConstant.DELETE_FLAG_TEXT_FILE_NAME_DEFAULT);
            if (System.IO.File.Exists(path)) return true;

            return CreateDataFile(path, tablename, "Delete_Flag", DateTime.Now, out exception);
        }

        /// <summary>
        /// SA質問からWB仮想カラムのデータテキストファイルを作成する静的メソッド
        /// </summary>
        /// <param name="qcwebid">QCWEBID</param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <param name="path">生成するファイルのパス (戻り値)</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>成功した場合true、失敗した場合false</returns>
        public static List<Data> CreateWBData(decimal qcwebid, string dirpath, out string path, out QCWebException exception)
        {
            exception = null;
            path = System.IO.Path.Combine(dirpath, DataIoConstant.WEIGHTBACK_TEXT_FILE_NAME);

            ReadDBInfo readDBInfo = new ReadDBInfo();
            QuillInjector.GetInstance().Inject(readDBInfo);
            ListResultBean<TWeightbackValue> list = readDBInfo.GetWeightBackValues(qcwebid);
            if (list.Count == 0)
            {
                // ウエイトバック情報がありません
                exception = new QCWebException("QCCMN15001000", GlobalsCommonConstant.LogLevel.FATAL, null);
            }

            if (System.IO.File.Exists(path))
            {
                DateTime fileCreated = System.IO.File.GetLastWriteTime(path);
                DateTime dbModifiedTime = (DateTime)list[0].TWeightback.LastUpdateDatetime;
                if (fileCreated >= dbModifiedTime)
                {
                    return ReadTextFile.ReadData(path, QuestionType.N, out exception);
                }
                else
                {
                    try
                    {
                        System.IO.File.Delete(path);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();
                        exception = new QCWebException(new Message(e.Message), e);
                        return null;
                    }
                }
            }

            int ivNo = (int)GlobalsCommonConstant.WeightbackNo.Iv;
            int naNo = (int)GlobalsCommonConstant.WeightbackNo.Na;
            int ivNacount = list.Where(w => w.WeightbackItemNo == ivNo || w.WeightbackItemNo == naNo).Count();
            double[] wbs = new double[list.Count - ivNacount];
            double nawb = 0;    // 無回答
            double ivwb = 0;    // 非該当
            for (int i = 0, j = 0; i < list.Count; ++i)
            {
                TWeightbackValue valBean = list[i];
                if (valBean.WeightbackItemNo == ivNo)
                {
                    ivwb = double.Parse(valBean.WeightbackValue);
                }
                else if (valBean.WeightbackItemNo == naNo)
                {
                    nawb = double.Parse(valBean.WeightbackValue);
                }
                else
                {
                    wbs[j] = double.Parse(valBean.WeightbackValue);
                    ++j;
                }
            }
            return SetWeightBack.Execute(qcwebid, (decimal)list[0].TWeightback.WeightbackItemId
                                         , wbs, nawb, ivwb, out exception, dirpath);
        }

        /// <summary>
        /// N質問をWBとする場合を除いたWB仮想カラムのデータテキストファイルを作成する静的メソッド
        /// <note>N質問をWBとする場合はCreateDataメソッドを使う</note>
        /// </summary>
        /// <param name="qcwebid">QCWEBID</param>
        /// <param name="scenarioid">シナリオID</param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <param name="path">生成するファイルのパス (戻り値)</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>成功した場合true、失敗した場合false</returns>
        public static List<Data> CreateWBData(decimal qcwebid, decimal scenarioid, string dirpath, out string path, out QCWebException exception)
        {
            exception = null;
            ReadDBInfo readDBInfo = new ReadDBInfo();
            QuillInjector.GetInstance().Inject(readDBInfo);
            decimal itemid = readDBInfo.GetWeightBackItemid(scenarioid);
            if (itemid == -1)
            {
                return CreateWBData(qcwebid, dirpath, out path, out exception);
            }

            // Nアイテムの読み込み
            QuestionType questionType;
            return ReadTextFile.ReadData2(itemid, dirpath, out path, out questionType, out exception, true);
        }

        /// <summary>
        /// サンプルIDのテキストファイルを作成する静的メソッド
        /// </summary>
        /// <param name="tablename">サンプルIDデータが入っているテーブル名</param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <param name="path">生成するファイルのパス (戻り値)</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>成功した場合true、失敗した場合false</returns>
        [System.ObsoleteAttribute]
        public static bool CreateSampleId(string tablename, string dirpath, out string path, out QCWebException exception)
        {
            // 命名規則にしたがってパス生成
            path = System.IO.Path.Combine(dirpath, DataIoConstant.SAMPLE_ID_TEXT_FILE_NAME);

            // TODO:常に新しく作成し直す
            return CreateDataFile(path, tablename, "SAMPLE_ID", DateTime.Now, out exception);
        }

        /// <summary>
        /// サンプルIDのテキストファイルを作成する静的メソッド
        /// </summary>
        /// <param name="question">集計する質問のQuestionクラスのインスタンスへの参照</param>
        /// <param name="tablename">サンプルIDデータが入っているテーブル名</param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <param name="path">生成するファイルのパス (戻り値)</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>成功した場合true、失敗した場合false</returns>
        [System.ObsoleteAttribute]
        public static bool CreateSampleId(Questions.Question question, string tablename, string dirpath, out string path, out QCWebException exception)
        {
            // 命名規則にしたがってパス生成
            path = System.IO.Path.Combine(dirpath, DataIoConstant.SAMPLE_ID_TEXT_FILE_NAME);
            return CreateDataFile(path, tablename, "SAMPLE_ID", question.LastUpdateDateTime, out exception);
        }

        /// <summary>
        /// 通常アイテムのローデータテキストファイルを作成する静的メソッド
        /// </summary>
        /// <param name="qcwebid">QCWEBID</param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <note>
        /// Quillからのトランザクション取得は
        /// Seasar.Extension.Tx.Impl.LocalRequiredTxHandlerクラスを参考にしています
        /// </note>
        /// <returns>成功した場合true、失敗した場合false</returns>
        public static bool CreateData(decimal qcwebid, string dirpath, out QCWebException exception)
        {
            exception = null;
            ReadDBInfo readDBInfo = new ReadDBInfo();
            QuillInjector injector = QuillInjector.GetInstance();
            injector.Inject(readDBInfo);

            // ITransactionContextの取得
            ITransactionSetting s =
                (ITransactionSetting)ComponentUtil.GetComponent(injector.Container, typeof(TypicalTransactionSetting));
            ITransactionContext txc = s.TransactionContext;
            //OracleConnection conn = null;
            ITransactionContext parent = null;              // Quillが作成したトランザクション管理クラス
            ITransactionContext current = txc.Create();     // 新しいトランザクション管理クラスの作成
            try
            {
                using (current)
                {
                    parent = txc.Current;                   // 作成済みトランザクション管理クラスの保存
                    current.Parent = parent;                // 既にトランザクションが貼られていたら継続させるため親に入れる
                    current.Begin();                        // コネクションオープン
                    txc.Current = current;                  // 新たに作成したトランザクション管理クラスをトランザクション設定クラスに渡す
                                                            //conn = (OracleConnection)current.Connection;

                    //using (conn) {
                    using (DatabaseReader myReader = new DatabaseReader(current))
                    {
                        // ローデータテーブル単位で取得カラムをまとめる
                        var sw = System.Diagnostics.Stopwatch.StartNew();
                        ListResultBean<TItemInfo> itemList = readDBInfo.GetNormalItemList(qcwebid);
                        System.Diagnostics.Debug.WriteLine("かかった時間: " + sw.ElapsedMilliseconds.ToString() + "ms");
                        Dictionary<string, List<string[]>> map = new Dictionary<string, List<string[]>>();
                        foreach (TItemInfo entity in itemList)
                        {
                            decimal itemid = (decimal)entity.ItemInfoId;
                            string tableName = entity.TableName;
                            string columnName = entity.ColumnName;
                            bool isFA = entity.IsAnswerTypeFA;
                            DateTime lastUpdateDatetime = (DateTime)entity.LastUpdateDatetime;
                            string filepath = System.IO.Path.Combine(dirpath, itemid.ToString() + ".txt");

                            if (System.IO.File.Exists(filepath))
                            {
                                //DateTime fileCreated = System.IO.File.GetCreationTime(filepath);
                                //ファイルシステムのトンネリング機能によって、作成日時が引き継がれてしまうので更新日時を使い比較する
                                DateTime fileCreated = System.IO.File.GetLastWriteTime(filepath);
                                // 作りなおす必要ないアイテムは飛ばす
                                if (fileCreated >= lastUpdateDatetime)
                                {
                                    continue;
                                }
                                else
                                {
                                    // 作りなおす必要がある場合は削除
                                    try
                                    {
                                        System.IO.File.Delete(filepath);
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                                        Debug.Indent();
                                        Debug.WriteLine("Type:{0}", ex.GetType().ToString());
                                        Debug.WriteLine("Description:{0}", ex.Message);
                                        Debug.Unindent();
                                        exception = new QCWebException(new Message(ex.Message), ex);
                                        return false;
                                    }
                                }
                            }

                            List<string[]> entityList = null;
                            if (map.ContainsKey(tableName))
                            {
                                entityList = map[tableName];
                                entityList.Add(new string[] { columnName, itemid.ToString(), isFA.ToString() });
                                map.Remove(tableName);
                            }
                            else
                            {
                                entityList = new List<string[]>();
                                entityList.Add(new string[] { columnName, itemid.ToString(), isFA.ToString() });
                            }
                            map.Add(tableName, entityList);
                        }

                        GlobalMethodClass.GuaranteeDirectoryExist(dirpath);

                        foreach (string keyTableName in map.Keys)
                        {
                            List<string[]> entityList = map[keyTableName];
                            string[] columnNameArray = new string[entityList.Count];
                            decimal[] itemidArray = new decimal[entityList.Count];
                            bool[] isFA = new bool[entityList.Count];
                            for (int i = 0; i < entityList.Count; ++i)
                            {
                                string[] values = entityList[i];
                                columnNameArray[i] = values[0];
                                itemidArray[i] = decimal.Parse(values[1]);
                                isFA[i] = bool.Parse(values[2]);
                            }
                            string columns = string.Join(",", columnNameArray);
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("SELECT ");
                            sb.Append(columns);
                            sb.Append(" FROM ");
                            sb.Append(keyTableName);
                            sb.Append(" ORDER BY SORT_NO ASC");

                            //OracleCommand cmd = new OracleCommand(sb.ToString(), conn);
                            myReader.ExecuteReader(sb.ToString());
                            System.IO.StreamWriter[] writer = new System.IO.StreamWriter[entityList.Count];
                            string[] paths = new string[entityList.Count];

                            try
                            {
                                //using (OracleDataReader myReader = cmd.ExecuteReader()) {
                                //myReader.FetchSize = cmd.RowSize * 3000;
                                myReader.FetchSize = myReader.RowSize * 3000;
                                var watch = System.Diagnostics.Stopwatch.StartNew();
                                while (myReader.Read())
                                {   // 行のループ
                                    for (int i = 0; i < myReader.FieldCount; ++i)
                                    {    // 列のループ
                                        if (writer[i] == null)
                                        {
                                            paths[i] = System.IO.Path.Combine(dirpath, itemidArray[i].ToString() + ".txt");
                                            writer[i] = new System.IO.StreamWriter(paths[i], true, System.Text.Encoding.UTF8);
                                        }

                                        if (myReader.IsDBNull(i))
                                        {
                                            writer[i].WriteLine();
                                        }
                                        else
                                        {
                                            //if(keyTableName.ToLower().IndexOf("fa") > 0){
                                            if (isFA[i])
                                            {
                                                string val = myReader.GetString(i) == null ? "" : myReader.GetString(i);
                                                writer[i].WriteLine(System.Text.RegularExpressions.Regex.Escape(val));
                                            }
                                            else
                                            {
                                                writer[i].WriteLine(myReader.GetString(i));
                                            }
                                        }
                                    }
                                }
                                System.Diagnostics.Debug.WriteLine("1TBL当たりかかった時間: " + watch.ElapsedMilliseconds.ToString() + "ms");
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                                Debug.Indent();
                                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                                Debug.WriteLine("Description:{0}", e.Message);
                                Debug.Unindent();
                                try
                                {
                                    for (int i = 0; i < writer.Length; ++i)
                                    {
                                        if (writer[i] != null)
                                        {
                                            writer[i].Close();
                                            writer[i].Dispose();
                                            writer[i] = null;
                                        }

                                        if (paths[i] != null)
                                        {
                                            System.IO.File.Delete(paths[i]);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                                    Debug.Indent();
                                    Debug.WriteLine("Type:{0}", ex.GetType().ToString());
                                    Debug.WriteLine("Description:{0}", ex.Message);
                                    Debug.Unindent();
                                }
                                exception = new QCWebException(new Message(e.Message), e);
                                return false;
                            }
                            finally
                            {
                                for (int i = 0; i < writer.Length; ++i)
                                {
                                    if (writer[i] != null)
                                    {
                                        writer[i].Close();
                                        writer[i].Dispose();
                                        writer[i] = null;
                                    }
                                }
                            }
                        }

                        // 削除フラグファイルの作成
                        System.Text.StringBuilder sbDelFlag = new System.Text.StringBuilder();
                        sbDelFlag.Append("SELECT ");
                        sbDelFlag.Append("BASE_TABLE_NAME ");
                        sbDelFlag.Append("FROM ");
                        sbDelFlag.Append("T_TABLE_CONTROL ");
                        sbDelFlag.Append("WHERE ");
                        sbDelFlag.Append(" QCWEBID = " + qcwebid);
                        sbDelFlag.Append(" AND ROWNUM = 1");

                        //OracleCommand cmdDelFlag = new OracleCommand(sbDelFlag.ToString(), conn);
                        //using (OracleDataReader myReader = cmdDelFlag.ExecuteReader()) {
                        myReader.ExecuteReader(sbDelFlag.ToString());
                        {
                            myReader.Read();
                            //string baseTableName = (string)myReader[0];
                            string baseTableName = myReader.GetMyReader(0);
                            string filepath = null;
                            if (!CreateDeleteFlag(baseTableName, dirpath, out filepath, out exception))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (QCWebException)
            {
                throw;
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                exception = new QCWebException(new Message(e.Message), e);
                return false;
            }
            finally
            {
                txc.Current = parent;               // 現在使用中トランザクション管理クラスを親に戻す
                if (parent != null) txc.Current.Parent = null;      // 親がNullならその親もNull
            }

            return true;
        }
    }
    #endregion
}
