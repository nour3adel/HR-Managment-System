using HR.Domain.Classes.Identity;
using HR.Infrastructure;
using HR.Infrastructure.Context;
using HR.Infrastructure.Seeders;
using HR.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Allow CORS
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{


    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
#endregion


#region SQL SERVER CONNECTION
builder.Services.AddDbContext<HRdbContext>(option =>
{
    option.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("HR_CON"));
});
#endregion

#region Dependency injections

builder.Services.AddInfrastructureDependencies()
                .AddServicesDependencies()
                .AddServiceRegisteration(builder.Configuration);

#endregion


var app = builder.Build();

#region Seeder

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await RoleSeeder.Seed(roleManager);
}

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    #region Swagger
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/All/swagger.json", "All");
        options.SwaggerEndpoint("/swagger/Employees/swagger.json", "Employees");
        options.SwaggerEndpoint("/swagger/Departments/swagger.json", "Departments");
        options.SwaggerEndpoint("/swagger/Attendances/swagger.json", "Attendances");
        options.SwaggerEndpoint("/swagger/LeaveRequests/swagger.json", "LeaveRequests");
        options.SwaggerEndpoint("/swagger/Notifications/swagger.json", "Notifications");
        options.SwaggerEndpoint("/swagger/Payroll/swagger.json", "Payroll");
        options.SwaggerEndpoint("/swagger/PerformanceReview/swagger.json", "PerformanceReview");
        options.SwaggerEndpoint("/swagger/Autherization/swagger.json", "Autherization");
        options.SwaggerEndpoint("/swagger/Authentication/swagger.json", "Authentication");
        options.RoutePrefix = "swagger";
        options.DisplayRequestDuration();
        options.DefaultModelsExpandDepth(-1);
    });
    #endregion
}
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
