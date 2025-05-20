using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Consultants.Queries.GetAll;

public sealed record GetAllConsultantsQuery()
    : IRequest<Result<IEnumerable<Domain.Entities.Consultant>>>;
