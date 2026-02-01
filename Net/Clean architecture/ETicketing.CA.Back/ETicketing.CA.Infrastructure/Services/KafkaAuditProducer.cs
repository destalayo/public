using Confluent.Kafka;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static Confluent.Kafka.ConfigPropertyNames;

namespace ETicketing.CA.Infrastructure.Services
{
    public class KafkaAuditProducer : IAuditProducer
    {
        private KafkaProducerSettings Settings;

        public KafkaAuditProducer(IOptions<KafkaProducerSettings> settings)
        {
            Settings = settings.Value;
        }

        public async Task SendAsync(AuditDTO audit)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = $"{Settings.HostName}:{Settings.Port}"
            };
            using(var producer= new ProducerBuilder<string, string>(config)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .Build()){
                var message = new Message<string, string>
                {
                    Key = audit.Method,
                    Value = JsonConvert.SerializeObject(audit)
                };

                await producer.ProduceAsync(Settings.Topic, message);
            }            
        }
    }
    public class KafkaProducerSettings
    {
        public string HostName { get; set; }
        public string Port { get; set; }
        public string Topic { get; set; }
    }
}
