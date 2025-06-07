namespace ice_cream_world_backend.models
{
    public class ServingContainer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<Flavor> Flavors { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Photo { get; set; }
    }
}