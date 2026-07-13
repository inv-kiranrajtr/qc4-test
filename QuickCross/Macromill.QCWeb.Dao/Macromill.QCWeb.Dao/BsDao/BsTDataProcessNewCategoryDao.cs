
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
    [Bean(typeof(TDataProcessNewCategory))]
    public partial interface TDataProcessNewCategoryDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDataProcessNewCategoryCB cb);
        IList<TDataProcessNewCategory> SelectList(TDataProcessNewCategoryCB cb);

        int Insert(TDataProcessNewCategory entity);
        int UpdateModifiedOnly(TDataProcessNewCategory entity);
        int UpdateNonstrictModifiedOnly(TDataProcessNewCategory entity);
        int UpdateByQuery(TDataProcessNewCategoryCB cb, TDataProcessNewCategory entity);
        int Delete(TDataProcessNewCategory entity);
        int DeleteNonstrict(TDataProcessNewCategory entity);
        int DeleteByQuery(TDataProcessNewCategoryCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
