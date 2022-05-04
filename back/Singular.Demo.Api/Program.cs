using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Singular.Demo.Api.Authorization;
using Singular.Demo.Api.Db;

var builder = WebApplication.CreateBuilder(args);

var authority = "https://dev-x3vx68sh.auth0.com/";
var audience = "https://localhost:7163";

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

    setupAction.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            ClientCredentials = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri($"{authority}oauth/token"),
                Scopes = new Dictionary<string, string> {
                { "create:phones", "Crear número telefónico" },
                { "update:phones", "Actualizar número telefónico" },
                { "delete:phones", "Eliminar número telefónico" },
                { "get:phones", "Obtener número telefónico" },
                { "list:phones", "Listar número telefónico" }
                }
            }
        }
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Name = JwtBearerDefaults.AuthenticationScheme,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
                Reference = new OpenApiReference  {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new List<string>()
        }
    });
});

#endregion

#region Storage

/*
var connectionString = "Data Source=Phones.db";
builder.Services.AddSqlite<DbPhonesDbContext>(connectionString);
*/

var connectionString = "Server=.;Initial Catalog=Phones;User ID=sa;Password=Pass@word1;";
builder.Services.AddSqlServer<DbPhonesDbContext>(connectionString);

#endregion

#region Security

// 1. Add Authentication Services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = authority;
    options.Audience = audience;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = audience
    };
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("create:phones", policy => policy.Requirements.Add(new HasScopeRequirement("create:phones", authority)));
    options.AddPolicy("update:phones", policy => policy.Requirements.Add(new HasScopeRequirement("update:phones", authority)));
    options.AddPolicy("delete:phones", policy => policy.Requirements.Add(new HasScopeRequirement("delete:phones", authority)));
    options.AddPolicy("get:phones", policy => policy.Requirements.Add(new HasScopeRequirement("get:phones", authority)));
    options.AddPolicy("list:phones", policy => policy.Requirements.Add(new HasScopeRequirement("list:phones", authority)));
});

#endregion

var app = builder.Build();

#region Documentación

app.UseSwagger();
app.UseSwaggerUI(C =>
{
    C.SwaggerEndpoint("/swagger/v1/swagger.json", "Singular API v1");
    //Security
    C.OAuthClientId("xbgvJxhbjNdYnU63tVrfWZzTuplepIbe");
    C.OAuthClientSecret("KR6_IZa2W5w3MJiuwftIqKyRETcZ_QcgPsn9ccX_y4K0c470LcUsYt0Tb-MxTLPC");
    C.UseRequestInterceptor("(req) => { if (req.url.endsWith('oauth/token') && req.body) req.body += '&audience=" + audience + "'; return req; }");
});

#endregion

#region Routing

app.MapControllers();

#endregion

#region Security

// 2. Enable authentication middleware
app.UseAuthentication();
app.UseAuthorization();

#endregion

app.Run();
