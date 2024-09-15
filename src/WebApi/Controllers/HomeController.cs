﻿using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.WebApi.Controllers;

#if DEBUG
[Route("/")]
[ApiExplorerSettings(IgnoreApi = true)]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
#endif