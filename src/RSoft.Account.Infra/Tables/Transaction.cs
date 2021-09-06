using RSoft.Lib.Design.Infra.Data;
using RSoft.Lib.Design.Infra.Data.Tables;
using System;

namespace RSoft.Account.Infra.Tables
{

    /// <summary>
    /// Transaction table entity
    /// </summary>
    public class Transaction : TableIdCreatedAuthorBase<Guid, Transaction>, ITable, ICreatedAuthor<Guid>
    {

        #region Constructors

        /// <summary>
        /// Create a new table instance
        /// </summary>
        public Transaction() : base(Guid.NewGuid())
        {
            Initialize();
        }

        /// <summary>
        /// Create a new table instance
        /// </summary>
        /// <param name="id">User id value</param>
        public Transaction(Guid id) : base(id)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new table instance
        /// </summary>
        /// <param name="id">User id text</param>
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
        public short Year { get; set; }

        /// <summary>
        /// Transaction month
        /// </summary>
        public byte Month { get; set; }

        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction Comments/Annotations
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Account id
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Payment method id
        /// </summary>
        public Guid PaymentMethodId { get; set; }

        #endregion

        #region Navigation/Lazy

        /// <summary>
        /// Created author data
        /// </summary>
        public virtual User CreatedAuthor { get; set; }

        /// <summary>
        /// Account data
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Payment method data
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        #endregion

        #region Local methods

        /// <summary>
        /// Iniatialize objects/properties/fields with default values
        /// </summary>
        private void Initialize()
        {
            
        }

        #endregion

        #region Public methods

        #endregion

    }
}
