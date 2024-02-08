using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class StudentPrivateCertificate:Entity<Guid>
{

    public Guid StudentId { get; set; }
    public string CertificateUrl { get; set; }
    public virtual Student? Student { get; set; }
}

