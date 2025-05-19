// Infrastructure/Persistence/Mongo/MongoConsultantRepository.cs
using Domain.Entities;
using Domain.Shared;
using MongoDB.Bson;
using MongoDB.Driver;
using Application.Common.Persistence;


namespace Infrastructure.Persistence.Mongo
{
    public class MongoConsultantRepository : IConsultantRepository
    {
        private readonly IMongoCollection<Consultant> _collection;

        public MongoConsultantRepository(IMongoDatabase database)
        {
            // ensure the database & collection names match your config
            _collection = database.GetCollection<Consultant>("Consultants");
        }

        public async Task<Result<Unit>> AddAsync(Consultant consultant, CancellationToken ct = default)
        {
            try
            {
                await _collection.InsertOneAsync(consultant, cancellationToken: ct);
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                // surface the error so you can see it in logs if needed
                return Result<Unit>.Failure($"Failed to insert consultant: {ex.Message}");
            }
        }

        public async Task<Result<Consultant>> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var filter = Builders<Consultant>.Filter.Eq(c => c.Id, id);
                var consultant = await _collection
                    .Find(filter)
                    .FirstOrDefaultAsync(ct);

                return consultant is not null
                    ? Result<Consultant>.Success(consultant)
                    : Result<Consultant>.Failure($"Consultant with id {id} not found.");
            }
            catch (Exception ex)
            {
                return Result<Consultant>.Failure($"Error fetching consultant: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<Consultant>>> GetAllAsync(CancellationToken ct = default)
        {
            try
            {
                var all = await _collection
                    .Find(Builders<Consultant>.Filter.Empty)
                    .ToListAsync(ct);

                return Result<IEnumerable<Consultant>>.Success(all);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Consultant>>.Failure($"Error fetching consultants: {ex.Message}");
            }
        }

        public Task<Result<IEnumerable<Consultant>>> GetBySpecialityAsync(string speciality, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
