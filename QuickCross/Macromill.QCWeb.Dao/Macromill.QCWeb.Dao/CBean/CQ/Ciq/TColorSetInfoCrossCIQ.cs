
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CKey;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ.BS;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.CQ.Ciq {

    [System.Serializable]
    public class TColorSetInfoCrossCIQ : AbstractBsTColorSetInfoCrossCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTColorSetInfoCrossCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TColorSetInfoCrossCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTColorSetInfoCrossCQ myCQ)
            : base(childQuery, sqlClause, aliasName, nestLevel) {
            _myCQ = myCQ;
            _foreignPropertyName = _myCQ.xgetForeignPropertyName();// Accept foreign property name.
            _relationPath = _myCQ.xgetRelationPath();// Accept relation path.
        }

        // ===================================================================================
        //                                                             Override about Register
        //                                                             =======================
        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            throw new UnsupportedOperationException("InlineQuery must not need UNION method: " + baseQueryAsSuper + " : " + unionQueryAsSuper);
        }
    
        protected override void setupConditionValueAndRegisterWhereClause(ConditionKey key, Object value, ConditionValue cvalue, String colName) {
            regIQ(key, value, cvalue, colName);
        }
    
        protected override void setupConditionValueAndRegisterWhereClause(ConditionKey key, Object value, ConditionValue cvalue
                                                                        , String colName, ConditionOption option) {
            regIQ(key, value, cvalue, colName, option);
        }
    
        protected override void registerWhereClause(String whereClause) {
            registerInlineWhereClause(whereClause);
        }
    
        protected override String getInScopeSubQueryRealColumnName(String columnName) {
            if (_onClause) {
                throw new UnsupportedOperationException("InScopeSubQuery of on-clause is unsupported");
            }
            return _onClause ? xgetAliasName() + "." + columnName : columnName;
        }
    
        protected override void registerExistsSubQuery(ConditionQuery subQuery
                                     , String columnName, String relatedColumnName, String propertyName) {
            throw new UnsupportedOperationException("Sorry! ExistsSubQuery at inline view is unsupported. So please use InScopeSubQyery.");
        }


        protected override ConditionValue getCValueColorSetInfoCrossId() {
            return _myCQ.ColorSetInfoCrossId;
        }


        public override String keepColorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepColorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossList(subQuery);
        }

        public override String keepColorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepColorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossList(subQuery);
        }

        public override String keepColorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            return _myCQ.keepColorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossList(subQuery);
        }

        public override String keepColorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            return _myCQ.keepColorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossList(subQuery);
        }
        public override String keepColorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueTypeCode() {
            return _myCQ.TypeCode;
        }


        protected override ConditionValue getCValueGradationType() {
            return _myCQ.GradationType;
        }


        protected override ConditionValue getCValueCrossScenarioTargetId() {
            return _myCQ.CrossScenarioTargetId;
        }


        public override String keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget(TCrossScenarioTargetCQ subQuery) {
            return _myCQ.keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget(subQuery);
        }

        public override String keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget(TCrossScenarioTargetCQ subQuery) {
            return _myCQ.keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget(subQuery);
        }

        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TColorSetInfoCrossCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TColorSetInfoCrossCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
