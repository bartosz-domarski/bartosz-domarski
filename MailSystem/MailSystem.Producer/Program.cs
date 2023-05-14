using MailSystem.Producer;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

Console.Title = "MailProducer";
Console.WriteLine("MailProducer");

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "MailSystem",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);

    while (true)
    {
        Console.WriteLine("Write what you want to send");
        Console.WriteLine("Write nothing to exit.");

        string userMessage = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(userMessage))
            break;

        var mail = new Mail()
        {
            Message = userMessage,
            MailType = MailType.SmtpClient
        };

        var message = JsonConvert.SerializeObject(mail);
        var body = Encoding.UTF8.GetBytes(message);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(exchange: "",
            routingKey: "MailSystem",
            basicProperties: properties, body);

        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Sent msg: {mail.Message}, type: {mail.MailType}");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("");
    }
}