
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
    [Bean(typeof(TQcwebSurveyDetail))]
    public partial interface TQcwebSurveyDetailDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TQcwebSurveyDetailCB cb);
        IList<TQcwebSurveyDetail> SelectList(TQcwebSurveyDetailCB cb);

        int Insert(TQcwebSurveyDetail entity);
        int UpdateModifiedOnly(TQcwebSurveyDetail entity);
        int UpdateNonstrictModifiedOnly(TQcwebSurveyDetail entity);
        int UpdateByQuery(TQcwebSurveyDetailCB cb, TQcwebSurveyDetail entity);
        int Delete(TQcwebSurveyDetail entity);
        int DeleteNonstrict(TQcwebSurveyDetail entity);
        int DeleteByQuery(TQcwebSurveyDetailCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
