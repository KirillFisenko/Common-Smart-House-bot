using Common_Smart_House_bot;
using Webhook;

var builder = WebApplication.CreateBuilder(args);

ContainerConfigurator.Configure(builder.Configuration, builder.Services);

builder.Services.AddHostedService<WebHookConfigurator>();

builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();