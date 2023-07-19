using Example.Domain.Service;
using Nest;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Example.Api.Extensions
{
    public static class KafkaTopicExtensions
    {
        public static void CreateTopic(this IServiceCollection _, IConfiguration configuration)
        {
            string bootstrapServers = configuration["KafkaConfiguration:bootstrapServers"];
            string topicName = configuration["KafkaConfiguration:topicName"];
            int numPartitions = Convert.ToInt32(configuration["KafkaConfiguration:numPartitions"]);
            short replicationFactor =  Convert.ToInt16(configuration["KafkaConfiguration:replicationFactor"]);
            
            var adminClientConfig = new AdminClientConfig { BootstrapServers = bootstrapServers };

            using (var adminClient = new AdminClientBuilder(adminClientConfig).Build())
            {
                var topicExists = adminClient.GetMetadata(TimeSpan.FromSeconds(5)).Topics
                    .Exists(t => t.Topic == topicName);

                if (topicExists)
                {
                    Console.WriteLine($"El tema '{topicName}' ya existe");
                }
                else
                {
                    try
                    {
                        adminClient.CreateTopicsAsync(new TopicSpecification[]
                        {
                            new TopicSpecification
                            {
                                Name = topicName,
                                NumPartitions = numPartitions,
                                ReplicationFactor = replicationFactor
                            }
                        }).Wait();
                        Console.WriteLine($"Tema '{topicName}' creado correctamente");
                    }
                    catch (CreateTopicsException ex)
                    {
                        Console.WriteLine($"Error al crear el tema: {ex.Message}");
                    }
                }
            }
        }
    }
}