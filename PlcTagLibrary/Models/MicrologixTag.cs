﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PlcTagLibrary.Models
{
    public partial class MicrologixTag
    {
        public int TagId { get; set; }
        public string CustomName { get; set; }
        public string ConfiguredName { get; set; }
        public int? Value { get; set; }
        public string TagType { get; set; }
        public int? PlcDeviceId { get; set; }

        public virtual MicrologixPlc PlcDevice { get; set; }
    }
}
