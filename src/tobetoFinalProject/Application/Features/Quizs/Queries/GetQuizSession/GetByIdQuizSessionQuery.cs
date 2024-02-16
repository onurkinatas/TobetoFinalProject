using Application.Features.Quizs.Queries.GetById;
using Application.Features.Quizs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Quizs.Queries.GetQuizSession;
public class GetByIdQuizSessionQuery : IRequest<GetByIdQuizSessionResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { "Admin","Student" };

    public class GetByIdQuizSessionQueryHandler : IRequestHandler<GetByIdQuizSessionQuery, GetByIdQuizSessionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly QuizBusinessRules _quizBusinessRules;

        public GetByIdQuizSessionQueryHandler(IMapper mapper, IQuizRepository quizRepository, QuizBusinessRules quizBusinessRules)
        {
            _mapper = mapper;
            _quizRepository = quizRepository;
            _quizBusinessRules = quizBusinessRules;
        }

        public async Task<GetByIdQuizSessionResponse> Handle(GetByIdQuizSessionQuery request, CancellationToken cancellationToken)
        {
            Quiz? quiz = await _quizRepository.GetAsync(
                include: q => q
                    .Include(q => q.QuizQuestions)
                    .ThenInclude(qq=>qq.Question)
                    .ThenInclude(q=>q.QuestionOptions)
                    .ThenInclude(qo=>qo.Option),
                predicate: q => q.Id == request.Id,
                cancellationToken: cancellationToken);
            await _quizBusinessRules.QuizShouldExistWhenSelected(quiz);

            GetByIdQuizSessionResponse response = _mapper.Map<GetByIdQuizSessionResponse>(quiz);

            return response;
        }
    }
}

