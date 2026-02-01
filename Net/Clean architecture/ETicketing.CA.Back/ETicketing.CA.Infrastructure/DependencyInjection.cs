using Confluent.Kafka;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Application.Services;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using ETicketing.CA.Domain.Models.Seats;
using ETicketing.CA.Domain.Models.Users;
using ETicketing.CA.Infrastructure.Database;
using ETicketing.CA.Infrastructure.Repositories;
using ETicketing.CA.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static ETicketing.CA.Infrastructure.Services.KafkaAuditConsumer;

namespace ETicketing.CA.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("MiBaseEnMemoria")
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)),
            ServiceLifetime.Scoped,
            ServiceLifetime.Scoped);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<ISeatsRepository, SeatsRepository>();
            services.AddScoped<IRoomsRepository, RoomsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ISeasonsRepository, SeasonsRepository>();
            services.AddScoped<IReservationsRepository, ReservationsRepository>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<ICryptoService, CryptoService>();
            services.AddSingleton<IAuditProducer, KafkaAuditProducer>();
            services.AddSingleton<IAuditConsumer, KafkaAuditConsumer>();
            services.AddSingleton<IAuditProcessor, AuditProcessor>();
            services.AddHostedService<AuditConsumerHostedService>();

            services.Configure<KafkaProducerSettings>(
                configuration.GetSection(nameof(KafkaProducerSettings))
                );
            services.Configure<KafkaConsumerSettings>(
                configuration.GetSection(nameof(KafkaConsumerSettings))
                );

            return services;
        }
    }
}
