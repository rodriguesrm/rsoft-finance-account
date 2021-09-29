using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Account.Application.Handlers;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Account.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Tests.Application.Handlers
{
    
    public class CreateCategoryCommandHandlerTest : TestFor<CreateCategoryCommandHandler>
    {

        #region Constructors

        public CreateCategoryCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            Mock<ICategoryDomainService> domainService = new();
            domainService
                .Setup(m => m.AddAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Category entity, CancellationToken token) => entity);
            _fixture.Inject(domainService.Object);
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {
            CreateCategoryCommand command = new("CATEGORY_NAME");
            CommandResult<Guid?> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
