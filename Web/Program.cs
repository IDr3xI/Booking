using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(o => { o.DetailedErrors = true; });

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

// Identity & Authentication
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(o => o.DetailedErrors = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapRazorPages();

// AUTH endpoints – pøihlášení (GET/POST)
app.MapGet("/auth/signin", async (UserManager<User> userManager, SignInManager<User> signInManager, string email, string password) =>
{
    var user = await userManager.FindByEmailAsync(email);
    if (user is null) return Results.BadRequest("Uživatel neexistuje.");

    var signInResult = await signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);
    if (!signInResult.Succeeded) return Results.BadRequest("Pøihlášení se nezdaøilo.");

    return Results.Redirect("/");
});

app.MapPost("/auth/signin", async (UserManager<User> userManager, SignInManager<User> signInManager, string email, string password) =>
{
    var user = await userManager.FindByEmailAsync(email);
    if (user is null) return Results.BadRequest("Uživatel neexistuje.");

    var signInResult = await signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);
    if (!signInResult.Succeeded) return Results.BadRequest("Pøihlášení se nezdaøilo.");

    return Results.Redirect("/");
});

// AUTH endpoints – odhlášení (GET/POST)
app.MapGet("/auth/signout", async (SignInManager<User> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/");
});

app.MapPost("/auth/signout", async (SignInManager<User> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/");
});

app.MapFallbackToPage("/_Host");

app.Run();
