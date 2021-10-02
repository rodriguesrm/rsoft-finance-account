using RSoft.Lib.Common.Contracts;
using RSoft.Lib.Common.Contracts.Entities;
using RSoft.Lib.Design.Domain.Entities;
using System;
using System.Collections.Generic;
using RSoft.Finance.Contracts.Enum;

namespace RSoft.Entry.Core.Entities
{

    /// <summary>
    /// PaymentMethod entity
    /// </summary>
    public class PaymentMethod : EntityIdNameAuditBase<Guid, PaymentMethod>, IEntity, IAuditAuthor<Guid>, IActive
    {

        #region Constructors

        /// <summary>
        /// Create a new PaymentMethod instance
        /// </summary>
        public PaymentMethod() : base(Guid.NewGuid(), null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new PaymentMethod instance
        /// </summary>
        /// <param name="id">PaymentMethod id value</param>
        public PaymentMethod(Guid id) : base(id, null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new PaymentMethod instance
        /// </summary>
        /// <param name="id">PaymentMethod id text</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.FormatException"></exception>
        /// <exception cref="System.OverflowException"></exception>
        public PaymentMethod(string id) : base()
        {
            Id = new Guid(id);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Payment type
        /// </summary>
        public PaymentTypeEnum? PaymentType { get; set; }

        /// <summary>
        /// Indicate if entity is active
        /// </summary>
        public bool IsActive { get; set; }

        #endregion

        #region Local Methods

        /// <summary>
        /// Iniatialize objects/properties/fields with default values
        /// </summary>
        private void Initialize()
        {
            IsActive = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validate entity
        /// </summary>
        public override void Validate()
        {
            if (CreatedAuthor != null) AddNotifications(CreatedAuthor.Notifications);
            if (ChangedAuthor != null) AddNotifications(ChangedAuthor.Notifications);
            AddNotifications(new SimpleStringValidationContract(Name, nameof(Name), true, 3, 50).Contract.Notifications);

            int? paymentType = PaymentType.HasValue ? (int)PaymentType : null;
            AddNotifications(new EnumCastFromIntegerValidationContract<PaymentTypeEnum>(paymentType, nameof(paymentType), true).Contract.Notifications);
        }

        #endregion

    }

}
