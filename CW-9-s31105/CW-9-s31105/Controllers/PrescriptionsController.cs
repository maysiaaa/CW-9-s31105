using CW_9_s31105.DTOs;
using CW_9_s31105.Exceptions;
using CW_9_s31105.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_9_s31105.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController(IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateDTO prescriptionData)
    {
        try
        {
            service.CreatePrescriptionAsync(prescriptionData);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (PastDueDateException e)
        {
            return NotFound(e.Message);
        }
        catch (TooManyMedicamentsException e)
        {
            return NotFound(e.Message);
        }
    }
}