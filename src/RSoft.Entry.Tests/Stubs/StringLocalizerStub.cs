using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Tests.Stubs
{

    [ExcludeFromCodeCoverage(Justification = "Stub class")]
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
