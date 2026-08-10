using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Tpo_DotNet_bb.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected int ObtenerIdCliente()
        {
            var claim = User.FindFirst("IDCLIENTE");

            if (claim == null)
                throw new UnauthorizedAccessException("No se encontró el claim IDCLIENTE.");

            return int.Parse(claim.Value);
        }
    }
}