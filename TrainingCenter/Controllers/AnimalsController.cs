using TrainingCenter.DTOs;
using TrainingCenter.Exceptions;
using TrainingCenter.Services;
using Microsoft.AspNetCore.Mvc;

namespace TrainingCenter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController(IAnimalService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] string? species)
    {
        return Ok(service.GetAll(species));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            return Ok(service.GetById(id));
        }
        catch (AnimalNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateAnimalDto animal)
    {
        var createdAnimal = service.Add(animal);
        
        return CreatedAtAction(
            nameof(GetById), 
            new { id = createdAnimal.Id },
            createdAnimal
        );
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(
        [FromRoute] int id, 
        [FromBody] UpdateAnimalDto animal
    )
    {
        try
        {
            return Ok(service.Update(id, animal));
        }
        catch (AnimalNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            service.Remove(id);
            return NoContent();
        }
        catch (AnimalNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}