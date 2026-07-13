#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：IReportset.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/3/19
 * 作　成　者：井川はるき
 * 更　新　日：2012/3/29
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Runtime.Serialization;

namespace Macromill.QCWeb.ReportRequest
{
    /// <summary>
    /// 出力ファイルの種類を表すコード
    /// </summary>
    [Flags, ComVisible(true)]
    public enum FileType : int
    {
        /// <summary>
        /// Excelブックを表す (= 1)
        /// </summary>
        Excel = 1,
        /// <summary>
        /// PowerPointプレゼンテーションを表す (= 2)
        /// </summary>
        PowerPoint = 2,
        /// <summary>
        /// PDFファイルを表す (= 4)
        /// </summary>
        PDF = 4,
        /// <summary>
        /// レポート出力を表す (= 16)<br />
        /// 単独で指定することはなく、他の値との組み合わせで使用する
        /// </summary>
        Report = 16
    }

    /// <summary>
    /// レポートセットコレクションインターフェイス
    /// </summary>
    [ComVisible(true), Guid("217255BC-E551-474F-AAEF-B06E17305E58")]
    public interface IReportsets : IDisposable, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
    {
        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="key">キーとなる文字列</param>
        /// <returns>キーが示すコレクションの要素であるReportsetクラス(IReportsetインターフェイスの実装クラス)のインスタンスへの参照</returns>
        IReportset this[string key] { get; }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="id">レポートセットID</param>
        /// <returns>レポートセットIDが示すコレクションの要素であるReportsetクラス(IReportsetインターフェイスの実装クラス)のインスタンスへの参照</returns>
        [ComVisible(false)]
        IReportset this[decimal id] { get; }

        /// <summary>
        /// コレクションの要素数を返す読み取り専用プロパティ
        /// <note>ICollectionでCountが定義されているが、COM連携のために、ここで明示的に定義</note>
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// 自身のインスタンスの親であるRequestクラス(IRequestインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IRequest ParentRequest { get; }
    }

    /// <summary>
    /// レポートセットインターフェイス
    /// </summary>
    [ComVisible(true), Guid("91DA571A-F8F6-4166-BEC7-657303C62320")]
    public interface IReportset : IDisposable
    {
        /// <summary>
        /// レポートセットに紐づくOutputsコレクションクラス(IOutputsインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IOutputs Outputs { get; }

        /// <summary>
        /// レポートセットIDを返す読み取り専用プロパティ
        /// </summary>
        [ComVisible(false)]
        decimal ID { get; }

        /// <summary>
        /// 出力ファイルの種類を表すFileType列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        FileType FileType { get; }

        /// <summary>
        /// 使用するPPテンプレートのパスを返す読み取り専用プロパティ<br />
        /// PowerPoint、PDFのいずれか、あるいは両方を出力する場合にのみ有効
        /// </summary>
        string TemplatePath { get; }

        /// <summary>
        /// 出力物のファイル名のプリフィックスを返す読み取り専用プロパティ
        /// </summary>
        string FileNamePrefix { get; }

        /// <summary>
        /// 出力物のPowerPointの保存ファイル形式を返す読み取り専用プロパティ
        /// </summary>
        Common.PpSaveAsFileType PowerPointFileType { get; }

        /// <summary>
        /// コメントを出力するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool OutputComment { get; }

        /// <summary>
        /// 調査票情報を保持した文字列型一次元配列への参照を返す読み取り専用プロパティ<br />
        /// 調査票の出力時のみ有効
        /// </summary>
        string[] QuestionnaireInformation { get; }

        /// <summary>
        /// 自身のインスタンスが格納されているReportsetsコレクションクラス(IReportsetsインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IReportsets ParentCollection { get; }

        /// <summary>
        /// 自身のインスタンスの親であるRequestクラス(Requestインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IRequest ParentRequest { get; }

        ///// <summary>
        ///// PPテンプレートIDの値を返す読み取り専用プロパティ
        ///// </summary>
        //decimal TemplateId { get; }

        string DivName { get; }
    }
}
