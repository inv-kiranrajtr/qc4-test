#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：COMOperate.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/2/20
 * 作　成　者：井川はるき
 * 更　新　日：2012/3/29
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Drawing;
using Macromill.QCWeb.Common;
using System.Windows.Forms;

namespace Macromill.QCWeb.COMOperate
{
    #region COMWholeOperateクラス
    /// <summary>
    /// COMを扱う処理全般で使用するメソッドを集めた静的クラス
    /// </summary>
    [ComVisible(false), Guid("F3A8D599-76DC-419d-94B2-91BB8C33106C")]
    public static class COMWholeOperate
    {
        /// <alias>releaseComObject00</alias>
        /// <summary>
        /// <para>エイリアス:releaseComObject00</para>
        /// COMオブジェクトの参照カウンタをデクリメントするreleaseComObjectオーバーロードメソッド群の根幹メソッド<br />
        /// また、実引数へのnull投入を伴う<br />
        /// <note>解放漏れを起こさないよう、COMオブジェクト解放時には必須実行</note>
        /// </summary>
        /// <typeparam name="T">参照型</typeparam>
        /// <param name="ComObject">解放するCOMオブジェクトへの参照</param>
        /// <param name="force">指定したCOMオブジェクトに関連付けられたRCWの参照カウンタを0にし、強制的にすべての参照を解放する場合はtrue</param>
        /// <returns>
        /// 成功時は、実行後の参照カウンタ(0以上(通常は0))を返す<br />
        /// また、引数ComObjectがnullの場合やCOMオブジェクトへの参照ではない場合は0を返す<br />
        /// 失敗時には、-1を返す
        /// </returns>
        /// <example>
        /// 次のサンプルは、新たにExcelを起動して、ワークブックを1つ新規で作成し、その1枚目のワークシートのA1セルの値、フォント色、背景色を設定します
        /// <code lang="C#">
        /// // Excel.Applicationクラスのインスタンスを生成
        /// Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        /// // 生成したExcelアプリケーションを表示
        /// xlApp.Visible = true;
        /// // Excelアプリケーションをユーザが起動したことにする
        /// xlApp.UserControl = true;
        /// // Excel.Workbooksコレクションクラスのインスタンスへの参照を取得
        /// Microsoft.Office.Interop.Excel.Workbooks xlBooks = xlApp.Workbooks;
        /// // 不要になったCOMオブジェクト(Excel.Applicationクラスのインスタンス)参照を解放
        /// Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject(xlApp);
        /// // ワークブックを追加して、そのExcel.Workbookクラスのインスタンスへの参照を取得
        /// Microsoft.Office.Interop.Excel.Workbook xlBook = xlBooks.Add();
        /// // 不要になったCOMオブジェクト(Excel.Workbooksクラスのインスタンス)参照を解放
        /// Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject(xlBooks);
        /// // ワークシートからなるExcel.Sheetsコレクションクラスのインスタンスへの参照を取得
        /// Microsoft.Office.Interop.Excel.Sheets xlSheets = xlBook.Worksheets;
        /// // 不要になったCOMオブジェクト(Excel.Workbookクラスのインスタンス)参照を解放
        /// Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject(xlBook);
        /// // 1枚目のワークシートを表すExcel.Worksheetクラスのインスタンスへの参照を取得
        /// Microsoft.Office.Interop.Excel.Worksheet xlSheet= (Microsoft.Office.Interop.Excel.Worksheet)xlSheets.Item[1];
        /// // 不要になったCOMオブジェクト(Excel.Sheetsクラスのインスタンス)参照を解放
        /// Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject(xlSheets);
        /// // A1セルを表すExcel.Rangeクラスのインスタンスへの参照を取得
        /// Microsoft.Office.Interop.Excel.Range xlRange = xlSheet.Range["A1"];
        /// // 不要になったCOMオブジェクト(Excel.Worksheetクラスのインスタンス)参照を解放
        /// Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject(xlSheet);
        /// // セルのフォントを表すExcel.Fontクラスのインスタンスへの参照を取得
        /// Microsoft.Office.Interop.Excel.Font xlFont = xlRange.Font;
        /// // セルの背景を表すExcel.Interiorクラスのインスタンスへの参照を取得
        /// Microsoft.Office.Interop.Excel.Interior xlInterior = xlRange.Interior;
        /// // セルの値を100にする
        /// xlRange.Value = 100;
        /// // 不要になったCOMオブジェクト(Excel.Rangeクラスのインスタンス)参照を解放
        /// Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject(xlRange);
        /// // フォント色を白にする
        /// xlFont.ColorIndex = 2;
        /// // フォントを太字にする
        /// xlFont.Bold = true;
        /// // 不要になったCOMオブジェクト(Excel.Fontクラスのインスタンス)参照を解放
        /// Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject(xlFont);
        /// // 背景色をインディゴにする
        /// xlInterior.ColorIndex = 55;
        /// // 不要になったCOMオブジェクト(Excel.Interiorクラスのインスタンス)参照を解放
        /// Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject(xlInterior);
        /// </code>
        /// </example>
        public static int releaseComObject<T>(ref T ComObject, bool force) where T : class
        {
            if (ComObject == null) return 0;
            try
            {
                int c = 0;
                if (System.Runtime.InteropServices.Marshal.IsComObject(ComObject))
                {
                    if (force)
                    {
                        c = System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ComObject);
                    }
                    {
                        c = System.Runtime.InteropServices.Marshal.ReleaseComObject(ComObject);
                    }
                }
                ComObject = null;
                return c;
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                return -1;
            }
        }

