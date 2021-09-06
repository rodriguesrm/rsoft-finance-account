using RSoft.Account.Infra.Tables;
using RSoft.Lib.Common.ValueObjects;
using UserDomain = RSoft.Account.Core.Entities.User;

namespace RSoft.Account.Infra.Extensions
{

    /// <summary>
    /// User table extension class
    /// </summary>
    public static class UserExtension
    {

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        public static UserDomain Map(this User table)
        {

            UserDomain result = null;
            if (table != null)
            {

                result = new UserDomain(table.Id)
                {
                    Name = new Name(table.FirstName, table.LastName),
                    IsActive = table.IsActive
                };

            }

            return result;

        }

        /// <summary>
        /// Maps entity to table
        /// </summary>
        /// <param name="entity">Domain entity to map</param>
        public static User Map(this UserDomain entity)
        {

            User result = null;

            if (entity != null)
            {
                result = new User(entity.Id)
                {
                    FirstName = entity.Name.FirstName,
                    LastName = entity.Name.LastName
                };
            }

            return result;

        }

        /// <summary>
        /// Maps entity to an existing table
        /// </summary>
        /// <param name="entity">Domain entity to map</param>
        /// <param name="table">Instance of existing table entity</param>
        public static User Map(this UserDomain entity, User table)
        {

            if (entity != null && table != null)
            {

                table.FirstName = entity.Name.FirstName;
                table.LastName = entity.Name.LastName;
            }

            return table;

        }

    }

}
