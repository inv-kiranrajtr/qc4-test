#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：ApplicationConfig.cs
 * バージョン：1.0.0
 * 概　　　要：アプリケーション環境設定クラス
 * 作　成　日：2012/03/28
 * 作　成　者：寺嶋 千晴
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seasar.Quill.Attrs;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Exceptions;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace Macromill.QCWeb.Common 
{
    /// <summary>
    /// アプリケーション環境設定クラス
    /// </summary>
    [ComVisible(false), Guid("0560481D-0F15-466D-B8C8-0BA1FAE6401D"), Implementation]
    public class ApplicationConfig 
    {
        /// <summary>アプリ環境設定情報格納</summary>
        private Dictionary<string, string> map;
        /// <summary>アプリ環境設定情報アクセス</summary>
        protected TApplicationBhv tApplicationBhv;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Init() 
        {
            ReadAppConfig();
        }

        /// <summary>
        /// 指定したキーワードの情報を取得する
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public string GetValue(string keyword) 
        {
            ReadAppConfigExcel();
            if (keyword == null)
            {
                // throw new ArgumentNullException(GetResource.GetMessage(Constants.REFER_NULL_PARAMETER_MESSAGE_ID, "keyword"));
                throw new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "keyword");
            }
            string key = keyword.ToLower();
            if (!map.ContainsKey(key))
            {
                // throw new KeyNotFoundException(GetResource.GetMessage(Constants.NOT_EXIST_APPLICATION_ENVIRONMENT_INFORMATION_WITH_KEY_MESSAGE_ID, keyword));
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NotExistApplicationEnvironmentInformationWithKeyMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , keyword);
            }
            return map[key];
            /*
            string val = null;
            try {
                val = map[keyword.ToLower()].ToString();
            } catch (Exception ex) {
                // TODO:適切なExceptionを
                throw new KeyNotFoundException("アプリ環境設定情報取得でエラーが発生しました：" + ex.ToString());
            }
            return val;
            */
        }

        /// <summary>
        /// 指定したキーワードの情報を取得する
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="sep"></param>
        /// <returns></returns>
        public string[] GetValue(string keyword, char sep) 
        {
            /*
            ReadAppConfig();
            string val = null;
            try {
                val = map[keyword.ToLower()].ToString();
            } catch (Exception ex) {
                // TODO:適切なExceptionを
                throw new KeyNotFoundException("アプリ環境設定情報取得でエラーが発生しました：" + ex.ToString());
            }
            */
            string val = GetValue(keyword);
            return val.Split(sep);
        }

        /// <summary>
        /// アプリ環境設定TBLの読み込み
        /// </summary>
        private void ReadAppConfig() {
            if (map == null || map.Count <= 0) 
            {
                TApplicationCB tApplicationCB = new TApplicationCB();
                tApplicationCB.Specify().ColumnIdentifier();
                tApplicationCB.Specify().ColumnSettingValue();
                ListResultBean<TApplication> list = tApplicationBhv.SelectList(tApplicationCB);

                map = new Dictionary<string, string>();
                foreach (TApplication entity in list) 
                {
                    map[entity.Identifier.ToLower()] = entity.SettingValue;
                }
            }
        }

        private void ReadAppConfigExcel()
        {
            map = new Dictionary<string, string>();
            if (map == null || map.Count <= 0)
            {
                map = new Dictionary<string, string>();
                Process currentProcess = Process.GetCurrentProcess();                
                map[GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP.ToLower()] = Path.Combine(Path.GetTempPath(), "QC4", currentProcess.Id.ToString());
            }
        }
    }
}
