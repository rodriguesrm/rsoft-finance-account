using Grpc.Core;
using NUnit.Framework;
using RSoft.Entry.Grpc.Protobuf;
using RSoft.Entry.GrpcService.Services;
using RSoft.Entry.Tests.Stubs;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using RSoft.Entry.Contracts.Models;
using System.Linq;
using System;

namespace RSoft.Entry.Tests.Web_GrpcService.Services
{
    
    public class TransactionGrpcServiceTest : TestFor<TransactionGrpcService>
    {

        #region Tests

        [Test]
        public async Task CreateTransaction_ProcessSuccessReturnSuccessWithTransacitonId()
        {
            ServerCallContext context = One<ServerCallContext>();
            CreateTransactionRequest request = One<CreateTransactionRequest>();
            CommandResult<Guid?> mockReply = new()
            {
                Response = Guid.NewGuid()
            };
            MediatorSub.SetMockResponse(mockReply);
            CreateTransactionReply result = await Target.CreateTransaction(request, context);
            Assert.AreEqual(mockReply.Response.Value.ToString(), result.Id);
        }

        [Test]
        public async Task GetTransactin_ReturnTransaction()
        {
            ServerCallContext context = One<ServerCallContext>();
            GetTransactionRequest request = One<GetTransactionRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<TransactionDto> mockReply = new()
            {
                Response = One<TransactionDto>()
            };
            mockReply.Response.Id = new Guid(request.Id);
            MediatorSub.SetMockResponse(mockReply);
            TransactionDetail result = await Target.GetTransaction(request, context);
            Assert.IsTrue(mockReply.Success);
            Assert.AreEqual(mockReply.Response.Id.ToString(), result.Id);
            Assert.AreEqual(mockReply.Response.Year, result.Year);
            Assert.AreEqual(mockReply.Response.Month, result.Month);
            Assert.AreEqual(mockReply.Response.Amount, result.Amount);
            Assert.AreEqual(mockReply.Response.Comment, result.Comment);
            Assert.AreEqual(mockReply.Response.Date.ToUniversalTime().Date, result.Date.ToDateTime().Date);
        }

        [Test]
        public async Task ListTransaction_ReturnTransactionList()
        {
            ServerCallContext context = One<ServerCallContext>();
            ListTransactionRequest request = One<ListTransactionRequest>();
            CommandResult<IEnumerable<TransactionDto>> mockReply = new()
            {
                Response = One<IEnumerable<TransactionDto>>()
            };
            MediatorSub.SetMockResponse(mockReply);
            ListTransactionReply result = await Target.ListTransaction(request, context);
            Assert.IsTrue(mockReply.Success);
            Assert.AreEqual(mockReply.Response.Count(), result.Data.Count);
        }

        [Test]
        public void ListTransaction_WhenArgumentsIsInvalid_ThrowRpcException()
        {
            async Task DoTest()
            {
                ServerCallContext context = One<ServerCallContext>();
                ListTransactionRequest request = One<ListTransactionRequest>();
                ArgumentException mockReply = new();
                MediatorSub.SetMockResponse(mockReply);
                ListTransactionReply result = await Target.ListTransaction(request, context);
            }
            Assert.ThrowsAsync<RpcException>(DoTest);
        }

        [Test]
        public void ListTransaction_WhenErrorOccurs_ThrowException()
        {
            Exception mockReply = new("FORCE EXCEPTION");
            async Task DoTest()
            {
                ServerCallContext context = One<ServerCallContext>();
                ListTransactionRequest request = new();
                MediatorSub.SetMockResponse(mockReply);
                ListTransactionReply result = await Target.ListTransaction(request, context);
            }
            Exception ex = Assert.ThrowsAsync<Exception>(DoTest);
            Assert.AreEqual(mockReply.Message, ex.Message);
        }

        [Test]
        public async Task RollBackTransaction_ProcessSuccessReturnNewTransactionId()
        {
            ServerCallContext context = One<ServerCallContext>();
            RollbackTransactionRequest request = One<RollbackTransactionRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<Guid?> mockReply = new()
            {
                Response = Guid.NewGuid()
            };
            MediatorSub.SetMockResponse(mockReply);
            var result = await Target.RollbackTransaction(request, context);
            Assert.AreEqual(mockReply.Response.Value.ToString(), result.Id);
        }

        #endregion

    }
}
