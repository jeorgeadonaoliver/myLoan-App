using myLoan.Application.Common;
using myLoan.Application.Features.Users.Command;
using myLoan.Application.Features.Users.Query.GetUsers;
using myLoan.Application.Features.Users.Query.GetUsersByEmail;
using myLoan.Application.Features.Users.Query.GetUsersById;
using myLoan.Application.Interface.Request;

namespace myLoan.Api.Users.Endpoint
{
    public static class UsersEndpoint
    {
        public static IEndpointRouteBuilder MapUsersEndpoint(this IEndpointRouteBuilder app)
        {

            app.MapGet("/users", async (IRequestDispatcher mediator, CancellationToken cancellationToken) =>
            {
                var data = await mediator.Send(new GetUsersQuery(), cancellationToken);
                return Results.Ok(PayloadHandler.CreatePayload(data.Value));
            });


            app.MapGet("/usersbyemail", async (string email, IRequestDispatcher mediator, CancellationToken cancellationToken) =>
            {
                var data = await mediator.Send(new GetUsersByEmailQuery(email), cancellationToken);
                return Results.Ok(PayloadHandler.CreatePayload(data.Value));
            });


            app.MapGet("/users/{id}", async (int id, IRequestDispatcher mediator, CancellationToken cancellationToken) =>
            {
                var data = await mediator.Send(new GetUsersByIdQuery(id), cancellationToken);
                return Results.Ok(PayloadHandler.CreatePayload(data.Value));
            });


            app.MapPost("/users", async (UpdateUserCommand cmd, IRequestDispatcher mediator, CancellationToken cancellationToken) =>
            {
                var data = await mediator.Send(cmd, cancellationToken);
                return Results.Ok(PayloadHandler.CreatePayload(data.Value));
            });

            return app;
        }
    }
}
