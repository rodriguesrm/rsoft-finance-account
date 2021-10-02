using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Lib.Common.Abstractions;
using RSoft.Lib.Design.Domain.Entities;
using System;
using RSoft.Lib.Common.Contracts;
using RSoft.Finance.Contracts.Enum;

namespace RSoft.Account.Core.Entities
{

    /// <summary>
    /// Transaction entity
    /// </summary>
    public class Transaction : EntityIdCreatedAuthorBase<Guid, Transaction>
    {

        #region Constructors

        /// <summary>
        /// Create a new Transaction instance
        /// </summary>
        public Transaction() : base(Guid.NewGuid())
        {
            Initialize();
        }

        /// <summary>
        /// Create a new Transaction instance
        /// </summary>
        /// <param name="id">Account id value</param>
        public Transaction(Guid id) : base(id)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new Transaction instance
        /// </summary>
        /// <param name="id">Account id text</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.FormatException"></exception>
        /// <exception cref="System.OverflowException"></exception>
        public Transaction(string id) : base()
        {
            Id = new Guid(id);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Transaction year
        /// </summary>
        public int Year { get { return Date?.Year ?? 0; } }

        /// <summary>
        /// Transaction month
        /// </summary>
        public int Month { get { return Date?.Month ?? 0; }  }

        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Transaction type
        /// </summary>
        public TransactionTypeEnum? TransactionType { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Transaction Comments/Annotations
        /// </summary>
        public string Comment { get; set; }

        #endregion

        #region Navigation/Lazy

        /// <summary>
        /// Account data
        /// </summary>
        public Entry Entries { get; set; }

        /// <summary>
        /// Payment method data
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        #endregion

        #region Local Methods

        /// <summary>
        /// Iniatialize objects/properties/fields with default values
        /// </summary>
        private void Initialize()
        {
            CreatedOn = DateTime.UtcNow;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Validate entity
        /// </summary>
        public override void Validate()
        {
            IStringLocalizer<Transaction> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<Transaction>>();
            if (CreatedAuthor != null) AddNotifications(CreatedAuthor.Notifications);

            if (Amount <= 0)
                AddNotification(nameof(Amount), localizer["GREATER_THAN_ZERO"]);

            AddNotifications(new PastDateValidationContract(Date, nameof(Date), localizer["DATE_REQUIRED"]).Contract.Notifications);

            int? transactionType = TransactionType.HasValue ? (int)TransactionType : null;
            AddNotifications(new EnumCastFromIntegerValidationContract<TransactionTypeEnum>(transactionType, nameof(transactionType), true).Contract.Notifications);

            AddNotifications(new RequiredValidationContract<Guid?>(Entries?.Id, nameof(Entries), localizer["ACCOUNT_REQUIRED"]).Contract.Notifications);
            AddNotifications(new RequiredValidationContract<Guid?>(PaymentMethod?.Id, nameof(PaymentMethod), localizer["PAYMENTMETHOD_REQUIRED"]).Contract.Notifications);
        }

        #endregion

    }
}
