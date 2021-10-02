using RSoft.Entry.Core.Entities;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Entry.Core.Ports
{

    /// <summary>
    /// Category domain service interface
    /// </summary>
    public interface ICategoryDomainService : IDomainServiceBase<Category, Guid>
    {
    }

}