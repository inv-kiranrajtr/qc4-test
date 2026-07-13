using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelAddIn.Common
{
    public class ReturnClass
    {
        bool result;
        Object value;
        string msg;
        string msgCode;
        private bool v;

        public ReturnClass()
        {
        }

        public ReturnClass(bool result)
        {
            this.result = result;
        }

        public ReturnClass(bool result, object value)
        {
            this.result = result;
            this.value = value;
        }

        public ReturnClass(bool result, object value, string msg)
        {
            this.result = result;
            this.value = value;
            this.msg = msg;
        }

        public ReturnClass(bool result, object value, string msgCode, string[] msgParams)
        {
            this.result = result;
            this.value = value;
            this.msgCode = msgCode;
            this.msg = string.Format(msgCode, msgParams);
        }

        public bool Result { get => result; set => result = value; }
        public object Value { get => value; set => this.value = value; }
        public string Msg { get => msg; set => msg = value; }
        public string MsgCode { get => msgCode; set => msgCode = value; }
    }
}
