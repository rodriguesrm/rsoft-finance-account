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

namespace RSoft.Entry.Tests.Web_GrpcService.Services
{

    public class PaymentMethodGrpcServiceTest : TestFor<PaymentMethodGrpcService>
    {

        #region Tests

        [Test]
        public async Task CreatePaymentMethod_ProcessSuccessReturnSuccessWithPaymentMethodId()
        {
            ServerCallContext context = One<ServerCallContext>();
            CreatePaymentMethodRequest request = One<CreatePaymentMethodRequest>();
            CommandResult<Guid?> mockReply = new()
            {
                Response = Guid.NewGuid()
            };
            MediatorSub.SetMockResponse(mockReply);
            CreatePaymentMethodReply result = await Target.CreatePaymentMethod(request, context);
            Assert.AreEqual(mockReply.Response.Value.ToString(), result.Id);
        }

        [Test]
        public void CreatePaymentMethod_WhenFailProcess_ThrowRpcException()
        {
            async Task RunMethod()
            {
                ServerCallContext context = One<ServerCallContext>();
                CreatePaymentMethodRequest request = One<CreatePaymentMethodRequest>();
                CommandResult<Guid?> mockReply = new()
                {
                    Errors = new List<GenericNotification>()
                {
                    new GenericNotification("TEST", "TEST ERROR-EXCEPTION")
                }
                };
                MediatorSub.SetMockResponse(mockReply);
                var result = await Target.CreatePaymentMethod(request, context);
            }
            Assert.ThrowsAsync<RpcException>(RunMethod);
        }

        [Test]
        public async Task UpdatePaymentMethod_ProcessSuccessReturnEmpty()
        {
            ServerCallContext context = One<ServerCallContext>();
            UpdatePaymentMethodRequest request = One<UpdatePaymentMethodRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.UpdatePaymentMethod(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public async Task EnablePaymentMethod_ProcessSuccessReturnEmpt()
        {
            ServerCallContext context = One<ServerCallContext>();
            ChangeStatusPaymentMethodRequest request = One<ChangeStatusPaymentMethodRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.EnablePaymentMethod(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public async Task DisablePaymentMethodPaymentMethod_ProcessSuccessReturnEmpt()
        {
            ServerCallContext context = One<ServerCallContext>();
            ChangeStatusPaymentMethodRequest request = One<ChangeStatusPaymentMethodRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.DisablePaymentMethod(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetPaymentMethod_ReturnPaymentMethod()
        {
            ServerCallContext context = One<ServerCallContext>();
            GetPaymentMethodRequest request = One<GetPaymentMethodRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<PaymentMethodDto> mockReply = new()
            {
                Response = One<PaymentMethodDto>()
            };
            mockReply.Response.Id = new Guid(request.Id);
            MediatorSub.SetMockResponse(mockReply);
            GetPaymentMethodReply result = await Target.GetPaymentMethod(request, context);
            Assert.IsTrue(mockReply.Success);
            Assert.AreEqual(mockReply.Response.Id.ToString(), result.Data.Id);
            Assert.AreEqual(mockReply.Response.Name, result.Data.Name);
        }

        [Test]
        public async Task ListPaymentMethod_ReturnPaymentMethodList()
        {
            ServerCallContext context = One<ServerCallContext>();
            Empty request = One<Empty>();
            CommandResult<IEnumerable<PaymentMethodDto>> mockReply = new()
            {
                Response = One<IEnumerable<PaymentMethodDto>>()
            };
            MediatorSub.SetMockResponse(mockReply);
            ListPaymentMethodReply result = await Target.ListPaymentMethod(request, context);
            Assert.IsTrue(mockReply.Success);
            Assert.AreEqual(mockReply.Response.Count(), result.Data.Count);
        }

        #endregion

    }
}
