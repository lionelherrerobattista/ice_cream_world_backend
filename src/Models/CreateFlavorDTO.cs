namespace ice_cream_world_backend.models
{
    public class CreateFlavorDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
    }
}