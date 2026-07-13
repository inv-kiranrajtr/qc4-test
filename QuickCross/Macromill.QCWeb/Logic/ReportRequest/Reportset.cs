#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Reportset.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/3/23
 * 作　成　者：井川はるき
 * 更　新　日：2012/3/30
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections;
/*
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
*/
using Seasar.Quill;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Common;

namespace Macromill.QCWeb.ReportRequest
{
    /// <summary>
    /// 出力命令側から扱うレポートセットのコレクションクラス
    /// </summary>
    [ComVisible(false), Guid("9B84B9A0-EFF7-4082-95D6-AE447C3C3936")]
    public class Reportsets : Hashtable, IReportsets
    {
        /// <summary>
        /// 出力命令側から扱うレポートセットを扱うクラス
        /// </summary>
        [ComVisible(false), Guid("13B3B211-85D3-4dbd-BB90-F71B243B606D")]
        public class Reportset : IReportset
        {
            /// <summary>
            /// 調査票情報コード
            /// </summary>
            [ComVisible(true)]
            public enum QuestionnaireInformationCode
            {
                /// <summary>
                /// 調査目的
                /// </summary>
                Purpose,
                /// <summary>
                /// 調査方法
                /// </summary>
                Method,
                /// <summary>
                /// 調査時期
                /// </summary>
                Term,
                /// <summary>
                /// 割付・有効回答数
                /// </summary>
                Assignment,
                /// <summary>
                /// 調査実施機関
                /// </summary>
                Organization
            }

            private decimal id = 0;
            private FileType filetype = FileType.Excel;
            private string temppath = null;
            private string fnprefix = null;
            // private PowerPoint.PpSaveAsFileType ppfmt = PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation;
            private Common.PpSaveAsFileType ppfmt = Common.PpSaveAsFileType.ppSaveAsOpenXMLPresentation;
            private bool outputcommentflg = false;
            private string[] questionnaireinformation = new string[5];
            private decimal templateId = 0;

            private Reportsets Collection = null;

            private Outputs outputs = null;
            private string divName = null;

            public Reportset(Reportsets reportsets, decimal id, FileType filetype, string templatepath, string filenameprefix, Common.PpSaveAsFileType ppfileformat, bool outputcomment
                    , string questionnairepurpose, string questionnairemethod, string questionnaireterm, string questionnaireassignment, string questionnaireorganization)
            {
                Collection = reportsets;
                outputs = new Outputs(this);
                this.id = id;
                this.temppath = templatepath;
                this.fnprefix = filenameprefix;
                this.filetype = filetype;
                if (ppfileformat == Common.PpSaveAsFileType.ppSaveAsOpenXMLPresentation || ppfileformat == Common.PpSaveAsFileType.ppSaveAsPresentation)
                {
                    this.ppfmt = ppfileformat;
                }
                this.outputcommentflg = outputcomment;
                this.questionnaireinformation[0] = questionnairepurpose;    // 調査目的
                this.questionnaireinformation[1] = questionnairemethod;     // 調査方法
                this.questionnaireinformation[2] = questionnaireterm;       // 調査時期
                this.questionnaireinformation[3] = questionnaireassignment; // 割付有効回答数
                this.questionnaireinformation[4] = questionnaireorganization;   // 調査実施機関
            }


            internal Reportset(Reportsets reportsets) 
            {
                Collection = reportsets;
                outputs = new Outputs(this);
            }

            internal void InsertToDB(decimal requestid)
            {
                if ((filetype & ReportRequest.FileType.Report) == ReportRequest.FileType.Report
                    && temppath == null) 
                {
                    //throw new QCWebException(
                    //    string.Format("レポートセットの構成が不正です。 出力ファイル種別：{0} PPテンプレートパス：{1}", filetype, temppath));    // レポートセットの構成が不正
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustReportsetConstructionWithDetailMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , GetResource.GetLogMessage(Constants.OUTPUT_FILETYPE_AND_POWERPOINT_TEMPLATE_PATH_MESSAGE_ID, filetype.ToString(), temppath));
                }

