using NUnit.Framework;
using RSoft.Entry.Application.Arguments;
using RSoft.Entry.Core.Ports;
using RSoft.Finance.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Arguments
{

    public class ListTransactionFilterTest : TestBase
    {

        #region Local objects/varibles

        public class ArgumentToTest
        {
            public IListTransactionFilter Filter { get; set; }
            public bool Valid { get; set; }
        }


        #endregion

        #region Tests

        [DatapointSource]
        public ArgumentToTest[] arguments = new ArgumentToTest[] 
        {
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { } },
            new ArgumentToTest() { Valid = true, Filter = new ListTransactionFilter() { StartAt = DateTime.UtcNow.Date.AddDays(-10), EndAt = DateTime.UtcNow.Date.AddDays(1) } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { StartAt = DateTime.UtcNow.Date.AddDays(-10) } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { EndAt = DateTime.UtcNow.Date.AddDays(1) } },
            new ArgumentToTest() { Valid = true, Filter = new ListTransactionFilter() { Year = DateTime.UtcNow.Year, Month = DateTime.UtcNow.Month } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Year = DateTime.UtcNow.Year } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Year = DateTime.UtcNow.Year, Month = 0 } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Year = DateTime.UtcNow.Year, Month = 13 } },
            new ArgumentToTest() { Valid = true, Filter = new ListTransactionFilter() { EntryId = Guid.NewGuid() } },
            new ArgumentToTest() { Valid = true, Filter = new ListTransactionFilter() { TransactionType = TransactionTypeEnum.Credit } },
            new ArgumentToTest() { Valid = true, Filter = new ListTransactionFilter() { PaymentMethodId = Guid.NewGuid() } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Year = DateTime.UtcNow.Year, StartAt = DateTime.UtcNow.Date.AddDays(-10) } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Year = DateTime.UtcNow.Year, EndAt = DateTime.UtcNow.Date.AddDays(1) } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Month = DateTime.UtcNow.Month, StartAt = DateTime.UtcNow.Date.AddDays(-10) } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Month = DateTime.UtcNow.Month, EndAt = DateTime.UtcNow.Date.AddDays(1) } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Year = DateTime.UtcNow.Year, StartAt = DateTime.UtcNow.Date.AddDays(-10), EndAt = DateTime.UtcNow.Date.AddDays(1) } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Month = DateTime.UtcNow.Month, StartAt = DateTime.UtcNow.Date.AddDays(-10), EndAt = DateTime.UtcNow.Date.AddDays(1) } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Year = DateTime.UtcNow.Year, Month = DateTime.UtcNow.Month, StartAt = DateTime.UtcNow.Date.AddDays(-10) } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Year = DateTime.UtcNow.Year, Month = DateTime.UtcNow.Month, EndAt = DateTime.UtcNow.Date.AddDays(1) } },
            new ArgumentToTest() { Valid = false, Filter = new ListTransactionFilter() { Year = DateTime.UtcNow.Year, Month = DateTime.UtcNow.Month, StartAt = DateTime.UtcNow.Date.AddDays(-10), EndAt = DateTime.UtcNow.Date.AddDays(1) } }
        };
        [Theory]
        public void ValidateArgument(ArgumentToTest argument)
        {
            Assert.AreEqual(argument.Valid, argument.Filter.IsValid());
        }

        #endregion

    }
}
