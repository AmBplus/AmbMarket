using ambMarket.Infrastructure.Hubs;
using ambMarket.Infrastructure.Utilities;
using Web.Endpoint.Infrastructure.WebUtility;

var builder = WebApplication.CreateBuilder(args);
// WireUp Infrastructure Services
builder.Services.WireUpInfrastructureServices(builder.Configuration);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddSingleton<WebEndpointUtility>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
//app.UseSetVisitorId();
app.MapHub<OnlineVisitor>("/OnlineVisitor");
app.Run();
