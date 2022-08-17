using Newtonsoft.Json;
using RabbitMQ.Client;
using Rachis.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetApp1.Sender
{
    public class MessageSender : IMessageSender
    {
        public void Publish(string message)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };



            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();



            channel.QueueDeclare("tweet-queue",
                 durable: true, exclusive: false, autoDelete: false, arguments: null);



            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", "tweet-queue", null, body);
        }
    }
}
