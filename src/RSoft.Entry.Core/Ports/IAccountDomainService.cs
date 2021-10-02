using EntryAccount = RSoft.Entry.Core.Entities.Entry;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Entry.Core.Ports
{

    /// <summary>
    /// Account domain service interface
    /// </summary>
    public interface IAccountDomainService : IDomainServiceBase<EntryAccount, Guid>
    {
    }

}