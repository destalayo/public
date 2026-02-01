namespace ETicketing.CA.API.Models.Responses
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
    }
}
