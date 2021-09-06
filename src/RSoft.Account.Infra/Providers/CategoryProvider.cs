using RSoft.Account.Core.Ports;
using RSoft.Account.Infra.Extensions;
using RSoft.Account.Infra.Tables;
using RSoft.Lib.Design.Infra.Data;
using System;
using CategoryDomain = RSoft.Account.Core.Entities.Category;

namespace RSoft.Account.Infra.Providers
{

    /// <summary>
    /// Category provider
    /// </summary>
    public class CategoryProvider : RepositoryBase<CategoryDomain, Category, Guid>, ICategoryProvider
    {

        #region Constructors

        ///<inheritdoc/>
        public CategoryProvider(AccountContext ctx) : base(ctx) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override CategoryDomain Map(Category table)
            => table.Map();

        ///<inheritdoc/>
        protected override Category MapForAdd(CategoryDomain entity)
            => entity.Map();

        ///<inheritdoc/>
        protected override Category MapForUpdate(CategoryDomain entity, Category table)
            => entity.Map(table);

        #endregion

    }
}
