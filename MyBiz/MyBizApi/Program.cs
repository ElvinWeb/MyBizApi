using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MyBizApi.DataAccessLayer;
using MyBizApi.DTO.ProfessionDtos;
using MyBizApi.MappingProfile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssembly(typeof(ProfessionCreateDtoValidator).Assembly);
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myDb1"));
});
builder.Services.AddAutoMapper(typeof(MapProfile));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
