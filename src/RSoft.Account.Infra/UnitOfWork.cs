using RSoft.Lib.Design.Infra.Data;

namespace RSoft.Account.Infra
{

    /// <summary>
    /// Unit of work object to maintain the integrity of transactional operations
    /// </summary>
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {

        #region Constructors

        /// <summary>
        /// Create a new UnitOfWork instance
        /// </summary>
        /// <param name="ctx">Database context object</param>
        public UnitOfWork(AccountContext ctx) : base(ctx) { }

        #endregion

    }

}
