using CollectionsProject.Context;
using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories;
using CollectionsProject.Services.Implementation;
using CollectionsProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Owin.Security.Cookies;
using System.Globalization;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        string connection = builder.Configuration.GetConnectionString("DefaultConnection");
        ServerVersion version = ServerVersion.AutoDetect(connection);
        // 1. 
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
        // 2. 
        builder.Services.AddControllersWithViews()
            .AddViewLocalization
            (LanguageViewLocationExpanderFormat.SubFolder)
            .AddDataAnnotationsLocalization();

        // 3. 
        builder.Services.Configure<RequestLocalizationOptions>(options => {
            var supportedCultures = new[] { "en", "pl"};
            options.SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
        });
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(connection, version));
        builder.Services.AddIdentity<User, IdentityRole>(opts =>
        {
            opts.Password.RequiredLength = 1;
            opts.Password.RequireNonAlphanumeric = false;
            opts.Password.RequireLowercase = false;
            opts.Password.RequireUppercase = false;
            opts.Password.RequireDigit = false;
            opts.User.RequireUniqueEmail = true;

        }).AddEntityFrameworkStores<ApplicationContext>();

        builder.Services.Configure<SecurityStampValidatorOptions>(opts =>
        {
            opts.ValidationInterval = TimeSpan.Zero;
        });

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<IItemService, ItemService>();
        builder.Services.AddScoped<ICollectionService, CollectionService>();
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<ITagRepository, TagRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<IHomeService, HomeService>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await RoleInitializer.InitializeAsync(userManager, rolesManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
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
        var supportedCultures = new[] { "en", "pl"};
        // 5. 
        // Culture from the HttpRequest
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}