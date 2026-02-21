using Application.Logging;
using Infrastructure;
using Tailwind;
using Web.App;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Starting myfanwy...");


builder.WebHost.ConfigureKestrel(options =>
{
    // remove the Server header for security reasons
    options.AddServerHeader = false;
});

builder.Services.AddBlazorBootstrap();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddApplicationPart(typeof(Api.Api).Assembly);

builder.Services.ConfigureLogging();
builder.Services.AddInfrastructureBlocks(isUsingDocker: !builder.Environment.IsDevelopment());

Console.WriteLine("Loading modules...");
builder.Services.LoadModules(isDevelopment: !builder.Environment.IsDevelopment());
builder.Services.LoadConfigurations(builder);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    _ = app.RunTailwind("watch", "./");
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders
app.UseSecurityHeaders();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

Console.WriteLine("Application started.");
app.Run();