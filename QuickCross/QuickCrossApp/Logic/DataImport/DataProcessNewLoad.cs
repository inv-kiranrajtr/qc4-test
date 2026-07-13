using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Logic.DataImport
{
    public class DataProcessNewLoad
    {
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
     
        public QuestionSettings Txt_Change_New_Item(string Variable)
        {
            QuestionSettings question = new QuestionSettings();
            var comparer = StringComparer.OrdinalIgnoreCase;
            Dictionary<string, QuestionSettings> Dic = new Dictionary<string, QuestionSettings>(Util.Definiotion.VariableDictionary,comparer);
          
           
            if (Dic.ContainsKey(Variable))
            {
                question.Question = frmutil.UnEscapeCRLF(Dic[Variable].Question);
                question.TableHeading = frmutil.UnEscapeCRLF(Dic[Variable].TableHeading);
                question.AnswerType= (Dic[Variable].AnswerType);
                question.Choices = (Dic[Variable].Choices);
                return question;
            }
            else
            {
                return null;
            }
        }
    }
}
