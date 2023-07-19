using Example.Domain.Entities;
using Example.Domain.Service;
using Microsoft.EntityFrameworkCore;
using Nest;
using Confluent.Kafka;
using System.Text.Json;

namespace Example.Repository
{
    public class BaseRepository<T> : Domain.Service.IRepository<T> where T : class
    {
        private readonly RegistrationPermissionContext _context;
        private readonly IElasticClient _elasticClient;
        private static readonly ProducerConfig Config = new ProducerConfig { BootstrapServers = "localhost:9092" };
        private static readonly IProducer<Null, string> Producer = new ProducerBuilder<Null, string>(Config).Build();

        public BaseRepository(RegistrationPermissionContext context, IElasticClient elasticClient)
        {
            _context = context;
            _elasticClient = elasticClient;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<PermissionDto>> GetAllElasticAsync()
        {
            var results = await _elasticClient.SearchAsync<PermissionDto>(
                s => s.Query(
                    q => q.MatchAll()).Size(1000)
            );
            return results.Documents.ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateElasticAsync(Permission permission)
        {
            await _elasticClient.IndexDocumentAsync(permission);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateElasticAsync(Permission permission)
        {
            await _elasticClient.UpdateAsync<Permission>(permission.Id, u => u
                .Doc(new Permission
                {
                    Id = permission.Id,
                    EmployeeName = permission.EmployeeName,
                    LastNameEmployee = permission.LastNameEmployee,
                    TypePermit = permission.TypePermit,
                    DatePermission = permission.DatePermission
                })
            );
        }


        public void SendMessageTopic(OperationDto operationDto)
        {
            var topic = "permission-topic";
            var message = new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(operationDto)
            };

            try
            {
                Producer.ProduceAsync(topic, message).Wait();
                Console.WriteLine("Mensaje enviado correctamente");
            }
            catch (ProduceException<Null, string> ex)
            {
                Console.WriteLine($"Error al enviar el mensaje: {ex.Message}");
            }
        }
    }
}