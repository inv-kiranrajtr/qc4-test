
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
    [Bean(typeof(TQcwebSurveyInfo))]
    public partial interface TQcwebSurveyInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TQcwebSurveyInfoCB cb);
        IList<TQcwebSurveyInfo> SelectList(TQcwebSurveyInfoCB cb);

        int Insert(TQcwebSurveyInfo entity);
        int UpdateModifiedOnly(TQcwebSurveyInfo entity);
        int UpdateNonstrictModifiedOnly(TQcwebSurveyInfo entity);
        int UpdateByQuery(TQcwebSurveyInfoCB cb, TQcwebSurveyInfo entity);
        int Delete(TQcwebSurveyInfo entity);
        int DeleteNonstrict(TQcwebSurveyInfo entity);
        int DeleteByQuery(TQcwebSurveyInfoCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
