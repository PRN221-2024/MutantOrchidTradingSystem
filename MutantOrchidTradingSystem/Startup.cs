﻿using BusinessObject;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSession();
        services.AddRazorPages();
        services.AddHttpContextAccessor();
        services.AddDbContext<AuctionItemDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionStr")));
        services.AddScoped<ProductObject>();
        services.AddScoped<ProductRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<AccountRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<AccountObject>();
        services.AddScoped<IRoleAccountRepository, RoleAccountRepository>();
        services.AddScoped<RoleAccountRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<OrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<OrderDetailRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseSession();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}