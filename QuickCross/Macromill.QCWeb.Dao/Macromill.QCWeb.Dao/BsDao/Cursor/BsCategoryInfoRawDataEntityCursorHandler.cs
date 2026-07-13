
using System;
using System.Data;
using Macromill.QCWeb.Dao.AllCommon.Ado;

namespace Macromill.QCWeb.Dao.ExDao.Cursor {

    [System.Serializable]
    public partial class CategoryInfoRawDataEntityCursorHandler : Macromill.QCWeb.Dao.AllCommon.Ado.CursorHandler {

        protected CursorFetcher<CategoryInfoRawDataEntityCursor> _cursorFetcher;

        public CategoryInfoRawDataEntityCursorHandler(CursorFetcher<CategoryInfoRawDataEntityCursor> cursorFetcher) {
            _cursorFetcher = cursorFetcher;
        }

        public virtual Object Handle(IDataReader reader) {
            return FetchCursor(reader);
        }

        protected virtual Object FetchCursor(IDataReader reader) {
            return _cursorFetcher.Invoke(CreateTypeSafeCursor(reader));
        }

        protected virtual CategoryInfoRawDataEntityCursor CreateTypeSafeCursor(IDataReader reader) {
            CategoryInfoRawDataEntityCursor cursor = new CategoryInfoRawDataEntityCursor();
            cursor.Accept(reader);
            return cursor;
        }
    }
}
