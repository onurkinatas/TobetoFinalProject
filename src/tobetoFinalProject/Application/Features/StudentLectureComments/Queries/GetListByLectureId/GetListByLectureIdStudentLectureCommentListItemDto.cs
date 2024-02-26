using Application.Features.CommentSubComments.Queries.GetById;
using Application.Features.CommentSubComments.Queries.GetList;
using Application.Features.StudentLectureComments.Queries.Dtos;
using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentLectureComments.Queries.GetListByLectureId;
public class GetListByLectureIdStudentLectureCommentListItemDto : IDto
{
    public int Id { get; set; }
    public string ProfilePhotoPath { get; set; }
    public Guid StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Comment { get; set; }
    public ICollection<StudentSubCommentDto>? CommentSubComments { get; set; }
    public bool IsDeletable { get; set; }
    public DateTime CreatedDate { get; set; }
}
