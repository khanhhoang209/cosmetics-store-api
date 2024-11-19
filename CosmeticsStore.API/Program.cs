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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepostory>();

// Add services
builder.Services.AddScoped<ICategoryService, CategoryService>();


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
                message = "Thông tin yêu cầu không chính xác!",
                errors
            }
        };

        // Return a BadRequest with the custom response
        return new BadRequestObjectResult(errorResponse);
    };
});
builder.Services.AddSwaggerGen(options =>
{
    options.ParameterFilter<KebabCaseParameterFilter>();
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




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();

