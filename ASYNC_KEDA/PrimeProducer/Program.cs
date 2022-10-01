using PrimeProducer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/{num}", (int num) =>
{
    QueueManager queueManager = new QueueManager();

    Console.WriteLine($"HOST: {Environment.GetEnvironmentVariable("HOST")}");
    Console.WriteLine($"PORT: {Environment.GetEnvironmentVariable("PORT")}");
    Console.WriteLine($"USER: {Environment.GetEnvironmentVariable("USER")}");
    Console.WriteLine($"PASSWORD: {Environment.GetEnvironmentVariable("PASSWORD")}");
    Console.WriteLine($"ADDRESS: {Environment.GetEnvironmentVariable("ADDRESS")}");

    queueManager.AddTask(num,
        Environment.GetEnvironmentVariable("HOST"),
        Environment.GetEnvironmentVariable("PORT"),
        Environment.GetEnvironmentVariable("USER"),
        Environment.GetEnvironmentVariable("PASSWORD"),
        Environment.GetEnvironmentVariable("ADDRESS"));
})
.WithName("AddTask");

app.Run();
