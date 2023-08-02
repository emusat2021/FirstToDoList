using FirstToDoListBlazor.Services;
using FirstToDoListBlazor.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration
    // .SetBasePath(Path.Combine(AppContext.BaseDirectory))
    .AddIniFile("config.env", optional: true)
    // .AddEnvironmentVariables()
    .Build();

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/");
});
builder.Services.AddServerSideBlazor();

var connectionString = config["DB_CONNECTION_STRING"];

//Singleton and Service can be used to save in memory if is choosed in stead of ex.DB;
// builder.Services.AddSingleton<ToDoServicesMemory>();

var database = new Database(connectionString);
database.EnsureJsonStandardTable(Tables.FirstToDoListMemory).Wait();

builder.Services.AddTransient<Database>(x=>new Database(connectionString));
builder.Services.AddSingleton<ToDoServicePGAdmin>();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
