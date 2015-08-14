using System.Collections.Generic;

namespace Simon8029.EMPDemo.Model.ModelsForEasyUI
{
    /// <summary>
    ///     easyUI分页数据模型
    /// </summary>
    public class EasyUIModel_PageData<TEntity>
    {
        public int total { get; set; } //数据的总行数，total为eazyUI的总行数关键字，不可变
        public List<TEntity> rows { get; set; } //当前页数据集合，rows为eazyUI的当前页数据集合关键字，不可变
    }
}