using BusinessObject;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MutantOrchidTradingSysRazorPage;

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
        services.AddSignalR();
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
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<CategoryRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<ProductCategoryRepository>();
        services.AddScoped<IAuctionRepository, AuctionRepository>();
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<BidRepository>();
        services.AddScoped<IDepositRequestRepository, DepositRequestRepository>();
        services.AddScoped<IDeductionRequestRepository, DeductionRequestRepository>();
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
            endpoints.MapControllerRoute(
            name: "manageProductEdit",
            pattern: "/Admin/ManageProduct/Edit/{productId?}",
            defaults: new { controller = "ManageProduct", action = "Edit" });

            endpoints.MapRazorPages();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapHub<SignalServer>("/signalrServer");
          
        });


    }
}