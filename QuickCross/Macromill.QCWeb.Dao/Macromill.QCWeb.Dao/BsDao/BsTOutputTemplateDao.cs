
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
    [Bean(typeof(TOutputTemplate))]
    public partial interface TOutputTemplateDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputTemplateCB cb);
        IList<TOutputTemplate> SelectList(TOutputTemplateCB cb);

        int Insert(TOutputTemplate entity);
        int UpdateModifiedOnly(TOutputTemplate entity);
        int UpdateNonstrictModifiedOnly(TOutputTemplate entity);
        int UpdateByQuery(TOutputTemplateCB cb, TOutputTemplate entity);
        int Delete(TOutputTemplate entity);
        int DeleteNonstrict(TOutputTemplate entity);
        int DeleteByQuery(TOutputTemplateCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
