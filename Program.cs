using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
using ToDoManagerAPI.Data;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDatabase"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  // Only needed for minimal APIs
//builder.Services.AddMvc();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<ToDoDbContext>(options => options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
//var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");

//builder.Services.AddDbContext<ToDoDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDatabase"))
//builder.Services.AddDbContext<ToDoDbContext>(options =>
//    options.UseSqlite(connectionString)
//);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    //app.UseSwagger();
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapControllers();
app.Run();

