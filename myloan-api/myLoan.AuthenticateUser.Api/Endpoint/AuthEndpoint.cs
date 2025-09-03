using myLoan.Application.Features.Auth.Command.Login;
using myLoan.Application.Features.Auth.Command.Register;
using myLoan.Application.Interface.Request;

namespace myLoan.Api.AuthenticateUser.Endpoint;

public static class AuthEndpoint
{
    public static IEndpointRouteBuilder MapAuthEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (RegisterCommand command, IRequestDispatcher mediator, CancellationToken cancellationToken) =>
        {
            var data = await mediator.Send(command, cancellationToken);
            return Results.Ok(myLoan.Application.Common.PayloadHandler.CreatePayload(data.Value));
        });
        app.MapPost("/auth/login", async (LoginCommand command, IRequestDispatcher mediator, CancellationToken cancellationToken) =>
        {
            var data = await mediator.Send(command, cancellationToken);
            return Results.Ok(myLoan.Application.Common.PayloadHandler.CreatePayload(data.Value));
        });
        app.MapGet("/auth/hello", () =>
        {
            return Results.Ok("Hello yarp! :)");
        });
        return app;
    }
}
