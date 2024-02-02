using Microsoft.EntityFrameworkCore;
using WebAPIClass.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
//builder.Services.AddControllers();
builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBConnection"));
});

//use setting api is open to sth
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder=>builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod()
);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


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

//use
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
