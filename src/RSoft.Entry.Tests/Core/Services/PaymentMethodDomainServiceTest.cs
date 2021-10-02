using AutoFixture;
using AutoFixture.Kernel;
using NUnit.Framework;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Core.Services;
using RSoft.Entry.Infra;
using RSoft.Entry.Infra.Providers;
using RSoft.Entry.Tests.Extensions;
using RSoft.Finance.Contracts.Enum;
using RSoft.Lib.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentMethodTable = RSoft.Entry.Infra.Tables.PaymentMethod;

namespace RSoft.Entry.Tests.Core.Services
{

    public class PaymentMethodDomainServiceTest : TestFor<PaymentMethodDomainService>
    {

        #region Local objects/variables

        private const string _paymentAName = "MONEY";
        private const string _paymentBName = "CREDIT CARD";

        private EntryContext _dbContext;

        #endregion

        #region Constructors

        public PaymentMethodDomainServiceTest() : base() { }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            _fixture.WithInMemoryDatabase(out _dbContext);
            _fixture.Customizations.Add(new TypeRelay(typeof(IPaymentMethodProvider), typeof(PaymentMethodProvider)));

        }

        #endregion

        #region Local methods

        /// <summary>
        /// Load initial payments methods
        /// </summary>
        /// <param name="paymentMethodYd">Payment methods id output</param>
        private void LoadInitialPayments(out Guid paymentMethodYd)
        {
            PaymentMethodTable table = _dbContext.PaymentMethods.Where(p => p.Name == _paymentAName).FirstOrDefault();
            if (table == null)
            {
                PaymentMethodTable rowA = _fixture.CreatePaymentMethod(_paymentAName, PaymentTypeEnum.Money);
                PaymentMethodTable rowB = _fixture.CreatePaymentMethod(_paymentBName, PaymentTypeEnum.CreditCard);
                _fixture.WithSeedData(_dbContext, new List<PaymentMethodTable>() { rowA, rowB });
                paymentMethodYd = rowA.Id;
            }
            else
            {
                paymentMethodYd = table.Id;
            }
        }

        #endregion

        #region Tests

        [Test]
        public async Task AddAsync_ReturnEntitySaved()
        {
            PaymentMethod payment = _fixture.Build<PaymentMethod>()
                .With(c => c.Name, "PIC PAY")
                .With(c => c.CreatedAuthor, One<Author<Guid>>())
                .With(c => c.ChangedAuthor, One<AuthorNullable<Guid>>())
                .Create();
            var result = await Sut.AddAsync(payment, default);
            Assert.IsTrue(result.Valid);
            PaymentMethodTable check = _dbContext.PaymentMethods.Find(result.Id);
            Assert.NotNull(check);
            Assert.AreEqual(payment.Id, check.Id);
            Assert.AreEqual(payment.Name, check.Name);
        }

        [Test]
        public async Task GetById_ReturnEntity()
        {
            LoadInitialPayments(out Guid id);
            PaymentMethodTable table = _dbContext.PaymentMethods.Find(id);
            PaymentMethod result = await Sut.GetByKeyAsync(id, default);
            Assert.NotNull(result);
            Assert.AreEqual(table.Name, result.Name);
        }

        [Test]
        public async Task GetAllPayment_ReturnEntityList()
        {
            LoadInitialPayments(out _);
            IEnumerable<PaymentMethod> result = await Sut.GetAllAsync(default);
            Assert.GreaterOrEqual(result.Count(), 2);
            PaymentMethodTable paymentA = _dbContext.PaymentMethods.First(c => c.Name == _paymentAName);
            PaymentMethodTable paymentB = _dbContext.PaymentMethods.First(c => c.Name == _paymentBName);
            Assert.True(result.Any(x => x.Id == paymentA.Id));
            Assert.True(result.Any(x => x.Id == paymentB.Id));
        }

        [Test]
        public void UpdateCategory_SuccessOnUpdate()
        {
            string oldName = "BANK TRANSFER";
            string newName = "PIX";
            PaymentMethodTable oldTableRow = _fixture.CreatePaymentMethod(oldName, PaymentTypeEnum.BankTransaction);
            _fixture.WithSeedData(_dbContext, new PaymentMethodTable[] { oldTableRow });
            PaymentMethod payment = new PaymentMethod(oldTableRow.Id)
            {
                Name = newName,
                PaymentType = PaymentTypeEnum.BankTransaction
            };
            payment = Sut.Update(payment.Id, payment);
            PaymentMethodTable check = _dbContext.PaymentMethods.Where(c => c.Id == payment.Id).FirstOrDefault();
            Assert.NotNull(payment);
            Assert.NotNull(check);
            Assert.AreEqual(check.Name, payment.Name);
            Assert.AreEqual(check.Name, newName);
            Assert.AreEqual(payment.Name, newName);
        }

        [Test]
        public void DeleteCategory_SuccessOnDelete()
        {
            PaymentMethodTable tableRow = _fixture.CreatePaymentMethod("PAYMENT TO REMOVE", PaymentTypeEnum.FoodStamps);
            Guid id = tableRow.Id;
            _fixture.WithSeedData(_dbContext, new PaymentMethodTable[] { tableRow });
            Sut.Delete(id);
            _dbContext.SaveChanges();
            PaymentMethodTable check = _dbContext.PaymentMethods.FirstOrDefault(c => c.Id == id);
            Assert.Null(check);
        }

        #endregion

    }
}
