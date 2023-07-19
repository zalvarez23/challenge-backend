using Example.Domain.Entities;
using Example.Domain.Service;
using Nest;

namespace Example.Repository
{
    public class TypePermitRepository : BaseRepository<TypePermit>, ITTypePermitRepository
    {
        public TypePermitRepository(RegistrationPermissionContext context, IElasticClient elasticClient) : base(context,
            elasticClient)
        {
        }
    }
}