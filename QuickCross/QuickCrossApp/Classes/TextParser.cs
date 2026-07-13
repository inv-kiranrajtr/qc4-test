using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace Qc4Launcher.Classes
{
    public static class TextParser
    {
        public static DataTable ExecuteParse(string filePath, string colDelimitter, Encoding encoding, char? enclosingChar)
        {
            var dt = new DataTable();
            Util.FileUtil fileUtil = new Util.FileUtil();
            try
            {
                string splitValue = colDelimitter;
                if (enclosingChar != null)
                {
                    splitValue = enclosingChar.Value.ToString() + splitValue;
                }

                string line;
                int i = 1;
                var splitArray = new[] { splitValue };
                using (var reader = new StreamReader(@filePath, encoding))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] columnValues = line.Split(splitArray, StringSplitOptions.None);
                        if (enclosingChar != null)
                        {
                            StandardizeColumnValues(ref columnValues, enclosingChar.Value.ToString());
                        }

                        if (i == 1) // Header Row
                        {
                            foreach (string columnValue in columnValues)
                            {
                                dt.Columns.Add(fileUtil.GetNewColumnName(dt, columnValue));
                            }
                        }
                        else
                        {
                            DataRow drow = dt.NewRow();
                            for (int j = 0; j <= columnValues.Length - 1; j++)
                            {
                                if (j + 1 > dt.Columns.Count) // Column not exist in table
                                {
                                    dt.Columns.Add(fileUtil.GetNewColumnName(dt));
                                }
                                drow[j] = columnValues[j];
                            }
                            dt.Rows.Add(drow);
                        }
                        i++;
                    }
                }

            }
            catch
            {
                dt = new DataTable();
            }

            return dt;
        }

        private static string[] StandardizeColumnValues(ref string[] columnValues, string firstElement)
        {
            for (int i = 0; i <= columnValues.Length - 1; i++)
            {
                if (columnValues[i] != null)
                {
                    columnValues[i] = columnValues[i].Trim();
                    if (columnValues[i].Length > 0 && columnValues[i].ElementAt(0).ToString() == firstElement) // Remove 1st quote
                    {
                        columnValues[i] = columnValues[i].Remove(0, 1);
                    }

                    if (columnValues[i].Length > 0 && columnValues[i].ElementAt(columnValues[i].LastIndexOf(firstElement)).ToString() == firstElement) // Remove Last Quote
                    {
                        columnValues[i] = columnValues[i].Remove(columnValues[i].LastIndexOf(firstElement), 1);
                    }
                }
            }
            return columnValues;
        }

        public static TextFieldParser ParseFile(String filePath, Encoding encode, String deLimiter)
        {
            TextFieldParser parser = new TextFieldParser(filePath, encode);
            parser.TrimWhiteSpace = false;
            try
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(deLimiter);
                return parser;
            }
            catch (Exception ex)
            {
                parser.Close();
            }
            return parser;
        }

        public static List<String[]> SplitBy(TextFieldParser parser, char splitBy)
        {
            List<String[]> stringData = new List<string[]>();
            try
            {
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        try
                        {
                            string[] csvValues = field.Split(splitBy);
                            stringData.Add(csvValues);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                parser.Close();
            }
            return stringData;
        }
        public static List<String[]> ReadFile(String filePath, Encoding encode, String deLimiter)
        {
            List<String[]> stringData = new List<string[]>();
            char deLimit = Convert.ToChar(deLimiter); 
            using (TextFieldParser parser = new TextFieldParser(filePath, encode))
            {
                parser.TrimWhiteSpace = false;
              //  parser.Delimiters = new[] { deLimiter };
                try
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = null;
                        try
                        {
                            fields = parser.ReadLine().Split(deLimit);
                        }
                        catch (MalformedLineException ex)
                        {
                            if (parser.ErrorLine.Contains("\""))
                            {
                                int j = 1;
                                string[] line;
                                string fullLine = parser.ErrorLine;
                                if (parser.ErrorLine.Contains("\r\n"))
                                    fullLine = parser.ErrorLine.Replace("\r\n", "\n");
                                line = fullLine.Split('\n');
                                fields = line[0].Split(new string[] { deLimiter }, StringSplitOptions.None);
                                //fields = fields[0].Split(',');
                                while (line.Count() > j)
                                {
                                    stringData.Add(fields);
                                    fields = line[j].Split(new string[] { deLimiter }, StringSplitOptions.None);
                                    //fields = fields[0].Split(',');
                                    j++;
                                }
                            }
                            else
                            {
                                throw;
                            }
                        }
                        stringData.Add(fields);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return stringData;
        }
    }
}
