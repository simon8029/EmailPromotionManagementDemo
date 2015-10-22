using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Simon8029.EMPDemo.IRepository;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;

namespace Simon8029.EMPDemo.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        
        private readonly DbContext _dbContext = DbContextFactory.GetDbContext();
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository()
        {
            _dbContext.Configuration.ValidateOnSaveEnabled = false;
            //在构造函数中实例化dbSet，通过EF容器对象获取一个DbSet<TEntity>，用来操作与TEntity实体类对应的数据表
            _dbSet = _dbContext.Set<TEntity>();

           }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _dbSet.Where(whereExpression);
        }

        public IQueryable<TEntity> Get<TKey>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TKey>> orderBy, bool isAsc = true)
        {
            if (isAsc)
            {
                return _dbSet.Where(where).OrderBy(orderBy);
            }
            else
            {
                return _dbSet.Where(where).OrderByDescending(orderBy);
            }
        }

        /// <summary>
        /// 修改指定实体的指定属性值
        /// </summary>
        /// <param name="entity">要修改的实体对象</param>
        /// <param name="properties">要修改的属性名数组</param>
        public void Update(TEntity entity, params string[] properties)
        {
            DbEntityEntry entry = _dbContext.Entry(entity);//将实体对象添加到EF容器中，并返回代理类对象的一个指示器对象
            entry.State=EntityState.Unchanged;//手动将代理对象里的state状态改为Unchanged，因为默认是Detached，不能直接修改IsModified属性
            foreach (var property in properties)//循环要修改的实体类属性名
            {
                entry.Property(property).IsModified = true;//设置实体类对象的属性为已修改状态。注意：实体对象的第一次属性被修改时，state属性被自动设置为Modified
            }
        }

        public void UpdateBy(Expression<Func<TEntity, bool>> whereExpression, string[] propertyNames, object[] newValues)
        {
            if (propertyNames==null||newValues==null)
            {
                throw new ArgumentException("Property name or new value can not be null.");
            }

            var list = _dbSet.Where(whereExpression);//查询要修改的实体对象集合
            Type type = typeof (TEntity);//获取要修改的实体类型的类型对象
            foreach (var entity in list)//遍历要修改的实体对象
            {
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    PropertyInfo propertyInfo = type.GetProperty(propertyNames[i]);//获取要修改的属性对象
                    propertyInfo.SetValue(entity,newValues[i],null);//修改实体对象里的属性值
                }
            }
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            var list = _dbSet.Where(whereExpression);//查询出的对象已经存入EF容器了，但state都是unchanged
            foreach (var entity in list)
            {
                _dbSet.Remove(entity);//将EF容器里的相应对象的state都改成Deleted
            }
        }

        public EasyUIModel_PageData<TEntity> GetWithPagination<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TKey>> orderByExpression, bool isAsc = true)
        {
            var list = _dbSet.Where(whereExpression);
            IOrderedQueryable<TEntity> orderedList = null;
            orderedList = isAsc ? list.OrderBy(orderByExpression) : list.OrderByDescending(orderByExpression);

            EasyUIModel_PageData<TEntity> pageData = new EasyUIModel_PageData<TEntity>
            {
                rows = orderedList.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList(),
                total = orderedList.Count()
            };

            return pageData;
        }

        public EasyUIModel_PageData<TEntity> GetWithPaginationAndNavigationProperty<TKey>(int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TKey>> orderByExpression, bool isAsc = true, params string[] navigationPropertyName)
        {
            //dbSet.Include("导航属性名1").Include("导航属性名2").Include("导航属性名3").Where(where).OrderBy(orderBy).Skip(0).Take(10);
            //dbSet.Where(where).OrderBy(orderBy).Skip(0).Take(10);
            DbQuery<TEntity> dbQuery = _dbSet;
            //循环 加入 要连接查询的 导航属性名称
            foreach (string includeN in navigationPropertyName)
            {
                dbQuery = dbQuery.Include(includeN);
            }

            var dbSetWhered = dbQuery.Where(whereExpression);
            IOrderedQueryable<TEntity> orderWhere = null;
            if (isAsc)
                orderWhere = dbSetWhered.OrderBy(orderByExpression);
            else
                orderWhere = dbSetWhered.OrderByDescending(orderByExpression);

            EasyUIModel_PageData<TEntity>  pageData = new EasyUIModel_PageData<TEntity>();
            pageData.rows = orderWhere.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            pageData.total = dbSetWhered.Count();

            return pageData;
        }

        #region Validation Entity ID: public void ValidateEntityId(int entityId)
        public void ValidateEntityId(int entityId)
        {
            if (entityId < 1)
            {
                throw new ArgumentNullException("entityId", "Entity ID must be greater than 0");
            }
        }
        #endregion

        #region Validation Entity is not Null: public void ValidateNull<TEntity>(TEntity entity)
        public void ValidateNull<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(TEntity).ToString() + "entity", "Entity should not be null.");
            }
        }

        #endregion
    }
}
