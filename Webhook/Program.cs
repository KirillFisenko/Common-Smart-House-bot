using Common_Smart_House_bot.Firebase;
using Common_Smart_House_bot.Storage;
using Firebase.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<UserStateStorage>();
builder.Services.AddSingleton<FirebaseProvider>();
builder.Services.AddSingleton<FirebaseClient>(services =>
{
    const string BasePath = "https://common-smart-house-bot-default-rtdb.firebaseio.com/";
    const string Secret = "IPQf8F887AUMm2eHcEbig11BV6ypcoAQhqRCxMHW";
    return new FirebaseClient(BasePath, new FirebaseOptions
    {
        AuthTokenAsyncFactory = () => Task.FromResult(Secret)
    });
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
