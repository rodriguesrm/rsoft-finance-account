using System.Threading.Tasks;
using Grpc.Core;
using RSoft.Account.Grpc;

namespace RSoft.Account.GrpcService.Services
{

    /// <summary>
    /// User service
    /// </summary>
    public class UserGrpcService : Users.UsersBase
    {

        public override Task<CreateUserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            return base.CreateUser(request, context);
        }

    }
}
