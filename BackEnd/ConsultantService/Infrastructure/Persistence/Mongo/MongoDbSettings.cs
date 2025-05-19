using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Mongo;

public sealed class MongoDbSettings
{
    public string ConnectionString { get; init; } = default!;
    public string DatabaseName { get; init; } = default!;
}
