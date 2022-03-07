using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Services;
using PMS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IEmployeePaymentService, EmployeePaymentService>();
builder.Services.AddScoped<IEmployeePaymentRepository, EmployeePaymentRepository>();


builder.Services.AddScoped<IEmployeeProjectService, EmployeeProjectService>();
builder.Services.AddScoped<IEmployeeProjectRepository, EmployeeProjectRepository>();


builder.Services.AddScoped<IGlobalCodeService, GlobalCodeService>();
builder.Services.AddScoped<IGlobalCodeRepository, GlobalCodeRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();


builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

builder.Services.AddScoped<IProjectPaymentService, ProjectPaymentService>();
builder.Services.AddScoped<IProjectPaymentRepository, ProjectPaymentRepository>();

builder.Services.AddScoped<ICompanyExpenseService, CompanyExpenseService>();
builder.Services.AddScoped<ICompanyExpenseRepository, CompanyExpenseRepository>();


builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
