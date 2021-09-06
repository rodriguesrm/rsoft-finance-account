using RSoft.Lib.Common.Contracts.Entities;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Domain.Entities;
using System;

namespace RSoft.Account.Core.Entities
{

    /// <summary>
    /// User entity
    /// </summary>
    public class User : EntityIdBase<Guid, User>, IActive
    {

        #region Properties

        /// <summary>
        /// User full name
        /// </summary>
        public Name Name { get; set; }

        /// <summary>
        /// Indicate if entity is active
        /// </summary>
        public bool IsActive { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Validate entity
        /// </summary>
        public override void Validate()
        {
            AddNotifications(Name.Notifications);
        }

        #endregion

    }
}
