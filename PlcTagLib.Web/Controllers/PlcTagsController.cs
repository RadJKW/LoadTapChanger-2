using Microsoft.AspNetCore.Mvc;
using PlcTagLib.PlcTags.Commands;
using PlcTagLib.PlcTags.DTOs;
using PlcTagLib.PlcTags.Queries;

namespace PlcTagLib.Web.Controllers;
public class PlcTagsController : ApiControllerBase
{
    /// <summary>
    /// Get All Tags
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<TagList>> Get()
    {
        return await Mediator.Send(new GetTagsQuery());
    }

    /// <summary>
    /// Get Plc{id} Details
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TagDetailsDto>> Get(int id)
    {
        return await Mediator.Send(new GetTagDetailsQuery(id));
    }

    /// <summary>
    /// Exports a Plc{id}'s Tags to CSV file
    /// </summary>
    /// <param name="plcId"></param>
    /// <returns></returns>
    [HttpGet("file/{plcId}")]
    public async Task<FileResult> GetFile(int plcId)
    {
        var vm = await Mediator.Send(new ExportPlcTagsQuery(plcId));

        return File(vm.Content, vm.ContentType, vm.FileName);
    }

    /// <summary>
    /// Create PlcTag
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTagCommand command)
    {
        return await Mediator.Send(command);
    }

    /// <summary>
    /// Update PlcTag
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    [HttpPut("{id}")]

    public async Task<ActionResult<TagDto>> Update(int id, TagUpdateDto tag)
    {
        var command = new UpdateTagCommand(id, tag);

        if (id != command.Id)
        {
            return BadRequest();
        }

        return Ok(await Mediator.Send(command));
    }



}
