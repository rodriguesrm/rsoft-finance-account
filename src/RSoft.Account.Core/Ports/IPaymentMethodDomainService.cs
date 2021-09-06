using RSoft.Account.Core.Entities;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Account.Core.Ports
{

    /// <summary>
    /// PaymentMethod domain service interface
    /// </summary>
    public interface IPaymentMethodDomainService : IDomainServiceBase<PaymentMethod, Guid>
    {
    }

}