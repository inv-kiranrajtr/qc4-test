using log4net;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static ExcelAddIn.Common.Constants;

namespace Qc4Launcher.Forms.UserControls
{
    /// <summary>
    /// Interaction logic for NewItemSelectionWindow.xaml
    /// </summary>
    public partial class NewItemSelectionWindow : Window
    {
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        private ObservableCollection<ExistingVariableList> existingVariableList = new ObservableCollection<ExistingVariableList>();
        private string windowName;
        private string AnsType;
        private List<string> types;
        private ExistingVariableList selectedVariable;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ExistingVariableList SelectedVariable
        {
            get
            {
                return selectedVariable;
            }
            set
            {
                selectedVariable = value;
            }
        }

        public ObservableCollection<ExistingVariableList> ExistingVariableListView
        {
            get
            {
                return existingVariableList;
            }
            set
            {
                existingVariableList = value;
            }
        }

        public NewItemSelectionWindow(string title, string answerType = null)
        {
            InitializeComponent();
            windowName = title;
            AnsType = answerType;
            ReadDataOnLoad();
        }

        private void List_Reuse_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (List_Reuse.SelectedItem != null)
            {
                selectedVariable = (List_Reuse.SelectedItem as ExistingVariableList);
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (List_Reuse.SelectedItem == null)
            {
                Select.IsEnabled = false;
            }
        }

        private void ReadDataOnLoad()
        {
            PopulatedDictionary = Util.Definiotion.VariableDictionary;

            types = new List<string>();

            switch (windowName)
            {
                case Util.Constants.ProcessingMethod.RECODE:
                    if (AnsType != AnswerType.MA)
                    {
                        types.Add(AnswerType.SA);
                    }
                    types.Add(AnswerType.MA);
                    break;
                case Util.Constants.ProcessingMethod.INTEGRATE:
                    types.Add(AnswerType.MA);
                    types.Add(AnswerType.SA);
                    break;
                case Util.Constants.ProcessingMethod.CLASS:
                    types.Add(AnswerType.SA);
                    break;
                case Util.Constants.ProcessingMethod.MCONVERT:
                    types.Add(AnswerType.MA);
                    break;
                case Util.Constants.ProcessingMethod.COUNT:
                    types.Add(AnswerType.N);
                    types.Add(AnswerType.SA);
                    break;
                case Util.Constants.ProcessingMethod.ADD:
                    types.Add(AnswerType.MA);
                    break;
                case Util.Constants.ProcessingMethod.JOINT:
                    types.Add(AnswerType.MA);
                    break;
                case Util.Constants.ProcessingMethod.MTOS:
                    types.Add(AnswerType.SA);
                    break;
                case Util.Constants.ProcessingMethod.GROUP:
                    types.Add(AnswerType.N);
                    break;
                case Util.Constants.ProcessingMethod.COMPUTE:
                    types.Add(AnswerType.N);
                    break;
            }
            foreach (KeyValuePair<string, QuestionSettings> item in PopulatedDictionary)
            {
                QuestionSettings qs = item.Value;
                if (qs.QuestionFlag == "New")
                {
                    foreach (var type in types)
                    {
                        if (qs.AnswerType == type)
                        {
                            existingVariableList.Add(new ExistingVariableList()
                            {
                                Variable = qs.Variable,
                                AnswerType = qs.AnswerType,
                                Question = frmutil.EscapeCRLF(qs.Question),
                                Title = frmutil.EscapeCRLF(qs.TableHeading),
                                Choices = qs.Choices,
                                AswerTypes = types
                            });
                        }

                    }

                }
            }
            if ((ExistingVariableListView == null) || (ExistingVariableListView.Count == 0))
            {
                Select.IsEnabled = false;
            }
            else
            {
                List_Reuse.ItemsSource = ExistingVariableListView;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SearchNewVariable()
        {
            var result = ExistingVariableListView.Where(p => (p.Question.IndexOf(txt_question_search.Text, StringComparison.OrdinalIgnoreCase) != -1) && (p.Variable.IndexOf(txt_newVariable_search.Text, StringComparison.OrdinalIgnoreCase) != -1)).ToList();

            if (result.Count == 0)
            {
                Select.IsEnabled = false;
            }
            List_Reuse.ItemsSource = result;
        }
        #region class

        public class ExistingVariableList
        {
            private string variable;
            private string answertype;
            private string question;
            private string title;
            private List<string> choices;
            private List<string> aswerTypes;

            public string Variable
            {
                get
                {
                    return variable;
                }
                set
                {
                    variable = value;
                }
            }

            public string AnswerType
            {
                get
                {
                    return answertype;
                }
                set
                {
                    answertype = value;
                }
            }
            public string Question
            {
                get
                {
                    return question;
                }
                set
                {
                    question = value;
                }
            }
            public string Title
            {
                get
                {
                    return title;
                }
                set
                {
                    title = value;
                }
            }
            public List<string> Choices
            {
                get
                {
                    return choices;
                }
                set
                {
                    choices = value;
                }
                    
            }
            public string AnswerTypeWithNoOfChoices
            {
                get
                {
                    if (choices.Count == 0)
                    {
                        return answertype;
                    }
                    else
                    {
                        return string.Join("/", answertype, choices.Count);
                    }
                }
            }
            public List<string> AswerTypes
            {
                get
                {
                    return aswerTypes;
                }
                set
                {
                    aswerTypes = value;
                }
            }
        }
        #endregion

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Txt_newVariable_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (List_Reuse.SelectedItem == null)
            {
                Select.IsEnabled = false;
            }
            SearchNewVariable();
        }

        private void Txt_question_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (List_Reuse.SelectedItem == null)
            {
                Select.IsEnabled = false;
            }
            SearchNewVariable();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            if (List_Reuse.SelectedItem != null)
            {
                selectedVariable = (List_Reuse.SelectedItem as ExistingVariableList);
            }
            this.Close();
        }

        private void List_Reuse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_Reuse.SelectedItem != null)
            {
                Select.IsEnabled = true;
            }
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
