using AutoMapper;
using BillingAPI.Commands;
using BillingAPI.Infrastructure.Repositories;
using BillingAPI.Models;
using BillingAPI.Models.Responses;
using BillingAPI.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BillingAPI.CommandHandlers
{
    public class BillingCommandHandler
        : IRequestHandler<NewBillingCommand, BillingResponse>
    {
        private readonly IBillingRepository _billingRepository;
        private readonly IMapper _mapper;

        public BillingCommandHandler(IBillingRepository billingRepository, IMapper mapper)
        {
            _billingRepository = billingRepository;
            _mapper = mapper;
        }

        public async Task<BillingResponse> Handle(NewBillingCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return new BillingResponse(request.ValidationResult.Errors.Select(x => x.ErrorMessage).ToArray());

            var billing = _mapper.Map<Billing>(request);
            
            await _billingRepository.Insert(billing);
            return new BillingResponse(billing);
        }
    }
}
