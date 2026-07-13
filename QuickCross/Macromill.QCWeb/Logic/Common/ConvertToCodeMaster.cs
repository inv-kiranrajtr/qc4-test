#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Common.cs
 * バージョン：1.0.0
 * 概　　　要：汎用コードマスター用変換クラス
 * 作　成　日：2012/7/3
 * 作　成　者：窪田知弘
 * 更　新　日：
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Common;
using System.Runtime.InteropServices;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.AllCommon;
using Seasar.Quill;

namespace Macromill.QCWeb.Logic.Common {

    /// <summary>
    /// 汎用コードマスター用変換クラス
    /// </summary>
    [ComVisible(false), Guid("E71506F3-6963-4fb2-A4AE-9B16FB1D765C")]
    public class ConvertToCodeMaster {

        /// <summary>デフォルト環境設定TBL</summary>
        protected TDefaultEnvBaseBhv tDefaultEnvBaseBhv = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConvertToCodeMaster() {
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// グラフ種別を取得
        /// </summary>
        /// <param name="qType">質問タイプ</param>
        /// <param name="answerType">回答タイプ</param>
        /// <param name="language">言語(既定値:ja)</param>
        /// <returns>グラフ種別</returns>
        public string GetGraphType(string qType, string answerType, string language = "ja") {
            // デフォルト環境設定情報の検索
            TDefaultEnvBase entity = tDefaultEnvBaseBhv.SelectByPKValueWithDeletedCheck(language);
            return GetGraphType(qType, answerType, entity);
        }

        /// <summary>
        /// グラフ種別を取得
        /// </summary>
        /// <param name="qType">質問タイプ</param>
        /// <param name="answerType">回答タイプ</param>
        /// <param name="entity">デフォルト環境設定Entity</param>
        /// <returns>グラフ種別</returns>
        public string GetGraphType(string qType, string answerType, TDefaultEnvBase entity) {
            if ("SAR" == qType)
                return GlobalsCommonConstant.GRAPH_TYPE_QCCIRCLE;          // QC円グラフ
            if ("MAC" == qType)
                return GlobalsCommonConstant.GRAPH_TYPE_QCWIDTHSTICK;      // QC横棒グラフ
            if ("SAP" == qType)
                return GlobalsCommonConstant.GRAPH_TYPE_QCCIRCLE;          // QC円グラフ
            if ("SAS" == qType)
                return GlobalsCommonConstant.GRAPH_TYPE_QCCIRCLE;          // QC円グラフ
            if ("MTS" == qType)
                return GlobalsCommonConstant.GRAPH_TYPE_QCWIDTHBELT;       // QC横帯グラフ
            if ("MTM" == qType)
                return GlobalsCommonConstant.GRAPH_TYPE_QCMWIDTHSTICK;     // QCM横棒グラフ
            if ("MTT" == qType)
                return GlobalsCommonConstant.GRAPH_TYPE_QCWIDTHBELT;       // QC横帯グラフ
            if ("RAT" == qType)
                return GlobalsCommonConstant.GRAPH_TYPE_QCMCIRCLERAT;      // QCM円RATグラフ
            if ("RNK" == qType)
                return GlobalsCommonConstant.GRAPH_TYPE_QCWIDTHONSTICK;    // QC積上横棒グラフ

            if (answerType != null) {
                if (answerType.ToLower() == CDef.AnswerType.SA.Code) {
                    return entity.GraphTypeSa;
                } else if (answerType.ToLower() == CDef.AnswerType.MA.Code) {
                    return entity.GraphTypeMaSimple;
                }
            }

            return null;
        }
    }
}
