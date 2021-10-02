using RSoft.Lib.Common.Contracts.Dtos;
using RSoft.Lib.Common.Dtos;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Models
{

    /// <summary>
    /// Category data transport object
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class CategoryDto : AppDtoIdAuditBase<Guid>, IAuditDto<Guid>
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

        #endregion

    }

}
