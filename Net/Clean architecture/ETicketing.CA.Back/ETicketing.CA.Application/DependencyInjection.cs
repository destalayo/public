using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Application.UserCases.Reservations.CreateReservation;
using ETicketing.CA.Application.UserCases.Reservations.DeleteReservation;
using ETicketing.CA.Application.UserCases.Reservations.ReadReservations;
using ETicketing.CA.Application.UserCases.Reservations.ReadReservationsBySeasonBySeatIds;
using ETicketing.CA.Application.UserCases.Rooms.CreateRoom;
using ETicketing.CA.Application.UserCases.Rooms.DeleteRoom;
using ETicketing.CA.Application.UserCases.Rooms.ReadRoom;
using ETicketing.CA.Application.UserCases.Rooms.ReadRooms;
using ETicketing.CA.Application.UserCases.Rooms.UpdateRoom;
using ETicketing.CA.Application.UserCases.Seasons.CreateSeason;
using ETicketing.CA.Application.UserCases.Seasons.DeleteSeason;
using ETicketing.CA.Application.UserCases.Seasons.ReadSeason;
using ETicketing.CA.Application.UserCases.Seasons.ReadSeasons;
using ETicketing.CA.Application.UserCases.Seasons.UpdateSeason;
using ETicketing.CA.Application.UserCases.Seats.ReadSeats;
using ETicketing.CA.Application.UserCases.Seats.ReadSeatsBySeason;
using ETicketing.CA.Application.UserCases.Sessions.CreateSession;
using ETicketing.CA.Application.UserCases.Sessions.UpdateSession;
using ETicketing.CA.Application.UserCases.Users.CreateUser;
using ETicketing.CA.Application.UserCases.Users.ReadUsers;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using ETicketing.CA.Domain.Models.Seats;
using ETicketing.CA.Domain.Models.Sessions;
using ETicketing.CA.Domain.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicketing.CA.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICommandHandler<CreateSessionCommand, Session>, CreateSessionHandler>();

            services.AddScoped<ICommandHandler<CreateSessionCommand, Session>, CreateSessionHandler>();
            services.AddScoped<ICommandHandler<UpdateSessionCommand, Session>, UpdateSessionHandler>();

            services.AddScoped<ICommandHandler<CreateReservationCommand>, CreateReservationHandler>();
            services.AddScoped<ICommandHandler<DeleteReservationCommand>, DeleteReservationHandler>();
            services.AddScoped<IQueryHandler<ReadReservationsQuery, IEnumerable<Reservation>>, ReadReservationsHandler>();
            services.AddScoped<IQueryHandler<ReadReservationsBySeasonBySeatIdsQuery, IEnumerable<Reservation>>, ReadReservationsBySeasonBySeatIdsHandler>();

            services.AddScoped<ICommandHandler<CreateUserCommand, string>, CreateUserHandler>();
            services.AddScoped<IQueryHandler<ReadUsersQuery, IEnumerable<User>>, ReadUsersHandler>();

            services.AddScoped<IQueryHandler<ReadSeatsBySeasonQuery, IEnumerable<Guid>>, ReadSeatsBySeasonHandler>();
            services.AddScoped<IQueryHandler<ReadSeatsQuery, IEnumerable<Seat>>, ReadSeatsHandler>();

            services.AddScoped<ICommandHandler<CreateSeasonCommand, Guid>, CreateSeasonHandler>();
            services.AddScoped<ICommandHandler<UpdateSeasonCommand>, UpdateSeasonHandler>();
            services.AddScoped<ICommandHandler<DeleteSeasonCommand>, DeleteSeasonHandler>();
            services.AddScoped<IQueryHandler<ReadSeasonsQuery, IEnumerable<Season>>, ReadSeasonsHandler>();
            services.AddScoped<IQueryHandler<ReadSeasonQuery, Season>, ReadSeasonHandler>();

            services.AddScoped<ICommandHandler<CreateRoomCommand, Guid>, CreateRoomHandler>();
            services.AddScoped<ICommandHandler<UpdateRoomCommand>, UpdateRoomHandler>();
            services.AddScoped<ICommandHandler<DeleteRoomCommand>, DeleteRoomHandler>();
            services.AddScoped<IQueryHandler<ReadRoomsQuery, IEnumerable<Room>>, ReadRoomsHandler>();
            services.AddScoped<IQueryHandler<ReadRoomQuery, Room>, ReadRoomHandler>();

            return services;
        }
    }
}
