using Example.BusinessLogic.Infrastructure;
using Example.BusinessLogic.Services;
using Example.DataAccess;
using Example.DataAccess.Infrastructure;
using Example.DataAccess.Repositories;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Register dependency implementations.
        ConfigureServices(builder);

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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

    }

    /// <summary>
    /// Inject dependencies for services and repositories.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder to build the app.</param>
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();

        // The dependency on the real repository was commented out, and mock is used instead, as we don't have the actual DB in this demo.
        // builder.Services.AddScoped<IUserRepository, UserRepository>();

        // Use the mock repository. Set its lifetime to Singleton to keep mocked data in the application's memory.
        builder.Services.AddSingleton<IUserRepository, MockUserRepository>();

        builder.Services.AddScoped<IUserService, UserService>();
    }

}
