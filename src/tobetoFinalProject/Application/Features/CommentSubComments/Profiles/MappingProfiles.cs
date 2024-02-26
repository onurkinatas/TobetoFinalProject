using Application.Features.CommentSubComments.Commands.Create;
using Application.Features.CommentSubComments.Commands.Delete;
using Application.Features.CommentSubComments.Commands.Update;
using Application.Features.CommentSubComments.Queries.GetById;
using Application.Features.CommentSubComments.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.CommentSubComments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CommentSubComment, CreateCommentSubCommentCommand>().ReverseMap();
        CreateMap<CommentSubComment, CreatedCommentSubCommentResponse>().ReverseMap();
        CreateMap<CommentSubComment, UpdateCommentSubCommentCommand>().ReverseMap();
        CreateMap<CommentSubComment, UpdatedCommentSubCommentResponse>().ReverseMap();
        CreateMap<CommentSubComment, DeleteCommentSubCommentCommand>().ReverseMap();
        CreateMap<CommentSubComment, DeletedCommentSubCommentResponse>().ReverseMap();
        CreateMap<CommentSubComment, GetByIdCommentSubCommentResponse>().ReverseMap();
        CreateMap<CommentSubComment, GetListCommentSubCommentListItemDto>().ReverseMap();
        CreateMap<IPaginate<CommentSubComment>, GetListResponse<GetListCommentSubCommentListItemDto>>().ReverseMap();
    }
}