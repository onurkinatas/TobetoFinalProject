using Core.Persistence.Repositories;
using System.Data;

namespace Domain.Entities;

public class Content : Entity<Guid>
{
    public string Name { get; set; }
    public Guid LanguageId { get; set; }
    public Guid SubTypeId { get; set; }    
    public string VideoUrl { get; set; }
    public string Description { get; set; }  
    public Guid ManufacturerId { get; set; }
    public int Duration { get; set; }
    public Guid? ContentCategoryId { get; set; }
    public virtual SubType? SubType { get; set; }
    public virtual ContentCategory? ContentCategory { get; set; }
    public virtual ICollection<ContentInstructor>? ContentInstructors { get; set; }
    public virtual ICollection<CourseContent>? CourseContents { get; set; }
    public virtual Manufacturer? Manufacturer { get; set; }
    public virtual Language? Language { get; set; }
    public virtual ICollection<ContentTag>? ContentTags { get; set; }
}
