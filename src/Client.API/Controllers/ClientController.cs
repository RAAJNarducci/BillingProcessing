using Client.API.Models;
using Client.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
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
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ClientViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ClientViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] ClientCpfViewModel clientCpfViewModel)
        {
            ClientResponse response;

            if (string.IsNullOrEmpty(clientCpfViewModel.Cpf))
            {
                response = await _clientService.GetAll();
                if (!response.IsSuccess)
                    return BadRequest(response.Message);
                
                var result = (IEnumerable<ClientViewModel>)response.Result;
                
                if (!result.Any())
                    return NoContent();
            }
            else
                response = await _clientService.GetByCpf(long.Parse(clientCpfViewModel.Cpf));

            if (!response.IsSuccess)
                return BadRequest(response.Message);
            else if (response.Result is null)
                return NoContent();
            else
                return Ok(response.Result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
