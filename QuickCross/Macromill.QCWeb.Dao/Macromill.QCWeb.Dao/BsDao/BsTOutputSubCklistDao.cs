
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
    [Bean(typeof(TOutputSubCklist))]
    public partial interface TOutputSubCklistDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputSubCklistCB cb);
        IList<TOutputSubCklist> SelectList(TOutputSubCklistCB cb);

        int Insert(TOutputSubCklist entity);
        int UpdateModifiedOnly(TOutputSubCklist entity);
        int UpdateNonstrictModifiedOnly(TOutputSubCklist entity);
        int UpdateByQuery(TOutputSubCklistCB cb, TOutputSubCklist entity);
        int Delete(TOutputSubCklist entity);
        int DeleteNonstrict(TOutputSubCklist entity);
        int DeleteByQuery(TOutputSubCklistCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
