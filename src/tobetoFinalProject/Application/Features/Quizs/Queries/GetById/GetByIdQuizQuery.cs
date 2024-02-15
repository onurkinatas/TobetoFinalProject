using Application.Features.Quizs.Constants;
using Application.Features.Quizs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Quizs.Constants.QuizsOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Quizs.Queries.GetById;

public class GetByIdQuizQuery : IRequest<GetByIdQuizResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdQuizQueryHandler : IRequestHandler<GetByIdQuizQuery, GetByIdQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly QuizBusinessRules _quizBusinessRules;

        public GetByIdQuizQueryHandler(IMapper mapper, IQuizRepository quizRepository, QuizBusinessRules quizBusinessRules)
        {
            _mapper = mapper;
            _quizRepository = quizRepository;
            _quizBusinessRules = quizBusinessRules;
        }

        public async Task<GetByIdQuizResponse> Handle(GetByIdQuizQuery request, CancellationToken cancellationToken)
        {
            Quiz? quiz = await _quizRepository.GetAsync(
                include:q=>q.Include(q=>q.QuizQuestions),
                predicate: q => q.Id == request.Id, 
                cancellationToken: cancellationToken);
            await _quizBusinessRules.QuizShouldExistWhenSelected(quiz);

            GetByIdQuizResponse response = _mapper.Map<GetByIdQuizResponse>(quiz);
            response.QuizQuestionCount=quiz.QuizQuestions.Count;
            return response;
        }
    }
}