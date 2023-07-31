using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using FirstToDoListBlazor.Services;
using FirstToDoListBlazor.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


var connectionString = "Host=container-todo;Port=80;Username=todolist-memory;Password=123456;Database=FirstToDoListMemory";

builder.Services.AddTransient<Database>(_=>new Database(connectionString));
builder.Services.AddSingleton<ToDoServicePGAdmin>();

var database = new Database(connectionString);
database.EnsureJsonStandardTable(Tables.FirstToDoListMemory).Wait();


//Singleton and Service can be used to save in memory if is choosed in stead of ex.DB;
// builder.Services.AddSingleton<ToDoServicesMemory>();

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
