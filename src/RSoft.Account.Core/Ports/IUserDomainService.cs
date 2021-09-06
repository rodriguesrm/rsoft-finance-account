using RSoft.Account.Core.Entities;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Account.Core.Ports
{

    /// <summary>
    /// User domain service interface
    /// </summary>
    public interface IUserDomainService : IDomainServiceBase<User, Guid>
    {
    }

}