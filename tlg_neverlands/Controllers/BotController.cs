using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using tlg_neverlands.Filters;
using tlg_neverlands.Services;

namespace tlg_neverlands.Controllers;

[ApiController]
public class BotController : ControllerBase
{
    private readonly ILogger<BotController> _logger;

    public BotController(ILogger<BotController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string Get()
    {
        _logger.LogInformation("Lalala");
        return "Hello world";
    }
    
    [HttpPost]
    [ValidateTelegramBot]
    public async Task<IActionResult> Post(
        [FromBody] Update update,
        [FromServices] UpdateHandlers handleUpdateService,
        CancellationToken cancellationToken)
    {
        await handleUpdateService.HandleUpdateAsync(update, cancellationToken);
        return Ok();
    }
}