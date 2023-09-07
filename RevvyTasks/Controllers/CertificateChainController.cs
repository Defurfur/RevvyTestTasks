using FluentValidation;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using RevvyTasks.Models;
using RevvyTasks.Abstractions;
using System.Web.Http.Results;
using RevvyTasks.Dtos;

namespace RevvyTasks.Controllers;
[ApiController]
[Route("[controller]")]
public class CertificateChainController : ControllerBase
{

    private readonly ILogger<CertificateChainController> _logger;
    private readonly ICertificateChainCreator _certificateChainCreator;
    private readonly IValidator<IClerkCollection> _clerkCollectionValidator;

    public CertificateChainController(
        ILogger<CertificateChainController> logger,
        ICertificateChainCreator certificateChainCreator,
        IValidator<IClerkCollection> clerkCollectionValidator)
    {
        _logger = logger;
        _certificateChainCreator = certificateChainCreator;
        _clerkCollectionValidator = clerkCollectionValidator;
    }

    [HttpPost]
    public ActionResult<ICertificateChain> Post([FromBody] ClerkCollectionDto clerkCollectionDto)
    {
        IClerkCollection clerkCollection = new ClerkCollection(
            clerkCollectionDto.Clerks.Select(x => new Clerk(x.Id, x.ClerkCertificatesRequired) as IClerk).ToList());

        ICertificateChain chain;

        var validationResult = _clerkCollectionValidator.Validate(clerkCollection);

        if (!validationResult.IsValid)
        {
            var message = $"Validation Failed. Validator returned following errors: \r\n " +
                string.Join(
                        "r\\n\\",
                        validationResult.Errors.Select(x => x.ToString()));

            _logger.LogWarning(message);

            return BadRequest(message);
        }

        try
        {
            chain = _certificateChainCreator.CreateChain(clerkCollection);
        }
        catch(Exception ex)
        {
            _logger.LogError("{ExceptionName} occurred while " +
                "trying to create a chain for the clerkCollection with Id - {ClerkCollectionId}." +
                "\r\nFull message: \r\n {FullException}",
                ex.GetType().Name,
                clerkCollection.Id,
                ex.ToString());

            var result = new ObjectResult(ex.ToString());
            result.StatusCode = 500;

            return result;
        }
       

         return new OkObjectResult(chain);
    }
}
