using Microsoft.Extensions.Options;
using SignalRChat.Hubs;
using SignalRChat.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ConUserService>();

builder.Services.AddSignalR(options =>
{
    options.HandshakeTimeout = TimeSpan.FromMinutes(3);
    options.KeepAliveInterval = TimeSpan.FromHours(1);
    options.EnableDetailedErrors = true;
}).AddJsonProtocol();

// ¸óºô°ì
//builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// ¸óºô°ì
//app.UseCors(builder =>
//{
//    builder.WithOrigins("*")
//        .AllowAnyHeader()
//        .WithMethods("GET", "POST")
//        .AllowCredentials();
//});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

app.Run();
