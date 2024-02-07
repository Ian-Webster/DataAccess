using Microsoft.AspNetCore.Mvc;

namespace DataAccess.Example.Web.Controllers;

public class BaseController: ControllerBase
{
    protected CancellationToken Token => HttpContext.RequestAborted;
}