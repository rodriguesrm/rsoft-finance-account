using RSoft.Entry.Core.Ports;
using RSoft.Entry.Infra.Extensions;
using RSoft.Entry.Infra.Tables;
using RSoft.Lib.Design.Infra.Data;
using System;
using PaymentMethodDomain = RSoft.Entry.Core.Entities.PaymentMethod;

namespace RSoft.Entry.Infra.Providers
{

    /// <summary>
    /// PaymentMethod provider
    /// </summary>
    public class PaymentMethodProvider : RepositoryBase<PaymentMethodDomain, PaymentMethod, Guid>, IPaymentMethodProvider
    {

        #region Constructors

        ///<inheritdoc/>
        public PaymentMethodProvider(AccountContext ctx) : base(ctx) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override PaymentMethodDomain Map(PaymentMethod table)
            => table.Map();

        ///<inheritdoc/>
        protected override PaymentMethod MapForAdd(PaymentMethodDomain entity)
            => entity.Map();

        ///<inheritdoc/>
        protected override PaymentMethod MapForUpdate(PaymentMethodDomain entity, PaymentMethod table)
            => entity.Map(table);

        #endregion

    }
}
