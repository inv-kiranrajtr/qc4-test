
using System;
using System.Collections.Generic;

using Seasar.Quill.Attrs;
using Seasar.Dao.Attrs;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.S2Dao;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.CBean;

namespace Macromill.QCWeb.Dao.ExDao {

    [Implementation]
    [S2Dao(typeof(S2DaoSetting))]
    [Bean(typeof(TPolylineCategoryList))]
    public partial interface TPolylineCategoryListDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TPolylineCategoryListCB cb);
        IList<TPolylineCategoryList> SelectList(TPolylineCategoryListCB cb);

        int Insert(TPolylineCategoryList entity);
        int UpdateModifiedOnly(TPolylineCategoryList entity);
        int UpdateNonstrictModifiedOnly(TPolylineCategoryList entity);
        int UpdateByQuery(TPolylineCategoryListCB cb, TPolylineCategoryList entity);
        int Delete(TPolylineCategoryList entity);
        int DeleteNonstrict(TPolylineCategoryList entity);
        int DeleteByQuery(TPolylineCategoryListCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
