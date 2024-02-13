using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LectureViews.Queries.GelAllForLoggedStudent;
public class GetAllLectureViewForLoggedStudentItemDto : IDto
{
    public ICollection<DateTime> LectureViewCreatedDates { get; set; }
    public int TotalContentCountForClass { get; set; }
}

