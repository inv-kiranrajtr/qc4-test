#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：ConfigReader.cs
 * バージョン：1.0.0
 * 概　　　要：Reportバッチ用設定ファイルの読み込み
 * 作　成　日：2012/03/22
 * 作　成　者：寺嶋　千晴
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Batch.Report {
    /// <summary>
    /// 設定ファイルの読み込み
    /// </summary>
    [ComVisible(false), Guid("F3731350-D440-44AD-A077-7F3E3C55AAB9")]
    public class ConfigReader {
        private static Dictionary<String, String> keyToVal;

        /// <summary>
        /// 指定したKeyの情報を設定ファイルから取得する
        /// </summary>
        /// <param name="key">キーワード</param>
        /// <returns>値</returns>
        public static String GetValue(String key) {
            if (keyToVal == null) {
                keyToVal = new Dictionary<String, String>();
                try {
                    ReadConfig();
                } catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    System.Diagnostics.Debug.Indent();
                    System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                    System.Diagnostics.Debug.Unindent();
                    throw;
                }
            }
            return keyToVal[key.ToLower()].ToString();
        }

#if FOR_UNIT_TEST
        public
#else
        private
#endif
        static void ReadConfig() {
            // モジュールと同じ場所
            String filePath = System.IO.Path.Combine(
                                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                                , "QCWebReport.conf");
            StreamReader sr = null;
            try {
                sr = new StreamReader(filePath, Encoding.UTF8);
            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                System.Diagnostics.Debug.Indent();
                System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                System.Diagnostics.Debug.Unindent();
                throw;
            }
            using (sr) {
                try {
                    String line = null;
                    while ((line = sr.ReadLine()) != null) {
                        if (line.StartsWith("#")) {
                            continue;
                        }
                        // やっつけた・・
                        char[] splits = { '=' };
                        string[] valArray = line.Split(splits, 2);

                        if (valArray.Length != 2) {
                            continue;
                        }

                        // 取得したデータをハッシュに登録する
                        keyToVal[valArray[0].Trim().ToLower()] = valArray[1].Trim();
                    }
                } catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    System.Diagnostics.Debug.Indent();
                    System.Diagnostics.Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    System.Diagnostics.Debug.WriteLine("Description:{0}", e.Message);
                    System.Diagnostics.Debug.Unindent();
                    throw;
                }
            }
        }
    }
}
