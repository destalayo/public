namespace ETicketing.CA.API.Models.Responses
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid SeasonId { get; set; }
        public Guid SeatId { get; set; }
    }
}
