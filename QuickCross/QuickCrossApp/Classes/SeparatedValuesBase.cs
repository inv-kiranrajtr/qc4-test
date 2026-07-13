using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Qc4Launcher.Util.Constants.DataOutput;

namespace Qc4Launcher.Classes
{
	class SeparatedValuesBase
	{
		public string[] GetRawDataBuffer(string ColumnDelimiter
										, string[,] rawdataArray
										, string RowDelimiter = "\r\n"
										, bool EnclosedWithDoubleQuotes = true
										, char EscapeDoubleQuotesLetter = '"',string outputType = "",string[] itemTypeAry =null,string exportType=""
            ,string c1="",string c2="",bool isRawdata=false)
		{

			try
			{
				int columnsCountPerPage = 1; // 1ページの最大カラム数
											 //string[,] rawdataArray = this.Config.RawdataArray;
				bool[] divisiblePoint = null;//this.Config.DivisiblePoint;
				List<DividedIndexes> DividedColumnsList = new List<DividedIndexes>();

				int s = 0;
				int last = rawdataArray.GetUpperBound(1);
				if (columnsCountPerPage < 2 || divisiblePoint == null)
				{
					DividedColumnsList.Add(new DividedIndexes(s, last));
				}
				else
				{
					while (s <= last)
					{
						int nextS = s + columnsCountPerPage + (DividedColumnsList.Count == 0 ? 0 : -1);
						if (nextS <= last)
						{
							for (int i = nextS; i > s; --i)
							{
								if (divisiblePoint[i])
								{
									nextS = i;
									break;
								}
							}
							int e = nextS - 1;
							DividedColumnsList.Add(new DividedIndexes(s, e));
							s = nextS;
						}
						else
						{
							DividedColumnsList.Add(new DividedIndexes(s, last));
							break;
						}
					}
				}
				string[] result = new string[DividedColumnsList.Count];
				for (int i = 0; i < DividedColumnsList.Count; ++i)
				{
					System.Text.StringBuilder tmpBuilder = new System.Text.StringBuilder();
					bool addRowDelimiter = false;
					for (int r = 0; r < rawdataArray.GetLength(0); ++r)//CHNAGED CONDITION
					{
						if (addRowDelimiter) tmpBuilder.Append(RowDelimiter);
						addRowDelimiter = true;
						bool addColumnDelimiter = false;
						if (i > 0)
						{
							addColumnDelimiter = true;
							string tmp = ConvertField(rawdataArray[r, 0], EnclosedWithDoubleQuotes, EscapeDoubleQuotesLetter, ColumnDelimiter,c1:c1,c2:c2);
							tmpBuilder.Append(tmp);
						}
						for (int c = DividedColumnsList[i].StartIndex; c <= DividedColumnsList[i].EndIndex; ++c)
						{
                            if (rawdataArray[r, c] != null)
                            {
                                if (addColumnDelimiter) tmpBuilder.Append(ColumnDelimiter);
                                addColumnDelimiter = true;
                                string ansType = "";
                                if (exportType == "CODE" || isRawdata)
                                        ansType = itemTypeAry != null ? itemTypeAry[c] : "";
                                if (exportType != "CODE" && isRawdata)
                                    ansType = ansType == "FA" ? "FA" : "";

                                string tmp = ConvertField(rawdataArray[r, c], EnclosedWithDoubleQuotes, EscapeDoubleQuotesLetter, ColumnDelimiter, outputType, ansType, c1: c1, c2: c2);
                                tmpBuilder.Append(tmp);
                            }
						}
					}
					//Fix for #281011- Modified the String append operation to omit null or empty string results
					string tmpstring = tmpBuilder.ToString();
					result[i] = string.IsNullOrEmpty(tmpstring) ? string.Empty : tmpstring + RowDelimiter; //ADDED NEW LINE
				}
				return result;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private struct DividedIndexes
		{
			/// <summary>
			/// 開始インデックス
			/// </summary>
			public int StartIndex;

			/// <summary>
			/// 終了インデックス
			/// </summary>
			public int EndIndex;

			/// <summary>
			/// インスタンスを新規に生成します
			/// </summary>
			/// <param name="start">開始インデックス</param>
			/// <param name="end">終了インデックス</param>
			internal DividedIndexes(int start, int end)
			{
				StartIndex = start;
				EndIndex = end;
			}
		}

        /// <summary>
        /// フィールド値の書式変換を行います
        /// </summary>
        /// <param name="field">フィールド値</param>
        /// <param name="EncloseWithDoubleQuot">半角ダブルクォートでセルの値を囲む場合true</param>
        /// <param name="EscapeDoubleQuotLetter">セル内の半角ダブルクォートのエスケープ文字</param>
        /// <remarks>Mantis#0002378(0008281)</remarks>
        private string ConvertField(string field
                                    , bool EncloseWithDoubleQuot
                                    , char EscapeDoubleQuotLetter
                                    , string FiledSeparator, string outputType = "", string ansType = ""
            , string c1 = "", String c2 = "")
        {
             // MANTIS#0001038(0003136) 改行コードは「LF」管理とする

            bool isContainsSeparator = field.Contains(FiledSeparator); // セルにセパレーターが存在する場合はダブルクォートで囲う
            if (isContainsSeparator || ((outputType == "CSV" || outputType == "TAB") && ansType == "MA"))
            {
                if (field.Trim() != "-" && field.Trim() != "0" && field.Trim() != "－" && field.Trim() != "*" && field.Trim() != "")
                {
                    field = field.Replace("\"", EscapeDoubleQuotLetter + "\"");        // セルにダブルクォートが存在する場合はエスケープする
                    field = (EncloseWithDoubleQuot ? "\"" : "")
                             + field
                             + (EncloseWithDoubleQuot ? "\"" : "");
                }
            }
            else if (field.Length > 0 && (outputType == "CSV" || outputType == "TAB") && ansType == "FA")
            {
                if (field.Contains(","))
                    field = "\"" + field + "\"";
            }
            //if (outputType == "CSV" || outputType == "TAB" || outputType == "QLayout")
            //    field = field.Replace(",", "，");
            //if(outputType== "CSV")
            //{
            //    field = field.Replace("\n", " ").Replace("\r", "");
            //}


            if (outputType == "TAB" || outputType == "QLayout")
            {
                field = field == null ? string.Empty : field;
                field = field.Replace("\n", "<LF>").Replace("\r", "");
            }
            else
            {
                field = field == null ? string.Empty : field;
                field = field.Replace("\n", " ").Replace("\r", "");
            }
            return field;
        }

        public void Export(string filename, string contents, string encoding = "Shift-JIS", FileType typ = FileType.NONE)
        {
            try
            {
				Encoding enc = Encoding.GetEncoding(encoding);
                switch (typ)
                {
                    case FileType.CSV:
                    case FileType.TAB:
                    case FileType.QLayout:
                    case FileType.R2D3:

						if (encoding == "UTF-8")
							enc = new UTF8Encoding(false);

						using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, true, enc))
                        {
                            sw.Write(contents);
                            sw.Close();
                        }
                        break;
                    case FileType.NONE:
                    case FileType.SPSS:
                        string actualContent = "\r\n" + contents;
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, true, enc))
                        {
                            sw.Write(actualContent.TrimEnd('\t').TrimEnd('\n').TrimEnd('\r').TrimEnd(' '));
                            sw.Close();
                        }
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
	}
}
