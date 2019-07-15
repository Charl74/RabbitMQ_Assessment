using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using SharedLibrary;
using Newtonsoft.Json;

namespace Consumer
{
    class Receive
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(queue: "name",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        string jsonString = Encoding.UTF8.GetString(ea.Body);

                        Message message = JsonConvert.DeserializeObject<Message>(jsonString);
                        if (message != null)
                        {
                            //Console.WriteLine("json: {0}", jsonString);
                            if (!string.IsNullOrWhiteSpace(message.Name))
                            {
                                Console.WriteLine("Hello {0}, I am your father!", message.Name);
                            }
                        }
                    };

                    channel.BasicConsume(queue: "name",
                    autoAck: true,
                    consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
