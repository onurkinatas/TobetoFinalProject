using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentAnnouncements.Queries.GetListByAnnouncementId;
public class GetListByAnnouncementIdStudentAnnouncementResponse:IDto
{
    public Guid AnnouncementId { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string StudentEmail { get; set; }
    public string AnnouncementName { get; set; }
}

