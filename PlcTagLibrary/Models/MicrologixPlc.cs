// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PlcTagLibrary.Models
{
    public partial class MicrologixPlc
    {
        public MicrologixPlc()
        {
            MicrologixTags = new HashSet<MicrologixTag>();
        }

        public int PlcId { get; set; }
        public string Name { get; set; }
        public int? DefaultName { get; set; }
        public string Gateway { get; set; }
        public short TimeoutSeconds { get; set; }
        public string PlcType { get; set; }
        public string Protocol { get; set; }

        public virtual ICollection<MicrologixTag> MicrologixTags { get; set; }
    }
}
