using Telegram.Bot;
using tlg_neverlands;
using tlg_neverlands.Controllers;
using tlg_neverlands.Models;
using tlg_neverlands.Services;

var builder = WebApplication.CreateBuilder(args);
var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.Configuration);
builder.Services.Configure<BotConfiguration>(botConfigurationSection);

var botConfiguration = botConfigurationSection.Get<BotConfiguration>();
if (botConfiguration == null)
    throw new ArgumentException(nameof(botConfiguration));

builder.Services.AddHttpClient("telegram_bot_client")
    .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
    {
        var botConfig = sp.GetConfiguration<BotConfiguration>();
        var options = new TelegramBotClientOptions(botConfig.BotToken);
        return new TelegramBotClient(options, httpClient);
    });

builder.Services.AddScoped<UpdateHandlers>();

builder.Services.AddHostedService<ConfigureWebhook>();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();
app.MapBotWebhookRoute<BotController>(route: botConfiguration.Route);
app.MapControllers();

app.Run();






