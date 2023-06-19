using tlg_neverlands.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();
//При запросе gRPC в т.ч. рефлексии идём url=localhost:5002 (без http)
builder.Services.AddGrpcReflection();

var app = builder.Build();
app.MapGrpcReflectionService();
app.MapGrpcService<GreeterService>();
//app.MapGet("/", () => "HelloWorld");

app.Run();
