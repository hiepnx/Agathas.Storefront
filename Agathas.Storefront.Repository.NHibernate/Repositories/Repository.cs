using System;
using System.Collections.Generic;
using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.UnitOfWork;
using Agathas.Storefront.Repository.NHibernate.SessionStorage;
using NHibernate;
using Agathas.Storefront.Infrastructure.Specification;
using System.Linq.Expressions;
using System.Linq;
using System.Data.SqlClient;

namespace Agathas.Storefront.Repository.NHibernate.Repositories
{
    public abstract class Repository<T, TEntityKey> where T : IAggregateRoot
    {
        private IUnitOfWork _uow;

        public Repository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(T entity)
        {
            SessionFactory.GetCurrentSession().Save(entity);
        }

        public void Remove(T entity)
        {
            SessionFactory.GetCurrentSession().Delete(entity);
        }

        public void Save(T entity)
        {
            SessionFactory.GetCurrentSession().SaveOrUpdate(entity);
        }

        public T FindBy(TEntityKey id)
        {
            return SessionFactory.GetCurrentSession().Get<T>(id);
        }

        public IEnumerable<T> FindAll()
        {
            ICriteria criteriaQuery =
                    SessionFactory.GetCurrentSession().CreateCriteria(typeof(T));

            return (List<T>)criteriaQuery.List<T>();
        }

        public IEnumerable<T> FindAll(int index, int count)
        {
            ICriteria criteriaQuery =
                      SessionFactory.GetCurrentSession().CreateCriteria(typeof(T));

            return (List<T>)criteriaQuery.SetFetchSize(count)
                                    .SetFirstResult(index).List<T>();
        }

        public IEnumerable<T> FindBy(Query query)
        {
            ICriteria criteriaQuery =
                     SessionFactory.GetCurrentSession().CreateCriteria(typeof(T));

            AppendCriteria(criteriaQuery);

            query.TranslateIntoNHQuery<T>(criteriaQuery);

            return criteriaQuery.List<T>();
        }

        public IEnumerable<T> FindBy(Query query, int index, int count)
        {
            ICriteria criteriaQuery =
                     SessionFactory.GetCurrentSession().CreateCriteria(typeof(T));

            AppendCriteria(criteriaQuery);

            query.TranslateIntoNHQuery<T>(criteriaQuery);

            return criteriaQuery.SetFetchSize(count).SetFirstResult(index).List<T>();
        }

        public virtual void AppendCriteria(ICriteria criteria)
        {

        }

     #region implemented by EF
        #region implemented by EntityFramework

        public T GetByKey(object keyValue)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public IQueryable<T> GetQuery()
        {
            throw new NotImplementedException("implemented by EF");
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public IQueryable<T> GetQuery(ISpecification<T> criteria)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public T Single(Expression<Func<T, bool>> criteria)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public T Single(ISpecification<T> criteria)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public T First(ISpecification<T> criteria)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public IEnumerable<T> Find(ISpecification<T> criteria) 
        {
            throw new NotImplementedException("implemented by EF");
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> criteria)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public T FindOne(ISpecification<T> criteria)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public T FindOne(Expression<Func<T, bool>> criteria)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public IEnumerable<T> Get<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public IEnumerable<T> Get<TOrderBy>(Expression<Func<T, bool>> criteria, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public IEnumerable<T> Get<TOrderBy>(ISpecification<T> specification, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public int Count()
        {
            throw new NotImplementedException("implemented by EF");
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            throw new NotImplementedException("implemented by EF");
        }

        public int Count(ISpecification<T> criteria)
        {
            throw new NotImplementedException("implemented by EF");
        }
        public IEFUnitOfWork UnitOfWork
        {
            get
            {
                return null;
            }
        }
        
        #endregion
#endregion

    }

}
