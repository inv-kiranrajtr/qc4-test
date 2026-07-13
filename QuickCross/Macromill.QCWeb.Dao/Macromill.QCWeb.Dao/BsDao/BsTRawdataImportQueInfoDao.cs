
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
    [Bean(typeof(TRawdataImportQueInfo))]
    public partial interface TRawdataImportQueInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TRawdataImportQueInfoCB cb);
        IList<TRawdataImportQueInfo> SelectList(TRawdataImportQueInfoCB cb);

        int Insert(TRawdataImportQueInfo entity);
        int UpdateModifiedOnly(TRawdataImportQueInfo entity);
        int UpdateNonstrictModifiedOnly(TRawdataImportQueInfo entity);
        int UpdateByQuery(TRawdataImportQueInfoCB cb, TRawdataImportQueInfo entity);
        int Delete(TRawdataImportQueInfo entity);
        int DeleteNonstrict(TRawdataImportQueInfo entity);
        int DeleteByQuery(TRawdataImportQueInfoCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
