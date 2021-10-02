using RSoft.Lib.Design.Infra.Data;
using System;

namespace RSoft.Entry.Core.Ports
{

    /// <summary>
    /// Account provider ports contract
    /// </summary>
    public interface IAccountProvider : IRepositoryBase<Entities.Entry, Guid>
    {
    }
}