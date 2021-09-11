using RSoft.Lib.Common.Contracts.Dtos;
using RSoft.Lib.Common.Dtos;
using RSoft.Lib.Common.Models;
using System;

namespace RSoft.Account.Contracts.Models
{

    /// <summary>
    /// Transaction data transport object
    /// </summary>
    public class TransactionDto : AppDtoIdCreatedAuthorBase<Guid>, ICreatedAuthorDto<Guid>
    {

        #region Properties

        /// <summary>
        /// Transaction year
        /// </summary>
        public int Year { get { return Date.Year; } }

        /// <summary>
        /// Transaction month
        /// </summary>
        public int Month { get { return Date.Month; } }

        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Transaction type
        /// </summary>
        public SimpleIdentification<int> TransactionType { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Transaction Comments/Annotations
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Account detail
        /// </summary>
        public SimpleIdentification<Guid> Account { get; set; }

        /// <summary>
        /// Payment method detail
        /// </summary>
        public SimpleIdentification<Guid> PaymentMethod { get; set; }

        #endregion

    }

}