using System;
using System.Linq;
using Cards.Application.Abstraction.Dictionaries;
using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers;

[Route("admin")]
public class AdminController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnectionStringProvider _connectionStringProvider;
    private readonly IDictionary _dictionary;

    public AdminController(IConfiguration configuration,
        IServiceProvider serviceProvider,
        IConnectionStringProvider connectionStringProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
        _connectionStringProvider = connectionStringProvider;
    }
    
    


    [HttpGet("env")]
    public IActionResult Env()
        => Ok(_configuration.AsEnumerable().Select(
            x => new { x.Key, x.Value }
        ));

    [HttpGet("connectionString")]
    public IActionResult ConnectionString()
        => Ok(_connectionStringProvider.ConnectionString);
}