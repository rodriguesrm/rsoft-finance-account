using RSoft.Account.Core.Entities;
using RSoft.Lib.Design.Infra.Data;
using System;

namespace RSoft.Account.Core.Ports
{

    /// <summary>
    /// Payment method provider ports contract
    /// </summary>
    public interface IPaymentMethodProvider : IRepositoryBase<PaymentMethod, Guid>
    {
    }
}