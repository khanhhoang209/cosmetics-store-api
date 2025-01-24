using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CosmeticsStore.API.Configs;
using CosmeticsStore.Repositories.Context;
using CosmeticsStore.Repositories.Implements;
using CosmeticsStore.Repositories.Interfaces;
using CosmeticsStore.Repositories.Models.Domain;
using CosmeticsStore.Services.Implements;
using CosmeticsStore.Services.Interfaces;
using CosmeticsStore.Services.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

// Add services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        // Extract validation errors and customize response structure
        var errors = context.ModelState
            .Where(m => m.Value!.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key.ToLower(),  // Convert key (property name) to lowercase
                kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).FirstOrDefault()  // Get the first error message
            );

        // Define the custom error response structure
        var errorResponse = new
        {
            status = "fail",
            details = new
            {
                message = "Thông tin yêu cầu chưa chính xác hoặc không hợp lệ!",
                errors
            }
        };
        // Return a BadRequest with the custom response
        return new BadRequestObjectResult(errorResponse);
    };
});
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "cosmetics-store-api", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            []
        }
    });

    opt.ParameterFilter<KebabCaseParameterFilter>();
});
builder.Services
    .AddControllers(options =>
    {
        options.ModelBinderProviders.Insert(0, new KebabCaseModelBinderProvider());
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower;
    });

//Add DbContext
builder.Services.AddDbContext<CosmeticsStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CosmeticsStoreDb"));
});


//Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 1;
    })
    .AddDefaultTokenProviders()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CosmeticsStore")
    .AddEntityFrameworkStores<CosmeticsStoreDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientCors", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("Location");
    });
});

//Add Authentication
builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromMinutes(5));
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var _config = builder.Configuration;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireAudience = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
            ValidAudience = _config["Jwt:Audience"],
            ValidIssuer = _config["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new ArgumentException()))
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ClientCors");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();