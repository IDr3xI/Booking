using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infrastructure")
    )
);
// Repositories & services
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<ISeatRepository, SeatRepository>();
//builder.Services.AddScoped<ISeatService, SeatService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapRazorPages();

app.MapFallbackToPage("/_Host");

app.Run();
