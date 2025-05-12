using DataAcess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Models.DTOs.Mapper;
using DataAcess.Repos;
using DataAcess.Repos.IRepos;
using Models.Domain;
using IdentityManagerAPI.Middlewares;
using IdentityManager.Services.ControllerService.IControllerService;
using IdentityManager.Services.ControllerService;
using IdentityManagerAPI.ControllerService.IControllerService;
using IdentityManagerAPI.ControllerService;
using IdentityManagerAPI.Repos.IRepos;
using IdentityManagerAPI.Repos;
using Microsoft.SemanticKernel;
using IdentityManagerAPI;
using OpenStreetMapAdapter = IdentityManagerAPI.ControllerService.OpenStreetMapAdapter;
using UserService = IdentityManagerAPI.ControllerService.UserService;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddKernel().AddOpenAIChatCompletion("gpt-4o-mini", builder.Configuration["ApiSettings:Secret"]);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(); 

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));


builder.Services.AddSignalR();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddHttpClient(); 

builder.Services.AddScoped<IGeolocationService, OpenStreetMapAdapter>();

/////////////////AddSingleton//////////////////////////
builder.Services.AddSingleton<NotificationService>();

builder.Services.AddScoped<IWorkerFacadeService, WorkerFacadeService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ServiceFactory>();

// Add Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWorkerFacadeService, WorkerFacadeService>();
builder.Services.AddScoped<IDistanceCalculationStrategy, GeolocationDistanceStrategy>();
builder.Services.AddScoped<IDistanceCalculationStrategy, DistanceAndRatingStrategy>();
builder.Services.AddScoped<DistanceStrategyFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Add Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();


// Add OpenAPI with Bearer Authentication Support
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

// Configure JWT Authentication insted of cookies
//var key = Encoding.ASCII.GetBytes(builder.Configuration["ApiSettings:Secret"]);
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ClockSkew = TimeSpan.FromDays(7)
//    };
//});


// Register the global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Use the global exception handler
app.UseExceptionHandler();


app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication(); 
app.UseAuthorization();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
//    RequestPath = "/Images"
//});

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");
app.Run();
