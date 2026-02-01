using ETicketing.CA.Application.Abstractions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Infrastructure.Services
{
    public class AuditConsumerHostedService : BackgroundService
    {
        private readonly IAuditConsumer _consumer;

        public AuditConsumerHostedService(IAuditConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.ConsumeAsync(stoppingToken);
        }
    }

}
