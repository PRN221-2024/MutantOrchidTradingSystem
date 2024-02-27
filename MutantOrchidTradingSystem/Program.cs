using BusinessObject;
using DataAccess.Models;
using DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AuctionItemDBContext>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ProductObject>();
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddSession(options =>
{
    // configure session options here if needed
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseRouting();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/Index");
    return Task.CompletedTask;
});

app.Run();

