using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace RSoft.Entry.WorkerService.Extensions
{

    /// <summary>
    /// Provides log extensions methods
    /// </summary>
    public static class LogExtension
    {

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">he Microsoft.Extensions.Logging.ILogger to write to</param>
        /// <param name="message">Log text message</param>
        /// <param name="content">Adicional content to log</param>
        /// <param name="contentName">Tag content name to log</param>
        public static void LogInformation(this ILogger logger, string message, string content, string contentName = "MessageContent")
        {
            IList<KeyValuePair<string, object>> pairs = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>(contentName, content)
            };
            logger.Log(LogLevel.Information, new EventId(1010, "RSoft:Process:Message"), state: pairs, null, (i, e) => { return message; });
        }

    }

}
