using Microsoft.AspNetCore.Mvc;
using TrainingCenter.DTOs;
using TrainingCenter.Exceptions;
using TrainingCenter.Services;

namespace TrainingCenter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(IRoomService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        if (minCapacity.HasValue || hasProjector.HasValue || activeOnly.HasValue)
            return Ok(service.GetFiltered(minCapacity, hasProjector, activeOnly));

        return Ok(service.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            return Ok(service.GetById(id));
        }
        catch (RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuilding([FromRoute] string buildingCode)
    {
        return Ok(service.GetByBuilding(buildingCode));
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateRoomDto dto)
    {
        var created = service.Add(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateRoomDto dto)
    {
        try
        {
            return Ok(service.Update(id, dto));
        }
        catch (RoomNotFoundException e)
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
        catch (RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (RoomHasFutureReservationsException e)
        {
            return Conflict(e.Message);
        }
    }
}
