using System;
using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseAPIController
{

    [HttpGet("unauthorized")]
    public ActionResult<string> GetUnauthorized()
    {
        return Unauthorized("unauthorized");
    }

    [HttpGet("notfound")]
    public ActionResult<string> GetNotFound()
    {

        return NotFound();

    }


    [HttpGet("badrequest")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("vary bad request");
    }

    [HttpGet("internalerror")]
    public ActionResult<string> Getinternalerror()
    {
        throw new Exception("server error");
    }


    [HttpPost("validationerror")]
    public ActionResult<string> Getvalidationerror(createProductDto product)
    {
        return Ok();
    }


}
