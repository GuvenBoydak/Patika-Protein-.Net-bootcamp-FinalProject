using FinalProject.Entities;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FinalProject.Business
{
    public static class ProducerService
    {

        private static IConfiguration _configuration;

        static ProducerService()
        {

            _configuration = Configuration;
        }

        public static IConfiguration Configuration
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                _configuration = builder.Build();
                return _configuration;
            }
        }


        public static void Producer(AppUser appUser)
        {
            //Baglantıyı Oluşturuyoruz..
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri(_configuration.GetSection("RabbitMq").Value);
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            //Oluşturdugumuz Channel(Kanal) ile QueueDeclare metodu kullanarak mesajlarımızı göndericemiz bir kuyruk oluşturuyoruz.
            channel.QueueDeclare("email_queue", false, false, false);


            //Parametreden gelen degeri önce Json formata serialize ediyoruz.
            string data = JsonSerializer.Serialize(appUser);

            //Serialize edilen degeri Kuyruga byte[] olarak göndermemiz gerekiyor. Bu yüzden byte[] e çeviriyoruz.
            Byte[] bytes = Encoding.UTF8.GetBytes(data);

            //Kanal üzerinden BasicPublish ile kuyruga mesajlarımızı gönderiyoruz.
            channel.BasicPublish("", "email_queue", body: bytes);
        }
    }
}
