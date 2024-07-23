using AdminPortal.Data;
using AdminPortal.Mappings;
using AdminPortal.Middleware;
using AdminPortal.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ÊÂ–« Ì”«⁄œ ›Ì „—«ﬁ»… «·√œ«¡° «ﬂ ‘«› «·√Œÿ«¡° Ê Õ·Ì· «” Œœ«„ «· ÿ»Ìﬁ.ÊÌŸÂ— ⁄·Ï «·ﬂÊ‰”·
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/AdminPortal_Log.txt", rollingInterval:RollingInterval.Minute)
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(op =>
      op.UseSqlServer(builder.Configuration.GetConnectionString("myCon")));
builder.Services.AddDbContext<AuthDbContext>(op =>
      op.UseSqlServer(builder.Configuration.GetConnectionString("myCon")));

builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepositery, WalkRepository > ();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<AdminPortal.Middleware.ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//Serving Static Files  Œœ„… «·„·›«  «·À«» Â 
// Ìÿ·⁄ «·’Ê—Â »«·—«»ÿ  »⁄Â« ›Ì «·„ ’›Õ
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"

    //Â–« Ì⁄‰Ì https://localhost:7044/Images
});

app.MapControllers();

app.Run();
