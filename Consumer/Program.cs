using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using Models;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus= RabbitHutch.CreateBus("host=localhost"))
            {
                //bus.PubSub.Subscribe<TextMessage>("test", HandleTextMessage);
                //bus.PubSub.Subscribe<TextMessage>("test222", HandleTextMessage2);
                bus.SendReceive.Receive<TextMessage>("my.queue222", message => Console.WriteLine("MyMessage: {0}", message.Text));
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
