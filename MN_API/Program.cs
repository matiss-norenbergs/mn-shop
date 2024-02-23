using Microsoft.EntityFrameworkCore;
using MN_API.Models;

var MyPolicy = "MN_Policy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:3000").WithMethods("GET", "PUT", "DELETE");
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("UserList"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors(MyPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
