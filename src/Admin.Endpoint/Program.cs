using ambMarket.Infrastructure.ConfigurationServices.AdminService;
using ambMarket.Infrastructure.Hubs;
using ambMarket.Infrastructure.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.WireUpInfrastructureServices(builder.Configuration);
builder.Services.BootstrapCustomAdminServices();
builder.Services.AddRazorPages();
//mapper
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

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<OnlineVisitor>("/OnlineVisitor");
app.Run();
