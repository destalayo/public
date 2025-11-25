namespace GanttPert.API.Models.Request
{
    public class UpdateRelationRequest<T>
    {
        public T Id { get; set; }
        public bool IsAdd { get; set; }
    }
}
