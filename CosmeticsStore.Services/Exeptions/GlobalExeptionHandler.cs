using CosmeticsStore.Services.Schema;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CosmeticsStore.Services.Exeptions;

public class GlobalExeptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExeptionHandler> _logger;

    public GlobalExeptionHandler(ILogger<GlobalExeptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var serviceResponse = new ServiceResponse();
        _logger.LogError(exception, "Exeption occurred: {Message}", exception.Message);

        serviceResponse.SetSucceeded(false)
            .SetStatusCode(StatusCodes.Status500InternalServerError)
            .AddDetail("message", "Có lỗi xảy ra, vui lòng thử lại sau!")
            .AddError("outOfService", "Hệ thống đang gặp sự cố, vui lòng thử lại sau!");

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(serviceResponse, cancellationToken);

        return true;
    }
}