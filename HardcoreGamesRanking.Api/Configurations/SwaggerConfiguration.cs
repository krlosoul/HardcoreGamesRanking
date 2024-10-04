namespace HardcoreGamesRanking.Api.Configurations
{
    using Swashbuckle.AspNetCore.SwaggerUI;
    using Api.Filters;
    using Microsoft.OpenApi.Models;

    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerSettings = configuration.GetSection("Swagger");
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerSettings["Name"], new OpenApiInfo
                {
                    Title = swaggerSettings["Title"],
                    Version = swaggerSettings["Version"]
                });
                options.AddSecurityDefinition(swaggerSettings["SecurityName"], new OpenApiSecurityScheme
                {
                    Description = swaggerSettings["DescriptionToken"],
                    Name = swaggerSettings["HeaderName"],
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = swaggerSettings["Scheme"],
                    BearerFormat = swaggerSettings["BearerFormat"]
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
            });
        }

        public static void AddSwaggerConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerSettings = configuration.GetSection("Swagger");
            var url = swaggerSettings["Url"];

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint(url, swaggerSettings["DefinitionName"]);
                c.DocumentTitle = swaggerSettings["DocumentTitle"];
                c.DocExpansion(DocExpansion.None);
            });
        }

    }
}