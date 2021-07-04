using Client.API.Models;
using Client.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Client.API.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ClientViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] ClientCpfViewModel clientCpfViewModel)
        {
            var response = await _clientService.GetByCpf(long.Parse(clientCpfViewModel.Cpf));
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            else if (response.Result is null)
                return NotFound();
            else
                return Ok(response.Result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> Post([FromBody] ClientViewModel client)
        {
            var response = await _clientService.Insert(client);
            if (response.IsSuccess)
                return Created(string.Empty, response.Result);
            else
                return BadRequest(response.Message);
        }
    }
}
