using Microsoft.Office.Interop.Excel;

namespace QC4Common.Model
{
    public class CossTableInput
    {

        public CossTableInput(string target, string axis1, string axis2, string lineSettings, FilterSettingsCr filter,
           Range targetRange, AxisSetting Axis1Range, AxisSetting Axis2Range, AxisSetting lineSettingsRange, AxisSetting filterRange)
        {
            this.target = target;
            this.axis1 = axis1;
            this.axis2 = axis2;
            this.lineSettings = lineSettings;
            this.filter = filter;
            this.targetRange = targetRange;
            this.Axis1Range = Axis1Range;
            this.Axis2Range = Axis2Range;
            this.lineSettingsRange = lineSettingsRange;
            this.filterRange = filterRange;
        }
        public CossTableInput(string target, string axis1, string lineSettings, FilterSettingsCr filter,
           Range targetRange, AxisSetting Axis1Range, AxisSetting lineSettingsRange, AxisSetting filterRange, bool combine = false)
        {
            this.target = target;
            this.axis1 = axis1;
            this.lineSettings = lineSettings;
            this.filter = filter;
            this.targetRange = targetRange;
            this.Axis1Range = Axis1Range;
            this.lineSettingsRange = lineSettingsRange;
            this.filterRange = filterRange;
            this.combine = combine;
        }
        bool hasCount = false;
        bool hasWeight = false;
        public string target { get; set; }
        public string axis1 { get; set; }
        public string axis2 { get; set; }
        public string lineSettings { get; set; }
        public FilterSettingsCr filter { get; set; }
        public bool HasCount { get => hasCount; set => hasCount = value; }
        public bool HasWeight { get => hasWeight; set => hasWeight = value; }
        public Range targetRange { get; set; }
        public AxisSetting Axis1Range { get; set; }
        public AxisSetting Axis2Range { get; set; }
        public AxisSetting lineSettingsRange { get; set; }
        public AxisSetting filterRange { get; set; }
        public string filePathTarget { get; set; }
        public int lineNo { get; set; }
        public string filePathAxis1 { get; set; }
        public string filePathAxis2 { get; set; }
        public bool combine { get; set; }
    }

    public class AxisSetting
    {
        public AxisSetting(string variable, string variableType, string choiceCnt, int col)
        {
            this.variable = variable;
            this.variableType = variableType;
            this.choiceCnt = choiceCnt;
            this.Column = col;
        }

        public AxisSetting(string variable, string variableType, string choiceCnt, int col, string variableTripple) : this(variable, variableType, choiceCnt, col)
        {
            this.variableTripple = variableTripple;
        }

        public AxisSetting(string variable, int column)
        {
            this.variable = variable;
            Column = column;
        }

        public string variable { get; set; }
        public string variableType { get; set; }
        public string choiceCnt { get; set; }
        public string variableTripple { get; set; }
        public int Column { get; set; }
    }

    public class FilterSettingsCr
    {

        public FilterSettingsCr()
        {

        }

        public FilterSettingsCr(string variable, string operatorType, string values, string conditionType,string Question)
        {
            this.variable = variable;
            this.operatorType = operatorType;
            this.values = values;
            this.conditionType = conditionType;
            this.Question = Question;
        }

        public string variable { get; set; }
        public string Question { get; set; }
        public string operatorType { get; set; }
        public string values { get; set; }
        public string conditionType { get; set; }
    }
}

