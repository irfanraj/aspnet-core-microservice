using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizMaker.Abstractions.Options;
using QuizMaker.DataAccess;
using QuizMaker.Services.Services;

namespace QuizMaker.Services
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension method to register services classes.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddQuizMakerServices(this IServiceCollection services)
        {
            services
                .AddSingleton(typeof(IQuizService), typeof(QuizService));
                
            //Registers services from DataAccess Proj.
            services.AddDataAccessServices();

            return services;    
        }

        /// <summary>
        /// Register appsettings config 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddQuizMakerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AwsSettingOptions>(configuration.GetSection("AwsSetting"));
            
            //Read connection string info from Secret Manager and stores as config.
            configuration.GetSection("DatabaseConnectionOptions:ConnectionString").Value = GetConnStrFromSecretManager(configuration.GetSection("AwsSetting:SecretKey").Value);
            
            services.Configure<DatabaseConnectionOptions>(configuration.GetSection("DatabaseConnectionOptions"));
            
            return services;
        }

        /// <summary>
        /// Read connection string information from AWS secret Manager and stores into Configuration
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        private static string GetConnStrFromSecretManager(string secretKey)
        {
            string connStr = string.Empty;
            string region = "us-east-1";

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretKey,
                VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
            };

            GetSecretValueResponse response;

            // In this sample we only handle the specific exceptions for the 'GetSecretValue' API.
            // See https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
            // We rethrow the exception by default.
            try
            {
                response = client.GetSecretValueAsync(request).Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception trying to retrieve secret file {secretKey} from region {region}.", ex);
            }

            // Decrypts secret using the associated KMS CMK.
            // Depending on whether the secret is a string or binary, one of these fields will be populated.
            if (response?.SecretString != null)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                SecretConnInfo connInfo = JsonSerializer.Deserialize<SecretConnInfo>(response.SecretString, options);
                connStr = $"Server={connInfo.HostName};Database={connInfo.DatabaseName};User Id={connInfo.WebUser};Password={connInfo.WebUserPassword}";

            }

            return connStr;
        }

    }
}
