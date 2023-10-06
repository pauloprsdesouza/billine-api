using Billine.Admin.Domain.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Billine.Admin.Api.Dependencies
{
    public static class NotificationDependency
    {
        public static void AddNotifications(this IServiceCollection services)
        {
            _ = services.AddScoped<INotificationContext, NotificationContext>();
        }
    }
}
