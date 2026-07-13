
using System;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;

namespace Macromill.QCWeb.Dao.AllCommon.CBean {

// JavaLike
public interface ConditionQuery {

    // ===================================================================================
    //                                                                          Table Name
    //                                                                          ==========
    String getTableDbName();
    String getTableSqlName();
	
    // ===================================================================================
    //                                                                  Important Accessor
    //                                                                  ==================
    ConditionQuery xgetReferrerQuery();
    SqlClause xgetSqlClause();
    String xgetAliasName();
    String toColumnRealName(String columnName);
    int xgetNestLevel();
    int xgetNextNestLevel();
    bool isBaseQuery();
	String xgetForeignPropertyName();
    String xgetRelationPath();
    String xgetLocationBase();
	
    // ===================================================================================
    //                                                                 Reflection Invoking
    //                                                                 ===================
    ConditionValue invokeValue(String columnFlexibleName);
    void invokeQuery(String columnFlexibleName, String conditionKeyName, Object value);
    void invokeOrderBy(String columnFlexibleName, bool isAsc);
    ConditionQuery invokeForeignCQ(String foreignPropertyName);
    bool invokeHasForeignCQ(String foreignPropertyName);
}

}
