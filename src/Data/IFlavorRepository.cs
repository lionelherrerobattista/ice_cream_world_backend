using ice_cream_world_backend.models;

namespace IceCreamWorld.Data;

public interface IFlavorRepository
{
    Task<List<FlavorDTO>> GetFlavorsAsync();
    Task<FlavorDTO?> GetFlavorByIdAsync(Guid id);
    Task<Flavor?> GetFlavorEntityAsync(Guid id);
    void AddFlavor(Flavor flavor);

    void UpdateFlavor(Flavor flavor);
    void RemoveFlavor(Flavor flavor);
    Task<bool> FlavorExists(Guid id);
    Task<bool> SaveChangesAsync();
}