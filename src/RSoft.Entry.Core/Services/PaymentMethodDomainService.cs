using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Common.Contracts.Web;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Entry.Core.Services
{

    /// <summary>
    /// PaymentMethod domain service operations
    /// </summary>
    public class PaymentMethodDomainService : DomainServiceBase<PaymentMethod, Guid, IPaymentMethodProvider>, IPaymentMethodDomainService
    {

        #region Constructors

        /// <summary>
        /// Create a new scopde domain service instance
        /// </summary>
        /// <param name="provider">PaymentMethod provier</param>
        /// <param name="authenticatedPaymentMethod">Authenticated PaymentMethod object</param>
        public PaymentMethodDomainService(IPaymentMethodProvider provider, IAuthenticatedUser authenticatedPaymentMethod) : base(provider, authenticatedPaymentMethod) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        public override void PrepareSave(PaymentMethod entity, bool isUpdate) 
        {
            if (isUpdate)
            {
                entity.ChangedAuthor = new AuthorNullable<Guid>(_authenticatedUser.Id.Value, $"{_authenticatedUser.FirstName} {_authenticatedUser.LastName}");
                entity.ChangedOn = DateTime.UtcNow;
            }
            else
            {
                entity.CreatedAuthor = new Author<Guid>(_authenticatedUser.Id.Value, $"{_authenticatedUser.FirstName} {_authenticatedUser.LastName}");
                entity.CreatedOn = DateTime.UtcNow;
            }
        }

        #endregion

    }
}
