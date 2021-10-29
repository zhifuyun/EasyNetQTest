using System;
using System.Reflection;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspnetCoreDi
{
    public static class ApplicationExtension
    {
        public static IApplicationBuilder UseSubscribe(this IApplicationBuilder appBuilder)
        {
            var services = appBuilder.ApplicationServices.CreateScope().ServiceProvider;

            var lifeTime = services.GetService<IHostApplicationLifetime>();
            var bus = services.GetService<IBus>();
            lifeTime.ApplicationStarted.Register(() =>
            {
               var subscriber = new AutoSubscriber(bus, "subscriptionIdPrefix");
               subscriber.Subscribe(new[] { Assembly.GetExecutingAssembly() });
               subscriber.SubscribeAsync(new[] { Assembly.GetExecutingAssembly() });
            });

            lifeTime.ApplicationStopped.Register(() => { bus.Dispose(); });

            return appBuilder;
        }
    }
}