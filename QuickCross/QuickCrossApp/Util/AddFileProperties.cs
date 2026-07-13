using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Util
{
    class AddFileProperties
    {
        public void AddFileVersion(Excel.Workbook workbook)
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, QC4Common.Common.Constants.SheetCodeName.Setting);
            Excel.Range r = s.Cells[QC4Common.Common.Constants.FileProperties.File_version_Row,QC4Common.Common.Constants.FileProperties.File_version_Column];
            r.Value = "0.1.0";
        }
        public void AddSource(Microsoft.Office.Interop.Excel.Workbook workbook)
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, QC4Common.Common.Constants.SheetCodeName.Setting);
            Excel.Range r = s.Cells[QC4Common.Common.Constants.FileProperties.Source_Row, QC4Common.Common.Constants.FileProperties.Source_Column];
            r.Value = QC4Common.Common.Constants.FileProperties.QC4;
            r = s.Cells[QC4Common.Common.Constants.FileProperties.Source_vesion_Row, QC4Common.Common.Constants.FileProperties.Source_version_Column];
            r.Value = string.Format("{0}.{1}.{2}", Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(), Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString(), Assembly.GetExecutingAssembly().GetName().Version.Build.ToString());
        }
        public void AddUpdatedSource(Excel.Workbook workbook)
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, QC4Common.Common.Constants.SheetCodeName.Setting);
            Excel.Range r = s.Cells[QC4Common.Common.Constants.FileProperties.Updated_Source_Row, QC4Common.Common.Constants.FileProperties.Updated_Source_Column];
            r.Value = QC4Common.Common.Constants.FileProperties.QC4;
            r = s.Cells[QC4Common.Common.Constants.FileProperties.Updated_Source_vesion_Row, QC4Common.Common.Constants.FileProperties.Updated_Source_vesion_Column];
            r.Value = string.Format("{0}.{1}.{2}", Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(), Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString(), Assembly.GetExecutingAssembly().GetName().Version.Build.ToString());

        }

    }
}
