using RSoft.Finance.Contracts.Enum;
using RSoft.Lib.Common.Contracts.Dtos;
using RSoft.Lib.Common.Dtos;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.Contracts.Models
{

    /// <summary>
    /// PaymentMethod data transport object
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class PaymentMethodDto : AppDtoIdAuditBase<Guid>, IAuditDto<Guid>
    {

        #region Properties

        /// <summary>
        /// Entity name value
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicate if entity is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Payment type code (number)
        /// </summary>
        public PaymentTypeEnum PaymentType { get; set; }

        #endregion

    }

}
