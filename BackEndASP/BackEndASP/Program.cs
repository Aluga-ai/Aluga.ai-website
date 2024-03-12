using BackEndASP.Interfaces;
using BackEndASP.Services;
using DSLearn.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




/* Database Context Dependency Injection */
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=True";
builder.Services.AddDbContext<SystemDbContext>(opt => opt.UseSqlServer(connectionString));
//builder.Configuration.GetConnectionString("DefaultConnection"))
/* ===================================== */



// Configura��o do banco de dados com usu�rios e fun��es
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Configura��es de normaliza��o
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 "; // Adicione ou remova caracteres conforme necess�rio
    options.User.RequireUniqueEmail = true; // Garante que os emails sejam �nicos
})
    .AddEntityFrameworkStores<SystemDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWork>();
builder.Services.AddScoped<ITokenRepository, TokenService>();
builder.Services.AddScoped<IPropertyRepository, PropertyService>();





// Configura��o JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var secretKey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("Invalid Secret key");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

// Configura��o e cria��o de pol�ticas de acesso
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("OwnerOnly", policy => policy.RequireRole("Owner"));
    options.AddPolicy("StudentOnly", policy => policy.RequireRole("Student"));
    options.AddPolicy("StudentOrOwner", policy => policy.RequireRole("Student", "Owner"));
});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Configura��es Swagger
builder.Services.AddSwaggerGen(c =>
{
    // Configura��o de autentica��o do Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DSCommerce",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Joao Silveira",
            Email = "joaoadsistemas@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/this-joao/")
        }
    });
});



var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SystemDbContext>();

    // Lista de todas as entidades que voc� deseja verificar
    var entitiesToCheck = new List<Type>
    {
        typeof(Building),
        typeof(College),
        typeof(Image),
        typeof(Notification),
        typeof(Owner),
        typeof(Property),
        typeof(Student),
        typeof(User)
    };

    foreach (var entity in entitiesToCheck)
    {
        var tableExists = dbContext.Model.FindEntityType(entity) != null;

        if (!tableExists)
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
                break; // Se uma migra��o for aplicada, n�o h� necessidade de verificar outras entidades
            }
        }
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
