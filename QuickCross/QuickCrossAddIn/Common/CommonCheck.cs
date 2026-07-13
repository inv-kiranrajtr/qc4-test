using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelAddIn.Common
{
	class CommonCheck
	{
		internal static bool PrinterCheck()
		{
			return System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count > 0;
		}
	}
}
