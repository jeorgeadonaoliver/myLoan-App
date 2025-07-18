using myLoan.Infrastructure.Persistence;
using myLoan.Application.Service;
using myLoan.Users.Api.Extension;
using myLoan.Infrastructure.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceRegistration(builder.Configuration);
builder.Services.AddRequestService();
builder.Services.AddUsersService();
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
