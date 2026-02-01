namespace ETicketing.CA.API.Models.Responses
{
    public class SeatResponse
    {
        public Guid Id { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public Guid RoomId { get; set; }
    }
}
