#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Reportset.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/2/23
 * 作　成　者：井川はるき
 * 更　新　日：2012/3/29
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Runtime.InteropServices;
using System.Collections;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Common;

namespace Macromill.QCWeb.Batch.Report
{
    /// <summary>
    /// レポートセットのコレクションクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("DD9ECD8F-69A7-4814-A26B-7FFBDF3FAF34")]
    public class Reportsets : Hashtable, IReportsets
    {
        /// <summary>
        /// レポートセットを扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("E2511A47-1829-4436-836A-03A2C8A38CBA")]
        public class Reportset : IReportset
        {
            private decimal id = 0;
            private FileType filetype = FileType.Excel;
            private string temppath = null;
            private string ulpath = null;
            private string fnprefix = null;
            private PowerPoint.PpSaveAsFileType ppfmt = PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation;
            private bool outputcommentflg = false;
            private string[] questionnaireinformation = new string[5];

            private Reportsets Collection = null;

            private Outputs outputs = null;
            private string divName = null;


#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            Reportset(Reportsets reportsets, decimal id, FileType filetype, string templatepath, string filenameprefix, PowerPoint.PpSaveAsFileType ppfileformat, bool outputcomment
                    , string questionnairepurpose, string questionnairemethod, string questionnaireterm, string questionnaireassignment, string questionnaireorganization, string divName)
            {
                Collection = reportsets;
                outputs = new Outputs(this);
                this.id = id;
                this.temppath = templatepath;
                this.fnprefix = filenameprefix;
                this.filetype = filetype;
                if (ppfileformat == PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation || ppfileformat == PowerPoint.PpSaveAsFileType.ppSaveAsPresentation)
                {
                    this.ppfmt = ppfileformat;
                }
                this.outputcommentflg = outputcomment;
                this.questionnaireinformation[0] = questionnairepurpose;    // 調査目的
                this.questionnaireinformation[1] = questionnairemethod;     // 調査方法
                this.questionnaireinformation[2] = questionnaireterm;       // 調査時期
                this.questionnaireinformation[3] = questionnaireassignment; // 割付有効回答数
                this.questionnaireinformation[4] = questionnaireorganization;   // 調査実施機関
                this.divName = divName;
            }

            /// <summary>
            /// レポートセットに紐づく出力物コレクションへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IOutputs Outputs
            {
                get
                {
                    return outputs;
                }
            }

            /// <summary>
            /// レポートセットIDを返す読み取り専用プロパティ
            /// </summary>
            [ComVisible(false)]
            public decimal ID
            {
                get
                {
                    return id;
                }
            }

            /// <summary>
            /// レポートセットIDを返す読み取り専用プロパティ
            /// <note>VBAから呼べるようにdouble</note>
            /// </summary>
            public double ID2
            {
                get
                {
                    return (double)id;
                }
            }

            /// <summary>
            /// 出力ファイルの種類を表すFileType列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public FileType FileType
            {
                get
                {
                    return filetype;
                }
            }

            /// <summary>
            /// 使用するPPテンプレートのパスを取得/設定するプロパティ<br />
            /// PowerPoint、PDFのいずれか、あるいは両方を出力する場合にのみ有効
            /// </summary>
            public string TemplatePath
            {
                get
                {
                    return temppath;
                }
                set
                {
                    temppath = value;
                }
            }

            /// <summary>
            /// アップロード先のパスを返す読み取り専用プロパティ
            /// </summary>
            public string UploadPath
            {
                get
                {
                    return ulpath;
                }
                internal set
                {
                    ulpath = value;
                }
            }

            /// <summary>
            /// 出力物のファイル名のプリフィックスを返す読み取り専用プロパティ
            /// </summary>
            public string FileNamePrefix
            {
                get
                {
                    return fnprefix;
                }
            }

            /// <summary>
            /// PowerPointレポートの保存ファイル形式を表すPpSaveAsFileType列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public PpSaveAsFileType PowerPointFileType
            {
                get
                {
                    return (PpSaveAsFileType)ppfmt;
                }
            }

            /// <summary>
            /// コメントを出力するかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool OutputComment
            {
                get
                {
                    return outputcommentflg;
                }
            }

