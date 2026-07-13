
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
    [Bean(typeof(TOutputSubFa))]
    public partial interface TOutputSubFaDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputSubFaCB cb);
        IList<TOutputSubFa> SelectList(TOutputSubFaCB cb);

        int Insert(TOutputSubFa entity);
        int UpdateModifiedOnly(TOutputSubFa entity);
        int UpdateNonstrictModifiedOnly(TOutputSubFa entity);
        int UpdateByQuery(TOutputSubFaCB cb, TOutputSubFa entity);
        int Delete(TOutputSubFa entity);
        int DeleteNonstrict(TOutputSubFa entity);
        int DeleteByQuery(TOutputSubFaCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
