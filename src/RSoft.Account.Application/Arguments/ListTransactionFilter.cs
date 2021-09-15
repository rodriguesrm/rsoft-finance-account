using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Enum;
using System;

namespace RSoft.Account.Application.Arguments
{

    /// <summary>
    /// List transaction filter arguments contract
    /// </summary>
    public class ListTransactionFilter : IListTransactionFilter
    {

        #region Properties


        ///<inheritdoc/>
        public DateTime? StartAt { get; set; }

        ///<inheritdoc/>
        public DateTime? EndAt { get; set; }

        ///<inheritdoc/>
        public int? Year { get; set; }

        ///<inheritdoc/>
        public int? Month { get; set; }

        ///<inheritdoc/>
        public Guid? AccountId { get; set; }

        ///<inheritdoc/>
        public TransactionTypeEnum? TransactionType { get; set; }

        ///<inheritdoc/>
        public Guid? PaymentMethodId { get; set; }

        #endregion

        #region Public Methods

        ///<inheritdoc/>
        public bool IsValid()
        {
            bool valid =
                (StartAt.HasValue && EndAt.HasValue) ||
                (Year.HasValue || (Month.HasValue && Month.Value >= 1 && Month.Value <= 12)) ||
                (AccountId.HasValue) ||
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
