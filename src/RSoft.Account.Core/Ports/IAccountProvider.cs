using RSoft.Lib.Design.Infra.Data;
using System;

namespace RSoft.Account.Core.Ports
{

    public interface IAccountProvider : IRepositoryBase<Entities.Account, Guid>
    {
    }
}