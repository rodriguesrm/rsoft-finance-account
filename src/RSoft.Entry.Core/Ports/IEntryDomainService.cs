using DomainEntry = RSoft.Entry.Core.Entities.Entry;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Entry.Core.Ports
{

    /// <summary>
    /// Entry domain service interface
    /// </summary>
    public interface IEntryDomainService : IDomainServiceBase<DomainEntry, Guid>
    {
    }

}