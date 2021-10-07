using Microsoft.Extensions.Logging;
using RSoft.Lib.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RSoft.Entry.Tests.Stubs
{

    [ExcludeFromCodeCoverage(Justification = "Stub class")]
    public class LoggerStub<T> : ILogger<T>
    {

        #region Local objects/variables

        private readonly List<string> _logs = new();

        #endregion

        #region Properties

        public IList<string> Logs
            => _logs.ToList().AsReadOnly();

        #endregion

        #region Public methods

        public IDisposable BeginScope<TState>(TState state)
            => ServiceActivator.GetScope();

        public bool IsEnabled(LogLevel logLevel)
            => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = formatter.Invoke(state, exception);
            _logs.Add(message);
        }

        #endregion

    }
}
