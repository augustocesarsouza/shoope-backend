using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shoope.Infra.Data.UtilityExternal;
using Shoope.Infra.IoC;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

// No início da aplicação, por exemplo em Program.cs ou Startup.cs
ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en");

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{ // Usar isso para os secrets, ir certo para cada "Pasta" "Shoope" Folder
  //builder.Configuration.AddUserSecrets(typeof(DependectyInjection).Assembly);
  //builder.Configuration.AddUserSecrets(typeof(SendEmailBrevo).Assembly);

    var targetDirectories = new List<string>
    {
        @"C:\Users\lucas\Desktop\shoope-backend\Shoope.Api\bin\Debug\net6.0",
        @"C:\Users\lucas\Desktop\shoope-backend\shoope.Application\bin\Debug\net6.0",
        @"C:\Users\lucas\Desktop\shoope-backend\Shoope.Domain\bin\Debug\net6.0",
        @"C:\Users\lucas\Desktop\shoope-backend\Shoope.Infra.Data\bin\Debug\net6.0",
        @"C:\Users\lucas\Desktop\shoope-backend\Shoope.Infra.IoC\bin\Debug\net6.0"
    };

    // Verificar se os assemblies estão carregados e carregar os secrets
    foreach (var dir in targetDirectories)
    {
        if (Directory.Exists(dir))
        {
            var dllFiles = Directory.GetFiles(dir, "*.dll");

            foreach (var dllFile in dllFiles)
            {
                var assembly = Assembly.LoadFrom(dllFile);
                builder.Configuration.AddUserSecrets(assembly);
            }
        }
    }
}

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolity", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

var keyJwtBearerSecret = builder.Configuration["key-jwt-bearer-secret"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJwtBearerSecret));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddSignalR();

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
//});

var app = builder.Build();

app.UseRouting();
app.UseCors("CorsPolity");

app.MapControllers();
app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<GeneralHub>("/generalhub");
//});

app.Run();
