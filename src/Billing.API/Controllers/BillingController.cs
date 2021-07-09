﻿using BillingAPI.Commands;
using BillingAPI.Models;
using BillingAPI.Models.Responses;
using BillingAPI.Queries;
using BillingAPI.ViewModels;
using FluentValidation.Results;
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
            ValidationResult validation = new BillingViewModelValidator().Validate(billingViewModel);
            if (!validation.IsValid)
                return BadRequest(new BillingResponse("Pelo menos um dos filtros é obrigatório"));

            var response = await _billingQueries.Get(billingViewModel);

            if (!response.IsSuccess)
                return BadRequest(response.Message);

            var result = (IEnumerable<Billing>)response.Result;
            if (!result.Any())
                return NotFound();
            else
                return Ok(response.Result);
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
