using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{
    public class ListCategoryCommandHandlerTest : TestFor<ListCategoryCommandHandler>
    {

        #region Constructors

        public ListCategoryCommandHandlerTest() : base() { }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            Mock<ICategoryDomainService> domainService = new();
            domainService
                .Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {

                    IEnumerable<Category> entities = new List<Category>() { One<Category>(), One<Category>(), One<Category>()};
                    return entities;
                });
            _fixture.Inject(domainService.Object);
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            ListCategoryCommand command = new();
            CommandResult<IEnumerable<CategoryDto>> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            IEnumerable<CategoryDto> dtos = result.Response;
            Assert.NotNull(dtos);
            Assert.AreEqual(3, dtos.Count());
        }

        #endregion

    }
}
