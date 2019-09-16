using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;

namespace AzureKeyVaultDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration((context, config) =>
                   {
                       var builtConfig = config.Build();
                       config.AddAzureKeyVault(
                           $"https://{builtConfig["azureKeyVault:vault"]}.vault.azure.net/",
                           builtConfig["azureKeyVault:clientId"],
                           builtConfig["azureKeyVault:clientSecret"],
                           new DefaultKeyVaultSecretManager());
                   })
                   .UseStartup<Startup>();

    }
}
// $"https://{builtConfig["azureKeyVault:vault"]}.vault.azure.net/",
//builtConfig["azureKeyVault:clientId"],
//                          builtConfig["azureKeyVault:clientSecret"],    