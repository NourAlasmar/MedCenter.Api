using MedCenter.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();
app.UseAppPipeline(app.Environment);
app.Run();