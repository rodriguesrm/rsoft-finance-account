using RSoft.Account.Core.Ports;
using RSoft.Account.Infra.Extensions;
using RSoft.Account.Infra.Tables;
using RSoft.Lib.Design.Infra.Data;
using System;
using PaymentMethodDomain = RSoft.Account.Core.Entities.PaymentMethod;

namespace RSoft.Account.Infra.Providers
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
