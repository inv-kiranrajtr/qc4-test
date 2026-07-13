#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：IAIRsCommunication.cs
 * バージョン：1.0.0
 * 概　　　要：AIRs連携を管理するインターフェース
 * 作　成　日：2012/6/16
 * 作　成　者：寺嶋　千晴
 * $Id: IAirsCommunication.cs 11166 2013-12-19 06:52:02Z kousaka $ / $Date: 2013-12-19 15:52:02 +0900 (2013/12/19 (木)) $ / $Rev: 11166 $ / $Author: kousaka $
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seasar.Quill.Attrs;

namespace Macromill.QCWeb.Common.OtherSystems {

    /// <summary>
    /// AIRs連携実装クラス
    /// </summary>
    [Implementation(typeof(AirsCommunication))]
    [Mock(typeof(MockAirsCommunication))]
    [System.Runtime.InteropServices.ComVisible(false)]
    public interface IAirsCommunication {
        /// <summary>
        /// AIRs連携APIの実行
        /// </summary>
        /// <param name="bean">AIRs連携WebAPIへの引数を格納したBean</param>
        /// <param name="loglevel">ログレベル</param>
        /// <returns>AIRs連携WebAPIからの連携結果を格納したBean</returns>
        AirsResultBean ExecAIRsAPI(AirsParameterBean bean,GlobalsCommonConstant.LogLevel loglevel = GlobalsCommonConstant.LogLevel.FATAL);
    }
}
