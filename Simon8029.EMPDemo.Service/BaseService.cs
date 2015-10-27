using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Simon8029.EMPDemo.IRepository;
using Simon8029.EMPDemo.IService;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;

namespace Simon8029.EMPDemo.Service
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        public IBaseRepository<TEntity> IbaseRepository = null; 

        public BaseService()
        {
            SetIBaseRepository();
        }

        public abstract void SetIBaseRepository();

        public IRepository.IDbSession DbSession
        {
            get { return DbSessionFactory.GetDbSession(); }
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression)
        {
            return IbaseRepository.Get(whereExpression);
        }

        public IQueryable<TEntity> Get<TKey>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TKey>> orderBy, bool isAsc = true)
        {
            return IbaseRepository.Get<TKey>(where, orderBy, isAsc);
        }

        public EasyUIModel_PageData<TEntity> GetWithPaginationAndNavigationProperty<TKey>(int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TKey>> orderByExpression, bool isAsc = true, params string[] navigationPropertyName)
        {
            return IbaseRepository.GetWithPaginationAndNavigationProperty <
                   TKey>(pageIndex, pageSize, whereExpression, orderByExpression, isAsc, navigationPropertyName);
        }

        public void Update(TEntity entity, params string[] properties)
        {
            IbaseRepository.Update(entity,properties);
        }

        public void UpdateBy(Expression<Func<TEntity, bool>> whereExpression, string[] propertyNames, object[] newValues)
        {
            IbaseRepository.UpdateBy(whereExpression,propertyNames,newValues);
        }

        public void Add(TEntity entity)
        {
            IbaseRepository.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            IbaseRepository.Delete(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            IbaseRepository.Delete(whereExpression);
        }

        public EasyUIModel_PageData<TEntity> GetWithPagination<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TKey>> orderByExpression, bool isAsc = true)
        {
            return IbaseRepository.GetWithPagination(pageIndex, pageSize, whereExpression, orderByExpression, isAsc);
        }
    }
}
