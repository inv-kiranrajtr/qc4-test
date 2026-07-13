
using System;
using System.Collections.Generic;
using Macromill.QCWeb.Dao.AllCommon;

namespace Macromill.QCWeb.Dao.ExEntity.Customize {

    partial class TItemInfoQuestions {

        public CDef.AnswerType AnswerTypeAsAnswerType {
            get {
                return CDef.AnswerType.CodeOf(_answerType);
            }
            set {
                AnswerType = value != null ? value.Code : null;
            }
        }

        /// <summary>
        /// Is the value of answerType 'SA'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// SA: SA‚ðŽ¦‚·
        /// ]]>
        /// </summary>
        public bool IsAnswerTypeSA {
            get {
                CDef.AnswerType cls = AnswerTypeAsAnswerType;
                return cls != null ? cls.Equals(CDef.AnswerType.SA) : false;
            }
        }

        /// <summary>
        /// Is the value of answerType 'MA'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// MA: MA‚ðŽ¦‚·
        /// ]]>
        /// </summary>
        public bool IsAnswerTypeMA {
            get {
                CDef.AnswerType cls = AnswerTypeAsAnswerType;
                return cls != null ? cls.Equals(CDef.AnswerType.MA) : false;
            }
        }

    }
}
