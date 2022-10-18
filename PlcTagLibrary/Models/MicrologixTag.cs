
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlcTagLibrary.Models
{
    public class MicrologixTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// User Defined Name for the Tag
        /// </summary>
        public string? CustomName { get; set; }

        /// <summary>
        /// Name of the tag that is used within the Plc Program --> Example 'I1:0/1'
        /// </summary>
        public string? LookupName { get; set; }

        public TagType TagType { get; set; }

        public int? Value { get; set; }

        public int PlcId { get; set; }

    }
}