using Microsoft.AspNetCore.Mvc;
using TrainingCenter.DTOs;
using TrainingCenter.Exceptions;
using TrainingCenter.Services;

namespace TrainingCenter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(IReservationService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        if (date.HasValue || status is not null || roomId.HasValue)
            return Ok(service.GetFiltered(date, status, roomId));

        return Ok(service.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            return Ok(service.GetById(id));
        }
        catch (ReservationNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateReservationDto dto)
    {
        try
        {
            var created = service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (RoomInactiveException e)
        {
            return BadRequest(e.Message);
        }
        catch (ReservationConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateReservationDto dto)
    {
        try
        {
            return Ok(service.Update(id, dto));
        }
        catch (ReservationNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (RoomInactiveException e)
        {
            return BadRequest(e.Message);
        }
        catch (ReservationConflictException e)
        {
            return Conflict(e.Message);
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
        catch (ReservationNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
