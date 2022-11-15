using PlcTagLib.PlcTags.DTOs;

namespace PlcTagLib.Common.Interfaces;

public interface ICsvService
{
    byte[] BuildPlcTagsFile(IEnumerable<TagRecord> records);
}
