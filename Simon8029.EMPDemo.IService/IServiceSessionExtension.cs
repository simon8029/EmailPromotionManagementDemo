using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simon8029.EMPDemo.IService
{
    /// <summary>
    /// 业务仓储 接口
    /// 目的：1.封装 各个业务子接口属性 方便外部调用
    ///      2.调用 数据仓储接口的 SaveChanges 帮数据层完成 数据批量提交
    /// </summary>
    public partial interface IServiceSession
    {
        /// <summary>
        /// 调用 数据仓储接口的 SaveChanges 帮数据层完成 数据批量提交
        /// </summary>
        /// <returns></returns>
        int SaveChange();
    }
}
