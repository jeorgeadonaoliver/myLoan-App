using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myLoan.Application.Common;
using myLoan.Application.Features.Users.Command;
using myLoan.Application.Features.Users.Query.GetUsers;
using myLoan.Application.Features.Users.Query.GetUsersByEmail;
using myLoan.Application.Features.Users.Query.GetUsersById;
using myLoan.Application.Interface.Request;

namespace myLoan.Users.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRequestDispatcher _mediator;
        public UsersController(IRequestDispatcher mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new GetUsersQuery(), cancellationToken);
            return Ok(PayloadHandler.CreatePayload(data.Value));
        }

        [HttpGet("usersbyemail")]
        public async Task<IActionResult> GetUsersByEmail([FromQuery]string email, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new GetUsersByEmailQuery(email), cancellationToken);
            return Ok(PayloadHandler.CreatePayload(data.Value));
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUsers(int id, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new GetUsersByIdQuery(id), cancellationToken);
            return Ok(PayloadHandler.CreatePayload(data.Value));
        }

        [HttpPut("users")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand cmd, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(cmd, cancellationToken);
            return Ok(PayloadHandler.CreatePayload(data.Value));
        }
    }
}
