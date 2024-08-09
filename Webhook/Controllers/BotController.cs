namespace Webhook.Controllers
{
    [ApiController]
    public class BotController : Controller
    {
        [HttpPost("webhook/update")]
        public IActionResult Post([FromBody] Update update)
        {
            return View();
        }
    }
}
