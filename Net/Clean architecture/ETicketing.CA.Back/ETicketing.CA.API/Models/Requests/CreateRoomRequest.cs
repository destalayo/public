namespace ETicketing.CA.API.Models.Requests
{
    public class CreateRoomRequest
    {
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
    }
}
