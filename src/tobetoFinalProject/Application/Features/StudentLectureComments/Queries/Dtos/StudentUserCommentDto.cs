using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentLectureComments.Queries.Dtos;
public class StudentSubCommentDto:IDto
{
    public int Id { get; set; }
    public string ProfilePhotoPath { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Comment { get; set; }
    public int StudentLectureCommentId { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid StudentId { get; set; }
    public bool? IsDeletable { get; set; }
}

