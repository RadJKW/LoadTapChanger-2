
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PlcTagLibrary.Models;
public class MicrologixTag
{
    [Key]
    int Id { get; set; }
    /// <summary>
    /// User Defined Name for the Tag
    /// </summary>
    [Required]
    string CustomName { get; set; }

    /// <summary>
    /// Name of the tag that is used within the Plc Program --> Example 'I1:0/1'
    /// </summary>
    [Required]
    string LookupName { get; set; }

    int Value { get; set; }

}
