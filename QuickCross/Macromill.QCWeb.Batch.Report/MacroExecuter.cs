#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：MacroExecuter.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/4/5
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/5
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using Macromill.QCWeb.COMOperate;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using Macromill.QCWeb.Exceptions;
using System.Runtime.InteropServices;
using Macromill.QCWeb.Common;
using Seasar.Quill;

namespace Macromill.QCWeb.Batch.Report
{
    #region MacroExecuterクラス
    /// <summary>
    /// Excelマクロブックのオープンからマクロの実行までの制御を行うクラス
    /// </summary>
    [ComVisible(false), Guid("66CBFFB4-FF5A-46B1-9A4E-71EB55CBD97A")]
    public class MacroExecuter : IDisposable
    {
        private const string MACROBOOK_NAME = "test.xlsm";
        //private const string MACROBOOK_NAME = "blank.xlsm";
        private const string METHOD_NAME = "SetReportset";
        private const string TEMPLATE_BOOK_PASSWORD_KEY = "Excel.Template.Book.Password";
        private const string TEMPLATE_SHEET_PASSWORD_KEY = "Excel.Template.Sheet.Password";
        private const string OUTPUT_DIRECTORY_PATH_KEY = "Common.Temporary.Path.RP";

        private ExcelOperate excelOperator = null;
        private Excel.Workbook macroBook = null;
        private string templatebookpassword = null;
        private string templatesheetpassword = null;
        private string outputdirPath = null;
        private string macrobookDirectoryPath = null;

        /// <summary>アプリ環境設定情報共通クラス</summary>
        protected ApplicationConfig appConfig = null;

        /// <summary>
        /// レポートセット情報を与えてマクロを非同期実行する
        /// </summary>
        /// <param name="reportset">レポートセット情報を保持したReportsetクラスのインスタンスへの参照</param>
        public void ExecMacro(Macromill.QCWeb.Batch.Report.Reportsets.Reportset reportset) {
            if (macroBook == null) return;
            LateBind.RunMethodLateBind(macroBook, METHOD_NAME, reportset, templatebookpassword, templatesheetpassword, outputdirPath);
        }

        #region コンストラクタ
#if FOR_UNIT_TEST
        public
#else
        private
#endif
        bool init() {
            try {
                QuillInjector.GetInstance().Inject(this);
                templatebookpassword = appConfig.GetValue(TEMPLATE_BOOK_PASSWORD_KEY);
                templatesheetpassword = appConfig.GetValue(TEMPLATE_SHEET_PASSWORD_KEY);
                outputdirPath = appConfig.GetValue(OUTPUT_DIRECTORY_PATH_KEY);
                macrobookDirectoryPath = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_MACROBOOK_DIRECTORY_PATH);

                string path = System.IO.Path.Combine(macrobookDirectoryPath, MACROBOOK_NAME);
                if (!System.IO.File.Exists(path)) {
                    throw new QCWebException(Constants.MACROBOOK_NOT_FOUND_FATAL_MESSAGE_ID
                                , new string[] { path }, QCWeb.Common.GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                excelOperator = new ExcelOperate();
                Excel.Workbooks wbs = excelOperator.Excel.Workbooks;
                macroBook = wbs.Open(path, ReadOnly: true);
                COMWholeOperate.releaseComObject<Excel.Workbooks>(ref wbs);
                return true;
            } catch (Exception e) {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                Dispose();
                throw;
            }
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MacroExecuter()
        {
            init();
        }

        /// <summary>
        /// コンストラクタ<br />
        /// インスタンシングと同時にマクロの非同期実行を行う
        /// </summary>
        /// <param name="reportset">レポートセット情報を保持したReportsetクラスのインスタンスへの参照</param>
        public MacroExecuter(Macromill.QCWeb.Batch.Report.Reportsets.Reportset reportset)
        {
            if (init()) ExecMacro(reportset);
        }
        #endregion

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            if (macroBook != null)
            {
                try
                {
                    COMWholeOperate.releaseComObject<Excel.Workbook>(ref macroBook);
                }
                catch
                {
                }
            }
            if (excelOperator != null)
            {
                try
                {
                    excelOperator.Dispose();
                }
                finally
                {
                    excelOperator = null;
                }
            }
        }

        /// <summary>
        /// Excel.Application実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public ExcelOperate ExcelOperator 
        {
            get
            {
                return excelOperator; 
            }
        }

        /// <summary>
        /// Reportサーバ上の出力物置き場のパスを返す読み取り専用プロパティ
        /// </summary>
        public string OutputDirectoryPath
        {
            get
            {
                return outputdirPath;
            }
        }

        public Excel.Workbook MacroBook { get => macroBook; set => macroBook = value; }
        public string Templatesheetpassword { get => templatesheetpassword; set => templatesheetpassword = value; }
        public string Templatebookpassword { get => templatebookpassword; set => templatebookpassword = value; }
    }
    #endregion
}
