
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
    [Bean(typeof(TSurveyInfo))]
    public partial interface TSurveyInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TSurveyInfoCB cb);
        IList<TSurveyInfo> SelectList(TSurveyInfoCB cb);

        int Insert(TSurveyInfo entity);
        int UpdateModifiedOnly(TSurveyInfo entity);
        int UpdateNonstrictModifiedOnly(TSurveyInfo entity);
        int UpdateByQuery(TSurveyInfoCB cb, TSurveyInfo entity);
        int Delete(TSurveyInfo entity);
        int DeleteNonstrict(TSurveyInfo entity);
        int DeleteByQuery(TSurveyInfoCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
