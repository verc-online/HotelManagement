using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using HotelAppLibrary.Data;
using HotelAppLibrary.Databases;

namespace HotelAppDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            services.AddTransient<MainWindow>();
            services.AddTransient<CheckInForm>();

            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<ISqlLiteDataAccess, SqlLiteDataAccess>();


            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();

            services.AddSingleton(configuration);
            string dbChoice = configuration.GetValue<string>("DatabaseChoice").ToLower();
            if (dbChoice == "sql")
            {
                services.AddTransient<IDatabaseData, SqlData>();
            }
            else if (dbChoice == "sqlite")
            {
                services.AddTransient<IDatabaseData, SqliteData>();
            }
            else
            {
                // Fallback / Default value
                services.AddTransient<IDatabaseData, SqlData>();
            }

            serviceProvider = services.BuildServiceProvider();

            var mainWindow = serviceProvider.GetService<MainWindow>();

            mainWindow.Show();
        }
    }
}