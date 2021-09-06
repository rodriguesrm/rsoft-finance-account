using RSoft.Account.Core.Entities;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Account.Core.Ports
{

    /// <summary>
    /// Transaction domain service interface
    /// </summary>
    public interface ITransactionDomainService : IDomainServiceBase<Transaction, Guid>
    {
    }

}