using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Agathas.Storefront.Infrastructure.Domain;

using Agathas.Storefront.Infrastructure.Specification;
using Agathas.Storefront.Repository.EntityFramework.UnitOfWork;

namespace Agathas.Storefront.Repository.EntityFramework.Repository
{
    public class Repository<T, TId> : IRepository<T,TId> where T :class, IAggregateRoot
    {
        
        private DbContext _context;
        private readonly PluralizationService _pluralizer = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en"));

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TEntity&gt;"/> class.
        /// </summary>
        public Repository()            
        {
        }
        

       
        public T GetByKey(object keyValue)
        {
            EntityKey key = GetEntityKey(keyValue);

            object originalItem;
            if (((IObjectContextAdapter)DbContext).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                return (T)originalItem;
            }
            return default(T);
        }

        public IQueryable<T> GetQuery()
        {
            /* 
             * From CTP4, I could always safely call this to return an IQueryable on DbContext 
             * then performed any with it without any problem:
             */
            // return DbContext.Set<TEntity>();

            /*
             * but with 4.1 release, when I call GetQuery<TEntity>().AsEnumerable(), there is an exception:
             * ... System.ObjectDisposedException : The ObjectContext instance has been disposed and can no longer be used for operations that require a connection.
             */

            // here is a work around: 
            // - cast DbContext to IObjectContextAdapter then get ObjectContext from it
            // - call CreateQuery<TEntity>(entityName) method on the ObjectContext
            // - perform querying on the returning IQueryable, and it works!
            var entityName = GetEntityName();
            return ((IObjectContextAdapter)DbContext).ObjectContext.CreateQuery<T>(entityName);
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate)
        {
            return GetQuery().Where(predicate);
        }

        public IQueryable<T> GetQuery(ISpecification<T> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery());
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate, int from, int count)
        {
            return GetQuery().Where(predicate).Skip(from).Take(count);
        }

        public IQueryable<T> GetQuery(ISpecification<T> criteria, int from, int count)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery()).Skip(from).Take(count);
        }

        public IEnumerable<T> Get<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery().OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery().OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<T> Get<TOrderBy>(Expression<Func<T, bool>> criteria, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery(criteria).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery(criteria).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<T> Get<TOrderBy>(ISpecification<T> specification, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return specification.SatisfyingEntitiesFrom(GetQuery()).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return specification.SatisfyingEntitiesFrom(GetQuery()).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public T Single(Expression<Func<T, bool>> criteria) 
        {
            return GetQuery().Single(criteria);
        }

        public T Single(ISpecification<T> criteria)
        {
            return criteria.SatisfyingEntityFrom(GetQuery());
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return GetQuery().First(predicate);
        }

        public T First(ISpecification<T> criteria) 
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery()).First();
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<T>().Add(entity);
        }

        public void Attach(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<T>().Attach(entity);
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<T>().Remove(entity);
        }

        public void Remove(Expression<Func<T, bool>> criteria)
        {
            IEnumerable<T> records = Find(criteria);

            foreach (T record in records)
            {
                Remove(record);
            }
        }

        public void Remove(ISpecification<T> criteria) 
        {
            IEnumerable<T> records = Find(criteria);
            foreach (T record in records)
            {
                Remove(record);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return GetQuery().AsEnumerable();
        }

        public void Save(T entity)
        {
            Add(entity);
            DbContext.SaveChanges();
           
        }
        
        public void Update(T entity)
        {
            var fqen = GetEntityName();

            object originalItem;
            EntityKey key = ((IObjectContextAdapter)DbContext).ObjectContext.CreateEntityKey(fqen, entity);
            if (((IObjectContextAdapter)DbContext).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                ((IObjectContextAdapter)DbContext).ObjectContext.ApplyCurrentValues(key.EntitySetName, entity);
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> criteria) 
        {
            return GetQuery().Where(criteria);
        }

        public T FindOne(Expression<Func<T, bool>> criteria)
        {
            return GetQuery().Where(criteria).FirstOrDefault();
        }

        public T FindOne(ISpecification<T> criteria)
        {
            return criteria.SatisfyingEntityFrom(GetQuery());
        }

        public IEnumerable<T> Find(ISpecification<T> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery()).AsEnumerable();
        }

        public int Count() 
        {
            return GetQuery().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return GetQuery().Count(criteria);
        }

        public int Count(ISpecification<T> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery()).Count();
        }

        public IEFUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new EFUnitOfWork(this.DbContext);
                }
                return unitOfWork;
            }
        }

        private EntityKey GetEntityKey(object keyValue) 
        {
            var entitySetName = GetEntityName();
            var objectSet = ((IObjectContextAdapter)DbContext).ObjectContext.CreateObjectSet<T>();
            var keyPropertyName = objectSet.EntitySet.ElementType.KeyMembers[0].ToString();
            var entityKey = new EntityKey(entitySetName, new[] { new EntityKeyMember(keyPropertyName, keyValue) });
            return entityKey;
        }

        private string GetEntityName()
        {
            return string.Format("{0}.{1}", ((IObjectContextAdapter)DbContext).ObjectContext.DefaultContainerName, _pluralizer.Pluralize(typeof(T).Name));
        }

        private DbContext DbContext
        {
            get
            {
                if (this._context == null)
                {
                  this._context = DbContextFactory.GetDataContext();
                }
                return this._context;
            }
        }       

        private IEFUnitOfWork unitOfWork;
        

        #region implemented by NHibernate
        public T FindBy(TId id)
        {
            return this.GetByKey(id);
        }
        public IEnumerable<T> FindAll()
        {
            return this.GetAll();
        }
        
        #endregion
    }
}
