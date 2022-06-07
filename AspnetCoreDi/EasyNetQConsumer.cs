using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Models;
using Newtonsoft.Json;

namespace AspnetCoreDi
{
    public class EasyNetQConsumer:IConsume<WeatherForecast>,IConsumeAsync<TextMessage>
    {
        private static ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();

        public void Consume(WeatherForecast message, CancellationToken cancellationToken = new CancellationToken())
        {
       
                Debug.WriteLine(JsonConvert.SerializeObject(message));

        }

        public void Consume(TextMessage message, CancellationToken cancellationToken = new CancellationToken())
        {
            _queue.Enqueue(message.Text);
            Thread.Sleep(2000);
            Console.WriteLine($"队列已接收：{message.Text} 集合中的个数为{_queue.Count}");
        }

        public async Task ConsumeAsync(TextMessage message, CancellationToken cancellationToken = new CancellationToken())
        {
            await Task.Run(() =>
            {
                _queue.Enqueue(message.Text);
                Thread.Sleep(2000);
                Console.WriteLine($"队列已接收：{message.Text} 集合中的个数为{_queue.Count}");
            }, cancellationToken);
        }
    }
}