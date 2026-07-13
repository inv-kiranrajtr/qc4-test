
using System;

namespace Macromill.QCWeb.Dao.AllCommon.CBean.CKey {

public class ConditionKeyNotEqualStandard : ConditionKeyNotEqual {

    protected override String defineOperand() {
        return "<>"; // is SQL standard operand
    }
}
	
}
