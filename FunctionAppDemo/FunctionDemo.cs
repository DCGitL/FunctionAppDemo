using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppDemo;

public class FunctionDemo
{
    private readonly ILogger<FunctionDemo> _logger;

    public FunctionDemo(ILogger<FunctionDemo> logger)
    {
        _logger = logger;
    }

    [Function("FunctionDemo")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        using var sr = new StreamReader(req.Body);
        string data = await sr.ReadToEndAsync();
        if (!string.IsNullOrWhiteSpace(data))
        {
            _logger.LogInformation(data);
        }
        else
        {
            _logger.LogInformation("Request Body is empty.");
        }
        return new OkResult();
    }
}