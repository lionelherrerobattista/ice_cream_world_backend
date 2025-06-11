using ice_cream_world_backend.models;
using IceCreamWorld.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ice_cream_world_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FlavorController : ControllerBase
    {
        private readonly IFlavorRepository flavorRepository;

        public FlavorController(IFlavorRepository flavorRepository)
        {
            this.flavorRepository = flavorRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FlavorDTO>> GetFlavor(Guid id)
        {
            var flavor = await flavorRepository.GetFlavorByIdAsync(id);

            if (flavor == null)
                return NotFound();

            return flavor;
        }

        [HttpGet]
        public async Task<ActionResult<List<FlavorDTO>>> GetFlavors()
        {
            return await flavorRepository.GetFlavorsAsync();
        }

        [HttpPost]
        public async Task<ActionResult<FlavorDTO>> CreateFlavor(CreateFlavorDTO flavor)
        {
            var newFlavor = CreateFlavorDTOToFlavor(flavor);

            flavorRepository.AddFlavor(newFlavor); // id added by EF when created

            var result = await flavorRepository.SaveChangesAsync();

            if (!result)
                return BadRequest("Could not save changes to the DB");

            var flavorDTO = FlavorToDTO(newFlavor);

            return CreatedAtAction(nameof(GetFlavor), new { id = newFlavor.Id }, flavorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFlavor(Guid id, UpdateFlavorDTO flavorToUpdate)
        {
            if (id != flavorToUpdate.Id)
                return BadRequest();

            var flavor = UpdateFlavorDTOToFlavor(flavorToUpdate);

            flavorRepository.UpdateFlavor(flavor);

            try
            {
                await flavorRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await flavorRepository.FlavorExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlavor(Guid id)
        {
            var flavor = await flavorRepository.GetFlavorEntityAsync(id);

            if (flavor == null)
                return NotFound();

            flavorRepository.RemoveFlavor(flavor);

            await flavorRepository.SaveChangesAsync();

            return NoContent();
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

        private static Flavor UpdateFlavorDTOToFlavor(UpdateFlavorDTO flavor)
        {
            return new Flavor
            {
                Id = flavor.Id,
                Name = flavor.Name,
                Description = flavor.Description,
                Photo = flavor.Photo
            };
        }


        private static Flavor CreateFlavorDTOToFlavor(CreateFlavorDTO flavor)
        {
            return new Flavor
            {
                Name = flavor.Name,
                Description = flavor.Description,
                Photo = flavor.Photo
            };
        }
    }
}