
using System;
using System.Collections;

namespace Macromill.QCWeb.Dao.AllCommon {

    public interface EntityDefinedCommonColumn : Entity {

        String LastUpdateUser { get; set; }

        DateTime? LastUpdateDatetime { get; set; }

        void EnableCommonColumnAutoSetup(); // for after disable because the default is enabled
        void DisableCommonColumnAutoSetup();
        bool CanCommonColumnAutoSetup();
    }
}
