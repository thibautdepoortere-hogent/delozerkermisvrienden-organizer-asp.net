using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DeLozerkermisVrienden.Organizer.API.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DeLozerkermisVrienden.Organizer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;

            if (isDevelopment)
            {
                //// START Development:
                ///////////////////////////////////////////////////////////////////////
                //// ENKEL TE GEBRUIKEN WANNEER VERBONDEN MET LOCALDB DATABASE!!!! ////
                ///////////////////////////////////////////////////////////////////////
                //var host = CreateHostBuilder(args).Build();

                //using (var scope = host.Services.CreateScope())
                //{
                //    try
                //    {
                //        var organizerContext = scope.ServiceProvider.GetService<OrganizerContext>();
                //        //organizerContext.Database.EnsureDeleted();
                //        organizerContext.Database.Migrate();
                //    }
                //    catch (Exception ex)
                //    {
                //        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                //        logger.LogError(ex, "An error occured while migration was in progress");
                //    }
                //}

                //host.Run();
                //// END Development


                // START Production
                CreateHostBuilder(args).Build().Run();
                // END Production
            }
            else
            {
                // START Production
                CreateHostBuilder(args).Build().Run();
                // END Production
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var isDevelopment = environment == Environments.Development;

                    if (!isDevelopment) {
                        var keyVaultEndpoint = "";
                        if (isDevelopment)
                        {
                            keyVaultEndpoint = GetKeyVaultEndpointDevelopment();
                        }
                        else
                        {
                            keyVaultEndpoint = GetKeyVaultEndpointProduction();
                        };
                        if (!string.IsNullOrEmpty(keyVaultEndpoint))
                        {
                            var azureServiceTokenProvider = new AzureServiceTokenProvider();
                            var keyVaultClient = new KeyVaultClient(
                                new KeyVaultClient.AuthenticationCallback(
                                    azureServiceTokenProvider.KeyVaultTokenCallback));
                            config.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
                        }
                    }
                    
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static string GetKeyVaultEndpointDevelopment() => "https://PPH-DATA-KV.vault.azure.net/";
        private static string GetKeyVaultEndpointProduction() => "https://dlv-org-data-keyvault.vault.azure.net/";
    }
}
