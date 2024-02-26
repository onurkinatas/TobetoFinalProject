using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentLectureComments.Queries.Dtos;
public class StudentUserCommentDto:IDto
{
    public string ProfilePhotoPath { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
}

