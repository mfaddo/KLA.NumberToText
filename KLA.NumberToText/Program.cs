using KLA.NumberToText.NumberConverter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the static data service as a singleton
builder.Services.AddSingleton<IStaticNumbersService, StaticNumbersService>();

//Register the Number convertor service 
builder.Services.AddScoped<INumberConverterService, NumberConverterService>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
