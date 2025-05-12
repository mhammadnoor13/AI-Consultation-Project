using ConsultantService.ConsultantService.Application.Interfaces;
using ConsultantService.ConsultantService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace ConsultantService.ConsultantService.Infrastructure.Repositories
{
    public class MongoConsultantRepository : IConsultantService
    {
        private readonly IMongoCollection<Consultant> _collection;

        public MongoConsultantRepository(IMongoDatabase database)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Consultant)))
            {
                BsonClassMap.RegisterClassMap<Consultant>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);

                    // tell Mongo how to store the Id
                    cm.MapIdMember(c => c.Id)
                      .SetSerializer(new GuidSerializer(BsonType.String));
                });
            }
            _collection = database.GetCollection<Consultant>("consultants");
        }

        public async Task AddAsync(Consultant consultant)
        {
            await _collection.InsertOneAsync(consultant);
        }

        public async Task UpdateAsync(Consultant consultant)
        {
            var filter = Builders<Consultant>.Filter.Eq(c => c.Id, consultant.Id);
            await _collection.ReplaceOneAsync(filter, consultant);
        }

        public async Task<Consultant> GetByIdAsync(Guid id)
        {
            return await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Consultant>> ListBySpecialtyAsync(
            string specialty,
            bool onlyActive = true)
        {
            var filter = Builders<Consultant>.Filter.Eq(c => c.Specialty, specialty);

            return await _collection.Find(filter).ToListAsync();
        }
    }
}
