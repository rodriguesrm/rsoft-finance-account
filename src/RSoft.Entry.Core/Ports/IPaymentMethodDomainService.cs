using RSoft.Entry.Core.Entities;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Entry.Core.Ports
{

    /// <summary>
    /// PaymentMethod domain service interface
    /// </summary>
    public interface IPaymentMethodDomainService : IDomainServiceBase<PaymentMethod, Guid>
    {
    }

}