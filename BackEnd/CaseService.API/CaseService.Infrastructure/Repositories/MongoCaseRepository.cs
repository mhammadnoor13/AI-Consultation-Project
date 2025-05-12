using CaseService.API.CaseService.Application.Interfaces;
using CaseService.API.CaseService.Domain.Entities;
using MongoDB.Driver;

namespace CaseService.API.CaseService.Infrastructure.Repositories
{
    public class MongoCaseRepository : ICaseRepository
    {
        private readonly IMongoCollection<Case> _collection;

        public MongoCaseRepository(IMongoDatabase database)
        {
            // Assumes you injected IMongoDatabase via DI
            _collection = database.GetCollection<Case>("Cases");
        }

        public Task SaveAsync(Case c, CancellationToken ct)
        {
            // Upsert: replaces existing document or inserts new one
            return _collection.ReplaceOneAsync(
                filter: Builders<Case>.Filter.Eq(x => x.Id, c.Id),
                replacement: c,
                options: new ReplaceOptions { IsUpsert = true },
                cancellationToken: ct);
        }

        public Task<Case?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return _collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IEnumerable<Case>> GetBySpecialityAsync(string speciality, CancellationToken ct)
        {
            var bySpeciality = Builders<Case>.Filter.Eq(x => x.Speciality, speciality);
            var byStatus = Builders<Case>.Filter.Eq(x => x.Status, "Submitted");
            var filter = Builders<Case>.Filter.And(bySpeciality, byStatus);
            var list = await _collection.Find(filter).ToListAsync(ct);
            return list;
        }

        public Task DeleteAsync(Guid id, CancellationToken ct)
        {
            return _collection.DeleteOneAsync(x => x.Id == id, ct);
        }
    }
}
