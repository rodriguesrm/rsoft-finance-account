using RSoft.Lib.Common.Contracts.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.NTests.Stubs
{

    [ExcludeFromCodeCoverage(Justification = "Test class should not be considered in test coverage.")]
    public class AuthenticatedUserStub : IAuthenticatedUser
    {

        public static Guid UserAdminId => new("745991cc-c21f-4512-ba8f-9533435b64ab");

        public Guid? Id { get => UserAdminId; }
        public string FirstName { get => "Admin"; }
        public string LastName { get => "RSoft"; }
        public string Login { get => "admin"; }
        public string Email { get => "master@server.com"; }
        public IEnumerable<string> Scopes { get => new List<string>() { "Account Service", "Authentication Service" }; }
        public IEnumerable<string> Roles { get => new List<string>() { "service" }; }
    }
}
