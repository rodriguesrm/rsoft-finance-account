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

namespace RSoft.Entry.Tests.Web_GrpcService.Services
{

    public class AccrualPeriodGrpcServiceTest : TestFor<AccrualPeriodGrpcService>
    {

        #region Tests

        [Test]
        public async Task StartPeriod_ProcessSuccessReturnEmpty()
        {
            ServerCallContext context = One<ServerCallContext>();
            PeriodRequest request = One<PeriodRequest>();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.StartPeriod(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public async Task ClosePeriod_ProcessSuccessReturnEmpty()
        {
            ServerCallContext context = One<ServerCallContext>();
            PeriodRequest request = One<PeriodRequest>();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.ClosePeriod(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public async Task ListAccrualPeriod_ReturnPaymentMethodList()
        {
            ServerCallContext context = One<ServerCallContext>();
            Empty request = One<Empty>();
            CommandResult<IEnumerable<AccrualPeriodDto>> mockReply = new()
            {
                Response = One<IEnumerable<AccrualPeriodDto>>()
            };
            MediatorSub.SetMockResponse(mockReply);
            ListPeriodReply result = await Target.ListPeriod(request, context);
            Assert.IsTrue(mockReply.Success);
            Assert.AreEqual(mockReply.Response.Count(), result.Data.Count);
        }

        #endregion

    }
}
