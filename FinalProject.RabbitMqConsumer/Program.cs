using FinalProject.Business;
using FinalProject.Entities;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

//Consumar tarafında tüketecegimiz mesaj kurugunu burada tekrar declare ediyoruz ve bir connection ve kanal açıyoruz.


//Appsettings.json dosyasını okuyoruz.
IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

//Baglantıyı Oluşturuyoruz..
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri(config.GetSection("RabbitMq").Value);
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Oluşturdugumuz Channel(Kanal) ile QueueDeclare metodu kullanarak mesajları tüketecegimiz kuyrugu belirtiyoruz.
channel.QueueDeclare("email_queue", false, false, false);

//Kuyruktaki mesajları yakalayacak bir event oluşturuyoruz.
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

//BasicConsume ile consumer tarafından yakaladıgımız mesaları tüketiyoruz.
channel.BasicConsume("email_queue", false, consumer);

//consumer.Received ile kuyruga gelen mesajı tüketigimiz event oluşturuyoruz.
consumer.Received += async (s, e) =>
{
    //e.Body.Span ile kuyruktan gelen mesaja ulaşabiliyoruz.
    string serializData = Encoding.UTF8.GetString(e.Body.Span);
    AppUser appUser = JsonSerializer.Deserialize<AppUser>(serializData);

    string subject = "Aktivasyon işlemlemi";
    string body = "Kayıt için lütfen aktivasyon linkine tıklayınız.\n   https://localhost:7137/api/appusers/" + appUser.ActivationCode + " ";

    try
    {
        await EmailSender.SendAsync(appUser,subject,body);
    }
    catch (Exception)
    {
        Console.WriteLine("RabitMq tarafıdan Mesaj Gönderilemedi");
    }
};

Console.Read();