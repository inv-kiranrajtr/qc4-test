
using System;

using Seasar.Quill.Attrs;
using Macromill.QCWeb.Dao.AllCommon.Bhv;

namespace Macromill.QCWeb.Dao.AllCommon {

    [Implementation(typeof(CacheBehaviorSelector))]
    public interface BehaviorSelector {
        void InitializeConditionBeanMetaData();
        BEHAVIOR Select<BEHAVIOR>() where BEHAVIOR : BehaviorReadable;
        BehaviorReadable ByName(String tableFlexibleName);
    }
}
