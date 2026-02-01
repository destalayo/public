namespace ETicketing.CA.API.Models.Requests
{
    public class CreateReservationRequest
    {
        public Guid SeasonId { get; set; }
        public List<Guid> SeatIds { get; set; }
    }
}
