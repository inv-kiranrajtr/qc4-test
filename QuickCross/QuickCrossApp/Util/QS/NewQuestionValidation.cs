using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Util.QS
{
    class NewQuestionValidation
    {
        string variable = string.Empty;
        public static bool exist = false;
        public  NewQuestionValidation(string variable)
        {
            this.variable = variable;
        }
       public bool Validation_Variable(bool isintegrate=false)
        {
          
            if(!QC4Common.Util.QSUtil.ValidateVariable(variable,out string message))
            {
                QC4Common.Common.MessageDialog.ErrorOk(message);
                exist = false;
                return false;
            }
            else
            {
                bool flag = false;
                if (!isintegrate)
                {
                    for (int i = 0; i < Util.Definiotion.VariableDictionary.Count; i++)
                    {
                        if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).Variable.ToString().ToUpper() == variable.ToUpper())
                        {
                            flag = true;
                            exist = true;
                          
                            return false;

                        }
                    }
                }
                if (!flag)
                {
                    return true;
                }
                return true;
            }
        }
       

    }
}
