
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
    [Bean(typeof(TColorSetInfoCross))]
    public partial interface TColorSetInfoCrossDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TColorSetInfoCrossCB cb);
        IList<TColorSetInfoCross> SelectList(TColorSetInfoCrossCB cb);

        int Insert(TColorSetInfoCross entity);
        int UpdateModifiedOnly(TColorSetInfoCross entity);
        int UpdateNonstrictModifiedOnly(TColorSetInfoCross entity);
        int UpdateByQuery(TColorSetInfoCrossCB cb, TColorSetInfoCross entity);
        int Delete(TColorSetInfoCross entity);
        int DeleteNonstrict(TColorSetInfoCross entity);
        int DeleteByQuery(TColorSetInfoCrossCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
