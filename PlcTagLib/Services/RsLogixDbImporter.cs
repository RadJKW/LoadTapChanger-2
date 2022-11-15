// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;
using PlcTagLib.Enums;

namespace PlcTagLib.Services;

public class RslogixDbImporter : IRsLogixDbImporter
{
    public List<PlcTag> PlcTags => _plcTags;

    private readonly List<PlcTag> _plcTags = new();
    private bool _isValid = false;
    private bool _isChild = false;

    /// <summary>
    ///    Initializes a new instance of the <see cref="RslogixDbImporter" /> class.
    /// </summary>
    /// <param name="csvFilePath"></param>
    /// <param name="jsonFilePath"></param>
    /// <param name="addressColumn"></param>
    /// <param name="symbolColumn"></param>
    /// <param name="descriptionColumns"></param>
    /// <param name="plc"></param>
    public void Convert(Uri csvFilePath, Uri jsonFilePath, int addressColumn, int symbolColumn, int[] descriptionColumns, MicrologixPlc plc)
    {
        // if the plc is null throw an exception
        if (plc == null)
        {
            throw new ArgumentNullException(nameof(plc));
        }

        plc.Program = csvFilePath.Segments[^1];

        using var reader = new StreamReader(csvFilePath.LocalPath);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line!.Split(',');

            if (values[symbolColumn] != string.Empty)
            {
                _isValid = true;
                _isChild = false;
            }
            else if (values[addressColumn].Split('/')[0] == _plcTags[^1].Address)
            {
                _isValid = true;
                _isChild = true;
            }

            if (_isValid)
            {
                var useCsv = values[symbolColumn];
                var plcTag = new PlcTag
                {
                    Address = values[addressColumn],
                    SymbolName = _isChild ? _plcTags[^1].SymbolName + $"_{values[addressColumn].Split('/')[1]}" : useCsv,
                    Description = GetDescription(values, descriptionColumns),
                    PlcId = plc.Id,
                    TagTypeId = GetTagTypeId(values[addressColumn])

                };

                _plcTags.Add(plcTag);
                _isValid = false;
                _isChild = false;
            }
        }
        plc.PlcTags = _plcTags;
        File.WriteAllText(jsonFilePath.LocalPath, string.Empty);
        var json = JsonConvert.SerializeObject(plc, Formatting.Indented);
        File.WriteAllText(jsonFilePath.LocalPath, json);
    }

    private static TagTypeId GetTagTypeId(string address)
    {
        // get teh first letter of the address
        var firstLetter = address[0];

        // return the TagTypeId based on the first letter of the address
        return firstLetter switch
        {
            'B' => TagTypeId.Binary,
            'I' => TagTypeId.Input,
            'O' => TagTypeId.Output,
            'S' => TagTypeId.Status,
            'T' => TagTypeId.Timer,
            'C' => TagTypeId.Counter,
            'R' => TagTypeId.Control,
            'N' => TagTypeId.Integer,
            _ => TagTypeId.Unknown
        };

    }

    private static string GetDescription(string[] values, int[] dataColumns)
    {
        var description = string.Empty;
        foreach (var column in dataColumns)
        {
            description += values[column] + " ";
        }
        return description
                .Replace("\"", string.Empty)
                .Replace("\\", string.Empty)
                .Trim();
    }
}

