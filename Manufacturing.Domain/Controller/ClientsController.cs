using System;
using Manufacturing.Domain.Multitenancy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Manufacturing.Domain.Controller
{
    [ApiController]
    [Route("api/Clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
    public ClientsController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("GetClientName")]
    public IActionResult GetClientName()
    {
        var ClientName = _httpContextAccessor.HttpContext.GetClient().CompanyCode.ToLowerInvariant();
        return Ok(ClientName);
    }
}
}
