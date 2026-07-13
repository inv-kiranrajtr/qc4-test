
using System;
using System.Collections.Generic;
using Macromill.QCWeb.Dao.AllCommon;

namespace Macromill.QCWeb.Dao.ExEntity.Customize {

    partial class RawdataImportQueInfoImportDataUnionAll {
        public CDef.SurveyDataType SurveyDataTypeType {
            get {
                return CDef.SurveyDataType.CodeOf(_surveyDataType);
            }
        }

        public bool IsSurveyDataTypeNormal {
            get {
                CDef.SurveyDataType cls = SurveyDataTypeType;
                return cls != null ? cls.Equals(CDef.SurveyDataType.NORMAL) : false;
            }
        }

        public bool IsSurveyDataTypeAdd {
            get {
                CDef.SurveyDataType cls = SurveyDataTypeType;
                return cls != null ? cls.Equals(CDef.SurveyDataType.ADD) : false;
            }
        }
    }
}
