
using System;
using System.Data;
using Seasar.Framework.Util;

namespace Macromill.QCWeb.Dao.ExDao.Cursor {

    public partial class CategoryInfoRawDataEntityCursor {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        // -------------------------------------------------
        //                                    Column DB Name
        //                                    --------------
        public static readonly String DB_NAME_SAMPLE_ID = "SAMPLE_ID";
        public static readonly String DB_NAME_RAWDATA = "RAWDATA";

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected IDataReader _reader;

        // ===============================================================================
        //                                                                         Prepare
        //                                                                         =======
        public virtual void Accept(IDataReader reader) {
            this._reader = reader;
        }

        // ===============================================================================
        //                                                                          Direct
        //                                                                          ======
        public virtual IDataReader Cursor {
            get { return _reader; }
        }

        // ===============================================================================
        //                                                                        Delegate
        //                                                                        ========
        public virtual bool Next() {
            return _reader.Read();
        }

        // ===============================================================================
        //                                                              Type Safe Accessor
        //                                                              ==================
        public String SampleId {
            get { return ExtractValueAsString("SAMPLE_ID"); }
        }

        public String Rawdata {
            get { return ExtractValueAsString("RAWDATA"); }
        }

        // ===============================================================================
        //                                                                   Assist Helper
        //                                                                   =============
        protected virtual String ExtractValueAsString(String name) {
            Object objValue = ExtractValueAsObject(name);
            if (objValue == null || objValue.Equals(DBNull.Value)) { return null; }
            return (String)objValue;
        }

        protected virtual bool? ExtractValueAsBoolean(String name) {
            Object objValue = ExtractValueAsObject(name);
            if (objValue == null || objValue.Equals(DBNull.Value)) { return null; }
            return new bool?((bool)objValue);
        }

        protected virtual Object ExtractValueAsNumber(Type type, String name) {
            Object objValue = ExtractValueAsObject(name);
            if (objValue == null || objValue.Equals(DBNull.Value)) { return null; }
            decimal value = DecimalConversionUtil.ToDecimal(objValue);
            if (typeof(int?).Equals(type)) { return new int?((int)value); }
            if (typeof(long?).Equals(type)) { return new long?((long)value); }
            if (typeof(decimal?).Equals(type)) { return new decimal?(value); }
            return value;
        }

        protected virtual DateTime? ExtractValueAsDateTime(Type type, String name) {
            Object objValue = ExtractValueAsObject(name);
            if (objValue == null || objValue.Equals(DBNull.Value)) { return null; }
            return new DateTime?((DateTime)objValue);
        }

        protected virtual Object ExtractValueAsObject(String name) {
            return _reader[name];
        }
    }
}
