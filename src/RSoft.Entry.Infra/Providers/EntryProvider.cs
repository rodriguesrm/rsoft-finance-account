using RSoft.Entry.Core.Ports;
using RSoft.Entry.Infra.Extensions;
using RSoft.Lib.Design.Infra.Data;
using System;
using EntryDomain = RSoft.Entry.Core.Entities.Entry;
using EntryTable = RSoft.Entry.Infra.Tables.Entry;

namespace RSoft.Entry.Infra.Providers
{

    /// <summary>
    /// Account provider
    /// </summary>
    public class EntryProvider : RepositoryBase<EntryDomain, EntryTable, Guid>, IEntryProvider
    {

        #region Constructors

        ///<inheritdoc/>
        public EntryProvider(AccountContext ctx) : base(ctx) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override EntryDomain Map(EntryTable table)
            => table.Map();

        ///<inheritdoc/>
        protected override EntryTable MapForAdd(EntryDomain entity)
            => entity.Map();

        ///<inheritdoc/>
        protected override EntryTable MapForUpdate(EntryDomain entity, EntryTable table)
            => entity.Map(table);

        #endregion

    }
}
