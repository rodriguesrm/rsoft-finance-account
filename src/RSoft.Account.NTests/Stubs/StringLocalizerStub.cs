using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.NTests.Stubs
{
    
    [ExcludeFromCodeCoverage(Justification = "Test class should not be considered in test coverage.")]
    public class StringLocalizerStub<T> : IStringLocalizer<T>
    {
        ///<inheritdoc/>
        public LocalizedString this[string name] { get => new(name, name); }

        ///<inheritdoc/>
        public LocalizedString this[string name, params object[] arguments] { get => new(name, name); }

        ///<inheritdoc/>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => new List<LocalizedString>();

    }
}
