using RSoft.Finance.Contracts.Enum;
using System;

namespace RSoft.Entry.Core.Ports
{

    /// <summary>
    /// List transaction filter arguments contract interface
    /// </summary>
    public interface IListTransactionFilter
    {

        #region Properties

        /// <summary>
        /// Period start date
        /// </summary>
        DateTime? StartAt { get; }
        
        /// <summary>
        /// Period end date
        /// </summary>
        DateTime? EndAt { get; }

        /// <summary>
        /// Period year dates
        /// </summary>
        int? Year { get; }

        /// <summary>
        /// Period month date
        /// </summary>
        int? Month { get; }
        
        /// <summary>
        /// Entry id
        /// </summary>
        Guid? EntryId { get; }
        
        /// <summary>
        /// Transaction type
        /// </summary>
        TransactionTypeEnum? TransactionType { get; }
        
        /// <summary>
        /// Payment method id
        /// </summary>
        Guid? PaymentMethodId { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Valid status flag
        /// </summary>
        public bool IsValid()
        {
            bool valid =
                (StartAt.HasValue && EndAt.HasValue) ||
                (Year.HasValue && (Month.HasValue && Month.Value >= 1 && Month.Value <= 12)) ||
                (EntryId.HasValue) ||
                (TransactionType.HasValue) ||
                (PaymentMethodId.HasValue);

            if (valid)
            {
                if ((StartAt.HasValue || EndAt.HasValue) && (Year.HasValue || Month.HasValue))
                    valid = false;
            }
            return valid;
        }

        #endregion
    }
}
