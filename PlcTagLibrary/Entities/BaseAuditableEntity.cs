using System.ComponentModel.DataAnnotations.Schema;

namespace PlcTagLib.Entities;

public abstract class BaseAuditableEntity : BaseEntity
{

    [Column(TypeName = "datetime2(0)")]
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime2(0)")]
    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
