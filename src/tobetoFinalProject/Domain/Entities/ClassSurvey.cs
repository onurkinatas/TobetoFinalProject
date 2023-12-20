using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class ClassSurvey:Entity<Guid>
{
    public Guid StudentClassId { get; set; }
    public Guid SurveyId { get; set; }
    public virtual StudentClass? StudentClass { get; set; }
    public virtual Survey? Survey { get; set; }
}
