using tlg_neverlands.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcReflection();

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "HelloWorld");

app.Run();
