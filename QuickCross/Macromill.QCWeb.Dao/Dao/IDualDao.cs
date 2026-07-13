using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seasar.Dao.Attrs;
using Seasar.Quill.Attrs;
using Macromill.QCWeb.Dao.HelloDBEntity;

namespace Macromill.QCWeb.Dao
{
    /// <summary>
    /// Dualテーブルを扱うDao
    /// </summary>
    [S2Dao]
    [Bean(typeof(Dual))]
    [Implementation]
    public interface IDualDao
    {
        /// <summary>
        /// 全て検索する。
        /// </summary>
        /// <returns></returns>
        List<Dual> GetAll();
        
        /// <summary>
        /// レコード件数を取得する
        /// </summary>
        /// <returns></returns>
        int selectCount();
    }
}