            /// <summary>
            /// 調査票情報を保持した文字列型一次元配列への参照を返す読み取り専用プロパティ<br />
            /// 調査票の出力時のみ有効
            /// </summary>
            public string[] QuestionnaireInformation
            {
                get
                {
                    return questionnaireinformation;
                }
            }

            /// <summary>
            /// 分類アイテムの最大選択肢番号を返すメソッド
            /// </summary>
            /// <param name="keyItemName">分類アイテム名</param>
            /// <returns>最大選択肢番号</returns>
            public int KeyItemMaxSectorNumber(string keyItemName)
            {
                if (outputs == null) return 0;
                int maxSecNo = 0;
                foreach (Outputs.Output output in outputs.Values)
                {
                    if (output != null && output.Tables != null)
                    {
                        int tmpSecNo = (output.Tables as Tables).KeyItemMaxSectorNumber(keyItemName);
                        if (tmpSecNo > maxSecNo) maxSecNo = tmpSecNo;
                    }
                }
                return maxSecNo;
            }

            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                if (outputs != null) outputs.Dispose();
                Collection = null;
            }

            /// <summary>
            /// 自身のインスタンスが格納されているReportsetsコレクションクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IReportsets ParentCollection
            {
                get
                {
                    return Collection;
                }
            }

            /// <summary>
            /// 自身のインスタンスの親であるRequestクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IRequest ParentRequest
            {
                get
                {
                    if (Collection == null) return null;
                    return Collection.ParentRequest;
                }
            }

            /// <summary>
            /// 処理中ステータス(true:処理中 false:処理完了)
            /// </summary>
            public bool ProcStatus {
                get {
                    if (((Request)ParentRequest).StatusDescription != null) return false;   // 継続不可能なエラー発生時
                    foreach (Outputs.Output op in outputs.Values) {
                        if (op.Status == StatusCode.WaitRequest || (op.Status & StatusCode.Running) == StatusCode.Running) {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public string DivName { get => divName; set => divName = value; }
        }

        private Request request = null;

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        Reportsets(Request request)
        {
            this.request = request;
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        Reportset Add(decimal id, FileType filetype, string templatepath, string filenameprefix, PowerPoint.PpSaveAsFileType ppfileformat, bool outputcomment
                , string questionnairepurpose, string questionnairemethod, string questionnaireterm, string questionnaireassignment, string questionnaireorganization, string divName = null)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                Reportset newReportset = new Reportset(this, id, filetype, templatepath, filenameprefix, ppfileformat, outputcomment, questionnairepurpose, questionnairemethod, questionnaireterm, questionnaireassignment, questionnaireorganization, divName);
                this.Add(key, newReportset);
                return newReportset;
            }
            else
            {
                return this[key] as Reportset;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        Reportset Add(decimal id)
        {
            return Add(id, (FileType)0, null, null, (PowerPoint.PpSaveAsFileType)0, false, null, null, null, null, null);
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="key">キーとなる文字列</param>
        /// <returns>キーが示すコレクションの要素であるReportsetクラスのインスタンスへの参照</returns>
        public IReportset this[string key]
        {
            get
            {
                if (this.Contains(key))
                {
                    return base[key] as Reportset;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="id">レポートセットID</param>
        /// <returns>レポートセットIDが示すコレクションの要素であるReportsetクラスのインスタンスへの参照</returns>
        [ComVisible(false)]
        public IReportset this[decimal id]
        {
            get
            {
                string key = id.ToString();
                return this[key];
            }
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// <note>VBAから呼べるようにdouble</note>
        /// </summary>
        /// <param name="id">レポートセットID</param>
        /// <returns>レポートセットIDが示すコレクションの要素であるReportsetクラスのインスタンスへの参照</returns>
        public Reportset this[double id]
        {
            get
            {
                return this[(decimal)id] as Reportset;
            }
        }

        /// <summary>
        /// 要素数を返す読み取り専用プロパティ
        /// </summary>
        public new int Count
        {
            get
            {
                return (this as Hashtable).Count;
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (Reportset rs in this.Values)
            {
                rs.Dispose();
            }
            request = null;
        }

        /// <summary>
        /// 自身のインスタンスの親であるRequestクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IRequest ParentRequest
        {
            get
            {
                return request;
            }
        }
    }
}
