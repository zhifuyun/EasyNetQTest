using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using Models;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus= RabbitHutch.CreateBus("host=localhost"))
            {
                // var input = String.Empty;
                // Console.WriteLine("Enter a message. 'Quit' to quit.");
                // while ((input = Console.ReadLine()) != "Quit")
                // {
                //     //bus.PubSub.Publish(new TextMessage { Text = input });
                //     //bus.SendReceive.Send("my.queue", new TextMessage { Text = "Hello Widgets!" });
                //     bus.SendReceive.Send("my.queue222", new TextMessage { Text = "Hello Widgets!" });
                //     Console.WriteLine("Message published!");
                // }
                // Parallel.For(1, 100, i =>
                // {
                //     var msg = new TextMessage { Text = i.ToString() };
                //     bus.PubSub.PublishAsync(msg);
                //     Console.WriteLine($"{i} 已发送");
                // });

                // Parallel.For(1, 100, async i =>
                // {
                //     var msg = new TextMessage { Text = i.ToString() };
                //    await bus.SendReceive.SendAsync("myqueue", msg);
                //     Console.WriteLine($"{i} 已发送");
                // });

                for (int i = 1; i <= 100; i++)
                {
                    var msg = new TextMessage { Text = i.ToString() };
                  var t= bus.SendReceive.SendAsync("myqueue", msg);
                  t.Wait();
                    Console.WriteLine($"{i} 已发送");
                }


            }
            Console.WriteLine("发送完成");
            Console.Read();
        }
    }
}