        /// <alias>releaseComObject01</alias>
        /// <summary>
        /// <para>エイリアス:releaseComObject01</para>
        /// COMオブジェクトの参照カウンタをデクリメントする<br />
        /// 引数forceにfalseを指定して、releaseComObject00に仲介
        /// </summary>
        /// <typeparam name="T">参照型</typeparam>
        /// <param name="ComObject">解放するCOMオブジェクトへの参照</param>
        /// <returns>
        /// 成功時は、実行後の参照カウンタ (0以上(通常は0))を返す<br />
        /// また、引数ComObjectがnullの場合やCOMオブジェクトへの参照ではない場合は0を返す<br />
        /// 失敗時には、-1を返す
        /// </returns>
        /// <seealso cref="M:Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject``1(``0@,System.Boolean)">releaseComObject00</seealso>
        public static int releaseComObject<T>(ref T ComObject) where T : class
        {
            return releaseComObject<T>(ref ComObject, false);
        }

        /// <alias>releaseComObject10</alias>
        /// <summary>
        /// <para>エイリアス:releaseComObject10</para>
        /// COMオブジェクトの参照カウンタをデクリメントする<br />
        /// releaseComObject00に仲介するが、実引数へのnull投入を伴わない
        /// </summary>
        /// <typeparam name="T">参照型</typeparam>
        /// <param name="ComObject">解放するCOMオブジェクトへの参照</param>
        /// <param name="force">指定したCOMオブジェクトに関連付けられたRCWの参照カウンタを0にし、強制的にすべての参照を解放する場合はtrue</param>
        /// <returns>
        /// 成功時は、実行後の参照カウンタ (0以上(通常は0))を返す<br />
        /// また、引数ComObjectがnullの場合やCOMオブジェクトへの参照ではない場合は0を返す<br />
        /// 失敗時には、-1を返す
        /// </returns>
        /// <seealso cref="M:Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject``1(``0@,System.Boolean)">releaseComObject00</seealso>
        public static int releaseComObject<T>(T ComObject, bool force) where T : class
        {
            return releaseComObject<T>(ref ComObject, force);
        }

