using myLoan.Api.AuthenticateUser.Endpoint;
using myLoan.Api.AuthenticateUser.ServiceExtention;
using myLoan.AuthenticateUser.Api.Extension;
using myLoan.Infrastructure.Common;
using myLoan.Infrastructure.Identity;
using myLoan.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceRegistration(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddRequestService();
builder.Services.AddAuthenticationService();

builder.Services.AddCorsService();

//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.ApplyMigrations();
}

app.UseHttpsRedirection();

//app.MapIdentityApi<ApplicationUser>();

app.UseCors("AllowAllOrigins");

//app.UseAuthorization();

//app.MapControllers();
app.MapAuthEndpoint();

app.Run();
