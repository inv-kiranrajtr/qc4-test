
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
    [Bean(typeof(TOutputTemplateMaster))]
    public partial interface TOutputTemplateMasterDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputTemplateMasterCB cb);
        IList<TOutputTemplateMaster> SelectList(TOutputTemplateMasterCB cb);

        int Insert(TOutputTemplateMaster entity);
        int UpdateModifiedOnly(TOutputTemplateMaster entity);
        int UpdateNonstrictModifiedOnly(TOutputTemplateMaster entity);
        int UpdateByQuery(TOutputTemplateMasterCB cb, TOutputTemplateMaster entity);
        int Delete(TOutputTemplateMaster entity);
        int DeleteNonstrict(TOutputTemplateMaster entity);
        int DeleteByQuery(TOutputTemplateMasterCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
