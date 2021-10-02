using RSoft.Entry.Core.Entities;
using RSoft.Lib.Design.Infra.Data;
using System;

namespace RSoft.Entry.Core.Ports
{

    /// <summary>
    /// Category provider ports contract
    /// </summary>
    public interface ICategoryProvider: IRepositoryBase<Category, Guid>
    {
    }
}