using FluentValidator;
using RSoft.Lib.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Account.Application.Extensions
{

    /// <summary>
    /// Dictionary extensions
    /// </summary>
    public static class NotificationExtension
    {

        /// <summary>
        /// Convert notifications to generic notification list
        /// </summary>
        /// <param name="notifications">Notifications list</param>
        public static IEnumerable<GenericNotification> ToGenericNotifications(this IEnumerable<Notification> notifications)
            => notifications.Select(n => new GenericNotification(n.Property, n.Message)).ToList();

    }
}
