using Example.Domain.Service;
using Nest;


namespace Example.Api.Extensions
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ELKConfiguration:Uri"];
            var defaultIndex = configuration["ELKConfiguration:index"];

            var settings = new ConnectionSettings(new Uri(url)).PrettyJson().DefaultIndex(defaultIndex);

            //AddDefaultMappings(settings);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<PermissionDto>(p =>
                p.Ignore(x => x.LastNameEmployee)
                    //.Ignore(x=>x.Id)
                    //.Ignore(x=>x.TypePermit)
                    .Ignore(x => x.DatePermission));
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i.Map<PermissionDto>(x => x.AutoMap()));
        }

    }
}
