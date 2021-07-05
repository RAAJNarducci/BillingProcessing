using BillingAPI.Commands;
using BillingAPI.Queries;
using BillingAPI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBillingQueries _billingQueries;

        public BillingController(IMediator mediator, IBillingQueries billingQueries)
        {
            _mediator = mediator;
            _billingQueries = billingQueries;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] BillingViewModel billingViewModel)
        {
            var tst = await _billingQueries.Get(billingViewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewBillingCommand newBillingCommand)
        {
            var response = await _mediator.Send(newBillingCommand);
            if (response.IsSuccess)
                return Created(string.Empty, response.Result);
            else
                return BadRequest(response.Message);
        }
    }
}
