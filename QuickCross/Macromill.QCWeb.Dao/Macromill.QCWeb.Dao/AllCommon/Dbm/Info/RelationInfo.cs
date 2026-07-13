
using System;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;

namespace Macromill.QCWeb.Dao.AllCommon.Dbm.Info {
    
    public interface RelationInfo {

        String RelationPropertyName { get; }
        DBMeta LocalDBMeta { get; }
        DBMeta TargetDBMeta { get; }
        Map<ColumnInfo,ColumnInfo> LocalTargetColumnInfoMap { get; }
        bool IsOneToOne { get; }
        bool IsReferrer { get; }
    }
}
