using ice_cream_world_backend.models;
using Microsoft.EntityFrameworkCore;

namespace IceCreamWorld.Data;

public class FlavorRepository : IFlavorRepository
{
    private readonly IceCreamWorldContext context;

    public FlavorRepository(IceCreamWorldContext context)
    {
        this.context = context;
    }
    public void AddFlavor(Flavor flavor)
    {
        context.Flavors.Add(flavor);
    }

    public async Task<FlavorDTO?> GetFlavorByIdAsync(Guid id)
    {
        var flavor = await context.Flavors.FirstOrDefaultAsync(x => x.Id == id);

        if (flavor == null)
            return null;

        return FlavorToDTO(flavor);
    }

    public async Task<Flavor?> GetFlavorEntityAsync(Guid id)
    {
        return await context.Flavors.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<FlavorDTO>> GetFlavorsAsync()
    {
        var flavorDTOList = new List<FlavorDTO>();

        var flavorList = await context.Flavors.ToListAsync();

        foreach (var flavor in flavorList)
        {
            flavorDTOList.Add(FlavorToDTO(flavor));
        }

        return flavorDTOList;
    }

    public void UpdateFlavor(Flavor flavor)
    {
        context.Entry(flavor).State = EntityState.Modified;
    }

    public void RemoveFlavor(Flavor flavor)
    {
        context.Flavors.Remove(flavor);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> FlavorExists(Guid id)
    {
        return await context.Flavors.AnyAsync(x => x.Id == id);
    }

    private static FlavorDTO FlavorToDTO(Flavor flavor)
    {
        return new FlavorDTO
        {
            Id = flavor.Id,
            Name = flavor.Name,
            Description = flavor.Description,
            Photo = flavor.Photo
        };
    }
}