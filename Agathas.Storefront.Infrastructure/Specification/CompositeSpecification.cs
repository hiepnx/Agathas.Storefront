using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agathas.Storefront.Infrastructure.Specification
{
    public abstract class CompositeSpecification<TEntity> : ISpecification<TEntity>
    {
        /// <summary>
        /// The _left side.
        /// </summary>
        protected readonly Specification<TEntity> _leftSide;

        /// <summary>
        /// The _right side.
        /// </summary>
        protected readonly Specification<TEntity> _rightSide;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeSpecification{TEntity}"/> class.
        /// </summary>
        /// <param name="leftSide">
        /// The left side.
        /// </param>
        /// <param name="rightSide">
        /// The right side.
        /// </param>
        public CompositeSpecification(Specification<TEntity> leftSide, Specification<TEntity> rightSide)
        {
            this._leftSide = leftSide;
            this._rightSide = rightSide;
        }

        /// <summary>
        /// The satisfying entity from.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public abstract TEntity SatisfyingEntityFrom(IQueryable<TEntity> query);

        /// <summary>
        /// The satisfying entities from.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public abstract IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query);
    }
}
