using System;
using System.IO;
using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using AWSProductListDynamoDb.Libs.AWSProductListDynamoDb;

namespace AWSProductListDynamoDb
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public IConfigurationRoot Configuration { get; set; } 
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());

            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID",Configuration["AWS:AccessKey"]);
            Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY",Configuration["AWS:SecretKey"]);
            Environment.SetEnvironmentVariable("AWS_REGION", Configuration["AWS:Region"]);
            Environment.SetEnvironmentVariable("AWS_CONTENT", Configuration["VAR:TableName"]);

            services.AddAWSService<IAmazonDynamoDB>();

            services.AddSingleton<IAWSProductListDynamoDbExamples, AWSProductListDynamoDbExamples>();
            services.AddSingleton<IInsertItem, InsertItem>();
            services.AddSingleton<IQueryItem, QueryItem>();
            services.AddSingleton<IDeleteItem, DeleteItem>();
            services.AddSingleton<IUpdateItem, UpdateItem>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });

        }
    }
}
