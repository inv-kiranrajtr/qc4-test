using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VB = Microsoft.VisualBasic;
namespace Qc4Launcher.Forms.QCM
{
    class QCMValidation
    {
        public static bool ValidateSelectedFiles(String qlayoutFilePath, String qrawdataFilePath, String outputPath, out string message)
        {
            try
            {
                message = "";
                if (qlayoutFilePath.Length > 260 || qrawdataFilePath.Length > 260 || outputPath.Length > 260)
                {
                    message = LocalResource.MSG_PATH_TOO_LONG;
                    return false;
                }
                if (!System.IO.File.Exists(qlayoutFilePath) || !System.IO.File.Exists(qrawdataFilePath))
                {
                    message = LocalResource.QCM_INPUT_FILE_NOTEXIST;
                    return false;
                }
                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(outputPath)))
                {
                    message = LocalResource.QCM_INVALID_PATH;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                message = QCMHelper.ReturnErrorLabel() + ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException;
                return false;
            }
        }

        public static bool ValidateFileName(String qlayoutFilePath, String qrawdataFilePath, out string message)
        {
            try
            {
                message = "";
                String layoutFileName = System.IO.Path.GetFileName(qlayoutFilePath).Replace("_Qlayout.csv", "");
                String rawdataFileName = System.IO.Path.GetFileName(qrawdataFilePath).Replace("_Qrawdata.tsv", "");

                if (layoutFileName != rawdataFileName)
                {
                    message = LocalResource.QCM_FILENAME_MISMATCH;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                message = QCMHelper.ReturnErrorLabel() + ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException;
                return false;
            }
        }

        public static bool ValidateEncoding(string fileName, Encoding encode, out string message,bool isFromIM = false)
        {
            try
            {
                message = "";
                Encoding detectedEncode = QCMHelper.DetectEncoding(fileName);
                if (!detectedEncode.EncodingName.Equals(encode.EncodingName))
                {
                    message = LocalResource.QCM_WRNG_ENCODING;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                string label = isFromIM ? "IM ERROR: " : QCMHelper.ReturnErrorLabel();
                message = label + ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException;
                return false;
            }
        }

        public static bool Validate_QlayoutSampleID(List<string[]> parsedCsv, out string message)
        {
            message = "";
            if (parsedCsv.Count() >= 3 && parsedCsv[1].Count() >= 11)
            {
                parsedCsv[2][3] = parsedCsv[2][3].ToUpper();
                if (SampleIDChecker(parsedCsv[2][3]))
                {
                    if (FileEndChecker(parsedCsv[parsedCsv.Count - 1][0].ToUpper()))
                    {
                        parsedCsv.RemoveAt(parsedCsv.Count - 1);
                    }
                    else
                    {
                        message = LocalResource.QCM_FILEEND_NOTSET_QLAYOUT;
                        return false;
                    }
                }
                else
                {
                    message = LocalResource.QC3PARSE_ALERT_NOT_CONTAIN_SAMPLEID;
                    return false;
                }
            }
            else
            {
                message = LocalResource.QCM_ZERO_QLAYOUT_DATA;
                return false;
            }
            return true;
        }

        public static bool Validate_QrawDataSampleID(List<string[]> parsedTsv, out string message)
        {
            message = "";
            if (parsedTsv != null && parsedTsv.Count > 1)
            {
                parsedTsv[0][0] = parsedTsv[0][0].ToUpper();
                if (SampleIDChecker(parsedTsv[0][0]))
                {
                    if (FileEndChecker(parsedTsv[parsedTsv.Count - 1][0].ToUpper()))
                    {
                        parsedTsv.RemoveAt(parsedTsv.Count - 1);
                    }
                    else
                    {
                        message = LocalResource.QCM_FILEEND_NOTSET_QRAWDATA;
                        return false;
                    }
                }
                else
                {
                    message = LocalResource.QC3PARSE_ALERT_NOT_CONTAIN_SAMPLEID;
                    return false;
                }
            }
            else
            {
                message = LocalResource.QCM_ZERO_QRAWDATA;
                return false;
            }
            return true;
        }

        public static bool ValidateQlayoutdataAndQrawdata(List<Qc3Parse.QDataDetail> qDataDetails, String[] qlayoutHeader, out string message)
        {
            try
            {
                message = "";

                if (qlayoutHeader.Distinct().Count() != qlayoutHeader.Count())
                {
                    message = LocalResource.QCM_DUPLICATE_ITEM_QRAWDATA;
                    return false;
                }

                if (qDataDetails != null && qlayoutHeader != null && qDataDetails.Count() == qlayoutHeader.Count())
                {
                    for (int i = 0; i < qDataDetails.Count(); i++)
                    {
                        string itemName = qlayoutHeader[i].ToUpper();
                        itemName = QCMConversionRules.ReConvertProcess(itemName);
                        itemName = QCMConversionRules.ConvertCommaArrow(itemName);
                        if (!ValidateEachCharacter(qDataDetails[i].variableName.ToUpper(), itemName) /*&& !(qDataDetails[i].variableName.ToUpper().Equals(itemName))*/)
                        {
                            message = LocalResource.QCM_ITEM_NOTMATCH;
                            return false;
                        }
                    }
                }
                else
                {
                    message = LocalResource.QCM_ITEM_NOTMATCH;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                message = QCMHelper.ReturnErrorLabel() + ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException;
                return false;
            }
        }

        public static bool SampleIDChecker(string value)
        {
            if (value != "SAMPLEID")
                return false;

            return true;
        }

        public static bool FileEndChecker(string value)
        {
            if (value != "FILEEND")
                return false;

            return true;
        }

        public static bool CheckAnswerType(string answerType)
        {
            if (answerType.Equals(Util.Constants.AnswerType.SA))
                return true;
            if (answerType.Equals(Util.Constants.AnswerType.N))
                return true;
            if (answerType.Equals(Util.Constants.AnswerType.D))
                return true;
            if (answerType.Equals(Util.Constants.AnswerType.FA))
                return true;
            if (answerType.Equals(Util.Constants.AnswerType.MA))
                return true;

            return false;
        }

        public static bool CheckQuestionType(string questionType)
        {
            if (questionType.Equals(Util.Constants.QuestionType.SAP))
                return true;
            if (questionType.Equals(Util.Constants.QuestionType.SAR))
                return true;
            if (questionType.Equals(Util.Constants.QuestionType.MTS))
                return true;
            if (questionType.Equals(Util.Constants.QuestionType.RNK))
                return true;
            if (questionType.Equals(Util.Constants.QuestionType.MTT))
                return true;
            if (questionType.Equals(Util.Constants.QuestionType.SAS))
                return true;
            if (questionType.Equals(Util.Constants.QuestionType.MAC))
                return true;
            if (questionType.Equals(Util.Constants.QuestionType.MTM))
                return true;
            if (questionType.Equals(Util.Constants.QuestionType.RAT))
                return true;
            if (questionType.Equals(Util.Constants.QuestionType.FAS))
                return true;
            if (questionType.Equals(Util.Constants.QuestionType.FAL))
                return true;          
            return false;
        }

        public static bool ValidateQuestionTypeAndAnswerType(string answerType, string questionType)
        {
            if (answerType.Equals(Util.Constants.AnswerType.SA))
            {
                if (questionType.Equals(Util.Constants.QuestionType.SAP))
                    return true;
                if (questionType.Equals(Util.Constants.QuestionType.SAR))
                    return true;
                if (questionType.Equals(Util.Constants.QuestionType.MTS))
                    return true;
                if (questionType.Equals(Util.Constants.QuestionType.RNK))
                    return true;
                if (questionType.Equals(Util.Constants.QuestionType.MTT))
                    return true;
                if (questionType.Equals(Util.Constants.QuestionType.SAS))
                    return true;
                if (questionType.Equals(""))
                    return true;
                else
                    return false;
            }

            if (answerType.Equals(Util.Constants.AnswerType.MA))
            {
                if (questionType.Equals(Util.Constants.QuestionType.MAC))
                    return true;
                if (questionType.Equals(Util.Constants.QuestionType.MTM))
                    return true;
                if (questionType.Equals(""))
                    return true;
                else
                    return false;
            }

            if (answerType.Equals(Util.Constants.AnswerType.N))
            {
                if (questionType.Equals(Util.Constants.QuestionType.RAT))
                    return true;
                if (questionType.Equals(Util.Constants.QuestionType.FAS))
                    return true;
                if (questionType.Equals(""))
                    return true;
                else
                    return false;
            }

            if (answerType.Equals(Util.Constants.AnswerType.FA))
            {
                if (questionType.Equals(Util.Constants.QuestionType.FAL))
                    return true;
                if (questionType.Equals(Util.Constants.QuestionType.FAS))
                    return true;
                if (questionType.Equals(""))
                    return true;
                else
                    return false;
            }

            if (answerType.Equals(Util.Constants.AnswerType.D))
            {
                if (questionType.Equals(""))
                    return true;
                else
                    return false;
            }
            return true;
        }

        public static bool ValidateSortValue(string answerType, string sortValue,int categoryCount)
        {
            if (answerType.Equals(Util.Constants.AnswerType.SA) || answerType.Equals(Util.Constants.AnswerType.MA))
            {
                if (QCMConversionRules.IsDigitsOnly(sortValue))
                {
                    if (Convert.ToInt32(sortValue) >= 1 && Convert.ToInt32(sortValue) <= categoryCount)
                        return true;
                }
            }
            return false;
        }

        public static bool ValidateChoiceAndAnswerType(string answertype)
        {
            if (answertype.Equals(Util.Constants.AnswerType.SA))
                return true;
            if (answertype.Equals(Util.Constants.AnswerType.MA))
                return true;

            return false;
        }

        public static bool ValidateZeroChoiceAndAnswerType(string answerType)
        {
            if (answerType.Equals(Util.Constants.AnswerType.N))
                return true;
            if (answerType.Equals(Util.Constants.AnswerType.D))
                return true;
            if (answerType.Equals(Util.Constants.AnswerType.FA))
                return true;

            return false;
        }

        //public static bool ValidateInteger(string value)
        //{
        //    if (value.Contains("."))
        //        return false;

        //    return true;
        //}

        public static bool ValidateInRange(int value1, int value2, string value3)
        {
            try
            {
                int intValue = Convert.ToInt32(value3);
                if (intValue < value1 || intValue > value2)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumeric(string text)
        {
            if (text.Equals(""))
                return true;

            double test;
            return double.TryParse(text, out test);
        }

        public static bool ValidateEachCharacter(String value1, String value2)
        {
            string Str1;
            string Str2;
            int i;
            if (value1.Count() == value2.Count())
            {
                for (i = 1; i <= VB.Strings.Len(value1); i++)
                {
                    Str1 = VB.Strings.Mid(value1, i, 1);
                    Str2 = VB.Strings.Mid(value2, i, 1);
                    if (Str1 != Str2)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
