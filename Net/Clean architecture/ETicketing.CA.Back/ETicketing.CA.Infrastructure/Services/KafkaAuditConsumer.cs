using Confluent.Kafka;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Application.Services;
using ETicketing.CA.Domain.Abstractions.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ETicketing.CA.Infrastructure.Services
{
    public class KafkaAuditConsumer : IAuditConsumer
    {
        private IAuditProcessor _processor;
        private KafkaConsumerSettings Settings;

        public KafkaAuditConsumer(IOptions<KafkaConsumerSettings> settings, IAuditProcessor processor)
        {
            _processor = processor;
            Settings = settings.Value;
        }

        public async Task ConsumeAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = Settings.GroupId,
                BootstrapServers = Settings.BootstrapServers,
                EnableAutoCommit = Settings.EnableAutoCommit,
                AutoOffsetReset = Enum.Parse<AutoOffsetReset>(Settings.AutoOffsetReset, ignoreCase: true),
                AllowAutoCreateTopics = Settings.AllowAutoCreateTopics
            };

            using (var consumer = new ConsumerBuilder<string, string>(config)
               .SetKeyDeserializer(Deserializers.Utf8)
               .SetValueDeserializer(Deserializers.Utf8)
               .Build())
            {
                consumer.Subscribe(Settings.Topic);

                while (!cancellationToken.IsCancellationRequested)
                {
                    ConsumeResult<string, string> consumerResult=null;
                    try
                    {
                        consumerResult = consumer.Consume(cancellationToken);
                        if (consumerResult is null || consumerResult.Message is null)
                        {
                            continue;
                        }
                        else
                        {
                            AuditDTO message = JsonConvert.DeserializeObject<AuditDTO>(consumerResult.Value);
                            await _processor.ProcessAsync(message, cancellationToken);
                        }
                    }
                    catch (Exception e) { }
                    finally
                    {
                        consumer.Commit(consumerResult);
                    }
                }
            }
        }
        public class KafkaConsumerSettings
        {
            public string GroupId { get; set; }
            public string BootstrapServers { get; set; }
            public string AutoOffsetReset { get; set; }
            public bool EnableAutoCommit { get; set; }
            public bool AllowAutoCreateTopics { get; set; }
            public string Topic { get; set; }
        }
    }
}