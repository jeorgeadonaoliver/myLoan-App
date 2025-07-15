using Microsoft.AspNetCore.Mvc;
using myLoan.Application.Common;
using myLoan.Application.Features.Auth.Command.Login;
using myLoan.Application.Features.Auth.Command.Register;
using myLoan.Application.Interface.Request;

namespace myLoan.RegisterUser.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRequestDispatcher _request;
        public AuthController(IRequestDispatcher request)
        {
            _request = request;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            var data = await _request.Send(command, cancellationToken);
            return Ok(PayloadHandler.CreatePayload(data.Value));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            var data = await _request.Send(command, cancellationToken);
            return Ok(PayloadHandler.CreatePayload(data.Value));
        }

        [HttpGet("hello")]
        public IActionResult Hello() 
        {
            return Ok("Hello yarp! :)");
        }
    }
}
