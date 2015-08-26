using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;

namespace Simon8029.EMPDemo.IRepository
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// // 根据条件查询
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <returns>查询结果</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 根据条件查询 并且 排序
        /// </summary>
        /// <typeparam name="TKey">排序字段类型（可以不写，通过编译器类型推断出来）</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">第一个排序条件</param>
        /// <returns></returns>
        IQueryable<TEntity> Get<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderBy, bool isAsc = true);

        /// <summary>
        /// 修改指定实体的指定属性
        /// </summary>
        /// <param name="entity">待修改的实体对象</param>
        /// <param name="properties">待修改的属性</param>
        void Update(TEntity entity, params string[] properties);

        /// <summary>
        /// 根据条件批量修改实体的属性值
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="newValues">新的属性值</param>
        void UpdateBy(Expression<Func<TEntity, bool>> whereExpression, string[] propertyNames, object[] newValues);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">待增加的实体对象</param>
        void Add(TEntity entity);

        /// <summary>
        /// 删除指定实体对象
        /// </summary>
        /// <param name="entity">待删除的实体对象</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="whereExpression">删除实体的查询条件</param>
        void Delete(Expression<Func<TEntity, bool>> whereExpression);

        EasyUIModel_PageData<TEntity> GetWithPagination<TKey>(int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TKey>> orderByExpression,
            bool isAsc = true);

        EasyUIModel_PageData<TEntity> GetWithPaginationAndNavigationProperty<TKey>(int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TKey>> orderByExpression,
            bool isAsc = true, params string[] navigationPropertyName);
    }
}
