using System.Text;
using Microsoft.EntityFrameworkCore;
using PortManagementSystem.BLL.Managers;
using PortManagementSystem.BLL.Services;
using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Models;
using PortManagementSystem.DAL.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Swagger 
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        });


        // Dependency Injection
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITerminalRepository, TerminalRepository>();
        builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
        builder.Services.AddScoped<IShipRepository, ShipRepository>();
        builder.Services.AddScoped<IShipServices, ShipServices>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddDbContext<ProgramContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var key = Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:TokenKey"]);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "JwtBearer";
            options.DefaultChallengeScheme = "JwtBearer";
        }).AddJwtBearer("JwtBearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin"));
            options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
        });

        var app = builder.Build();

        //Seed default admin user 3shan m3ndnash register 
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ProgramContext>();
            context.Database.EnsureCreated();

            // Check if no users exist
            if (!context.Users.Any())
            {
                var hmac = new HMACSHA512();
                var admin = new User
                {
                    username = "admin",
                    email = "admin@example.com",
                    role = "Admin",
                    PasswordSalt = hmac.Key,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Admin123@"))
                };

                context.Users.Add(admin);
                context.SaveChanges();
                Console.WriteLine("Admin user seeded successfully!");
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
