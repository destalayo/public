using ETicketing.CA.API.Models.Requests;
using ETicketing.CA.API.Models.Responses;
using ETicketing.CA.Application.UserCases.Reservations.CreateReservation;
using ETicketing.CA.Application.UserCases.Rooms.CreateRoom;
using ETicketing.CA.Application.UserCases.Rooms.UpdateRoom;
using ETicketing.CA.Application.UserCases.Seasons.CreateSeason;
using ETicketing.CA.Application.UserCases.Seasons.UpdateSeason;
using ETicketing.CA.Domain.Models.Seats;

namespace ETicketing.CA.API.Extensions
{
    public static class RequestMapper {
        public static CreateReservationCommand ToDTO(this CreateReservationRequest item, string userId) => new CreateReservationCommand(userId, item.SeasonId, item.SeatIds);
        public static CreateRoomCommand ToDTO(this CreateRoomRequest item) => new CreateRoomCommand (item.Name, item.Rows, item.Columns);
        public static UpdateRoomCommand ToDTO(this UpdateRoomRequest item, Guid id) => new UpdateRoomCommand(id,item.Name);
        public static CreateSeasonCommand ToDTO(this CreateSeasonRequest item) => new CreateSeasonCommand(item.RoomId, item.Name);
        public static UpdateSeasonCommand ToDTO(this UpdateSeasonRequest item, Guid id) => new UpdateSeasonCommand(id, item.Name);
    }
}
