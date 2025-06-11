namespace ice_cream_world_backend.models
{
    public class UpdateFlavorDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
    }
}