        /// <alias>releaseComObject11</alias>
        /// <summary>
        /// <para>エイリアス:releaseComObject11</para>
        /// COMオブジェクトの参照カウンタをデクリメントする<br />
        /// releaseComObject01に仲介するが、実引数へのnull投入を伴わない
        /// </summary>
        /// <typeparam name="T">参照型</typeparam>
        /// <param name="ComObject">解放するCOMオブジェクトへの参照</param>
        /// <returns>
        /// 成功時は、実行後の参照カウンタ (0以上(通常は0))を返す<br />
        /// また、引数ComObjectがnullの場合やCOMオブジェクトへの参照ではない場合は0を返す<br />
        /// 失敗時には、-1を返す
        /// </returns>
        /// <seealso cref="M:Macromill.QCWeb.COMOperate.COMWholeOperate.releaseComObject``1(``0@)">releaseComObject01</seealso>
        public static int releaseComObject<T>(T ComObject) where T : class
        {
            return releaseComObject<T>(ref ComObject);
        }
    }
    #endregion

    #region LateBindクラス
    /// <summary>
    /// Early Bind不可なオブジェクトで定義したカスタムなインスタンスメンバをコールするメソッドを定義した静的クラス
    /// </summary>
    [ComVisible(false), Guid("9ED67A9D-3EC6-464b-856B-407865C7CB49")]
    public static class LateBind
    {
        /// <summary>
        /// Late Bindで実行するメンバの実行方法を表すコード
        /// </summary>
        [ComVisible(false)]
        public enum CallType : int
        {
            /// <summary>
            ///  メソッドの実行を表す
            /// </summary>
            RunMethod,
            /// <summary>
            /// プロパティの取得を表す
            /// </summary>
            GetProperty,
            /// <summary>
            /// プロパティの設定を表す
            /// </summary>
            SetProperty
        }

        /// <summary>
        /// Late Bindでメソッドを実行する
        /// </summary>
        /// <typeparam name="T">参照型</typeparam>
        /// <param name="obj">オブジェクトへの参照</param>
        /// <param name="MethodName">メソッド名</param>
        /// <param name="args">実行するメソッドの引数 (可変長)</param>
        /// <returns>メソッドの戻り値</returns>
        /// <example>
        /// 次のサンプルは、Excelを起動して、Excelブック「C:\temp\test.xls」のブックのカスタムメソッド「SubMethodTest」を実行します。<br />
        /// <code lang="C#">
        /// Excel.Application xlApp = new Excel.Application();
        /// xlApp.Visible = true;
        /// Excel.Workbooks xlWorkbooks = xlApp.Workbooks;
        /// COMWholeOperate.releaseComObject(xlApp);
        /// Excel.Workbook xlWorkbook = xlWorkbooks.Open(@"C:\temp\test.xls", ReadOnly: true);
        /// COMWholeOperate.releaseComObject(xlWorkbooks);
        /// RunMethodLateBind(xlWorkbook, "SubMethodTest");
        /// COMWholeOperate.releaseComObject(xlWorkbook);
        /// </code>
        /// <note>上のサンプルでは、Microsoft.Office.Interrop.Excelへの参照を追加し、usingディレクティブで以下の宣言を行う必要があります。</note>
        /// <code lang="C#">
        /// using Excel = Microsoft.Office.Interop.Excel;
        /// </code>
        /// 次のサンプルは、Excelを起動して、Excelブック「C:\temp\test.xls」のブックのカスタムメソッド「FunctionMethodTest」に引数5を与えて実行し、戻り値を表示します。
        /// <code lang="C#">
        /// Excel.Application xlApp = new Excel.Application();
        /// xlApp.Visible = true;
        /// Excel.Workbooks xlWorkbooks = xlApp.Workbooks;
        /// COMWholeOperate.releaseComObject(xlApp);
        /// Excel.Workbook xlWorkbook = xlWorkbooks.Open(@"C:\temp\test.xls", ReadOnly: true);
        /// COMWholeOperate.releaseComObject(xlWorkbooks);
        /// MessageBox.Show(RunMethodLateBind(xlWorkbook, "FunctionMethodTest", 5).ToString());
        /// COMWholeOperate.releaseComObject(xlWorkbook);
        /// </code>
        /// <note>上のサンプルでは、Microsoft.Office.Interrop.Excelへの参照を追加し、usingディレクティブで以下の宣言を行う必要があります。</note>
        /// <code lang="C#">
        /// using Excel = Microsoft.Office.Interop.Excel;
        /// </code>
        /// </example>
        public static object RunMethodLateBind<T>(T obj, string MethodName, params object[] args) where T : class
        {
            try
            {
                if (obj == null) return null;
                return obj.GetType().InvokeMember(MethodName, System.Reflection.BindingFlags.InvokeMethod, null, obj, args);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                throw;
            }
        }

        /// <summary>
        /// Late Bindでプロパティの値を設定する
        /// </summary>
        /// <typeparam name="T">参照型</typeparam>
        /// <param name="obj">オブジェクトへの参照</param>
        /// <param name="PropertyName">プロパティ名</param>
        /// <param name="value">設定する値</param>
        /// <param name="args">実行するプロパティの引数 (可変長)</param>
        /// <example>
        /// 次のサンプルは、Excelを起動して、Excelブック「C:\temp\test.xls」のブックのカスタムプロパティ「PropertyTest」に引数「test」を与えて、100を設定します。
        /// <code lang="C#">
        /// Excel.Application xlApp = new Excel.Application();
        /// xlApp.Visible = true;
        /// Excel.Workbooks xlWorkbooks = xlApp.Workbooks;
        /// COMWholeOperate.releaseComObject(xlApp);
        /// Excel.Workbook xlWorkbook = xlWorkbooks.Open(@"C:\temp\test.xls", ReadOnly: true);
        /// COMWholeOperate.releaseComObject(xlWorkbooks);
        /// string key = "test";
        /// SetPropertyLateBind(xlWorkbook, "PropertyTest", 100, key);
        /// COMWholeOperate.releaseComObject(xlWorkbook);
        /// </code>
        /// <note>上のサンプルでは、Microsoft.Office.Interrop.Excelへの参照を追加し、usingディレクティブで以下の宣言を行う必要があります。</note>
        /// <code lang="C#">
        /// using Excel = Microsoft.Office.Interop.Excel;
        /// </code>
        /// </example>
        public static void SetPropertyLateBind<T>(T obj, string PropertyName, object value, params object[] args) where T : class
        {
            try
            {
                if (obj == null) return;
                object[] arguments = null;
                if (args == null)
                {
                    arguments = new object[1];
                }
                else
                {
                    arguments = new object[args.Length + 1];
                    Array.Copy(args, arguments, args.Length);
                }
                arguments[arguments.GetUpperBound(0)] = value;
                obj.GetType().InvokeMember(PropertyName, System.Reflection.BindingFlags.SetProperty, null, obj, arguments);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                throw;
            }
        }

        /// <summary>
        /// Late Bindでプロパティの値を取得する
        /// </summary>
        /// <typeparam name="T">参照型</typeparam>
        /// <param name="obj">オブジェクトへの参照</param>
        /// <param name="PropertyName">プロパティ名</param>
        /// <param name="args">実行するプロパティの引数 (可変長)</param>
        /// <returns>プロパティの値</returns>
        /// <example>
        /// 次のサンプルは、Excelを起動して、Excelブック「C:\temp\test.xls」のブックのカスタムプロパティ「PropertyTest」に引数「test」を与えて、その値を表示します。<br />
        /// その後カスタムプロパティ「PropertyTest」に引数「test」を与えて100を設定し、改めて「PropertyTest」に引数「test」を与えて、値の変更を確認します。<br />
        /// <code lang="C#">
        /// Excel.Application xlApp = new Excel.Application();
        /// xlApp.Visible = true;
        /// Excel.Workbooks xlWorkbooks = xlApp.Workbooks;
        /// COMWholeOperate.releaseComObject(xlApp);
        /// Excel.Workbook xlWorkbook = xlWorkbooks.Open(@"C:\temp\test.xls", ReadOnly: true);
        /// COMWholeOperate.releaseComObject(xlWorkbooks);
        /// string key = "test";
        /// MessageBox.Show(GetPropertyLateBind(xlWorkbook, "PropertyTest", key).ToString());
        /// SetPropertyLateBind(xlWorkbook, "PropertyTest", 100, key);
        /// MessageBox.Show(GetPropertyLateBind(xlWorkbook, "PropertyTest", key).ToString());
        /// COMWholeOperate.releaseComObject(xlWorkbook);
        /// </code>
        /// <note>上のサンプルでは、Microsoft.Office.Interrop.Excelへの参照を追加し、usingディレクティブで以下の宣言を行う必要があります。</note>
        /// <code lang="C#">
        /// using Excel = Microsoft.Office.Interop.Excel;
        /// </code>
        /// </example>
        public static object GetPropertyLateBind<T>(T obj, string PropertyName, params object[] args) where T : class
        {
            try
            {
                if (obj == null) return null;
                return obj.GetType().InvokeMember(PropertyName, System.Reflection.BindingFlags.GetProperty, null, obj, args);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                throw;
            }
        }
    }
    #endregion

    #region ExcelOperateクラス
    /// <summary>
    /// IDisposableインターフェイスを実装し解放を容易にした、Excel.Applicationインターフェイスの実装クラスのインスタンスを扱うクラス
    /// </summary>
    [ComVisible(false), Guid("3599FBBB-264A-42E6-AEFC-4B6D338988C6")]
    public class ExcelOperate : IDisposable
    {
        private Excel.Application xlApp = null;
        private int processId = 0;
        private IntPtr threadId = new IntPtr(0);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExcelOperate()
        {
            xlApp = new Excel.Application();
            IntPtr hwnd = new IntPtr(xlApp.Hwnd);
            threadId = GetWindowThreadProcessId(hwnd, out processId);
        }

        /// <summary>
        /// このインスタンスに関連付けられているExcel.Applicationインターフェイスの実装クラスのインスタンスへの参照を返す
        /// </summary>
        public Excel.Application Excel
        {
            get
            {
                try
                {
                    bool check = xlApp.Ready;
                }
                catch (Exception ex)
                {
                    xlApp = new Excel.Application();
                    IntPtr hwnd = new IntPtr(xlApp.Hwnd);
                    threadId = GetWindowThreadProcessId(hwnd, out processId);
                }
                return xlApp;
            }
        }

        /// <summary>
        /// このインスタンスに関連付けられているExcel.Applicationインターフェイスの実装クラスのインスタンスのプロセスIDを返す読み取り専用プロパティ
        /// </summary>
        public int ProcessId
        {
            get
            {
                return processId;
            }
        }

        /// <summary>
        /// このインスタンスに関連付けられているExcel.Applicationインターフェイスの実装クラスのインスタンスのスレッドIDを返す読み取り専用プロパティ
        /// </summary>
        public IntPtr ThreadId
        {
            get
            {
                return threadId;
            }
        }

        /// <summary>
        /// Excel.Applicationインターフェイスの実装クラスのメンバをLate Bindで実行するメソッド
        /// </summary>
        /// <param name="ProcName">メンバ名</param>
        /// <param name="CallType">メンバの実行方法を表すCallType列挙型の値</param>
        /// <param name="Args">引数 (可変長)<note>プロパティの設定時には1つ以上必要。最後の1つが設定値となる</note></param>
        /// <returns></returns>
        public object CallByName(string ProcName, LateBind.CallType CallType, params object[] Args)
        {
            switch (CallType)
            {
                case LateBind.CallType.RunMethod:
                    return LateBind.RunMethodLateBind(xlApp, ProcName, Args);
                case LateBind.CallType.GetProperty:
                    return LateBind.GetPropertyLateBind(xlApp, ProcName, Args);
                case LateBind.CallType.SetProperty:
                    if (Args == null) return null;
                    object value = Args[Args.GetUpperBound(0)];
                    if (Args.Length == 1)
                    {
                        Args = null;
                    }
                    else
                    {
                        Array.Resize<object>(ref Args, Args.Length - 1);
                    }
                    LateBind.SetPropertyLateBind(xlApp, ProcName, value, Args);
                    return null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// すべてのブックを閉じる
        /// </summary>
        public void CloseAllBooks()
        {
            if (xlApp == null) return;
            Excel.Workbooks wbs = null;
            Excel.Workbook wb = null;
            try
            {
                wbs = xlApp.Workbooks;
                for (int i = wbs.Count; i > 0; --i)
                {
                    wb = wbs[i];
                    wb.Close(false);
                    COMWholeOperate.releaseComObject(ref wb);
                }
            }
            finally
            {
                COMWholeOperate.releaseComObject(ref wb);
                COMWholeOperate.releaseComObject(ref wbs);
            }
        }

        /// <summary>
        /// ワークシートへの参照を返す静的メソッド
        /// </summary>
        /// <param name="shts">目的のワークシートを含むシートコレクションへの参照</param>
        /// <param name="name">目的のワークシートの名前</param>
        /// <returns>目的のワークシートへの参照、存在しない場合はnull</returns>
        public static Excel.Worksheet GetWorksheetReference(Excel.Sheets shts, string name)
        {
            try
            {
                return shts[name] as Excel.Worksheet;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// <paramref name="startcell"/>の右下(<paramref name="startcell"/>が基点のセル範囲で、値の入っているセル群によって構成される矩形の領域の右下端のセルへの参照を返す
        /// </summary>
        /// <param name="startcell">左上端のセルへの参照</param>
        /// <param name="lastcell">右下端のセルへの参照 (戻り値)</param>
        public static void GetLastCell(Excel.Range startcell, out Excel.Range lastcell)
        {
            lastcell = null;
            if (startcell == null) return;
            Excel.Worksheet sht = startcell.Worksheet;
            // 同じセル範囲への参照を各種Rangeオブジェクトで
            Excel.Range rows = sht.Rows;
            Excel.Range cols = sht.Columns;
            Excel.Range cells = sht.Cells;
            // startcell以降(右下)のセル範囲
            Excel.Range roughlyRange = startcell.get_Resize(
                    rows.Count - startcell.Row + 1, cols.Count - startcell.Column + 1);
            COMWholeOperate.releaseComObject(ref rows);
            COMWholeOperate.releaseComObject(ref cols);
            Excel.Range usedRange = sht.UsedRange;
            COMWholeOperate.releaseComObject(ref sht);
            Excel.Application xlApp = startcell.Application;
            // 検索範囲
            Excel.Range searchRange = xlApp.Intersect(roughlyRange, usedRange);
            COMWholeOperate.releaseComObject(ref xlApp);
            COMWholeOperate.releaseComObject(ref roughlyRange);
            COMWholeOperate.releaseComObject(ref usedRange);
            if (searchRange != null)
            {
                if (searchRange.Count == 1)
                {
                    object v = searchRange.Value;
                    if (v != null)
                    {
                        string tmp = v.ToString();
                        if (!string.IsNullOrEmpty(tmp))
                        {
                            lastcell = searchRange.Cells;   // searchRange解放でlastcellが切断されないように
                        }
                    }
                }
                else
                {
                    Excel.XlFindLookIn lookin = Microsoft.Office.Interop.Excel.XlFindLookIn.xlValues;
                    Excel.XlLookAt lookat = Microsoft.Office.Interop.Excel.XlLookAt.xlWhole;
                    Excel.XlSearchDirection searchdirection = Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious;
                    // 下端行のどこかのセル
                    Excel.Range endRowCell = searchRange.Find("*", startcell
                            , lookin, lookat, Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows, searchdirection);
                    Excel.Range endColumnCell = null;
                    if (endRowCell != null) // 値の入っているセルがある
                    {
                        // 右端列のどこかのセル
                        endColumnCell = searchRange.Find("*", startcell
                                , lookin, lookat, Microsoft.Office.Interop.Excel.XlSearchOrder.xlByColumns, searchdirection);
                        // endRowCellがnullでなければendColumnCellもnullではない
                        int endRowIndex = endRowCell.Row;
                        int endColumnIndex = endColumnCell.Column;
                        COMWholeOperate.releaseComObject(ref endRowCell);
                        COMWholeOperate.releaseComObject(ref endColumnCell);
                        lastcell = cells.get_Item(endRowIndex, endColumnIndex) as Excel.Range;
                    }
                }
                COMWholeOperate.releaseComObject(ref searchRange);
            }
            COMWholeOperate.releaseComObject(ref cells);
        }

        /// <summary>
        /// ワークシート内の、値の入っているセル群によって構成される矩形の領域の右下端のセルへの参照を返す
        /// </summary>
        /// <param name="sht">ワークシートへの参照</param>
        /// <param name="lastcell">右下端のセルへの参照 (戻り値)</param>
        public static void GetLastCell(Excel.Worksheet sht, out Excel.Range lastcell)
        {
            lastcell = null;
            if (sht == null) return;
            Excel.Range startcell = sht.get_Range("A1");
            GetLastCell(startcell, out lastcell);
            COMWholeOperate.releaseComObject(ref startcell);
        }

        /// <summary>
        /// セル範囲の値からなる二次元配列を返す
        /// </summary>
        /// <param name="range">セル範囲への参照</param>
        /// <returns>
        /// セル範囲の値からなる二次元配列
        /// <note>
        /// 通常1ベースの配列を返す<br />
        /// <paramref name="range"/>に与えられたセルが1つだけの場合は、2×2の0ベースの配列を返し、1,1の箇所に値を持つ
        /// </note>
        /// </returns>
        public static object[,] GetValueArray(Excel.Range range)
        {
            if (range == null) return null;
            Excel.Range rng = range.Cells;
            object[,] res = null;
            if (rng.Count == 1)
            {
                res = new object[2, 2];
                res[1, 1] = rng.Value;
            }
            else
            {
                res = rng.Value as object[,];
            }
            COMWholeOperate.releaseComObject(ref rng);
            return res;
        }

        /// <summary>
        /// セル範囲の値からなる二次元配列を返す
        /// </summary>
        /// <param name="startCell">
        /// 値を取得するセル範囲の左上端のセルへの参照
        /// <note>2つ以上のセルからなるセル範囲への参照を指定したり、あるいは2つ以上のセル範囲からなるエリア群への参照を指定しても、1つ目のセル範囲の左上端のセルを採用する</note>
        /// </param>
        /// <returns>
        /// <paramref name="startCell"/>を左上端とし、そのワークシートの値が入っている領域の右下端のセルを右下端とするセル範囲の値からなる二次元配列
        /// <note>
        /// 通常1ベースの配列を返す<br />
        /// 取得するセル範囲のセルが1つだけの場合は、2×2の0ベースの配列を返し、1,1の箇所に値を持つ
        /// </note>
        /// </returns>
        public static object[,] GetSheetValueArray(Excel.Range startCell)
        {
            if (startCell == null) return null;
            Excel.Worksheet sht = startCell.Worksheet;
            Excel.Range lastcell = null;
            GetLastCell(sht, out lastcell);
            object[,] res = null;
            if (lastcell != null)
            {
                if (startCell.Row <= lastcell.Row && startCell.Column <= lastcell.Column)
                {
                    Excel.Areas areas = startCell.Areas;
                    Excel.Range rng = areas.get_Item(1);
                    COMWholeOperate.releaseComObject(ref areas);
                    Excel.Range startcell = rng.get_Item(1, 1) as Excel.Range;
                    COMWholeOperate.releaseComObject(ref rng);
                    Excel.Range range = sht.get_Range(startcell, lastcell);
                    COMWholeOperate.releaseComObject(ref startcell);
                    res = GetValueArray(range);
                    COMWholeOperate.releaseComObject(ref range);
                }
                COMWholeOperate.releaseComObject(ref lastcell);
            }
            COMWholeOperate.releaseComObject(ref sht);
            return res;
        }

        private static string[,] convertToStringArray(object[,] sourceArray)
        {
            if (sourceArray == null) return null;
            string[,] res = new string[sourceArray.GetUpperBound(0), sourceArray.GetUpperBound(1)];
            for (int i = 1; i <= sourceArray.GetUpperBound(0); ++i)
            {
                for (int j = 1; j <= sourceArray.GetUpperBound(1); ++j)
                {
                    object tmp = sourceArray[i, j];
                    if (tmp != null)
                    {
                        string buf = GlobalMethodClass.CleanSpecialCharacters(tmp.ToString());
                        if (!string.IsNullOrEmpty(buf)) res[i - 1, j - 1] = buf;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// セル範囲の文字列値からなる二次元配列を返す
        /// </summary>
        /// <param name="range">セル範囲への参照</param>
        /// <returns>セル範囲の値からなる0ベースの二次元配列</returns>
        public static string[,] GetStringValueArray(Excel.Range range)
        {
            return convertToStringArray(GetValueArray(range));
        }

        /// <summary>
        /// セル範囲の文字列値からなる二次元配列を返す
        /// </summary>
        /// <param name="startCell">
        /// 値を取得するセル範囲の左上端のセルへの参照
        /// <note>2つ以上のセルからなるセル範囲への参照を指定したり、あるいは2つ以上のセル範囲からなるエリア群への参照を指定しても、1つ目のセル範囲の左上端のセルを採用する</note>
        /// </param>
        /// <returns>
        /// <paramref name="startCell"/>を左上端とし、そのワークシートの値が入っている領域の右下端のセルを右下端とするセル範囲の値からなる0ベースの二次元配列
        /// </returns>
        public static string[,] GetSheetStringValueArray(Excel.Range startCell)
        {
            return convertToStringArray(GetSheetValueArray(startCell));
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (xlApp.Ready)    // 印刷プレビューはありえないので無視
                {
                    CloseAllBooks();
                    xlApp.DisplayAlerts = false;
                    xlApp.UserControl = false;
                    xlApp.Quit();
                }
                else
                {
                    throw new Exception();
                }
				try
				{
					if (processId != 0)
					{
						System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById(processId);
						process.Kill();
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
                    if (processId != 0)
                    {
                        System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById(processId);
                        process.Kill();
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
            }
            finally
            {
                try
                {
                    COMWholeOperate.releaseComObject(ref xlApp);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                }
            }
        }
    }
    #endregion

    #region ExecuteStaticMethodクラス
    /// <summary>
    /// COM側からstaticメソッドを実行するための、インスタンシング可能な仲介クラス
    /// </summary>
    [ComVisible(true), ClassInterfaceAttribute(ClassInterfaceType.AutoDual), GuidAttribute("D0031A41-BDF9-40F6-A74C-B91A40088F47")]
    public class ExecuteStaticMethod
    {
        /*
        /// <summary>
        /// レイアウトイメージ二次元配列を生成する
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids"><paramref name="questions"/>内での出力対象の質問IDからなる配列</param>
        /// <param name="datatype">出力データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="orientation">レイアウト表の向きを表すLayoutOrientation列挙型の値</param>
        /// <param name="resultArray">レイアウトイメージの二次元配列 (戻り値)</param>
        public void GetLayoutArray(Question.Questions questions, ref decimal[] questionids
                    , ReportRequest.OutputDataType datatype, ReportRequest.LayoutOrientation orientation
                    , ref string[,] resultArray)
        {
            Tabulation.RawDataTabulation.GetLayoutArray(questions, questionids, datatype, orientation, out resultArray);
        }
        */

        /// <summary>
        /// 一定のフォントでの文字列の行数を算出するメソッド
        /// </summary>
        /// <param name="buffer">行数を調べる文字列</param>
        /// <param name="fontName">フォント名</param>
        /// <param name="fontPointSize">フォントサイズ (ポイント値)</param>
        /// <param name="areaPointWidth">表示領域の幅 (ポイント値)</param>
        /// <param name="leftPointMargin">左余白 (ポイント値)</param>
        /// <param name="rightPointMargin">右余白 (ポイント値)</param>
        /// <param name="fontBold">太字の場合true (省略可:既定値false)</param>
        /// <param name="fontItalic">斜体の場合true (省略可:既定値false)</param>
        /// <param name="delimiterPattern">改行コードとする文字列の正規表現パターン (省略可:既定値「\r\n|\r|\n」)</param>
        /// <returns>成功時:行数、引数不正時:0、表示領域幅が小さすぎる時:-1</returns>
        public int GetRowsCount(string buffer
                    , string fontName, float fontPointSize
                    , float areaPointWidth, float leftPointMargin, float rightPointMargin
                    , bool fontBold = false, bool fontItalic = false
                    , string delimiterPattern = @"\r\n|\r|\n")
        {
            FontStyle fontStyle = FontStyle.Regular;
            if (fontBold) fontStyle |= FontStyle.Bold;
            if (fontItalic) fontStyle |= FontStyle.Italic;
            int w = 0;
            Font font = null;
            try
            {
                font = new Font(fontName, fontPointSize, fontStyle);
            }
            catch (Exception)
            {
                return w;
                throw;
            }
            using (font)
            {
                int areaWidth = GlobalMethodClass.PointToPixel(areaPointWidth);
                int leftMargin = GlobalMethodClass.PointToPixel(leftPointMargin);
                int rightMargin = GlobalMethodClass.PointToPixel(rightPointMargin);
                w = GlobalMethodClass.GetRowsCount(buffer, font, areaWidth, leftMargin, rightMargin, delimiterPattern);
            }
            return w;
        }

        /// <summary>
        /// Excelマクロで必要なメッセージ情報を返すメソッド
        /// </summary>
        /// <param name="index">分類コード込のインデックスを表すConstants.ReportMessageIndex列挙型の値</param>
        /// <param name="lccd">ロケーション情報を表す2文字の文字列</param>
        /// <param name="joinedReplaceWords">パラメータリストをTAB区切りで連結した文字列 (省略可)</param>
        /// <returns>メッセージ情報を保持したMessageクラスのインスタンスへの参照</returns>
        public Common.Message GetMessage(Constants.ReportMessageIndex index, string lccd, string joinedReplaceWords = null)
        {
            if (joinedReplaceWords == null) return new Common.Message(index, lccd);
            return new Common.Message(index, lccd, joinedReplaceWords.Split('\t'));
        }

        /// <summary>
        /// Excelマクロで必要なリソースデータを返すメソッド
        /// </summary>
        /// <param name="index">レポートメッセージを表すConstants.ReportMessageIndex列挙型の値</param>
        /// <param name="lccd">ロケーション情報を表す2文字の文字列 (省略可、既定値ja)</param>
        /// <param name="joinedReplaceWords">パラメータリストをTAB区切りで連結した文字列 (省略可)</param>
        /// <param name="unescape">アンエスケープが必要な場合はtrue (省略可、既定値false)</param>
        /// <returns></returns>
        public string GetReportKeyword(Constants.ReportMessageIndex index, string lccd = "ja", string joinedReplaceWords = null, bool unescape = false)
        {
            // string msgId = Constants.GlobalConstants.REPORT_ID + ((int)index).ToString(new string('0', 7));
            // if (joinedReplaceWords == null) return GetResource.GetCommonResourceData(msgId, lccd);
            // return GetResource.GetCommonResourceData(msgId, lccd, joinedReplaceWords.Split('\t'));
            if (joinedReplaceWords == null) return GetResource.GetReportKeyword(index, lccd, unescape);
            return GetResource.GetReportKeyword(index, lccd, unescape, joinedReplaceWords.Split('\t'));
        }

        /// <summary>
        /// ピクセル値からポイント値を求める静的メソッド
        /// </summary>
        /// <param name="pixel">ピクセル値</param>
        /// <returns>ポイント値</returns>
        public float PixelToPoint(int pixel)
        {
            return GlobalMethodClass.PixelToPoint(pixel);
        }

        /// <summary>
        /// ポイント値からピクセル値を求める静的メソッド
        /// <note>換算の結果が小数を含む場合、整数に切り上げる</note>
        /// </summary>
        /// <param name="point">ポイント値</param>
        /// <returns>ピクセル値</returns>
        public int PointToPixel(float point)
        {
            return GlobalMethodClass.PointToPixel(point);
        }

        /// <summary>文字列データのMD5ハッシュ値を返す</summary>
        /// <param name="buffer">文字列データ</param>
        /// <returns>MD5ハッシュ値</returns>
        public string GetHash(string buffer)
        {
            return GlobalMethodClass.getHash(buffer);
        }

        /// <summary>ファイルのMD5ハッシュを返す</summary>
        /// <param name="path">ファイルのパス</param>
        /// <returns>ファイルのMD5ハッシュ値</returns>
        public string GetFileHash(string path)
        {
            return GlobalMethodClass.getFileHash(path);
        }

        /// <summary>
        /// クリップボードからBITMAP以外のデータをクリアする
        /// </summary>
        /// <returns>処理後のクリップボード内のBITMAPデータの有無</returns>
        public bool ClearClipboardExceptBitmap()
        {
            bool res = false;
            IDataObject data = Clipboard.GetDataObject();
            DataObject newData = new DataObject();
            if (data.GetDataPresent(DataFormats.Bitmap))
            {
                Bitmap bmp = data.GetData(DataFormats.Bitmap) as Bitmap;
                res = bmp != null;
                if (res) newData.SetData(bmp);
            }
            Clipboard.Clear();
            Clipboard.SetDataObject(newData, true);
            return res;
        }
    }
    #endregion
}
