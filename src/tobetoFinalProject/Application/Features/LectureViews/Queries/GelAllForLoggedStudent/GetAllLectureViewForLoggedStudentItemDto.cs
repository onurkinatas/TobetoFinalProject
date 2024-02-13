using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LectureViews.Queries.GelAllForLoggedStudent;
public class GetAllLectureViewForLoggedStudentItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public Guid ContentId { get; set; }
}

