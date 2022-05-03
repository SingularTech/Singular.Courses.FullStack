using Singular.Demo.Api.Db;
using Singular.Demo.Api.Models;

var builder = WebApplication.CreateBuilder(args);

#region Routing

builder.Services.AddControllers();

#endregion

#region Documentation

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Singular API",
            Description = "Documentación de API Singular",
            Version = "v1"
        }
     );
});

#endregion

var app = builder.Build();

/*app.MapPost("/phone", (Phone phone) => { DbPhones.Add(phone); });
app.MapPut("/phone", (Phone phone) => { DbPhones.Update(phone); });
app.MapDelete("/phone", (int id) => { DbPhones.Delete(id); });
app.MapGet("/phone", (int id) => { DbPhones.Get(id); });
app.MapGet("/phones", () => { DbPhones.List(); });
*/

#region Documentación

app.UseSwagger();
app.UseSwaggerUI(C =>
{
    C.SwaggerEndpoint("/swagger/v1/swagger.json", "Singular API v1");
});

#endregion

#region Routing

app.MapControllers();

#endregion

app.Run();
