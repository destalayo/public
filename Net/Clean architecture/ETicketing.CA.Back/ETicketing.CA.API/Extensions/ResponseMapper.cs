using ETicketing.CA.API.Models.Responses;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using ETicketing.CA.Domain.Models.Seats;
using ETicketing.CA.Domain.Models.Sessions;
using ETicketing.CA.Domain.Models.Users;
using Microsoft.AspNetCore.Http.Features;

namespace ETicketing.CA.API.Extensions
{
    public static class ResponseMapper
    {
        public static ReservationResponse ToResponse(this Reservation item) => new ReservationResponse { Id = item.Id, SeasonId=item.SeasonId, UserId=item.UserId, SeatId= item.SeatId };
        public static SeatResponse ToResponse(this Seat item) => new SeatResponse { Id=item.Id, Row=item.Row, Column=item.Column, RoomId=item.RoomId};
        public static RoomResponse ToResponse(this Room item) => new RoomResponse { Id = item.Id,Name=item.Name, Rows=item.Rows, Columns=item.Columns };
        public static UserResponse ToResponse(this User item) => new UserResponse { Id = item.Id };
        public static SeasonResponse ToResponse(this Season item) => new SeasonResponse { Id = item.Id, Name=item.Name, RoomId=item.RoomId};
        public static SessionResponse ToResponse(this Session item) => new SessionResponse { AccessToken = item.AccessToken, RefreshToken=item.RefreshToken, AccessTokenExpires=item.AccessTokenExpires };
    }
}
