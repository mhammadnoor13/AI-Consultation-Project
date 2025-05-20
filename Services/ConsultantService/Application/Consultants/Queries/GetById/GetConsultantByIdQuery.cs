using Application.Consultants.DTOs;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Consultants.Queries.GetById;

public sealed record GetConsultantByIdQuery (Guid Id):IRequest<Result<ConsultantResponse>>;
