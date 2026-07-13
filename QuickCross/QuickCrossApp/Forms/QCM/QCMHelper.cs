using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Forms.QCM
{
    class QCMHelper
    {
        public static int TitleQuestionChoice_TextLimit = 32767;

        internal static class QlayoutVariables
        {
            public static string surveyID;
            public static string surveyName;
            public static int ColQuestionNumber = 0;
            public static int ColQuestionType = 1;
            public static int ColNumberOfQuestion = 2;
            public static int ColItem = 3;
            public static int ColAnswerType = 4;
            public static int ColCategories = 5;
            public static int ColWT = 6;
            public static int ColSortDisplay = 7;
            public static int Colmn = 8;
            public static int ColTableHeading = 9;
            public static int ColQuestion = 10;
            public static int ColChoices = 11;
        }

        public static void SetQlayoutvaraibles(string[] header)
        {
            for (int i = 0; i < header.Count(); i++)
            {
                if (header[i].Equals(LocalResource.LBL_QUESTION_NUMBER))
                    QlayoutVariables.ColQuestionNumber = i;
                else if (header[i].Equals(LocalResource.LBL_QUESTION_TYPE))
                    QlayoutVariables.ColQuestionType = i;
                else if (header[i].Equals(LocalResource.LBL_QUESTION_COUNT))
                    QlayoutVariables.ColNumberOfQuestion = i;
                else if (header[i].Equals(LocalResource.LBL_VARIABLE))
                    QlayoutVariables.ColItem = i;
                else if (header[i].Equals(LocalResource.LBL_ANSWER_TYPE))
                    QlayoutVariables.ColAnswerType = i;
                else if (header[i].Equals(LocalResource.LBL_CATEGORY_COUNT))
                    QlayoutVariables.ColCategories = i;
                else if (header[i].Equals(LocalResource.LBL_SCORE))
                    QlayoutVariables.ColWT = i;
                else if (header[i].Equals(LocalResource.LBL_SORTING))
                    QlayoutVariables.ColSortDisplay = i;
                else if (header[i].Equals(LocalResource.LBL_COLUMN))
                    QlayoutVariables.Colmn = i;
                else if (header[i].Equals(LocalResource.LBL_QUESTION_A))
                    QlayoutVariables.ColTableHeading = i;
                else if (header[i].Equals(LocalResource.LBL_QUESTION_B))
                    QlayoutVariables.ColQuestion = i;
            }
        }

        public static Encoding DetectEncoding(string fileName)
        {
            Byte[] bytes = File.ReadAllBytes(fileName);
            Encoding encoding = null;
            String text = null;
            // Test UTF8 with BOM. This check can easily be copied and adapted
            // to detect many other encodings that use BOMs.
            UTF8Encoding encUtf8Bom = new UTF8Encoding(true, true);
            Boolean couldBeUtf8 = true;
            Byte[] preamble = encUtf8Bom.GetPreamble();
            Int32 prLen = preamble.Length;
            if (bytes.Length >= prLen && preamble.SequenceEqual(bytes.Take(prLen)))
            {
                // UTF8 BOM found; use encUtf8Bom to decode.
                try
                {
                    // Seems that despite being an encoding with preamble,
                    // it doesn't actually skip said preamble when decoding...
                    text = encUtf8Bom.GetString(bytes, prLen, bytes.Length - prLen);
                    encoding = encUtf8Bom;
                }
                catch (ArgumentException)
                {
                    // Confirmed as not UTF-8!
                    couldBeUtf8 = false;
                }
            }
            // use boolean to skip this if it's already confirmed as incorrect UTF-8 decoding.
            if (couldBeUtf8 && encoding == null)
            {
                // test UTF-8 on strict encoding rules. Note that on pure ASCII this will
                // succeed as well, since valid ASCII is automatically valid UTF-8.
                UTF8Encoding encUtf8NoBom = new UTF8Encoding(false, true);
                try
                {
                    text = encUtf8NoBom.GetString(bytes);
                    encoding = encUtf8NoBom;
                }
                catch (ArgumentException)
                {
                    // Confirmed as not UTF-8!
                }
            }
            // fall back to default ANSI encoding.
            if (encoding == null)
            {
                encoding = Encoding.GetEncoding("shift_jis");
                text = encoding.GetString(bytes);
            }
            return encoding;
        }

        public static string GenerateFileName(int count, string ext, string fileName, string filePath)
        {
            string fullPath = filePath + "\\" + fileName + "(" + (count++) + ")" + ext;
            if (File.Exists(fullPath))
            {
                fullPath = GenerateFileName(count, ext, fileName, filePath);
            }
            return fullPath;
        }

        public static string ReturnErrorLabel()
        {
            return "QCM ERROR: ";
        }

        public static void SetQstnColumn_WT(int value)
        {
            QlayoutVariables.ColSortDisplay = 7 + value;
            QlayoutVariables.Colmn = 8 + value;
            QlayoutVariables.ColTableHeading = 9 + value;
            QlayoutVariables.ColQuestion = 10 + value;
            QlayoutVariables.ColChoices = 11 + value;
        }
    }
}
