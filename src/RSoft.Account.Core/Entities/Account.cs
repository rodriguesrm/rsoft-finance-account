using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Lib.Common.Abstractions;
using RSoft.Lib.Common.Contracts;
using RSoft.Lib.Common.Contracts.Entities;
using RSoft.Lib.Design.Domain.Entities;
using System;

namespace RSoft.Account.Core.Entities
{

    public class Account : EntityIdNameAuditBase<Guid, Account>, IEntity, IAuditAuthor<Guid>, IActive
    {

        #region Constructors

        /// <summary>
        /// Create a new Account instance
        /// </summary>
        public Account() : base(Guid.NewGuid(), null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new Account instance
        /// </summary>
        /// <param name="id">Account id value</param>
        public Account(Guid id) : base(id, null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new Account instance
        /// </summary>
        /// <param name="id">Account id text</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.FormatException"></exception>
        /// <exception cref="System.OverflowException"></exception>
        public Account(string id) : base()
        {
            Id = new Guid(id);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indicate if entity is active
        /// </summary>
        public bool IsActive { get; set; }

        #endregion

        #region Navigation/Lazy

        /// <summary>
        /// Category data
        /// </summary>
        public virtual Category Category { get; set; }

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
            IStringLocalizer<Account> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<Account>>();
            if (CreatedAuthor != null) AddNotifications(CreatedAuthor.Notifications);
            if (ChangedAuthor != null) AddNotifications(ChangedAuthor.Notifications);
            AddNotifications(new SimpleStringValidationContract(Name, nameof(Name), true, 3, 100).Contract.Notifications);
            AddNotifications(new RequiredValidationContract<string>(Category?.Name, nameof(Category), localizer["CATEGORY_REQUIRED"]).Contract.Notifications);
        }

        #endregion

    }
}
