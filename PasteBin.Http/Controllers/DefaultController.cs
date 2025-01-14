﻿using Microsoft.AspNetCore.Mvc;
using PasteBin.Common;

namespace PasteBin.Http.Controllers;
public abstract class DefaultController : ControllerBase
{
    protected IActionResult Success<T>(T result) => Ok(new ResponseDto
    {
        IsSuccess = true,
        Result = result
    });
}
