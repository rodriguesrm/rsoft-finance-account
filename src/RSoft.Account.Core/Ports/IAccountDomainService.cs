using EntryAccount = RSoft.Account.Core.Entities.Entry;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Account.Core.Ports
{

    /// <summary>
    /// Account domain service interface
    /// </summary>
    public interface IAccountDomainService : IDomainServiceBase<EntryAccount, Guid>
    {
    }

}