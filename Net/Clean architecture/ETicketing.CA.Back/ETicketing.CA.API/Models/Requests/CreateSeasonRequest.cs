namespace ETicketing.CA.API.Models.Requests
{
    public class CreateSeasonRequest
    {
        public string Name { get; set; }
        public Guid RoomId { get; set; }
    }
}
