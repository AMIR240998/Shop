using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}")]
public class BaseController : ControllerBase
{
}