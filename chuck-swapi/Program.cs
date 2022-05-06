using chuck_swapi.ApplicationLib.Config;
using chuck_swapi.ApplicationLib.Core;
using chuck_swapi.ApplicationLib.Modules.Chuck;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(CategoryList.QueryList).Assembly);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IApiCall, ApiCall>();
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", p => p.AllowAnyOrigin()));
//p.WithOrigins("https://localhost:7127/", "https://localhost:3000/").WithMethods("GET")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
