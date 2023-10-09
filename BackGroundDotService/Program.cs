using BackGroundDotService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<RandomNumberGeneratorBackgroundService>();
builder.Services.AddHostedService<BackGroundExecuteAsyncSample>();
builder.Services.AddLogging();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
