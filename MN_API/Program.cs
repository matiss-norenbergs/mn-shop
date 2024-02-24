using MN_API.Services;

var MyPolicy = "MN_Policy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddSingleton<UserService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(MyPolicy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
