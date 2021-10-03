using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{
    public class GetCategoryByIdCommandHandlerTest : TestFor<GetCategoryByIdCommandHandler>
    {

        #region Constructors

        public GetCategoryByIdCommandHandlerTest() : base() { }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            Category entity = null;

            Mock<ICategoryDomainService> domainService = new();
            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<Category>(c => c.FromFactory(() => new Category(id)));
                    entity = One<Category>();
                    return entity;
                });
            _fixture.Inject(domainService.Object);

            GetCategoryByIdCommand command = new(Guid.NewGuid());
            CommandResult<CategoryDto> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            CategoryDto dto = result.Response;
            Assert.NotNull(dto);
            Assert.AreEqual(entity.Id, dto.Id);
            Assert.AreEqual(entity.Name, dto.Name);
        }

        #endregion

    }
}
