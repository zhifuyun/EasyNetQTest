using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;

namespace AspnetCoreDi
{
   
    public static class ApplicationExtension
    {
        private static ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();

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
            bus.SendReceive.ReceiveAsync<TextMessage>("myqueue", HandlerMsg);
            lifeTime.ApplicationStopped.Register(() => { bus.Dispose(); });

            return appBuilder;
        }

        private static  void HandlerMsg(TextMessage message)
        {
            // await Task.Delay(2000);
            Thread.Sleep(1000);
            _queue.Enqueue(message.Text);
            Console.WriteLine($"队列已接收：{message.Text} 集合中的个数为{_queue.Count}");
        }
    }
}