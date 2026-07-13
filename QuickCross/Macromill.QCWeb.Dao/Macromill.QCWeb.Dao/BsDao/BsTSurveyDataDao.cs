
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
    [Bean(typeof(TSurveyData))]
    public partial interface TSurveyDataDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TSurveyDataCB cb);
        IList<TSurveyData> SelectList(TSurveyDataCB cb);

        int Insert(TSurveyData entity);
        int UpdateModifiedOnly(TSurveyData entity);
        int UpdateNonstrictModifiedOnly(TSurveyData entity);
        int UpdateByQuery(TSurveyDataCB cb, TSurveyData entity);
        int Delete(TSurveyData entity);
        int DeleteNonstrict(TSurveyData entity);
        int DeleteByQuery(TSurveyDataCB cb);// {DBFlute-0.7.9}
    }
}
