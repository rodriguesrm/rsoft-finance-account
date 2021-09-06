using DomainAccount = RSoft.Account.Core.Entities.Account;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Account.Core.Ports
{

    /// <summary>
    /// Account domain service interface
    /// </summary>
    public interface IAccountDomainService : IDomainServiceBase<DomainAccount, Guid>
    {
    }

}