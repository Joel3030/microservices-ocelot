using identity;
using identity.DTOs;
using identity.services;
using identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT token with the prefix Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.Configure<SwaggerGeneratorOptions>(options =>
{
    options.InferSecuritySchemes = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

var users = app.MapGroup("/api/user").WithTags("User");
var auth = app.MapGroup("/api/auth").WithTags("Auth");

users.MapPost("/create", (IUserService userService,
    [FromBody] CreateUserDTO createUserDTO) => userService.Create(createUserDTO)).RequireAuthorization();

users.MapGet("/", (IUserService userService) => userService.FindAll()).RequireAuthorization();

users.MapGet("/{id:Guid}", (IUserService userService, Guid id) => userService.FindOne(id)).RequireAuthorization();

users.MapPut("/update/{id:Guid}", (IUserService userService,
    Guid id, [FromBody] UpdateUserDTO updateUserDTO) => userService.Update(id, updateUserDTO)).RequireAuthorization();

users.MapDelete("/remove/{id:Guid}", (IUserService userService,
    Guid id) => userService.Remove(id)).RequireAuthorization();

auth.MapPost("/login", (IAuthService authService,
    LoginDTO loginDTO) => authService.Login(loginDTO));

app.Run();

