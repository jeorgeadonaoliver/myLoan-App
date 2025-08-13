using FluentValidation;
using myLoan.Api.Users.Extension;
using myLoan.Application.Features.Auth.Command.Register;
using myLoan.Infrastructure.Common;
using myLoan.Infrastructure.Identity;
using myLoan.Infrastructure.Persistence;
using myLoan.Users.Api.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceRegistration(builder.Configuration);
builder.Services.AddUsersService();
builder.Services.AddRequestService();
builder.Services.AddCorsService();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
