using Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Collections.Generic;

namespace Qc4Launcher.Logic
{
    public class ReportBook
    {

        private List<Workbook> ThisBooks;
        private ReportSources ThisReportSources;

        public ReportBook()
        {
            ThisBooks = new List<Workbook>();
            ThisReportSources = new ReportSources();
        }
        ~ReportBook()
        {
            ThisBooks = null;
            ThisReportSources = null;
        }


        public Workbook LastBook()
        {
            Workbook LastBook = null;
            if (ThisBooks.Count > 0) { LastBook = (Workbook)ThisBooks[ThisBooks.Count - 1]; }
            return LastBook;
        }

        public List<Workbook> Books()
        {
            return ThisBooks;
        }

        public void CloseAllBooks()
        {
            foreach (Workbook wb in ThisBooks)
            {
                wb.Close(false);
            }
            ThisBooks = new List<Workbook>();
        }

        public Workbook Item(int Index)
        {
            Workbook Item = null;
            if (Index >= 0 && Index < ThisBooks.Count)
            {
                Item = (Workbook)ThisBooks[Index];
            }
            return Item;
        }

        //Public Property Get NewEnum() As stdole.IUnknown
        //Attribute NewEnum.VB_UserMemId = -4
        //    Set NewEnum = ThisBooks.[_NewEnum]
        //}

        public void Add(Workbook Book){
            ThisBooks.Add(Book);
        }

        public ReportSources Sources() {
            return ThisReportSources;
       }

    }
}