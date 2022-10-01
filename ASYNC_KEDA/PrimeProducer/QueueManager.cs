using ActiveMQ.Artemis.Client;

namespace PrimeProducer
{
    public class QueueManager
    {
        public async void AddTask(int num, string host, string port, string user, string password, string address) {
            
            var connectionFactory = new ConnectionFactory();
            var endpoint = ActiveMQ.Artemis.Client.Endpoint.Create(host, Convert.ToInt32(port), user, password);
            var connection = await connectionFactory.CreateAsync(endpoint);
            var producer = await connection.CreateProducerAsync(address, RoutingType.Anycast);
            Message message = new Message(num);
            message.MessageId = Guid.NewGuid().ToString();
            message.GroupId = message.MessageId;
            Console.WriteLine("Added a task(1)");
            await producer.SendAsync(message);
            //Can be potentially removed in the future
            await connection.DisposeAsync();
        }
    }
}
