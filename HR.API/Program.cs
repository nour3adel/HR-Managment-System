using HR.Domain.Classes;
using HR.Domain.Classes.Identity;
using HR.Infrastructure;
using HR.Infrastructure.Context;
using HR.Infrastructure.Seeders;
using HR.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HRdbContext>(option =>
{
    option.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("HR_CON"));
});

builder.Services.AddInfrastructureDependencies()
                .AddServicesDependencies();
builder.Services.AddIdentity<Employee, Role>(option =>
{
    // Password settings.
    option.Password.RequireDigit = true;
    option.Password.RequireLowercase = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequiredLength = 6;
    option.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    option.Lockout.MaxFailedAccessAttempts = 5;
    option.Lockout.AllowedForNewUsers = true;

    // User settings.
    option.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    option.User.RequireUniqueEmail = true;
    //option.SignIn.RequireConfirmedEmail = true;

}).AddEntityFrameworkStores<HRdbContext>().AddDefaultTokenProviders();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await RoleSeeder.Seed(roleManager);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
