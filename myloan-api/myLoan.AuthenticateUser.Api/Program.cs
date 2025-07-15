using myLoan.AuthenticateUser.Api.Extension;
using myLoan.Infrastructure.Common;
using myLoan.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddCommonServices();

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

    //app.ApplyMigrations();
}

app.UseHttpsRedirection();

//app.MapIdentityApi<ApplicationUser>();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