                /*
                bool existOutputques = false;
                foreach (DictionaryEntry de in outputs)
                {
                    Outputs.Output output = de.Value as Outputs.Output;
                    if (output.OutputType == OutputType.Questionnaire)
                    {
                        existOutputques = true;
                        break;
                    }
                }
                if (existOutputques && questionnaireinformation.Contains(null))
                    throw new QCWebException("レポートセットの構成が不正");    // レポートセットの構成が不正
                */
                if (questionnaireinformation.Contains(null))
                {
                    foreach (DictionaryEntry de in outputs)
                    {
                        Outputs.Output output = de.Value as Outputs.Output;
                        if (output.OutputType == OutputType.Questionnaire)
                        {
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustReportsetConstructionWithDetailMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , GetResource.GetLogMessage(Constants.EXIST_NULL_IN_QUESTIONNAIRE_INFORMATION_MESSAGE_ID));
                        }
                    }
                }

                // レポートセット情報の書き込み
                TOutputReportsetInfo entity = new TOutputReportsetInfo();
                entity.OutputFileTypeCode = (int)filetype;  // 出力ファイル形式コード
                entity.ReportFilenNamePrefix = fnprefix;    // レポートファイル名プリフィックス
                entity.CommentOutputFlag = outputcommentflg ? 1 : 0;    // コメント出力フラグ
                entity.PowerpointType = (int)ppfmt;  // PowerPoint出力形式
                if (templateId != 0) {
                    entity.OutputTemplateId = templateId;    // テンプレートID
                }

                (ParentRequest as Request).TOutputReportsetInfoBhv.Insert(entity);
                // 新規レコードのID取得
                this.id = (decimal)entity.OutputReportsetInfoId;

                outputs.InsertToDB();
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
            public decimal ID
            {
                get
                {
                    return id;
                }
            }

            /// <summary>
            /// 出力ファイルの種類を表すFileType列挙型の値を取得/設定するプロパティ
            /// </summary>
            public FileType FileType
            {
                get 
                {
                    return filetype;
                }
                set
                {
                    value &= ReportRequest.FileType.Excel | ReportRequest.FileType.PowerPoint | ReportRequest.FileType.PDF | ReportRequest.FileType.Report;
                    if ((int)value == 0) return;
                    filetype = value;
                }
            }

            /// <summary>
            /// 使用するPPテンプレートのパスを取得/設定するプロパティ<br />
            /// PowerPoint、PDFのいずれか、あるいは両方を出力する場合にのみ有効<br />
            /// 設定は1度のみ可能
            /// </summary>
            public string TemplatePath
            {
                get 
                {
                    return temppath;
                }
                set
                {
                    if (temppath == null) temppath = value;
                }
            }

            /// <summary>
            /// 出力物のファイル名のプリフィックスを取得/設定するプロパティ<br />
            /// 設定は1度のみ可能
            /// </summary>
            public string FileNamePrefix
            {
                get 
                {
                    return fnprefix;
                }
                set
                {
                    if (fnprefix == null) fnprefix = value;
                }
            }

            /// <summary>
            /// PowerPointレポートの保存ファイル形式を表すPpSaveAsFileType列挙型の値を取得/設定するプロパティ<br />
            /// 設定できる値は、PpSaveAsFileType.ppSaveAsOpenXMLPresentation (PowerPoint 2007形式を表す)、PpSaveAsFileType.ppSaveAsPresentation (PowerPoint 2003形式を表す)のいずれか
            /// </summary>
            public Common.PpSaveAsFileType PowerPointFileType
            {
                get
                {
                    // return (Common.PpSaveAsFileType)ppfmt;
                    return ppfmt;
                }
                set
                {
                    switch (value)
                    {
                        case Common.PpSaveAsFileType.ppSaveAsOpenXMLPresentation:
                        case Common.PpSaveAsFileType.ppSaveAsPresentation:
                            // ppfmt = (PowerPoint.PpSaveAsFileType)value;
                            ppfmt = value;
                            break;
                    }
                }
            }

