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

    public class ChangeStatusCategoryCommandHandlerTest : TestFor<ChangeStatusCategoryCommandHandler>
    {

        #region Constructors

        public ChangeStatusCategoryCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            Mock<ICategoryDomainService> domainService = new();
            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<Category>(c => c.FromFactory(() => new Category(id)));
                    Category entity = One<Category>();
                    return entity;
                });
            _fixture.Inject(domainService.Object);
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {
            ChangeStatusCategoryCommand command = new(Guid.NewGuid(), true);
            CommandResult<bool> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
