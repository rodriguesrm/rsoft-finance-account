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

    public class CategoryGrpcServiceTest : TestFor<CategoryGrpcService>
    {

        #region Tests

        [Test]
        public async Task CreateCategory_ProcessSuccessReturnSuccessWithCategoryId()
        {
            ServerCallContext context = One<ServerCallContext>();
            CreateCategoryRequest request = One<CreateCategoryRequest>();
            CommandResult<Guid?> mockReply = new()
            {
                Response = Guid.NewGuid()
            };
            MediatorSub.SetMockResponse(mockReply);
            CreateCategoryReply result = await Target.CreateCategory(request, context);
            Assert.AreEqual(mockReply.Response.Value.ToString(), result.Id);
        }

        [Test]
        public void CreateCategory_WhenFailProcess_ThrowRpcException()
        {
            async Task RunMethod()
            {
                ServerCallContext context = One<ServerCallContext>();
                CreateCategoryRequest request = One<CreateCategoryRequest>();
                CommandResult<Guid?> mockReply = new()
                {
                    Errors = new List<GenericNotification>()
                {
                    new GenericNotification("TEST", "TEST ERROR-EXCEPTION")
                }
                };
                MediatorSub.SetMockResponse(mockReply);
                var result = await Target.CreateCategory(request, context);
            }
            Assert.ThrowsAsync<RpcException>(RunMethod);
        }

        [Test]
        public async Task UpdateCategory_ProcessSuccessReturnEmpty()
        {
            ServerCallContext context = One<ServerCallContext>();
            UpdateCategoryRequest request = One<UpdateCategoryRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.UpdateCategory(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public async Task EnableCategory_ProcessSuccessReturnEmpt()
        {
            ServerCallContext context = One<ServerCallContext>();
            EnableCategoryRequest request = One<EnableCategoryRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.EnableCategory(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public async Task DisableCategoryCategory_ProcessSuccessReturnEmpt()
        {
            ServerCallContext context = One<ServerCallContext>();
            DisableCategoryRequest request = One<DisableCategoryRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<bool> mockReply = new()
            {
                Response = true
            };
            MediatorSub.SetMockResponse(mockReply);
            Empty result = await Target.DisableCategory(request, context);
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetCategory_ReturnCategory()
        {
            ServerCallContext context = One<ServerCallContext>();
            GetCategoryRequest request = One<GetCategoryRequest>();
            request.Id = Guid.NewGuid().ToString();
            CommandResult<CategoryDto> mockReply = new()
            {
                Response = One<CategoryDto>()
            };
            mockReply.Response.Id = new Guid(request.Id);
            MediatorSub.SetMockResponse(mockReply);
            CategoryDetail result = await Target.GetCategory(request, context);
            Assert.IsTrue(mockReply.Success);
            Assert.AreEqual(mockReply.Response.Id.ToString(), result.Id);
            Assert.AreEqual(mockReply.Response.Name, result.Name);
        }

        [Test]
        public async Task ListCategory_ReturnCategoryList()
        {
            ServerCallContext context = One<ServerCallContext>();
            Empty request = One<Empty>();
            CommandResult<IEnumerable<CategoryDto>> mockReply = new()
            {
                Response = One<IEnumerable<CategoryDto>>()
            };
            MediatorSub.SetMockResponse(mockReply);
            ListCategoryReply result = await Target.ListCategory(request, context);
            Assert.IsTrue(mockReply.Success);
            Assert.AreEqual(mockReply.Response.Count(), result.Data.Count);
        }

        #endregion

    }
}
