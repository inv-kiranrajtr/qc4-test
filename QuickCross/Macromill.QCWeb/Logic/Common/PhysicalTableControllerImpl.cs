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
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.ExDao.PmBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.ExEntity.Customize;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Question;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Logic.Common {
    /// <summary>
    /// ローデータ共通管理実装クラス
    /// </summary>
    [ComVisible(false), Guid("8A1C8650-5A37-41d9-9157-5312FFAC80CA")]
    public class PhysicalTableControllerImpl : IPhysicalTableController {
        #region ビヘイビア
        /// <summary>テーブルコントロールTBLBhv</summary>
        protected TTableControlBhv tTableControlBhv = null;
        /// <summary>テーブルコントロール詳細TBLBhv</summary>
        protected TTableDetailInfoBhv tTableDetailInfoBhv = null;
        /// <summary>アイテム情報TBLBhv</summary>
        protected TItemInfoBhv tItemInfoBhv = null;
        /// <summary>本調査管理TBLBhv</summary>
        protected TSurveyDataBhv tSurveyDataBhv = null;
        #endregion

        private QCAnswerType ConvertToDefinedQCAnswerType(int _answerType)
        {
            if (!System.Enum.IsDefined(typeof(QCAnswerType), _answerType))
            {
                // throw new QCWebException("回答タイプに誤りがあります。：" + _answerType);
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustQCAnswerTypeValueMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, _answerType.ToString());
            }
            return (QCAnswerType)_answerType;
        }

        private delegate RawDataPhysicalTableInfo GetAvailableTableColumnDelegate(decimal _qcwebid, int _answerType);

        /// <summary>
        /// 利用可能な列のテーブル情報を返す
        /// </summary>
        /// <param name="_qcwebId">QCWeb管理ID</param>
        /// <param name="_answerType">回答タイプ</param>
        /// <returns></returns>
        public RawDataPhysicalTableInfo GetAvailableTableColumn(decimal _qcwebId, int _answerType) 
        {
            QCAnswerType aType = ConvertToDefinedQCAnswerType(_answerType);
            GetAvailableTableColumnDelegate getAvailableTableColumnMethod
                    = aType == QCAnswerType.FA ? 
                        (GetAvailableTableColumnDelegate)GetAvailableTableColumnFA : 
                        (GetAvailableTableColumnDelegate)GetAvailableTableColumnNormal;
            return getAvailableTableColumnMethod(_qcwebId, _answerType);

            /*
            RawDataPhysicalTableInfo returnBean;
            switch ((QCAnswerType)_answerType) {
                case QCAnswerType.FA:
                    returnBean = GetAvailableTableColumnFA(_qcwebId, _answerType);
                    break;
                case QCAnswerType.SA:
                case QCAnswerType.MA:
                case QCAnswerType.N:
                case QCAnswerType.D:
                    returnBean = GetAvailableTableColumnNormal(_qcwebId, _answerType);
                    break;
                default:
                    // TODO:メッセージIDに変更
                    throw new QCWebException("回答タイプに誤りがあります。：" + _answerType);
            }
            return returnBean;
            */
        }

        /// <summary>
        /// 列データ削除処理
        /// </summary>
        /// <param name="_qcwebId">QCWeb管理ID</param>
        /// <param name="_tableNo">テーブル番号</param>
        /// <param name="_fieldNo">カラム番号：（今の所未使用）</param>
        /// <returns>true</returns>
        public void DeleteTableColumn(decimal _qcwebId, int _tableNo, int _fieldNo) {
            int tableNo = IsOnlySampleID(_qcwebId, _tableNo);
            DecrementUsedColumn(_qcwebId, tableNo);
        }


        /// <summary>
        /// 指定されたテーブルの最大利用数をデクリメントする
        /// </summary>
        /// <param name="_qcwebId">QCWeb管理ID</param>
        /// <param name="_tableNo">テーブル番号</param>
        private void DecrementUsedColumn(decimal _qcwebId, int _tableNo) {
            TTableDetailInfoCB cb = new TTableDetailInfoCB();
            cb.Query().SetQcwebid_Equal(_qcwebId);
            cb.Query().SetTableNo_Equal(_tableNo);
            TTableDetailInfo bean = tTableDetailInfoBhv.SelectEntity(cb);
            bean.UsedNo--;
            tTableDetailInfoBhv.Update(bean);
        }


        /// <summary>
        /// デクリメント対象のテーブル番号を返し、アクティブテーブルを変更する
        /// </summary>
        /// <param name="_qcwebId">QCWeb管理ID</param>
        /// <param name="_tableNo">現在のアクティブテーブル番号</param>
        /// <returns>削除対象のテーブル番号</returns>
        private int IsOnlySampleID(decimal _qcwebId, int _tableNo) {
            TTableDetailInfoCB cb = new TTableDetailInfoCB();
            cb.Query().SetQcwebid_Equal(_qcwebId);
            cb.Query().SetTableNo_Equal(_tableNo);
            TTableDetailInfo tableInfo = tTableDetailInfoBhv.SelectEntity(cb);
            if (tableInfo.UsedNo == 1) {   //1つ前のテーブル番号を検索する
                tableInfo.UsedNo--;
                tTableDetailInfoBhv.Update(tableInfo);
                int activeTableNo = GetBeforeTable(_qcwebId);
                UpdateTableInfo(_qcwebId, activeTableNo);
                return activeTableNo;
            } else {
                return _tableNo;
            }
        }

        /// <summary>
        /// 最大列数まで使っているテーブルのテーブル番号の最大値を返す
        /// </summary>
        /// <param name="_qcwebID">QCWeb管理</param>
        /// <returns>最大テーブル番号</returns>
        private int GetBeforeTable(decimal _qcwebID) {
            TTableDetailInfoCB cb = new TTableDetailInfoCB();
            cb.Query().SetQcwebid_Equal(_qcwebID);
            cb.Query().SetUsedNo_Equal(GlobalsCommonConstant.MAX_COLUMN_COUNT);
            cb.Query().AddOrderBy_TableNo_Desc();
            ListResultBean<TTableDetailInfo> beanList = tTableDetailInfoBhv.SelectList(cb);
            return (int)beanList[0].TableNo;
        }

        /// <summary>
        /// 利用可能なテーブル情報を取得する
        /// </summary>
        /// <param name="_qcwebId">QCWeb管理ID</param>
        /// <param name="_answerType">回答タイプ</param>
        /// <returns>テーブル情報</returns>
        private RawDataPhysicalTableInfo GetAvailableTableColumnNormal(decimal _qcwebId, int _answerType)
        {
            //RawDataPhysicalTableInfo returnBean = new RawDataPhysicalTableInfo();
            // テーブル情報を取得する
            TTableControlCB conditionBean = new TTableControlCB();
            conditionBean.Query().SetQcwebid_Equal(_qcwebId);
            TTableControl tableInfo = tTableControlBhv.SelectEntity(conditionBean);
            int tableNo = (int)tableInfo.ActiveTableNo;     // ActiveTable番号
            int maxNo = (int)tableInfo.MaxNo;

            // 空き番検索：外だしSQL
            string sqlPath = TItemInfoBhv.PATH_AvailableNo;     // 外だしSQLへのPath
            // 検索条件
            TAvailableTableInfo pmb = new TAvailableTableInfo();
            pmb.QCWebID = _qcwebId;
            pmb.TableNo = tableNo;
            // SQL実行
            ListResultBean<TAvailableNoEntity> lists = tItemInfoBhv.OutsideSql().SelectList<TAvailableNoEntity>(sqlPath, pmb);
            int colNo;
            if (lists.SelectedList[0].AvailableNo == null) 
            {   //該当データが無い→新規テーブルの1つ目
                colNo = 1;
            } else {
                colNo = (int)lists.SelectedList[0].AvailableNo; // 利用可能なカラム番号
            }
            // 利用可能テーブル情報を生成
            RawDataPhysicalTableInfo retBean = CreateTableInfo(_qcwebId, _answerType, tableNo, colNo);

            // テーブル管理更新処理
            int nextTableNo;
            if (UpdateTableDetailInfo(_qcwebId, tableNo) == GlobalsCommonConstant.MAX_COLUMN_COUNT) 
            {
                nextTableNo = GetActiveTableNo(_qcwebId, maxNo, tableNo);
                if (nextTableNo == -1) 
                {
                    // TODO:メッセージIDに変更
                    // throw new QCWebException("空きテーブルがありません。QCWeb管理ID：" + _qcwebId.ToString());
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.NotExistEmptyTableMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL, _qcwebId.ToString());
                }
                InitNewTable(_qcwebId, nextTableNo, _answerType);
            } 
            else
            {
                nextTableNo = tableNo;
            }
            UpdateTableInfo(_qcwebId, nextTableNo);

            return retBean;
        }

        /// <summary>
        /// FA型のテーブル情報を取得する
        /// </summary>
        /// <param name="_qcwebId">QCWeb管理ID</param>
        /// <param name="_answerType">回答タイプ</param>
        /// <returns>テーブル情報</returns>
        private RawDataPhysicalTableInfo GetAvailableTableColumnFA(decimal _qcwebId, int _answerType) {
            RawDataPhysicalTableInfo returnBean = new RawDataPhysicalTableInfo();

            // 空き番検索：外だしSQL
            string sqlPath = TItemInfoBhv.PATH_AvailableNo;     // 外だしSQLへのPath
            // 検索条件
            TAvailableTableInfo pmb = new TAvailableTableInfo();
            pmb.QCWebID = _qcwebId;
            pmb.TableNo = GlobalsCommonConstant.FA_TABLE_NO;          // -1固定
            // SQL実行
            ListResultBean<TAvailableNoEntity> lists = tItemInfoBhv.OutsideSql().SelectList<TAvailableNoEntity>(sqlPath, pmb);
            int colNo = (int)(lists.SelectedList[0].AvailableNo ?? 1); // 利用可能なカラム番号

            // FAの上限チェックを入れるか？

            // 利用可能テーブル情報を生成
            RawDataPhysicalTableInfo retBean = CreateTableInfo(_qcwebId, _answerType, GlobalsCommonConstant.FA_TABLE_NO, colNo);

            return returnBean;
        }

        /// <summary>
        /// テーブル詳細情報の初期化処理（不要なら実行されない）
        /// </summary>
        /// <param name="_qcwebId">QCWeb調査管理ID</param>
        /// <param name="_tableNo">テーブル番号</param>
        /// <param name="_answerType">質問タイプ</param>
        private void InitNewTable(decimal _qcwebId, int _tableNo, int _answerType) {
            TTableDetailInfoCB cb = new TTableDetailInfoCB();
            cb.Query().SetQcwebid_Equal(_qcwebId);
            cb.Query().SetTableNo_Equal(_tableNo);
            TTableDetailInfo entity = tTableDetailInfoBhv.SelectEntity(cb);
            if (entity.UsedNo == 0) {
                CopySampleID(_qcwebId, _answerType, _tableNo);
            }
        }

        /// <summary>
        /// 1番テーブルのサンプルIDをコピーする
        /// </summary>
        /// <param name="_qcwebId">QCWeb調査管理ID</param>
        /// <param name="_answerType">質問タイプ</param>
        /// <param name="_tableno">テーブル番号</param>
        private void CopySampleID(decimal _qcwebId, int _answerType, int _tableno) {
            string sqlPath = TSurveyDataBhv.PATH_InitRawDataTable;     // 外だしSQLへのPath
            TRawDataCopyPmb pmb = new TRawDataCopyPmb();
            pmb.FromTable = GetTableName(_qcwebId, _answerType, 1);
            pmb.ToTable = GetTableName(_qcwebId, _answerType, _tableno);
            tSurveyDataBhv.OutsideSql().Execute(sqlPath, pmb);
        }

        /// <summary>
        /// テーブル詳細情報を更新する
        /// </summary>
        /// <param name="_qcwebId">QCWeb管理ID</param>
        /// <param name="_tableNo">テーブル番号</param>
        /// <returns>使用済みカラム数</returns>
        private int UpdateTableDetailInfo(decimal _qcwebId, int _tableNo) {
            TTableDetailInfoCB cb = new TTableDetailInfoCB();
            cb.Query().SetQcwebid_Equal(_qcwebId);
            cb.Query().SetTableNo_Equal(_tableNo);
            TTableDetailInfo entity = tTableDetailInfoBhv.SelectEntity(cb);
            entity.UsedNo++;
            tTableDetailInfoBhv.Update(entity);
            return (int)entity.UsedNo;
        }

        /// <summary>
        /// テーブル管理情報を更新する
        /// </summary>
        /// <param name="_qcwebId"></param>
        /// <param name="_nextTableNo"></param>
        private void UpdateTableInfo(decimal _qcwebId, int _nextTableNo) {
            TTableControl entity = new TTableControl();
            entity.Qcwebid = _qcwebId;
            entity.ActiveTableNo = _nextTableNo;
            tTableControlBhv.InsertOrUpdate(entity);
        }
        /// <summary>
        /// 利用可能なテーブル番号を返す
        /// </summary>
        /// <param name="_qcwebid">QCWeb管理ID</param>
        /// <param name="_max_no"></param>
        /// <param name="_active_no"></param>
        /// <returns></returns>
        private int GetActiveTableNo(decimal _qcwebid, int _max_no, int _active_no) {
            TTableDetailInfoCB conditionBean = new TTableDetailInfoCB();
            conditionBean.Query().SetQcwebid_Equal(_qcwebid);
            conditionBean.Query().SetTableNo_NotEqual(_active_no);
            conditionBean.Query().SetUsedNo_NotEqual(GlobalsCommonConstant.MAX_COLUMN_COUNT);
            conditionBean.Query().SetTableNo_GreaterThan(0);
            conditionBean.Query().AddOrderBy_TableNo_Asc();
            ListResultBean<TTableDetailInfo> resultlist = tTableDetailInfoBhv.SelectList(conditionBean);
            if (resultlist == null || resultlist.Count == 0) {
                return -1;      // ERROR
            } else {
                return (int)resultlist[0].TableNo;
            }
        }
        /// <summary>
        /// テーブル情報を生成する
        /// </summary>
        /// <param name="_qcwebid"></param>
        /// <param name="_answerType"></param>
        /// <param name="_tableNo"></param>
        /// <param name="_fieldNo"></param>
        /// <returns></returns>
        private RawDataPhysicalTableInfo CreateTableInfo(decimal _qcwebid, int _answerType, int _tableNo, int _fieldNo) {
            RawDataPhysicalTableInfo tableInfoBean = new RawDataPhysicalTableInfo();
            tableInfoBean.TableName = GetTableName(_qcwebid, _answerType, _tableNo);
            tableInfoBean.FieldName = getFieldName(_answerType, _fieldNo);
            tableInfoBean.TableNo = _tableNo;
            tableInfoBean.FieldNo = _fieldNo;
            return tableInfoBean;
        }

        #region テーブル名/フィールド名の生成部
        /// <summary>
        /// RawDataテーブル名を生成し返す
        /// </summary>
        /// <param name="_qcwebid">QCWeb管理ID</param>
        /// <param name="_answerType">回答タイプ</param>
        /// <param name="_tableNo">テーブル番号</param>
        /// <returns>RawDataテーブル名</returns>
        private string GetTableName(decimal _qcwebid, int _answerType, int _tableNo) 
        {
            QCAnswerType aType = ConvertToDefinedQCAnswerType(_answerType);
            string suffix = aType == QCAnswerType.FA ? GlobalsCommonConstant.TABLE_SUFFIX_FA : GlobalsCommonConstant.TABLE_SUFFIX + _tableNo.ToString("00");
            /*
            string suffix;
            switch ((QCAnswerType)_answerType) {
                case QCAnswerType.FA:            // "_FA"
                    suffix = GlobalsCommonConstant.TABLE_SUFFIX_FA;
                    break;
                case QCAnswerType.SA:
                case QCAnswerType.MA:
                case QCAnswerType.N:
                case QCAnswerType.D:
                    suffix = GlobalsCommonConstant.TABLE_SUFFIX + _tableNo.ToString("00");
                    break;
                default:
                    // TODO:メッセージIDに変更
                    throw new QCWebException("回答タイプに誤りがあります。:" + _answerType);
            }
            */
            // "T_" ＋ (0詰め18桁のQCWeb管理ID) ＋ サフィックス
            return (GlobalsCommonConstant.TABLE_PREFIX + _qcwebid.ToString(new string('0', 18)) + suffix);
        }
        /// <summary>
        /// RawDataフィールド名を返す
        /// </summary>
        /// <param name="_answerType"></param>
        /// <param name="_fieldNo"></param>
        /// <returns></returns>
        private string getFieldName(int _answerType, int _fieldNo) 
        {
            QCAnswerType aType = ConvertToDefinedQCAnswerType(_answerType);
            string prefix = aType == QCAnswerType.FA ? GlobalsCommonConstant.FIELD_PREFIX_FA : GlobalsCommonConstant.FIELD_PREFIX;
            /*
            string prefix;
            switch ((QCAnswerType)_answerType) {
                case QCAnswerType.FA:
                    prefix = GlobalsCommonConstant.FIELD_PREFIX_FA;   // "F"
                    break;
                case QCAnswerType.SA:
                case QCAnswerType.MA:
                case QCAnswerType.N:
                case QCAnswerType.D:
                    prefix = GlobalsCommonConstant.FIELD_PREFIX;      // "C"
                    break;
                default:
                    // TODO:メッセージIDに変更
                    throw new QCWebException("回答タイプに誤りがあります。:" + _answerType);
            }
            */
            // プレフィックス ＋ (0詰め3桁のフィールド番号)
            return prefix + _fieldNo.ToString(new string('0', 3));
        }
        #endregion
    }
}
