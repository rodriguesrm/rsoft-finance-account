using RSoft.Lib.Common.Contracts.Web;
using System;
using System.Collections.Generic;

namespace RSoft.Account.Tests.Stubs
{

    public class AuthenticatedUserStub : IAuthenticatedUser
    {
        public Guid? Id { get; } = new Guid("745991cc-c21f-4512-ba8f-9533435b64ab");
        public string FirstName { get => "Admin"; }
        public string LastName { get => "RSoft"; }
        public string Login { get => "admin"; }
        public string Email { get => "master@server.com"; }
        public IEnumerable<string> Scopes { get => new List<string>() { "Account Service", "Authentication Service" }; }
        public IEnumerable<string> Roles { get => new List<string>() { "service" }; }
    }
}
