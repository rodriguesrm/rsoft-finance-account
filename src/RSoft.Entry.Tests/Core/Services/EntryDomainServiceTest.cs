using AutoFixture;
using AutoFixture.Kernel;
using NUnit.Framework;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Core.Services;
using RSoft.Entry.Infra;
using RSoft.Entry.Infra.Providers;
using RSoft.Entry.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntryTable = RSoft.Entry.Infra.Tables.Entry;
using EntryDomain = RSoft.Entry.Core.Entities.Entry;
using CategoryDomain = RSoft.Entry.Core.Entities.Category;

namespace RSoft.Entry.Tests.Core.Services
{

    public class EntryDomainServiceTest : TestFor<EntryDomainService>
    {

        #region Local objects/variables

        private const string _entryAName = "SUPERMARKET";
        private const string _entryBName = "TRAVEL";

        private EntryContext _dbContext;

        #endregion

        #region Constructors

        public EntryDomainServiceTest() : base() { }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            _fixture.WithInMemoryDatabase(out _dbContext);
            _fixture.Customizations.Add(new TypeRelay(typeof(IEntryProvider), typeof(EntryProvider)));

        }

        #endregion

        #region Local methods

        /// <summary>
        /// Load initial categories
        /// </summary>
        /// <param name="id">Entry id output</param>
        private void LoadInitialEntry(out Guid id)
        {
            EntryTable tableA = _dbContext.Entries.FirstOrDefault(a => a.Name == _entryAName);
            if (tableA == null)
            {
                EntryTable rowA = _fixture.CreateEntry(_entryAName);
                EntryTable rowB = _fixture.CreateEntry(_entryBName);
                _fixture.WithSeedData(_dbContext, new List<EntryTable>() { rowA, rowB });
                id = rowA.Id;
            }
            else
            {
                id = tableA.Id;
            }
        }

        #endregion

        #region Tests

        [Test]
        public async Task AddAsync_ReturnEntitySaved()
        {
            EntryDomain entity = _fixture.Build<EntryDomain>()
                .With(c => c.Name, "Entry TEST")
                .Create();
            EntryDomain result = await Sut.AddAsync(entity, default);
            Assert.IsTrue(result.Valid);
            EntryTable check = _dbContext.Entries.Find(result.Id);
            Assert.NotNull(check);
            Assert.AreEqual(entity.Id, check.Id);
            Assert.AreEqual(entity.Name, check.Name);
        }

        [Test]
        public async Task GetById_ReturnEntity()
        {
            LoadInitialEntry(out Guid id);
            EntryTable table = _dbContext.Entries.Find(id);
            EntryDomain result = await Sut.GetByKeyAsync(id, default);
            Assert.NotNull(result);
            Assert.AreEqual(table.Name, result.Name);
        }

        [Test]
        public async Task GetAllEntry_ReturnEntityList()
        {
            LoadInitialEntry(out _);
            IEnumerable<EntryDomain> result = await Sut.GetAllAsync(default);
            Assert.GreaterOrEqual(result.Count(), 2);
            EntryTable tableA = _dbContext.Entries.First(c => c.Name == _entryAName);
            EntryTable tableB = _dbContext.Entries.First(c => c.Name == _entryBName);
            Assert.True(result.Any(x => x.Id == tableA.Id));
            Assert.True(result.Any(x => x.Id == tableB.Id));
        }

        [Test]
        public void UpdateEntry_SuccessOnUpdate()
        {
            string oldName = "Entry X";
            string newName = "New Entry";
            EntryTable oldTableRow = _fixture.CreateEntry(oldName);
            _fixture.WithSeedData(_dbContext, new EntryTable[] { oldTableRow });
            EntryDomain entity = new(oldTableRow.Id) { Name = newName, Category = new CategoryDomain(MockBuilder.InitialCategoryId) { Name = _fixture.GetItinialCategoryName() } };
            entity = Sut.Update(entity.Id, entity);
            EntryTable check = _dbContext.Entries.Where(c => c.Id == entity.Id).FirstOrDefault();
            Assert.NotNull(entity);
            Assert.NotNull(check);
            Assert.AreEqual(check.Name, entity.Name);
            Assert.AreEqual(check.Name, newName);
            Assert.AreEqual(entity.Name, newName);
        }

        [Test]
        public void DeleteEntry_SuccessOnDelete()
        {
            EntryTable tableRow = _fixture.CreateEntry("ENTRY TO_REMOVE");
            Guid id = tableRow.Id;
            _fixture.WithSeedData(_dbContext, new EntryTable[] { tableRow });
            Sut.Delete(id);
            _dbContext.SaveChanges();
            EntryTable check = _dbContext.Entries.FirstOrDefault(c => c.Id == id);
            Assert.Null(check);
        }

        #endregion

    }
}
