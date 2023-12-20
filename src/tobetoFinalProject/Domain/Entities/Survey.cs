using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Survey:Entity<Guid>
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SurveyUrl { get; set; }
    public string Description { get; set; }    
    public virtual ICollection<ClassSurvey>? ClassSurveys { get; set; }   
}
