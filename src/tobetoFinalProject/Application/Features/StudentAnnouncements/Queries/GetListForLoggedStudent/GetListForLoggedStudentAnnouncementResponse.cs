using Core.Application.Dtos;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentAnnouncements.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentAnnouncementResponse : IDto
{
    public Guid? AnnouncementId { get; set; }
    public string AnnouncementName { get; set; }
    public string AnnouncementDescription { get; set; }
    public string StudentClassName { get; set; }
    public DateTime AnnouncementCreatedDate { get; set; }
}

