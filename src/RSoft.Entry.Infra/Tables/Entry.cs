using RSoft.Lib.Common.Contracts.Entities;
using RSoft.Lib.Design.Infra.Data;
using RSoft.Lib.Design.Infra.Data.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Infra.Tables
{

    /// <summary>
    /// Entry table entity
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Class to map database table in EntityFramework")]
    public class Entry : TableIdNameAuditBase<Guid, Entry>, ITable, IAuditNavigation<Guid, User>, IActive
    {

        #region Constructors

        /// <summary>
        /// Create a new table instance
        /// </summary>
        public Entry() : base(Guid.NewGuid(), null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new table instance
        /// </summary>
        /// <param name="id">User id value</param>
        public Entry(Guid id) : base(id, null)
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
        public Entry(string id) : base()
        {
            Id = new Guid(id);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Active status flag
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Category id
        /// </summary>
        public Guid CategoryId { get; set; }

        #endregion

        #region Navigation/Lazy

        /// <summary>
        /// Created author data
        /// </summary>
        public virtual User CreatedAuthor { get; set; }

        /// <summary>
        /// Changed author data
        /// </summary>
        public virtual User ChangedAuthor { get; set; }

        /// <summary>
        /// Category data
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Transactions by this entry
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; set; }

        #endregion

        #region Local methods

        /// <summary>
        /// Iniatialize objects/properties/fields with default values
        /// </summary>
        private void Initialize()
        {
            IsActive = true;
            Transactions = new HashSet<Transaction>();
        }

        #endregion

        #region Public methods

        #endregion

    }
}