            /// <summary>
            /// コメントを出力するかどうかを取得/設定するプロパティ
            /// </summary>
            public bool OutputComment
            {
                get
                {
                    return outputcommentflg;
                }
                set
                {
                    outputcommentflg = value;
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
            /// 調査票情報を個別に返すメソッド<br />
            /// 調査票の出力時のみ有効
            /// </summary>
            /// <param name="questionnaireInformationCode">取得する情報の内容を表すQuestionnaireInformationCode列挙型の値</param>
            /// <returns>questionnaireInformationCodeが示す種類の調査票情報</returns>
            public string GetQuestionnaireInformation(QuestionnaireInformationCode questionnaireInformationCode)
            {
                if (!Enum.IsDefined(typeof(QuestionnaireInformationCode), questionnaireInformationCode)) return null;
                int index = (int)questionnaireInformationCode;
                return questionnaireinformation[index];
            }

            /// <summary>
            /// 調査票情報を保持した文字列型一次元配列の要素を設定するメソッド<br />
            /// 設定は各要素ごとに1度のみ可能
            /// </summary>
            /// <param name="questionnaireInformationCode">設定する内容を表すQuestionnaireInformationCode列挙型の値</param>
            /// <param name="value">設定する値</param>
            public void SetQuestionnaireInformation(QuestionnaireInformationCode questionnaireInformationCode, string value)
            {
                if (!Enum.IsDefined(typeof(QuestionnaireInformationCode), questionnaireInformationCode)) return;
                int index = (int)questionnaireInformationCode;
                if (questionnaireinformation[index] == null) questionnaireinformation[index] = value;
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
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                outputs.Dispose();
                Collection = null;
            }

            /// <summary>
            /// PPテンプレートIDの値を取得/設定するプロパティ
            /// </summary>
            public decimal TemplateId {
                get { return templateId; }
                set { templateId = value; }
            }

            public string DivName { get => divName; set => divName = value; }
        }

        private Request request = null;

        internal Reportsets(Request request) {
            this.request = request;
        }

        /// <summary>
        /// Reportsetクラスのインスタンスを生成してそれへの参照を返す
        /// </summary>
        /// <returns>生成したReportsetクラスのインスタンスへの参照</returns>
        public Reportset Add()
        {
            string key = this.Count.ToString();
            Reportset newReportset = new Reportset(this);
            this.Add(key, newReportset);
            return newReportset;
        }

        public Reportset Add(decimal id, FileType filetype, string templatepath, string filenameprefix, Common.PpSaveAsFileType ppfileformat, bool outputcomment
                , string questionnairepurpose, string questionnairemethod, string questionnaireterm, string questionnaireassignment, string questionnaireorganization)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                Reportset newReportset = new Reportset(this, id, filetype, templatepath, filenameprefix, ppfileformat, outputcomment, questionnairepurpose, questionnairemethod, questionnaireterm, questionnaireassignment, questionnaireorganization);
                this.Add(key, newReportset);
                return newReportset;
            }
            else
            {
                return this[key] as Reportset;
            }
        }

        internal void InsertToDB()
        {
            decimal requestid = request.ID;
            foreach (DictionaryEntry de in this)
            {
                Reportset rs = de.Value as Reportset;
                rs.InsertToDB(requestid);
            }
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="key">
        /// キーとなる文字列<br />
        /// この値は要素の順序を表す0ベースのインデックス番号を文字列化したもの
        /// </param>
        /// <returns>キーが示すコレクションの要素であるReportsetクラスのインスタンスへの参照</returns>
        public IReportset this[string key]
        {
            get 
            {
                return base[key] as Reportset;
            }
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="id">0ベースのインデックス番号</param>
        /// <returns>インデックス番号が示すコレクションの要素であるReportsetクラスのインスタンスへの参照</returns>
        public IReportset this[decimal id]
        {
            get 
            {
                return this[id.ToString()];
            }
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

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (DictionaryEntry de in this)
            {
                Reportset rs = de.Value as Reportset;
                rs.Dispose();
            }
            request = null;
        }
    }
}
