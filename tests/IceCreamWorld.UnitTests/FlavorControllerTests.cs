using AutoFixture;
using ice_cream_world_backend.Controllers;
using ice_cream_world_backend.models;
using IceCreamWorld.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IceCreamWorld.UnitTests;

public class FlavorControllerTests
{

    private readonly Mock<IFlavorRepository> _flavorRepository;
    private readonly Fixture _fixture;
    private readonly FlavorController _controller;

    public FlavorControllerTests()
    {
        _fixture = new Fixture();
        _flavorRepository = new Mock<IFlavorRepository>();

        _controller = new FlavorController(_flavorRepository.Object);
    }

    [Fact]
    public async Task GetFlavors_WithNoParams_Returns10Flavor()
    {
        // arrange
        // create 10 flavor objects
        var flavors = _fixture.CreateMany<FlavorDTO>(10).ToList();
        // setup mock repository
        _flavorRepository.Setup(repo => repo.GetFlavorsAsync()).ReturnsAsync(flavors);

        // act
        var result = await _controller.GetFlavors();

        //assert
        Assert.NotNull(result.Value);
        Assert.Equal(10, result.Value.Count);
        Assert.IsType<ActionResult<List<FlavorDTO>>>(result);
    }

    [Fact]
    public async Task GetFlavor_WithValidGuid_ReturnsFlavor()
    {
        // arrange
        var flavor = _fixture.Create<FlavorDTO>();
        _flavorRepository.Setup(repo => repo.GetFlavorByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(flavor);

        // act
        var result = await _controller.GetFlavor(flavor.Id);

        // assert
        Assert.NotNull(result.Value);
        Assert.Equal(flavor.Name, result.Value.Name);
        Assert.IsType<ActionResult<FlavorDTO>>(result);
    }

    [Fact]
    public async Task GetFlavor_WithInvalidGuid_ReturnsNotFound()
    {
        // arrange
        _flavorRepository.Setup(repo => repo.GetFlavorByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // act
        var result = await _controller.GetFlavor(Guid.NewGuid());

        // assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateFlavor_WithValidCreateFlavorDto_ReturnsCreated()
    {
        // arrange
        var flavor = _fixture.Create<CreateFlavorDTO>();

        _flavorRepository.Setup(repo => repo.AddFlavor(It.IsAny<Flavor>()));
        _flavorRepository.Setup(repo => repo.SaveChangesAsync())
            .ReturnsAsync(true);

        // act
        var result = await _controller.CreateFlavor(flavor);
        var createdResult = result.Result as CreatedAtActionResult;

        // assert
        Assert.NotNull(createdResult);
        Assert.Equal("GetFlavor", createdResult.ActionName);
        Assert.IsType<FlavorDTO>(createdResult.Value);
    }


}
