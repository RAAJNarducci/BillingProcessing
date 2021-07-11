using ConsumingCalculator.Clients;
using ConsumingCalculator.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumingCalculator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await GetClientsAndCreateBilling();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task GetClientsAndCreateBilling()
        {
            try
            {
                var service = RestService.For<IClient>("https://localhost:6001");
                var clients = await service.GetClients();
                foreach (var client in clients)
                {
                    CreateBilling(client);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CreateBilling(ClientResponse clientResponse)
        {
            var settings = new RefitSettings(new NewtonsoftJsonContentSerializer());
            var otherApi = RestService.For<IBilling>("https://localhost:5001", settings);
            var billing = otherApi.CreateBilling(new Models.BillingRequest
            {
                Cpf = clientResponse.Cpf,
                DataVencimento = DateTime.Now.AddDays(30),
                ValorCobranca = CalculateValue(clientResponse.Cpf)
            });
        }

        private double CalculateValue(string cpf)
        {
            int cpfLength = cpf.Length;
            char[] chars = { cpf[0], cpf[1], cpf[cpfLength - 2], cpf[cpfLength - 1] };
            return double.Parse(chars);
        }
    }
}
