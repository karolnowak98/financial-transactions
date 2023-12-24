using server.Infrastructure;
using server.Operations;
using server.Web;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddInfrastructureServices(builder.Configuration);
services.AddOperationsServices();
services.AddWebServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.InitializeDatabaseAsync();
}
else
{
    app.UseDefaultExceptionHandler();
    app.UseHsts();
}

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseFastEndpoints();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Run();