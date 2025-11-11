using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Data.Seed;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Web.Components;
using Web.Pages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories & services
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Seed database
DatabaseSeeder.EnsureDatabaseCreated(app.Services);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapRazorPages();

// Fallback must point to the host page that renders the Blazor app
app.MapFallbackToPage("/_Host");

app.Run();
