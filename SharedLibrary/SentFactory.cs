using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;

namespace SharedLibrary
{
    public class SentFactory
    {
        public static void Factory(Message message)
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

                    var jsonString = JsonConvert.SerializeObject(message);

                    var body = Encoding.UTF8.GetBytes(jsonString);

                    channel.BasicPublish(exchange: "",
                    routingKey: "name",
                    basicProperties: null,
                    body: body);
                }
            }
        }
    }
}
