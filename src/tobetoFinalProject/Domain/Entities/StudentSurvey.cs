using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class StudentSurvey : Entity<Guid>
{
    public Guid SurveyId { get; set; }
    public Guid StudentId { get; set; }
    public virtual Survey? Survey { get; set; }
    public virtual Student? Student { get; set; }
}
