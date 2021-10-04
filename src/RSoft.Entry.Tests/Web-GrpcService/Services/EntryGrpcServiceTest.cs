using Grpc.Core;
using NUnit.Framework;
using RSoft.Entry.Grpc.Protobuf;
using RSoft.Entry.GrpcService.Services;
using RSoft.Entry.Tests.Stubs;
using RSoft.Lib.Common.Models;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using RSoft.Entry.Contracts.Models;
using System.Linq;
using AutoFixture;

namespace RSoft.Entry.Tests.Web_GrpcService.Services
{

    public class EntryGrpcServiceTest : TestFor<EntryGrpcService>
    {

        #region Tests

        [Test]
        public async Task CreateEntry_ProcessSuccess_ReturnId()
        {
            ServerCallContext context = One<ServerCallContext>();
            CreateEntryRequest request = _fixture
                .Build<CreateEntryRequest>()
                .With(c => c.CategoryId, Guid.NewGuid().ToString())
                .Create();
            CommandResult<Guid?> mockReply = new()
            {
                Response = Guid.NewGuid()
            };
            MediatorSub.SetMockResponse(mockReply);
            CreateEntryReply result = await Target.CreateEntry(request, context);
            Assert.AreEqual(mockReply.Response.Value.ToString(), result.Id);
        }

        [Test]
        public void CreateEntry_WhenFailProcess_ThrowRpcException()
        {
            async Task RunMethod()
            {
                ServerCallContext context = One<ServerCallContext>();
                CreateEntryRequest request = One<CreateEntryRequest>();
                CommandResult<Guid?> mockReply = new()
                {
                    Errors = new List<GenericNotification>()
                {
                    new GenericNotification("TEST", "TEST ERROR-EXCEPTION")
                }
                };
                MediatorSub.SetMockResponse(mockReply);
                var result = await Target.CreateEntry(request, context);
            }
            Assert.ThrowsAsync<RpcException>(RunMethod);
        }

        [Test]
        public async Task UpdateEntry_ProcessSuccessReturnEmpty()
        {
            ServerCallContext context = One<ServerCallContext>();
            UpdateEntryRequest request = One<UpdateEntryRequest>();
            request.Id = Guid.NewGuid().ToString();
            request.CategoryId = Guid.NewGuid().ToString();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.UpdateEntry(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public void UpdateEntry_WhenCategoryIsInvalid_ThrowRpcException()
        {
            ArgumentException mockReply = new("INVALID_CATEGORY");
            async Task DoTest()
            {
                ServerCallContext context = One<ServerCallContext>();
                UpdateEntryRequest request = One<UpdateEntryRequest>();
                request.Id = Guid.NewGuid().ToString();
                request.CategoryId = "AA33";
                MediatorSub.SetMockResponse(mockReply);
                Empty result = await Target.UpdateEntry(request, context);
            }
            ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(DoTest);
            Assert.AreEqual(mockReply.Message, ex.Message);
        }


        [Test]
        public async Task EnableEntry_ProcessSuccessReturnEmpt()
        {
            ServerCallContext context = One<ServerCallContext>();
            ChangeStatusEntryRequest request = One<ChangeStatusEntryRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.EnableEntry(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public async Task DisableEntryEntry_ProcessSuccessReturnEmpt()
        {
            ServerCallContext context = One<ServerCallContext>();
            ChangeStatusEntryRequest request = One<ChangeStatusEntryRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.DisableEntry(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetEntry_ReturnEntry()
        {
            ServerCallContext context = One<ServerCallContext>();
            GetEntryRequest request = One<GetEntryRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<EntryDto> mockReply = new()
            {
                Response = One<EntryDto>()
            };
            mockReply.Response.Id = new Guid(request.Id);
            MediatorSub.SetMockResponse(mockReply);
            EntryDetail result = await Target.GetEntry(request, context);
            Assert.IsTrue(mockReply.Success);
            Assert.AreEqual(mockReply.Response.Id.ToString(), result.Id);
            Assert.AreEqual(mockReply.Response.Name, result.Name);
        }

        [Test]
        public async Task ListEntry_ReturnEntryList()
        {
            ServerCallContext context = One<ServerCallContext>();
            Empty request = One<Empty>();
            CommandResult<IEnumerable<EntryDto>> mockReply = new()
            {
                Response = One<IEnumerable<EntryDto>>()
            };
            MediatorSub.SetMockResponse(mockReply);
            ListEntryReply result = await Target.ListEntry(request, context);
            Assert.IsTrue(mockReply.Success);
            Assert.AreEqual(mockReply.Response.Count(), result.Data.Count);
        }

        #endregion

    }
}
