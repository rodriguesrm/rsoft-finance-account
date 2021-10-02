using RSoft.Entry.Core.Ports;
using RSoft.Finance.Contracts.Enum;
using System;

namespace RSoft.Entry.Application.Arguments
{

    /// <summary>
    /// List transaction filter arguments contract
    /// </summary>
    public class ListTransactionFilter : IListTransactionFilter
    {

        #region Local objects/variables

        private DateTime? _startAt = null;
        private DateTime? _endAt = null;

        #endregion

        #region Properties


        ///<inheritdoc/>
        public DateTime? StartAt 
        {
            get { return _startAt?.Date ?? null; }
            set { _startAt = value; } 
        }

        ///<inheritdoc/>
        public DateTime? EndAt 
        {
            get { return _endAt; }
            set 
            {
                _endAt = value;
                if (_endAt.HasValue)
                    _endAt = _endAt.Value.Date.AddDays(1).AddSeconds(-1);
            }
        }

        ///<inheritdoc/>
        public int? Year { get; set; }

        ///<inheritdoc/>
        public int? Month { get; set; }

        ///<inheritdoc/>
        public Guid? EntryId { get; set; }

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
