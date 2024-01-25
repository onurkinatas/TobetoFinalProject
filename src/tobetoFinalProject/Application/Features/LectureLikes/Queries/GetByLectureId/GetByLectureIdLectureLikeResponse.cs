using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LectureLikes.Queries.GetByLectureId;
public class GetByLectureIdLectureLikeResponse
{
    public Guid Id { get; set; }
    public bool? IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
}

