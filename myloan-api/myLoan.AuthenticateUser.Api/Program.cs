using FluentResults;
using myLoan.AuthenticateUser.Api.Extension;
using myLoan.Infrastructure.Common;
using myLoan.Infrastructure.Identity;
using myLoan.Infrastructure.Persistence;
using System.Data.SqlTypes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddCommonServices();
builder.Services.AddPersistenceRegistration(builder.Configuration);

Result.Setup(cfg =>
{
    cfg.DefaultTryCatchHandler = exception =>
    {
        if (exception is SqlTypeException sqlException)
            return new ExceptionalError("Sql Fehler", sqlException);

        //if (exception is DomainException domainException)
        //    return new Error("Domain Fehler")
        //        .CausedBy(new ExceptionError(domainException.Message, domainException));

        return new Error(exception.Message);
    };
});


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
