using Microsoft.AspNetCore.Mvc;
using RevvyTasks.Abstractions;
using RevvyTasks.Models;

namespace RevvyTasks.Controllers;
[ApiController]
[Route("[controller]")]
public class SumRepresentationController : ControllerBase
{
    private readonly ILogger<SumRepresentationController> _logger;
    private readonly ISumRepresentationFinder _sumRepresentationFinder;

    public SumRepresentationController(
        ILogger<SumRepresentationController> logger,
        ISumRepresentationFinder sumRepresentationFinder)
    {
        _logger = logger;
        _sumRepresentationFinder = sumRepresentationFinder;
    }

    [HttpPost]
    public ActionResult Post([FromBody] FindSumRepresentationRequest request)
    {
        bool canBeRepresented;

        try
        {
            canBeRepresented = _sumRepresentationFinder
                .SumCanBeRepresentedFromArray(request.Array, request.Sum);
        }
        catch(Exception ex)
        {
            _logger.LogError("{ExceptionName} occurred during SumCanBeRepresentedFromArray method execution" +
                "with the following request - {Request}" +
                "\r\nFull message: \r\n {FullException}",
                ex.GetType().Name,
                request.ToString(),
                ex.ToString());

            var internalServerError = new ObjectResult(ex.ToString());
            internalServerError.StatusCode = 500;

            return internalServerError;
        }

        var result = new FindSumRepresentationResult(request, canBeRepresented);

        return Ok(result);
    }
}
