
using System;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;

namespace Macromill.QCWeb.Dao.AllCommon.CBean.PageNavi.Group {

[System.Serializable]
public class PageGroupOption {

    // ===================================================================================
    //                                                                           Attribute
    //                                                                           =========
    public int _pageGroupSize;
    public int PageGroupSize { get { return _pageGroupSize; } set { _pageGroupSize = value; } }

    // ===================================================================================
    //                                                                      Basic Override
    //                                                                      ==============
    public override String ToString() {
        StringBuilder sb = new StringBuilder();
        sb.append("{");
        sb.append("pageGroupSize=").append(PageGroupSize);
        sb.append("}");
        return sb.toString();
    }
}

}
