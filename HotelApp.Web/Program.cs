using HotelAppLibrary.Data;
using HotelAppLibrary.Databases;

namespace HotelApp.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        
        // Tranzient - creates an instance everytime we ask for it
        // We want every one to have it's own connection to database
        // Ex. Every time u want an IDatabaseData it will give new SqlData instance
        builder.Services.AddTransient<IDatabaseData, SqlData>();
        builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}