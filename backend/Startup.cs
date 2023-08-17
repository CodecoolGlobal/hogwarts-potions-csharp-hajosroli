using System;
using HogwartsPotions.Data;
using HogwartsPotions.Models;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HogwartsPotions;

public class Startup
{
    private readonly IWebHostEnvironment _env;
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        _env = env;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(o => o.AddPolicy("AllowOrigin", options =>
        {
            options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        
        string connectionString = _env.IsDevelopment()
            ? "DefaultConnection"
            : "DockerCommandsConnectionString";

        Console.WriteLine(connectionString);
        services.AddDbContext<HogwartsContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString(connectionString ?? throw new InvalidOperationException("no connectionString"))));
        
       
        
        services.AddTransient<IRoomService, RoomService>();
        services.AddTransient<IStudentService, StudentService>();
        services.AddTransient<IPotionService, PotionService>();
        services.AddTransient<IRecipeService, RecipeService>();
        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<HogwartsContext>();
            if (context.Database.EnsureCreated())
            {
                HogwartsContextSeed.ContextSeed(context);
            }
            
        }
        if (env.IsDevelopment())
        {
           
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();
        }
        
        app.UseCors("AllowOrigin");       
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
        });
    }
}