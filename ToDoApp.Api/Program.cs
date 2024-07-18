using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Services.Interfaces;
using ToDoApp.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToDoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoAppDb"));
});

builder.Services.AddScoped<IToDoItemService, ToDoItemService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserServiceMock>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
