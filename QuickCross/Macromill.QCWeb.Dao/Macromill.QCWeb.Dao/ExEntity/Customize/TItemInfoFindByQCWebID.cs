
using System;
using System.Collections.Generic;
using Macromill.QCWeb.Dao.AllCommon;

namespace Macromill.QCWeb.Dao.ExEntity.Customize {

    partial class TItemInfoFindByQCWebID {
        protected IList<TCategoryInfo> _tCategoryInfoList;
        /// <summary>T_CATEGORY_INFO as 'TCategoryInfoList'.</summary>
        public IList<TCategoryInfo> TCategoryInfoList {
            get { if (_tCategoryInfoList == null) { _tCategoryInfoList = new List<TCategoryInfo>(); } return _tCategoryInfoList; }
            set { _tCategoryInfoList = value; }
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
                CDef.AnswerType cls = CDef.AnswerType.CodeOf(AnswerType);
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
                CDef.AnswerType cls = CDef.AnswerType.CodeOf(AnswerType);
                return cls != null ? cls.Equals(CDef.AnswerType.MA) : false;
            }
        }

        /// <summary>
        /// Is the value of answerType 'N'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// N: N‚ðŽ¦‚·
        /// ]]>
        /// </summary>
        public bool IsAnswerTypeN {
            get {
                CDef.AnswerType cls = CDef.AnswerType.CodeOf(AnswerType);
                return cls != null ? cls.Equals(CDef.AnswerType.N) : false;
            }
        }

        /// <summary>
        /// Is the value of answerType 'FA'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// FA: FA‚ðŽ¦‚·
        /// ]]>
        /// </summary>
        public bool IsAnswerTypeFA {
            get {
                CDef.AnswerType cls = CDef.AnswerType.CodeOf(AnswerType);
                return cls != null ? cls.Equals(CDef.AnswerType.FA) : false;
            }
        }

        /// <summary>
        /// Is the value of answerType 'D'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// D: D‚ðŽ¦‚·
        /// ]]>
        /// </summary>
        public bool IsAnswerTypeD {
            get {
                CDef.AnswerType cls = CDef.AnswerType.CodeOf(AnswerType);
                return cls != null ? cls.Equals(CDef.AnswerType.D) : false;
            }
        }
    }
}
