using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Account.Application.Handlers;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using DomainAccount = RSoft.Account.Core.Entities.Account;
using RSoft.Account.Core.Ports;
using RSoft.Account.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Tests.Application.Handlers
{
    public class ListAccountCommandHandlerTest : TestFor<ListAccountCommandHandler>
    {

        #region Constructors

        public ListAccountCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            Mock<IAccountDomainService> domainService = new();
            domainService
                .Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {

                    IEnumerable<DomainAccount> entities = new List<DomainAccount>() { One<DomainAccount>(), One<DomainAccount>(), One<DomainAccount>()};
                    return entities;
                });
            _fixture.Inject(domainService.Object);
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            ListAccountCommand command = new();
            CommandResult<IEnumerable<AccountDto>> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            IEnumerable<AccountDto> dtos = result.Response;
            Assert.NotNull(dtos);
            Assert.AreEqual(3, dtos.Count());
        }

        #endregion

    }
}
