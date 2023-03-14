using MySql.Data.MySqlClient;
using SushiProject;
using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies; //Added

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDbConnection>((s) =>
{
    IDbConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("sushirestaurant"));
    conn.Open();
    return conn;
});

builder.Services.AddTransient<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddTransient<IFoodBevIngredientRepository, FoodBevIngredientRepository>();
builder.Services.AddTransient<IAccessRepository, AccessRepository>();
builder.Services.AddTransient<IRestaurantTableRepository, RestaurantTableRepository>();
builder.Services.AddTransient<IFoodBevOrderRepository, FoodBevOrderRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => { option.LoginPath = "/Access/Login"; option.ExpireTimeSpan = TimeSpan.FromMinutes(60);});//Automatic Logout after 60 minutes

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");
    //pattern: "{controller=Home}/{action=Index}/{id?}"); // Previous

app.Run();
