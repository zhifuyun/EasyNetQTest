using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using YiOu.Cloud.Service.PayChannels;
using TextMessage = Models.TextMessage;

namespace Consumer
{
    class Program
    {
        private static ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();
        static void Main(string[] args)
        {
            using (var bus= RabbitHutch.CreateBus("host=localhost"))
            {
                //bus.PubSub.Subscribe<TextMessage>("test", HandleTextMessage);
                //bus.PubSub.Subscribe<TextMessage>("test222", HandleTextMessage2);
                // bus.SendReceive.Receive<TextMessage>("myqueue", message =>
                // {
                //     _queue.Enqueue(message.Text);
                //     // Thread.Sleep(2000);
                //     Console.WriteLine($"队列已接收：{message.Text} 集合中的个数为{_queue.Count}");
                // });

                bus.Rpc.Respond<CreateChannelOrderDto, TextMessage>(request => new TextMessage{Text = "1111"} );
                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }

        static void HandleTextMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Text);
            Console.ResetColor();
        }

        static void HandleTextMessage2(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Got message2: {0}", textMessage.Text);
            Console.ResetColor();
        }
    }
}
