using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.MicrologixPlcs.DTOs;
using PlcTagLib.PlcTags.DTOs;

namespace PlcTagLib.Services;
public class CsvService : ICsvService
{
    /// <summary>
    /// Builds a CSV file from a collection of PlcTagRecord objects.
    /// </summary>
    /// <param name="records"></param>
    /// <returns></returns>
    public byte[] BuildPlcTagsFile(IEnumerable<TagRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            csvWriter.Context.RegisterClassMap<PlcTagRecordMap>();
            csvWriter.WriteRecords(records);
        }


        return memoryStream.ToArray();
    }

}
