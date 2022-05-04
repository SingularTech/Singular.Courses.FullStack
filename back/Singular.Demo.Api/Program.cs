using Singular.Demo.Api.Db;

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

#region Storage

/*
var connectionString = "Data Source=Phones.db";
builder.Services.AddSqlite<DbPhonesDbContext>(connectionString);
*/

var connectionString = "Server=.;Initial Catalog=Phones;User ID=sa;Password=Pass@word1;";
builder.Services.AddSqlServer<DbPhonesDbContext>(connectionString);

#endregion

#endregion

var app = builder.Build();

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
