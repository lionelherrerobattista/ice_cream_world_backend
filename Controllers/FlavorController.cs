using ice_cream_world_backend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ice_cream_world_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FlavorController : ControllerBase
    {
        private readonly IceCreamWorldContext context;

        public FlavorController(IceCreamWorldContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flavor>> GetFlavor(int id)
        {
            var flavor = await context.Flavors.FirstOrDefaultAsync(x => x.Id == id);

            if (flavor == null)
                return NotFound();

            return flavor;
        }

        [HttpGet]
        public async Task<ActionResult<List<Flavor>>> GetFlavors()
        {
            return await context.Flavors.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Flavor>> CreateFlavor(CreateFlavorDTO flavor)
        {
            var newFlavor = new Flavor
            {
                Name = flavor.Name,
                Description = flavor.Description,
                Photo = flavor.Photo
            };

            context.Add(newFlavor);

            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFlavor), new { id = newFlavor.Id }, newFlavor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFlavor(int id, Flavor flavor)
        {
            if (id != flavor.Id)
                return BadRequest();

            context.Entry(flavor).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FlavorExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlavor(int id)
        {
            var flavor = await context.Flavors.FindAsync(id);

            if (flavor == null)
                return NotFound();

            context.Flavors.Remove(flavor);

            await context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> FlavorExists(int id)
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
}