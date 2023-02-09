using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Prototype.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    
}
