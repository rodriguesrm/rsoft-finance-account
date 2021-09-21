using RSoft.Account.Core.Entities;
using RSoft.Lib.Design.Infra.Data;
using System;

namespace RSoft.Account.Core.Ports
{

    /// <summary>
    /// Category provider ports contract
    /// </summary>
    public interface ICategoryProvider: IRepositoryBase<Category, Guid>
    {
    }
}