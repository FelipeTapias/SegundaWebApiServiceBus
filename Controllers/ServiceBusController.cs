using Adapters;
using Adapters.Interface;
using Microsoft.AspNetCore.Mvc;

namespace SegundaWebAPI.Controllers
{
    [Route("api/serviceBus")]
    [ApiController]
    public class ServiceBusController: ControllerBase
    {
        private readonly ISenderMessage senderMessage;

        public ServiceBusController(SenderMessage senderMessage) 
        {
            this.senderMessage = senderMessage;
        }

        [HttpPost]
        public async void Post([FromBody] string message)
        {
            await senderMessage.CreateMessage(message);
        }
    }
}
