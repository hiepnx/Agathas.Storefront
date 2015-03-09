using Agathas.Storefront.Infrastructure.Querying;
using Agathas.Storefront.Infrastructure.Specification;
using Agathas.Storefront.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;


namespace Agathas.Storefront.Infrastructure.Domain
{
    public interface IReadOnlyRepository<T, TId> where T : IAggregateRoot 
    {
        T FindBy(TId id);
        IEnumerable<T> FindAll();

        #region implemented by NHibernate
        IEnumerable<T> FindBy(Query query);
        IEnumerable<T> FindBy(Query query, int index, int count);
        #endregion

        #region implemented by EntityFramework
        /// <summary>
        /// Gets entity by key.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="keyValue">The key value.</param>
        /// <returns></returns>
        T GetByKey(object keyValue);

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <returns></returns>
        IQueryable<T> GetQuery();

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IQueryable<T> GetQuery(ISpecification<T> criteria);

        /// <summary>
        /// Gets one entity based on matching criteria
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Gets single entity using specification
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T Single(ISpecification<T> criteria);

        /// <summary>
        /// Firsts the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets first entity with specification.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T First(ISpecification<T> criteria);

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IEnumerable<T> Find(ISpecification<T> criteria);

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T FindOne(ISpecification<T> criteria);

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T FindOne(Expression<Func<T, bool>> criteria);

        
        /// <summary>
        /// Gets the specified order by.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<T> Get<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);

        /// <summary>
        /// Gets the specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<T> Get<TOrderBy>(Expression<Func<T, bool>> criteria, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);

        /// <summary>
        /// Gets entities which satifies a specification.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<T> Get<TOrderBy>(ISpecification<T> specification, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);

        /// <summary>
        /// Counts the specified entities.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <returns></returns>
         int Count();

        /// <summary>
        /// Counts entities with the specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Counts entities satifying specification.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count(ISpecification<T> criteria);

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        IEFUnitOfWork UnitOfWork { get; }
        #endregion
    }
}
