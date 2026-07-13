
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
    [Bean(typeof(TColorInfoDetailCross))]
    public partial interface TColorInfoDetailCrossDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TColorInfoDetailCrossCB cb);
        IList<TColorInfoDetailCross> SelectList(TColorInfoDetailCrossCB cb);

        int Insert(TColorInfoDetailCross entity);
        int UpdateModifiedOnly(TColorInfoDetailCross entity);
        int UpdateNonstrictModifiedOnly(TColorInfoDetailCross entity);
        int UpdateByQuery(TColorInfoDetailCrossCB cb, TColorInfoDetailCross entity);
        int Delete(TColorInfoDetailCross entity);
        int DeleteNonstrict(TColorInfoDetailCross entity);
        int DeleteByQuery(TColorInfoDetailCrossCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
