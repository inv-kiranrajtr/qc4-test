#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：IPhysicalTableController.cs
 * バージョン：0.1.0
 * 概　　　要：物理テーブル情報 管理クラス
 * 作　成　日：2012/04/19
 * 作　成　者：小松 正明
 * $Id$ / $Date$ / $Rev$ / $Author$
  ***************************************************************/
#endregion
using Seasar.Quill.Attrs;
using System.Collections.Generic;


namespace Macromill.QCWeb.Logic.Common {
    /// <summary>
    /// ローデータ共通管理インターフェース
    /// </summary>
    [Implementation(typeof(PhysicalTableControllerImpl)), System.Runtime.InteropServices.ComVisible(false)]
    public interface IPhysicalTableController {
        /// <summary>
        /// 利用可能な物理テーブル情報を取得する
        /// </summary>
        /// <param name="_qcwebId">QCWeb管理ID</param>
        /// <param name="_answerType">回答タイプ</param>
        /// <returns>物理テーブル情報</returns>
        RawDataPhysicalTableInfo GetAvailableTableColumn(decimal _qcwebId, int _answerType);

        /// <summary>
        /// 列削除時に呼ばれる処理
        /// </summary>
        /// <param name="_qcwebId">QCWeb管理ID</param>
        /// <param name="_tableNo">削除対象アイテムのテーブルNo</param>
        /// <param name="_fieldNo">削除対象アイテムのカラムNo</param>
        /// <returns></returns>
        void DeleteTableColumn(decimal _qcwebId, int _tableNo, int _fieldNo);
    }
}
