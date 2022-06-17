using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts =>
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:SportsStoreConnection"])
);

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();
app.UseStaticFiles();

app.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });

app.MapControllerRoute("page","Page{productPage:int}",new {Controller="Home",action="Index",productPage=1});

app.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("pagination","Products/Page{productPage}",new {Controller="Home",action="Index",productPage=1});

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

// Configure the HTTP request pipeline.
/*if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();*/

app.Run();