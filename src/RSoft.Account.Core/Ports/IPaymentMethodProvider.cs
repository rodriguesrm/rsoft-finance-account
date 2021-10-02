using RSoft.Entry.Core.Entities;
using RSoft.Lib.Design.Infra.Data;
using System;

namespace RSoft.Entry.Core.Ports
{

    /// <summary>
    /// Payment method provider ports contract
    /// </summary>
    public interface IPaymentMethodProvider : IRepositoryBase<PaymentMethod, Guid>
    {
    }
}