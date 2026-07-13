
using System;
using System.Data;
using Macromill.QCWeb.Dao.AllCommon.Ado;

namespace Macromill.QCWeb.Dao.ExDao.Cursor {

    [System.Serializable]
    public partial class RawDataItemViewFAListEntityCursorHandler : Macromill.QCWeb.Dao.AllCommon.Ado.CursorHandler {

        protected CursorFetcher<RawDataItemViewFAListEntityCursor> _cursorFetcher;

        public RawDataItemViewFAListEntityCursorHandler(CursorFetcher<RawDataItemViewFAListEntityCursor> cursorFetcher) {
            _cursorFetcher = cursorFetcher;
        }

        public virtual Object Handle(IDataReader reader) {
            return FetchCursor(reader);
        }

        protected virtual Object FetchCursor(IDataReader reader) {
            return _cursorFetcher.Invoke(CreateTypeSafeCursor(reader));
        }

        protected virtual RawDataItemViewFAListEntityCursor CreateTypeSafeCursor(IDataReader reader) {
            RawDataItemViewFAListEntityCursor cursor = new RawDataItemViewFAListEntityCursor();
            cursor.Accept(reader);
            return cursor;
        }
    }
}
