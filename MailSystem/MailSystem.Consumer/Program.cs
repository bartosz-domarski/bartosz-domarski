using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Mail;
using System.Net;
using MailSystem.Consumer;
using Newtonsoft.Json;

Console.Title = "MailConsumer";
Console.WriteLine("MailConsumer");

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "MailSystem", durable: false,
        exclusive: false, autoDelete: false, arguments: null);

    Console.WriteLine(" Waiting for messages.");

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        var message = Encoding.UTF8.GetString(ea.Body.ToArray());
        var mail = JsonConvert.DeserializeObject<Mail>(message);

        if (mail != null)
        {
            if (mail.MailType == MailType.SmtpClient)
            {
                var client = new SmtpClient()
                {
                    Host = "smtp.address.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("user", "password"),
                    EnableSsl = true
                };

                var exampleMail = new MailMessage()
                {
                    From = new MailAddress("sender@email.com"),
                    To =
                    {
                        new MailAddress("recipent@email.com")
                    },
                    Subject = "Test mail",
                    Body = mail.Message
                };

                //client.Send(exampleMail);

                Console.WriteLine("-> Received: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Message: {mail.Message}");
                Console.WriteLine($"Type: {mail.MailType}");
                Console.ForegroundColor = ConsoleColor.Gray;
                
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
        }
    };

    channel.BasicConsume(queue: "MailSystem",
        autoAck: false, 
        consumer: consumer);


    char key = 'z';

    while (key != 'q' && key != 'Q')
    {
        Console.WriteLine(" Press [q] or [Q] to exit.");
        key = Console.ReadKey().KeyChar;
    }
}