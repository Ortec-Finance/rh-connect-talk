using ActiveMQ.Artemis.Client;
using ActiveMQ.Artemis.Client.Transactions;
using PrimeConsumer;

var connectionFactory = new ConnectionFactory();
var endpoint = ActiveMQ.Artemis.Client.Endpoint.Create(Environment.GetEnvironmentVariable("HOST"),
        Convert.ToInt32(Environment.GetEnvironmentVariable("PORT")),
        Environment.GetEnvironmentVariable("USER"),
        Environment.GetEnvironmentVariable("PASSWORD"));

var connection = await connectionFactory.CreateAsync(endpoint);
var consumer = await connection.CreateConsumerAsync(new ConsumerConfiguration
{
    Address = Environment.GetEnvironmentVariable("ADDRESS"),
    RoutingType = RoutingType.Anycast,
    Credit = 1
});

bool isActive = true;

PrimeCalculator primeCalculator = new PrimeCalculator();


DateTime latestCommit = DateTime.Now;

long timeout = Convert.ToInt32(Environment.GetEnvironmentVariable("TIMEOUT"));

while (connection.IsOpened && isActive)
{
    if ((DateTime.Now - latestCommit).TotalSeconds > timeout)
    {
        isActive = false;
        break;
    }
    Console.WriteLine("Connection open (1)  " + DateTime.Now);
    var messageIn = await consumer.ReceiveAsync();
    Console.WriteLine("Recieved a message " + DateTime.Now);
    int num = messageIn.GetBody<int>();
    Console.WriteLine("Recieved the body of the message.Calculating. " + DateTime.Now);
    int result = primeCalculator.GetLargestPrimeUpTo(num);
    Console.WriteLine($"Largest prime less than {num} is {result} " + DateTime.Now);
    try
    {
        Console.WriteLine("Created transaction " + DateTime.Now);
        await using var transaction = new Transaction();
        Console.WriteLine("Accepted transaction " + DateTime.Now);
        await consumer.AcceptAsync(messageIn, transaction);
        Console.WriteLine("Commited transaction " + DateTime.Now);
        await transaction.CommitAsync();
        latestCommit = DateTime.Now;
    }
    catch (Exception e)
    {
        // Never fail the main loop.
        // If we get here, this indicates programming error.
        // Probably no use to reject the message and pick it up again later.
        Console.WriteLine($"Message unhandled: {messageIn.MessageId}: error: {e}.");
        await consumer.AcceptAsync(messageIn);
    }
}

Console.WriteLine("FINISHED");


